using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A7 RID: 423
public class Menu_Marketing_KonsoleKampagne : MonoBehaviour
{
	// Token: 0x06000FE4 RID: 4068 RVA: 0x0000B44F File Offset: 0x0000964F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x000B5DBC File Offset: 0x000B3FBC
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

	// Token: 0x06000FE6 RID: 4070 RVA: 0x000B5EA8 File Offset: 0x000B40A8
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

	// Token: 0x06000FE7 RID: 4071 RVA: 0x000B5EFC File Offset: 0x000B40FC
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

	// Token: 0x06000FE8 RID: 4072 RVA: 0x000B5F48 File Offset: 0x000B4148
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

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000B63F0 File Offset: 0x000B45F0
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

	// Token: 0x06000FEA RID: 4074 RVA: 0x0000B457 File Offset: 0x00009657
	public void SetKonsole(platformScript script_)
	{
		this.selectedKonsole = script_;
		this.SetData();
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x000B6538 File Offset: 0x000B4738
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

	// Token: 0x06000FEC RID: 4076 RVA: 0x000B66CC File Offset: 0x000B48CC
	public platformScript FindKonsole()
	{
		if (this.selectedKonsole && this.selectedKonsole.playerConsole && !this.selectedKonsole.vomMarktGenommen)
		{
			return this.selectedKonsole;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.playerConsole && !component.vomMarktGenommen)
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0000B466 File Offset: 0x00009666
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0000B481 File Offset: 0x00009681
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x000B6750 File Offset: 0x000B4950
	public void BUTTON_SelectKonsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[322]);
		this.guiMain_.uiObjects[322].GetComponent<Menu_Marketing_SelectKonsole>().Init();
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x000B67A4 File Offset: 0x000B49A4
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

	// Token: 0x06000FF1 RID: 4081 RVA: 0x000B6934 File Offset: 0x000B4B34
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

	// Token: 0x0400146F RID: 5231
	public GameObject[] uiObjects;

	// Token: 0x04001470 RID: 5232
	public int[] preise;

	// Token: 0x04001471 RID: 5233
	public int[] maxHype;

	// Token: 0x04001472 RID: 5234
	public int[] workPoints;

	// Token: 0x04001473 RID: 5235
	public int[] hypeProKampagne;

	// Token: 0x04001474 RID: 5236
	public Sprite[] sprites;

	// Token: 0x04001475 RID: 5237
	private GameObject main_;

	// Token: 0x04001476 RID: 5238
	private mainScript mS_;

	// Token: 0x04001477 RID: 5239
	private textScript tS_;

	// Token: 0x04001478 RID: 5240
	private unlockScript unlock_;

	// Token: 0x04001479 RID: 5241
	private GUI_Main guiMain_;

	// Token: 0x0400147A RID: 5242
	private sfxScript sfx_;

	// Token: 0x0400147B RID: 5243
	private cameraMovementScript cmS_;

	// Token: 0x0400147C RID: 5244
	private roomScript rS_;

	// Token: 0x0400147D RID: 5245
	private int selectedKampagne = -1;

	// Token: 0x0400147E RID: 5246
	private platformScript selectedKonsole;

	// Token: 0x0400147F RID: 5247
	private float updateTimer;
}
