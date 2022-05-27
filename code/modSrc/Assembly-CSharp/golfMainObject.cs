using System;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class golfMainObject : MonoBehaviour
{
	// Token: 0x06001A6F RID: 6767 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x0010AF08 File Offset: 0x00109108
	public void RandomRotation()
	{
		if (this.mainObject)
		{
			this.mainObject.transform.localEulerAngles = new Vector3(0f, UnityEngine.Random.Range(-13f, 6f), 0f);
			this.audio_.Play();
		}
	}

	// Token: 0x04002173 RID: 8563
	public GameObject mainObject;

	// Token: 0x04002174 RID: 8564
	public AudioSource audio_;
}
