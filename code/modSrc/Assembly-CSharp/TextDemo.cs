using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class TextDemo : MonoBehaviour
{
	
	private void Start()
	{
		this.textLine = new VectorLine("Text", new List<Vector2>(), 1f);
		this.textLine.color = Color.yellow;
		this.textLine.drawTransform = base.transform;
		this.textLine.MakeText(this.text, new Vector2((float)(Screen.width / 2 - this.text.Length * this.textSize / 2), (float)(Screen.height / 2 + this.textSize / 2)), (float)this.textSize);
	}

	
	private void Update()
	{
		base.transform.RotateAround(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vector3.forward, Time.deltaTime * 45f);
		Vector3 localScale = base.transform.localScale;
		localScale.x = 1f + Mathf.Sin(Time.time * 3f) * 0.3f;
		localScale.y = 1f + Mathf.Cos(Time.time * 3f) * 0.3f;
		base.transform.localScale = localScale;
		this.textLine.Draw();
	}

	
	public string text = "Vectrosity!";

	
	public int textSize = 40;

	
	private VectorLine textLine;
}
