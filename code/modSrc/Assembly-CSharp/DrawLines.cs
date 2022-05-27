using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034C RID: 844
public class DrawLines : MonoBehaviour
{
	// Token: 0x06001FB0 RID: 8112 RVA: 0x0014A572 File Offset: 0x00148772
	private void Start()
	{
		this.SetLine();
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x0014A57C File Offset: 0x0014877C
	private void SetLine()
	{
		VectorLine.Destroy(ref this.line);
		if (!this.continuous)
		{
			this.fillJoins = false;
		}
		LineType lineType = this.continuous ? LineType.Continuous : LineType.Discrete;
		Joins joins = this.fillJoins ? Joins.Fill : Joins.None;
		int num = this.thickLine ? 24 : 2;
		this.line = new VectorLine("Line", new List<Vector2>(), (float)num, lineType, joins);
		this.line.drawTransform = base.transform;
		this.endReached = false;
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x0014A5FC File Offset: 0x001487FC
	private void Update()
	{
		Vector3 v = base.transform.InverseTransformPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0) && this.canClick && !this.endReached)
		{
			this.line.points2.Add(v);
			if (this.line.points2.Count == 1)
			{
				this.line.points2.Add(Vector2.zero);
			}
			if ((float)this.line.points2.Count == this.maxPoints)
			{
				this.endReached = true;
			}
		}
		if (this.line.points2.Count >= 2)
		{
			this.line.points2[this.line.points2.Count - 1] = v;
			this.line.Draw();
		}
		base.transform.RotateAround(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vector3.forward, Time.deltaTime * this.rotateSpeed * Input.GetAxis("Horizontal"));
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x0014A718 File Offset: 0x00148918
	private void OnGUI()
	{
		Rect screenRect = new Rect(20f, 20f, 265f, 220f);
		this.canClick = !screenRect.Contains(Event.current.mousePosition);
		GUILayout.BeginArea(screenRect);
		GUI.contentColor = Color.black;
		GUILayout.Label("Click to add points to the line\nRotate with the right/left arrow keys", Array.Empty<GUILayoutOption>());
		GUILayout.Space(5f);
		this.continuous = GUILayout.Toggle(this.continuous, "Continuous line", Array.Empty<GUILayoutOption>());
		this.thickLine = GUILayout.Toggle(this.thickLine, "Thick line", Array.Empty<GUILayoutOption>());
		this.line.lineWidth = (float)(this.thickLine ? 24 : 2);
		this.fillJoins = GUILayout.Toggle(this.fillJoins, "Fill joins (only works with continuous line)", Array.Empty<GUILayoutOption>());
		if (this.line.lineType != LineType.Continuous)
		{
			this.fillJoins = false;
		}
		this.weldJoins = GUILayout.Toggle(this.weldJoins, "Weld joins", Array.Empty<GUILayoutOption>());
		if (this.oldContinuous != this.continuous)
		{
			this.oldContinuous = this.continuous;
			this.line.lineType = (this.continuous ? LineType.Continuous : LineType.Discrete);
		}
		if (this.oldFillJoins != this.fillJoins)
		{
			if (this.fillJoins)
			{
				this.weldJoins = false;
			}
			this.oldFillJoins = this.fillJoins;
		}
		else if (this.oldWeldJoins != this.weldJoins)
		{
			if (this.weldJoins)
			{
				this.fillJoins = false;
			}
			this.oldWeldJoins = this.weldJoins;
		}
		if (this.fillJoins)
		{
			this.line.joins = Joins.Fill;
		}
		else if (this.weldJoins)
		{
			this.line.joins = Joins.Weld;
		}
		else
		{
			this.line.joins = Joins.None;
		}
		GUILayout.Space(10f);
		GUI.contentColor = Color.white;
		if (GUILayout.Button("Randomize Color", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			this.RandomizeColor();
		}
		if (GUILayout.Button("Randomize All Colors", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			this.RandomizeAllColors();
		}
		if (GUILayout.Button("Reset line", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			this.SetLine();
		}
		if (this.endReached)
		{
			GUI.contentColor = Color.black;
			GUILayout.Label("No more points available. You must be bored!", Array.Empty<GUILayoutOption>());
		}
		GUILayout.EndArea();
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x0014A982 File Offset: 0x00148B82
	private void RandomizeColor()
	{
		this.line.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x0014A9A8 File Offset: 0x00148BA8
	private void RandomizeAllColors()
	{
		int segmentNumber = this.line.GetSegmentNumber();
		for (int i = 0; i < segmentNumber; i++)
		{
			this.line.SetColor(new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value), i);
		}
	}

	// Token: 0x040027C9 RID: 10185
	public float rotateSpeed = 90f;

	// Token: 0x040027CA RID: 10186
	public float maxPoints = 500f;

	// Token: 0x040027CB RID: 10187
	private VectorLine line;

	// Token: 0x040027CC RID: 10188
	private bool endReached;

	// Token: 0x040027CD RID: 10189
	private bool continuous = true;

	// Token: 0x040027CE RID: 10190
	private bool oldContinuous = true;

	// Token: 0x040027CF RID: 10191
	private bool fillJoins;

	// Token: 0x040027D0 RID: 10192
	private bool oldFillJoins;

	// Token: 0x040027D1 RID: 10193
	private bool weldJoins;

	// Token: 0x040027D2 RID: 10194
	private bool oldWeldJoins;

	// Token: 0x040027D3 RID: 10195
	private bool thickLine;

	// Token: 0x040027D4 RID: 10196
	private bool canClick = true;
}
