using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D8 RID: 472
public class Menu_Trendsetter : MonoBehaviour
{
	// Token: 0x060011B9 RID: 4537 RVA: 0x0000C67A File Offset: 0x0000A87A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x000C72AC File Offset: 0x000C54AC
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0000C682 File Offset: 0x0000A882
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000C7394 File Offset: 0x000C5594
	public void Init(gameScript script_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(31);
		if (script_)
		{
			string text = this.tS_.GetText(760);
			text = text.Replace("<NAME>", script_.GetNameWithTag());
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(script_.maingenre);
			this.uiObjects[2].GetComponent<Text>().text = this.genres_.GetName(script_.maingenre);
			this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetThemes(script_.gameMainTheme);
			script_.trendsetter = true;
			this.mS_.trendWeeks = UnityEngine.Random.Range(50, 100);
			this.mS_.trendGenre = script_.maingenre;
			int num = 0;
			bool flag = false;
			while (!flag)
			{
				this.mS_.trendAntiGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[this.mS_.trendAntiGenre] && this.mS_.trendAntiGenre != this.mS_.trendGenre)
				{
					flag = true;
				}
				num++;
				if (num > 10000)
				{
					flag = true;
				}
			}
			this.mS_.trendTheme = script_.gameMainTheme;
			if (this.mS_.trendAntiTheme == this.mS_.trendTheme)
			{
				if (this.mS_.trendAntiTheme > 0)
				{
					this.mS_.trendAntiTheme--;
				}
				else
				{
					this.mS_.trendAntiTheme++;
				}
			}
			this.mS_.awards[6]++;
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Trend();
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Trend();
					return;
				}
			}
		}
		else
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x000C75C0 File Offset: 0x000C57C0
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.guiMain_.CreateTopNewsTrend(this.genres_.GetName(this.mS_.trendGenre) + " / " + this.tS_.GetThemes(this.mS_.trendTheme), this.genres_.GetPic(this.mS_.trendGenre));
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0000C69D File Offset: 0x0000A89D
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001644 RID: 5700
	public GameObject[] uiObjects;

	// Token: 0x04001645 RID: 5701
	private GameObject main_;

	// Token: 0x04001646 RID: 5702
	private mainScript mS_;

	// Token: 0x04001647 RID: 5703
	private textScript tS_;

	// Token: 0x04001648 RID: 5704
	private GUI_Main guiMain_;

	// Token: 0x04001649 RID: 5705
	private sfxScript sfx_;

	// Token: 0x0400164A RID: 5706
	private genres genres_;

	// Token: 0x0400164B RID: 5707
	private themes themes_;
}
