using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000366 RID: 870
public class SelectionBox : MonoBehaviour
{
	// Token: 0x06002015 RID: 8213 RVA: 0x0014D408 File Offset: 0x0014B608
	private void Start()
	{
		this.lineColors = new List<Color32>(new Color32[4]);
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), 3f, LineType.Continuous);
		this.selectionLine.capLength = 1.5f;
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x0014D447 File Offset: 0x0014B647
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x0014D46C File Offset: 0x0014B66C
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			base.StopCoroutine("CycleColor");
			this.selectionLine.SetColor(Color.white);
			this.originalPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			this.selectionLine.MakeRect(this.originalPos, Input.mousePosition);
			this.selectionLine.Draw();
		}
		if (Input.GetMouseButtonUp(0))
		{
			base.StartCoroutine("CycleColor");
		}
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x0014D4F3 File Offset: 0x0014B6F3
	private IEnumerator CycleColor()
	{
		for (;;)
		{
			for (int i = 0; i < 4; i++)
			{
				this.lineColors[i] = Color.Lerp(Color.yellow, Color.red, Mathf.PingPong((Time.time + (float)i * 0.25f) * 3f, 1f));
			}
			this.selectionLine.SetColors(this.lineColors);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400286E RID: 10350
	private VectorLine selectionLine;

	// Token: 0x0400286F RID: 10351
	private Vector2 originalPos;

	// Token: 0x04002870 RID: 10352
	private List<Color32> lineColors;
}
