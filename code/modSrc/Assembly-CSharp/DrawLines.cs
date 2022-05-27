using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000349 RID: 841
public class DrawLines : MonoBehaviour
{
	// Token: 0x06001F5D RID: 8029 RVA: 0x00014C18 File Offset: 0x00012E18
	private void Start()
	{
		this.SetLine();
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x0014B230 File Offset: 0x00149430
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

	// Token: 0x06001F5F RID: 8031 RVA: 0x0014B2B0 File Offset: 0x001494B0
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

	// Token: 0x06001F60 RID: 8032 RVA: 0x0014B3CC File Offset: 0x001495CC
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

	// Token: 0x06001F61 RID: 8033 RVA: 0x00014C20 File Offset: 0x00012E20
	private void RandomizeColor()
	{
		this.line.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x0014B638 File Offset: 0x00149838
	private void RandomizeAllColors()
	{
		int segmentNumber = this.line.GetSegmentNumber();
		for (int i = 0; i < segmentNumber; i++)
		{
			this.line.SetColor(new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value), i);
		}
	}

	// Token: 0x040027B3 RID: 10163
	public float rotateSpeed = 90f;

	// Token: 0x040027B4 RID: 10164
	public float maxPoints = 500f;

	// Token: 0x040027B5 RID: 10165
	private VectorLine line;

	// Token: 0x040027B6 RID: 10166
	private bool endReached;

	// Token: 0x040027B7 RID: 10167
	private bool continuous = true;

	// Token: 0x040027B8 RID: 10168
	private bool oldContinuous = true;

	// Token: 0x040027B9 RID: 10169
	private bool fillJoins;

	// Token: 0x040027BA RID: 10170
	private bool oldFillJoins;

	// Token: 0x040027BB RID: 10171
	private bool weldJoins;

	// Token: 0x040027BC RID: 10172
	private bool oldWeldJoins;

	// Token: 0x040027BD RID: 10173
	private bool thickLine;

	// Token: 0x040027BE RID: 10174
	private bool canClick = true;
}
