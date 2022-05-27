using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A6 RID: 422
public class Menu_MarketingSpezial : MonoBehaviour
{
	// Token: 0x06000FDE RID: 4062 RVA: 0x000A7F8A File Offset: 0x000A618A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x000A7F94 File Offset: 0x000A6194
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

	// Token: 0x06000FE0 RID: 4064 RVA: 0x000A8080 File Offset: 0x000A6280
	private void Update()
	{
		if (this.selectedKampagne == -1 || this.selectedGame == null)
		{
			this.uiObjects[16].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[16].GetComponent<Button>().interactable = true;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000A80D4 File Offset: 0x000A62D4
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

	// Token: 0x06000FE2 RID: 4066 RVA: 0x000A8120 File Offset: 0x000A6320
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		this.selectedKampagne = -1;
		this.selectedGame = this.FindGame();
		this.SetButtonColor(-1);
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[0], true);
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[1], true);
		this.uiObjects[14].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[2], true);
		this.uiObjects[15].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[3], true);
		this.uiObjects[20].GetComponent<Text>().text = this.mS_.GetMoney((long)this.preise[4], true);
		this.SetData();
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x000A8224 File Offset: 0x000A6424
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Button>().interactable = true;
		this.uiObjects[4].SetActive(false);
		this.uiObjects[1].GetComponent<Button>().interactable = true;
		this.uiObjects[5].SetActive(false);
		this.uiObjects[2].GetComponent<Button>().interactable = true;
		this.uiObjects[6].SetActive(false);
		this.uiObjects[3].GetComponent<Button>().interactable = true;
		this.uiObjects[7].SetActive(false);
		this.uiObjects[18].GetComponent<Button>().interactable = true;
		this.uiObjects[19].SetActive(false);
		if (this.selectedGame)
		{
			this.uiObjects[9].GetComponent<Text>().text = this.selectedGame.GetNameWithTag();
			this.uiObjects[11].GetComponent<Image>().sprite = this.selectedGame.GetTypSprite();
			this.uiObjects[8].GetComponent<tooltip>().c = this.selectedGame.GetTooltip();
			this.uiObjects[17].GetComponent<Text>().text = Mathf.RoundToInt(this.selectedGame.GetHype()).ToString();
			this.uiObjects[10].GetComponent<Text>().text = this.selectedGame.GetReleaseDateString();
			if (this.selectedGame.specialMarketing[0] != 0 || (!this.selectedGame.inDevelopment && !this.selectedGame.schublade) || this.WirdInAnderenRaumBearbeitet(0))
			{
				this.uiObjects[0].GetComponent<Button>().interactable = false;
				if (this.selectedGame.specialMarketing[0] != 0)
				{
					this.uiObjects[4].SetActive(true);
				}
			}
			if (this.selectedGame.specialMarketing[1] != 0 || (!this.selectedGame.inDevelopment && !this.selectedGame.schublade) || this.WirdInAnderenRaumBearbeitet(1))
			{
				this.uiObjects[1].GetComponent<Button>().interactable = false;
				if (this.selectedGame.specialMarketing[1] != 0)
				{
					this.uiObjects[5].SetActive(true);
				}
			}
			if (this.selectedGame.specialMarketing[2] != 0 || (!this.selectedGame.inDevelopment && !this.selectedGame.schublade) || this.selectedGame.hype < 90f || this.WirdInAnderenRaumBearbeitet(2))
			{
				this.uiObjects[2].GetComponent<Button>().interactable = false;
				if (this.selectedGame.specialMarketing[2] != 0)
				{
					this.uiObjects[6].SetActive(true);
				}
			}
			if (this.selectedGame.specialMarketing[3] != 0 || this.selectedGame.inDevelopment || this.selectedGame.schublade || this.WirdInAnderenRaumBearbeitet(3))
			{
				this.uiObjects[3].GetComponent<Button>().interactable = false;
				if (this.selectedGame.specialMarketing[3] != 0)
				{
					this.uiObjects[7].SetActive(true);
				}
			}
			if (this.selectedGame.specialMarketing[4] != 0 || this.selectedGame.inDevelopment || this.selectedGame.schublade || this.WirdInAnderenRaumBearbeitet(4))
			{
				this.uiObjects[18].GetComponent<Button>().interactable = false;
				if (this.selectedGame.specialMarketing[4] != 0)
				{
					this.uiObjects[19].SetActive(true);
					return;
				}
			}
		}
		else
		{
			this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(611);
			this.uiObjects[10].GetComponent<Text>().text = "---";
			this.uiObjects[17].GetComponent<Text>().text = "-";
			this.uiObjects[11].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		}
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x000A8602 File Offset: 0x000A6802
	public void SetGame(gameScript script_)
	{
		this.selectedKampagne = -1;
		this.SetButtonColor(-1);
		this.selectedGame = script_;
		this.SetData();
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x000A8620 File Offset: 0x000A6820
	private void SetButtonColor(int i)
	{
		this.uiObjects[0].GetComponent<Image>().color = Color.white;
		this.uiObjects[1].GetComponent<Image>().color = Color.white;
		this.uiObjects[2].GetComponent<Image>().color = Color.white;
		this.uiObjects[3].GetComponent<Image>().color = Color.white;
		this.uiObjects[18].GetComponent<Image>().color = Color.white;
		if (i != -1)
		{
			switch (i)
			{
			case 0:
				this.uiObjects[0].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 1:
				this.uiObjects[1].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 2:
				this.uiObjects[2].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 3:
				this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colors[7];
				return;
			case 4:
				this.uiObjects[18].GetComponent<Image>().color = this.guiMain_.colors[7];
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x000A8778 File Offset: 0x000A6978
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

	// Token: 0x06000FE7 RID: 4071 RVA: 0x000A8865 File Offset: 0x000A6A65
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x000A888B File Offset: 0x000A6A8B
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000A88A8 File Offset: 0x000A6AA8
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[295]);
		this.guiMain_.uiObjects[295].GetComponent<Menu_Marketing_SpezialSelectGame>().Init();
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000A88FC File Offset: 0x000A6AFC
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
		if (this.selectedGame.specialMarketing[this.selectedKampagne] != 0)
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
		taskMarketingSpezial taskMarketingSpezial = this.guiMain_.AddTask_MarketingSpezial();
		taskMarketingSpezial.Init(false);
		taskMarketingSpezial.targetID = this.selectedGame.myID;
		taskMarketingSpezial.kampagne = this.selectedKampagne;
		taskMarketingSpezial.points = (float)this.workPoints[this.selectedKampagne];
		taskMarketingSpezial.pointsLeft = (float)this.workPoints[this.selectedKampagne];
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskMarketingSpezial.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x000A8A3C File Offset: 0x000A6C3C
	private bool WirdInAnderenRaumBearbeitet(int kampagne_)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				taskMarketingSpezial component = array[i].GetComponent<taskMarketingSpezial>();
				if (component && component.targetID == this.selectedGame.myID && component.kampagne == kampagne_)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04001458 RID: 5208
	public GameObject[] uiObjects;

	// Token: 0x04001459 RID: 5209
	public int[] preise;

	// Token: 0x0400145A RID: 5210
	public int[] workPoints;

	// Token: 0x0400145B RID: 5211
	public Sprite[] sprites;

	// Token: 0x0400145C RID: 5212
	private GameObject main_;

	// Token: 0x0400145D RID: 5213
	private mainScript mS_;

	// Token: 0x0400145E RID: 5214
	private textScript tS_;

	// Token: 0x0400145F RID: 5215
	private unlockScript unlock_;

	// Token: 0x04001460 RID: 5216
	private GUI_Main guiMain_;

	// Token: 0x04001461 RID: 5217
	private sfxScript sfx_;

	// Token: 0x04001462 RID: 5218
	private cameraMovementScript cmS_;

	// Token: 0x04001463 RID: 5219
	private roomScript rS_;

	// Token: 0x04001464 RID: 5220
	private int selectedKampagne = -1;

	// Token: 0x04001465 RID: 5221
	private gameScript selectedGame;

	// Token: 0x04001466 RID: 5222
	private float updateTimer;
}
