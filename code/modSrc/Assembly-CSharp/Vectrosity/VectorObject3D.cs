using System;
using UnityEngine;

namespace Vectrosity
{
	
	public class VectorObject3D : MonoBehaviour, IVectorObject
	{
		
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

		
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this.m_mesh);
			if (!this.m_useCustomMaterial)
			{
				UnityEngine.Object.Destroy(this.m_material);
			}
		}

		
		public void Enable(bool enable)
		{
			if (this == null)
			{
				return;
			}
			base.GetComponent<MeshRenderer>().enabled = enable;
		}

		
		public void SetTexture(Texture tex)
		{
			base.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tex;
		}

		
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

		
		private void SetupMesh()
		{
			this.m_mesh = new Mesh();
			this.m_mesh.name = this.m_vectorLine.name;
			this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
			base.GetComponent<MeshFilter>().mesh = this.m_mesh;
		}

		
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

		
		private void SetVerts()
		{
			this.m_mesh.vertices = this.m_vectorLine.lineVertices;
			this.m_updateVerts = false;
			this.m_mesh.RecalculateBounds();
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
		}

		
		public void UpdateUVs()
		{
			this.m_updateUVs = true;
		}

		
		public void UpdateColors()
		{
			this.m_updateColors = true;
		}

		
		public void UpdateNormals()
		{
			this.m_updateNormals = true;
		}

		
		public void UpdateTangents()
		{
			this.m_updateTangents = true;
		}

		
		public void UpdateTris()
		{
			this.m_updateTris = true;
		}

		
		public void UpdateMeshAttributes()
		{
			this.m_mesh.Clear();
			this.m_updateVerts = true;
			this.m_updateUVs = true;
			this.m_updateColors = true;
			this.m_updateTris = true;
		}

		
		public void ClearMesh()
		{
			if (this.m_mesh == null)
			{
				return;
			}
			this.m_mesh.Clear();
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

		
		private VectorLine m_vectorLine;

		
		private Material m_material;

		
		private bool m_useCustomMaterial;
	}
}
