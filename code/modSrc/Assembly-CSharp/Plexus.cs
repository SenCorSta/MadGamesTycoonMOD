using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class Plexus : MonoBehaviour
{
	// Token: 0x06000086 RID: 134 RVA: 0x0001B098 File Offset: 0x00019298
	private void Start()
	{
		this.lineMaterial.SetVector("_BoxDims", new Vector4(this.box.x, this.box.y, this.box.z, 1f));
		this.positions = new Vector3[this.amountOfPoints];
		this.defaultPositions = new Vector3[this.amountOfPoints];
		for (int i = 0; i < this.amountOfPoints; i++)
		{
			this.positions[i] = new Vector3(UnityEngine.Random.Range(-this.box.x, this.box.x), UnityEngine.Random.Range(-this.box.y, this.box.y), UnityEngine.Random.Range(-this.box.z, this.box.z));
			this.defaultPositions[i] = this.positions[i];
		}
		this.lineMesh = new Mesh();
		int[] triangles = new int[]
		{
			0,
			1,
			2,
			3,
			2,
			1
		};
		this.lineMesh.vertices = this.verts;
		this.lineMesh.triangles = triangles;
		this.velocities = new Vector3[this.amountOfPoints];
		base.StartCoroutine(this.ConnectDots());
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00002657 File Offset: 0x00000857
	private void Update()
	{
		this.MovePoints();
		this.RenderLines();
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0001B1FC File Offset: 0x000193FC
	private void MovePoints()
	{
		int kernelIndex = this.plexus.FindKernel("MoveParticels");
		ComputeBuffer computeBuffer = new ComputeBuffer(this.positions.Length, 12);
		computeBuffer.SetData(this.positions);
		this.plexus.SetBuffer(kernelIndex, "positions", computeBuffer);
		ComputeBuffer computeBuffer2 = new ComputeBuffer(this.defaultPositions.Length, 12);
		computeBuffer2.SetData(this.defaultPositions);
		this.plexus.SetBuffer(kernelIndex, "defaultPositions", computeBuffer2);
		ComputeBuffer computeBuffer3 = new ComputeBuffer(this.velocities.Length, 12);
		computeBuffer3.SetData(this.velocities);
		this.plexus.SetBuffer(kernelIndex, "velocities", computeBuffer3);
		this.plexus.SetFloat("deltaTime", Time.deltaTime);
		this.plexus.SetFloat("elapsedTime", Time.time);
		this.plexus.SetFloat("particleSpeed", this.particleSpeed);
		this.plexus.Dispatch(kernelIndex, this.positions.Length, 1, 1);
		computeBuffer.GetData(this.positions);
		computeBuffer.Release();
		computeBuffer2.Release();
		computeBuffer3.Release();
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0001B318 File Offset: 0x00019518
	private static float DistanceSqr(Vector3 p1, Vector3 p2)
	{
		return (p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y) + (p1.z - p2.z) * (p1.z - p2.z);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0001B378 File Offset: 0x00019578
	private void RenderLines()
	{
		this.lineMesh = new Mesh();
		for (int i = 0; i < this.connected.Count; i++)
		{
			this.p1 = this.positions[this.connected[i].Key];
			this.p2 = this.positions[this.connected[i].Value];
			this.normal = Vector3.Cross(this.p1, this.p2);
			this.side = Vector3.Cross(this.normal, this.p2 - this.p1);
			this.side.Normalize();
			this.startingVerticesIndex = this.lineVerts.Count;
			this.verts[0] = this.p1 + this.side * (this.lineWidth / 2f);
			this.verts[1] = this.p1 + this.side * (this.lineWidth / -2f);
			this.verts[2] = this.p2 + this.side * (this.lineWidth / 2f);
			this.verts[3] = this.p2 + this.side * (this.lineWidth / -2f);
			this.trigs[0] = this.startingVerticesIndex;
			this.trigs[1] = (this.trigs[5] = this.startingVerticesIndex + 1);
			this.trigs[2] = (this.trigs[4] = this.startingVerticesIndex + 2);
			this.trigs[3] = this.startingVerticesIndex + 3;
			this.lineVerts.AddRange(this.verts);
			this.lineTrigs.AddRange(this.trigs);
		}
		this.lineMesh.vertices = this.lineVerts.ToArray();
		this.lineMesh.triangles = this.lineTrigs.ToArray();
		this.lineMesh.RecalculateBounds();
		Graphics.DrawMesh(this.lineMesh, base.transform.localToWorldMatrix, this.lineMaterial, 0);
		this.lineTrigs.Clear();
		this.lineVerts.Clear();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00002665 File Offset: 0x00000865
	private IEnumerator ConnectDots()
	{
		WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
		int indx = 0;
		this.maxConnDistanceSqr = this.maxConnDistance * this.maxConnDistance;
		do
		{
			yield return wfeof;
			for (int i = 0; i < this.PPPS; i++)
			{
				Vector3 vector = this.positions[indx];
				this.connected.RemoveAll((KeyValuePair<int, int> x) => x.Key == indx || x.Value == indx);
				this.connectedHashSet.RemoveWhere((KeyValuePair<int, int> x) => x.Key == indx || x.Value == indx);
				for (int j = 0; j < this.amountOfPoints; j++)
				{
					if (j != indx && Plexus.DistanceSqr(vector, this.positions[j]) < this.maxConnDistanceSqr)
					{
						KeyValuePair<int, int> item = new KeyValuePair<int, int>(indx, j);
						if (this.connectedHashSet.Add(item))
						{
							this.connected.Add(new KeyValuePair<int, int>(indx, j));
						}
					}
				}
				int indx2 = indx + 1;
				indx = indx2;
				if (indx >= this.amountOfPoints)
				{
					indx = 0;
				}
			}
		}
		while (!this.isEnabled);
		yield break;
	}

	// Token: 0x04000099 RID: 153
	public ComputeShader plexus;

	// Token: 0x0400009A RID: 154
	public int amountOfPoints = 100;

	// Token: 0x0400009B RID: 155
	public int PPPS = 2;

	// Token: 0x0400009C RID: 156
	public float lineWidth = 0.02f;

	// Token: 0x0400009D RID: 157
	public Material lineMaterial;

	// Token: 0x0400009E RID: 158
	public Vector3 box = new Vector3(4f, 4f, 4f);

	// Token: 0x0400009F RID: 159
	public float particleSpeed = 1f;

	// Token: 0x040000A0 RID: 160
	public float maxConnDistance = 3f;

	// Token: 0x040000A1 RID: 161
	private float maxConnDistanceSqr;

	// Token: 0x040000A2 RID: 162
	private Vector3[] defaultPositions;

	// Token: 0x040000A3 RID: 163
	private Vector3[] velocities;

	// Token: 0x040000A4 RID: 164
	private Vector3[] positions;

	// Token: 0x040000A5 RID: 165
	private Mesh lineMesh;

	// Token: 0x040000A6 RID: 166
	private Vector3 normal;

	// Token: 0x040000A7 RID: 167
	private Vector3 side;

	// Token: 0x040000A8 RID: 168
	private Vector3 p1;

	// Token: 0x040000A9 RID: 169
	private Vector3 p2;

	// Token: 0x040000AA RID: 170
	private int startingVerticesIndex;

	// Token: 0x040000AB RID: 171
	private List<int> lineTrigs = new List<int>();

	// Token: 0x040000AC RID: 172
	private List<Vector3> lineVerts = new List<Vector3>();

	// Token: 0x040000AD RID: 173
	private Vector3[] verts = new Vector3[4];

	// Token: 0x040000AE RID: 174
	private int[] trigs = new int[6];

	// Token: 0x040000AF RID: 175
	[HideInInspector]
	public bool isEnabled;

	// Token: 0x040000B0 RID: 176
	private List<KeyValuePair<int, int>> connected = new List<KeyValuePair<int, int>>();

	// Token: 0x040000B1 RID: 177
	private HashSet<KeyValuePair<int, int>> connectedHashSet = new HashSet<KeyValuePair<int, int>>();
}
