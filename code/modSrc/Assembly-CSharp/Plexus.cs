using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plexus : MonoBehaviour
{
	
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

	
	private void Update()
	{
		this.MovePoints();
		this.RenderLines();
	}

	
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

	
	private static float DistanceSqr(Vector3 p1, Vector3 p2)
	{
		return (p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y) + (p1.z - p2.z) * (p1.z - p2.z);
	}

	
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

	
	public ComputeShader plexus;

	
	public int amountOfPoints = 100;

	
	public int PPPS = 2;

	
	public float lineWidth = 0.02f;

	
	public Material lineMaterial;

	
	public Vector3 box = new Vector3(4f, 4f, 4f);

	
	public float particleSpeed = 1f;

	
	public float maxConnDistance = 3f;

	
	private float maxConnDistanceSqr;

	
	private Vector3[] defaultPositions;

	
	private Vector3[] velocities;

	
	private Vector3[] positions;

	
	private Mesh lineMesh;

	
	private Vector3 normal;

	
	private Vector3 side;

	
	private Vector3 p1;

	
	private Vector3 p2;

	
	private int startingVerticesIndex;

	
	private List<int> lineTrigs = new List<int>();

	
	private List<Vector3> lineVerts = new List<Vector3>();

	
	private Vector3[] verts = new Vector3[4];

	
	private int[] trigs = new int[6];

	
	[HideInInspector]
	public bool isEnabled;

	
	private List<KeyValuePair<int, int>> connected = new List<KeyValuePair<int, int>>();

	
	private HashSet<KeyValuePair<int, int>> connectedHashSet = new HashSet<KeyValuePair<int, int>>();
}
