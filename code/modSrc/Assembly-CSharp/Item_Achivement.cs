using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AF RID: 175
public class Item_Achivement : MonoBehaviour
{
	// Token: 0x06000667 RID: 1639 RVA: 0x000502BC File Offset: 0x0004E4BC
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

	// Token: 0x06000668 RID: 1640 RVA: 0x0005039C File Offset: 0x0004E59C
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

	// Token: 0x06000669 RID: 1641 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040009F5 RID: 2549
	public GameObject[] uiObjects;

	// Token: 0x040009F6 RID: 2550
	public mainScript mS_;

	// Token: 0x040009F7 RID: 2551
	public textScript tS_;

	// Token: 0x040009F8 RID: 2552
	public GUI_Main guiMain_;

	// Token: 0x040009F9 RID: 2553
	private int myID;
}
