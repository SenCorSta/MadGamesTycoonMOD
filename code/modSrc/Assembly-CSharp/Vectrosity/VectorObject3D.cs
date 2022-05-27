using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200038C RID: 908
	public class VectorObject3D : MonoBehaviour, IVectorObject
	{
		// Token: 0x060021A3 RID: 8611 RVA: 0x0015EAA8 File Offset: 0x0015CCA8
		public void SetVectorLine(VectorLine vectorLine, Texture tex, Material mat, bool useCustomMaterial)
		{
			base.gameObject.AddComponent<MeshRenderer>();
			base.gameObject.AddComponent<MeshFilter>();
			this.m_vectorLine = vectorLine;
			this.m_material = mat;
			this.m_material.mainTexture = tex;
			base.GetComponent<MeshRenderer>().sharedMaterial = this.m_material;
			this.m_useCustomMaterial = useCustomMaterial;
			this.SetupMesh();
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00016798 File Offset: 0x00014998
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this.m_mesh);
			if (!this.m_useCustomMaterial)
			{
				UnityEngine.Object.Destroy(this.m_material);
			}
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000167B8 File Offset: 0x000149B8
		public void Enable(bool enable)
		{
			if (this == null)
			{
				return;
			}
			base.GetComponent<MeshRenderer>().enabled = enable;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000167D0 File Offset: 0x000149D0
		public void SetTexture(Texture tex)
		{
			base.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tex;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000167E3 File Offset: 0x000149E3
		public void SetMaterial(Material mat)
		{
			this.m_material = mat;
			this.m_useCustomMaterial = true;
			base.GetComponent<MeshRenderer>().sharedMaterial = mat;
			if (mat != null)
			{
				base.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = this.m_vectorLine.texture;
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0015EB08 File Offset: 0x0015CD08
		private void SetupMesh()
		{
			this.m_mesh = new Mesh();
			this.m_mesh.name = this.m_vectorLine.name;
			this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
			base.GetComponent<MeshFilter>().mesh = this.m_mesh;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0015EB54 File Offset: 0x0015CD54
		private void LateUpdate()
		{
			if (this.m_updateVerts)
			{
				this.SetVerts();
			}
			if (this.m_updateUVs)
			{
				if (this.m_vectorLine.lineUVs.Length == this.m_mesh.vertexCount)
				{
					this.m_mesh.uv = this.m_vectorLine.lineUVs;
				}
				this.m_updateUVs = false;
			}
			if (this.m_updateColors)
			{
				if (this.m_vectorLine.lineColors.Length == this.m_mesh.vertexCount)
				{
					this.m_mesh.colors32 = this.m_vectorLine.lineColors;
				}
				this.m_updateColors = false;
			}
			if (this.m_updateTris)
			{
				this.m_mesh.SetTriangles(this.m_vectorLine.lineTriangles, 0);
				this.m_updateTris = false;
			}
			if (this.m_updateNormals)
			{
				this.m_mesh.RecalculateNormals();
				this.m_updateNormals = false;
			}
			if (this.m_updateTangents)
			{
				this.m_mesh.tangents = this.m_vectorLine.CalculateTangents(this.m_mesh.normals);
				this.m_updateTangents = false;
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00016823 File Offset: 0x00014A23
		private void SetVerts()
		{
			this.m_mesh.vertices = this.m_vectorLine.lineVertices;
			this.m_updateVerts = false;
			this.m_mesh.RecalculateBounds();
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0001684D File Offset: 0x00014A4D
		public void SetName(string name)
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.name = name;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0001686A File Offset: 0x00014A6A
		public void UpdateVerts()
		{
			this.m_updateVerts = true;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00016873 File Offset: 0x00014A73
		public void UpdateUVs()
		{
			this.m_updateUVs = true;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0001687C File Offset: 0x00014A7C
		public void UpdateColors()
		{
			this.m_updateColors = true;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00016885 File Offset: 0x00014A85
		public void UpdateNormals()
		{
			this.m_updateNormals = true;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0001688E File Offset: 0x00014A8E
		public void UpdateTangents()
		{
			this.m_updateTangents = true;
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00016897 File Offset: 0x00014A97
		public void UpdateTris()
		{
			this.m_updateTris = true;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000168A0 File Offset: 0x00014AA0
		public void UpdateMeshAttributes()
		{
			this.m_mesh.Clear();
			this.m_updateVerts = true;
			this.m_updateUVs = true;
			this.m_updateColors = true;
			this.m_updateTris = true;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000168C9 File Offset: 0x00014AC9
		public void ClearMesh()
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.Clear();
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000168E5 File Offset: 0x00014AE5
		public int VertexCount()
		{
			return this.m_mesh.vertexCount;
		}

		// Token: 0x04002942 RID: 10562
		private bool m_updateVerts = true;

		// Token: 0x04002943 RID: 10563
		private bool m_updateUVs = true;

		// Token: 0x04002944 RID: 10564
		private bool m_updateColors = true;

		// Token: 0x04002945 RID: 10565
		private bool m_updateNormals;

		// Token: 0x04002946 RID: 10566
		private bool m_updateTangents;

		// Token: 0x04002947 RID: 10567
		private bool m_updateTris = true;

		// Token: 0x04002948 RID: 10568
		private Mesh m_mesh;

		// Token: 0x04002949 RID: 10569
		private VectorLine m_vectorLine;

		// Token: 0x0400294A RID: 10570
		private Material m_material;

		// Token: 0x0400294B RID: 10571
		private bool m_useCustomMaterial;
	}
}
