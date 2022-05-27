using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class randomRotation : MonoBehaviour
{
	// Token: 0x06001A76 RID: 6774 RVA: 0x00110ECC File Offset: 0x0010F0CC
	private void Start()
	{
		if (this.randX)
		{
			base.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(0f, 359f), base.transform.eulerAngles.y, base.transform.eulerAngles.z);
		}
		if (this.randY)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 359f), base.transform.eulerAngles.z);
		}
		if (this.randZ)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, UnityEngine.Random.Range(0f, 359f));
		}
	}

	// Token: 0x040021C5 RID: 8645
	public bool randX;

	// Token: 0x040021C6 RID: 8646
	public bool randY;

	// Token: 0x040021C7 RID: 8647
	public bool randZ;
}
