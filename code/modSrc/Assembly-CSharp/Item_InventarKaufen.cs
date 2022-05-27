using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AC RID: 172
public class Item_InventarKaufen : MonoBehaviour
{
	// Token: 0x0600064A RID: 1610 RVA: 0x0006202C File Offset: 0x0006022C
	private void Start()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetObjects(this.typ);
		this.CheckUnlock();
		this.uiObjects[2].GetComponent<Image>().sprite = this.guiMain_.inventarSprites[this.typ];
		if (this.typ != 17 && this.typ != 129 && this.typ != 130 && this.typ != 132 && this.typ != 133 && this.typ != 134 && this.typ != 135 && this.typ != 142 && this.typ != 143 && this.mapS_.prefabsInventar[this.typ])
		{
			this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mapS_.prefabsInventar[this.typ].GetComponent<objectScript>().preis, true);
		}
		this.SetTooltip();
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00062164 File Offset: 0x00060364
	private void Update()
	{
		this.Highlight();
		int num = this.typ;
		if (num != 17)
		{
			switch (num)
			{
			case 129:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.platinSchallplatten.ToString();
				if (this.mS_.platinSchallplatten <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 130:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.diamantSchallplatten.ToString();
				if (this.mS_.diamantSchallplatten <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 131:
			case 136:
			case 137:
			case 138:
			case 139:
			case 140:
			case 141:
				break;
			case 132:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.award_GOTY.ToString();
				if (this.mS_.award_GOTY <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 133:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.award_Studio.ToString();
				if (this.mS_.award_Studio <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 134:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.award_Sound.ToString();
				if (this.mS_.award_Sound <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 135:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.award_Grafik.ToString();
				if (this.mS_.award_Grafik <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 142:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.award_Trendsetter.ToString();
				if (this.mS_.award_Trendsetter <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				return;
			case 143:
				this.uiObjects[3].GetComponent<Text>().text = this.mS_.award_Publisher.ToString();
				if (this.mS_.award_Publisher <= 0)
				{
					this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
					return;
				}
				this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
				break;
			default:
				return;
			}
			return;
		}
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.goldeneSchallplatten.ToString();
		if (this.mS_.goldeneSchallplatten <= 0)
		{
			this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[8];
			return;
		}
		this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[4];
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x00002098 File Offset: 0x00000298
	private void OnDisable()
	{
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00062604 File Offset: 0x00060804
	private void CheckUnlock()
	{
		objectScript component = this.mapS_.prefabsInventar[this.typ].GetComponent<objectScript>();
		if (component.unlockYear == -1)
		{
			return;
		}
		if (this.mS_.year < component.unlockYear)
		{
			this.uiObjects[1].SetActive(true);
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x00062654 File Offset: 0x00060854
	public void BUTTON_Click()
	{
		if (this.uiObjects[1].activeSelf)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (this.mS_.pickedObject)
		{
			UnityEngine.Object.Destroy(this.mS_.pickedObject);
		}
		this.mapS_.CreateObject(this.typ);
		this.uiObjects[2].GetComponent<Animation>().Play();
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x000626C8 File Offset: 0x000608C8
	private void Highlight()
	{
		if (this.mS_.pickedObject && this.mS_.pickedObject.GetComponent<objectScript>().typ == this.typ)
		{
			this.uiObjects[4].GetComponent<Image>().color = this.colors[1];
			return;
		}
		this.uiObjects[4].GetComponent<Image>().color = this.colors[0];
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00062744 File Offset: 0x00060944
	private void SetTooltip()
	{
		objectScript component = this.mapS_.prefabsInventar[this.typ].GetComponent<objectScript>();
		string text = "<b>" + this.tS_.GetObjects(this.typ) + "</b>";
		text = text + "<br>" + this.tS_.GetObjectsTooltip(this.typ) + "<br>";
		if (component.isServer)
		{
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)component.serverplatz, false));
		}
		if (component.qualitaet > 0)
		{
			text = text + "<br>" + this.tS_.GetText(532);
			text = text + "<br><size=21>" + this.GetQualitatStars(Mathf.RoundToInt((float)component.qualitaet)) + "</size><br>";
			if (component.isArbeitsplatz)
			{
				string text2 = this.tS_.GetText(533);
				text2 = text2.Replace("<NUM>", Mathf.RoundToInt((float)((component.qualitaet - 1) * 10)).ToString());
				text = text + "<br>" + text2;
			}
		}
		if (component.ausstattung > 0f)
		{
			text = text + "<br>" + this.tS_.GetText(311);
			text = text + "<br><size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.ausstattung)) + "</size><br>";
		}
		if (component.motivationRegen > 0f)
		{
			text = text + "<br>" + this.tS_.GetText(313);
			text = text + "<br><size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.motivationRegen / 20f)) + "</size><br>";
		}
		if (component.waerme > 0f)
		{
			text = text + "<br>" + this.tS_.GetText(312);
			text = text + "<br><size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.waerme)) + "</size><br>";
		}
		if (component.kaelte > 0f)
		{
			text = text + "<br>" + this.tS_.GetText(510);
			text = text + "<br><size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.kaelte)) + "</size><br>";
		}
		if (component.monatsKosten > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"<br>",
				this.tS_.GetText(310),
				": ",
				this.mS_.GetMoney((long)component.monatsKosten, true)
			});
		}
		if (component.preis > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"<br>",
				this.tS_.GetText(218),
				": ",
				this.mS_.GetMoney((long)component.preis, true)
			});
		}
		if (component.aufladungenMax > 0)
		{
			string text3 = this.tS_.GetText(775);
			text3 = text3.Replace("<NUM>", component.aufladungenMax.ToString());
			text = text + "<br>" + text3;
		}
		if (this.mS_.year < component.unlockYear)
		{
			string text4 = this.tS_.GetText(535);
			text4 = text4.Replace("<NUM>", component.unlockYear.ToString());
			text = text + "<br><br><color=red><b>" + text4 + "</b></color>";
		}
		if (component.unlockYear == 99999)
		{
			text = this.tS_.GetText(1217);
		}
		base.GetComponent<tooltip>().c = text;
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0003D590 File Offset: 0x0003B790
	private string GetQualitatStars(int i)
	{
		string result;
		switch (i)
		{
		case 0:
			result = "☆☆☆☆☆";
			break;
		case 1:
			result = "★☆☆☆☆";
			break;
		case 2:
			result = "★★☆☆☆";
			break;
		case 3:
			result = "★★★☆☆";
			break;
		case 4:
			result = "★★★★☆";
			break;
		case 5:
			result = "★★★★★";
			break;
		default:
			result = "☆☆☆☆☆";
			break;
		}
		return result;
	}

	// Token: 0x040009E2 RID: 2530
	public GameObject[] uiObjects;

	// Token: 0x040009E3 RID: 2531
	public int typ;

	// Token: 0x040009E4 RID: 2532
	public Color[] colors;

	// Token: 0x040009E5 RID: 2533
	public mainScript mS_;

	// Token: 0x040009E6 RID: 2534
	public textScript tS_;

	// Token: 0x040009E7 RID: 2535
	public mapScript mapS_;

	// Token: 0x040009E8 RID: 2536
	public GUI_Main guiMain_;

	// Token: 0x040009E9 RID: 2537
	public sfxScript sfx_;
}
