using System;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class destroyRandomValue : MonoBehaviour
{
	// Token: 0x06001A5B RID: 6747 RVA: 0x0010A91B File Offset: 0x00108B1B
	private void Start()
	{
		if (UnityEngine.Random.Range(0, 100) < this.rand)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400215E RID: 8542
	public int rand = 95;
}
