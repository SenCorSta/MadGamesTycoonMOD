using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200038F RID: 911
	public class VectorObject3D : MonoBehaviour, IVectorObject
	{
		// Token: 0x060021F6 RID: 8694 RVA: 0x0015F980 File Offset: 0x0015DB80
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

		// Token: 0x060021F7 RID: 8695 RVA: 0x0015F9DE File Offset: 0x0015DBDE
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this.m_mesh);
			if (!this.m_useCustomMaterial)
			{
				UnityEngine.Object.Destroy(this.m_material);
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0015F9FE File Offset: 0x0015DBFE
		public void Enable(bool enable)
		{
			if (this == null)
			{
				return;
			}
			base.GetComponent<MeshRenderer>().enabled = enable;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0015FA16 File Offset: 0x0015DC16
		public void SetTexture(Texture tex)
		{
			base.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tex;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0015FA29 File Offset: 0x0015DC29
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

		// Token: 0x060021FB RID: 8699 RVA: 0x0015FA6C File Offset: 0x0015DC6C
		private void SetupMesh()
		{
			this.m_mesh = new Mesh();
			this.m_mesh.name = this.m_vectorLine.name;
			this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
			base.GetComponent<MeshFilter>().mesh = this.m_mesh;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0015FAB8 File Offset: 0x0015DCB8
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

		// Token: 0x060021FD RID: 8701 RVA: 0x0015FBC1 File Offset: 0x0015DDC1
		private void SetVerts()
		{
			this.m_mesh.vertices = this.m_vectorLine.lineVertices;
			this.m_updateVerts = false;
			this.m_mesh.RecalculateBounds();
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0015FBEB File Offset: 0x0015DDEB
		public void SetName(string name)
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.name = name;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0015FC08 File Offset: 0x0015DE08
		public void UpdateVerts()
		{
			this.m_updateVerts = true;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0015FC11 File Offset: 0x0015DE11
		public void UpdateUVs()
		{
			this.m_updateUVs = true;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0015FC1A File Offset: 0x0015DE1A
		public void UpdateColors()
		{
			this.m_updateColors = true;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0015FC23 File Offset: 0x0015DE23
		public void UpdateNormals()
		{
			this.m_updateNormals = true;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0015FC2C File Offset: 0x0015DE2C
		public void UpdateTangents()
		{
			this.m_updateTangents = true;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0015FC35 File Offset: 0x0015DE35
		public void UpdateTris()
		{
			this.m_updateTris = true;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0015FC3E File Offset: 0x0015DE3E
		public void UpdateMeshAttributes()
		{
			this.m_mesh.Clear();
			this.m_updateVerts = true;
			this.m_updateUVs = true;
			this.m_updateColors = true;
			this.m_updateTris = true;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0015FC67 File Offset: 0x0015DE67
		public void ClearMesh()
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.Clear();
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0015FC83 File Offset: 0x0015DE83
		public int VertexCount()
		{
			return this.m_mesh.vertexCount;
		}

		// Token: 0x04002958 RID: 10584
		private bool m_updateVerts = true;

		// Token: 0x04002959 RID: 10585
		private bool m_updateUVs = true;

		// Token: 0x0400295A RID: 10586
		private bool m_updateColors = true;

		// Token: 0x0400295B RID: 10587
		private bool m_updateNormals;

		// Token: 0x0400295C RID: 10588
		private bool m_updateTangents;

		// Token: 0x0400295D RID: 10589
		private bool m_updateTris = true;

		// Token: 0x0400295E RID: 10590
		private Mesh m_mesh;

		// Token: 0x0400295F RID: 10591
		private VectorLine m_vectorLine;

		// Token: 0x04002960 RID: 10592
		private Material m_material;

		// Token: 0x04002961 RID: 10593
		private bool m_useCustomMaterial;
	}
}
