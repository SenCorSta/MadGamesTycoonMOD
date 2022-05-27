using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000175 RID: 373
public class Menu_LoadGame : MonoBehaviour
{
	// Token: 0x06000DD9 RID: 3545 RVA: 0x0009582D File Offset: 0x00093A2D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00095838 File Offset: 0x00093A38
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.save_)
		{
			this.save_ = this.main_.GetComponent<savegameScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x00095922 File Offset: 0x00093B22
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x00095930 File Offset: 0x00093B30
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.LoadFile(i);
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x00095964 File Offset: 0x00093B64
	private void LoadFile(int i)
	{
		string filePath = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		Transform child = this.uiObjects[0].transform.GetChild(i);
		Transform child2 = child.transform.GetChild(0);
		Transform child3 = child.transform.GetChild(1);
		Transform child4 = child.transform.GetChild(2);
		Transform child5 = child2.transform.GetChild(0);
		if (ES3.FileExists(filePath))
		{
			string text = ES3.LoadRawString(filePath);
			if (text.Length > 0 && text[0] == '{')
			{
				this.reader = ES3Reader.Create(filePath);
				if (this.reader != null)
				{
					this.reader.Dispose();
					if (child2)
					{
						child2.GetComponent<Button>().interactable = true;
					}
					if (child3)
					{
						child3.GetComponent<Button>().interactable = true;
					}
					if (child5)
					{
						child5.GetComponent<Text>().text = this.save_.LoadSaveGameName(i);
					}
					if (this.save_.IsSaveGameOutdatet(i))
					{
						if (child4)
						{
							child4.gameObject.SetActive(true);
						}
						if (child5)
						{
							child5.GetComponent<Text>().color = this.guiMain_.colors[5];
							return;
						}
					}
					else
					{
						if (child4)
						{
							child4.gameObject.SetActive(false);
						}
						if (child5)
						{
							child5.GetComponent<Text>().color = this.guiMain_.colors[6];
						}
					}
					return;
				}
			}
		}
		if (child2)
		{
			child2.GetComponent<Button>().interactable = false;
		}
		if (child3)
		{
			child3.GetComponent<Button>().interactable = false;
		}
		if (child5)
		{
			child5.GetComponent<Text>().text = this.tS_.GetText(783);
		}
		if (child5)
		{
			child5.GetComponent<Text>().color = this.guiMain_.colors[6];
		}
		if (child4)
		{
			child4.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00095B7A File Offset: 0x00093D7A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00095BAC File Offset: 0x00093DAC
	private IEnumerator LoadSaveGameAfterOneFrame(int i)
	{
		this.guiMain_.uiObjects[152].SetActive(true);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.save_.Load(i);
		this.guiMain_.uiObjects[151].SetActive(false);
		this.guiMain_.uiObjects[152].SetActive(false);
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00095BC2 File Offset: 0x00093DC2
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x00095BE0 File Offset: 0x00093DE0
	public void BUTTON_LoadGame(int i)
	{
		this.FindScripts();
		if (!this.mS_.multiplayer)
		{
			PlayerPrefs.SetInt("LoadSavegame", i);
			PlayerPrefs.SetInt("LoadMPSavegame", -1);
		}
		else
		{
			if (this.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Command(5);
			}
			PlayerPrefs.SetInt("LoadSavegame", -1);
			PlayerPrefs.SetInt("LoadMPSavegame", i);
		}
		this.guiMain_.RemoveVectrocity();
		this.guiMain_.uiObjects[152].SetActive(true);
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00095C78 File Offset: 0x00093E78
	public void BUTTON_DeleteSaveGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[153].SetActive(true);
		this.guiMain_.uiObjects[153].GetComponent<Menu_DeleteSaveGame>().Init(i, this.save_.LoadSaveGameName(i));
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00095CD1 File Offset: 0x00093ED1
	public void BUTTON_DeleteAllSaveGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[264].SetActive(true);
	}

	// Token: 0x04001274 RID: 4724
	public GameObject[] uiObjects;

	// Token: 0x04001275 RID: 4725
	private GameObject main_;

	// Token: 0x04001276 RID: 4726
	private mainScript mS_;

	// Token: 0x04001277 RID: 4727
	private textScript tS_;

	// Token: 0x04001278 RID: 4728
	private GUI_Main guiMain_;

	// Token: 0x04001279 RID: 4729
	private sfxScript sfx_;

	// Token: 0x0400127A RID: 4730
	private savegameScript save_;

	// Token: 0x0400127B RID: 4731
	private mpCalls mpCalls_;

	// Token: 0x0400127C RID: 4732
	private ES3Writer writer;

	// Token: 0x0400127D RID: 4733
	private ES3Reader reader;
}
