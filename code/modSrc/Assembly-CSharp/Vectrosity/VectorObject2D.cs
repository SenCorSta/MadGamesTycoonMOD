using System;
using UnityEngine;
using UnityEngine.UI;

namespace Vectrosity
{
	
	[Serializable]
	public class VectorObject2D : RawImage, IVectorObject
	{
		
		public void SetVectorLine(VectorLine vectorLine, Texture tex, Material mat, bool useCustomMaterial)
		{
			this.vectorLine = vectorLine;
			this.SetTexture(tex);
			this.SetMaterial(mat);
		}

		
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this.m_mesh);
		}

		
		public void DestroyNow()
		{
			UnityEngine.Object.DestroyImmediate(this.m_mesh);
		}

		
		public void Enable(bool enable)
		{
			if (this == null)
			{
				return;
			}
			base.enabled = enable;
		}

		
		public void SetTexture(Texture tex)
		{
			base.texture = tex;
		}

		
		public void SetMaterial(Material mat)
		{
			this.material = mat;
		}

		
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

		
		private void SetupMesh()
		{
			this.m_mesh = new Mesh();
			this.m_mesh.name = this.vectorLine.name;
			this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
			this.SetMeshBounds();
		}

		
		private void SetMeshBounds()
		{
			if (this.m_mesh != null)
			{
				this.m_mesh.bounds = new Bounds(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f), new Vector3((float)Screen.width, (float)Screen.height, 0f));
			}
		}

		
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

		
		public void SetName(string name)
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.name = name;
		}

		
		public void UpdateVerts()
		{
			this.m_updateVerts = true;
			this.SetVerticesDirty();
		}

		
		public void UpdateUVs()
		{
			this.m_updateUVs = true;
			this.SetVerticesDirty();
		}

		
		public void UpdateColors()
		{
			this.m_updateColors = true;
			this.SetVerticesDirty();
		}

		
		public void UpdateNormals()
		{
			this.m_updateNormals = true;
			this.SetVerticesDirty();
		}

		
		public void UpdateTangents()
		{
			this.m_updateTangents = true;
			this.SetVerticesDirty();
		}

		
		public void UpdateTris()
		{
			this.m_updateTris = true;
			this.SetVerticesDirty();
		}

		
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

		
		public void ClearMesh()
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.Clear();
			this.UpdateGeometry();
		}

		
		public int VertexCount()
		{
			return this.m_mesh.vertexCount;
		}

		
		private bool m_updateVerts = true;

		
		private bool m_updateUVs = true;

		
		private bool m_updateColors = true;

		
		private bool m_updateNormals;

		
		private bool m_updateTangents;

		
		private bool m_updateTris = true;

		
		private Mesh m_mesh;

		
		public VectorLine vectorLine;

		
		private static VertexHelper vertexHelper;
	}
}
