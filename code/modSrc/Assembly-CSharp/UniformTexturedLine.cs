using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000373 RID: 883
public class UniformTexturedLine : MonoBehaviour
{
	// Token: 0x06001FF8 RID: 8184 RVA: 0x0014FACC File Offset: 0x0014DCCC
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

	// Token: 0x0400288C RID: 10380
	public Texture lineTexture;

	// Token: 0x0400288D RID: 10381
	public float lineWidth = 8f;

	// Token: 0x0400288E RID: 10382
	public float textureScale = 1f;
}
