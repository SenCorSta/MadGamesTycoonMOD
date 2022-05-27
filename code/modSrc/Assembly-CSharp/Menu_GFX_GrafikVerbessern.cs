using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015A RID: 346
public class Menu_GFX_GrafikVerbessern : MonoBehaviour
{
	// Token: 0x06000CBF RID: 3263 RVA: 0x0008BAEF File Offset: 0x00089CEF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0008BAF8 File Offset: 0x00089CF8
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

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0008BC96 File Offset: 0x00089E96
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0008BCB8 File Offset: 0x00089EB8
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

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0008BD04 File Offset: 0x00089F04
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0008BD2C File Offset: 0x00089F2C
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
			this.uiObjects[i].transform.GetChild(6).gameObject.SetActive(false);
		}
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0008BE04 File Offset: 0x0008A004
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

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0008BE54 File Offset: 0x0008A054
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.selectedGame)
			{
				if (this.selectedGame.grafikStudio[i])
				{
					this.uiObjects[i].GetComponent<Button>().interactable = false;
					this.uiObjects[i].transform.GetChild(4).gameObject.SetActive(true);
				}
				else
				{
					this.uiObjects[i].GetComponent<Button>().interactable = true;
					this.uiObjects[i].transform.GetChild(4).gameObject.SetActive(false);
				}
				if (this.WirdInAnderenRaumBearbeitet(i))
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
		if (this.selectedGame && !this.selectedGame.gameGameplayFeatures[40])
		{
			this.uiObjects[5].GetComponent<Button>().interactable = false;
			this.uiObjects[5].transform.GetChild(6).gameObject.SetActive(true);
			string text = this.tS_.GetText(919);
			text = text.Replace("<NAME>", this.gF_.GetName(40));
			this.uiObjects[5].transform.GetChild(6).GetChild(0).GetComponent<Text>().text = text;
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

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0008C13D File Offset: 0x0008A33D
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0008C154 File Offset: 0x0008A354
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[175]);
		this.guiMain_.uiObjects[175].GetComponent<Menu_GFX_GrafikVerbessernSelectGame>().Init();
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0008C1A8 File Offset: 0x0008A3A8
	public gameScript FindGame()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.developerID == this.mS_.myID && !component.isOnMarket && component.inDevelopment)
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0008C210 File Offset: 0x0008A410
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

	// Token: 0x06000CCB RID: 3275 RVA: 0x0008C24C File Offset: 0x0008A44C
	private bool WirdInAnderenRaumBearbeitet(int slot)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				taskGrafikVerbessern component = array[i].GetComponent<taskGrafikVerbessern>();
				if (component && component.targetID == this.selectedGame.myID && component.adds[slot])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0008C2AD File Offset: 0x0008A4AD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0008C2D3 File Offset: 0x0008A4D3
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0008C2FC File Offset: 0x0008A4FC
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

	// Token: 0x06000CCF RID: 3279 RVA: 0x0008C360 File Offset: 0x0008A560
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
		taskGrafikVerbessern taskGrafikVerbessern = this.guiMain_.AddTask_GrafikVerbessern();
		taskGrafikVerbessern.Init(false);
		taskGrafikVerbessern.targetID = this.selectedGame.myID;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				taskGrafikVerbessern.adds[i] = true;
			}
		}
		taskGrafikVerbessern.FindNewAdd();
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskGrafikVerbessern.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001140 RID: 4416
	public GameObject[] uiObjects;

	// Token: 0x04001141 RID: 4417
	public int[] costs;

	// Token: 0x04001142 RID: 4418
	public float[] points;

	// Token: 0x04001143 RID: 4419
	public float[] pointsInPercent;

	// Token: 0x04001144 RID: 4420
	private bool[] buttonAdds = new bool[6];

	// Token: 0x04001145 RID: 4421
	private GameObject main_;

	// Token: 0x04001146 RID: 4422
	private mainScript mS_;

	// Token: 0x04001147 RID: 4423
	private textScript tS_;

	// Token: 0x04001148 RID: 4424
	private GUI_Main guiMain_;

	// Token: 0x04001149 RID: 4425
	private sfxScript sfx_;

	// Token: 0x0400114A RID: 4426
	private genres genres_;

	// Token: 0x0400114B RID: 4427
	private themes themes_;

	// Token: 0x0400114C RID: 4428
	private licences licences_;

	// Token: 0x0400114D RID: 4429
	private engineFeatures eF_;

	// Token: 0x0400114E RID: 4430
	private cameraMovementScript cmS_;

	// Token: 0x0400114F RID: 4431
	private unlockScript unlock_;

	// Token: 0x04001150 RID: 4432
	private gameplayFeatures gF_;

	// Token: 0x04001151 RID: 4433
	private games games_;

	// Token: 0x04001152 RID: 4434
	private gameScript selectedGame;

	// Token: 0x04001153 RID: 4435
	private roomScript rS_;

	// Token: 0x04001154 RID: 4436
	private float updateTimer;

	// Token: 0x04001155 RID: 4437
	private bool allAdds;
}
