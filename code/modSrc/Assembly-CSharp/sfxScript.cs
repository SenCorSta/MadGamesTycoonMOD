using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class sfxScript : MonoBehaviour
{
	// Token: 0x06001E6A RID: 7786 RVA: 0x000144AB File Offset: 0x000126AB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0014201C File Offset: 0x0014021C
	private void FindScripts()
	{
		if (this.mS_)
		{
			return;
		}
		if (!this.mS_)
		{
			this.mS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<mainScript>();
		}
		if (!this.sS_)
		{
			this.sS_ = GameObject.FindGameObjectWithTag("Main").GetComponent<settingsScript>();
		}
		if (!this.savegame_)
		{
			this.savegame_ = GameObject.FindGameObjectWithTag("Main").GetComponent<savegameScript>();
		}
		this.sfxAudioSource = new AudioSource[this.sfxObjects.Length];
		for (int i = 0; i < this.sfxObjects.Length; i++)
		{
			if (this.sfxObjects[i])
			{
				this.sfxAudioSource[i] = this.sfxObjects[i].GetComponent<AudioSource>();
			}
		}
		this.musicSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x000144B3 File Offset: 0x000126B3
	private void Update()
	{
		this.PlayMusic();
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x001420F4 File Offset: 0x001402F4
	public void PlaySound(int i, bool force)
	{
		if (!this.savegame_)
		{
			return;
		}
		if (!this.savegame_.loadingSavegame && this.sfxAudioSource[i] && (force || (!force && !this.sfxAudioSource[i].isPlaying)))
		{
			this.sfxAudioSource[i].Play();
		}
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x00142150 File Offset: 0x00140350
	public void PlaySound(int i)
	{
		if (!this.savegame_)
		{
			return;
		}
		if (!this.savegame_.loadingSavegame)
		{
			if (this.sfxAudioSource == null)
			{
				return;
			}
			if (this.sfxAudioSource[i])
			{
				this.sfxAudioSource[i].Play();
			}
		}
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x001421A0 File Offset: 0x001403A0
	public void Play3DSound(int i, float time, bool force, Vector3 pos)
	{
		if (!this.savegame_)
		{
			return;
		}
		if (!this.savegame_.loadingSavegame)
		{
			float num = time;
			if (this.mS_.GetGameSpeed() > 0f)
			{
				num /= this.mS_.GetGameSpeed();
			}
			base.StartCoroutine(this.iPlay3DSound(i, num, force, pos));
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x000144BB File Offset: 0x000126BB
	private IEnumerator iPlay3DSound(int i, float time, bool force, Vector3 pos)
	{
		if (this.savegame_ && !this.savegame_.loadingSavegame)
		{
			yield return new WaitForSeconds(time);
			if (force || (!force && !this.sfxAudioSource[i].isPlaying))
			{
				this.sfxObjects[i].transform.position = pos;
				this.sfxAudioSource[i].Play();
			}
		}
		yield break;
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x000144E7 File Offset: 0x000126E7
	public void PlaySoundDelay(int i, float time, bool force)
	{
		if (!this.savegame_)
		{
			return;
		}
		if (!this.savegame_.loadingSavegame)
		{
			base.StartCoroutine(this.iPlaySoundDelay(i, time / this.mS_.GetGameSpeed(), force));
		}
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x00014520 File Offset: 0x00012720
	private IEnumerator iPlaySoundDelay(int i, float time, bool force)
	{
		if (this.savegame_ && !this.savegame_.loadingSavegame)
		{
			yield return new WaitForSeconds(time);
			if (force || (!force && !this.sfxAudioSource[i].isPlaying))
			{
				this.sfxAudioSource[i].Play();
			}
		}
		yield break;
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x00014544 File Offset: 0x00012744
	public AudioSource GetAudioSource(int i)
	{
		return this.sfxAudioSource[i];
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x001421FC File Offset: 0x001403FC
	private void PlayMusic()
	{
		if (!this.musicSource.isPlaying)
		{
			this.SetVolume();
			this.aktMusik++;
			if (this.aktMusik >= this.musicClips.Length)
			{
				this.aktMusik = 0;
			}
			this.musicSource.clip = this.musicClips[this.aktMusik];
			this.musicSource.Play();
		}
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0001454E File Offset: 0x0001274E
	public void SetRandomMusic()
	{
		this.FindScripts();
		this.musicSource.Stop();
		this.aktMusik = UnityEngine.Random.Range(0, this.musicClips.Length);
		this.PlayMusic();
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x0001457B File Offset: 0x0001277B
	public void SetVolume()
	{
		AudioListener.volume = this.sS_.masterVolume;
		this.musicSource.volume = this.sS_.musicVolume;
	}

	// Token: 0x04002690 RID: 9872
	private mainScript mS_;

	// Token: 0x04002691 RID: 9873
	public AudioClip[] musicClips;

	// Token: 0x04002692 RID: 9874
	public GameObject[] sfxObjects;

	// Token: 0x04002693 RID: 9875
	private AudioSource[] sfxAudioSource;

	// Token: 0x04002694 RID: 9876
	private AudioSource musicSource;

	// Token: 0x04002695 RID: 9877
	private settingsScript sS_;

	// Token: 0x04002696 RID: 9878
	private savegameScript savegame_;

	// Token: 0x04002697 RID: 9879
	public int aktMusik;
}
