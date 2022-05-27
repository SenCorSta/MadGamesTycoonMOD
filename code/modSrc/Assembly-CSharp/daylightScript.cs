using System;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class daylightScript : MonoBehaviour
{
	// Token: 0x06001A09 RID: 6665 RVA: 0x00011875 File Offset: 0x0000FA75
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x0001187D File Offset: 0x0000FA7D
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

	// Token: 0x06001A0B RID: 6667 RVA: 0x0010E908 File Offset: 0x0010CB08
	private void Update()
	{
		if (this.mS_)
		{
			float y = this.mS_.dayTimer * 90f + (float)(this.mS_.week - 1) * 90f;
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
		}
	}

	// Token: 0x04002140 RID: 8512
	private GameObject main_;

	// Token: 0x04002141 RID: 8513
	private mainScript mS_;
}
