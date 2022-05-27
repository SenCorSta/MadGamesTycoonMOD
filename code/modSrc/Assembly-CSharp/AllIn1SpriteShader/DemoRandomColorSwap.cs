using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FF RID: 1023
	public class DemoRandomColorSwap : MonoBehaviour
	{
		// Token: 0x060023F3 RID: 9203 RVA: 0x00171BEC File Offset: 0x0016FDEC
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

		// Token: 0x060023F4 RID: 9204 RVA: 0x00171C4C File Offset: 0x0016FE4C
		private void NewColor()
		{
			this.mat.SetColor("_ColorSwapRed", this.GenerateColor());
			this.mat.SetColor("_ColorSwapGreen", this.GenerateColor());
			this.mat.SetColor("_ColorSwapBlue", this.GenerateColor());
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00018796 File Offset: 0x00016996
		private Color GenerateColor()
		{
			return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		}

		// Token: 0x04002E4E RID: 11854
		private Material mat;
	}
}
