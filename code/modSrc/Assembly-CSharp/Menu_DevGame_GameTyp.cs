using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_GameTyp : MonoBehaviour
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
	}

	
	private void OnEnable()
	{
		this.FindScripts();
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
		this.Unlock(21, this.uiObjects[3], this.uiObjects[5]);
		this.Unlock(22, this.uiObjects[4], this.uiObjects[6]);
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mDevGame_.costs_gameTyp[0], true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameTyp[1] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameTyp[2] * (this.mS_.difficulty + 1)), true);
	}

	
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			lock_.SetActive(false);
			return;
		}
		button_.GetComponent<Button>().interactable = false;
		lock_.SetActive(true);
	}

	
	public void BUTTON_GameTyp(int i)
	{
		this.mDevGame_.SetGameTyp(i);
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

	
	private float updateTimer;
}
