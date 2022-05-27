using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class daylightScript : MonoBehaviour
{
	// Token: 0x06001A53 RID: 6739 RVA: 0x0010A809 File Offset: 0x00108A09
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x0010A811 File Offset: 0x00108A11
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x0010A850 File Offset: 0x00108A50
	private void Update()
	{
		if (this.mS_)
		{
			float y = this.mS_.dayTimer * 90f + (float)(this.mS_.week - 1) * 90f;
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
		}
	}

	// Token: 0x0400215A RID: 8538
	private GameObject main_;

	// Token: 0x0400215B RID: 8539
	private mainScript mS_;
}
