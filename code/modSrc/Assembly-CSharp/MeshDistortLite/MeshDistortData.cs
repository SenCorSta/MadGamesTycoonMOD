using System;
using UnityEngine;

namespace MeshDistortLite
{
	
	[Serializable]
	public class MeshDistortData
	{
		
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00172AB0 File Offset: 0x00170CB0
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				if (this.skin == null)
				{
					return this.meshTransform.localToWorldMatrix;
				}
				return this.skinLocalToWorldMatrix;
			}
		}

		
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x00172AD2 File Offset: 0x00170CD2
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				if (this.skin == null)
				{
					return this.meshTransform.worldToLocalMatrix;
				}
				return this.skinWorldToLocalMatrix;
			}
		}

		
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x00172AF4 File Offset: 0x00170CF4
		public Vector3[] skinVertices
		{
			get
			{
				if (this.skin == null)
				{
					return this.originalVertices;
				}
				this.mesh.vertices = this.originalVertices;
				Transform parent = this.skin.transform.parent;
				Vector3 localScale = this.skin.transform.localScale;
				this.skin.transform.parent = null;
				this.skin.transform.localScale = Vector3.one;
				this.skin.BakeMesh(this.bakedMesh);
				this.skinLocalToWorldMatrix = this.skin.transform.localToWorldMatrix;
				this.skinWorldToLocalMatrix = this.skin.transform.worldToLocalMatrix;
				this.skin.transform.parent = parent;
				this.skin.transform.localScale = localScale;
				return this.bakedMesh.vertices;
			}
		}

		
		public MeshDistortData(Transform transform, Material material, MeshFilter filter)
		{
			this.filter = filter;
			this.originalMaterial = material;
			this.meshTransform = transform;
			this.UpdateMesh();
			this.originalVertices = this.mesh.vertices;
		}

		
		public MeshDistortData(Transform transform, Material material, SkinnedMeshRenderer skin)
		{
			this.skin = skin;
			this.originalMaterial = material;
			this.meshTransform = transform;
			this.UpdateMesh();
			this.originalVertices = this.mesh.vertices;
			if (Application.isPlaying)
			{
				this.bones = skin.bones;
				this.root = skin.rootBone;
			}
		}

		
		public void CreateBuffers()
		{
			this.ReleaseBuffers();
			this.verticeBuffer = new ComputeBuffer(this.originalVertices.Length, 12);
			this.matrixBuffer = new ComputeBuffer(2, 64);
		}

		
		public void ReleaseBuffers()
		{
			if (this.verticeBuffer != null)
			{
				this.verticeBuffer.Dispose();
				this.verticeBuffer = null;
			}
			if (this.matrixBuffer != null)
			{
				this.matrixBuffer.Dispose();
				this.matrixBuffer = null;
			}
		}

		
		public void BufferSet(ComputeShader shader, int kernel)
		{
			shader.SetBuffer(kernel, "vertices", this.verticeBuffer);
			shader.SetBuffer(kernel, "matrixList", this.matrixBuffer);
		}

		
		public void UpdateMesh()
		{
			if (!Application.isPlaying)
			{
				this.mesh = UnityEngine.Object.Instantiate<Mesh>((this.filter != null) ? this.filter.sharedMesh : this.skin.sharedMesh);
				this.mesh.hideFlags = HideFlags.HideAndDontSave;
				return;
			}
			if (this.skin != null)
			{
				this.skin.sharedMesh = UnityEngine.Object.Instantiate<Mesh>(this.skin.sharedMesh);
				this.mesh = this.skin.sharedMesh;
				return;
			}
			this.mesh = this.filter.mesh;
		}

		
		public void ResetMesh()
		{
			this.mesh.vertices = this.originalVertices;
			this.mesh.RecalculateNormals();
			this.mesh.RecalculateBounds();
		}

		
		public Mesh mesh;

		
		public MeshFilter filter;

		
		public Material originalMaterial;

		
		public Transform meshTransform;

		
		protected Matrix4x4 skinLocalToWorldMatrix;

		
		protected Matrix4x4 skinWorldToLocalMatrix;

		
		private Mesh bakedMesh = new Mesh();

		
		public Vector3[] originalVertices;

		
		public SkinnedMeshRenderer skin;

		
		public ComputeBuffer verticeBuffer;

		
		public ComputeBuffer matrixBuffer;

		
		public Transform[] bones;

		
		public Transform root;
	}
}
