using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000174 RID: 372
public class Menu_LoadGame : MonoBehaviour
{
	// Token: 0x06000DC1 RID: 3521 RVA: 0x000099D5 File Offset: 0x00007BD5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x000A3AB8 File Offset: 0x000A1CB8
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

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000099DD File Offset: 0x00007BDD
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x000A3BA4 File Offset: 0x000A1DA4
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.LoadFile(i);
		}
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x000A3BD8 File Offset: 0x000A1DD8
	private void LoadFile(int i)
	{
		string text = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		Debug.Log("Savegame: " + text);
		Transform child = this.uiObjects[0].transform.GetChild(i);
		Transform child2 = child.transform.GetChild(0);
		Transform child3 = child.transform.GetChild(1);
		Transform child4 = child.transform.GetChild(2);
		Transform child5 = child2.transform.GetChild(0);
		if (ES3.FileExists(text))
		{
			string text2 = ES3.LoadRawString(text);
			Debug.Log("Savegame 2: " + text);
			if (text2.Length > 0 && text2[0] == '{')
			{
				this.reader = ES3Reader.Create(text);
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

	// Token: 0x06000DC6 RID: 3526 RVA: 0x000099EB File Offset: 0x00007BEB
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00009A1D File Offset: 0x00007C1D
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

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00009A33 File Offset: 0x00007C33
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x000A3E10 File Offset: 0x000A2010
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

	// Token: 0x06000DCA RID: 3530 RVA: 0x000A3EA8 File Offset: 0x000A20A8
	public void BUTTON_DeleteSaveGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[153].SetActive(true);
		this.guiMain_.uiObjects[153].GetComponent<Menu_DeleteSaveGame>().Init(i, this.save_.LoadSaveGameName(i));
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00009A4E File Offset: 0x00007C4E
	public void BUTTON_DeleteAllSaveGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[264].SetActive(true);
	}

	// Token: 0x0400126C RID: 4716
	public GameObject[] uiObjects;

	// Token: 0x0400126D RID: 4717
	private GameObject main_;

	// Token: 0x0400126E RID: 4718
	private mainScript mS_;

	// Token: 0x0400126F RID: 4719
	private textScript tS_;

	// Token: 0x04001270 RID: 4720
	private GUI_Main guiMain_;

	// Token: 0x04001271 RID: 4721
	private sfxScript sfx_;

	// Token: 0x04001272 RID: 4722
	private savegameScript save_;

	// Token: 0x04001273 RID: 4723
	private mpCalls mpCalls_;

	// Token: 0x04001274 RID: 4724
	private ES3Writer writer;

	// Token: 0x04001275 RID: 4725
	private ES3Reader reader;
}
