using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035E RID: 862
public class DrawPath : MonoBehaviour
{
	// Token: 0x06001FF0 RID: 8176 RVA: 0x0014C5C0 File Offset: 0x0014A7C0
	private void Start()
	{
		this.pathLine = new VectorLine("Path", new List<Vector3>(), this.lineTex, 12f, LineType.Continuous);
		this.pathLine.color = Color.green;
		this.pathLine.textureScale = 1f;
		this.MakeBall();
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x0014C634 File Offset: 0x0014A834
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

	// Token: 0x06001FF2 RID: 8178 RVA: 0x0014C6CF File Offset: 0x0014A8CF
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

	// Token: 0x06001FF3 RID: 8179 RVA: 0x0014C6E8 File Offset: 0x0014A8E8
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

	// Token: 0x06001FF4 RID: 8180 RVA: 0x0014C758 File Offset: 0x0014A958
	private void Reset()
	{
		base.StopAllCoroutines();
		this.MakeBall();
		this.pathLine.points3.Clear();
		this.pathLine.Draw();
		this.pathIndex = 0;
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	// Token: 0x0400283D RID: 10301
	public Texture lineTex;

	// Token: 0x0400283E RID: 10302
	public Color lineColor = Color.green;

	// Token: 0x0400283F RID: 10303
	public int maxPoints = 500;

	// Token: 0x04002840 RID: 10304
	public bool continuousUpdate = true;

	// Token: 0x04002841 RID: 10305
	public GameObject ballPrefab;

	// Token: 0x04002842 RID: 10306
	public float force = 16f;

	// Token: 0x04002843 RID: 10307
	private VectorLine pathLine;

	// Token: 0x04002844 RID: 10308
	private int pathIndex;

	// Token: 0x04002845 RID: 10309
	private GameObject ball;
}
