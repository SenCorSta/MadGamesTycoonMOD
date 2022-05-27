using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E6 RID: 486
public class Menu_PersonalView : MonoBehaviour
{
	// Token: 0x0600124F RID: 4687 RVA: 0x0000CB53 File Offset: 0x0000AD53
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x0000CB5B File Offset: 0x0000AD5B
	private void Update()
	{
		if (!this.cS_)
		{
			this.BUTTON_Close();
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000CDC4C File Offset: 0x000CBE4C
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

	// Token: 0x06001252 RID: 4690 RVA: 0x000CDC98 File Offset: 0x000CBE98
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
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x000CDDA0 File Offset: 0x000CBFA0
	public void Init(characterScript charScript_)
	{
		this.FindScripts();
		this.cS_ = charScript_;
		this.mS_.CreateFoto(this.cS_, null);
		this.uiObjects[0].GetComponent<InputField>().text = this.cS_.myName;
		this.SetData();
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x000CDDF0 File Offset: 0x000CBFF0
	private void SetData()
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[17].GetComponent<Text>().text = this.tS_.GetText(137 + this.cS_.beruf);
		this.SetBalken(this.uiObjects[1], this.cS_.s_gamedesign, 0);
		this.SetBalken(this.uiObjects[2], this.cS_.s_programmieren, 1);
		this.SetBalken(this.uiObjects[3], this.cS_.s_grafik, 2);
		this.SetBalken(this.uiObjects[4], this.cS_.s_sound, 3);
		this.SetBalken(this.uiObjects[5], this.cS_.s_pr, 4);
		this.SetBalken(this.uiObjects[6], this.cS_.s_gametests, 5);
		this.SetBalken(this.uiObjects[7], this.cS_.s_technik, 6);
		this.SetBalken(this.uiObjects[8], this.cS_.s_forschen, 7);
		this.guiMain_.CreatePerkIcons(this.cS_, this.uiObjects[9].transform);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cS_.GetGehalt(), true);
		this.uiObjects[11].GetComponent<Image>().fillAmount = this.cS_.s_motivation * 0.01f;
		this.uiObjects[11].GetComponent<Image>().color = this.GetValColor(this.cS_.s_motivation);
		this.uiObjects[12].GetComponent<Text>().text = Mathf.RoundToInt(this.cS_.s_motivation).ToString();
		this.uiObjects[13].GetComponent<Text>().text = this.cS_.GetGroupString("magenta");
		this.uiObjects[19].SetActive(false);
		this.uiObjects[20].SetActive(false);
		this.uiObjects[21].SetActive(false);
		this.uiObjects[22].SetActive(false);
		this.uiObjects[23].SetActive(false);
		if (this.cS_.krank > 0)
		{
			this.uiObjects[19].SetActive(true);
		}
		int num = Mathf.RoundToInt(this.cS_.transform.position.x);
		int num2 = Mathf.RoundToInt(this.cS_.transform.position.z);
		if (this.mapS_.IsInMapLimit(num, num2))
		{
			if (!this.cS_.perks[16])
			{
				if (this.mapS_.mapMuell[num, num2] > 0f)
				{
					this.uiObjects[22].SetActive(true);
				}
				else
				{
					this.uiObjects[22].SetActive(false);
				}
			}
			if (this.mapS_.mapRoomID[num, num2] != 0)
			{
				if (!this.cS_.perks[11])
				{
					if (this.mapS_.mapWaerme[num, num2] <= 0.2f)
					{
						this.uiObjects[21].SetActive(true);
					}
					else
					{
						this.uiObjects[21].SetActive(false);
					}
				}
				if (!this.cS_.perks[12])
				{
					if (this.mapS_.mapAusstattung[num, num2] <= 0.2f)
					{
						this.uiObjects[23].SetActive(true);
					}
					else
					{
						this.uiObjects[23].SetActive(false);
					}
				}
				if (!this.cS_.perks[17])
				{
					if (this.cS_.IsUeberfuellt())
					{
						this.uiObjects[20].SetActive(true);
					}
					else
					{
						this.uiObjects[20].SetActive(false);
					}
				}
			}
		}
		this.uiObjects[14].SetActive(true);
		this.uiObjects[15].SetActive(true);
		this.uiObjects[16].SetActive(false);
		this.uiObjects[18].SetActive(false);
		if (this.cS_.perks[0])
		{
			this.uiObjects[14].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[14].GetComponent<Button>().interactable = true;
		}
		if (this.guiMain_.uiObjects[196].activeSelf)
		{
			this.uiObjects[14].SetActive(false);
			this.uiObjects[15].SetActive(false);
			this.uiObjects[16].SetActive(true);
			return;
		}
		if (this.guiMain_.uiObjects[324].activeSelf)
		{
			this.uiObjects[14].SetActive(false);
			this.uiObjects[15].SetActive(false);
			this.uiObjects[18].SetActive(true);
			return;
		}
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x000CE2CC File Offset: 0x000CC4CC
	public void SetBalken(GameObject go, float val, int beruf_)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
		if (this.cS_.beruf == beruf_)
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 1f;
			return;
		}
		if (this.cS_.perks[15])
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.6f;
			return;
		}
		go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.5f;
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x000CE3C8 File Offset: 0x000CC5C8
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x0000CB76 File Offset: 0x0000AD76
	public void INPUTFIELD_Name()
	{
		if (this.cS_)
		{
			this.cS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x0000CBA2 File Offset: 0x0000ADA2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x000CE43C File Offset: 0x000CC63C
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
		if (this.mS_.multiplayer)
		{
			this.uiObjects[0].GetComponent<InputField>().interactable = !this.mS_.multiplayer;
		}
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x0000CBBD File Offset: 0x0000ADBD
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000CE488 File Offset: 0x000CC688
	public void BUTTON_Select()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		if (this.guiMain_.uiObjects[26].activeSelf)
		{
			this.guiMain_.uiObjects[26].SetActive(false);
		}
		if (this.guiMain_.uiObjects[29].activeSelf)
		{
			this.guiMain_.uiObjects[29].SetActive(false);
		}
		this.pcS_.PickFromExternObject(this.cS_.gameObject);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000CE524 File Offset: 0x000CC724
	public void BUTTON_Entlassen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[27]);
		this.guiMain_.uiObjects[27].GetComponent<Menu_PersonalEntlassen>().AddCharacter(this.cS_);
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x000CE578 File Offset: 0x000CC778
	public void BUTTON_LeitenderDesigner()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[73].activeSelf)
		{
			this.guiMain_.uiObjects[73].GetComponent<Menu_Dev_GameEntwicklungsbericht>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x000CE700 File Offset: 0x000CC900
	public void BUTTON_LeitenderTechniker()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[318].activeSelf)
		{
			this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().SetLeitenderTechniker(this.cS_, true);
			this.guiMain_.uiObjects[324].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[325].activeSelf)
		{
			this.guiMain_.uiObjects[325].GetComponent<Menu_Dev_KonsoleEntwicklungsbericht>().SetLeitenderTechniker(this.cS_, true);
			this.guiMain_.uiObjects[324].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
	}

	// Token: 0x040016CC RID: 5836
	private mainScript mS_;

	// Token: 0x040016CD RID: 5837
	private GameObject main_;

	// Token: 0x040016CE RID: 5838
	private GUI_Main guiMain_;

	// Token: 0x040016CF RID: 5839
	private sfxScript sfx_;

	// Token: 0x040016D0 RID: 5840
	private textScript tS_;

	// Token: 0x040016D1 RID: 5841
	private pickCharacterScript pcS_;

	// Token: 0x040016D2 RID: 5842
	private characterScript cS_;

	// Token: 0x040016D3 RID: 5843
	private cameraMovementScript cmS_;

	// Token: 0x040016D4 RID: 5844
	private mapScript mapS_;

	// Token: 0x040016D5 RID: 5845
	public GameObject[] uiObjects;

	// Token: 0x040016D6 RID: 5846
	private float updateTimer;
}
