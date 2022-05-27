using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Bank : MonoBehaviour
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

	
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.GetKreditlimit(), true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.GetKredit(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mS_.GetKreditZinsen(), true);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(25000L, true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney(25000L, true);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney(50000L, true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney(50000L, true);
		this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney(100000L, true);
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(100000L, true);
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney(250000L, true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney(250000L, true);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_KreditAufnehmen(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (i == 0)
		{
			long num = this.mS_.GetKreditlimit() - this.mS_.kredit;
			this.mS_.kredit += num;
			this.mS_.money += num;
			this.Init();
			return;
		}
		if (this.mS_.kredit + (long)i <= this.mS_.GetKreditlimit())
		{
			this.mS_.kredit += (long)i;
			this.mS_.money += (long)i;
			this.Init();
			return;
		}
		long num2 = this.mS_.GetKreditlimit() - this.mS_.kredit;
		this.mS_.kredit += num2;
		this.mS_.money += num2;
		this.Init();
	}

	
	public void BUTTON_KreditAbzahlen(int i)
	{
		if (i == 0)
		{
			for (;;)
			{
				this.BUTTON_KreditAbzahlen(25000);
				if (this.mS_.money < 25000L)
				{
					break;
				}
				if (this.mS_.kredit <= 0L)
				{
					return;
				}
			}
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (this.mS_.money >= (long)i)
		{
			this.mS_.kredit -= (long)i;
			this.mS_.money -= (long)i;
			if (this.mS_.kredit < 0L)
			{
				this.mS_.money += this.mS_.kredit * -1L;
				this.mS_.kredit = 0L;
			}
			this.Init();
		}
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private float updateTimer;
}
