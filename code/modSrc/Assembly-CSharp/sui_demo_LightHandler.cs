using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class sui_demo_LightHandler : MonoBehaviour
{
	// Token: 0x060000CF RID: 207 RVA: 0x000097BD File Offset: 0x000079BD
	private void Start()
	{
		this.lightObject = base.gameObject.GetComponent<Light>();
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000097D0 File Offset: 0x000079D0
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

	// Token: 0x040001F1 RID: 497
	public float dayIntensity = 1f;

	// Token: 0x040001F2 RID: 498
	public float nightIntensity = 0.01f;

	// Token: 0x040001F3 RID: 499
	public float sunsetDegrees = 20f;

	// Token: 0x040001F4 RID: 500
	public float lightDegrees = 10f;

	// Token: 0x040001F5 RID: 501
	public Color dayColor = new Color(1f, 0.968f, 0.933f, 1f);

	// Token: 0x040001F6 RID: 502
	public Color sunsetColor = new Color(0.77f, 0.33f, 0f, 1f);

	// Token: 0x040001F7 RID: 503
	private Light lightObject;

	// Token: 0x040001F8 RID: 504
	private float lightFac;

	// Token: 0x040001F9 RID: 505
	private float sunsetFac;
}
