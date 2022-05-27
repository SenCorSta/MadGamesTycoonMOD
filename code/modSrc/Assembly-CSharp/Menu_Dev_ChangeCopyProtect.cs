using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012E RID: 302
public class Menu_Dev_ChangeCopyProtect : MonoBehaviour
{
	// Token: 0x06000AB1 RID: 2737 RVA: 0x00073E06 File Offset: 0x00072006
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x00073E10 File Offset: 0x00072010
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
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x00073FCC File Offset: 0x000721CC
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.SetCopyProtect(this.gS_.gameCopyProtect);
		this.SetAntiCheat(this.gS_.gameAntiCheat);
		this.Unlock(31, this.uiObjects[10], this.uiObjects[11]);
		this.Unlock(31, null, this.uiObjects[12]);
		this.Unlock(64, this.uiObjects[13], this.uiObjects[14]);
		this.Unlock(64, null, this.uiObjects[15]);
		if (game_.arcade)
		{
			this.uiObjects[11].GetComponent<Button>().interactable = false;
			this.uiObjects[14].GetComponent<Button>().interactable = false;
		}
		this.CalcDevCosts();
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x000740B4 File Offset: 0x000722B4
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			if (lock_)
			{
				lock_.SetActive(false);
				return;
			}
		}
		else
		{
			button_.GetComponent<Button>().interactable = false;
			if (lock_)
			{
				lock_.SetActive(true);
			}
		}
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x00074107 File Offset: 0x00072307
	public void BUTTON_CopyProtect()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[68]);
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0007412F File Offset: 0x0007232F
	public void BUTTON_CopyProtectKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[49]);
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00074157 File Offset: 0x00072357
	public void BUTTON_AntiCheat()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[236]);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00074182 File Offset: 0x00072382
	public void BUTTON_AntiCheatKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[234]);
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x000741AD File Offset: 0x000723AD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x000741D4 File Offset: 0x000723D4
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.CalcDevCosts(), 10);
		this.gS_.costs_entwicklung += (long)this.CalcDevCosts();
		this.gS_.gameAntiCheat = this.g_GameAntiCheat;
		this.gS_.gameCopyProtect = this.g_GameCopyProtect;
		this.gS_.gameAntiCheatScript_ = this.g_GameAntiCheatScript_;
		this.gS_.gameCopyProtectScript_ = this.g_GameCopyProtectScript_;
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x00074278 File Offset: 0x00072478
	public void SetCopyProtect(int i)
	{
		this.g_GameCopyProtect = i;
		if (i >= 0)
		{
			GameObject gameObject = GameObject.Find("COPYPROTECT_" + i.ToString());
			if (gameObject)
			{
				copyProtectScript component = gameObject.GetComponent<copyProtectScript>();
				this.g_GameCopyProtectScript_ = component;
				this.uiObjects[2].GetComponent<Text>().text = component.GetName();
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetDevCosts(), true);
				this.uiObjects[4].GetComponent<Image>().fillAmount = component.effekt * 0.01f;
				this.uiObjects[5].GetComponent<Text>().text = this.mS_.Round(component.effekt, 2) + "%";
				this.uiObjects[4].GetComponent<Image>().color = this.GetValColor(component.effekt);
			}
		}
		else
		{
			this.g_GameCopyProtectScript_ = null;
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(383);
			this.uiObjects[3].GetComponent<Text>().text = "";
			this.uiObjects[4].GetComponent<Image>().fillAmount = 0f;
			this.uiObjects[5].GetComponent<Text>().text = "0.0%";
			this.uiObjects[4].GetComponent<Image>().color = this.GetValColor(0f);
		}
		this.CalcDevCosts();
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00074408 File Offset: 0x00072608
	public void SetAntiCheat(int i)
	{
		this.g_GameAntiCheat = i;
		if (i >= 0)
		{
			GameObject gameObject = GameObject.Find("ANTICHEAT_" + i.ToString());
			if (gameObject)
			{
				antiCheatScript component = gameObject.GetComponent<antiCheatScript>();
				this.g_GameAntiCheatScript_ = component;
				this.uiObjects[6].GetComponent<Text>().text = component.GetName();
				this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetDevCosts(), true);
				this.uiObjects[8].GetComponent<Image>().fillAmount = component.effekt * 0.01f;
				this.uiObjects[9].GetComponent<Text>().text = this.mS_.Round(component.effekt, 2) + "%";
				this.uiObjects[8].GetComponent<Image>().color = this.GetValColor(component.effekt);
			}
		}
		else
		{
			this.g_GameAntiCheatScript_ = null;
			this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(1213);
			this.uiObjects[7].GetComponent<Text>().text = "";
			this.uiObjects[8].GetComponent<Image>().fillAmount = 0f;
			this.uiObjects[9].GetComponent<Text>().text = "0.0%";
			this.uiObjects[8].GetComponent<Image>().color = this.GetValColor(0f);
		}
		this.CalcDevCosts();
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x00074598 File Offset: 0x00072798
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

	// Token: 0x06000ABE RID: 2750 RVA: 0x0007460C File Offset: 0x0007280C
	private int CalcDevCosts()
	{
		int num = 0;
		if (this.g_GameCopyProtectScript_ && this.g_GameCopyProtect != this.gS_.gameCopyProtect)
		{
			num += this.g_GameCopyProtectScript_.GetDevCosts();
		}
		if (this.g_GameAntiCheatScript_ && this.g_GameAntiCheat != this.gS_.gameAntiCheat)
		{
			num += this.g_GameAntiCheatScript_.GetDevCosts();
		}
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		return num;
	}

	// Token: 0x04000EEC RID: 3820
	public GameObject[] uiObjects;

	// Token: 0x04000EED RID: 3821
	private GameObject main_;

	// Token: 0x04000EEE RID: 3822
	private mainScript mS_;

	// Token: 0x04000EEF RID: 3823
	private textScript tS_;

	// Token: 0x04000EF0 RID: 3824
	private GUI_Main guiMain_;

	// Token: 0x04000EF1 RID: 3825
	private sfxScript sfx_;

	// Token: 0x04000EF2 RID: 3826
	private genres genres_;

	// Token: 0x04000EF3 RID: 3827
	private themes themes_;

	// Token: 0x04000EF4 RID: 3828
	private licences licences_;

	// Token: 0x04000EF5 RID: 3829
	private engineFeatures eF_;

	// Token: 0x04000EF6 RID: 3830
	private cameraMovementScript cmS_;

	// Token: 0x04000EF7 RID: 3831
	private unlockScript unlock_;

	// Token: 0x04000EF8 RID: 3832
	private gameplayFeatures gF_;

	// Token: 0x04000EF9 RID: 3833
	private games games_;

	// Token: 0x04000EFA RID: 3834
	private platforms platforms_;

	// Token: 0x04000EFB RID: 3835
	public gameScript gS_;

	// Token: 0x04000EFC RID: 3836
	public int g_GameCopyProtect = -1;

	// Token: 0x04000EFD RID: 3837
	public copyProtectScript g_GameCopyProtectScript_;

	// Token: 0x04000EFE RID: 3838
	public int g_GameAntiCheat = -1;

	// Token: 0x04000EFF RID: 3839
	public antiCheatScript g_GameAntiCheatScript_;
}
