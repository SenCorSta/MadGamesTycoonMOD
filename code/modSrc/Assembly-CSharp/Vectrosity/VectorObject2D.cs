using System;
using UnityEngine;
using UnityEngine.UI;

namespace Vectrosity
{
	// Token: 0x0200038B RID: 907
	[Serializable]
	public class VectorObject2D : RawImage, IVectorObject
	{
		// Token: 0x0600218D RID: 8589 RVA: 0x00016642 File Offset: 0x00014842
		public void SetVectorLine(VectorLine vectorLine, Texture tex, Material mat, bool useCustomMaterial)
		{
			this.vectorLine = vectorLine;
			this.SetTexture(tex);
			this.SetMaterial(mat);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00016659 File Offset: 0x00014859
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this.m_mesh);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00016666 File Offset: 0x00014866
		public void DestroyNow()
		{
			UnityEngine.Object.DestroyImmediate(this.m_mesh);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00016673 File Offset: 0x00014873
		public void Enable(bool enable)
		{
			if (this == null)
			{
				return;
			}
			base.enabled = enable;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00016686 File Offset: 0x00014886
		public void SetTexture(Texture tex)
		{
			base.texture = tex;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0001668F File Offset: 0x0001488F
		public void SetMaterial(Material mat)
		{
			this.material = mat;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0015E834 File Offset: 0x0015CA34
		protected override void UpdateGeometry()
		{
			if (this.m_mesh == null)
			{
				this.SetupMesh();
			}
			if (base.rectTransform != null && base.rectTransform.rect.width >= 0f && base.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(VectorObject2D.vertexHelper);
			}
			base.canvasRenderer.SetMesh(this.m_mesh);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00016698 File Offset: 0x00014898
		private void SetupMesh()
		{
			this.m_mesh = new Mesh();
			this.m_mesh.name = this.vectorLine.name;
			this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
			this.SetMeshBounds();
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0015E8B4 File Offset: 0x0015CAB4
		private void SetMeshBounds()
		{
			if (this.m_mesh != null)
			{
				this.m_mesh.bounds = new Bounds(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f), new Vector3((float)Screen.width, (float)Screen.height, 0f));
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0015E910 File Offset: 0x0015CB10
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (this.m_updateVerts)
			{
				this.m_mesh.vertices = this.vectorLine.lineVertices;
				this.m_updateVerts = false;
			}
			if (this.m_updateUVs)
			{
				if (this.vectorLine.lineUVs.Length == this.m_mesh.vertexCount)
				{
					this.m_mesh.uv = this.vectorLine.lineUVs;
				}
				this.m_updateUVs = false;
			}
			if (this.m_updateColors)
			{
				if (this.vectorLine.lineColors.Length == this.m_mesh.vertexCount)
				{
					this.m_mesh.colors32 = this.vectorLine.lineColors;
				}
				this.m_updateColors = false;
			}
			if (this.m_updateTris)
			{
				this.m_mesh.SetTriangles(this.vectorLine.lineTriangles, 0);
				this.m_updateTris = false;
				this.SetMeshBounds();
			}
			if (this.m_updateNormals && this.m_mesh != null)
			{
				this.m_mesh.RecalculateNormals();
				this.m_updateNormals = false;
				this.UpdateGeometry();
			}
			if (this.m_updateTangents && this.m_mesh != null)
			{
				this.m_mesh.tangents = this.vectorLine.CalculateTangents(this.m_mesh.normals);
				this.m_updateTangents = false;
			}
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000166CE File Offset: 0x000148CE
		public void SetName(string name)
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.name = name;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000166EB File Offset: 0x000148EB
		public void UpdateVerts()
		{
			this.m_updateVerts = true;
			this.SetVerticesDirty();
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000166FA File Offset: 0x000148FA
		public void UpdateUVs()
		{
			this.m_updateUVs = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00016709 File Offset: 0x00014909
		public void UpdateColors()
		{
			this.m_updateColors = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00016718 File Offset: 0x00014918
		public void UpdateNormals()
		{
			this.m_updateNormals = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00016727 File Offset: 0x00014927
		public void UpdateTangents()
		{
			this.m_updateTangents = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00016736 File Offset: 0x00014936
		public void UpdateTris()
		{
			this.m_updateTris = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0015EA58 File Offset: 0x0015CC58
		public void UpdateMeshAttributes()
		{
			if (this.m_mesh != null)
			{
				this.m_mesh.Clear();
			}
			this.m_updateVerts = true;
			this.m_updateUVs = true;
			this.m_updateColors = true;
			this.m_updateTris = true;
			this.SetVerticesDirty();
			this.SetMeshBounds();
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00016745 File Offset: 0x00014945
		public void ClearMesh()
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.Clear();
			this.UpdateGeometry();
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00016767 File Offset: 0x00014967
		public int VertexCount()
		{
			return this.m_mesh.vertexCount;
		}

		// Token: 0x04002939 RID: 10553
		private bool m_updateVerts = true;

		// Token: 0x0400293A RID: 10554
		private bool m_updateUVs = true;

		// Token: 0x0400293B RID: 10555
		private bool m_updateColors = true;

		// Token: 0x0400293C RID: 10556
		private bool m_updateNormals;

		// Token: 0x0400293D RID: 10557
		private bool m_updateTangents;

		// Token: 0x0400293E RID: 10558
		private bool m_updateTris = true;

		// Token: 0x0400293F RID: 10559
		private Mesh m_mesh;

		// Token: 0x04002940 RID: 10560
		public VectorLine vectorLine;

		// Token: 0x04002941 RID: 10561
		private static VertexHelper vertexHelper;
	}
}
