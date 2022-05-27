using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Firmenstandort : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.main_)
		{
			return;
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
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

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			Transform child = this.uiObjects[0].transform.GetChild(i);
			child.gameObject.SetActive(true);
			Transform child2 = child.transform.GetChild(3);
			Component child3 = child.transform.GetChild(2);
			child2.GetComponent<Text>().text = this.tS_.GetCountry(i);
			child3.GetComponent<Image>().sprite = this.guiMain_.flagSprites[i];
			child.name = this.tS_.GetCountry(i);
		}
		this.mS_.SortChildrenByName(this.uiObjects[0]);
		this.EnableDisable();
	}

	
	private void EnableDisable()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			string text = this.uiObjects[0].transform.GetChild(i).name;
			this.searchStringA = this.searchStringA.ToLower();
			text = text.ToLower();
			if (this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
			{
				this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(true);
			}
			else
			{
				this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Country(int i)
	{
		Debug.Log("C: " + i);
		this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().SetCountry(i);
		this.BUTTON_Abbrechen();
	}

	
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.EnableDisable();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private string searchStringA = "";
}
