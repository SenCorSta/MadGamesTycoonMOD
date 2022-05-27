using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034A RID: 842
public class DrawLinesMouse : MonoBehaviour
{
	// Token: 0x06001F64 RID: 8036 RVA: 0x0014B684 File Offset: 0x00149884
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

	// Token: 0x06001F65 RID: 8037 RVA: 0x0014B750 File Offset: 0x00149950
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

	// Token: 0x06001F66 RID: 8038 RVA: 0x0014B8B4 File Offset: 0x00149AB4
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

	// Token: 0x040027BF RID: 10175
	public Texture2D lineTex;

	// Token: 0x040027C0 RID: 10176
	public int maxPoints = 5000;

	// Token: 0x040027C1 RID: 10177
	public float lineWidth = 4f;

	// Token: 0x040027C2 RID: 10178
	public int minPixelMove = 5;

	// Token: 0x040027C3 RID: 10179
	public bool useEndCap;

	// Token: 0x040027C4 RID: 10180
	public Texture2D capLineTex;

	// Token: 0x040027C5 RID: 10181
	public Texture2D capTex;

	// Token: 0x040027C6 RID: 10182
	public float capLineWidth = 20f;

	// Token: 0x040027C7 RID: 10183
	public bool line3D;

	// Token: 0x040027C8 RID: 10184
	public float distanceFromCamera = 1f;

	// Token: 0x040027C9 RID: 10185
	private VectorLine line;

	// Token: 0x040027CA RID: 10186
	private Vector3 previousPosition;

	// Token: 0x040027CB RID: 10187
	private int sqrMinPixelMove;

	// Token: 0x040027CC RID: 10188
	private bool canDraw;
}
