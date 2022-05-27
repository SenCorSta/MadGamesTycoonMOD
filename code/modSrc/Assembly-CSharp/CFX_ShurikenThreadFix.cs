using System;
using System.Collections;
using UnityEngine;


public class CFX_ShurikenThreadFix : MonoBehaviour
{
	
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

	
	private ParticleSystem[] systems;
}
