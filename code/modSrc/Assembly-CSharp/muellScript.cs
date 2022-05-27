using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class muellScript : MonoBehaviour
{
	// Token: 0x06001AB5 RID: 6837 RVA: 0x0010CFD5 File Offset: 0x0010B1D5
	private void Start()
	{
		this.FindScripts();
		this.mS_.findMuell = true;
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x0010CFE9 File Offset: 0x0010B1E9
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

	// Token: 0x06001AB7 RID: 6839 RVA: 0x0010D026 File Offset: 0x0010B226
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findMuell = true;
		}
	}

	// Token: 0x040021D7 RID: 8663
	public int myGFXSlot = -1;

	// Token: 0x040021D8 RID: 8664
	public mainScript mS_;

	// Token: 0x040021D9 RID: 8665
	public GameObject main_;
}
