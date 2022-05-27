using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000376 RID: 886
public class UniformTexturedLine : MonoBehaviour
{
	// Token: 0x0600204B RID: 8267 RVA: 0x0014F4B8 File Offset: 0x0014D6B8
	private void Start()
	{
		new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height / 2)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, this.lineTexture, this.lineWidth)
		{
			textureScale = this.textureScale
		}.Draw();
	}

	// Token: 0x040028A2 RID: 10402
	public Texture lineTexture;

	// Token: 0x040028A3 RID: 10403
	public float lineWidth = 8f;

	// Token: 0x040028A4 RID: 10404
	public float textureScale = 1f;
}
