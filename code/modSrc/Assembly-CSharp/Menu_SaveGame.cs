using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017E RID: 382
public class Menu_SaveGame : MonoBehaviour
{
	// Token: 0x06000E4A RID: 3658 RVA: 0x00009FD5 File Offset: 0x000081D5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000A9258 File Offset: 0x000A7458
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
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x00009FDD File Offset: 0x000081DD
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x000A9320 File Offset: 0x000A7520
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.LoadFile(i);
		}
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000A9354 File Offset: 0x000A7554
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
					if (i == 0)
					{
						child2.GetComponent<Button>().interactable = false;
					}
					return;
				}
			}
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

	// Token: 0x06000E4F RID: 3663 RVA: 0x00009FEB File Offset: 0x000081EB
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0000A01D File Offset: 0x0000821D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000A9588 File Offset: 0x000A7788
	public void BUTTON_SaveGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		string filePath = this.mS_.GetSavegameTitle() + i.ToString() + ".txt";
		this.reader = ES3Reader.Create(filePath);
		if (this.reader == null)
		{
			if (!this.mS_.multiplayer)
			{
				this.save_.Save(i);
			}
			else
			{
				this.save_.SaveMultiplayer(i);
			}
			this.guiMain_.uiObjects[155].SetActive(false);
			this.BUTTON_Abbrechen();
			this.guiMain_.ShowGameHasSaved();
			return;
		}
		this.reader.Dispose();
		this.guiMain_.uiObjects[157].SetActive(true);
		this.guiMain_.uiObjects[157].GetComponent<Menu_OverwriteSavegame>().Init(i, this.save_.LoadSaveGameName(i));
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x000A9670 File Offset: 0x000A7870
	public void BUTTON_DeleteSaveGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[153].SetActive(true);
		this.guiMain_.uiObjects[153].GetComponent<Menu_DeleteSaveGame>().Init(i, this.save_.LoadSaveGameName(i));
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0000A038 File Offset: 0x00008238
	public void BUTTON_DeleteAllSaveGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[264].SetActive(true);
	}

	// Token: 0x040012D1 RID: 4817
	public GameObject[] uiObjects;

	// Token: 0x040012D2 RID: 4818
	private GameObject main_;

	// Token: 0x040012D3 RID: 4819
	private mainScript mS_;

	// Token: 0x040012D4 RID: 4820
	private textScript tS_;

	// Token: 0x040012D5 RID: 4821
	private GUI_Main guiMain_;

	// Token: 0x040012D6 RID: 4822
	private sfxScript sfx_;

	// Token: 0x040012D7 RID: 4823
	private savegameScript save_;

	// Token: 0x040012D8 RID: 4824
	private ES3Writer writer;

	// Token: 0x040012D9 RID: 4825
	private ES3Reader reader;
}
