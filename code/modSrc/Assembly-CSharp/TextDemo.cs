using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200036E RID: 878
public class TextDemo : MonoBehaviour
{
	// Token: 0x06002036 RID: 8246 RVA: 0x0014DAE0 File Offset: 0x0014BCE0
	private void Start()
	{
		this.textLine = new VectorLine("Text", new List<Vector2>(), 1f);
		this.textLine.color = Color.yellow;
		this.textLine.drawTransform = base.transform;
		this.textLine.MakeText(this.text, new Vector2((float)(Screen.width / 2 - this.text.Length * this.textSize / 2), (float)(Screen.height / 2 + this.textSize / 2)), (float)this.textSize);
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x0014DB80 File Offset: 0x0014BD80
	private void Update()
	{
		base.transform.RotateAround(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vector3.forward, Time.deltaTime * 45f);
		Vector3 localScale = base.transform.localScale;
		localScale.x = 1f + Mathf.Sin(Time.time * 3f) * 0.3f;
		localScale.y = 1f + Mathf.Cos(Time.time * 3f) * 0.3f;
		base.transform.localScale = localScale;
		this.textLine.Draw();
	}

	// Token: 0x0400288D RID: 10381
	public string text = "Vectrosity!";

	// Token: 0x0400288E RID: 10382
	public int textSize = 40;

	// Token: 0x0400288F RID: 10383
	private VectorLine textLine;
}
