using System;
using Suimono.Core;
using UnityEngine;
using UnityEngine.UI;


public class ui_suimonoHandler : MonoBehaviour
{
	
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

	
	public float uiScale = 1f;

	
	private Transform lightObject;

	
	private SuimonoModule suimonoModule;

	
	private SuimonoObject suimonoObject;

	
	private CanvasScaler uiCanvasScale;

	
	private Text textVersion;

	
	private Slider sliderTOD;

	
	private Slider sliderBeaufort;
}
