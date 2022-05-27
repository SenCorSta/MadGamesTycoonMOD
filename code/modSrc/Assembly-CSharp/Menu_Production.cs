using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FD RID: 509
public class Menu_Production : MonoBehaviour
{
	// Token: 0x0600135E RID: 4958 RVA: 0x0000D476 File Offset: 0x0000B676
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x000D7614 File Offset: 0x000D5814
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

	// Token: 0x06001360 RID: 4960 RVA: 0x000D7700 File Offset: 0x000D5900
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

	// Token: 0x06001361 RID: 4961 RVA: 0x000D77D8 File Offset: 0x000D59D8
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

	// Token: 0x06001362 RID: 4962 RVA: 0x000D7824 File Offset: 0x000D5A24
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

	// Token: 0x06001363 RID: 4963 RVA: 0x000D797C File Offset: 0x000D5B7C
	private void SetData()
	{
		if (this.selectedGame)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[0], false);
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[1], false);
			this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[2], false);
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x000D7A20 File Offset: 0x000D5C20
	private void SetInputFieldData()
	{
		this.uiObjects[7].GetComponent<InputField>().text = this.produktionsmenge[0].ToString();
		this.uiObjects[8].GetComponent<InputField>().text = this.produktionsmenge[1].ToString();
		this.uiObjects[9].GetComponent<InputField>().text = this.produktionsmenge[2].ToString();
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x000D7A98 File Offset: 0x000D5C98
	public void INPUTFIELD_Production(int i)
	{
		if (this.uiObjects[7 + i].GetComponent<InputField>().text.Length <= 0)
		{
			this.produktionsmenge[i] = 0;
			return;
		}
		this.produktionsmenge[i] = int.Parse(this.uiObjects[7 + i].GetComponent<InputField>().text);
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x0000D47E File Offset: 0x0000B67E
	public void SLIDER_Production(int i)
	{
		this.produktionsmenge[i] = Mathf.RoundToInt(this.uiObjects[4 + i].GetComponent<Slider>().value * 1000f);
		this.SetInputFieldData();
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x0000D4AD File Offset: 0x0000B6AD
	private IEnumerator iMinusProduction(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusProduction(i);
		}
		yield break;
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x000D7AEC File Offset: 0x000D5CEC
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

	// Token: 0x06001369 RID: 4969 RVA: 0x0000D4C3 File Offset: 0x0000B6C3
	private IEnumerator iPlusProduction(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusProduction(i);
		}
		yield break;
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x000D7BC0 File Offset: 0x000D5DC0
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

	// Token: 0x0600136B RID: 4971 RVA: 0x0000D4D9 File Offset: 0x0000B6D9
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x000D7C9C File Offset: 0x000D5E9C
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

	// Token: 0x0600136D RID: 4973 RVA: 0x00002098 File Offset: 0x00000298
	public void TOGGLE_Auto()
	{
	}

	// Token: 0x040017A1 RID: 6049
	public GameObject[] uiObjects;

	// Token: 0x040017A2 RID: 6050
	private GameObject main_;

	// Token: 0x040017A3 RID: 6051
	private mainScript mS_;

	// Token: 0x040017A4 RID: 6052
	private textScript tS_;

	// Token: 0x040017A5 RID: 6053
	private unlockScript unlock_;

	// Token: 0x040017A6 RID: 6054
	private GUI_Main guiMain_;

	// Token: 0x040017A7 RID: 6055
	private sfxScript sfx_;

	// Token: 0x040017A8 RID: 6056
	private cameraMovementScript cmS_;

	// Token: 0x040017A9 RID: 6057
	private roomScript rS_;

	// Token: 0x040017AA RID: 6058
	private gameScript selectedGame;

	// Token: 0x040017AB RID: 6059
	public int[] produktionsmenge;

	// Token: 0x040017AC RID: 6060
	private float updateTimer;
}
