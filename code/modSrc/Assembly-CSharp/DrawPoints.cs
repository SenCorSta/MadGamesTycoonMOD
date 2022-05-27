using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000360 RID: 864
public class DrawPoints : MonoBehaviour
{
	// Token: 0x06001FFC RID: 8188 RVA: 0x0014C8A0 File Offset: 0x0014AAA0
	private void Start()
	{
		int num = this.numberOfDots * this.numberOfRings;
		Vector2[] collection = new Vector2[num];
		Color32[] array = new Color32[num];
		float b = 1f - 0.75f / (float)num;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.dotColor;
			this.dotColor *= b;
		}
		VectorLine vectorLine = new VectorLine("Dots", new List<Vector2>(collection), this.dotSize, LineType.Points);
		vectorLine.SetColors(new List<Color32>(array));
		for (int j = 0; j < this.numberOfRings; j++)
		{
			vectorLine.MakeCircle(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)(Screen.height / (j + 2)), this.numberOfDots, this.numberOfDots * j);
		}
		vectorLine.Draw();
	}

	// Token: 0x0400284B RID: 10315
	public float dotSize = 2f;

	// Token: 0x0400284C RID: 10316
	public int numberOfDots = 100;

	// Token: 0x0400284D RID: 10317
	public int numberOfRings = 8;

	// Token: 0x0400284E RID: 10318
	public Color dotColor = Color.cyan;
}
