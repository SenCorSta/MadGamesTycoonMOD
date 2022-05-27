using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MarketingSpezial : MonoBehaviour
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

	
	public void SetGame(gameScript script_)
	{
		this.selectedKampagne = -1;
		this.SetButtonColor(-1);
		this.selectedGame = script_;
		this.SetData();
	}

	
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

	
	public gameScript FindGame()
	{
		if (this.selectedGame && this.selectedGame.playerGame && (this.selectedGame.isOnMarket || this.selectedGame.inDevelopment))
		{
			return this.selectedGame;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && (component.isOnMarket || component.inDevelopment))
				{
					return component;
				}
			}
		}
		return null;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Select(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.selectedKampagne = i;
		this.SetButtonColor(i);
	}

	
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[295]);
		this.guiMain_.uiObjects[295].GetComponent<Menu_Marketing_SpezialSelectGame>().Init();
	}

	
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

	
	public GameObject[] uiObjects;

	
	public int[] preise;

	
	public int[] workPoints;

	
	public Sprite[] sprites;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private unlockScript unlock_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private roomScript rS_;

	
	private int selectedKampagne = -1;

	
	private gameScript selectedGame;

	
	private float updateTimer;
}
