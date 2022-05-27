﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000354 RID: 852
public class Highlight : MonoBehaviour
{
	// Token: 0x06001FCC RID: 8140 RVA: 0x0014B648 File Offset: 0x00149848
	private void Start()
	{
		Time.fixedDeltaTime = 0.01f;
		this.spheres = new GameObject[base.GetComponent<MakeSpheres>().numberOfSpheres];
		this.ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
		this.defaultLayer = LayerMask.NameToLayer("Default");
		this.line = new VectorLine("Line", new List<Vector2>(), (float)this.lineWidth);
		this.line.color = Color.green;
		this.line.capLength = (float)this.lineWidth * 0.5f;
		this.energyLine = new VectorLine("Energy", new List<Vector2>(this.pointsInEnergyLine), null, (float)this.energyLineWidth, LineType.Continuous);
		this.SetEnergyLinePoints();
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x0014B708 File Offset: 0x00149908
	private void SetEnergyLinePoints()
	{
		for (int i = 0; i < this.energyLine.points2.Count; i++)
		{
			float x = Mathf.Lerp(70f, (float)(Screen.width - 20), ((float)i + 0f) / (float)this.energyLine.points2.Count);
			this.energyLine.points2[i] = new Vector2(x, (float)Screen.height * 0.1f);
		}
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x0014B784 File Offset: 0x00149984
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > 50f && !this.fading)
		{
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && this.selectIndex > 0)
			{
				this.ResetSelection(true);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out this.hit))
			{
				this.spheres[this.selectIndex] = this.hit.collider.gameObject;
				this.spheres[this.selectIndex].layer = this.ignoreLayer;
				this.spheres[this.selectIndex].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
				this.selectIndex++;
				this.line.Resize(this.selectIndex * 10);
			}
		}
		for (int i = 0; i < this.selectIndex; i++)
		{
			float num = (float)Screen.height * this.selectionSize / Camera.main.transform.InverseTransformPoint(this.spheres[i].transform.position).z;
			Vector3 vector = Camera.main.WorldToScreenPoint(this.spheres[i].transform.position);
			Rect rect = new Rect(vector.x - num, vector.y - num, num * 2f, num * 2f);
			this.line.MakeRect(rect, i * 10);
			this.line.points2[i * 10 + 8] = new Vector2(rect.x - (float)this.lineWidth * 0.25f, rect.y + num);
			this.line.points2[i * 10 + 9] = new Vector2(35f, Mathf.Lerp(65f, (float)(Screen.height - 25), this.energyLevel));
			this.spheres[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(this.energyLevel, this.energyLevel, this.energyLevel));
		}
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x0014B9BC File Offset: 0x00149BBC
	private void FixedUpdate()
	{
		int i;
		for (i = 0; i < this.energyLine.points2.Count - 1; i++)
		{
			this.energyLine.points2[i] = new Vector2(this.energyLine.points2[i].x, this.energyLine.points2[i + 1].y);
		}
		this.timer += (double)(Time.deltaTime * Mathf.Lerp(5f, 20f, this.energyLevel));
		this.energyLine.points2[i] = new Vector2(this.energyLine.points2[i].x, (float)Screen.height * (0.1f + Mathf.Sin((float)this.timer) * 0.08f * this.energyLevel));
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x0014BAA5 File Offset: 0x00149CA5
	private void LateUpdate()
	{
		this.line.Draw();
		this.energyLine.Draw();
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x0014BAC0 File Offset: 0x00149CC0
	private void ResetSelection(bool instantFade)
	{
		if (this.energyLevel > 0f)
		{
			base.StartCoroutine(this.FadeColor(instantFade));
		}
		this.selectIndex = 0;
		this.energyLevel = 0f;
		this.line.points2.Clear();
		this.line.Draw();
		foreach (GameObject gameObject in this.spheres)
		{
			if (gameObject)
			{
				gameObject.layer = this.defaultLayer;
			}
		}
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x0014BB42 File Offset: 0x00149D42
	private IEnumerator FadeColor(bool instantFade)
	{
		if (instantFade)
		{
			for (int i = 0; i < this.selectIndex; i++)
			{
				this.spheres[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
			}
		}
		else
		{
			this.fading = true;
			Color startColor = new Color(this.energyLevel, this.energyLevel, this.energyLevel, 0f);
			int thisIndex = this.selectIndex;
			for (float t = 0f; t < 1f; t += Time.deltaTime)
			{
				for (int j = 0; j < thisIndex; j++)
				{
					this.spheres[j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(startColor, Color.black, t));
				}
				yield return null;
			}
			this.fading = false;
			startColor = default(Color);
		}
		yield break;
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x0014BB58 File Offset: 0x00149D58
	private void OnGUI()
	{
		GUI.Label(new Rect(60f, 20f, 600f, 40f), "Click to select sphere, shift-click to select multiple spheres\nThen change energy level slider and click Go");
		this.energyLevel = GUI.VerticalSlider(new Rect(30f, 20f, 10f, (float)(Screen.height - 80)), this.energyLevel, 1f, 0f);
		if (this.selectIndex == 0)
		{
			this.energyLevel = 0f;
		}
		if (GUI.Button(new Rect(20f, (float)(Screen.height - 40), 32f, 20f), "Go"))
		{
			for (int i = 0; i < this.selectIndex; i++)
			{
				this.spheres[i].GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * this.force * this.energyLevel, ForceMode.VelocityChange);
			}
			this.ResetSelection(false);
		}
	}

	// Token: 0x04002805 RID: 10245
	public int lineWidth = 5;

	// Token: 0x04002806 RID: 10246
	public int energyLineWidth = 4;

	// Token: 0x04002807 RID: 10247
	public float selectionSize = 0.5f;

	// Token: 0x04002808 RID: 10248
	public float force = 20f;

	// Token: 0x04002809 RID: 10249
	public int pointsInEnergyLine = 100;

	// Token: 0x0400280A RID: 10250
	private VectorLine line;

	// Token: 0x0400280B RID: 10251
	private VectorLine energyLine;

	// Token: 0x0400280C RID: 10252
	private RaycastHit hit;

	// Token: 0x0400280D RID: 10253
	private int selectIndex;

	// Token: 0x0400280E RID: 10254
	private float energyLevel;

	// Token: 0x0400280F RID: 10255
	private bool canClick;

	// Token: 0x04002810 RID: 10256
	private GameObject[] spheres;

	// Token: 0x04002811 RID: 10257
	private double timer;

	// Token: 0x04002812 RID: 10258
	private int ignoreLayer;

	// Token: 0x04002813 RID: 10259
	private int defaultLayer;

	// Token: 0x04002814 RID: 10260
	private bool fading;
}
