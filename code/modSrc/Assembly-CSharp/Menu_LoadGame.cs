using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu_LoadGame : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.LoadFile(i);
		}
	}

	
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

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
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

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
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

	
	public void BUTTON_DeleteSaveGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[153].SetActive(true);
		this.guiMain_.uiObjects[153].GetComponent<Menu_DeleteSaveGame>().Init(i, this.save_.LoadSaveGameName(i));
	}

	
	public void BUTTON_DeleteAllSaveGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[264].SetActive(true);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private savegameScript save_;

	
	private mpCalls mpCalls_;

	
	private ES3Writer writer;

	
	private ES3Reader reader;
}
