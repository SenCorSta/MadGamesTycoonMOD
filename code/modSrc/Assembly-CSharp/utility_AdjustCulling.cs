using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class utility_AdjustCulling : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x000027E7 File Offset: 0x000009E7
	private void Start()
	{
		this.cam = base.gameObject.GetComponent<Camera>();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0001BC38 File Offset: 0x00019E38
	private void LateUpdate()
	{
		if (this.cam != null)
		{
			if (base.transform.position.y > this.altitudeLowerThreshold)
			{
				this.useThreshold = Mathf.Clamp01((base.transform.position.y - this.altitudeLowerThreshold) / (this.altitudeUpperThreshold - this.altitudeLowerThreshold));
			}
			else
			{
				this.useThreshold = 0f;
			}
			this.cam.nearClipPlane = Mathf.Lerp(this.nearCullAtBase, this.nearCullAtAltitude, this.useThreshold);
		}
	}

	// Token: 0x04000104 RID: 260
	public float nearCullAtBase = 0.3f;

	// Token: 0x04000105 RID: 261
	public float nearCullAtAltitude = 5f;

	// Token: 0x04000106 RID: 262
	public float altitudeLowerThreshold = 50f;

	// Token: 0x04000107 RID: 263
	public float altitudeUpperThreshold = 250f;

	// Token: 0x04000108 RID: 264
	private Camera cam;

	// Token: 0x04000109 RID: 265
	private float useThreshold;
}
