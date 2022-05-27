using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035F RID: 863
public class CreateHills : MonoBehaviour
{
	// Token: 0x06001FB0 RID: 8112 RVA: 0x0014D518 File Offset: 0x0014B718
	private void Start()
	{
		this.storedPosition = this.ball.transform.position;
		this.splinePoints = new Vector2[this.numberOfHills * 2 + 1];
		this.hills = new VectorLine("Hills", new List<Vector2>(this.numberOfPoints), this.hillTexture, 12f, LineType.Continuous, Joins.Weld);
		this.hills.useViewportCoords = true;
		this.hills.collider = true;
		this.hills.physicsMaterial = this.hillPhysicsMaterial;
		UnityEngine.Random.InitState(95);
		this.CreateHillLine();
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x0014D5B0 File Offset: 0x0014B7B0
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 150f, 40f), "Make new hills"))
		{
			this.CreateHillLine();
			this.ball.transform.position = this.storedPosition;
			this.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.ball.GetComponent<Rigidbody2D>().WakeUp();
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x0014D624 File Offset: 0x0014B824
	private void CreateHillLine()
	{
		this.splinePoints[0] = new Vector2(-0.02f, UnityEngine.Random.Range(0.1f, 0.6f));
		float num = 0f;
		float num2 = 1f / (float)(this.numberOfHills * 2);
		int i;
		for (i = 1; i < this.splinePoints.Length; i += 2)
		{
			num += num2;
			this.splinePoints[i] = new Vector2(num, UnityEngine.Random.Range(0.3f, 0.7f));
			num += num2;
			this.splinePoints[i + 1] = new Vector2(num, UnityEngine.Random.Range(0.1f, 0.6f));
		}
		this.splinePoints[i - 1] = new Vector2(1.02f, UnityEngine.Random.Range(0.1f, 0.6f));
		this.hills.MakeSpline(this.splinePoints);
		this.hills.Draw();
	}

	// Token: 0x04002841 RID: 10305
	public Texture hillTexture;

	// Token: 0x04002842 RID: 10306
	public PhysicsMaterial2D hillPhysicsMaterial;

	// Token: 0x04002843 RID: 10307
	public int numberOfPoints = 100;

	// Token: 0x04002844 RID: 10308
	public int numberOfHills = 4;

	// Token: 0x04002845 RID: 10309
	public GameObject ball;

	// Token: 0x04002846 RID: 10310
	private Vector3 storedPosition;

	// Token: 0x04002847 RID: 10311
	private VectorLine hills;

	// Token: 0x04002848 RID: 10312
	private Vector2[] splinePoints;
}
