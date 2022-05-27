using System;
using UnityEngine;
using UnityEngine.UI;

namespace Vectrosity
{
	// Token: 0x0200038E RID: 910
	[Serializable]
	public class VectorObject2D : RawImage, IVectorObject
	{
		// Token: 0x060021E0 RID: 8672 RVA: 0x0015F5B2 File Offset: 0x0015D7B2
		public void SetVectorLine(VectorLine vectorLine, Texture tex, Material mat, bool useCustomMaterial)
		{
			this.vectorLine = vectorLine;
			this.SetTexture(tex);
			this.SetMaterial(mat);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0015F5C9 File Offset: 0x0015D7C9
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this.m_mesh);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0015F5D6 File Offset: 0x0015D7D6
		public void DestroyNow()
		{
			UnityEngine.Object.DestroyImmediate(this.m_mesh);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0015F5E3 File Offset: 0x0015D7E3
		public void Enable(bool enable)
		{
			if (this == null)
			{
				return;
			}
			base.enabled = enable;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0015F5F6 File Offset: 0x0015D7F6
		public void SetTexture(Texture tex)
		{
			base.texture = tex;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0015F5FF File Offset: 0x0015D7FF
		public void SetMaterial(Material mat)
		{
			this.material = mat;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0015F608 File Offset: 0x0015D808
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

		// Token: 0x060021E7 RID: 8679 RVA: 0x0015F687 File Offset: 0x0015D887
		private void SetupMesh()
		{
			this.m_mesh = new Mesh();
			this.m_mesh.name = this.vectorLine.name;
			this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
			this.SetMeshBounds();
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0015F6C0 File Offset: 0x0015D8C0
		private void SetMeshBounds()
		{
			if (this.m_mesh != null)
			{
				this.m_mesh.bounds = new Bounds(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f), new Vector3((float)Screen.width, (float)Screen.height, 0f));
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0015F71C File Offset: 0x0015D91C
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

		// Token: 0x060021EA RID: 8682 RVA: 0x0015F864 File Offset: 0x0015DA64
		public void SetName(string name)
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.name = name;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0015F881 File Offset: 0x0015DA81
		public void UpdateVerts()
		{
			this.m_updateVerts = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0015F890 File Offset: 0x0015DA90
		public void UpdateUVs()
		{
			this.m_updateUVs = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0015F89F File Offset: 0x0015DA9F
		public void UpdateColors()
		{
			this.m_updateColors = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0015F8AE File Offset: 0x0015DAAE
		public void UpdateNormals()
		{
			this.m_updateNormals = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0015F8BD File Offset: 0x0015DABD
		public void UpdateTangents()
		{
			this.m_updateTangents = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0015F8CC File Offset: 0x0015DACC
		public void UpdateTris()
		{
			this.m_updateTris = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0015F8DC File Offset: 0x0015DADC
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

		// Token: 0x060021F2 RID: 8690 RVA: 0x0015F92A File Offset: 0x0015DB2A
		public void ClearMesh()
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.Clear();
			this.UpdateGeometry();
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0015F94C File Offset: 0x0015DB4C
		public int VertexCount()
		{
			return this.m_mesh.vertexCount;
		}

		// Token: 0x0400294F RID: 10575
		private bool m_updateVerts = true;

		// Token: 0x04002950 RID: 10576
		private bool m_updateUVs = true;

		// Token: 0x04002951 RID: 10577
		private bool m_updateColors = true;

		// Token: 0x04002952 RID: 10578
		private bool m_updateNormals;

		// Token: 0x04002953 RID: 10579
		private bool m_updateTangents;

		// Token: 0x04002954 RID: 10580
		private bool m_updateTris = true;

		// Token: 0x04002955 RID: 10581
		private Mesh m_mesh;

		// Token: 0x04002956 RID: 10582
		public VectorLine vectorLine;

		// Token: 0x04002957 RID: 10583
		private static VertexHelper vertexHelper;
	}
}
