using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class loadSavegame : MonoBehaviour
{
	// Token: 0x06001A89 RID: 6793 RVA: 0x0010B743 File Offset: 0x00109943
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

	// Token: 0x06001A8A RID: 6794 RVA: 0x0010B774 File Offset: 0x00109974
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

	// Token: 0x06001A8B RID: 6795 RVA: 0x0010B85C File Offset: 0x00109A5C
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

	// Token: 0x06001A8C RID: 6796 RVA: 0x0010B898 File Offset: 0x00109A98
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

	// Token: 0x06001A8D RID: 6797 RVA: 0x0010B8ED File Offset: 0x00109AED
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

	// Token: 0x0400218E RID: 8590
	private GameObject main_;

	// Token: 0x0400218F RID: 8591
	private mainScript mS_;

	// Token: 0x04002190 RID: 8592
	private mpMain mpMain_;

	// Token: 0x04002191 RID: 8593
	private savegameScript save_;

	// Token: 0x04002192 RID: 8594
	private GUI_Main guiMain_;

	// Token: 0x04002193 RID: 8595
	private mpCalls mpCalls_;

	// Token: 0x04002194 RID: 8596
	private sfxScript sfX_;

	// Token: 0x04002195 RID: 8597
	private ES3Writer writer;

	// Token: 0x04002196 RID: 8598
	private ES3Reader reader;
}
