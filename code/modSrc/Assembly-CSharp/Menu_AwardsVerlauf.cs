using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000214 RID: 532
public class Menu_AwardsVerlauf : MonoBehaviour
{
	// Token: 0x06001468 RID: 5224 RVA: 0x0000DE57 File Offset: 0x0000C057
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x0000DE5F File Offset: 0x0000C05F
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x000DE5D8 File Offset: 0x000DC7D8
	public void Init()
	{
		this.FindScripts();
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > this.mS_.madGamesCon_BestGrafik.Count - 1)
		{
			this.seite = this.mS_.madGamesCon_BestGrafik.Count - 1;
		}
		if (this.mS_.madGamesCon_BestGrafik.Count <= 0)
		{
			return;
		}
		if (this.mS_.madGamesCon_BestGrafik.Count > this.seite)
		{
			this.FindWinners(this.mS_.madGamesCon_BestGrafik[this.seite], this.mS_.madGamesCon_BestSound[this.seite], this.mS_.madGamesCon_BestStudio[this.seite], this.mS_.madGamesCon_BestStudioPlayer[this.seite], this.mS_.madGamesCon_BestPublisher[this.seite], this.mS_.madGamesCon_BestPublisherPlayer[this.seite], this.mS_.madGamesCon_BestGame[this.seite], this.mS_.madGamesCon_BadGame[this.seite]);
		}
		this.ShowAwards();
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000DE718 File Offset: 0x000DC918
	public void FindWinners(int IDbestGrafik, int IDbestSound, int IDbestStudio, int bestStudioPlayer_, int IDbestPublisher, int bestPublisherPlayer_, int IDbestGame, int IDbadGame)
	{
		this.bestGrafik = null;
		this.bestSound = null;
		this.bestStudio = null;
		this.bestStudioPlayer = -1;
		this.bestPublisher = null;
		this.bestPublisherPlayer = -1;
		this.bestGame = null;
		this.badGame = null;
		this.bestStudioPlayer = bestStudioPlayer_;
		this.bestPublisherPlayer = bestPublisherPlayer_;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (IDbestGrafik == component.myID)
				{
					this.bestGrafik = component;
				}
				if (IDbestSound == component.myID)
				{
					this.bestSound = component;
				}
				if (IDbestGame == component.myID)
				{
					this.bestGame = component;
				}
				if (IDbadGame == component.myID)
				{
					this.badGame = component;
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("Publisher");
		if (array.Length != 0)
		{
			for (int j = 0; j < array.Length; j++)
			{
				publisherScript component2 = array[j].GetComponent<publisherScript>();
				if (IDbestStudio == component2.myID)
				{
					this.bestStudio = component2;
				}
				if (IDbestPublisher == component2.myID)
				{
					this.bestPublisher = component2;
				}
			}
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000DE824 File Offset: 0x000DCA24
	private void ShowAwards()
	{
		if (!this.bestGrafik)
		{
			this.uiObjects[0].GetComponent<Text>().text = "-";
			this.uiObjects[1].GetComponent<Text>().text = "-";
			this.uiObjects[2].GetComponent<Text>().text = "-";
			this.uiObjects[3].GetComponent<Text>().text = "-";
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
			this.uiObjects[6].GetComponent<Text>().text = "-";
			this.uiObjects[7].GetComponent<Text>().text = "-";
			return;
		}
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.madGamesCon_Jahr[this.seite].ToString();
		this.uiObjects[7].GetComponent<Text>().text = (this.seite + 1).ToString() + " / " + this.mS_.madGamesCon_Jahr.Count.ToString();
		gameScript gameScript = this.bestGrafik;
		if (gameScript)
		{
			if (!gameScript.playerGame)
			{
				this.uiObjects[0].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[0].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				}
			}
			else
			{
				this.uiObjects[0].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
			}
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = "-";
		}
		gameScript = this.bestSound;
		if (gameScript)
		{
			if (!gameScript.playerGame)
			{
				this.uiObjects[1].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[1].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				}
			}
			else
			{
				this.uiObjects[1].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
			}
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = "-";
		}
		publisherScript publisherScript = this.bestStudio;
		if (publisherScript)
		{
			this.uiObjects[2].GetComponent<Text>().text = publisherScript.GetName();
			this.uiObjects[12].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[12].GetComponent<Button>().interactable = false;
			if (!this.mS_.multiplayer || this.bestStudioPlayer == this.mS_.mpCalls_.myID)
			{
				this.uiObjects[2].GetComponent<Text>().text = "<color=blue>" + this.mS_.companyName + "</color>";
			}
			else
			{
				this.uiObjects[2].GetComponent<Text>().text = "<color=magenta>" + this.mS_.mpCalls_.GetCompanyName(this.bestStudioPlayer) + "</color>";
			}
		}
		publisherScript = this.bestPublisher;
		if (publisherScript)
		{
			this.uiObjects[3].GetComponent<Text>().text = publisherScript.GetName();
			this.uiObjects[13].GetComponent<Button>().interactable = true;
		}
		else
		{
			this.uiObjects[13].GetComponent<Button>().interactable = false;
			if (!this.mS_.multiplayer || this.bestPublisherPlayer == this.mS_.mpCalls_.myID)
			{
				this.uiObjects[3].GetComponent<Text>().text = "<color=blue>" + this.mS_.companyName + "</color>";
			}
			else
			{
				this.uiObjects[3].GetComponent<Text>().text = "<color=magenta>" + this.mS_.mpCalls_.GetCompanyName(this.bestPublisherPlayer) + "</color>";
			}
		}
		gameScript = this.bestGame;
		if (gameScript)
		{
			if (!gameScript.playerGame)
			{
				this.uiObjects[4].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[4].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				}
			}
			else
			{
				this.uiObjects[4].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
			}
		}
		else
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
		}
		gameScript = this.badGame;
		if (gameScript)
		{
			if (gameScript.playerGame)
			{
				this.uiObjects[5].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
				return;
			}
			this.uiObjects[5].GetComponent<Text>().text = gameScript.GetNameWithTag();
			if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
			{
				this.uiObjects[5].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				return;
			}
		}
		else
		{
			this.uiObjects[5].GetComponent<Text>().text = "-";
		}
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000DEDF8 File Offset: 0x000DCFF8
	public void BUTTON_Seite(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.seite += i;
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > this.mS_.madGamesCon_BestGrafik.Count - 1)
		{
			this.seite = this.mS_.madGamesCon_BestGrafik.Count - 1;
		}
		this.Init();
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0000DE67 File Offset: 0x0000C067
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x000DEE68 File Offset: 0x000DD068
	public void BUTTON_ShowGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
			if (this.bestGrafik)
			{
				this.guiMain_.uiObjects[46].SetActive(true);
				this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.bestGrafik);
				return;
			}
			break;
		case 1:
			if (this.bestSound)
			{
				this.guiMain_.uiObjects[46].SetActive(true);
				this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.bestSound);
				return;
			}
			break;
		case 2:
			if (this.bestGame)
			{
				this.guiMain_.uiObjects[46].SetActive(true);
				this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.bestGame);
				return;
			}
			break;
		case 3:
			if (this.badGame)
			{
				this.guiMain_.uiObjects[46].SetActive(true);
				this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.badGame);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x000DEFA0 File Offset: 0x000DD1A0
	public void BUTTON_ShowStudio()
	{
		this.sfx_.PlaySound(3, true);
		if (this.bestStudio)
		{
			this.guiMain_.uiObjects[359].SetActive(true);
			this.guiMain_.uiObjects[359].GetComponent<Menu_Stats_Developer_Main>().Init(this.bestStudio);
		}
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x000DF000 File Offset: 0x000DD200
	public void BUTTON_ShowPublisher()
	{
		this.sfx_.PlaySound(3, true);
		if (this.bestStudio)
		{
			this.guiMain_.uiObjects[373].SetActive(true);
			this.guiMain_.uiObjects[373].GetComponent<Menu_Stats_Publisher_Main>().Init(this.bestPublisher);
		}
	}

	// Token: 0x04001889 RID: 6281
	public GameObject[] uiObjects;

	// Token: 0x0400188A RID: 6282
	private GameObject main_;

	// Token: 0x0400188B RID: 6283
	private mainScript mS_;

	// Token: 0x0400188C RID: 6284
	private textScript tS_;

	// Token: 0x0400188D RID: 6285
	private GUI_Main guiMain_;

	// Token: 0x0400188E RID: 6286
	private sfxScript sfx_;

	// Token: 0x0400188F RID: 6287
	private genres genres_;

	// Token: 0x04001890 RID: 6288
	private themes themes_;

	// Token: 0x04001891 RID: 6289
	private games games_;

	// Token: 0x04001892 RID: 6290
	public gameScript bestGrafik;

	// Token: 0x04001893 RID: 6291
	public gameScript bestSound;

	// Token: 0x04001894 RID: 6292
	public publisherScript bestStudio;

	// Token: 0x04001895 RID: 6293
	public int bestStudioPlayer;

	// Token: 0x04001896 RID: 6294
	public publisherScript bestPublisher;

	// Token: 0x04001897 RID: 6295
	public int bestPublisherPlayer;

	// Token: 0x04001898 RID: 6296
	public gameScript bestGame;

	// Token: 0x04001899 RID: 6297
	public gameScript badGame;

	// Token: 0x0400189A RID: 6298
	public int seite;
}
