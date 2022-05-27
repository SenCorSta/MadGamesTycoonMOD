using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034D RID: 845
public class DrawLinesMouse : MonoBehaviour
{
	// Token: 0x06001FB7 RID: 8119 RVA: 0x0014AA28 File Offset: 0x00148C28
	private void Start()
	{
		Texture2D texture;
		float width;
		if (this.useEndCap)
		{
			VectorLine.SetEndCap("RoundCap", EndCap.Mirror, new Texture2D[]
			{
				this.capLineTex,
				this.capTex
			});
			texture = this.capLineTex;
			width = this.capLineWidth;
		}
		else
		{
			texture = this.lineTex;
			width = this.lineWidth;
		}
		if (this.line3D)
		{
			this.line = new VectorLine("DrawnLine3D", new List<Vector3>(), texture, width, LineType.Continuous, Joins.Weld);
		}
		else
		{
			this.line = new VectorLine("DrawnLine", new List<Vector2>(), texture, width, LineType.Continuous, Joins.Weld);
		}
		this.line.endPointsUpdate = 2;
		if (this.useEndCap)
		{
			this.line.endCap = "RoundCap";
		}
		this.sqrMinPixelMove = this.minPixelMove * this.minPixelMove;
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x0014AAF4 File Offset: 0x00148CF4
	private void Update()
	{
		Vector3 mousePos = this.GetMousePos();
		if (Input.GetMouseButtonDown(0))
		{
			if (this.line3D)
			{
				this.line.points3.Clear();
				this.line.Draw3D();
			}
			else
			{
				this.line.points2.Clear();
				this.line.Draw();
			}
			this.previousPosition = Input.mousePosition;
			if (this.line3D)
			{
				this.line.points3.Add(mousePos);
			}
			else
			{
				this.line.points2.Add(mousePos);
			}
			this.canDraw = true;
			return;
		}
		if (Input.GetMouseButton(0) && (Input.mousePosition - this.previousPosition).sqrMagnitude > (float)this.sqrMinPixelMove && this.canDraw)
		{
			this.previousPosition = Input.mousePosition;
			int count;
			if (this.line3D)
			{
				this.line.points3.Add(mousePos);
				count = this.line.points3.Count;
				this.line.Draw3D();
			}
			else
			{
				this.line.points2.Add(mousePos);
				count = this.line.points2.Count;
				this.line.Draw();
			}
			if (count >= this.maxPoints)
			{
				this.canDraw = false;
			}
		}
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x0014AC58 File Offset: 0x00148E58
	private Vector3 GetMousePos()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.line3D)
		{
			mousePosition.z = this.distanceFromCamera;
			return Camera.main.ScreenToWorldPoint(mousePosition);
		}
		return mousePosition;
	}

	// Token: 0x040027D5 RID: 10197
	public Texture2D lineTex;

	// Token: 0x040027D6 RID: 10198
	public int maxPoints = 5000;

	// Token: 0x040027D7 RID: 10199
	public float lineWidth = 4f;

	// Token: 0x040027D8 RID: 10200
	public int minPixelMove = 5;

	// Token: 0x040027D9 RID: 10201
	public bool useEndCap;

	// Token: 0x040027DA RID: 10202
	public Texture2D capLineTex;

	// Token: 0x040027DB RID: 10203
	public Texture2D capTex;

	// Token: 0x040027DC RID: 10204
	public float capLineWidth = 20f;

	// Token: 0x040027DD RID: 10205
	public bool line3D;

	// Token: 0x040027DE RID: 10206
	public float distanceFromCamera = 1f;

	// Token: 0x040027DF RID: 10207
	private VectorLine line;

	// Token: 0x040027E0 RID: 10208
	private Vector3 previousPosition;

	// Token: 0x040027E1 RID: 10209
	private int sqrMinPixelMove;

	// Token: 0x040027E2 RID: 10210
	private bool canDraw;
}
