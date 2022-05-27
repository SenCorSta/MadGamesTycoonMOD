using System;
using System.Collections;
using UnityEngine;


public class loadSavegame : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		if (this.ShouldSavegameLoad())
		{
			return;
		}
		if (this.ShouldMultiplayerSavegameLoad())
		{
			return;
		}
		this.guiMain_.uiObjects[151].SetActive(true);
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = base.gameObject;
		}
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.save_)
		{
			this.save_ = base.GetComponent<savegameScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mpMain_)
		{
			this.mpMain_ = this.guiMain_.uiObjects[201].GetComponent<mpMain>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
		if (!this.sfX_)
		{
			this.sfX_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	
	private bool ShouldSavegameLoad()
	{
		int @int = PlayerPrefs.GetInt("LoadSavegame", -1);
		if (@int >= 0)
		{
			base.StartCoroutine(this.LoadSaveGameAfterOneFrame(@int));
			PlayerPrefs.SetInt("LoadSavegame", -1);
			return true;
		}
		return false;
	}

	
	private bool ShouldMultiplayerSavegameLoad()
	{
		int @int = PlayerPrefs.GetInt("LoadMPSavegame", -1);
		if (@int >= 0)
		{
			this.mS_.multiplayer = true;
			this.mS_.SetGameSpeed(0f);
			base.StartCoroutine(this.LoadSaveGameAfterOneFrame(@int));
			PlayerPrefs.SetInt("LoadMPSavegame", -1);
			return true;
		}
		return false;
	}

	
	private IEnumerator LoadSaveGameAfterOneFrame(int i)
	{
		if (this.mS_.multiplayer)
		{
			this.save_.loadingSavegame = true;
		}
		this.sfX_.SetRandomMusic();
		this.guiMain_.uiObjects[152].SetActive(true);
		this.guiMain_.uiObjects[151].SetActive(false);
		this.guiMain_.uiObjects[155].SetActive(false);
		this.guiMain_.ShowInGameUI(false);
		this.mS_.LoadOffice(this.save_.GetOfficeFromSavegame(i), true);
		yield return new WaitUntil(() => this.mS_.officeLoaded);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.save_.Load(i);
		this.guiMain_.uiObjects[152].SetActive(false);
		this.guiMain_.ShowInGameUI(true);
		this.mS_.DestroyMainMenuObjects();
		if (this.mS_.multiplayer)
		{
			if (this.mpCalls_.isServer)
			{
				this.mpCalls_.SetPlayersUnready();
			}
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[202]);
			if (this.mpCalls_.isServer)
			{
				if (this.mS_.multiplayer)
				{
					this.save_.loadingSavegame = true;
				}
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				this.mpCalls_.SERVER_Send_Load(this.mS_.multiplayerSaveID);
				if (this.mS_.multiplayer)
				{
					this.save_.loadingSavegame = false;
				}
			}
			else
			{
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
			}
		}
		yield break;
	}

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private mpMain mpMain_;

	
	private savegameScript save_;

	
	private GUI_Main guiMain_;

	
	private mpCalls mpCalls_;

	
	private sfxScript sfX_;

	
	private ES3Writer writer;

	
	private ES3Reader reader;
}
