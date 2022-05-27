using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Arbeitsmarkt : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		if (!this.charAM_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public void SetData(string s, float val)
	{
		if (!this.charAM_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.charAM_.myName;
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(137 + this.charAM_.beruf);
		this.uiObjects[1].GetComponent<Text>().text = s;
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		this.uiObjects[3].GetComponent<Image>().fillAmount = val * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(val);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.charAM_.GetGehalt(), true);
		this.guiMain_.CreatePerkIconsArbeitsmarkt(this.charAM_, this.uiObjects[7].transform);
		if (this.mS_.multiplayer && this.uiObjects[4].activeSelf)
		{
			this.uiObjects[4].SetActive(false);
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[31]);
		this.guiMain_.uiObjects[31].GetComponent<Menu_PersonalViewArbeitsmarkt>().Init(this.charAM_);
	}

	
	public void BUTTON_Remove()
	{
		this.sfx_.PlaySound(3, true);
		if (this.charAM_)
		{
			UnityEngine.Object.Destroy(this.charAM_.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public int characterID = -1;

	
	public charArbeitsmarkt charAM_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
