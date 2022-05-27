using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_Size : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.Init();
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
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
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.Init();
	}

	
	private void Init()
	{
		this.FindScripts();
		this.fS_.Unlock(0, this.uiObjects[14], this.uiObjects[10]);
		this.fS_.Unlock(1, this.uiObjects[15], this.uiObjects[11]);
		this.fS_.Unlock(2, this.uiObjects[16], this.uiObjects[12]);
		this.fS_.Unlock(3, this.uiObjects[17], this.uiObjects[13]);
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mDevGame_.costs_gameSize[0], true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[1] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[2] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[3] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[4] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[0];
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[1];
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[2];
		this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[3];
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(335);
	}

	
	public void BUTTON_GameSize(int i)
	{
		this.mDevGame_.SetGameSize(i);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private Menu_DevGame mDevGame_;

	
	private unlockScript unlock_;

	
	private forschungSonstiges fS_;

	
	private float updateTimer;
}
