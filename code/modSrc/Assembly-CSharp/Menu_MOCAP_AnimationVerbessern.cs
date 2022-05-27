using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DB RID: 475
public class Menu_MOCAP_AnimationVerbessern : MonoBehaviour
{
	// Token: 0x060011CB RID: 4555 RVA: 0x0000C732 File Offset: 0x0000A932
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000C7818 File Offset: 0x000C5A18
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

	// Token: 0x060011CD RID: 4557 RVA: 0x0000C73A File Offset: 0x0000A93A
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x000C79B8 File Offset: 0x000C5BB8
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

	// Token: 0x060011CF RID: 4559 RVA: 0x0000C75B File Offset: 0x0000A95B
	public void Init(roomScript roomScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.selectedGame = this.FindGame();
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x000C7A04 File Offset: 0x000C5C04
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

	// Token: 0x060011D1 RID: 4561 RVA: 0x000C7AC0 File Offset: 0x000C5CC0
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

	// Token: 0x060011D2 RID: 4562 RVA: 0x000C7B10 File Offset: 0x000C5D10
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

	// Token: 0x060011D3 RID: 4563 RVA: 0x0000C782 File Offset: 0x0000A982
	public void SetGame(gameScript script_)
	{
		this.selectedGame = script_;
		this.DeselectAllButtons();
		this.UpdateGUI();
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000C7D60 File Offset: 0x000C5F60
	public void BUTTON_SelectGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[179]);
		this.guiMain_.uiObjects[179].GetComponent<Menu_MOCAP_AnimationVerbessernSelectGame>().Init();
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000C7DB4 File Offset: 0x000C5FB4
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

	// Token: 0x060011D6 RID: 4566 RVA: 0x000C7E4C File Offset: 0x000C604C
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

	// Token: 0x060011D7 RID: 4567 RVA: 0x000C7E88 File Offset: 0x000C6088
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

	// Token: 0x060011D8 RID: 4568 RVA: 0x0000C797 File Offset: 0x0000A997
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000C7EE4 File Offset: 0x000C60E4
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

	// Token: 0x060011DA RID: 4570 RVA: 0x000C7F40 File Offset: 0x000C6140
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

	// Token: 0x060011DB RID: 4571 RVA: 0x000C8130 File Offset: 0x000C6330
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

	// Token: 0x060011DC RID: 4572 RVA: 0x000C8194 File Offset: 0x000C6394
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

	// Token: 0x0400165A RID: 5722
	public GameObject[] uiObjects;

	// Token: 0x0400165B RID: 5723
	public int[] costs;

	// Token: 0x0400165C RID: 5724
	public float[] pointsInPercent;

	// Token: 0x0400165D RID: 5725
	private bool[] buttonAdds = new bool[6];

	// Token: 0x0400165E RID: 5726
	private GameObject main_;

	// Token: 0x0400165F RID: 5727
	private mainScript mS_;

	// Token: 0x04001660 RID: 5728
	private textScript tS_;

	// Token: 0x04001661 RID: 5729
	private GUI_Main guiMain_;

	// Token: 0x04001662 RID: 5730
	private sfxScript sfx_;

	// Token: 0x04001663 RID: 5731
	private genres genres_;

	// Token: 0x04001664 RID: 5732
	private themes themes_;

	// Token: 0x04001665 RID: 5733
	private licences licences_;

	// Token: 0x04001666 RID: 5734
	private engineFeatures eF_;

	// Token: 0x04001667 RID: 5735
	private cameraMovementScript cmS_;

	// Token: 0x04001668 RID: 5736
	private unlockScript unlock_;

	// Token: 0x04001669 RID: 5737
	private gameplayFeatures gF_;

	// Token: 0x0400166A RID: 5738
	private games games_;

	// Token: 0x0400166B RID: 5739
	private gameScript selectedGame;

	// Token: 0x0400166C RID: 5740
	private roomScript rS_;

	// Token: 0x0400166D RID: 5741
	private float updateTimer;

	// Token: 0x0400166E RID: 5742
	private bool allAdds;
}
