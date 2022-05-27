using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000215 RID: 533
public class Menu_AwardsVerlauf : MonoBehaviour
{
	// Token: 0x06001486 RID: 5254 RVA: 0x000D4D7D File Offset: 0x000D2F7D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x000D4D88 File Offset: 0x000D2F88
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

	// Token: 0x06001488 RID: 5256 RVA: 0x000D4E8C File Offset: 0x000D308C
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000D4E94 File Offset: 0x000D3094
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
			this.FindWinners(this.mS_.madGamesCon_BestGrafik[this.seite], this.mS_.madGamesCon_BestSound[this.seite], this.mS_.madGamesCon_BestStudio[this.seite], this.mS_.madGamesCon_BestPublisher[this.seite], this.mS_.madGamesCon_BestGame[this.seite], this.mS_.madGamesCon_BadGame[this.seite]);
		}
		this.ShowAwards();
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000D4FA8 File Offset: 0x000D31A8
	public void FindWinners(int IDbestGrafik, int IDbestSound, int IDbestStudio, int IDbestPublisher, int IDbestGame, int IDbadGame)
	{
		this.bestGrafik = null;
		this.bestSound = null;
		this.bestStudio = null;
		this.bestPublisher = null;
		this.bestGame = null;
		this.badGame = null;
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

	// Token: 0x0600148B RID: 5259 RVA: 0x000D5094 File Offset: 0x000D3294
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
			if (gameScript.ownerID == this.mS_.myID || gameScript.publisherID == this.mS_.myID)
			{
				this.uiObjects[0].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
			}
			else
			{
				this.uiObjects[0].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[0].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				}
			}
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = "-";
		}
		gameScript = this.bestSound;
		if (gameScript)
		{
			if (gameScript.ownerID == this.mS_.myID || gameScript.publisherID == this.mS_.myID)
			{
				this.uiObjects[1].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
			}
			else
			{
				this.uiObjects[1].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[1].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				}
			}
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = "-";
		}
		publisherScript publisherScript = this.bestStudio;
		if (publisherScript)
		{
			if (publisherScript.myID != this.mS_.myID)
			{
				this.uiObjects[2].GetComponent<Text>().text = publisherScript.GetName();
				if (publisherScript.isPlayer)
				{
					this.uiObjects[2].GetComponent<Text>().text = "<color=magenta>" + publisherScript.GetName() + "</color>";
				}
			}
			else
			{
				this.uiObjects[2].GetComponent<Text>().text = "<color=blue>" + publisherScript.GetName() + "</color>";
			}
			this.uiObjects[12].GetComponent<Button>().interactable = true;
		}
		publisherScript = this.bestPublisher;
		if (publisherScript)
		{
			if (publisherScript.myID != this.mS_.myID)
			{
				this.uiObjects[3].GetComponent<Text>().text = publisherScript.GetName();
				if (publisherScript.isPlayer)
				{
					this.uiObjects[3].GetComponent<Text>().text = "<color=magenta>" + publisherScript.GetName() + "</color>";
				}
			}
			else
			{
				this.uiObjects[3].GetComponent<Text>().text = "<color=blue>" + publisherScript.GetName() + "</color>";
			}
			this.uiObjects[13].GetComponent<Button>().interactable = true;
		}
		gameScript = this.bestGame;
		if (gameScript)
		{
			if (gameScript.ownerID == this.mS_.myID || gameScript.publisherID == this.mS_.myID)
			{
				this.uiObjects[4].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
			}
			else
			{
				this.uiObjects[4].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[4].GetComponent<Text>().text = "<color=magenta>" + gameScript.GetNameWithTag() + "</color>";
				}
			}
		}
		else
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
		}
		gameScript = this.badGame;
		if (gameScript)
		{
			if (gameScript.ownerID == this.mS_.myID || gameScript.publisherID == this.mS_.myID)
			{
				this.uiObjects[5].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>";
				return;
			}
			this.uiObjects[5].GetComponent<Text>().text = gameScript.GetNameWithTag();
			if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
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

	// Token: 0x0600148C RID: 5260 RVA: 0x000D5670 File Offset: 0x000D3870
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

	// Token: 0x0600148D RID: 5261 RVA: 0x000D56E0 File Offset: 0x000D38E0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000D56FC File Offset: 0x000D38FC
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

	// Token: 0x0600148F RID: 5263 RVA: 0x000D5834 File Offset: 0x000D3A34
	public void BUTTON_ShowStudio()
	{
		this.sfx_.PlaySound(3, true);
		if (this.bestStudio)
		{
			this.guiMain_.uiObjects[359].SetActive(true);
			this.guiMain_.uiObjects[359].GetComponent<Menu_Stats_Developer_Main>().Init(this.bestStudio);
		}
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x000D5894 File Offset: 0x000D3A94
	public void BUTTON_ShowPublisher()
	{
		this.sfx_.PlaySound(3, true);
		if (this.bestStudio)
		{
			this.guiMain_.uiObjects[373].SetActive(true);
			this.guiMain_.uiObjects[373].GetComponent<Menu_Stats_Publisher_Main>().Init(this.bestPublisher);
		}
	}

	// Token: 0x04001892 RID: 6290
	public GameObject[] uiObjects;

	// Token: 0x04001893 RID: 6291
	private GameObject main_;

	// Token: 0x04001894 RID: 6292
	private mainScript mS_;

	// Token: 0x04001895 RID: 6293
	private textScript tS_;

	// Token: 0x04001896 RID: 6294
	private GUI_Main guiMain_;

	// Token: 0x04001897 RID: 6295
	private sfxScript sfx_;

	// Token: 0x04001898 RID: 6296
	private genres genres_;

	// Token: 0x04001899 RID: 6297
	private themes themes_;

	// Token: 0x0400189A RID: 6298
	private games games_;

	// Token: 0x0400189B RID: 6299
	public gameScript bestGrafik;

	// Token: 0x0400189C RID: 6300
	public gameScript bestSound;

	// Token: 0x0400189D RID: 6301
	public publisherScript bestStudio;

	// Token: 0x0400189E RID: 6302
	public publisherScript bestPublisher;

	// Token: 0x0400189F RID: 6303
	public gameScript bestGame;

	// Token: 0x040018A0 RID: 6304
	public gameScript badGame;

	// Token: 0x040018A1 RID: 6305
	public int seite;
}
