using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000365 RID: 869
public class SelectionBox2 : MonoBehaviour
{
	// Token: 0x06001FCD RID: 8141 RVA: 0x0014DD10 File Offset: 0x0014BF10
	private void Start()
	{
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), this.lineTexture, 4f, LineType.Continuous);
		this.selectionLine.textureScale = this.textureScale;
		this.selectionLine.alignOddWidthToPixels = true;
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x00015134 File Offset: 0x00013334
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x0014DD5C File Offset: 0x0014BF5C
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.originalPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			this.selectionLine.MakeRect(this.originalPos, Input.mousePosition);
			this.selectionLine.Draw();
		}
		this.selectionLine.textureOffset = -Time.time * 2f % 1f;
	}

	// Token: 0x0400285E RID: 10334
	public Texture lineTexture;

	// Token: 0x0400285F RID: 10335
	public float textureScale = 4f;

	// Token: 0x04002860 RID: 10336
	private VectorLine selectionLine;

	// Token: 0x04002861 RID: 10337
	private Vector2 originalPos;
}
