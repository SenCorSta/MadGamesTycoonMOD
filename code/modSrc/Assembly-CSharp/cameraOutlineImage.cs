using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DA RID: 730
public class cameraOutlineImage : MonoBehaviour
{
	// Token: 0x06001A33 RID: 6707 RVA: 0x00109FC9 File Offset: 0x001081C9
	private void Start()
	{
		this.myImage = base.GetComponent<RawImage>();
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x00109FD8 File Offset: 0x001081D8
	private void Update()
	{
		if (this.blink)
		{
			float a = 0.1f + Mathf.PingPong(Time.realtimeSinceStartup * 2f, 1f) * 0.5f;
			this.myImage.color = new Color(this.myImage.color.r, this.myImage.color.g, this.myImage.color.b, a);
		}
	}

	// Token: 0x04002139 RID: 8505
	private RawImage myImage;

	// Token: 0x0400213A RID: 8506
	public bool blink = true;
}
