using System;
using System.Collections;
using UnityEngine;


public class sfxScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		this.PlayMusic();
	}

	
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

	
	public AudioSource GetAudioSource(int i)
	{
		return this.sfxAudioSource[i];
	}

	
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

	
	public void SetRandomMusic()
	{
		this.FindScripts();
		this.musicSource.Stop();
		this.aktMusik = UnityEngine.Random.Range(0, this.musicClips.Length);
		this.PlayMusic();
	}

	
	public void SetVolume()
	{
		AudioListener.volume = this.sS_.masterVolume;
		this.musicSource.volume = this.sS_.musicVolume;
	}

	
	private mainScript mS_;

	
	public AudioClip[] musicClips;

	
	public GameObject[] sfxObjects;

	
	private AudioSource[] sfxAudioSource;

	
	private AudioSource musicSource;

	
	private settingsScript sS_;

	
	private savegameScript savegame_;

	
	public int aktMusik;
}
