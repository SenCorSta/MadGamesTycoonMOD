using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E3 RID: 995
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	public class Distort : MonoBehaviour
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x00018277 File Offset: 0x00016477
		private void Awake()
		{
			this.animDistort = base.GetComponent<AnimatedDistort>();
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00002098 File Offset: 0x00000298
		private void Reset()
		{
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0016EBDC File Offset: 0x0016CDDC
		private void OnEnable()
		{
			if (this.meshList == null)
			{
				this.SetVertices();
			}
			if (Application.isPlaying)
			{
				foreach (MeshDistortData meshDistortData in this.meshList)
				{
					if (meshDistortData.skin != null)
					{
						meshDistortData.skin.enabled = false;
						this.hasSkinnedMesh = true;
					}
				}
			}
			this.UpdateDistort();
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0016EC64 File Offset: 0x0016CE64
		public UnityEngine.Object[] GetAllMeshes()
		{
			List<UnityEngine.Object> list = new List<UnityEngine.Object>();
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				list.Add(meshDistortData.mesh);
			}
			return list.ToArray();
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0016ECC8 File Offset: 0x0016CEC8
		private void SetDebugLines()
		{
			int num = 10;
			this.debugLines = new Vector3[12, num];
			float num2 = 0.2f;
			Bounds bounds = this.combinedBounds;
			Vector3 vector = bounds.extents;
			if (vector.x < num2)
			{
				vector.x = num2;
			}
			if (vector.y < num2)
			{
				vector.y = num2;
			}
			if (vector.z < num2)
			{
				vector.z = num2;
			}
			bounds.extents = vector;
			vector *= this.debugLinesDistance;
			for (int i = 0; i < num; i++)
			{
				float num3 = bounds.max.x - bounds.min.x;
				float num4 = bounds.max.y - bounds.min.y;
				float num5 = bounds.max.z - bounds.min.z;
				float x = bounds.min.x + num3 * ((float)i / (float)num);
				float y = bounds.min.y + num4 * ((float)i / (float)num);
				float z = bounds.min.z + num5 * ((float)i / (float)num);
				this.debugLines[0, i] = new Vector3(x, bounds.center.y + vector.y, bounds.center.z);
				this.debugLines[1, i] = new Vector3(x, bounds.center.y - vector.y, bounds.center.z);
				this.debugLines[2, i] = new Vector3(x, bounds.center.y, bounds.center.z + vector.z);
				this.debugLines[3, i] = new Vector3(x, bounds.center.y, bounds.center.z - vector.z);
				this.debugLines[4, i] = new Vector3(bounds.center.x + vector.x, y, bounds.center.z);
				this.debugLines[5, i] = new Vector3(bounds.center.x - vector.x, y, bounds.center.z);
				this.debugLines[6, i] = new Vector3(bounds.center.x, y, bounds.center.z + vector.z);
				this.debugLines[7, i] = new Vector3(bounds.center.x, y, bounds.center.z - vector.z);
				this.debugLines[8, i] = new Vector3(bounds.center.x + vector.x, bounds.center.y, z);
				this.debugLines[9, i] = new Vector3(bounds.center.x - vector.x, bounds.center.y, z);
				this.debugLines[10, i] = new Vector3(bounds.center.x, bounds.center.y + vector.y, z);
				this.debugLines[11, i] = new Vector3(bounds.center.x, bounds.center.y - vector.y, z);
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0016F068 File Offset: 0x0016D268
		private void SetVertices()
		{
			this.meshList = new List<MeshDistortData>();
			foreach (MeshFilter meshFilter in base.GetComponentsInChildren<MeshFilter>())
			{
				MeshDistortData item = new MeshDistortData(meshFilter.transform, meshFilter.GetComponent<Renderer>().sharedMaterial, meshFilter);
				this.meshList.Add(item);
			}
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in base.GetComponentsInChildren<SkinnedMeshRenderer>())
			{
				MeshDistortData item2 = new MeshDistortData(skinnedMeshRenderer.transform, skinnedMeshRenderer.sharedMaterial, skinnedMeshRenderer);
				this.meshList.Add(item2);
			}
			this.SetBounds();
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0016F104 File Offset: 0x0016D304
		private void SetBounds()
		{
			this.combinedBounds = default(Bounds);
			MeshFilter[] componentsInChildren = base.GetComponentsInChildren<MeshFilter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Bounds bounds = componentsInChildren[i].GetComponent<Renderer>().bounds;
				if (this.combinedBounds.size.magnitude == 0f)
				{
					this.combinedBounds = bounds;
				}
				else
				{
					this.combinedBounds.Encapsulate(bounds);
				}
			}
			SkinnedMeshRenderer[] componentsInChildren2 = base.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				Bounds bounds2 = componentsInChildren2[i].bounds;
				if (this.combinedBounds.size.magnitude == 0f)
				{
					this.combinedBounds = bounds2;
				}
				else
				{
					this.combinedBounds.Encapsulate(bounds2);
				}
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0016F1C4 File Offset: 0x0016D3C4
		private void ResetVertices()
		{
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				meshDistortData.ResetMesh();
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00002098 File Offset: 0x00000298
		public void EditParameters()
		{
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0016F214 File Offset: 0x0016D414
		public void LateUpdate()
		{
			if (base.transform.hasChanged)
			{
				this.UpdateDistort();
				base.transform.hasChanged = false;
				return;
			}
			if (this.hasSkinnedMesh && (this.animDistort == null || !this.animDistort.enabled || !this.animDistort.isPlaying))
			{
				this.UpdateDistort();
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00018285 File Offset: 0x00016485
		public void UpdateDistort()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.UpdateInCPU();
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0016F278 File Offset: 0x0016D478
		public void UpdateInCPU()
		{
			Vector3 position = base.transform.position;
			Vector3 localScale = base.transform.localScale;
			Quaternion rotation = base.transform.rotation;
			if (this.calculation == Distort.Calculate.local)
			{
				base.transform.position = Vector3.zero;
				base.transform.localScale = Vector3.one;
				base.transform.rotation = Quaternion.identity;
			}
			this.SetBounds();
			base.GetComponent<Animator>() != null;
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				if (!(meshDistortData.meshTransform == null))
				{
					if (meshDistortData.mesh == null)
					{
						meshDistortData.UpdateMesh();
					}
					Vector3[] array = meshDistortData.skinVertices.Clone() as Vector3[];
					int num = array.Length;
					Matrix4x4 localToWorldMatrix = meshDistortData.localToWorldMatrix;
					Matrix4x4 worldToLocalMatrix = meshDistortData.worldToLocalMatrix;
					for (int i = 0; i < num; i++)
					{
						array[i] = localToWorldMatrix.MultiplyPoint3x4(array[i]);
					}
					if (base.enabled)
					{
						foreach (DistortData distortData in this.distort)
						{
							if (distortData.enabled)
							{
								distortData.SetBounds(this.combinedBounds);
								for (int j = 0; j < num; j++)
								{
									distortData.DistortVertice(ref array[j]);
								}
							}
						}
					}
					for (int k = 0; k < num; k++)
					{
						array[k] = worldToLocalMatrix.MultiplyPoint3x4(array[k]);
					}
					meshDistortData.mesh.vertices = array;
					meshDistortData.mesh.RecalculateNormals();
					if (Application.isPlaying && meshDistortData.skin != null && this.calculation == Distort.Calculate.global)
					{
						Matrix4x4 matrix = Matrix4x4.TRS(meshDistortData.meshTransform.position, meshDistortData.meshTransform.rotation, Vector3.one);
						Graphics.DrawMesh(meshDistortData.mesh, matrix, meshDistortData.originalMaterial, 0);
					}
				}
			}
			if (this.calculation == Distort.Calculate.local)
			{
				base.transform.position = position;
				base.transform.localScale = localScale;
				base.transform.rotation = rotation;
				foreach (MeshDistortData meshDistortData2 in this.meshList)
				{
					if (Application.isPlaying && meshDistortData2.skin != null)
					{
						Matrix4x4 matrix2 = Matrix4x4.TRS(meshDistortData2.meshTransform.position, meshDistortData2.meshTransform.rotation, meshDistortData2.meshTransform.lossyScale);
						Graphics.DrawMesh(meshDistortData2.mesh, matrix2, meshDistortData2.originalMaterial, 0);
					}
				}
			}
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0016F5B4 File Offset: 0x0016D7B4
		public void UpdateDebugLines()
		{
			this.SetDebugLines();
			for (int i = 0; i < this.debugLines.GetLength(0); i++)
			{
				for (int j = 0; j < this.debugLines.GetLength(1); j++)
				{
					Vector3 vector = this.debugLines[i, j];
					for (int k = 0; k < this.distort.Count; k++)
					{
						if (this.distort[k].enabled)
						{
							this.distort[k].DistortVertice(ref vector);
						}
					}
					this.debugLines[i, j] = vector;
				}
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0016F650 File Offset: 0x0016D850
		public void MakeDynamic()
		{
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				meshDistortData.mesh.MarkDynamic();
			}
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0016F6A8 File Offset: 0x0016D8A8
		public void AddDistortion()
		{
			DistortData item = new DistortData();
			if (this.distort == null)
			{
				this.distort = new List<DistortData>();
			}
			this.distort.Add(item);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x00018296 File Offset: 0x00016496
		public void RemoveDistort(int index)
		{
			this.distort.RemoveAt(index);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0016F6DC File Offset: 0x0016D8DC
		private void OnDrawGizmos()
		{
			if (this.showMeshInEditor && !Application.isPlaying && base.isActiveAndEnabled)
			{
				foreach (MeshDistortData meshDistortData in this.meshList)
				{
					if (meshDistortData.meshTransform != null)
					{
						Gizmos.color = Color.magenta;
						Gizmos.DrawMesh(meshDistortData.mesh, meshDistortData.meshTransform.position, meshDistortData.meshTransform.rotation, meshDistortData.meshTransform.lossyScale);
					}
				}
			}
			if (this.showDebugLines)
			{
				if (this.debugLines == null)
				{
					this.SetDebugLines();
				}
				Vector3 zero = Vector3.zero;
				for (int i = 0; i < this.debugLines.GetLength(0); i++)
				{
					if (i <= 3)
					{
						Gizmos.color = Color.red;
					}
					else if (i <= 7)
					{
						Gizmos.color = Color.green;
					}
					else
					{
						Gizmos.color = Color.blue;
					}
					for (int j = 1; j < this.debugLines.GetLength(1); j++)
					{
						Gizmos.DrawLine(this.debugLines[i, j - 1], this.debugLines[i, j]);
					}
				}
			}
		}

		// Token: 0x04002D85 RID: 11653
		public Distort.Calculate calculation;

		// Token: 0x04002D86 RID: 11654
		[HideInInspector]
		public bool updateIntEditor = true;

		// Token: 0x04002D87 RID: 11655
		public List<DistortData> distort = new List<DistortData>();

		// Token: 0x04002D88 RID: 11656
		[NonSerialized]
		public List<MeshDistortData> meshList;

		// Token: 0x04002D89 RID: 11657
		public Bounds combinedBounds;

		// Token: 0x04002D8A RID: 11658
		public bool showDebugLines;

		// Token: 0x04002D8B RID: 11659
		public float debugLinesDistance = 1f;

		// Token: 0x04002D8C RID: 11660
		public Vector3[,] debugLines;

		// Token: 0x04002D8D RID: 11661
		public bool showMeshInEditor = true;

		// Token: 0x04002D8E RID: 11662
		public bool showPreviewWindow = true;

		// Token: 0x04002D8F RID: 11663
		public bool calculateInGPU;

		// Token: 0x04002D90 RID: 11664
		public ComputeShader distortShader;

		// Token: 0x04002D91 RID: 11665
		protected int dirtortKernel;

		// Token: 0x04002D92 RID: 11666
		private bool hasSkinnedMesh;

		// Token: 0x04002D93 RID: 11667
		private AnimatedDistort animDistort;

		// Token: 0x020003E4 RID: 996
		public enum Type
		{
			// Token: 0x04002D95 RID: 11669
			Stretch
		}

		// Token: 0x020003E5 RID: 997
		public enum Calculate
		{
			// Token: 0x04002D97 RID: 11671
			global,
			// Token: 0x04002D98 RID: 11672
			local
		}
	}
}
