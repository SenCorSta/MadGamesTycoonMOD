using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_DevGame_Genre : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		this.uiObjects[3].GetComponent<Text>().text = this.genres_.GetStringBeliebtheit(this.myID, true);
		if (this.mS_.trendGenre == this.myID)
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[31];
		}
		if (this.mS_.trendAntiGenre == this.myID)
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[32];
		}
		this.guiMain_.DrawStars(this.uiObjects[2], this.genres_.genres_LEVEL[this.myID]);
		this.uiObjects[5].GetComponent<Image>().sprite = this.genres_.GetSpriteMarkt(this.myID);
		this.tooltip_.c = this.genres_.GetTooltip(this.myID) + "\n";
		tooltip tooltip = this.tooltip_;
		tooltip.c = string.Concat(new string[]
		{
			tooltip.c,
			"\n",
			this.tS_.GetText(1380),
			": <color=blue>",
			this.genres_.GetStringBeliebtheit(this.myID, false),
			"</color>"
		});
		tooltip = this.tooltip_;
		tooltip.c = string.Concat(new string[]
		{
			tooltip.c,
			"\n",
			this.tS_.GetText(1665),
			": <color=blue>",
			this.genres_.GetStringMarktsaettigung(this.myID),
			"</color>"
		});
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.genreArt == 0)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMainGenre(this.myID);
		}
		else
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetSubGenre(this.myID);
		}
		this.guiMain_.uiObjects[61].GetComponent<Menu_DevGame_Genre>().BUTTON_Close();
	}

	
	public int genreArt;

	
	public int myID;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public genres genres_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;
}
