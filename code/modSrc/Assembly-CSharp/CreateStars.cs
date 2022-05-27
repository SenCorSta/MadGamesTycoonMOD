using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035A RID: 858
public class CreateStars : MonoBehaviour
{
	// Token: 0x06001FE5 RID: 8165 RVA: 0x0014C1AC File Offset: 0x0014A3AC
	private void Start()
	{
		Vector3[] array = new Vector3[this.numberOfStars];
		for (int i = 0; i < this.numberOfStars; i++)
		{
			array[i] = UnityEngine.Random.onUnitSphere * 100f;
		}
		float[] array2 = new float[this.numberOfStars];
		for (int j = 0; j < this.numberOfStars; j++)
		{
			array2[j] = UnityEngine.Random.Range(1.5f, 2.5f);
		}
		Color32[] array3 = new Color32[this.numberOfStars];
		for (int k = 0; k < this.numberOfStars; k++)
		{
			float num = UnityEngine.Random.value * 0.75f + 0.25f;
			array3[k] = new Color(num, num, num);
		}
		this.stars = new VectorLine("Stars", new List<Vector3>(array), 1f, LineType.Points);
		this.stars.SetColors(new List<Color32>(array3));
		this.stars.SetWidths(new List<float>(array2));
		this.stars.Draw();
		VectorLine.SetCanvasCamera(Camera.main);
		VectorLine.canvas.planeDistance = Camera.main.farClipPlane - 1f;
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x0014C2D9 File Offset: 0x0014A4D9
	private void LateUpdate()
	{
		this.stars.Draw();
	}

	// Token: 0x0400282F RID: 10287
	public int numberOfStars = 2000;

	// Token: 0x04002830 RID: 10288
	private VectorLine stars;
}
