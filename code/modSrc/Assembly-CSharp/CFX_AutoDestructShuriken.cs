using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	
	private void OnEnable()
	{
		base.StartCoroutine("CheckIfAlive");
	}

	
	private IEnumerator CheckIfAlive()
	{
		ParticleSystem ps = base.GetComponent<ParticleSystem>();
		while (ps != null)
		{
			yield return new WaitForSeconds(0.5f);
			if (!ps.IsAlive(true))
			{
				if (this.OnlyDeactivate)
				{
					base.gameObject.SetActive(false);
					break;
				}
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			}
		}
		yield break;
	}

	
	public bool OnlyDeactivate;
}
