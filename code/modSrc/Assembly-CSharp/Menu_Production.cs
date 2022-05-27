using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Production : MonoBehaviour
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
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	private void Update()
	{
		if (!this.selectedGame.isOnMarket)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.MultiplayerUpdate();
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.selectedGame.GetProduktionskosten(0) * (float)this.produktionsmenge[0]), true);
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.selectedGame.GetProduktionskosten(1) * (float)this.produktionsmenge[1]), true);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.selectedGame.GetProduktionskosten(2) * (float)this.produktionsmenge[2]), true);
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

	
	public void Init(roomScript roomS_, gameScript gS_)
	{
		if (!roomS_ || !gS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.FindScripts();
		this.rS_ = roomS_;
		this.selectedGame = gS_;
		if (this.selectedGame.typ_budget || this.selectedGame.typ_bundle || this.selectedGame.typ_goty || this.selectedGame.typ_addon || this.selectedGame.typ_mmoaddon || this.selectedGame.typ_addonStandalone)
		{
			this.uiObjects[5].GetComponent<Slider>().interactable = false;
			this.uiObjects[6].GetComponent<Slider>().interactable = false;
			this.uiObjects[8].GetComponent<InputField>().interactable = false;
			this.uiObjects[9].GetComponent<InputField>().interactable = false;
			this.produktionsmenge[1] = 0;
			this.produktionsmenge[2] = 0;
			this.SetInputFieldData();
		}
		else
		{
			this.uiObjects[5].GetComponent<Slider>().interactable = true;
			this.uiObjects[6].GetComponent<Slider>().interactable = true;
			this.uiObjects[8].GetComponent<InputField>().interactable = true;
			this.uiObjects[9].GetComponent<InputField>().interactable = true;
		}
		this.uiObjects[0].GetComponent<Text>().text = gS_.GetNameWithTag();
		this.SetData();
	}

	
	private void SetData()
	{
		if (this.selectedGame)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[0], false);
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[1], false);
			this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[2], false);
		}
	}

	
	private void SetInputFieldData()
	{
		this.uiObjects[7].GetComponent<InputField>().text = this.produktionsmenge[0].ToString();
		this.uiObjects[8].GetComponent<InputField>().text = this.produktionsmenge[1].ToString();
		this.uiObjects[9].GetComponent<InputField>().text = this.produktionsmenge[2].ToString();
	}

	
	public void INPUTFIELD_Production(int i)
	{
		if (this.uiObjects[7 + i].GetComponent<InputField>().text.Length <= 0)
		{
			this.produktionsmenge[i] = 0;
			return;
		}
		this.produktionsmenge[i] = int.Parse(this.uiObjects[7 + i].GetComponent<InputField>().text);
	}

	
	public void SLIDER_Production(int i)
	{
		this.produktionsmenge[i] = Mathf.RoundToInt(this.uiObjects[4 + i].GetComponent<Slider>().value * 1000f);
		this.SetInputFieldData();
	}

	
	private IEnumerator iMinusProduction(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusProduction(i);
		}
		yield break;
	}

	
	public void BUTTON_MinusProduction(int i)
	{
		if ((this.selectedGame.typ_budget || this.selectedGame.typ_bundle || this.selectedGame.typ_goty || this.selectedGame.typ_addon || this.selectedGame.typ_mmoaddon || this.selectedGame.typ_addonStandalone) && (i == 1 || i == 2))
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.produktionsmenge[i] -= 1000;
		if (this.produktionsmenge[i] < 0)
		{
			this.produktionsmenge[i] = 0;
		}
		base.StartCoroutine(this.iMinusProduction(i));
		this.SetInputFieldData();
		this.uiObjects[4 + i].GetComponent<Slider>().value = (float)(this.produktionsmenge[i] / 1000);
	}

	
	private IEnumerator iPlusProduction(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusProduction(i);
		}
		yield break;
	}

	
	public void BUTTON_PlusProduction(int i)
	{
		if ((this.selectedGame.typ_budget || this.selectedGame.typ_bundle || this.selectedGame.typ_goty || this.selectedGame.typ_addon || this.selectedGame.typ_mmoaddon || this.selectedGame.typ_addonStandalone) && (i == 1 || i == 2))
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.produktionsmenge[i] += 1000;
		if (this.produktionsmenge[i] > 99999999)
		{
			this.produktionsmenge[i] = 99999999;
		}
		base.StartCoroutine(this.iPlusProduction(i));
		this.SetInputFieldData();
		this.uiObjects[4 + i].GetComponent<Slider>().value = (float)(this.produktionsmenge[i] / 1000);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		if (!this.selectedGame)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (!this.uiObjects[13].GetComponent<Toggle>().isOn && this.produktionsmenge[0] == 0 && this.produktionsmenge[1] == 0 && this.produktionsmenge[2] == 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1136), false);
			return;
		}
		taskProduction taskProduction = this.guiMain_.AddTask_Production();
		taskProduction.Init(false);
		taskProduction.targetID = this.selectedGame.myID;
		taskProduction.automatic = this.uiObjects[13].GetComponent<Toggle>().isOn;
		taskProduction.amountStandard = this.produktionsmenge[0];
		taskProduction.amountDeluxe = this.produktionsmenge[1];
		taskProduction.amountCollectors = this.produktionsmenge[2];
		taskProduction.gesamtProduktion = this.produktionsmenge[0] + this.produktionsmenge[1] + this.produktionsmenge[2];
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskProduction.myID;
		}
		this.guiMain_.CloseMenu();
		this.guiMain_.uiObjects[221].SetActive(false);
		base.gameObject.SetActive(false);
	}

	
	public void TOGGLE_Auto()
	{
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private unlockScript unlock_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private roomScript rS_;

	
	private gameScript selectedGame;

	
	public int[] produktionsmenge;

	
	private float updateTimer;
}
