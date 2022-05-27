using System;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200037D RID: 893
	internal interface IVectorObject
	{
		// Token: 0x0600201A RID: 8218
		void SetName(string name);

		// Token: 0x0600201B RID: 8219
		void UpdateVerts();

		// Token: 0x0600201C RID: 8220
		void UpdateUVs();

		// Token: 0x0600201D RID: 8221
		void UpdateColors();

		// Token: 0x0600201E RID: 8222
		void UpdateTris();

		// Token: 0x0600201F RID: 8223
		void UpdateNormals();

		// Token: 0x06002020 RID: 8224
		void UpdateTangents();

		// Token: 0x06002021 RID: 8225
		void UpdateMeshAttributes();

		// Token: 0x06002022 RID: 8226
		void ClearMesh();

		// Token: 0x06002023 RID: 8227
		void SetMaterial(Material material);

		// Token: 0x06002024 RID: 8228
		void SetTexture(Texture texture);

		// Token: 0x06002025 RID: 8229
		void Enable(bool enable);

		// Token: 0x06002026 RID: 8230
		void SetVectorLine(VectorLine vectorLine, Texture texture, Material material, bool useCustomMaterial);

		// Token: 0x06002027 RID: 8231
		void Destroy();

		// Token: 0x06002028 RID: 8232
		int VertexCount();
	}
}
