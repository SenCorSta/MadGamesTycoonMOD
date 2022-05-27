using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_Restbestand : MonoBehaviour
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.menu_LagerSelect)
		{
			this.menu_LagerSelect = this.guiMain_.uiObjects[225].GetComponent<Menu_LagerSelect>();
		}
	}

	
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		gS_;
		this.game_ = gS_;
		this.SetData();
	}

	
	private void SetData()
	{
		if (this.game_)
		{
			this.money = this.GetSumme(this.game_);
			this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.money, true);
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = "";
		this.money = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			gameScript component = array[i].GetComponent<gameScript>();
			if (component && this.menu_LagerSelect.CheckGameData(component))
			{
				this.money += this.GetSumme(component);
			}
		}
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.money, true);
		if (this.money <= 0)
		{
			this.BUTTON_Abbrechen();
		}
	}

	
	private int GetSumme(gameScript script_)
	{
		if (!script_)
		{
			return 0;
		}
		int reviewTotal = script_.reviewTotal;
		return Mathf.RoundToInt(0.029000001f * (float)script_.reviewTotal * (float)script_.GetLagerbestand());
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		if (this.game_)
		{
			this.mS_.Earn((long)this.money, 1);
			this.game_.umsatzTotal += (long)this.money;
			for (int i = 0; i < this.game_.lagerbestand.Length; i++)
			{
				this.game_.lagerbestand[i] = 0;
			}
			this.games_.LagerplatzVerteilen();
			this.BUTTON_Abbrechen();
			return;
		}
		this.mS_.Earn((long)this.money, 1);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int j = 0; j < array.Length; j++)
		{
			gameScript component = array[j].GetComponent<gameScript>();
			if (component && this.menu_LagerSelect.CheckGameData(component))
			{
				component.umsatzTotal += (long)this.money;
				for (int k = 0; k < component.lagerbestand.Length; k++)
				{
					component.lagerbestand[k] = 0;
				}
			}
		}
		this.games_.LagerplatzVerteilen();
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private games games_;

	
	private gameScript game_;

	
	private Menu_LagerSelect menu_LagerSelect;

	
	private int money;

	
	private float updateTimer;
}
