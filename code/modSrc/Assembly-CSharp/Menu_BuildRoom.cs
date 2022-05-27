using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AF RID: 431
public class Menu_BuildRoom : MonoBehaviour
{
	// Token: 0x06001046 RID: 4166 RVA: 0x000AC4EB File Offset: 0x000AA6EB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x000AC4F4 File Offset: 0x000AA6F4
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		this.main_ = GameObject.Find("Main");
		this.mS_ = this.main_.GetComponent<mainScript>();
		this.tS_ = this.main_.GetComponent<textScript>();
		this.mapS_ = this.main_.GetComponent<mapScript>();
		this.unlock_ = this.main_.GetComponent<unlockScript>();
		this.rdS_ = this.main_.GetComponent<roomDataScript>();
		this.buildRoomScript_ = this.main_.GetComponent<buildRoomScript>();
		this.mCamS_ = GameObject.Find("Camera").GetComponent<mainCameraScript>();
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x000AC5DE File Offset: 0x000AA7DE
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(5);
		}
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000AC5FF File Offset: 0x000AA7FF
	private void OnDisable()
	{
		this.uiObjects[27].SetActive(false);
		this.uiObjects[29].SetActive(false);
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x000AC61F File Offset: 0x000AA81F
	private void Update()
	{
		if (this.mS_.multiplayer && !this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.Update_DesignRoomMenu();
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x000AC650 File Offset: 0x000AA850
	private void Update_DesignRoomMenu()
	{
		if (!Input.GetMouseButton(0) && this.buildRoomScript_.modus == 0 && Input.GetKeyDown(KeyCode.LeftShift))
		{
			this.BUTTON_RemoveRoom();
		}
		if (this.buildRoomScript_.modus == 1 && Input.GetKeyUp(KeyCode.LeftShift))
		{
			this.buildRoomScript_.Remove_DeleteMap();
			this.BUTTON_SetRoom();
		}
		int num = this.buildRoomScript_.AmountTiles();
		this.uiObjects[2].GetComponent<Image>().sprite = this.rdS_.roomData_SPRITE[this.roomTyp];
		this.uiObjects[23].GetComponent<Text>().text = num.ToString() + " " + this.tS_.GetText(72);
		this.uiObjects[24].GetComponent<Text>().text = this.mS_.GetMoney((long)this.rdS_.GetPrice(this.roomTyp), true);
		this.uiObjects[25].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetRoomPrice(), true);
		this.uiObjects[26].GetComponent<Text>().text = this.rdS_.GetName(this.roomTyp);
		if (!this.rdS_.KeineMitarbeiter(this.roomTyp))
		{
			string text = this.tS_.GetText(1681);
			text = text.Replace("<NUM>", this.AnzahlArbeitsplaetze().ToString());
			this.uiObjects[31].GetComponent<Text>().text = text;
		}
		else
		{
			this.uiObjects[31].GetComponent<Text>().text = "";
		}
		this.UpdateSizePanel();
		this.UpdateAcceptButton();
		this.UpdateCloseButton();
		if (this.buildRoomScript_.modus == 0 || this.buildRoomScript_.modus == 1 || this.buildRoomScript_.modus == 3)
		{
			if (!this.uiObjects[0].activeSelf)
			{
				this.uiObjects[0].SetActive(true);
			}
			if (this.uiObjects[1].activeSelf)
			{
				this.uiObjects[1].SetActive(false);
				return;
			}
		}
		else
		{
			if (this.uiObjects[0].activeSelf)
			{
				this.uiObjects[0].SetActive(false);
			}
			if (!this.uiObjects[1].activeSelf)
			{
				this.uiObjects[1].SetActive(true);
			}
		}
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x000AC8A8 File Offset: 0x000AAAA8
	public int AnzahlArbeitsplaetze()
	{
		float num = 3.3f;
		int num2 = this.roomTyp;
		if (num2 != 5)
		{
			if (num2 != 10)
			{
				if (num2 == 13)
				{
					num = 2.7f;
				}
			}
			else
			{
				num = 10f;
			}
		}
		else
		{
			num = 5f;
		}
		int num3 = Mathf.FloorToInt((float)this.buildRoomScript_.AmountTiles() / num);
		if (num3 < 0)
		{
			num3 = 0;
		}
		return num3;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x000AC904 File Offset: 0x000AAB04
	private int GetRoomPrice()
	{
		int num = this.buildRoomScript_.AmountTiles();
		int num2 = this.rdS_.GetPrice(this.roomTyp) * num;
		num2 -= this.buildRoomScript_.moneyRedesign;
		if (num2 < 0)
		{
			num2 = 0;
		}
		return num2;
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x000AC948 File Offset: 0x000AAB48
	private void UpdateSizePanel()
	{
		if ((this.buildRoomScript_.modus == 0 || this.buildRoomScript_.modus == 1) && !this.guiMain_.IsMouseOverGUI() && Input.GetMouseButton(0))
		{
			if (!this.uiObjects[27].activeSelf)
			{
				this.uiObjects[27].SetActive(true);
			}
			Vector2 v = Input.mousePosition;
			v.x += 10f;
			v.y += 10f;
			this.uiObjects[27].GetComponent<RectTransform>().anchoredPosition = this.guiMain_.GetAnchoredPosition(v);
			this.uiObjects[28].GetComponent<Text>().text = (Mathf.Abs(this.buildRoomScript_.roomStartX - this.buildRoomScript_.posX) + 1).ToString() + "x" + (Mathf.Abs(this.buildRoomScript_.roomStartY - this.buildRoomScript_.posY) + 1).ToString();
			return;
		}
		if (this.uiObjects[27].activeSelf)
		{
			this.uiObjects[27].SetActive(false);
		}
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x000ACA80 File Offset: 0x000AAC80
	private void UpdateCloseButton()
	{
		if (this.buildRoomScript_.replaceRoomID == -1)
		{
			if (!this.uiObjects[16].activeSelf)
			{
				this.uiObjects[16].SetActive(true);
			}
			if (!this.uiObjects[3].activeSelf)
			{
				this.uiObjects[3].SetActive(true);
				return;
			}
		}
		else
		{
			if (this.uiObjects[16].activeSelf)
			{
				this.uiObjects[16].SetActive(false);
			}
			if (this.uiObjects[3].activeSelf)
			{
				this.uiObjects[3].SetActive(false);
			}
		}
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x000ACB14 File Offset: 0x000AAD14
	private void UpdateAcceptButton()
	{
		Button component = this.uiObjects[17].GetComponent<Button>();
		int num = 3;
		if (this.roomTyp == 5)
		{
			num = 4;
		}
		if (this.roomTyp == 10)
		{
			num = 5;
		}
		if (this.roomTyp == 14)
		{
			num = 5;
		}
		if (this.roomTyp == 9)
		{
			num = 2;
		}
		if (!this.buildRoomScript_.ExistRoom())
		{
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			return;
		}
		if (this.buildRoomScript_.GetBiggestRoomQuad() < (float)num)
		{
			string text = this.tS_.GetText(75);
			text = text.Replace("<NUM>", num.ToString() + "x" + num.ToString());
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			if (!this.uiObjects[29].activeSelf)
			{
				this.uiObjects[29].SetActive(true);
			}
			this.uiObjects[30].GetComponent<Text>().text = text;
			return;
		}
		if (!this.buildRoomScript_.IsDoor())
		{
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			if (!this.uiObjects[29].activeSelf)
			{
				this.uiObjects[29].SetActive(true);
			}
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(76);
			return;
		}
		if (this.buildRoomScript_.noPath)
		{
			component.interactable = false;
			this.uiObjects[18].GetComponent<Image>().color = Color.grey;
			if (!this.uiObjects[29].activeSelf)
			{
				this.uiObjects[29].SetActive(true);
			}
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(73);
			return;
		}
		component.interactable = true;
		this.uiObjects[18].GetComponent<Image>().color = Color.white;
		if (this.uiObjects[29].activeSelf)
		{
			this.uiObjects[29].SetActive(false);
		}
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x000ACD3C File Offset: 0x000AAF3C
	public void BUTTON_AcceptRoomDesign()
	{
		if (!this.mS_.settings_TutorialOff && this.roomTyp == 1)
		{
			this.guiMain_.SetTutorialStep(6);
		}
		this.sfx_.PlaySound(7, true);
		this.buildRoomScript_.CreateRoom(this.roomTyp, this.GetRoomPrice());
		this.buildRoomScript_.SetInactive();
		this.buildRoomScript_.modus = 0;
		this.buildRoomScript_.moneyRedesign = 0;
		this.Close_DesignRoom();
		this.guiMain_.TOGGLE_Walls();
		this.guiMain_.DROPDOWN_BuyInventar(this.roomTyp);
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x000ACDD4 File Offset: 0x000AAFD4
	private void ResetButtonColors()
	{
		this.uiObjects[19].GetComponent<Image>().color = this.colors[1];
		this.uiObjects[20].GetComponent<Image>().color = this.colors[1];
		this.uiObjects[21].GetComponent<Image>().color = this.colors[1];
		this.uiObjects[22].GetComponent<Image>().color = this.colors[1];
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x000ACE5D File Offset: 0x000AB05D
	public void BUTTON_SetRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 0;
		this.ResetButtonColors();
		this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x000ACE9D File Offset: 0x000AB09D
	public void BUTTON_RemoveRoom()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 1;
		this.ResetButtonColors();
		this.uiObjects[20].GetComponent<Image>().color = this.colors[0];
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x000ACEDD File Offset: 0x000AB0DD
	public void BUTTON_SetDoor()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 2;
		this.ResetButtonColors();
		this.uiObjects[21].GetComponent<Image>().color = this.colors[0];
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x000ACF1D File Offset: 0x000AB11D
	public void BUTTON_SetWindow()
	{
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.modus = 3;
		this.ResetButtonColors();
		this.uiObjects[22].GetComponent<Image>().color = this.colors[0];
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x000ACF5D File Offset: 0x000AB15D
	public void BUTTON_Grab()
	{
		this.buildRoomScript_.modus = 4;
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x000ACF6C File Offset: 0x000AB16C
	public void BUTTON_SelectRoom(int i)
	{
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		this.uiObjects[27].SetActive(false);
		this.uiObjects[29].SetActive(false);
		this.roomTyp = i;
		this.buildRoomScript_.noPath = true;
		this.buildRoomScript_.SetActive();
		this.BUTTON_SetRoom();
		this.mS_.SetBuildGrid(true);
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x000ACFDC File Offset: 0x000AB1DC
	public void Close_DesignRoom()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				array[i].GetComponent<roomScript>().SetListGameObjectsLayer(0);
			}
		}
		this.sfx_.PlaySound(3, true);
		this.buildRoomScript_.SetInactive();
		this.buildRoomScript_.modus = 0;
		base.StartCoroutine(this.mS_.UpdatePathfindingNextFrame());
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.mS_.SetBuildGrid(false);
	}

	// Token: 0x040014BD RID: 5309
	public GameObject[] uiObjects;

	// Token: 0x040014BE RID: 5310
	public GameObject[] uiPrefabs;

	// Token: 0x040014BF RID: 5311
	public Color[] colors;

	// Token: 0x040014C0 RID: 5312
	private GameObject main_;

	// Token: 0x040014C1 RID: 5313
	private mainScript mS_;

	// Token: 0x040014C2 RID: 5314
	private textScript tS_;

	// Token: 0x040014C3 RID: 5315
	private mapScript mapS_;

	// Token: 0x040014C4 RID: 5316
	private unlockScript unlock_;

	// Token: 0x040014C5 RID: 5317
	private GUI_Main guiMain_;

	// Token: 0x040014C6 RID: 5318
	private buildRoomScript buildRoomScript_;

	// Token: 0x040014C7 RID: 5319
	private roomDataScript rdS_;

	// Token: 0x040014C8 RID: 5320
	private mainCameraScript mCamS_;

	// Token: 0x040014C9 RID: 5321
	private sfxScript sfx_;

	// Token: 0x040014CA RID: 5322
	public int roomTyp;
}
