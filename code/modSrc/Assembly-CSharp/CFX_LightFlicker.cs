using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
[RequireComponent(typeof(Light))]
public class CFX_LightFlicker : MonoBehaviour
{
	// Token: 0x06000058 RID: 88 RVA: 0x00002394 File Offset: 0x00000594
	private void Awake()
	{
		this.baseIntensity = base.GetComponent<Light>().intensity;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000023A7 File Offset: 0x000005A7
	private void OnEnable()
	{
		this.minIntensity = this.baseIntensity;
		this.maxIntensity = this.minIntensity + this.addIntensity;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000023C8 File Offset: 0x000005C8
	private void Update()
	{
		base.GetComponent<Light>().intensity = Mathf.Lerp(this.minIntensity, this.maxIntensity, Mathf.PerlinNoise(Time.time * this.smoothFactor, 0f));
	}

	// Token: 0x0400004D RID: 77
	public bool loop;

	// Token: 0x0400004E RID: 78
	public float smoothFactor = 1f;

	// Token: 0x0400004F RID: 79
	public float addIntensity = 1f;

	// Token: 0x04000050 RID: 80
	private float minIntensity;

	// Token: 0x04000051 RID: 81
	private float maxIntensity;

	// Token: 0x04000052 RID: 82
	private float baseIntensity;
}
