using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class loadSavegame : MonoBehaviour
{
	// Token: 0x06001A3F RID: 6719 RVA: 0x00011ABF File Offset: 0x0000FCBF
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

	// Token: 0x06001A40 RID: 6720 RVA: 0x0010F628 File Offset: 0x0010D828
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

	// Token: 0x06001A41 RID: 6721 RVA: 0x0010F710 File Offset: 0x0010D910
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

	// Token: 0x06001A42 RID: 6722 RVA: 0x0010F74C File Offset: 0x0010D94C
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

	// Token: 0x06001A43 RID: 6723 RVA: 0x00011AF0 File Offset: 0x0000FCF0
	private IEnumerator LoadSaveGameAfterOneFrame(int i)
	{
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
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				this.mpCalls_.SERVER_Send_Load(this.mS_.multiplayerSaveID);
				if (this.save_.savegamePlayerID != this.mpCalls_.myID)
				{
					this.mpCalls_.myID = this.save_.savegamePlayerID;
				}
			}
			else
			{
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				if (this.save_.savegamePlayerID != this.mpCalls_.myID)
				{
					this.mpCalls_.myID = this.save_.savegamePlayerID;
				}
			}
		}
		yield break;
	}

	// Token: 0x04002174 RID: 8564
	private GameObject main_;

	// Token: 0x04002175 RID: 8565
	private mainScript mS_;

	// Token: 0x04002176 RID: 8566
	private mpMain mpMain_;

	// Token: 0x04002177 RID: 8567
	private savegameScript save_;

	// Token: 0x04002178 RID: 8568
	private GUI_Main guiMain_;

	// Token: 0x04002179 RID: 8569
	private mpCalls mpCalls_;

	// Token: 0x0400217A RID: 8570
	private sfxScript sfX_;

	// Token: 0x0400217B RID: 8571
	private ES3Writer writer;

	// Token: 0x0400217C RID: 8572
	private ES3Reader reader;
}
