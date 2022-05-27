using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000133 RID: 307
public class Menu_Dev_F2PUpdate : MonoBehaviour
{
	// Token: 0x06000AFA RID: 2810 RVA: 0x00076E6F File Offset: 0x0007506F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00076E78 File Offset: 0x00075078
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

	// Token: 0x06000AFC RID: 2812 RVA: 0x00077016 File Offset: 0x00075216
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00077034 File Offset: 0x00075234
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
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = this.gS_.f2pInteresse * 0.01f;
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.f2pInteresse).ToString() + "%";
		for (int j = 0; j < this.goButtons.Length; j++)
		{
			if (this.goButtons[j])
			{
				this.goButtons[j].transform.GetChild(3).GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.GetGesamteEntwicklungskosten() * this.devCostsPercent[j]), true);
			}
		}
		this.UpdateGUI();
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00077158 File Offset: 0x00075358
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				this.goButtons[i].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.goButtons[i].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(this.GetDevCosts(), true);
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x000771E4 File Offset: 0x000753E4
	private long GetDevCosts()
	{
		long num = 0L;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += (long)Mathf.RoundToInt((float)this.gS_.GetGesamteEntwicklungskosten() * this.devCostsPercent[i]);
			}
		}
		return num;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00077230 File Offset: 0x00075430
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[299]);
		this.guiMain_.uiObjects[299].GetComponent<Menu_Dev_F2PUpdateSelectGame>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00077293 File Offset: 0x00075493
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x000772BC File Offset: 0x000754BC
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

	// Token: 0x06000B03 RID: 2819 RVA: 0x0007730C File Offset: 0x0007550C
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
		if (this.uiObjects[4].GetComponent<Toggle>().isOn)
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
		taskF2PUpdate taskF2PUpdate = this.guiMain_.AddTask_F2PUpdate();
		taskF2PUpdate.Init(false);
		taskF2PUpdate.targetID = this.gS_.myID;
		taskF2PUpdate.devCosts = Mathf.RoundToInt((float)this.GetDevCosts());
		taskF2PUpdate.automatic = this.uiObjects[4].GetComponent<Toggle>().isOn;
		float num2 = (float)this.gS_.GetGesamtDevPoints();
		num2 *= 0.1f;
		taskF2PUpdate.points = (float)Mathf.RoundToInt(num2);
		for (int j = 0; j < this.buttonAdds.Length; j++)
		{
			if (this.buttonAdds[j])
			{
				taskF2PUpdate.quality += this.interestBoost[j];
				taskF2PUpdate.points += (float)Mathf.RoundToInt((float)this.gS_.GetGesamtDevPoints() * this.interestBoost[j] * 0.02f);
			}
		}
		taskF2PUpdate.pointsLeft = taskF2PUpdate.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskF2PUpdate.myID;
		}
		this.guiMain_.uiObjects[299].SetActive(false);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000F46 RID: 3910
	public GameObject[] uiObjects;

	// Token: 0x04000F47 RID: 3911
	public float[] devCostsPercent;

	// Token: 0x04000F48 RID: 3912
	private bool[] buttonAdds = new bool[12];

	// Token: 0x04000F49 RID: 3913
	public float[] interestBoost;

	// Token: 0x04000F4A RID: 3914
	public GameObject[] goButtons;

	// Token: 0x04000F4B RID: 3915
	private GameObject main_;

	// Token: 0x04000F4C RID: 3916
	private mainScript mS_;

	// Token: 0x04000F4D RID: 3917
	private textScript tS_;

	// Token: 0x04000F4E RID: 3918
	private GUI_Main guiMain_;

	// Token: 0x04000F4F RID: 3919
	private sfxScript sfx_;

	// Token: 0x04000F50 RID: 3920
	private genres genres_;

	// Token: 0x04000F51 RID: 3921
	private themes themes_;

	// Token: 0x04000F52 RID: 3922
	private licences licences_;

	// Token: 0x04000F53 RID: 3923
	private engineFeatures eF_;

	// Token: 0x04000F54 RID: 3924
	private cameraMovementScript cmS_;

	// Token: 0x04000F55 RID: 3925
	private unlockScript unlock_;

	// Token: 0x04000F56 RID: 3926
	private gameplayFeatures gF_;

	// Token: 0x04000F57 RID: 3927
	private games games_;

	// Token: 0x04000F58 RID: 3928
	private gameScript gS_;

	// Token: 0x04000F59 RID: 3929
	private roomScript rS_;

	// Token: 0x04000F5A RID: 3930
	private bool allAdds;
}
