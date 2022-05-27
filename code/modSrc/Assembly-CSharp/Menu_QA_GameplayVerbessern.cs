using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000208 RID: 520
public class Menu_QA_GameplayVerbessern : MonoBehaviour
{
	// Token: 0x060013D3 RID: 5075 RVA: 0x0000D898 File Offset: 0x0000BA98
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000DA350 File Offset: 0x000D8550
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

	// Token: 0x060013D5 RID: 5077 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000DA4F0 File Offset: 0x000D86F0
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

	// Token: 0x060013D7 RID: 5079 RVA: 0x0000D8C1 File Offset: 0x0000BAC1
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x000DA53C File Offset: 0x000D873C
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

	// Token: 0x060013D9 RID: 5081 RVA: 0x000DA5F8 File Offset: 0x000D87F8
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

	// Token: 0x060013DA RID: 5082 RVA: 0x000DA648 File Offset: 0x000D8848
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

	// Token: 0x060013DB RID: 5083 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x000DA894 File Offset: 0x000D8A94
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[173]);
		this.guiMain_.uiObjects[173].GetComponent<Menu_QA_GameplayVerbessernSelectGame>().Init();
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x0009B0D8 File Offset: 0x000992D8
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

	// Token: 0x060013DE RID: 5086 RVA: 0x000DA8E8 File Offset: 0x000D8AE8
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

	// Token: 0x060013DF RID: 5087 RVA: 0x000DA924 File Offset: 0x000D8B24
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

	// Token: 0x060013E0 RID: 5088 RVA: 0x0000D8FD File Offset: 0x0000BAFD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x0000D923 File Offset: 0x0000BB23
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x000DA988 File Offset: 0x000D8B88
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

	// Token: 0x060013E3 RID: 5091 RVA: 0x000DA9EC File Offset: 0x000D8BEC
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

	// Token: 0x04001800 RID: 6144
	public GameObject[] uiObjects;

	// Token: 0x04001801 RID: 6145
	public int[] costs;

	// Token: 0x04001802 RID: 6146
	public float[] points;

	// Token: 0x04001803 RID: 6147
	public float[] pointsInPercent;

	// Token: 0x04001804 RID: 6148
	private bool[] buttonAdds = new bool[6];

	// Token: 0x04001805 RID: 6149
	private GameObject main_;

	// Token: 0x04001806 RID: 6150
	private mainScript mS_;

	// Token: 0x04001807 RID: 6151
	private textScript tS_;

	// Token: 0x04001808 RID: 6152
	private GUI_Main guiMain_;

	// Token: 0x04001809 RID: 6153
	private sfxScript sfx_;

	// Token: 0x0400180A RID: 6154
	private genres genres_;

	// Token: 0x0400180B RID: 6155
	private themes themes_;

	// Token: 0x0400180C RID: 6156
	private licences licences_;

	// Token: 0x0400180D RID: 6157
	private engineFeatures eF_;

	// Token: 0x0400180E RID: 6158
	private cameraMovementScript cmS_;

	// Token: 0x0400180F RID: 6159
	private unlockScript unlock_;

	// Token: 0x04001810 RID: 6160
	private gameplayFeatures gF_;

	// Token: 0x04001811 RID: 6161
	private games games_;

	// Token: 0x04001812 RID: 6162
	private gameScript selectedGame;

	// Token: 0x04001813 RID: 6163
	private roomScript rS_;

	// Token: 0x04001814 RID: 6164
	private float updateTimer;

	// Token: 0x04001815 RID: 6165
	private bool allAdds;
}
