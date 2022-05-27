using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000159 RID: 345
public class Menu_GFX_GrafikVerbessern : MonoBehaviour
{
	// Token: 0x06000CA9 RID: 3241 RVA: 0x00008DC9 File Offset: 0x00006FC9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0009AA84 File Offset: 0x00098C84
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

	// Token: 0x06000CAB RID: 3243 RVA: 0x00008DD1 File Offset: 0x00006FD1
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x0009AC24 File Offset: 0x00098E24
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

	// Token: 0x06000CAD RID: 3245 RVA: 0x00008DF2 File Offset: 0x00006FF2
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x0009AC70 File Offset: 0x00098E70
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

	// Token: 0x06000CAF RID: 3247 RVA: 0x0009AD48 File Offset: 0x00098F48
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

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0009AD98 File Offset: 0x00098F98
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

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00008E19 File Offset: 0x00007019
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0009B084 File Offset: 0x00099284
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[175]);
		this.guiMain_.uiObjects[175].GetComponent<Menu_GFX_GrafikVerbessernSelectGame>().Init();
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x0009B0D8 File Offset: 0x000992D8
	public gameScript FindGame()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && !component.isOnMarket && component.inDevelopment)
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x0009B134 File Offset: 0x00099334
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

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0009B170 File Offset: 0x00099370
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

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00008E2E File Offset: 0x0000702E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00008E54 File Offset: 0x00007054
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0009B1D4 File Offset: 0x000993D4
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

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0009B238 File Offset: 0x00099438
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

	// Token: 0x04001138 RID: 4408
	public GameObject[] uiObjects;

	// Token: 0x04001139 RID: 4409
	public int[] costs;

	// Token: 0x0400113A RID: 4410
	public float[] points;

	// Token: 0x0400113B RID: 4411
	public float[] pointsInPercent;

	// Token: 0x0400113C RID: 4412
	private bool[] buttonAdds = new bool[6];

	// Token: 0x0400113D RID: 4413
	private GameObject main_;

	// Token: 0x0400113E RID: 4414
	private mainScript mS_;

	// Token: 0x0400113F RID: 4415
	private textScript tS_;

	// Token: 0x04001140 RID: 4416
	private GUI_Main guiMain_;

	// Token: 0x04001141 RID: 4417
	private sfxScript sfx_;

	// Token: 0x04001142 RID: 4418
	private genres genres_;

	// Token: 0x04001143 RID: 4419
	private themes themes_;

	// Token: 0x04001144 RID: 4420
	private licences licences_;

	// Token: 0x04001145 RID: 4421
	private engineFeatures eF_;

	// Token: 0x04001146 RID: 4422
	private cameraMovementScript cmS_;

	// Token: 0x04001147 RID: 4423
	private unlockScript unlock_;

	// Token: 0x04001148 RID: 4424
	private gameplayFeatures gF_;

	// Token: 0x04001149 RID: 4425
	private games games_;

	// Token: 0x0400114A RID: 4426
	private gameScript selectedGame;

	// Token: 0x0400114B RID: 4427
	private roomScript rS_;

	// Token: 0x0400114C RID: 4428
	private float updateTimer;

	// Token: 0x0400114D RID: 4429
	private bool allAdds;
}
