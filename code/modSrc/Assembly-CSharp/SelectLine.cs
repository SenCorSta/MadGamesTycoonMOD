using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000365 RID: 869
public class SelectLine : MonoBehaviour
{
	// Token: 0x06002010 RID: 8208 RVA: 0x0014D244 File Offset: 0x0014B444
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

	// Token: 0x06002011 RID: 8209 RVA: 0x0014D2AC File Offset: 0x0014B4AC
	private void SetPoints(int i)
	{
		for (int j = 0; j < this.lines[i].points2.Count; j++)
		{
			this.lines[i].points2[j] = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height - 20));
		}
		this.lines[i].Draw();
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x0014D318 File Offset: 0x0014B518
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

	// Token: 0x06002013 RID: 8211 RVA: 0x0014D3C2 File Offset: 0x0014B5C2
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 800f, 30f), "Click a line to make a new line");
	}

	// Token: 0x04002869 RID: 10345
	public float lineThickness = 10f;

	// Token: 0x0400286A RID: 10346
	public int extraThickness = 2;

	// Token: 0x0400286B RID: 10347
	public int numberOfLines = 2;

	// Token: 0x0400286C RID: 10348
	private VectorLine[] lines;

	// Token: 0x0400286D RID: 10349
	private bool[] wasSelected;
}
