using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Marketing_GameKampagne : MonoBehaviour
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
			this.uiObjects[25].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[25].GetComponent<Button>().interactable = true;
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

	
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.SetData();
	}

	
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
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[90]);
		this.guiMain_.uiObjects[90].GetComponent<Menu_Marketing_SelectGame>().Init();
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

	
	public GameObject[] uiObjects;

	
	public int[] preise;

	
	public int[] maxHype;

	
	public int[] workPoints;

	
	public int[] hypeProKampagne;

	
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
