using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class golfMainObject : MonoBehaviour
{
	// Token: 0x06001A25 RID: 6693 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x0010EE84 File Offset: 0x0010D084
	public void RandomRotation()
	{
		if (this.mainObject)
		{
			this.mainObject.transform.localEulerAngles = new Vector3(0f, UnityEngine.Random.Range(-13f, 6f), 0f);
			this.audio_.Play();
		}
	}

	// Token: 0x04002159 RID: 8537
	public GameObject mainObject;

	// Token: 0x0400215A RID: 8538
	public AudioSource audio_;
}
