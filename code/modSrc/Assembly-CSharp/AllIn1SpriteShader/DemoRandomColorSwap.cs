using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000402 RID: 1026
	public class DemoRandomColorSwap : MonoBehaviour
	{
		// Token: 0x06002446 RID: 9286 RVA: 0x00174C64 File Offset: 0x00172E64
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

		// Token: 0x06002447 RID: 9287 RVA: 0x00174CC4 File Offset: 0x00172EC4
		private void NewColor()
		{
			this.mat.SetColor("_ColorSwapRed", this.GenerateColor());
			this.mat.SetColor("_ColorSwapGreen", this.GenerateColor());
			this.mat.SetColor("_ColorSwapBlue", this.GenerateColor());
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00174D13 File Offset: 0x00172F13
		private Color GenerateColor()
		{
			return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		}

		// Token: 0x04002E64 RID: 11876
		private Material mat;
	}
}
