using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000363 RID: 867
public class SelectionBox : MonoBehaviour
{
	// Token: 0x06001FC2 RID: 8130 RVA: 0x000150F5 File Offset: 0x000132F5
	private void Start()
	{
		this.lineColors = new List<Color32>(new Color32[4]);
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), 3f, LineType.Continuous);
		this.selectionLine.capLength = 1.5f;
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x00015134 File Offset: 0x00013334
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x0014DBE8 File Offset: 0x0014BDE8
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

	// Token: 0x06001FC5 RID: 8133 RVA: 0x00015159 File Offset: 0x00013359
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

	// Token: 0x04002858 RID: 10328
	private VectorLine selectionLine;

	// Token: 0x04002859 RID: 10329
	private Vector2 originalPos;

	// Token: 0x0400285A RID: 10330
	private List<Color32> lineColors;
}
