using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_KonsoleComplete : MonoBehaviour
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
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
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

	
	private void OnEnable()
	{
		this.FindScripts();
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(platformScript s1_, taskKonsole s2_)
	{
		this.FindScripts();
		this.pS_ = s1_;
		this.task_ = s2_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.myName;
		this.pS_.SetPic(this.uiObjects[1]);
		this.uiObjects[2].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		this.uiObjects[4].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(1612) + ": <b><color=blue>" + this.mS_.GetMoney((long)this.platforms_.GetPerformance(this.pS_), false) + "</color></b>";
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Release()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.startProduktionskosten = this.pS_.CalcStartProductionsCosts();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[328]);
		this.guiMain_.uiObjects[328].GetComponent<Menu_Konsolenpreis>().Init(this.pS_, this.task_);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Verwerfen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[327]);
		this.guiMain_.uiObjects[327].GetComponent<Menu_W_Dev_KonsoleVerwerfen>().Init(this.pS_, this.task_);
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

	
	private forschungSonstiges forschungSonstiges_;

	
	private platforms platforms_;

	
	private platformScript pS_;

	
	private taskKonsole task_;
}
