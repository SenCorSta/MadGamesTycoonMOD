using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000213 RID: 531
public class Menu_SFX_SoundVerbessern : MonoBehaviour
{
	// Token: 0x06001467 RID: 5223 RVA: 0x000D3F51 File Offset: 0x000D2151
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x000D3F5C File Offset: 0x000D215C
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

	// Token: 0x06001469 RID: 5225 RVA: 0x000D40FA File Offset: 0x000D22FA
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x000D411C File Offset: 0x000D231C
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

	// Token: 0x0600146B RID: 5227 RVA: 0x000D4168 File Offset: 0x000D2368
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000D4190 File Offset: 0x000D2390
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

	// Token: 0x0600146D RID: 5229 RVA: 0x000D4268 File Offset: 0x000D2468
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

	// Token: 0x0600146E RID: 5230 RVA: 0x000D42B8 File Offset: 0x000D24B8
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.selectedGame)
			{
				if (this.selectedGame.soundStudio[i])
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
		if (this.selectedGame && !this.selectedGame.gameGameplayFeatures[41])
		{
			this.uiObjects[5].GetComponent<Button>().interactable = false;
			this.uiObjects[5].transform.GetChild(6).gameObject.SetActive(true);
			string text = this.tS_.GetText(919);
			text = text.Replace("<NAME>", this.gF_.GetName(41));
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

	// Token: 0x0600146F RID: 5231 RVA: 0x000D45A1 File Offset: 0x000D27A1
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x000D45B8 File Offset: 0x000D27B8
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[177]);
		this.guiMain_.uiObjects[177].GetComponent<Menu_SFX_SoundVerbessernSelectGame>().Init();
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x000D460C File Offset: 0x000D280C
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

	// Token: 0x06001472 RID: 5234 RVA: 0x000D4674 File Offset: 0x000D2874
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

	// Token: 0x06001473 RID: 5235 RVA: 0x000D46B0 File Offset: 0x000D28B0
	private bool WirdInAnderenRaumBearbeitet(int slot)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Task");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				taskSoundVerbessern component = array[i].GetComponent<taskSoundVerbessern>();
				if (component && component.targetID == this.selectedGame.myID && component.adds[slot])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000D4711 File Offset: 0x000D2911
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000D4737 File Offset: 0x000D2937
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000D4760 File Offset: 0x000D2960
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

	// Token: 0x06001477 RID: 5239 RVA: 0x000D47C4 File Offset: 0x000D29C4
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
		taskSoundVerbessern taskSoundVerbessern = this.guiMain_.AddTask_SoundVerbessern();
		taskSoundVerbessern.Init(false);
		taskSoundVerbessern.targetID = this.selectedGame.myID;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				taskSoundVerbessern.adds[i] = true;
			}
		}
		taskSoundVerbessern.FindNewAdd();
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskSoundVerbessern.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001873 RID: 6259
	public GameObject[] uiObjects;

	// Token: 0x04001874 RID: 6260
	public int[] costs;

	// Token: 0x04001875 RID: 6261
	public float[] points;

	// Token: 0x04001876 RID: 6262
	public float[] pointsInPercent;

	// Token: 0x04001877 RID: 6263
	private bool[] buttonAdds = new bool[6];

	// Token: 0x04001878 RID: 6264
	private GameObject main_;

	// Token: 0x04001879 RID: 6265
	private mainScript mS_;

	// Token: 0x0400187A RID: 6266
	private textScript tS_;

	// Token: 0x0400187B RID: 6267
	private GUI_Main guiMain_;

	// Token: 0x0400187C RID: 6268
	private sfxScript sfx_;

	// Token: 0x0400187D RID: 6269
	private genres genres_;

	// Token: 0x0400187E RID: 6270
	private themes themes_;

	// Token: 0x0400187F RID: 6271
	private licences licences_;

	// Token: 0x04001880 RID: 6272
	private engineFeatures eF_;

	// Token: 0x04001881 RID: 6273
	private cameraMovementScript cmS_;

	// Token: 0x04001882 RID: 6274
	private unlockScript unlock_;

	// Token: 0x04001883 RID: 6275
	private gameplayFeatures gF_;

	// Token: 0x04001884 RID: 6276
	private games games_;

	// Token: 0x04001885 RID: 6277
	private gameScript selectedGame;

	// Token: 0x04001886 RID: 6278
	private roomScript rS_;

	// Token: 0x04001887 RID: 6279
	private float updateTimer;

	// Token: 0x04001888 RID: 6280
	private bool allAdds;
}
