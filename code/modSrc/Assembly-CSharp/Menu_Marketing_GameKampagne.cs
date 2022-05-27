using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A7 RID: 423
public class Menu_Marketing_GameKampagne : MonoBehaviour
{
	// Token: 0x06000FED RID: 4077 RVA: 0x000A8AB7 File Offset: 0x000A6CB7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x000A8AC0 File Offset: 0x000A6CC0
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

	// Token: 0x06000FEF RID: 4079 RVA: 0x000A8BAC File Offset: 0x000A6DAC
	private void Update()
	{
		if (this.selectedKampagne == -1 || this.selectedGame == null)
		{
			this.uiObjects[25].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[25].GetComponent<Button>().interactable = true;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x000A8C00 File Offset: 0x000A6E00
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

	// Token: 0x06000FF1 RID: 4081 RVA: 0x000A8C4C File Offset: 0x000A6E4C
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.selectedKampagne = -1;
		this.selectedGame = this.FindGame();
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

	// Token: 0x06000FF2 RID: 4082 RVA: 0x000A90F4 File Offset: 0x000A72F4
	private void SetData()
	{
		if (this.selectedGame)
		{
			this.uiObjects[22].GetComponent<Text>().text = this.selectedGame.GetNameWithTag();
			this.uiObjects[24].GetComponent<Text>().text = Mathf.RoundToInt(this.selectedGame.GetHype()).ToString();
			this.uiObjects[26].GetComponent<Image>().sprite = this.selectedGame.GetTypSprite();
			this.uiObjects[23].GetComponent<Text>().text = this.selectedGame.GetReleaseDateString();
			return;
		}
		this.uiObjects[22].GetComponent<Text>().text = this.tS_.GetText(611);
		this.uiObjects[24].GetComponent<Text>().text = "-";
		this.uiObjects[23].GetComponent<Text>().text = "---";
		this.uiObjects[26].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x000A920A File Offset: 0x000A740A
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.SetData();
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000A921C File Offset: 0x000A741C
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

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000A93B0 File Offset: 0x000A75B0
	public gameScript FindGame()
	{
		if (this.selectedGame && (this.selectedGame.developerID == this.mS_.myID || this.selectedGame.publisherID == this.mS_.myID) && (this.selectedGame.isOnMarket || this.selectedGame.inDevelopment) && !this.selectedGame.typ_contractGame)
		{
			return this.selectedGame;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && (component.developerID == this.mS_.myID || component.publisherID == this.mS_.myID) && (component.isOnMarket || component.inDevelopment) && !component.typ_contractGame)
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x000A949D File Offset: 0x000A769D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000A94B8 File Offset: 0x000A76B8
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x000A94D5 File Offset: 0x000A76D5
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[90]);
		this.guiMain_.uiObjects[90].GetComponent<Menu_Marketing_SelectGame>().Init();
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x000A9518 File Offset: 0x000A7718
	public void BUTTON_OK()
	{
		if (!this.selectedGame)
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
		taskMarketing.targetID = this.selectedGame.myID;
		taskMarketing.typ = 0;
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

	// Token: 0x06000FFA RID: 4090 RVA: 0x000A96A8 File Offset: 0x000A78A8
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

	// Token: 0x04001467 RID: 5223
	public GameObject[] uiObjects;

	// Token: 0x04001468 RID: 5224
	public int[] preise;

	// Token: 0x04001469 RID: 5225
	public int[] maxHype;

	// Token: 0x0400146A RID: 5226
	public int[] workPoints;

	// Token: 0x0400146B RID: 5227
	public int[] hypeProKampagne;

	// Token: 0x0400146C RID: 5228
	public Sprite[] sprites;

	// Token: 0x0400146D RID: 5229
	private GameObject main_;

	// Token: 0x0400146E RID: 5230
	private mainScript mS_;

	// Token: 0x0400146F RID: 5231
	private textScript tS_;

	// Token: 0x04001470 RID: 5232
	private unlockScript unlock_;

	// Token: 0x04001471 RID: 5233
	private GUI_Main guiMain_;

	// Token: 0x04001472 RID: 5234
	private sfxScript sfx_;

	// Token: 0x04001473 RID: 5235
	private cameraMovementScript cmS_;

	// Token: 0x04001474 RID: 5236
	private roomScript rS_;

	// Token: 0x04001475 RID: 5237
	private int selectedKampagne = -1;

	// Token: 0x04001476 RID: 5238
	private gameScript selectedGame;

	// Token: 0x04001477 RID: 5239
	private float updateTimer;
}
