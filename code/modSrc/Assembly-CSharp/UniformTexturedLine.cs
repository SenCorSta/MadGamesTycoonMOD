using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class UniformTexturedLine : MonoBehaviour
{
	
	private void Start()
	{
		new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height / 2)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, this.lineTexture, this.lineWidth)
		{
			textureScale = this.textureScale
		}.Draw();
	}

	
	public Texture lineTexture;

	
	public float lineWidth = 8f;

	
	public float textureScale = 1f;
}
