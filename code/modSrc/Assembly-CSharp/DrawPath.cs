using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035B RID: 859
public class DrawPath : MonoBehaviour
{
	// Token: 0x06001F9D RID: 8093 RVA: 0x0014CF28 File Offset: 0x0014B128
	private void Start()
	{
		this.pathLine = new VectorLine("Path", new List<Vector3>(), this.lineTex, 12f, LineType.Continuous);
		this.pathLine.color = Color.green;
		this.pathLine.textureScale = 1f;
		this.MakeBall();
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x0014CF9C File Offset: 0x0014B19C
	private void MakeBall()
	{
		if (this.ball)
		{
			UnityEngine.Object.Destroy(this.ball);
		}
		this.ball = UnityEngine.Object.Instantiate<GameObject>(this.ballPrefab, new Vector3(-2.25f, -4.4f, -1.9f), Quaternion.Euler(300f, 70f, 310f));
		this.ball.GetComponent<Rigidbody>().useGravity = true;
		this.ball.GetComponent<Rigidbody>().AddForce(this.ball.transform.forward * this.force, ForceMode.Impulse);
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x00014F74 File Offset: 0x00013174
	private IEnumerator SamplePoints(Transform thisTransform)
	{
		bool running = true;
		while (running)
		{
			this.pathLine.points3.Add(thisTransform.position);
			int num = this.pathIndex + 1;
			this.pathIndex = num;
			if (num == this.maxPoints)
			{
				running = false;
			}
			yield return new WaitForSeconds(0.05f);
			if (this.continuousUpdate)
			{
				this.pathLine.Draw();
			}
		}
		yield break;
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x0014D038 File Offset: 0x0014B238
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Reset"))
		{
			this.Reset();
		}
		if (!this.continuousUpdate && GUI.Button(new Rect(10f, 45f, 100f, 30f), "Draw Path"))
		{
			this.pathLine.Draw();
		}
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x0014D0A8 File Offset: 0x0014B2A8
	private void Reset()
	{
		base.StopAllCoroutines();
		this.MakeBall();
		this.pathLine.points3.Clear();
		this.pathLine.Draw();
		this.pathIndex = 0;
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	// Token: 0x04002827 RID: 10279
	public Texture lineTex;

	// Token: 0x04002828 RID: 10280
	public Color lineColor = Color.green;

	// Token: 0x04002829 RID: 10281
	public int maxPoints = 500;

	// Token: 0x0400282A RID: 10282
	public bool continuousUpdate = true;

	// Token: 0x0400282B RID: 10283
	public GameObject ballPrefab;

	// Token: 0x0400282C RID: 10284
	public float force = 16f;

	// Token: 0x0400282D RID: 10285
	private VectorLine pathLine;

	// Token: 0x0400282E RID: 10286
	private int pathIndex;

	// Token: 0x0400282F RID: 10287
	private GameObject ball;
}
