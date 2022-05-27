using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class muellScript : MonoBehaviour
{
	// Token: 0x06001A6B RID: 6763 RVA: 0x00011BF2 File Offset: 0x0000FDF2
	private void Start()
	{
		this.FindScripts();
		this.mS_.findMuell = true;
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x00011C06 File Offset: 0x0000FE06
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

	// Token: 0x06001A6D RID: 6765 RVA: 0x00011C43 File Offset: 0x0000FE43
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findMuell = true;
		}
	}

	// Token: 0x040021BD RID: 8637
	public int myGFXSlot = -1;

	// Token: 0x040021BE RID: 8638
	public mainScript mS_;

	// Token: 0x040021BF RID: 8639
	public GameObject main_;
}
