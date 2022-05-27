using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000209 RID: 521
public class Menu_QA_GameplayVerbessern : MonoBehaviour
{
	// Token: 0x060013EF RID: 5103 RVA: 0x000D04FC File Offset: 0x000CE6FC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x000D0504 File Offset: 0x000CE704
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

	// Token: 0x060013F1 RID: 5105 RVA: 0x000D06A2 File Offset: 0x000CE8A2
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000D06C4 File Offset: 0x000CE8C4
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

	// Token: 0x060013F3 RID: 5107 RVA: 0x000D0710 File Offset: 0x000CE910
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000D0738 File Offset: 0x000CE938
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

	// Token: 0x060013F5 RID: 5109 RVA: 0x000D07F4 File Offset: 0x000CE9F4
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

	// Token: 0x060013F6 RID: 5110 RVA: 0x000D0844 File Offset: 0x000CEA44
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.selectedGame)
			{
				if (this.selectedGame.gameplayStudio[i])
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

	// Token: 0x060013F7 RID: 5111 RVA: 0x000D0A8E File Offset: 0x000CEC8E
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x000D0AA4 File Offset: 0x000CECA4
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[173]);
		this.guiMain_.uiObjects[173].GetComponent<Menu_QA_GameplayVerbessernSelectGame>().Init();
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000D0AF8 File Offset: 0x000CECF8
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

	// Token: 0x060013FA RID: 5114 RVA: 0x000D0B60 File Offset: 0x000CED60
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

	// Token: 0x060013FB RID: 5115 RVA: 0x000D0B9C File Offset: 0x000CED9C
	private bool WirdInAnderenRaumBearbeitet(int slot)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				taskGameplayVerbessern component = array[i].GetComponent<taskGameplayVerbessern>();
				if (component && component.targetID == this.selectedGame.myID && component.adds[slot])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x000D0BFD File Offset: 0x000CEDFD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x000D0C23 File Offset: 0x000CEE23
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x000D0C4C File Offset: 0x000CEE4C
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

	// Token: 0x060013FF RID: 5119 RVA: 0x000D0CB0 File Offset: 0x000CEEB0
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
		taskGameplayVerbessern taskGameplayVerbessern = this.guiMain_.AddTask_GameplayVerbessern();
		taskGameplayVerbessern.Init(false);
		taskGameplayVerbessern.targetID = this.selectedGame.myID;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				taskGameplayVerbessern.adds[i] = true;
			}
		}
		if (this.uiObjects[11].GetComponent<Toggle>().isOn)
		{
			taskGameplayVerbessern.autoBugfix = true;
		}
		else
		{
			taskGameplayVerbessern.autoBugfix = false;
		}
		taskGameplayVerbessern.FindNewAdd();
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskGameplayVerbessern.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001809 RID: 6153
	public GameObject[] uiObjects;

	// Token: 0x0400180A RID: 6154
	public int[] costs;

	// Token: 0x0400180B RID: 6155
	public float[] points;

	// Token: 0x0400180C RID: 6156
	public float[] pointsInPercent;

	// Token: 0x0400180D RID: 6157
	private bool[] buttonAdds = new bool[6];

	// Token: 0x0400180E RID: 6158
	private GameObject main_;

	// Token: 0x0400180F RID: 6159
	private mainScript mS_;

	// Token: 0x04001810 RID: 6160
	private textScript tS_;

	// Token: 0x04001811 RID: 6161
	private GUI_Main guiMain_;

	// Token: 0x04001812 RID: 6162
	private sfxScript sfx_;

	// Token: 0x04001813 RID: 6163
	private genres genres_;

	// Token: 0x04001814 RID: 6164
	private themes themes_;

	// Token: 0x04001815 RID: 6165
	private licences licences_;

	// Token: 0x04001816 RID: 6166
	private engineFeatures eF_;

	// Token: 0x04001817 RID: 6167
	private cameraMovementScript cmS_;

	// Token: 0x04001818 RID: 6168
	private unlockScript unlock_;

	// Token: 0x04001819 RID: 6169
	private gameplayFeatures gF_;

	// Token: 0x0400181A RID: 6170
	private games games_;

	// Token: 0x0400181B RID: 6171
	private gameScript selectedGame;

	// Token: 0x0400181C RID: 6172
	private roomScript rS_;

	// Token: 0x0400181D RID: 6173
	private float updateTimer;

	// Token: 0x0400181E RID: 6174
	private bool allAdds;
}
