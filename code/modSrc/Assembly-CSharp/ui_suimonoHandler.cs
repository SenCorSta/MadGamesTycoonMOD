using System;
using Suimono.Core;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003F RID: 63
public class ui_suimonoHandler : MonoBehaviour
{
	// Token: 0x060000E3 RID: 227 RVA: 0x000214BC File Offset: 0x0001F6BC
	private void Start()
	{
		this.lightObject = GameObject.Find("Directional Light").GetComponent<Transform>();
		this.suimonoModule = GameObject.Find("SUIMONO_Module").GetComponent<SuimonoModule>();
		this.suimonoObject = GameObject.Find("SUIMONO_Surface").GetComponent<SuimonoObject>();
		this.uiCanvasScale = base.transform.GetComponent<CanvasScaler>();
		this.textVersion = GameObject.Find("Text_version").GetComponent<Text>();
		this.sliderTOD = GameObject.Find("Slider_TOD").GetComponent<Slider>();
		this.sliderBeaufort = GameObject.Find("Slider_Beaufort").GetComponent<Slider>();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00021558 File Offset: 0x0001F758
	private void LateUpdate()
	{
		if (this.uiCanvasScale != null)
		{
			this.uiCanvasScale.scaleFactor = this.uiScale;
		}
		if (this.lightObject != null && this.sliderTOD != null)
		{
			this.lightObject.localEulerAngles = new Vector3(Mathf.Lerp(-15f, 90f, this.sliderTOD.value), this.lightObject.localEulerAngles.y, this.lightObject.localEulerAngles.z);
		}
		if (this.suimonoModule != null)
		{
			this.textVersion.text = "Version " + this.suimonoModule.suimonoVersionNumber;
		}
		if (this.suimonoObject != null)
		{
			this.suimonoObject.beaufortScale = this.sliderBeaufort.value;
		}
	}

	// Token: 0x0400025D RID: 605
	public float uiScale = 1f;

	// Token: 0x0400025E RID: 606
	private Transform lightObject;

	// Token: 0x0400025F RID: 607
	private SuimonoModule suimonoModule;

	// Token: 0x04000260 RID: 608
	private SuimonoObject suimonoObject;

	// Token: 0x04000261 RID: 609
	private CanvasScaler uiCanvasScale;

	// Token: 0x04000262 RID: 610
	private Text textVersion;

	// Token: 0x04000263 RID: 611
	private Slider sliderTOD;

	// Token: 0x04000264 RID: 612
	private Slider sliderBeaufort;
}
