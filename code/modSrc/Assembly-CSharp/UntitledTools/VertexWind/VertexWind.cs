using System;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledTools.VertexWind
{
	// Token: 0x020003B0 RID: 944
	public class VertexWind : MonoBehaviour
	{
		// Token: 0x060022AA RID: 8874 RVA: 0x0016D55C File Offset: 0x0016B75C
		private void Start()
		{
			this.effectorObjs = UnityEngine.Object.FindObjectsOfType<WindEffectorRadius>();
			this.effectors = new VertexWind.WindEffector[this.effectorObjs.Length];
			for (int i = 0; i < this.effectorObjs.Length; i++)
			{
				this.effectors[i] = default(VertexWind.WindEffector);
			}
			this.windShader = Resources.Load<ComputeShader>("WindShader");
			this.doWindCalcId = this.windShader.FindKernel("DoWindCalc");
			if (this.useMeshCombination)
			{
				this.instancedMeshes = this.CombineMeshes(this.objs);
				this.objsOriginalVerts = new Vector3[this.instancedMeshes.Length][];
				for (int j = 0; j < this.instancedMeshes.Length; j++)
				{
					this.instancedMeshes[j].MarkDynamic();
					this.objsOriginalVerts[j] = this.instancedMeshes[j].vertices;
				}
				for (int k = 0; k < this.objs.Count; k++)
				{
					this.objs[k].gameObject.SetActive(false);
				}
				return;
			}
			this.instancedMeshes = new Mesh[this.objs.Count];
			this.objsOriginalVerts = new Vector3[this.objs.Count][];
			for (int l = 0; l < this.objs.Count; l++)
			{
				this.instancedMeshes[l] = UnityEngine.Object.Instantiate<Mesh>(this.objs[l].mesh);
				this.instancedMeshes[l].MarkDynamic();
				this.objsOriginalVerts[l] = this.instancedMeshes[l].vertices;
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0016D6E8 File Offset: 0x0016B8E8
		private void Update()
		{
			for (int i = 0; i < this.effectorObjs.Length; i++)
			{
				this.effectors[i].pos = this.effectorObjs[i].transform.position;
				this.effectors[i].radius = this.effectorObjs[i].radius;
				this.effectors[i].strength = this.effectorObjs[i].amount;
			}
			if (this.useMeshCombination)
			{
				for (int j = 0; j < this.instancedMeshes.Length; j++)
				{
					this.instancedMeshes[j].vertices = this.CalcNewVerts(this.objsOriginalVerts[j], this.newObjects[j].transform.position);
					this.newObjects[j].mesh = this.instancedMeshes[j];
				}
				return;
			}
			for (int k = 0; k < this.objs.Count; k++)
			{
				this.instancedMeshes[k].vertices = this.CalcNewVerts(this.objsOriginalVerts[k], this.objs[k].transform.position);
				this.objs[k].mesh = this.instancedMeshes[k];
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0016D830 File Offset: 0x0016BA30
		private Vector3[] CalcNewVerts(Vector3[] verts, Vector3 objectPos)
		{
			ComputeBuffer computeBuffer = new ComputeBuffer(verts.Length, 12);
			computeBuffer.SetData(verts);
			this.windShader.SetFloat("time", Time.time * this.speed);
			this.windShader.SetFloat("scale", this.scale);
			this.windShader.SetVector("amount", this.amount);
			this.windShader.SetVector("objPos", objectPos);
			this.windShader.SetBuffer(this.doWindCalcId, "verts", computeBuffer);
			if (this.effectors.Length != 0)
			{
				ComputeBuffer computeBuffer2 = new ComputeBuffer(this.effectors.Length, 28);
				computeBuffer2.SetData(this.effectors);
				this.windShader.SetBuffer(this.doWindCalcId, "effectors", computeBuffer2);
				this.windShader.SetBool("effectorsExist", true);
				this.windShader.Dispatch(this.doWindCalcId, verts.Length, this.effectors.Length, 1);
				computeBuffer2.Release();
			}
			else
			{
				ComputeBuffer computeBuffer3 = new ComputeBuffer(1, 16);
				this.windShader.SetBuffer(this.doWindCalcId, "effectors", computeBuffer3);
				this.windShader.SetBool("effectorsExist", false);
				this.windShader.Dispatch(this.doWindCalcId, verts.Length, 1, 1);
				computeBuffer3.Release();
			}
			Vector3[] array = new Vector3[verts.Length];
			computeBuffer.GetData(array);
			computeBuffer.Release();
			return array;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0016D9A0 File Offset: 0x0016BBA0
		private Mesh[] CombineMeshes(List<MeshFilter> meshes)
		{
			int num = 0;
			int num2 = 0;
			List<Mesh> list = new List<Mesh>();
			List<CombineInstance> list2 = new List<CombineInstance>();
			for (int i = 0; i < meshes.Count; i++)
			{
				Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(meshes[i].mesh);
				num += mesh.vertexCount;
				if (num < 65535)
				{
					CombineInstance item = default(CombineInstance);
					Vector3[] array = new Vector3[mesh.vertexCount];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = mesh.vertices[j] + meshes[i].transform.position;
					}
					item.mesh = mesh;
					item.mesh.vertices = array;
					list2.Add(item);
				}
				else
				{
					Mesh mesh2 = new Mesh
					{
						name = "Combined Mesh " + i
					};
					mesh2.CombineMeshes(list2.ToArray(), true, false, false);
					list2 = new List<CombineInstance>();
					list.Add(mesh2);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(meshes[num2].gameObject);
					gameObject.transform.position = Vector3.zero;
					gameObject.name = string.Concat(new object[]
					{
						"Combined Meshes ",
						num2,
						" - ",
						i
					});
					gameObject.GetComponent<MeshFilter>().mesh = mesh2;
					gameObject.hideFlags = HideFlags.HideInHierarchy;
					if (gameObject.GetComponent<Collider>())
					{
						UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
						gameObject.AddComponent<MeshCollider>().sharedMesh = mesh2;
					}
					this.newObjects.Add(gameObject.GetComponent<MeshFilter>());
					num2 = i;
					num = 0;
					i--;
				}
			}
			Mesh mesh3 = new Mesh
			{
				name = "Combined Final Mesh"
			};
			mesh3.CombineMeshes(list2.ToArray(), true, false, false);
			list.Add(mesh3);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(meshes[num2].gameObject);
			gameObject2.transform.position = Vector3.zero;
			gameObject2.name = string.Concat(new object[]
			{
				"Combined Meshes ",
				num2,
				" - ",
				meshes.Count
			});
			gameObject2.GetComponent<MeshFilter>().mesh = mesh3;
			gameObject2.hideFlags = HideFlags.HideInHierarchy;
			UnityEngine.Object.Destroy(gameObject2.GetComponent<Collider>());
			gameObject2.AddComponent<MeshCollider>().sharedMesh = mesh3;
			this.newObjects.Add(gameObject2.GetComponent<MeshFilter>());
			return list.ToArray();
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x00017278 File Offset: 0x00015478
		public void ObjsAddChildren()
		{
			this.objs.AddRange(base.GetComponentsInChildren<MeshFilter>());
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0001728B File Offset: 0x0001548B
		public void ObjsAddCurrent()
		{
			if (base.GetComponent<MeshFilter>() != null)
			{
				this.objs.Add(base.GetComponent<MeshFilter>());
			}
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x00002098 File Offset: 0x00000298
		public void ObjsAddSelected()
		{
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0016DC38 File Offset: 0x0016BE38
		public void ObjsAreTagged()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this.objectTag);
			List<MeshFilter> list = new List<MeshFilter>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].GetComponent<MeshFilter>())
				{
					list.Add(array[i].GetComponent<MeshFilter>());
				}
			}
			this.objs.AddRange(list);
		}

		// Token: 0x04002CFC RID: 11516
		public List<MeshFilter> objs = new List<MeshFilter>();

		// Token: 0x04002CFD RID: 11517
		public float speed = 10f;

		// Token: 0x04002CFE RID: 11518
		public float scale = 1f;

		// Token: 0x04002CFF RID: 11519
		public bool useMeshCombination = true;

		// Token: 0x04002D00 RID: 11520
		public Vector3 amount = Vector3.one * 0.5f;

		// Token: 0x04002D01 RID: 11521
		private Mesh[] instancedMeshes;

		// Token: 0x04002D02 RID: 11522
		private Vector3[][] objsOriginalVerts;

		// Token: 0x04002D03 RID: 11523
		private List<MeshFilter> newObjects = new List<MeshFilter>();

		// Token: 0x04002D04 RID: 11524
		private WindEffectorRadius[] effectorObjs;

		// Token: 0x04002D05 RID: 11525
		private VertexWind.WindEffector[] effectors;

		// Token: 0x04002D06 RID: 11526
		private ComputeShader windShader;

		// Token: 0x04002D07 RID: 11527
		private int doWindCalcId;

		// Token: 0x04002D08 RID: 11528
		public string objectTag = string.Empty;

		// Token: 0x04002D09 RID: 11529
		public bool showAdvancedSelection;

		// Token: 0x04002D0A RID: 11530
		public bool showObjectsList = true;

		// Token: 0x04002D0B RID: 11531
		public MeshFilter selectedObj;

		// Token: 0x020003B1 RID: 945
		public struct WindEffector
		{
			// Token: 0x04002D0C RID: 11532
			public Vector3 pos;

			// Token: 0x04002D0D RID: 11533
			public Vector3 strength;

			// Token: 0x04002D0E RID: 11534
			public float radius;
		}
	}
}
