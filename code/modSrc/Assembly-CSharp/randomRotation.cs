using System;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class randomRotation : MonoBehaviour
{
	// Token: 0x06001AC0 RID: 6848 RVA: 0x0010D178 File Offset: 0x0010B378
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

	// Token: 0x040021DF RID: 8671
	public bool randX;

	// Token: 0x040021E0 RID: 8672
	public bool randY;

	// Token: 0x040021E1 RID: 8673
	public bool randZ;
}
