using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Messe : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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
	}

	
	public void Init()
	{
		this.FindScripts();
		this.guiMain_.OpenMenu(false);
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.guiMain_.BUTTON_GameSpeed(0f);
			this.mS_.mpCalls_.SetPlayersUnready();
		}
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(0), true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(1), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(2), true);
		Menu_MesseErgebnis component = this.guiMain_.uiObjects[188].GetComponent<Menu_MesseErgebnis>();
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		int num2 = Mathf.RoundToInt((float)(Mathf.RoundToInt(350000f * component.curveBesucher.Evaluate(num / 600f)) + 1000 + UnityEngine.Random.Range(0, 1000)) * 1.5f);
		num2 = num2 / 1000 * 1000;
		string text = this.tS_.GetText(953);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)Mathf.RoundToInt((float)num2), false));
		this.uiObjects[3].GetComponent<Text>().text = text;
		if (this.mS_.settings_ && this.mS_.settings_.hideConvention)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.sfx_.PlaySound(50, false);
	}

	
	private void Update()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public int GetPrice(int i)
	{
		int num = this.mS_.year - 1975;
		if (num > 50)
		{
			num = 50;
		}
		return this.price[i] * num + 5000;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			this.mS_.mpCalls_.CLIENT_Send_Command(1);
		}
	}

	
	public void BUTTON_Stand(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[186]);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().Init(i);
	}

	
	public GameObject[] uiObjects;

	
	public int[] price;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;
}
