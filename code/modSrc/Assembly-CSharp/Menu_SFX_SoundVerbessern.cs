using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000212 RID: 530
public class Menu_SFX_SoundVerbessern : MonoBehaviour
{
	// Token: 0x0600144A RID: 5194 RVA: 0x0000DD1F File Offset: 0x0000BF1F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x000DD868 File Offset: 0x000DBA68
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

	// Token: 0x0600144C RID: 5196 RVA: 0x0000DD27 File Offset: 0x0000BF27
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000DDA08 File Offset: 0x000DBC08
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

	// Token: 0x0600144E RID: 5198 RVA: 0x0000DD48 File Offset: 0x0000BF48
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000DDA54 File Offset: 0x000DBC54
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

	// Token: 0x06001450 RID: 5200 RVA: 0x000DDB2C File Offset: 0x000DBD2C
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

	// Token: 0x06001451 RID: 5201 RVA: 0x000DDB7C File Offset: 0x000DBD7C
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

	// Token: 0x06001452 RID: 5202 RVA: 0x0000DD6F File Offset: 0x0000BF6F
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x000DDE68 File Offset: 0x000DC068
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[177]);
		this.guiMain_.uiObjects[177].GetComponent<Menu_SFX_SoundVerbessernSelectGame>().Init();
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0009B0D8 File Offset: 0x000992D8
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

	// Token: 0x06001455 RID: 5205 RVA: 0x000DDEBC File Offset: 0x000DC0BC
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

	// Token: 0x06001456 RID: 5206 RVA: 0x000DDEF8 File Offset: 0x000DC0F8
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

	// Token: 0x06001457 RID: 5207 RVA: 0x0000DD84 File Offset: 0x0000BF84
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x0000DDAA File Offset: 0x0000BFAA
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x000DDF5C File Offset: 0x000DC15C
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

	// Token: 0x0600145A RID: 5210 RVA: 0x000DDFC0 File Offset: 0x000DC1C0
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

	// Token: 0x0400186A RID: 6250
	public GameObject[] uiObjects;

	// Token: 0x0400186B RID: 6251
	public int[] costs;

	// Token: 0x0400186C RID: 6252
	public float[] points;

	// Token: 0x0400186D RID: 6253
	public float[] pointsInPercent;

	// Token: 0x0400186E RID: 6254
	private bool[] buttonAdds = new bool[6];

	// Token: 0x0400186F RID: 6255
	private GameObject main_;

	// Token: 0x04001870 RID: 6256
	private mainScript mS_;

	// Token: 0x04001871 RID: 6257
	private textScript tS_;

	// Token: 0x04001872 RID: 6258
	private GUI_Main guiMain_;

	// Token: 0x04001873 RID: 6259
	private sfxScript sfx_;

	// Token: 0x04001874 RID: 6260
	private genres genres_;

	// Token: 0x04001875 RID: 6261
	private themes themes_;

	// Token: 0x04001876 RID: 6262
	private licences licences_;

	// Token: 0x04001877 RID: 6263
	private engineFeatures eF_;

	// Token: 0x04001878 RID: 6264
	private cameraMovementScript cmS_;

	// Token: 0x04001879 RID: 6265
	private unlockScript unlock_;

	// Token: 0x0400187A RID: 6266
	private gameplayFeatures gF_;

	// Token: 0x0400187B RID: 6267
	private games games_;

	// Token: 0x0400187C RID: 6268
	private gameScript selectedGame;

	// Token: 0x0400187D RID: 6269
	private roomScript rS_;

	// Token: 0x0400187E RID: 6270
	private float updateTimer;

	// Token: 0x0400187F RID: 6271
	private bool allAdds;
}
