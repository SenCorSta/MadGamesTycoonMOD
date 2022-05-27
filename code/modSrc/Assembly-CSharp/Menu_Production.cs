using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FE RID: 510
public class Menu_Production : MonoBehaviour
{
	// Token: 0x06001379 RID: 4985 RVA: 0x000CD34B File Offset: 0x000CB54B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000CD354 File Offset: 0x000CB554
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

	// Token: 0x0600137B RID: 4987 RVA: 0x000CD440 File Offset: 0x000CB640
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

	// Token: 0x0600137C RID: 4988 RVA: 0x000CD518 File Offset: 0x000CB718
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

	// Token: 0x0600137D RID: 4989 RVA: 0x000CD564 File Offset: 0x000CB764
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

	// Token: 0x0600137E RID: 4990 RVA: 0x000CD6BC File Offset: 0x000CB8BC
	private void SetData()
	{
		if (this.selectedGame)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[0], false);
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[1], false);
			this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.selectedGame.lagerbestand[2], false);
		}
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x000CD760 File Offset: 0x000CB960
	private void SetInputFieldData()
	{
		this.uiObjects[7].GetComponent<InputField>().text = this.produktionsmenge[0].ToString();
		this.uiObjects[8].GetComponent<InputField>().text = this.produktionsmenge[1].ToString();
		this.uiObjects[9].GetComponent<InputField>().text = this.produktionsmenge[2].ToString();
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x000CD7D8 File Offset: 0x000CB9D8
	public void INPUTFIELD_Production(int i)
	{
		if (this.uiObjects[7 + i].GetComponent<InputField>().text.Length <= 0)
		{
			this.produktionsmenge[i] = 0;
			return;
		}
		this.produktionsmenge[i] = int.Parse(this.uiObjects[7 + i].GetComponent<InputField>().text);
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000CD82C File Offset: 0x000CBA2C
	public void SLIDER_Production(int i)
	{
		this.produktionsmenge[i] = Mathf.RoundToInt(this.uiObjects[4 + i].GetComponent<Slider>().value * 1000f);
		this.SetInputFieldData();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000CD85B File Offset: 0x000CBA5B
	private IEnumerator iMinusProduction(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusProduction(i);
		}
		yield break;
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000CD874 File Offset: 0x000CBA74
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

	// Token: 0x06001384 RID: 4996 RVA: 0x000CD945 File Offset: 0x000CBB45
	private IEnumerator iPlusProduction(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusProduction(i);
		}
		yield break;
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000CD95C File Offset: 0x000CBB5C
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

	// Token: 0x06001386 RID: 4998 RVA: 0x000CDA35 File Offset: 0x000CBC35
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000CDA50 File Offset: 0x000CBC50
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

	// Token: 0x06001388 RID: 5000 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_Auto()
	{
	}

	// Token: 0x040017AA RID: 6058
	public GameObject[] uiObjects;

	// Token: 0x040017AB RID: 6059
	private GameObject main_;

	// Token: 0x040017AC RID: 6060
	private mainScript mS_;

	// Token: 0x040017AD RID: 6061
	private textScript tS_;

	// Token: 0x040017AE RID: 6062
	private unlockScript unlock_;

	// Token: 0x040017AF RID: 6063
	private GUI_Main guiMain_;

	// Token: 0x040017B0 RID: 6064
	private sfxScript sfx_;

	// Token: 0x040017B1 RID: 6065
	private cameraMovementScript cmS_;

	// Token: 0x040017B2 RID: 6066
	private roomScript rS_;

	// Token: 0x040017B3 RID: 6067
	private gameScript selectedGame;

	// Token: 0x040017B4 RID: 6068
	public int[] produktionsmenge;

	// Token: 0x040017B5 RID: 6069
	private float updateTimer;
}
