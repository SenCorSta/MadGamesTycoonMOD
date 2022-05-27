using System;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class soundScript : MonoBehaviour
{
	// Token: 0x06001A90 RID: 6800 RVA: 0x00111BEC File Offset: 0x0010FDEC
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
		if (!this.sS_)
		{
			this.sS_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.soundSource)
		{
			this.soundSource = base.GetComponent<AudioSource>();
			this.orgVolume = this.soundSource.volume;
		}
		this.myRenderer = base.GetComponent<MeshRenderer>();
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x00111C88 File Offset: 0x0010FE88
	private void Update()
	{
		if (this.muteOnPausedGame && this.mS_.GetGameSpeed() <= 0f)
		{
			this.soundSource.volume = 0f;
			return;
		}
		if (this.pitchToGamespeed && this.oldGamespeed != this.mS_.GetGameSpeed())
		{
			this.oldGamespeed = this.mS_.GetGameSpeed();
			this.soundSource.pitch = this.mS_.GetGameSpeed();
		}
		this.soundSource.volume = this.orgVolume * this.sS_.masterVolume;
	}

	// Token: 0x040021E5 RID: 8677
	private AudioSource soundSource;

	// Token: 0x040021E6 RID: 8678
	private settingsScript sS_;

	// Token: 0x040021E7 RID: 8679
	private mainScript mS_;

	// Token: 0x040021E8 RID: 8680
	private GameObject main_;

	// Token: 0x040021E9 RID: 8681
	private MeshRenderer myRenderer;

	// Token: 0x040021EA RID: 8682
	private float orgVolume = 1f;

	// Token: 0x040021EB RID: 8683
	private float oldGamespeed = 1f;

	// Token: 0x040021EC RID: 8684
	public bool pitchToGamespeed;

	// Token: 0x040021ED RID: 8685
	public bool muteOnPausedGame;
}
