using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003EB RID: 1003
	[Serializable]
	public class MeshDistortData
	{
		// Token: 0x170000DE RID: 222
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

		// Token: 0x170000DF RID: 223
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

		// Token: 0x170000E0 RID: 224
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

		// Token: 0x060023DD RID: 9181 RVA: 0x00172BD9 File Offset: 0x00170DD9
		public MeshDistortData(Transform transform, Material material, MeshFilter filter)
		{
			this.filter = filter;
			this.originalMaterial = material;
			this.meshTransform = transform;
			this.UpdateMesh();
			this.originalVertices = this.mesh.vertices;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00172C18 File Offset: 0x00170E18
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

		// Token: 0x060023DF RID: 9183 RVA: 0x00172C81 File Offset: 0x00170E81
		public void CreateBuffers()
		{
			this.ReleaseBuffers();
			this.verticeBuffer = new ComputeBuffer(this.originalVertices.Length, 12);
			this.matrixBuffer = new ComputeBuffer(2, 64);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00172CAC File Offset: 0x00170EAC
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

		// Token: 0x060023E1 RID: 9185 RVA: 0x00172CE2 File Offset: 0x00170EE2
		public void BufferSet(ComputeShader shader, int kernel)
		{
			shader.SetBuffer(kernel, "vertices", this.verticeBuffer);
			shader.SetBuffer(kernel, "matrixList", this.matrixBuffer);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00172D08 File Offset: 0x00170F08
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

		// Token: 0x060023E3 RID: 9187 RVA: 0x00172DA7 File Offset: 0x00170FA7
		public void ResetMesh()
		{
			this.mesh.vertices = this.originalVertices;
			this.mesh.RecalculateNormals();
			this.mesh.RecalculateBounds();
		}

		// Token: 0x04002DCD RID: 11725
		public Mesh mesh;

		// Token: 0x04002DCE RID: 11726
		public MeshFilter filter;

		// Token: 0x04002DCF RID: 11727
		public Material originalMaterial;

		// Token: 0x04002DD0 RID: 11728
		public Transform meshTransform;

		// Token: 0x04002DD1 RID: 11729
		protected Matrix4x4 skinLocalToWorldMatrix;

		// Token: 0x04002DD2 RID: 11730
		protected Matrix4x4 skinWorldToLocalMatrix;

		// Token: 0x04002DD3 RID: 11731
		private Mesh bakedMesh = new Mesh();

		// Token: 0x04002DD4 RID: 11732
		public Vector3[] originalVertices;

		// Token: 0x04002DD5 RID: 11733
		public SkinnedMeshRenderer skin;

		// Token: 0x04002DD6 RID: 11734
		public ComputeBuffer verticeBuffer;

		// Token: 0x04002DD7 RID: 11735
		public ComputeBuffer matrixBuffer;

		// Token: 0x04002DD8 RID: 11736
		public Transform[] bones;

		// Token: 0x04002DD9 RID: 11737
		public Transform root;
	}
}
