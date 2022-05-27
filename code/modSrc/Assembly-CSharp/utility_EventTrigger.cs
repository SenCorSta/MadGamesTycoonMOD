using System;
using Suimono.Core;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class utility_EventTrigger : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x0001BD40 File Offset: 0x00019F40
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

	// Token: 0x060000B4 RID: 180 RVA: 0x0000282E File Offset: 0x00000A2E
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
