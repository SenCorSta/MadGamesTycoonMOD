using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Achivement : MonoBehaviour
{
	
	public void SetData(int i)
	{
		this.myID = i;
		if (this.guiMain_)
		{
			this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.iconAchivements[i];
			if (this.mS_.achivements[i])
			{
				this.uiObjects[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetAchivementName(i);
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetAchivementDesc(i);
			this.uiObjects[3].GetComponent<Text>().text = this.GetBonusText(this.mS_.achivementsBonus[i]);
		}
	}

	
	public string GetBonusText(int i)
	{
		int num = 1;
		string text = "";
		switch (i)
		{
		case 0:
			text = this.tS_.GetText(1801);
			break;
		case 1:
			text = this.tS_.GetText(1802);
			break;
		case 2:
			text = this.tS_.GetText(1803);
			break;
		case 3:
			text = this.tS_.GetText(1804);
			break;
		case 4:
			text = this.tS_.GetText(1805);
			break;
		case 5:
			text = this.tS_.GetText(1806);
			break;
		case 6:
			text = this.tS_.GetText(1807);
			break;
		case 7:
			text = this.tS_.GetText(1808);
			break;
		case 8:
			text = this.tS_.GetText(1809);
			break;
		case 9:
			text = this.tS_.GetText(1810);
			break;
		}
		return text.Replace("<NUM>", num.ToString());
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public GUI_Main guiMain_;

	
	private int myID;
}
