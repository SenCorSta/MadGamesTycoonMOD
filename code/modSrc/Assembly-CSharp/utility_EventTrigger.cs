using System;
using Suimono.Core;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class utility_EventTrigger : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00005798 File Offset: 0x00003998
	private void Start()
	{
		this.target = base.GetComponent<fx_EffectObject>();
		if (this.target != null)
		{
			this.target.OnTrigger += this.OnTrigger;
			return;
		}
		Debug.Log("#EffectTriggerUsage# Can't find fx_EffectObject on " + base.transform.name, base.gameObject);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000057F7 File Offset: 0x000039F7
	private void OnTrigger(Vector3 position, Quaternion rotatoin)
	{
		Debug.LogFormat(base.gameObject, "#EffectTriggerUsage# Trigger, position={0}, rotation={1}", new object[]
		{
			position,
			rotatoin.eulerAngles
		});
	}

	// Token: 0x04000113 RID: 275
	private fx_EffectObject target;
}
