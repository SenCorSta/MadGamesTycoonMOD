using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class Highlight : MonoBehaviour
{
	
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

	
	private void SetEnergyLinePoints()
	{
		for (int i = 0; i < this.energyLine.points2.Count; i++)
		{
			float x = Mathf.Lerp(70f, (float)(Screen.width - 20), ((float)i + 0f) / (float)this.energyLine.points2.Count);
			this.energyLine.points2[i] = new Vector2(x, (float)Screen.height * 0.1f);
		}
	}

	
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

	
	private void LateUpdate()
	{
		this.line.Draw();
		this.energyLine.Draw();
	}

	
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

	
	public int lineWidth = 5;

	
	public int energyLineWidth = 4;

	
	public float selectionSize = 0.5f;

	
	public float force = 20f;

	
	public int pointsInEnergyLine = 100;

	
	private VectorLine line;

	
	private VectorLine energyLine;

	
	private RaycastHit hit;

	
	private int selectIndex;

	
	private float energyLevel;

	
	private bool canClick;

	
	private GameObject[] spheres;

	
	private double timer;

	
	private int ignoreLayer;

	
	private int defaultLayer;

	
	private bool fading;
}
