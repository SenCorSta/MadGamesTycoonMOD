using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[RequireComponent(typeof(Light))]
public class CFX_LightIntensityFade : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x0000241A File Offset: 0x0000061A
	private void Start()
	{
		this.baseIntensity = base.GetComponent<Light>().intensity;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000242D File Offset: 0x0000062D
	private void OnEnable()
	{
		this.p_lifetime = 0f;
		this.p_delay = this.delay;
		if (this.delay > 0f)
		{
			base.GetComponent<Light>().enabled = false;
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0001A468 File Offset: 0x00018668
	private void Update()
	{
		if (this.p_delay > 0f)
		{
			this.p_delay -= Time.deltaTime;
			if (this.p_delay <= 0f)
			{
				base.GetComponent<Light>().enabled = true;
			}
			return;
		}
		if (this.p_lifetime / this.duration < 1f)
		{
			base.GetComponent<Light>().intensity = Mathf.Lerp(this.baseIntensity, this.finalIntensity, this.p_lifetime / this.duration);
			this.p_lifetime += Time.deltaTime;
			return;
		}
		if (this.autodestruct)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000053 RID: 83
	public float duration = 1f;

	// Token: 0x04000054 RID: 84
	public float delay;

	// Token: 0x04000055 RID: 85
	public float finalIntensity;

	// Token: 0x04000056 RID: 86
	private float baseIntensity;

	// Token: 0x04000057 RID: 87
	public bool autodestruct;

	// Token: 0x04000058 RID: 88
	private float p_lifetime;

	// Token: 0x04000059 RID: 89
	private float p_delay;
}
