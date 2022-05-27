using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008A RID: 138
public class Item_DevGame_Platform : MonoBehaviour
{
	// Token: 0x06000586 RID: 1414 RVA: 0x00049F10 File Offset: 0x00048110
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00049F18 File Offset: 0x00048118
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		string text = this.pS_.GetDateString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(220),
			": <b>",
			this.pS_.GetGames().ToString(),
			"</b>"
		});
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(219),
			": <b>",
			this.pS_.GetMarktanteilString(),
			"</b>"
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
		this.tooltip_.c = this.pS_.GetTooltip();
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(375) + ": " + this.mS_.GetMoney((long)this.pS_.GetDevCosts(), true);
		this.uiObjects[11].GetComponent<Text>().text = string.Concat(new object[]
		{
			this.tS_.GetText(1926),
			": -",
			Mathf.RoundToInt(this.pS_.GetExklusivBonus()),
			"%"
		});
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
		if (this.devGame_ && (this.devGame_.g_GamePlatform[0] == this.pS_.myID || this.devGame_.g_GamePlatform[1] == this.pS_.myID || this.devGame_.g_GamePlatform[2] == this.pS_.myID || this.devGame_.g_GamePlatform[3] == this.pS_.myID))
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip = this.tooltip_;
			tooltip.c = tooltip.c + "\n\n<color=red><b>" + this.tS_.GetText(379) + "</b></color>";
			base.gameObject.GetComponent<Button>().interactable = false;
			return;
		}
		if (this.changePlatform_ && (this.changePlatform_.g_GamePlatform[0] == this.pS_.myID || this.changePlatform_.g_GamePlatform[1] == this.pS_.myID || this.changePlatform_.g_GamePlatform[2] == this.pS_.myID || this.changePlatform_.g_GamePlatform[3] == this.pS_.myID))
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip2 = this.tooltip_;
			tooltip2.c = tooltip2.c + "\n\n<color=red><b>" + this.tS_.GetText(379) + "</b></color>";
			base.gameObject.GetComponent<Button>().interactable = false;
			return;
		}
		if (this.pS_.ownerID == this.mS_.myID && !this.pS_.isUnlocked)
		{
			this.uiObjects[3].SetActive(true);
			tooltip tooltip3 = this.tooltip_;
			tooltip3.c = tooltip3.c + "\n\n<color=red><b>" + this.tS_.GetText(1633) + "</b></color>";
		}
		if (this.devGame_)
		{
			this.devGame_.GetEngineTechLevel();
		}
		if (this.changePlatform_)
		{
			this.changePlatform_.GetEngineTechLevel();
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0004A3D6 File Offset: 0x000485D6
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

	// Token: 0x06000589 RID: 1417 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0004A40C File Offset: 0x0004860C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, false);
		if (this.devGame_)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetPlatform(this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().platformNR, this.myID);
		}
		if (this.changePlatform_)
		{
			this.guiMain_.uiObjects[102].GetComponent<Menu_Dev_ChangePlatform>().SetPlatform(this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().platformNR, this.myID, false);
		}
		this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().BUTTON_Close();
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0004A4C5 File Offset: 0x000486C5
	private bool IsExclusivGame()
	{
		return this.guiMain_.uiObjects[56].activeSelf && this.devGame_.uiObjects[146].GetComponent<Dropdown>().value == 1;
	}

	// Token: 0x040008A3 RID: 2211
	public int myID;

	// Token: 0x040008A4 RID: 2212
	public GameObject[] uiObjects;

	// Token: 0x040008A5 RID: 2213
	public mainScript mS_;

	// Token: 0x040008A6 RID: 2214
	public textScript tS_;

	// Token: 0x040008A7 RID: 2215
	public sfxScript sfx_;

	// Token: 0x040008A8 RID: 2216
	public GUI_Main guiMain_;

	// Token: 0x040008A9 RID: 2217
	public tooltip tooltip_;

	// Token: 0x040008AA RID: 2218
	public platformScript pS_;

	// Token: 0x040008AB RID: 2219
	public Menu_DevGame devGame_;

	// Token: 0x040008AC RID: 2220
	public Menu_Dev_ChangePlatform changePlatform_;

	// Token: 0x040008AD RID: 2221
	private float updateTimer;
}
