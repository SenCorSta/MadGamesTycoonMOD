using System;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledTools.VertexWind
{
	
	public class VertexWind : MonoBehaviour
	{
		
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

		
		public void ObjsAddChildren()
		{
			this.objs.AddRange(base.GetComponentsInChildren<MeshFilter>());
		}

		
		public void ObjsAddCurrent()
		{
			if (base.GetComponent<MeshFilter>() != null)
			{
				this.objs.Add(base.GetComponent<MeshFilter>());
			}
		}

		
		public void ObjsAddSelected()
		{
		}

		
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

		
		public List<MeshFilter> objs = new List<MeshFilter>();

		
		public float speed = 10f;

		
		public float scale = 1f;

		
		public bool useMeshCombination = true;

		
		public Vector3 amount = Vector3.one * 0.5f;

		
		private Mesh[] instancedMeshes;

		
		private Vector3[][] objsOriginalVerts;

		
		private List<MeshFilter> newObjects = new List<MeshFilter>();

		
		private WindEffectorRadius[] effectorObjs;

		
		private VertexWind.WindEffector[] effectors;

		
		private ComputeShader windShader;

		
		private int doWindCalcId;

		
		public string objectTag = string.Empty;

		
		public bool showAdvancedSelection;

		
		public bool showObjectsList = true;

		
		public MeshFilter selectedObj;

		
		public struct WindEffector
		{
			
			public Vector3 pos;

			
			public Vector3 strength;

			
			public float radius;
		}
	}
}
