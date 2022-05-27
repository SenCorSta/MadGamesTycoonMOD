using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014D RID: 333
public class Menu_Dev_Update : MonoBehaviour
{
	// Token: 0x06000C34 RID: 3124 RVA: 0x0008355F File Offset: 0x0008175F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00083568 File Offset: 0x00081768
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

	// Token: 0x06000C36 RID: 3126 RVA: 0x00083706 File Offset: 0x00081906
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00083724 File Offset: 0x00081924
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

	// Token: 0x06000C38 RID: 3128 RVA: 0x000839B4 File Offset: 0x00081BB4
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

	// Token: 0x06000C39 RID: 3129 RVA: 0x00083C50 File Offset: 0x00081E50
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

	// Token: 0x06000C3A RID: 3130 RVA: 0x00083CB8 File Offset: 0x00081EB8
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

	// Token: 0x06000C3B RID: 3131 RVA: 0x00083D20 File Offset: 0x00081F20
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

	// Token: 0x06000C3C RID: 3132 RVA: 0x00083D88 File Offset: 0x00081F88
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

	// Token: 0x06000C3D RID: 3133 RVA: 0x00083DF0 File Offset: 0x00081FF0
	private int GetP_Bugs()
	{
		float points_bugs = this.gS_.points_bugs;
		float num = this.uiObjects[6].GetComponent<Slider>().value;
		num *= 0.1f;
		return Mathf.RoundToInt(points_bugs * num);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00083E2A File Offset: 0x0008202A
	private long GetCosts_Bugs()
	{
		return (long)this.GetP_Bugs() * (this.gS_.costs_entwicklung / 7500L);
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00083E48 File Offset: 0x00082048
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

	// Token: 0x06000C40 RID: 3136 RVA: 0x00083EF4 File Offset: 0x000820F4
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[105]);
		this.guiMain_.uiObjects[105].GetComponent<Menu_Dev_UpdateSelectGame>().Init(this.rS_, 0);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00083F52 File Offset: 0x00082152
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00083F7A File Offset: 0x0008217A
	public void BUTTON_Sprache(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.sprachen[i] = !this.sprachen[i];
		this.UpdateGUI();
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00083FA4 File Offset: 0x000821A4
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

	// Token: 0x06000C44 RID: 3140 RVA: 0x0008400C File Offset: 0x0008220C
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

	// Token: 0x06000C45 RID: 3141 RVA: 0x0008405C File Offset: 0x0008225C
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

	// Token: 0x06000C46 RID: 3142 RVA: 0x000840BC File Offset: 0x000822BC
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

	// Token: 0x06000C47 RID: 3143 RVA: 0x00084361 File Offset: 0x00082561
	public void SLIDER_Bugs()
	{
		this.UpdateGUI();
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_Auto()
	{
	}

	// Token: 0x04001083 RID: 4227
	public GameObject[] uiObjects;

	// Token: 0x04001084 RID: 4228
	public float[] devCostsPercent;

	// Token: 0x04001085 RID: 4229
	private bool[] buttonAdds = new bool[8];

	// Token: 0x04001086 RID: 4230
	private bool[] sprachen = new bool[11];

	// Token: 0x04001087 RID: 4231
	private GameObject main_;

	// Token: 0x04001088 RID: 4232
	private mainScript mS_;

	// Token: 0x04001089 RID: 4233
	private textScript tS_;

	// Token: 0x0400108A RID: 4234
	private GUI_Main guiMain_;

	// Token: 0x0400108B RID: 4235
	private sfxScript sfx_;

	// Token: 0x0400108C RID: 4236
	private genres genres_;

	// Token: 0x0400108D RID: 4237
	private themes themes_;

	// Token: 0x0400108E RID: 4238
	private licences licences_;

	// Token: 0x0400108F RID: 4239
	private engineFeatures eF_;

	// Token: 0x04001090 RID: 4240
	private cameraMovementScript cmS_;

	// Token: 0x04001091 RID: 4241
	private unlockScript unlock_;

	// Token: 0x04001092 RID: 4242
	private gameplayFeatures gF_;

	// Token: 0x04001093 RID: 4243
	private games games_;

	// Token: 0x04001094 RID: 4244
	private gameScript gS_;

	// Token: 0x04001095 RID: 4245
	private roomScript rS_;

	// Token: 0x04001096 RID: 4246
	private bool allSprachen;

	// Token: 0x04001097 RID: 4247
	private bool allAdds;

	// Token: 0x04001098 RID: 4248
	private bool allBugs;
}
