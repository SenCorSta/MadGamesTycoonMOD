using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class destroyRandom : MonoBehaviour
{
	// Token: 0x06001A59 RID: 6745 RVA: 0x0010A8D2 File Offset: 0x00108AD2
	private void Start()
	{
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
