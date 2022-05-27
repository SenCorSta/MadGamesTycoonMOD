using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class maschineAnimSound : MonoBehaviour
{
	// Token: 0x06001AA5 RID: 6821 RVA: 0x0010C93B File Offset: 0x0010AB3B
	private void Start()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x0010C978 File Offset: 0x0010AB78
	private void Update()
	{
		if (!this.myAnimation.isPlaying)
		{
			this.mySound.Stop();
			this.mySound.Play();
			return;
		}
		if (this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.mySound.pitch = this.mS_.GetGameSpeed();
		}
	}

	// Token: 0x040021BC RID: 8636
	public AudioSource mySound;

	// Token: 0x040021BD RID: 8637
	public Animation myAnimation;

	// Token: 0x040021BE RID: 8638
	private mainScript mS_;

	// Token: 0x040021BF RID: 8639
	private GameObject main_;

	// Token: 0x040021C0 RID: 8640
	private float oldGamespeed;
}
