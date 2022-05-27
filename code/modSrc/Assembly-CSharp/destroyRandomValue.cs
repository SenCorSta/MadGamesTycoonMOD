using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class destroyRandomValue : MonoBehaviour
{
	// Token: 0x06001A11 RID: 6673 RVA: 0x00011913 File Offset: 0x0000FB13
	private void Start()
	{
		if (UnityEngine.Random.Range(0, 100) < this.rand)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002144 RID: 8516
	public int rand = 95;
}
