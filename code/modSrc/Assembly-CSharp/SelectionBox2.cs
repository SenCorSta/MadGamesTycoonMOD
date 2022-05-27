using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000368 RID: 872
public class SelectionBox2 : MonoBehaviour
{
	// Token: 0x06002020 RID: 8224 RVA: 0x0014D5BC File Offset: 0x0014B7BC
	private void Start()
	{
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), this.lineTexture, 4f, LineType.Continuous);
		this.selectionLine.textureScale = this.textureScale;
		this.selectionLine.alignOddWidthToPixels = true;
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x0014D447 File Offset: 0x0014B647
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x0014D608 File Offset: 0x0014B808
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

	// Token: 0x04002874 RID: 10356
	public Texture lineTexture;

	// Token: 0x04002875 RID: 10357
	public float textureScale = 4f;

	// Token: 0x04002876 RID: 10358
	private VectorLine selectionLine;

	// Token: 0x04002877 RID: 10359
	private Vector2 originalPos;
}
