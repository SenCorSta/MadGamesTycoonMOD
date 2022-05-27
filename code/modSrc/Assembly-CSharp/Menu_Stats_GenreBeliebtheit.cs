using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_GenreBeliebtheit : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	
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
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(245) + "<color=blue><b>\n" + this.genres_.GetName(this.mS_.GetFanGenreID()) + "</b></color>";
		this.uiObjects[15].GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.GetFanGenreID());
		string text = this.tS_.GetText(481);
		text = text.Replace("<TIME>", this.mS_.trendWeeks.ToString());
		this.uiObjects[5].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1911);
		text = text.Replace("<NAME>", "<color=blue>" + this.genres_.GetName(this.mS_.GetFanGenreID()) + "</color>");
		this.uiObjects[8].GetComponent<tooltip>().c = text;
		for (int i = 0; i < 5; i++)
		{
			if (this.mS_.lastGamesGenre[i] >= 0)
			{
				this.uiObjects[9 + i].transform.GetChild(0).GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.lastGamesGenre[i]);
				this.uiObjects[9 + i].GetComponent<tooltip>().c = this.genres_.GetName(this.mS_.lastGamesGenre[i]);
			}
			else
			{
				this.uiObjects[9 + i].transform.GetChild(0).GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
				this.uiObjects[9 + i].GetComponent<tooltip>().c = "";
			}
		}
		for (int j = 0; j < this.uiObjects[0].transform.childCount; j++)
		{
			this.uiObjects[0].transform.GetChild(j).gameObject.SetActive(false);
		}
		for (int k = 0; k < this.genres_.genres_PRICE.Length; k++)
		{
			if (this.genres_.genres_UNLOCK[k])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_Stats_GenreBeliebtheit component = gameObject.GetComponent<Item_Stats_GenreBeliebtheit>();
				component.guiMain_ = this.guiMain_;
				component.tS_ = this.tS_;
				float prozent = 0f;
				if (this.mS_.trendGenre != k && this.mS_.trendAntiGenre != k)
				{
					prozent = this.genres_.genres_BELIEBTHEIT[k];
				}
				if (this.mS_.trendAntiGenre == k)
				{
					prozent = 20f;
				}
				if (this.mS_.trendGenre == k)
				{
					prozent = 100f;
				}
				component.Init(this.genres_.GetName(k), prozent, this.genres_.GetPic(k));
				gameObject.name = prozent.ToString();
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (this.closeMenu)
		{
			this.closeMenu = false;
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private engineFeatures eF_;

	
	private genres genres_;

	
	private games games_;

	
	private themes themes_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	public bool closeMenu;

	
	private float updateTimer;
}
