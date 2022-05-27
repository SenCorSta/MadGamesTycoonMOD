using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A8 RID: 424
public class Menu_Marketing_KonsoleKampagne : MonoBehaviour
{
	// Token: 0x06000FFC RID: 4092 RVA: 0x000A9752 File Offset: 0x000A7952
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000A975C File Offset: 0x000A795C
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

	// Token: 0x06000FFE RID: 4094 RVA: 0x000A9848 File Offset: 0x000A7A48
	private void Update()
	{
		if (this.selectedKampagne == -1 || this.selectedKonsole == null)
		{
			this.uiObjects[25].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[25].GetComponent<Button>().interactable = true;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x000A989C File Offset: 0x000A7A9C
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

	// Token: 0x06001000 RID: 4096 RVA: 0x000A98E8 File Offset: 0x000A7AE8
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.selectedKampagne = -1;
		this.selectedKonsole = this.FindKonsole();
		if (this.unlock_.Get(56))
		{
			this.uiObjects[0].SetActive(false);
			this.uiObjects[5].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[0].SetActive(true);
			this.uiObjects[5].GetComponent<Button>().interactable = false;
		}
		if (this.unlock_.Get(57))
		{
			this.uiObjects[1].SetActive(false);
			this.uiObjects[6].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[1].SetActive(true);
			this.uiObjects[6].GetComponent<Button>().interactable = false;
		}
		this.SetButtonColor(-1);
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[0], true);
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[1], true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[2], true);
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[3], true);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[4], true);
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[5], true);
		this.uiObjects[16].GetComponent<Text>().text = string.Concat(new string[]
		{
			"+",
			this.hypeProKampagne[0].ToString(),
			" (",
			this.tS_.GetText(661),
			" ",
			this.maxHype[0].ToString(),
			")"
		});
		this.uiObjects[17].GetComponent<Text>().text = string.Concat(new string[]
		{
			"+",
			this.hypeProKampagne[1].ToString(),
			" (",
			this.tS_.GetText(661),
			" ",
			this.maxHype[1].ToString(),
			")"
		});
		this.uiObjects[18].GetComponent<Text>().text = string.Concat(new string[]
		{
			"+",
			this.hypeProKampagne[2].ToString(),
			" (",
			this.tS_.GetText(661),
			" ",
			this.maxHype[2].ToString(),
			")"
		});
		this.uiObjects[19].GetComponent<Text>().text = string.Concat(new string[]
		{
			"+",
			this.hypeProKampagne[3].ToString(),
			" (",
			this.tS_.GetText(661),
			" ",
			this.maxHype[3].ToString(),
			")"
		});
		this.uiObjects[20].GetComponent<Text>().text = string.Concat(new string[]
		{
			"+",
			this.hypeProKampagne[4].ToString(),
			" (",
			this.tS_.GetText(661),
			" ",
			this.maxHype[4].ToString(),
			")"
		});
		this.uiObjects[21].GetComponent<Text>().text = string.Concat(new string[]
		{
			"+",
			this.hypeProKampagne[5].ToString(),
			" (",
			this.tS_.GetText(661),
			" ",
			this.maxHype[5].ToString(),
			")"
		});
		this.SetData();
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x000A9D90 File Offset: 0x000A7F90
	private void SetData()
	{
		if (!this.selectedKonsole)
		{
			this.uiObjects[22].GetComponent<Text>().text = this.tS_.GetText(949);
			this.uiObjects[24].GetComponent<Text>().text = "-";
			this.uiObjects[23].GetComponent<Text>().text = "---";
			this.uiObjects[26].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			return;
		}
		this.uiObjects[22].GetComponent<Text>().text = this.selectedKonsole.myName;
		this.uiObjects[24].GetComponent<Text>().text = Mathf.RoundToInt(this.selectedKonsole.GetHype()).ToString();
		this.uiObjects[26].GetComponent<Image>().sprite = this.selectedKonsole.GetTypSprite();
		if (this.selectedKonsole.isUnlocked)
		{
			this.uiObjects[23].GetComponent<Text>().text = this.selectedKonsole.GetDateString();
			return;
		}
		this.uiObjects[23].GetComponent<Text>().text = this.tS_.GetText(528);
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x000A9ED7 File Offset: 0x000A80D7
	public void SetKonsole(platformScript script_)
	{
		this.selectedKonsole = script_;
		this.SetData();
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x000A9EE8 File Offset: 0x000A80E8
	private void SetButtonColor(int i)
	{
		this.uiObjects[2].GetComponent<Image>().color = Color.white;
		this.uiObjects[3].GetComponent<Image>().color = Color.white;
		this.uiObjects[4].GetComponent<Image>().color = Color.white;
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
		this.uiObjects[6].GetComponent<Image>().color = Color.white;
		this.uiObjects[7].GetComponent<Image>().color = Color.white;
		if (i != -1)
		{
			switch (i)
			{
			case 0:
				this.uiObjects[2].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 1:
				this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 2:
				this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 3:
				this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 4:
				this.uiObjects[6].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 5:
				this.uiObjects[7].GetComponent<Image>().color = this.guiMain_.colors[7];
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x000AA07C File Offset: 0x000A827C
	public platformScript FindKonsole()
	{
		if (this.selectedKonsole && this.selectedKonsole.ownerID == this.mS_.myID && !this.selectedKonsole.vomMarktGenommen)
		{
			return this.selectedKonsole;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.ownerID == this.mS_.myID && !component.vomMarktGenommen)
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x000AA114 File Offset: 0x000A8314
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x000AA12F File Offset: 0x000A832F
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x000AA14C File Offset: 0x000A834C
	public void BUTTON_SelectKonsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[322]);
		this.guiMain_.uiObjects[322].GetComponent<Menu_Marketing_SelectKonsole>().Init();
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x000AA1A0 File Offset: 0x000A83A0
	public void BUTTON_OK()
	{
		if (!this.selectedKonsole)
		{
			return;
		}
		if (this.selectedKampagne == -1)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (this.mS_.NotEnoughMoney(this.preise[this.selectedKampagne]))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.preise[this.selectedKampagne], 12);
		taskMarketing taskMarketing = this.guiMain_.AddTask_Marketing();
		taskMarketing.Init(false);
		taskMarketing.targetID = this.selectedKonsole.myID;
		taskMarketing.typ = 1;
		taskMarketing.kampagne = this.selectedKampagne;
		taskMarketing.automatic = this.uiObjects[14].GetComponent<Toggle>().isOn;
		taskMarketing.stopAutomatic = this.uiObjects[15].GetComponent<Toggle>().isOn;
		taskMarketing.disableWarten = this.uiObjects[27].GetComponent<Toggle>().isOn;
		taskMarketing.points = (float)this.workPoints[this.selectedKampagne];
		taskMarketing.pointsLeft = (float)this.workPoints[this.selectedKampagne];
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskMarketing.myID;
		}
		this.guiMain_.CloseMenu();
		this.guiMain_.uiObjects[88].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000AA330 File Offset: 0x000A8530
	public void TOGGLE_Auto()
	{
		if (!this.uiObjects[14].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[15].GetComponent<Toggle>().interactable = false;
			this.uiObjects[15].GetComponent<Toggle>().isOn = false;
			this.uiObjects[27].GetComponent<Toggle>().interactable = false;
			this.uiObjects[27].GetComponent<Toggle>().isOn = false;
			return;
		}
		this.uiObjects[15].GetComponent<Toggle>().interactable = true;
		this.uiObjects[27].GetComponent<Toggle>().interactable = true;
	}

	// Token: 0x04001478 RID: 5240
	public GameObject[] uiObjects;

	// Token: 0x04001479 RID: 5241
	public int[] preise;

	// Token: 0x0400147A RID: 5242
	public int[] maxHype;

	// Token: 0x0400147B RID: 5243
	public int[] workPoints;

	// Token: 0x0400147C RID: 5244
	public int[] hypeProKampagne;

	// Token: 0x0400147D RID: 5245
	public Sprite[] sprites;

	// Token: 0x0400147E RID: 5246
	private GameObject main_;

	// Token: 0x0400147F RID: 5247
	private mainScript mS_;

	// Token: 0x04001480 RID: 5248
	private textScript tS_;

	// Token: 0x04001481 RID: 5249
	private unlockScript unlock_;

	// Token: 0x04001482 RID: 5250
	private GUI_Main guiMain_;

	// Token: 0x04001483 RID: 5251
	private sfxScript sfx_;

	// Token: 0x04001484 RID: 5252
	private cameraMovementScript cmS_;

	// Token: 0x04001485 RID: 5253
	private roomScript rS_;

	// Token: 0x04001486 RID: 5254
	private int selectedKampagne = -1;

	// Token: 0x04001487 RID: 5255
	private platformScript selectedKonsole;

	// Token: 0x04001488 RID: 5256
	private float updateTimer;
}
