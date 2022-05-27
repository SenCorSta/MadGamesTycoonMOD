using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000362 RID: 866
public class SelectLine : MonoBehaviour
{
	// Token: 0x06001FBD RID: 8125 RVA: 0x0014DA68 File Offset: 0x0014BC68
	private void Start()
	{
		this.lines = new VectorLine[this.numberOfLines];
		this.wasSelected = new bool[this.numberOfLines];
		for (int i = 0; i < this.numberOfLines; i++)
		{
			this.lines[i] = new VectorLine("SelectLine", new List<Vector2>(5), this.lineThickness, LineType.Continuous, Joins.Fill);
			this.SetPoints(i);
		}
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x0014DAD0 File Offset: 0x0014BCD0
	private void SetPoints(int i)
	{
		for (int j = 0; j < this.lines[i].points2.Count; j++)
		{
			this.lines[i].points2[j] = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height - 20));
		}
		this.lines[i].Draw();
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x0014DB3C File Offset: 0x0014BD3C
	private void Update()
	{
		for (int i = 0; i < this.numberOfLines; i++)
		{
			int num;
			if (this.lines[i].Selected(Input.mousePosition, this.extraThickness, out num))
			{
				if (!this.wasSelected[i])
				{
					this.lines[i].SetColor(Color.green);
					this.wasSelected[i] = true;
				}
				if (Input.GetMouseButtonDown(0))
				{
					this.SetPoints(i);
				}
			}
			else if (this.wasSelected[i])
			{
				this.wasSelected[i] = false;
				this.lines[i].SetColor(Color.white);
			}
		}
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x000150AF File Offset: 0x000132AF
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 800f, 30f), "Click a line to make a new line");
	}

	// Token: 0x04002853 RID: 10323
	public float lineThickness = 10f;

	// Token: 0x04002854 RID: 10324
	public int extraThickness = 2;

	// Token: 0x04002855 RID: 10325
	public int numberOfLines = 2;

	// Token: 0x04002856 RID: 10326
	private VectorLine[] lines;

	// Token: 0x04002857 RID: 10327
	private bool[] wasSelected;
}
