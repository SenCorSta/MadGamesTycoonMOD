using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007D RID: 125
public class Item_DevEngine_Platform : MonoBehaviour
{
	// Token: 0x0600052F RID: 1327 RVA: 0x00047AB4 File Offset: 0x00045CB4
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00047ABC File Offset: 0x00045CBC
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		string text = this.pS_.GetDateString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(219),
			": ",
			this.pS_.GetMarktanteilString()
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.guiMain_.DrawStars(this.uiObjects[7], this.pS_.erfahrung);
		this.uiObjects[9].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		if (this.pS_.internet)
		{
			this.uiObjects[4].SetActive(true);
		}
		else
		{
			this.uiObjects[4].SetActive(false);
		}
		text = "";
		if (this.pS_.needFeatures[0] != -1)
		{
			text = this.gF_.GetName(this.pS_.needFeatures[0]);
		}
		if (this.pS_.needFeatures[1] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.pS_.needFeatures[1]);
		}
		if (this.pS_.needFeatures[2] != -1)
		{
			text = text + "\n" + this.gF_.GetName(this.pS_.needFeatures[2]);
		}
		this.uiObjects[5].GetComponent<Text>().text = text;
		this.tooltip_.c = this.pS_.GetTooltip();
		this.uiObjects[10].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[10].GetComponent<tooltip>().c = this.pS_.GetTypString();
		if (this.pS_.vomMarktGenommen)
		{
			this.uiObjects[3].SetActive(true);
		}
		else
		{
			this.uiObjects[8].SetActive(false);
		}
		if (this.pS_.tech < this.menuDevEngine_.techLevel)
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip = this.tooltip_;
			tooltip.c = tooltip.c + "\n\n<color=red><b>" + this.tS_.GetText(378) + "</b></color>";
			base.gameObject.GetComponent<Button>().interactable = false;
			return;
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00047D71 File Offset: 0x00045F71
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00047DA4 File Offset: 0x00045FA4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, false);
		this.menuDevEngine_.SetSpezialplatform(this.myID);
		this.guiMain_.uiObjects[237].GetComponent<Menu_Dev_EnginePlatform>().BUTTON_Close();
	}

	// Token: 0x04000825 RID: 2085
	public int myID;

	// Token: 0x04000826 RID: 2086
	public GameObject[] uiObjects;

	// Token: 0x04000827 RID: 2087
	public mainScript mS_;

	// Token: 0x04000828 RID: 2088
	public textScript tS_;

	// Token: 0x04000829 RID: 2089
	public sfxScript sfx_;

	// Token: 0x0400082A RID: 2090
	public GUI_Main guiMain_;

	// Token: 0x0400082B RID: 2091
	public tooltip tooltip_;

	// Token: 0x0400082C RID: 2092
	public platformScript pS_;

	// Token: 0x0400082D RID: 2093
	public gameplayFeatures gF_;

	// Token: 0x0400082E RID: 2094
	public Menu_Dev_Engine menuDevEngine_;

	// Token: 0x0400082F RID: 2095
	private float updateTimer;
}
