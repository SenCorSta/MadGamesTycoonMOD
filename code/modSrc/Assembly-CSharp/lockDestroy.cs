using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class lockDestroy : MonoBehaviour
{
	// Token: 0x06001A98 RID: 6808 RVA: 0x0010BEA8 File Offset: 0x0010A0A8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x0010BEB0 File Offset: 0x0010A0B0
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

	// Token: 0x06001A9A RID: 6810 RVA: 0x0010BF54 File Offset: 0x0010A154
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

	// Token: 0x0400219D RID: 8605
	public int unlockSlot = -1;

	// Token: 0x0400219E RID: 8606
	public bool sonstigeForschung;

	// Token: 0x0400219F RID: 8607
	public bool gameplayFeatures;

	// Token: 0x040021A0 RID: 8608
	private GameObject main_;

	// Token: 0x040021A1 RID: 8609
	private mainScript mS_;

	// Token: 0x040021A2 RID: 8610
	private unlockScript unlock_;

	// Token: 0x040021A3 RID: 8611
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x040021A4 RID: 8612
	private gameplayFeatures gF_;
}
