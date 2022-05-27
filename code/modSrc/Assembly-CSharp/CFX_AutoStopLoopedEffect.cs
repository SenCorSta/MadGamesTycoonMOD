using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoStopLoopedEffect : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x0000212F File Offset: 0x0000032F
	private void OnEnable()
	{
		this.d = this.effectDuration;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00019CF0 File Offset: 0x00017EF0
	private void Update()
	{
		if (this.d > 0f)
		{
			this.d -= Time.deltaTime;
			if (this.d <= 0f)
			{
				base.GetComponent<ParticleSystem>().Stop(true);
				CFX_Demo_Translate component = base.gameObject.GetComponent<CFX_Demo_Translate>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
		}
	}

	// Token: 0x0400001C RID: 28
	public float effectDuration = 2.5f;

	// Token: 0x0400001D RID: 29
	private float d;
}
