using System;
using Suimono.Core;
using UnityEngine;


public class utility_EventTrigger : MonoBehaviour
{
	
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

	
	private void OnTrigger(Vector3 position, Quaternion rotatoin)
	{
		Debug.LogFormat(base.gameObject, "#EffectTriggerUsage# Trigger, position={0}, rotation={1}", new object[]
		{
			position,
			rotatoin.eulerAngles
		});
	}

	
	private fx_EffectObject target;
}
