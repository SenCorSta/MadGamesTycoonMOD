using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000352 RID: 850
public class DrawGrid : MonoBehaviour
{
	// Token: 0x06001FC4 RID: 8132 RVA: 0x0014B249 File Offset: 0x00149449
	private void Start()
	{
		this.gridLine = new VectorLine("Grid", new List<Vector2>(), 1f);
		this.gridLine.alignOddWidthToPixels = true;
		this.MakeGrid();
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x0014B278 File Offset: 0x00149478
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 30f, 20f), this.gridPixels.ToString());
		this.gridPixels = (int)GUI.HorizontalSlider(new Rect(40f, 15f, 590f, 20f), (float)this.gridPixels, 5f, 200f);
		if (GUI.changed)
		{
			this.MakeGrid();
		}
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x0014B2F4 File Offset: 0x001494F4
	private void MakeGrid()
	{
		int newCount = (Screen.width / this.gridPixels + 1 + (Screen.height / this.gridPixels + 1)) * 2;
		this.gridLine.Resize(newCount);
		int num = 0;
		for (int i = 0; i < Screen.width; i += this.gridPixels)
		{
			this.gridLine.points2[num++] = new Vector2((float)i, 0f);
			this.gridLine.points2[num++] = new Vector2((float)i, (float)(Screen.height - 1));
		}
		for (int j = 0; j < Screen.height; j += this.gridPixels)
		{
			this.gridLine.points2[num++] = new Vector2(0f, (float)j);
			this.gridLine.points2[num++] = new Vector2((float)(Screen.width - 1), (float)j);
		}
		this.gridLine.Draw();
	}

	// Token: 0x040027FE RID: 10238
	public int gridPixels = 50;

	// Token: 0x040027FF RID: 10239
	private VectorLine gridLine;
}
