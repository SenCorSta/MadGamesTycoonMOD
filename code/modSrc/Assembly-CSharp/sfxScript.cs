using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class sfxScript : MonoBehaviour
{
	// Token: 0x06001EBD RID: 7869 RVA: 0x00140BB6 File Offset: 0x0013EDB6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x00140BC0 File Offset: 0x0013EDC0
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

	// Token: 0x06001EBF RID: 7871 RVA: 0x00140C97 File Offset: 0x0013EE97
	private void Update()
	{
		this.PlayMusic();
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x00140CA0 File Offset: 0x0013EEA0
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

	// Token: 0x06001EC1 RID: 7873 RVA: 0x00140CFC File Offset: 0x0013EEFC
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

	// Token: 0x06001EC2 RID: 7874 RVA: 0x00140D4C File Offset: 0x0013EF4C
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

	// Token: 0x06001EC3 RID: 7875 RVA: 0x00140DA8 File Offset: 0x0013EFA8
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

	// Token: 0x06001EC4 RID: 7876 RVA: 0x00140DD4 File Offset: 0x0013EFD4
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

	// Token: 0x06001EC5 RID: 7877 RVA: 0x00140E0D File Offset: 0x0013F00D
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

	// Token: 0x06001EC6 RID: 7878 RVA: 0x00140E31 File Offset: 0x0013F031
	public AudioSource GetAudioSource(int i)
	{
		return this.sfxAudioSource[i];
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x00140E3C File Offset: 0x0013F03C
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

	// Token: 0x06001EC8 RID: 7880 RVA: 0x00140EA4 File Offset: 0x0013F0A4
	public void SetRandomMusic()
	{
		this.FindScripts();
		this.musicSource.Stop();
		this.aktMusik = UnityEngine.Random.Range(0, this.musicClips.Length);
		this.PlayMusic();
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x00140ED1 File Offset: 0x0013F0D1
	public void SetVolume()
	{
		AudioListener.volume = this.sS_.masterVolume;
		this.musicSource.volume = this.sS_.musicVolume;
	}

	// Token: 0x040026A6 RID: 9894
	private mainScript mS_;

	// Token: 0x040026A7 RID: 9895
	public AudioClip[] musicClips;

	// Token: 0x040026A8 RID: 9896
	public GameObject[] sfxObjects;

	// Token: 0x040026A9 RID: 9897
	private AudioSource[] sfxAudioSource;

	// Token: 0x040026AA RID: 9898
	private AudioSource musicSource;

	// Token: 0x040026AB RID: 9899
	private settingsScript sS_;

	// Token: 0x040026AC RID: 9900
	private savegameScript savegame_;

	// Token: 0x040026AD RID: 9901
	public int aktMusik;
}
