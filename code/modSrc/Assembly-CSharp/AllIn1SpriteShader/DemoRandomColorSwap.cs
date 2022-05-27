using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	public class DemoRandomColorSwap : MonoBehaviour
	{
		
		private void Start()
		{
			if (base.GetComponent<SpriteRenderer>() != null)
			{
				this.mat = base.GetComponent<Renderer>().material;
				if (this.mat != null)
				{
					base.InvokeRepeating("NewColor", 0f, 0.6f);
					return;
				}
				Debug.LogError("No material found");
				UnityEngine.Object.Destroy(this);
			}
		}

		
		private void NewColor()
		{
			this.mat.SetColor("_ColorSwapRed", this.GenerateColor());
			this.mat.SetColor("_ColorSwapGreen", this.GenerateColor());
			this.mat.SetColor("_ColorSwapBlue", this.GenerateColor());
		}

		
		private Color GenerateColor()
		{
			return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		}

		
		private Material mat;
	}
}
