using System;
using UnityEngine;


public class sui_demo_LightHandler : MonoBehaviour
{
	
	private void Start()
	{
		this.lightObject = base.gameObject.GetComponent<Light>();
	}

	
	private void LateUpdate()
	{
		if (this.lightObject != null)
		{
			this.sunsetDegrees = Mathf.Clamp(this.sunsetDegrees, 0f, 90f);
			this.lightFac = base.transform.eulerAngles.x;
			if (this.lightFac > 90f)
			{
				this.lightFac = 0f;
			}
			this.sunsetFac = Mathf.Clamp01(this.lightFac / this.sunsetDegrees);
			this.lightFac = Mathf.Clamp01(this.lightFac / this.lightDegrees);
			this.lightObject.intensity = Mathf.Lerp(this.nightIntensity, this.dayIntensity, this.lightFac);
			if (this.lightObject.intensity < 0.01f)
			{
				this.lightObject.intensity = 0.01f;
			}
			this.lightObject.color = Color.Lerp(this.sunsetColor, this.dayColor, this.sunsetFac);
		}
	}

	
	public float dayIntensity = 1f;

	
	public float nightIntensity = 0.01f;

	
	public float sunsetDegrees = 20f;

	
	public float lightDegrees = 10f;

	
	public Color dayColor = new Color(1f, 0.968f, 0.933f, 1f);

	
	public Color sunsetColor = new Color(0.77f, 0.33f, 0f, 1f);

	
	private Light lightObject;

	
	private float lightFac;

	
	private float sunsetFac;
}
