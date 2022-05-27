using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D7 RID: 727
public class cameraOutlineImage : MonoBehaviour
{
	// Token: 0x060019E9 RID: 6633 RVA: 0x000117A1 File Offset: 0x0000F9A1
	private void Start()
	{
		this.myImage = base.GetComponent<RawImage>();
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x0010E1CC File Offset: 0x0010C3CC
	private void Update()
	{
		if (this.blink)
		{
			float a = 0.1f + Mathf.PingPong(Time.realtimeSinceStartup * 2f, 1f) * 0.5f;
			this.myImage.color = new Color(this.myImage.color.r, this.myImage.color.g, this.myImage.color.b, a);
		}
	}

	// Token: 0x0400211F RID: 8479
	private RawImage myImage;

	// Token: 0x04002120 RID: 8480
	public bool blink = true;
}
