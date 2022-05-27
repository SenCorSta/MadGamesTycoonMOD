using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200036B RID: 875
public class TextDemo : MonoBehaviour
{
	// Token: 0x06001FE3 RID: 8163 RVA: 0x0014E170 File Offset: 0x0014C370
	private void Start()
	{
		this.textLine = new VectorLine("Text", new List<Vector2>(), 1f);
		this.textLine.color = Color.yellow;
		this.textLine.drawTransform = base.transform;
		this.textLine.MakeText(this.text, new Vector2((float)(Screen.width / 2 - this.text.Length * this.textSize / 2), (float)(Screen.height / 2 + this.textSize / 2)), (float)this.textSize);
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x0014E210 File Offset: 0x0014C410
	private void Update()
	{
		base.transform.RotateAround(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vector3.forward, Time.deltaTime * 45f);
		Vector3 localScale = base.transform.localScale;
		localScale.x = 1f + Mathf.Sin(Time.time * 3f) * 0.3f;
		localScale.y = 1f + Mathf.Cos(Time.time * 3f) * 0.3f;
		base.transform.localScale = localScale;
		this.textLine.Draw();
	}

	// Token: 0x04002877 RID: 10359
	public string text = "Vectrosity!";

	// Token: 0x04002878 RID: 10360
	public int textSize = 40;

	// Token: 0x04002879 RID: 10361
	private VectorLine textLine;
}
