using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000E RID: 14
[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	// Token: 0x0600004B RID: 75 RVA: 0x00003925 File Offset: 0x00001B25
	private void OnEnable()
	{
		base.StartCoroutine("CheckIfAlive");
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003933 File Offset: 0x00001B33
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

	// Token: 0x04000042 RID: 66
	public bool OnlyDeactivate;
}
