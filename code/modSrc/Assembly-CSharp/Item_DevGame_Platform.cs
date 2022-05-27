using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_Platform : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		string text = this.pS_.GetDateString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(220),
			": <b>",
			this.pS_.GetGames().ToString(),
			"</b>"
		});
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(219),
			": <b>",
			this.pS_.GetMarktanteilString(),
			"</b>"
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.guiMain_.DrawStars(this.uiObjects[7], this.pS_.erfahrung);
		this.uiObjects[9].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		if (this.pS_.internet)
		{
			this.uiObjects[4].SetActive(true);
		}
		else
		{
			this.uiObjects[4].SetActive(false);
		}
		this.tooltip_.c = this.pS_.GetTooltip();
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(375) + ": " + this.mS_.GetMoney((long)this.pS_.GetDevCosts(), true);
		this.uiObjects[11].GetComponent<Text>().text = string.Concat(new object[]
		{
			this.tS_.GetText(1926),
			": -",
			Mathf.RoundToInt(this.pS_.GetExklusivBonus()),
			"%"
		});
		this.uiObjects[10].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[10].GetComponent<tooltip>().c = this.pS_.GetTypString();
		if (this.pS_.vomMarktGenommen)
		{
			this.uiObjects[3].SetActive(true);
		}
		else
		{
			this.uiObjects[8].SetActive(false);
		}
		if (this.devGame_ && (this.devGame_.g_GamePlatform[0] == this.pS_.myID || this.devGame_.g_GamePlatform[1] == this.pS_.myID || this.devGame_.g_GamePlatform[2] == this.pS_.myID || this.devGame_.g_GamePlatform[3] == this.pS_.myID))
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip = this.tooltip_;
			tooltip.c = tooltip.c + "\n\n<color=red><b>" + this.tS_.GetText(379) + "</b></color>";
			base.gameObject.GetComponent<Button>().interactable = false;
			return;
		}
		if (this.changePlatform_ && (this.changePlatform_.g_GamePlatform[0] == this.pS_.myID || this.changePlatform_.g_GamePlatform[1] == this.pS_.myID || this.changePlatform_.g_GamePlatform[2] == this.pS_.myID || this.changePlatform_.g_GamePlatform[3] == this.pS_.myID))
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip2 = this.tooltip_;
			tooltip2.c = tooltip2.c + "\n\n<color=red><b>" + this.tS_.GetText(379) + "</b></color>";
			base.gameObject.GetComponent<Button>().interactable = false;
			return;
		}
		if (this.pS_.playerConsole && !this.pS_.isUnlocked)
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip3 = this.tooltip_;
			tooltip3.c = tooltip3.c + "\n\n<color=red><b>" + this.tS_.GetText(1633) + "</b></color>";
		}
		if (this.devGame_)
		{
			this.devGame_.GetEngineTechLevel();
		}
		if (this.changePlatform_)
		{
			this.changePlatform_.GetEngineTechLevel();
		}
	}

	
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, false);
		if (this.devGame_)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetPlatform(this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().platformNR, this.myID);
		}
		if (this.changePlatform_)
		{
			this.guiMain_.uiObjects[102].GetComponent<Menu_Dev_ChangePlatform>().SetPlatform(this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().platformNR, this.myID, false);
		}
		this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().BUTTON_Close();
	}

	
	private bool IsExclusivGame()
	{
		return this.guiMain_.uiObjects[56].activeSelf && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 1;
	}

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public platformScript pS_;

	
	public Menu_DevGame devGame_;

	
	public Menu_Dev_ChangePlatform changePlatform_;

	
	private float updateTimer;
}
