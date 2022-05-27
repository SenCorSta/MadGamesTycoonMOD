using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014C RID: 332
public class Menu_Dev_Update : MonoBehaviour
{
	// Token: 0x06000C1F RID: 3103 RVA: 0x00008848 File Offset: 0x00006A48
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00092BDC File Offset: 0x00090DDC
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00008850 File Offset: 0x00006A50
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00092D7C File Offset: 0x00090F7C
	public void Init(roomScript roomScript_, gameScript gameScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.gS_ = gameScript_;
		this.allAdds = false;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = false;
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			this.sprachen[j] = false;
			if (this.gS_.gameLanguage[j])
			{
				this.uiObjects[26 + j].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[26 + j].GetComponent<Button>().interactable = true;
			}
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[0]), true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[1]), true);
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[2]), true);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[3]), true);
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[4]), true);
		this.uiObjects[14].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[5]), true);
		this.uiObjects[15].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[6]), true);
		this.uiObjects[16].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[7]), true);
		this.UpdateGUI();
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0009300C File Offset: 0x0009120C
	private void UpdateGUI()
	{
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_gameplay).ToString() + "+" + this.GetP_Gameplay().ToString();
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_grafik).ToString() + "+" + this.GetP_Grafik().ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_sound).ToString() + "+" + this.GetP_Sound().ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_technik).ToString() + "+" + this.GetP_Technik().ToString();
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_bugs).ToString() + "-" + this.GetP_Bugs().ToString();
		this.uiObjects[7].GetComponent<Text>().text = (this.uiObjects[6].GetComponent<Slider>().value * 10f).ToString() + "%";
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(this.GetCosts_Bugs(), true);
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				this.uiObjects[17 + i].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.uiObjects[17 + i].GetComponent<Image>().color = Color.white;
			}
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			if (this.sprachen[j])
			{
				this.uiObjects[26 + j].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.uiObjects[26 + j].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[25].GetComponent<Text>().text = this.mS_.GetMoney(this.GetDevCosts(), true);
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x000932A8 File Offset: 0x000914A8
	private int GetP_Gameplay()
	{
		float num = 0f;
		if (this.buttonAdds[0])
		{
			num += 1f;
			num += this.gS_.points_gameplay * 0.02f;
		}
		if (this.buttonAdds[1])
		{
			num += 1f;
			num += this.gS_.points_gameplay * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00093310 File Offset: 0x00091510
	private int GetP_Grafik()
	{
		float num = 0f;
		if (this.buttonAdds[2])
		{
			num += 1f;
			num += this.gS_.points_grafik * 0.02f;
		}
		if (this.buttonAdds[3])
		{
			num += 1f;
			num += this.gS_.points_grafik * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00093378 File Offset: 0x00091578
	private int GetP_Sound()
	{
		float num = 0f;
		if (this.buttonAdds[4])
		{
			num += 1f;
			num += this.gS_.points_sound * 0.02f;
		}
		if (this.buttonAdds[5])
		{
			num += 1f;
			num += this.gS_.points_sound * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000933E0 File Offset: 0x000915E0
	private int GetP_Technik()
	{
		float num = 0f;
		if (this.buttonAdds[6])
		{
			num += 1f;
			num += this.gS_.points_technik * 0.02f;
		}
		if (this.buttonAdds[7])
		{
			num += 1f;
			num += this.gS_.points_technik * 0.02f;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00093448 File Offset: 0x00091648
	private int GetP_Bugs()
	{
		float points_bugs = this.gS_.points_bugs;
		float num = this.uiObjects[6].GetComponent<Slider>().value;
		num *= 0.1f;
		return Mathf.RoundToInt(points_bugs * num);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0000886B File Offset: 0x00006A6B
	private long GetCosts_Bugs()
	{
		return (long)this.GetP_Bugs() * (this.gS_.costs_entwicklung / 7500L);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00093484 File Offset: 0x00091684
	private long GetDevCosts()
	{
		long num = this.GetCosts_Bugs();
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += (long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[i]);
				num += (long)Mathf.RoundToInt((float)this.gS_.costs_updates * this.devCostsPercent[i]);
			}
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			if (this.sprachen[j] && !this.mS_.Muttersprache(j))
			{
				num += (long)(this.gS_.GetGesamtDevPoints() * 5);
			}
		}
		return num;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00093530 File Offset: 0x00091730
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[105]);
		this.guiMain_.uiObjects[105].GetComponent<Menu_Dev_UpdateSelectGame>().Init(this.rS_, 0);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00008887 File Offset: 0x00006A87
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x000088AF File Offset: 0x00006AAF
	public void BUTTON_Sprache(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.sprachen[i] = !this.sprachen[i];
		this.UpdateGUI();
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00093590 File Offset: 0x00091790
	public void BUTTON_AlleSprache()
	{
		this.sfx_.PlaySound(3, true);
		this.allSprachen = !this.allSprachen;
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.uiObjects[26 + i].GetComponent<Button>().interactable)
			{
				this.sprachen[i] = this.allSprachen;
			}
		}
		this.UpdateGUI();
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x000935F8 File Offset: 0x000917F8
	public void BUTTON_AlleAdds()
	{
		this.sfx_.PlaySound(3, true);
		this.allAdds = !this.allAdds;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = this.allAdds;
		}
		this.UpdateGUI();
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00093648 File Offset: 0x00091848
	public void BUTTON_AllBugs()
	{
		this.sfx_.PlaySound(3, true);
		this.allBugs = !this.allBugs;
		if (this.allBugs)
		{
			this.uiObjects[6].GetComponent<Slider>().value = 10f;
			return;
		}
		this.uiObjects[6].GetComponent<Slider>().value = 0f;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x000936A8 File Offset: 0x000918A8
	public void BUTTON_Start()
	{
		int num = Mathf.RoundToInt((float)this.GetDevCosts());
		if (!this.gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (num <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(662), false);
			return;
		}
		if (this.uiObjects[37].GetComponent<Toggle>().isOn)
		{
			bool flag = false;
			for (int i = 0; i < this.buttonAdds.Length; i++)
			{
				if (this.buttonAdds[i])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.guiMain_.MessageBox(this.tS_.GetText(727), false);
				return;
			}
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)num, 15);
		taskUpdate taskUpdate = this.guiMain_.AddTask_Update();
		taskUpdate.Init(false);
		taskUpdate.targetID = this.gS_.myID;
		taskUpdate.devCosts = Mathf.RoundToInt((float)this.GetDevCosts());
		taskUpdate.pointsGameplay = this.GetP_Gameplay();
		taskUpdate.pointsGrafik = this.GetP_Grafik();
		taskUpdate.pointsSound = this.GetP_Sound();
		taskUpdate.pointsTechnik = this.GetP_Technik();
		taskUpdate.pointsBugs = this.GetP_Bugs();
		taskUpdate.automatic = this.uiObjects[37].GetComponent<Toggle>().isOn;
		float num2 = (float)this.gS_.GetGesamtDevPoints();
		num2 *= 0.1f;
		taskUpdate.points = (float)Mathf.RoundToInt(num2);
		for (int j = 0; j < this.buttonAdds.Length; j++)
		{
			if (this.buttonAdds[j])
			{
				taskUpdate.quality += 0.1f;
				taskUpdate.points += (float)Mathf.RoundToInt((float)this.gS_.GetGesamtDevPoints() * 0.02f);
			}
		}
		for (int k = 0; k < this.sprachen.Length; k++)
		{
			if (this.sprachen[k])
			{
				taskUpdate.quality += 0.02f;
				taskUpdate.sprachen[k] = this.sprachen[k];
				taskUpdate.points += 10f;
			}
		}
		taskUpdate.points += (float)this.GetP_Bugs() * 0.3f;
		taskUpdate.pointsLeft = taskUpdate.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskUpdate.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x000088D7 File Offset: 0x00006AD7
	public void SLIDER_Bugs()
	{
		this.UpdateGUI();
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00002098 File Offset: 0x00000298
	public void TOGGLE_Auto()
	{
	}

	// Token: 0x0400107B RID: 4219
	public GameObject[] uiObjects;

	// Token: 0x0400107C RID: 4220
	public float[] devCostsPercent;

	// Token: 0x0400107D RID: 4221
	private bool[] buttonAdds = new bool[8];

	// Token: 0x0400107E RID: 4222
	private bool[] sprachen = new bool[11];

	// Token: 0x0400107F RID: 4223
	private GameObject main_;

	// Token: 0x04001080 RID: 4224
	private mainScript mS_;

	// Token: 0x04001081 RID: 4225
	private textScript tS_;

	// Token: 0x04001082 RID: 4226
	private GUI_Main guiMain_;

	// Token: 0x04001083 RID: 4227
	private sfxScript sfx_;

	// Token: 0x04001084 RID: 4228
	private genres genres_;

	// Token: 0x04001085 RID: 4229
	private themes themes_;

	// Token: 0x04001086 RID: 4230
	private licences licences_;

	// Token: 0x04001087 RID: 4231
	private engineFeatures eF_;

	// Token: 0x04001088 RID: 4232
	private cameraMovementScript cmS_;

	// Token: 0x04001089 RID: 4233
	private unlockScript unlock_;

	// Token: 0x0400108A RID: 4234
	private gameplayFeatures gF_;

	// Token: 0x0400108B RID: 4235
	private games games_;

	// Token: 0x0400108C RID: 4236
	private gameScript gS_;

	// Token: 0x0400108D RID: 4237
	private roomScript rS_;

	// Token: 0x0400108E RID: 4238
	private bool allSprachen;

	// Token: 0x0400108F RID: 4239
	private bool allAdds;

	// Token: 0x04001090 RID: 4240
	private bool allBugs;
}
