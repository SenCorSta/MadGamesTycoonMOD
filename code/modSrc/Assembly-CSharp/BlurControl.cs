using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class BlurControl : MonoBehaviour
{
	// Token: 0x06000022 RID: 34 RVA: 0x00002F29 File Offset: 0x00001129
	private void Start()
	{
		this.value = 0f;
		base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002F58 File Offset: 0x00001158
	private void Update()
	{
		if (Input.GetButton("Up"))
		{
			this.value += Time.deltaTime;
			if (this.value > 20f)
			{
				this.value = 20f;
			}
			base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
			return;
		}
		if (Input.GetButton("Down"))
		{
			this.value = (this.value - Time.deltaTime) % 20f;
			if (this.value < 0f)
			{
				this.value = 0f;
			}
			base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00003018 File Offset: 0x00001218
	private void OnGUI()
	{
		GUI.TextArea(new Rect(10f, 10f, 200f, 50f), "Press the 'Up' and 'Down' arrows \nto interact with the blur plane\nCurrent value: " + this.value);
	}

	// Token: 0x0400001B RID: 27
	private float value;
}
