using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class soundScript : MonoBehaviour
{
	// Token: 0x06001ADA RID: 6874 RVA: 0x0010DFC8 File Offset: 0x0010C1C8
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

	// Token: 0x06001ADB RID: 6875 RVA: 0x0010E064 File Offset: 0x0010C264
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

	// Token: 0x040021FF RID: 8703
	private AudioSource soundSource;

	// Token: 0x04002200 RID: 8704
	private settingsScript sS_;

	// Token: 0x04002201 RID: 8705
	private mainScript mS_;

	// Token: 0x04002202 RID: 8706
	private GameObject main_;

	// Token: 0x04002203 RID: 8707
	private MeshRenderer myRenderer;

	// Token: 0x04002204 RID: 8708
	private float orgVolume = 1f;

	// Token: 0x04002205 RID: 8709
	private float oldGamespeed = 1f;

	// Token: 0x04002206 RID: 8710
	public bool pitchToGamespeed;

	// Token: 0x04002207 RID: 8711
	public bool muteOnPausedGame;
}
