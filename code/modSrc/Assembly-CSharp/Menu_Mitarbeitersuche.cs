using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Mitarbeitersuche : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[0].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(137));
		list.Add(this.tS_.GetText(138));
		list.Add(this.tS_.GetText(139));
		list.Add(this.tS_.GetText(140));
		list.Add(this.tS_.GetText(141));
		list.Add(this.tS_.GetText(142));
		list.Add(this.tS_.GetText(143));
		list.Add(this.tS_.GetText(144));
		this.uiObjects[0].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[0].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[0].GetComponent<Dropdown>().value = @int;
		@int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		list = new List<string>();
		list.Add("<b>[30-35]</b> " + this.tS_.GetText(1710));
		list.Add("<b>[50-55]</b> " + this.tS_.GetText(1711));
		list.Add("<b>[70-75]</b> " + this.tS_.GetText(1712));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	
	public void Init(roomScript room_)
	{
		this.rS_ = room_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	
	private void SetData()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		string text = this.tS_.GetText(1716);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.price[value], true));
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.GetChance(value)).ToString();
		this.uiObjects[5].GetComponent<Image>().fillAmount = this.GetChance(value) * 0.01f;
		this.uiObjects[5].GetComponent<Image>().color = this.GetValColor(this.GetChance(value));
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.workPoints[value]).ToString();
		this.uiObjects[6].GetComponent<Image>().fillAmount = this.workPoints[value] * 0.01f;
		this.uiObjects[6].GetComponent<Image>().color = this.GetValColor(this.workPoints[value]);
	}

	
	public float GetChance(int i)
	{
		this.FindScripts();
		float num = this.chance[i];
		num += (float)this.mS_.GetStudioLevel(this.mS_.studioPoints) * 1.5f;
		num += (float)(this.mS_.year - 1976) * 0.3f;
		num -= (float)this.mS_.difficulty;
		if (num < 1f)
		{
			num = 1f;
		}
		if (num > 100f)
		{
			num = 100f;
		}
		return num;
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

	
	public void DROPDOWN_Erfahrung()
	{
		PlayerPrefs.SetInt(this.uiObjects[0].name, this.uiObjects[0].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt(this.uiObjects[1].name, this.uiObjects[1].GetComponent<Dropdown>().value);
		this.SetData();
	}

	
	public void DROPDOWN_Profession()
	{
		PlayerPrefs.SetInt(this.uiObjects[0].name, this.uiObjects[0].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt(this.uiObjects[1].name, this.uiObjects[1].GetComponent<Dropdown>().value);
		this.SetData();
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		if (!this.rS_)
		{
			return;
		}
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		if (this.mS_.NotEnoughMoney(this.price[value]))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.price[value], 24);
		taskMitarbeitersuche taskMitarbeitersuche = this.guiMain_.AddTask_Mitarbeitersuche();
		taskMitarbeitersuche.Init(false);
		taskMitarbeitersuche.beruf = this.uiObjects[0].GetComponent<Dropdown>().value;
		taskMitarbeitersuche.automatic = this.uiObjects[7].GetComponent<Toggle>().isOn;
		taskMitarbeitersuche.points = this.workPoints[this.uiObjects[1].GetComponent<Dropdown>().value];
		taskMitarbeitersuche.pointsLeft = this.workPoints[this.uiObjects[1].GetComponent<Dropdown>().value];
		taskMitarbeitersuche.berufserfahrung = this.uiObjects[1].GetComponent<Dropdown>().value;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskMitarbeitersuche.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private pickCharacterScript pcS_;

	
	private roomDataScript rdS_;

	
	private roomScript rS_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	public int[] price;

	
	public float[] chance;

	
	public float[] workPoints;
}
