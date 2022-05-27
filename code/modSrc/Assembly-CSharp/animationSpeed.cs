using System;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class animationSpeed : MonoBehaviour
{
	// Token: 0x06001A17 RID: 6679 RVA: 0x00109720 File Offset: 0x00107920
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

	// Token: 0x06001A18 RID: 6680 RVA: 0x0010977A File Offset: 0x0010797A
	private void Update()
	{
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.SetAnimationSpeed();
		}
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x001097A6 File Offset: 0x001079A6
	private void SetAnimationSpeed()
	{
		this.myAnimation[this.animationString].speed = this.mS_.GetGameSpeed();
	}

	// Token: 0x0400211A RID: 8474
	public string animationString;

	// Token: 0x0400211B RID: 8475
	private Animation myAnimation;

	// Token: 0x0400211C RID: 8476
	private mainScript mS_;

	// Token: 0x0400211D RID: 8477
	private GameObject main_;

	// Token: 0x0400211E RID: 8478
	private float oldGamespeed;
}
