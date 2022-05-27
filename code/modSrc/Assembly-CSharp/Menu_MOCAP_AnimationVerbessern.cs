using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MOCAP_AnimationVerbessern : MonoBehaviour
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
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
		this.UpdateGUI();
	}

	
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	
	private void DeselectAllButtons()
	{
		this.allAdds = false;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = false;
			this.uiObjects[i].transform.Find("TextPreis").GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetCosts(i), true);
			this.uiObjects[i].GetComponent<Button>().interactable = false;
			this.uiObjects[i].transform.GetChild(4).gameObject.SetActive(false);
			this.uiObjects[i].transform.GetChild(5).gameObject.SetActive(false);
		}
	}

	
	public int GetCosts(int i)
	{
		if (!this.selectedGame)
		{
			return 0;
		}
		int num = this.costs[i] * this.selectedGame.GetGesamtDevPoints();
		num = num / 1000 * 1000;
		if (num < 1000)
		{
			num = 1000;
		}
		return num;
	}

	
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.selectedGame)
			{
				if (this.selectedGame.motionCaptureStudio[i])
				{
					this.uiObjects[i].GetComponent<Button>().interactable = false;
					this.uiObjects[i].transform.GetChild(4).gameObject.SetActive(true);
				}
				else
				{
					this.uiObjects[i].GetComponent<Button>().interactable = true;
					this.uiObjects[i].transform.GetChild(4).gameObject.SetActive(false);
				}
				if (this.WirdInAnderenRaumBearbeitet(i, this.selectedGame))
				{
					this.uiObjects[i].GetComponent<Button>().interactable = false;
					this.uiObjects[i].transform.GetChild(5).gameObject.SetActive(true);
				}
			}
			if (this.buttonAdds[i])
			{
				this.uiObjects[i].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.uiObjects[i].GetComponent<Image>().color = Color.white;
			}
		}
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney(this.GetDevCosts(), true);
		if (this.selectedGame)
		{
			this.uiObjects[7].GetComponent<Text>().text = this.selectedGame.GetNameWithTag();
			this.uiObjects[8].GetComponent<Image>().sprite = this.selectedGame.GetTypSprite();
			this.uiObjects[9].GetComponent<Text>().text = this.selectedGame.GetGenreString();
			this.uiObjects[10].GetComponent<Image>().sprite = this.selectedGame.GetPlatformTypSprite();
			return;
		}
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(611);
		this.uiObjects[9].GetComponent<Text>().text = "---";
		this.uiObjects[8].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		this.uiObjects[10].GetComponent<Image>().sprite = this.games_.gamePlatformTypSprites[0];
	}

	
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[179]);
		this.guiMain_.uiObjects[179].GetComponent<Menu_MOCAP_AnimationVerbessernSelectGame>().Init();
	}

	
	public gameScript FindGame()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && !component.isOnMarket && component.inDevelopment && !this.WirdInAnderenRaumBearbeitet(0, component) && !this.WirdInAnderenRaumBearbeitet(1, component) && !this.WirdInAnderenRaumBearbeitet(2, component) && !this.WirdInAnderenRaumBearbeitet(3, component) && !this.WirdInAnderenRaumBearbeitet(4, component) && !this.WirdInAnderenRaumBearbeitet(5, component))
				{
					return component;
				}
			}
		}
		return null;
	}

	
	private long GetDevCosts()
	{
		long num = 0L;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += (long)this.GetCosts(i);
			}
		}
		return num;
	}

	
	public bool WirdInAnderenRaumBearbeitet(int slot, gameScript g_)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				taskAnimationVerbessern component = array[i].GetComponent<taskAnimationVerbessern>();
				if (component && component.targetID == g_.myID && component.adds[slot])
				{
					return true;
				}
			}
		}
		return false;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Delete(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (i == 0)
		{
			this.buttonAdds[0] = false;
			this.buttonAdds[1] = false;
			this.buttonAdds[2] = false;
		}
		else
		{
			this.buttonAdds[3] = false;
			this.buttonAdds[4] = false;
			this.buttonAdds[5] = false;
		}
		this.UpdateGUI();
	}

	
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
			if (this.uiObjects[0].GetComponent<Button>().interactable)
			{
				this.buttonAdds[0] = true;
			}
			this.buttonAdds[1] = false;
			this.buttonAdds[2] = false;
			break;
		case 1:
			if (this.uiObjects[0].GetComponent<Button>().interactable)
			{
				this.buttonAdds[0] = true;
			}
			if (this.uiObjects[1].GetComponent<Button>().interactable)
			{
				this.buttonAdds[1] = true;
			}
			this.buttonAdds[2] = false;
			break;
		case 2:
			if (this.uiObjects[0].GetComponent<Button>().interactable)
			{
				this.buttonAdds[0] = true;
			}
			if (this.uiObjects[1].GetComponent<Button>().interactable)
			{
				this.buttonAdds[1] = true;
			}
			if (this.uiObjects[2].GetComponent<Button>().interactable)
			{
				this.buttonAdds[2] = true;
			}
			break;
		case 3:
			if (this.uiObjects[3].GetComponent<Button>().interactable)
			{
				this.buttonAdds[3] = true;
			}
			this.buttonAdds[4] = false;
			this.buttonAdds[5] = false;
			break;
		case 4:
			if (this.uiObjects[3].GetComponent<Button>().interactable)
			{
				this.buttonAdds[3] = true;
			}
			if (this.uiObjects[4].GetComponent<Button>().interactable)
			{
				this.buttonAdds[4] = true;
			}
			this.buttonAdds[5] = false;
			break;
		case 5:
			if (this.uiObjects[3].GetComponent<Button>().interactable)
			{
				this.buttonAdds[3] = true;
			}
			if (this.uiObjects[4].GetComponent<Button>().interactable)
			{
				this.buttonAdds[4] = true;
			}
			if (this.uiObjects[5].GetComponent<Button>().interactable)
			{
				this.buttonAdds[5] = true;
			}
			break;
		}
		this.UpdateGUI();
	}

	
	public void BUTTON_AlleAdds()
	{
		this.sfx_.PlaySound(3, true);
		this.allAdds = !this.allAdds;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.uiObjects[i].GetComponent<Button>().interactable)
			{
				this.buttonAdds[i] = this.allAdds;
			}
		}
		this.UpdateGUI();
	}

	
	public void BUTTON_Start()
	{
		int num = Mathf.RoundToInt((float)this.GetDevCosts());
		if (!this.selectedGame)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		if (num <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(914), false);
			return;
		}
		if (this.mS_.NotEnoughMoney(num))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)num, 10);
		taskAnimationVerbessern taskAnimationVerbessern = this.guiMain_.AddTask_AnimationVerbessern();
		taskAnimationVerbessern.Init(false);
		taskAnimationVerbessern.targetID = this.selectedGame.myID;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				taskAnimationVerbessern.adds[i] = true;
			}
		}
		taskAnimationVerbessern.FindNewAdd();
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskAnimationVerbessern.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	public int[] costs;

	
	public float[] pointsInPercent;

	
	private bool[] buttonAdds = new bool[6];

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private gameScript selectedGame;

	
	private roomScript rS_;

	
	private float updateTimer;

	
	private bool allAdds;
}
