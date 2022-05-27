using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class maschineAnimSound : MonoBehaviour
{
	// Token: 0x06001A5B RID: 6747 RVA: 0x00011B69 File Offset: 0x0000FD69
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

	// Token: 0x06001A5C RID: 6748 RVA: 0x00110790 File Offset: 0x0010E990
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

	// Token: 0x040021A2 RID: 8610
	public AudioSource mySound;

	// Token: 0x040021A3 RID: 8611
	public Animation myAnimation;

	// Token: 0x040021A4 RID: 8612
	private mainScript mS_;

	// Token: 0x040021A5 RID: 8613
	private GameObject main_;

	// Token: 0x040021A6 RID: 8614
	private float oldGamespeed;
}
