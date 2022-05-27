using System;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledTools.VertexWind
{
	// Token: 0x020003B3 RID: 947
	public class VertexWind : MonoBehaviour
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x0016EFF4 File Offset: 0x0016D1F4
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

		// Token: 0x060022FE RID: 8958 RVA: 0x0016F180 File Offset: 0x0016D380
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

		// Token: 0x060022FF RID: 8959 RVA: 0x0016F2C8 File Offset: 0x0016D4C8
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

		// Token: 0x06002300 RID: 8960 RVA: 0x0016F438 File Offset: 0x0016D638
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

		// Token: 0x06002301 RID: 8961 RVA: 0x0016F6D0 File Offset: 0x0016D8D0
		public void ObjsAddChildren()
		{
			this.objs.AddRange(base.GetComponentsInChildren<MeshFilter>());
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0016F6E3 File Offset: 0x0016D8E3
		public void ObjsAddCurrent()
		{
			if (base.GetComponent<MeshFilter>() != null)
			{
				this.objs.Add(base.GetComponent<MeshFilter>());
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00002715 File Offset: 0x00000915
		public void ObjsAddSelected()
		{
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0016F704 File Offset: 0x0016D904
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

		// Token: 0x04002D12 RID: 11538
		public List<MeshFilter> objs = new List<MeshFilter>();

		// Token: 0x04002D13 RID: 11539
		public float speed = 10f;

		// Token: 0x04002D14 RID: 11540
		public float scale = 1f;

		// Token: 0x04002D15 RID: 11541
		public bool useMeshCombination = true;

		// Token: 0x04002D16 RID: 11542
		public Vector3 amount = Vector3.one * 0.5f;

		// Token: 0x04002D17 RID: 11543
		private Mesh[] instancedMeshes;

		// Token: 0x04002D18 RID: 11544
		private Vector3[][] objsOriginalVerts;

		// Token: 0x04002D19 RID: 11545
		private List<MeshFilter> newObjects = new List<MeshFilter>();

		// Token: 0x04002D1A RID: 11546
		private WindEffectorRadius[] effectorObjs;

		// Token: 0x04002D1B RID: 11547
		private VertexWind.WindEffector[] effectors;

		// Token: 0x04002D1C RID: 11548
		private ComputeShader windShader;

		// Token: 0x04002D1D RID: 11549
		private int doWindCalcId;

		// Token: 0x04002D1E RID: 11550
		public string objectTag = string.Empty;

		// Token: 0x04002D1F RID: 11551
		public bool showAdvancedSelection;

		// Token: 0x04002D20 RID: 11552
		public bool showObjectsList = true;

		// Token: 0x04002D21 RID: 11553
		public MeshFilter selectedObj;

		// Token: 0x020003B4 RID: 948
		public struct WindEffector
		{
			// Token: 0x04002D22 RID: 11554
			public Vector3 pos;

			// Token: 0x04002D23 RID: 11555
			public Vector3 strength;

			// Token: 0x04002D24 RID: 11556
			public float radius;
		}
	}
}
