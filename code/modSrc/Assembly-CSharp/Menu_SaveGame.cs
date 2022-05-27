using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017F RID: 383
public class Menu_SaveGame : MonoBehaviour
{
	// Token: 0x06000E62 RID: 3682 RVA: 0x0009B6AA File Offset: 0x000998AA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0009B6B4 File Offset: 0x000998B4
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

	// Token: 0x06000E64 RID: 3684 RVA: 0x0009B77C File Offset: 0x0009997C
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0009B78C File Offset: 0x0009998C
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.LoadFile(i);
		}
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0009B7C0 File Offset: 0x000999C0
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

	// Token: 0x06000E67 RID: 3687 RVA: 0x0009B9F2 File Offset: 0x00099BF2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0009BA24 File Offset: 0x00099C24
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0009BA40 File Offset: 0x00099C40
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

	// Token: 0x06000E6A RID: 3690 RVA: 0x0009BB28 File Offset: 0x00099D28
	public void BUTTON_DeleteSaveGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[153].SetActive(true);
		this.guiMain_.uiObjects[153].GetComponent<Menu_DeleteSaveGame>().Init(i, this.save_.LoadSaveGameName(i));
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x0009BB81 File Offset: 0x00099D81
	public void BUTTON_DeleteAllSaveGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[264].SetActive(true);
	}

	// Token: 0x040012DA RID: 4826
	public GameObject[] uiObjects;

	// Token: 0x040012DB RID: 4827
	private GameObject main_;

	// Token: 0x040012DC RID: 4828
	private mainScript mS_;

	// Token: 0x040012DD RID: 4829
	private textScript tS_;

	// Token: 0x040012DE RID: 4830
	private GUI_Main guiMain_;

	// Token: 0x040012DF RID: 4831
	private sfxScript sfx_;

	// Token: 0x040012E0 RID: 4832
	private savegameScript save_;

	// Token: 0x040012E1 RID: 4833
	private ES3Writer writer;

	// Token: 0x040012E2 RID: 4834
	private ES3Reader reader;
}
