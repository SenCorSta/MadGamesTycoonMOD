using System;
using UnityEngine;

namespace MeshDistortLite
{
	// Token: 0x020003E8 RID: 1000
	[Serializable]
	public class MeshDistortData
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00018319 File Offset: 0x00016519
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0001833B File Offset: 0x0001653B
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0016FEB0 File Offset: 0x0016E0B0
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

		// Token: 0x0600238A RID: 9098 RVA: 0x0001835D File Offset: 0x0001655D
		public MeshDistortData(Transform transform, Material material, MeshFilter filter)
		{
			this.filter = filter;
			this.originalMaterial = material;
			this.meshTransform = transform;
			this.UpdateMesh();
			this.originalVertices = this.mesh.vertices;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0016FF98 File Offset: 0x0016E198
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

		// Token: 0x0600238C RID: 9100 RVA: 0x0001839C File Offset: 0x0001659C
		public void CreateBuffers()
		{
			this.ReleaseBuffers();
			this.verticeBuffer = new ComputeBuffer(this.originalVertices.Length, 12);
			this.matrixBuffer = new ComputeBuffer(2, 64);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000183C7 File Offset: 0x000165C7
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

		// Token: 0x0600238E RID: 9102 RVA: 0x000183FD File Offset: 0x000165FD
		public void BufferSet(ComputeShader shader, int kernel)
		{
			shader.SetBuffer(kernel, "vertices", this.verticeBuffer);
			shader.SetBuffer(kernel, "matrixList", this.matrixBuffer);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00170004 File Offset: 0x0016E204
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

		// Token: 0x06002390 RID: 9104 RVA: 0x00018423 File Offset: 0x00016623
		public void ResetMesh()
		{
			this.mesh.vertices = this.originalVertices;
			this.mesh.RecalculateNormals();
			this.mesh.RecalculateBounds();
		}

		// Token: 0x04002DB7 RID: 11703
		public Mesh mesh;

		// Token: 0x04002DB8 RID: 11704
		public MeshFilter filter;

		// Token: 0x04002DB9 RID: 11705
		public Material originalMaterial;

		// Token: 0x04002DBA RID: 11706
		public Transform meshTransform;

		// Token: 0x04002DBB RID: 11707
		protected Matrix4x4 skinLocalToWorldMatrix;

		// Token: 0x04002DBC RID: 11708
		protected Matrix4x4 skinWorldToLocalMatrix;

		// Token: 0x04002DBD RID: 11709
		private Mesh bakedMesh = new Mesh();

		// Token: 0x04002DBE RID: 11710
		public Vector3[] originalVertices;

		// Token: 0x04002DBF RID: 11711
		public SkinnedMeshRenderer skin;

		// Token: 0x04002DC0 RID: 11712
		public ComputeBuffer verticeBuffer;

		// Token: 0x04002DC1 RID: 11713
		public ComputeBuffer matrixBuffer;

		// Token: 0x04002DC2 RID: 11714
		public Transform[] bones;

		// Token: 0x04002DC3 RID: 11715
		public Transform root;
	}
}
