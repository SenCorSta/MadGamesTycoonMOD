using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Support_Fanshop : MonoBehaviour
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
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
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
		this.SetData();
	}

	
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.SetData();
	}

	
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		float num = 0f;
		for (int i = 0; i < this.games_.arrayMyIpScripts.Count; i++)
		{
			if (this.games_.arrayMyIpScripts[i])
			{
				for (int j = 0; j < this.games_.arrayMyIpScripts[i].merchBestellungen.Length; j++)
				{
					num += (float)this.games_.arrayMyIpScripts[i].merchBestellungen[j];
				}
			}
		}
		num /= 500f;
		this.guiMain_.DrawStars10_Color(this.uiObjects[0], Mathf.RoundToInt(num), Color.white);
		if (num <= 10f)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.Round(num, 2).ToString();
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = ">10.0";
	}

	
	public void BUTTON_Fanshop()
	{
		this.sfx_.PlaySound(3, false);
		this.guiMain_.uiObjects[366].SetActive(true);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Start()
	{
		this.sfx_.PlaySound(3, true);
		taskFanshop taskFanshop = this.guiMain_.AddTask_Fanshop();
		taskFanshop.Init(false);
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskFanshop.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private unlockScript unlock_;

	
	private games games_;

	
	public Color[] colors;

	
	private float updateTimer;
}
