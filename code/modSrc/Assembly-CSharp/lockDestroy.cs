using System;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class lockDestroy : MonoBehaviour
{
	// Token: 0x06001A4E RID: 6734 RVA: 0x00011B2A File Offset: 0x0000FD2A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x0010FD54 File Offset: 0x0010DF54
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x0010FDF8 File Offset: 0x0010DFF8
	private void OnEnable()
	{
		this.FindScripts();
		if (this.unlockSlot == -1)
		{
			return;
		}
		if (this.sonstigeForschung)
		{
			if (this.forschungSonstiges_.IsErforscht(this.unlockSlot))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (this.gameplayFeatures)
		{
			if (this.gF_.IsErforscht(this.unlockSlot))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (this.unlock_.Get(this.unlockSlot))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002183 RID: 8579
	public int unlockSlot = -1;

	// Token: 0x04002184 RID: 8580
	public bool sonstigeForschung;

	// Token: 0x04002185 RID: 8581
	public bool gameplayFeatures;

	// Token: 0x04002186 RID: 8582
	private GameObject main_;

	// Token: 0x04002187 RID: 8583
	private mainScript mS_;

	// Token: 0x04002188 RID: 8584
	private unlockScript unlock_;

	// Token: 0x04002189 RID: 8585
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x0400218A RID: 8586
	private gameplayFeatures gF_;
}
