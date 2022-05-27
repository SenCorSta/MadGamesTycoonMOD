using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000364 RID: 868
public class ScribbleCube : MonoBehaviour
{
	// Token: 0x06002009 RID: 8201 RVA: 0x0014CF18 File Offset: 0x0014B118
	private void Start()
	{
		this.line = new VectorLine("Line", new List<Vector3>(this.numberOfPoints), this.lineTexture, (float)this.lineWidth, LineType.Continuous);
		this.line.material = this.lineMaterial;
		this.line.drawTransform = base.transform;
		this.LineSetup(false);
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x0014CF78 File Offset: 0x0014B178
	private void LineSetup(bool resize)
	{
		if (resize)
		{
			this.lineColors = null;
			this.line.Resize(this.numberOfPoints);
		}
		for (int i = 0; i < this.line.points3.Count; i++)
		{
			this.line.points3[i] = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
		}
		this.SetLineColors();
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x0014D004 File Offset: 0x0014B204
	private void SetLineColors()
	{
		if (this.lineColors == null)
		{
			this.lineColors = new List<Color32>(new Color32[this.numberOfPoints - 1]);
		}
		for (int i = 0; i < this.lineColors.Count; i++)
		{
			this.lineColors[i] = Color.Lerp(this.color1, this.color2, (float)i / (float)this.lineColors.Count);
		}
		this.line.SetColors(this.lineColors);
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x0014D089 File Offset: 0x0014B289
	private void LateUpdate()
	{
		this.line.Draw();
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x0014D098 File Offset: 0x0014B298
	private void OnGUI()
	{
		GUI.Label(new Rect(20f, 10f, 250f, 30f), "Zoom with scrollwheel or arrow keys");
		if (GUI.Button(new Rect(20f, 50f, 100f, 30f), "Change colors"))
		{
			int num = UnityEngine.Random.Range(0, 3);
			int num2;
			do
			{
				num2 = UnityEngine.Random.Range(0, 3);
			}
			while (num2 == num);
			this.color1 = this.RandomColor(this.color1, num);
			this.color2 = this.RandomColor(this.color2, num2);
			this.SetLineColors();
		}
		GUI.Label(new Rect(20f, 100f, 150f, 30f), "Number of points: " + this.numberOfPoints);
		this.numberOfPoints = (int)GUI.HorizontalSlider(new Rect(20f, 130f, 120f, 30f), (float)this.numberOfPoints, 50f, 1000f);
		if (GUI.Button(new Rect(160f, 120f, 40f, 30f), "Set"))
		{
			this.LineSetup(true);
		}
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x0014D1C4 File Offset: 0x0014B3C4
	private Color RandomColor(Color color, int component)
	{
		for (int i = 0; i < 3; i++)
		{
			if (i == component)
			{
				color[i] = UnityEngine.Random.value * 0.25f;
			}
			else
			{
				color[i] = UnityEngine.Random.value * 0.5f + 0.5f;
			}
		}
		return color;
	}

	// Token: 0x04002861 RID: 10337
	public Texture lineTexture;

	// Token: 0x04002862 RID: 10338
	public Material lineMaterial;

	// Token: 0x04002863 RID: 10339
	public int lineWidth = 14;

	// Token: 0x04002864 RID: 10340
	private Color color1 = Color.green;

	// Token: 0x04002865 RID: 10341
	private Color color2 = Color.blue;

	// Token: 0x04002866 RID: 10342
	private VectorLine line;

	// Token: 0x04002867 RID: 10343
	private List<Color32> lineColors;

	// Token: 0x04002868 RID: 10344
	private int numberOfPoints = 350;
}
