using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class CFX_ShurikenThreadFix : MonoBehaviour
{
	// Token: 0x06000060 RID: 96 RVA: 0x00003BB8 File Offset: 0x00001DB8
	private void OnEnable()
	{
		this.systems = base.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in this.systems)
		{
			particleSystem.Stop(true);
			particleSystem.Clear(true);
		}
		base.StartCoroutine("WaitFrame");
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003C02 File Offset: 0x00001E02
	private IEnumerator WaitFrame()
	{
		yield return null;
		ParticleSystem[] array = this.systems;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play(true);
		}
		yield break;
	}

	// Token: 0x0400005A RID: 90
	private ParticleSystem[] systems;
}
