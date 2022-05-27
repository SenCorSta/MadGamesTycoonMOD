using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class animationSpeed : MonoBehaviour
{
	// Token: 0x060019CD RID: 6605 RVA: 0x0010DA1C File Offset: 0x0010BC1C
	private void Start()
	{
		this.myAnimation = base.GetComponent<Animation>();
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		this.SetAnimationSpeed();
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x00011673 File Offset: 0x0000F873
	private void Update()
	{
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.SetAnimationSpeed();
		}
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x0001169F File Offset: 0x0000F89F
	private void SetAnimationSpeed()
	{
		this.myAnimation[this.animationString].speed = this.mS_.GetGameSpeed();
	}

	// Token: 0x04002100 RID: 8448
	public string animationString;

	// Token: 0x04002101 RID: 8449
	private Animation myAnimation;

	// Token: 0x04002102 RID: 8450
	private mainScript mS_;

	// Token: 0x04002103 RID: 8451
	private GameObject main_;

	// Token: 0x04002104 RID: 8452
	private float oldGamespeed;
}
