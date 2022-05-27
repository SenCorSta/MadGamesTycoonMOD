using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;


public class EndCapDemo : MonoBehaviour
{
	
	private void Start()
	{
		VectorLine.SetEndCap("arrow", EndCap.Front, new Texture2D[]
		{
			this.lineTex,
			this.frontTex
		});
		VectorLine.SetEndCap("arrow2", EndCap.Both, new Texture2D[]
		{
			this.lineTex2,
			this.frontTex,
			this.backTex
		});
		VectorLine.SetEndCap("rounded", EndCap.Mirror, new Texture2D[]
		{
			this.lineTex3,
			this.capTex
		});
		VectorLine vectorLine = new VectorLine("Arrow", new List<Vector2>(50), 30f, LineType.Continuous, Joins.Weld);
		vectorLine.useViewportCoords = true;
		Vector2[] splinePoints = new Vector2[]
		{
			new Vector2(0.1f, 0.15f),
			new Vector2(0.3f, 0.5f),
			new Vector2(0.5f, 0.6f),
			new Vector2(0.7f, 0.5f),
			new Vector2(0.9f, 0.15f)
		};
		vectorLine.MakeSpline(splinePoints);
		vectorLine.endCap = "arrow";
		vectorLine.Draw();
		VectorLine vectorLine2 = new VectorLine("Arrow2", new List<Vector2>(50), 40f, LineType.Continuous, Joins.Weld);
		vectorLine2.useViewportCoords = true;
		splinePoints = new Vector2[]
		{
			new Vector2(0.1f, 0.85f),
			new Vector2(0.3f, 0.5f),
			new Vector2(0.5f, 0.4f),
			new Vector2(0.7f, 0.5f),
			new Vector2(0.9f, 0.85f)
		};
		vectorLine2.MakeSpline(splinePoints);
		vectorLine2.endCap = "arrow2";
		vectorLine2.continuousTexture = true;
		vectorLine2.Draw();
		new VectorLine("Rounded", new List<Vector2>
		{
			new Vector2(0.1f, 0.5f),
			new Vector2(0.9f, 0.5f)
		}, 20f)
		{
			useViewportCoords = true,
			endCap = "rounded"
		}.Draw();
	}

	
	public Texture2D lineTex;

	
	public Texture2D lineTex2;

	
	public Texture2D lineTex3;

	
	public Texture2D frontTex;

	
	public Texture2D backTex;

	
	public Texture2D capTex;
}
