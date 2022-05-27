using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class PowerBar : MonoBehaviour
{
	
	private void Start()
	{
		this.position = new Vector2(this.radius + 20f, (float)Screen.height - (this.radius + 20f));
		VectorLine vectorLine = new VectorLine("BarBackground", new List<Vector2>(50), null, (float)this.lineWidth, LineType.Continuous, Joins.Weld);
		vectorLine.MakeCircle(this.position, this.radius);
		vectorLine.Draw();
		this.bar = new VectorLine("TotalBar", new List<Vector2>(this.segmentCount + 1), null, (float)(this.lineWidth - 4), LineType.Continuous, Joins.Weld);
		this.bar.color = Color.black;
		this.bar.MakeArc(this.position, this.radius, this.radius, 0f, 270f);
		this.bar.Draw();
		this.currentPower = UnityEngine.Random.value;
		this.SetTargetPower();
		this.bar.SetColor(Color.red, 0, (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower));
	}

	
	private void SetTargetPower()
	{
		this.targetPower = UnityEngine.Random.value;
	}

	
	private void Update()
	{
		float t = this.currentPower;
		if (this.targetPower < this.currentPower)
		{
			this.currentPower -= this.speed * Time.deltaTime;
			if (this.currentPower < this.targetPower)
			{
				this.SetTargetPower();
			}
			this.bar.SetColor(Color.black, (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower), (int)Mathf.Lerp(0f, (float)this.segmentCount, t));
			return;
		}
		this.currentPower += this.speed * Time.deltaTime;
		if (this.currentPower > this.targetPower)
		{
			this.SetTargetPower();
		}
		this.bar.SetColor(Color.red, (int)Mathf.Lerp(0f, (float)this.segmentCount, t), (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower));
	}

	
	private void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width / 2 - 40), (float)(Screen.height / 2 - 15), 80f, 30f), "Power: " + (this.currentPower * 100f).ToString("f0") + "%");
	}

	
	public float speed = 0.25f;

	
	public int lineWidth = 25;

	
	public float radius = 60f;

	
	public int segmentCount = 200;

	
	private VectorLine bar;

	
	private Vector2 position;

	
	private float currentPower;

	
	private float targetPower;
}
