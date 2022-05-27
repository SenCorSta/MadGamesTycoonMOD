using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E6 RID: 998
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	public class Distort : MonoBehaviour
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x0017173E File Offset: 0x0016F93E
		private void Awake()
		{
			this.animDistort = base.GetComponent<AnimatedDistort>();
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00002715 File Offset: 0x00000915
		private void Reset()
		{
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0017174C File Offset: 0x0016F94C
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

		// Token: 0x060023C5 RID: 9157 RVA: 0x001717D4 File Offset: 0x0016F9D4
		public UnityEngine.Object[] GetAllMeshes()
		{
			List<UnityEngine.Object> list = new List<UnityEngine.Object>();
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				list.Add(meshDistortData.mesh);
			}
			return list.ToArray();
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00171838 File Offset: 0x0016FA38
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

		// Token: 0x060023C7 RID: 9159 RVA: 0x00171BD8 File Offset: 0x0016FDD8
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

		// Token: 0x060023C8 RID: 9160 RVA: 0x00171C74 File Offset: 0x0016FE74
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

		// Token: 0x060023C9 RID: 9161 RVA: 0x00171D34 File Offset: 0x0016FF34
		private void ResetVertices()
		{
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				meshDistortData.ResetMesh();
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00002715 File Offset: 0x00000915
		public void EditParameters()
		{
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00171D84 File Offset: 0x0016FF84
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

		// Token: 0x060023CC RID: 9164 RVA: 0x00171DE7 File Offset: 0x0016FFE7
		public void UpdateDistort()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.UpdateInCPU();
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00171DF8 File Offset: 0x0016FFF8
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

		// Token: 0x060023CE RID: 9166 RVA: 0x00172134 File Offset: 0x00170334
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

		// Token: 0x060023CF RID: 9167 RVA: 0x001721D0 File Offset: 0x001703D0
		public void MakeDynamic()
		{
			foreach (MeshDistortData meshDistortData in this.meshList)
			{
				meshDistortData.mesh.MarkDynamic();
			}
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00172228 File Offset: 0x00170428
		public void AddDistortion()
		{
			DistortData item = new DistortData();
			if (this.distort == null)
			{
				this.distort = new List<DistortData>();
			}
			this.distort.Add(item);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x0017225A File Offset: 0x0017045A
		public void RemoveDistort(int index)
		{
			this.distort.RemoveAt(index);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00172268 File Offset: 0x00170468
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

		// Token: 0x04002D9B RID: 11675
		public Distort.Calculate calculation;

		// Token: 0x04002D9C RID: 11676
		[HideInInspector]
		public bool updateIntEditor = true;

		// Token: 0x04002D9D RID: 11677
		public List<DistortData> distort = new List<DistortData>();

		// Token: 0x04002D9E RID: 11678
		[NonSerialized]
		public List<MeshDistortData> meshList;

		// Token: 0x04002D9F RID: 11679
		public Bounds combinedBounds;

		// Token: 0x04002DA0 RID: 11680
		public bool showDebugLines;

		// Token: 0x04002DA1 RID: 11681
		public float debugLinesDistance = 1f;

		// Token: 0x04002DA2 RID: 11682
		public Vector3[,] debugLines;

		// Token: 0x04002DA3 RID: 11683
		public bool showMeshInEditor = true;

		// Token: 0x04002DA4 RID: 11684
		public bool showPreviewWindow = true;

		// Token: 0x04002DA5 RID: 11685
		public bool calculateInGPU;

		// Token: 0x04002DA6 RID: 11686
		public ComputeShader distortShader;

		// Token: 0x04002DA7 RID: 11687
		protected int dirtortKernel;

		// Token: 0x04002DA8 RID: 11688
		private bool hasSkinnedMesh;

		// Token: 0x04002DA9 RID: 11689
		private AnimatedDistort animDistort;

		// Token: 0x020003E7 RID: 999
		public enum Type
		{
			// Token: 0x04002DAB RID: 11691
			Stretch
		}

		// Token: 0x020003E8 RID: 1000
		public enum Calculate
		{
			// Token: 0x04002DAD RID: 11693
			global,
			// Token: 0x04002DAE RID: 11694
			local
		}
	}
}
