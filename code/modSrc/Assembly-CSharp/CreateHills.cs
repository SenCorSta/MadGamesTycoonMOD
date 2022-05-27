using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000362 RID: 866
public class CreateHills : MonoBehaviour
{
	// Token: 0x06002003 RID: 8195 RVA: 0x0014CC7C File Offset: 0x0014AE7C
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

	// Token: 0x06002004 RID: 8196 RVA: 0x0014CD14 File Offset: 0x0014AF14
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

	// Token: 0x06002005 RID: 8197 RVA: 0x0014CD88 File Offset: 0x0014AF88
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

	// Token: 0x04002857 RID: 10327
	public Texture hillTexture;

	// Token: 0x04002858 RID: 10328
	public PhysicsMaterial2D hillPhysicsMaterial;

	// Token: 0x04002859 RID: 10329
	public int numberOfPoints = 100;

	// Token: 0x0400285A RID: 10330
	public int numberOfHills = 4;

	// Token: 0x0400285B RID: 10331
	public GameObject ball;

	// Token: 0x0400285C RID: 10332
	private Vector3 storedPosition;

	// Token: 0x0400285D RID: 10333
	private VectorLine hills;

	// Token: 0x0400285E RID: 10334
	private Vector2[] splinePoints;
}
