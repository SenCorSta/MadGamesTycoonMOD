using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Support_Anrufe : MonoBehaviour
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
		float anrufe100Prozent = this.mS_.GetAnrufe100Prozent();
		this.uiObjects[0].GetComponent<Text>().text = Mathf.RoundToInt(anrufe100Prozent).ToString() + "%";
		this.uiObjects[1].GetComponent<Image>().fillAmount = anrufe100Prozent * 0.01f;
		this.uiObjects[1].GetComponent<Image>().color = this.colors[this.mS_.GetAnrufeAmount()];
		this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(754 + this.mS_.GetAnrufeAmount());
		string text = this.tS_.GetText(758);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.mS_.anrufe, false));
		this.uiObjects[3].GetComponent<Text>().text = text;
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
		taskSupport taskSupport = this.guiMain_.AddTask_Support();
		taskSupport.Init(false);
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskSupport.myID;
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

	
	public Color[] colors;

	
	private float updateTimer;
}
