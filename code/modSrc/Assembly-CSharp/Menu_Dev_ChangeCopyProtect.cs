using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_ChangeCopyProtect : MonoBehaviour
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

	
	public void BUTTON_CopyProtect()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[68]);
	}

	
	public void BUTTON_CopyProtectKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[49]);
	}

	
	public void BUTTON_AntiCheat()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[236]);
	}

	
	public void BUTTON_AntiCheatKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[234]);
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
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

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private platforms platforms_;

	
	public gameScript gS_;

	
	public int g_GameCopyProtect = -1;

	
	public copyProtectScript g_GameCopyProtectScript_;

	
	public int g_GameAntiCheat = -1;

	
	public antiCheatScript g_GameAntiCheatScript_;
}
