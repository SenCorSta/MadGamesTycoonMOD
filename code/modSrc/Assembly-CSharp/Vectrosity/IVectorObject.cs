using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000380 RID: 896
	internal interface IVectorObject
	{
		// Token: 0x0600206D RID: 8301
		void SetName(string name);

		// Token: 0x0600206E RID: 8302
		void UpdateVerts();

		// Token: 0x0600206F RID: 8303
		void UpdateUVs();

		// Token: 0x06002070 RID: 8304
		void UpdateColors();

		// Token: 0x06002071 RID: 8305
		void UpdateTris();

		// Token: 0x06002072 RID: 8306
		void UpdateNormals();

		// Token: 0x06002073 RID: 8307
		void UpdateTangents();

		// Token: 0x06002074 RID: 8308
		void UpdateMeshAttributes();

		// Token: 0x06002075 RID: 8309
		void ClearMesh();

		// Token: 0x06002076 RID: 8310
		void SetMaterial(Material material);

		// Token: 0x06002077 RID: 8311
		void SetTexture(Texture texture);

		// Token: 0x06002078 RID: 8312
		void Enable(bool enable);

		// Token: 0x06002079 RID: 8313
		void SetVectorLine(VectorLine vectorLine, Texture texture, Material material, bool useCustomMaterial);

		// Token: 0x0600207A RID: 8314
		void Destroy();

		// Token: 0x0600207B RID: 8315
		int VertexCount();
	}
}
