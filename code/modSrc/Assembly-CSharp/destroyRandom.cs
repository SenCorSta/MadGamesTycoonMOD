using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class destroyRandom : MonoBehaviour
{
	// Token: 0x06001A0F RID: 6671 RVA: 0x000118FA File Offset: 0x0000FAFA
	private void Start()
	{
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
