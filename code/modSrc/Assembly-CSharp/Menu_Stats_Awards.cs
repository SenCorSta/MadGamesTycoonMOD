using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_Awards : MonoBehaviour
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
		this.SetData();
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	private void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.awards[4].ToString() + "x";
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.awards[2].ToString() + "x";
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.awards[3].ToString() + "x";
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.awards[0].ToString() + "x";
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.awards[1].ToString() + "x";
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.awards[8].ToString() + "x";
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.awards[7].ToString() + "x";
		this.uiObjects[7].GetComponent<Text>().text = this.mS_.awards[6].ToString() + "x";
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.awards[5].ToString() + "x";
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.awards[9].ToString() + "x";
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.awards[10].ToString() + "x";
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.awards[11].ToString() + "x";
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
