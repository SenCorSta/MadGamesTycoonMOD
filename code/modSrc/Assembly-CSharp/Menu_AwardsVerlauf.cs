using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_AwardsVerlauf : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void OnEnable()
	{
		this.Init();
	}

	
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
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

	
	public void BUTTON_ShowStudio()
	{
		this.sfx_.PlaySound(3, true);
		if (this.bestStudio)
		{
			this.guiMain_.uiObjects[359].SetActive(true);
			this.guiMain_.uiObjects[359].GetComponent<Menu_Stats_Developer_Main>().Init(this.bestStudio);
		}
	}

	
	public void BUTTON_ShowPublisher()
	{
		this.sfx_.PlaySound(3, true);
		if (this.bestStudio)
		{
			this.guiMain_.uiObjects[373].SetActive(true);
			this.guiMain_.uiObjects[373].GetComponent<Menu_Stats_Publisher_Main>().Init(this.bestPublisher);
		}
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private games games_;

	
	public gameScript bestGrafik;

	
	public gameScript bestSound;

	
	public publisherScript bestStudio;

	
	public publisherScript bestPublisher;

	
	public gameScript bestGame;

	
	public gameScript badGame;

	
	public int seite;
}
