using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C6 RID: 198
public class Item_Arbeitsmarkt : MonoBehaviour
{
	// Token: 0x060006E4 RID: 1764 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x000524DE File Offset: 0x000506DE
	private void Update()
	{
		if (!this.charAM_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x000524F8 File Offset: 0x000506F8
	public void SetData(string s, float val)
	{
		if (!this.charAM_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.charAM_.myName;
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(137 + this.charAM_.beruf);
		this.uiObjects[1].GetComponent<Text>().text = s;
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		this.uiObjects[3].GetComponent<Image>().fillAmount = val * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(val);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.charAM_.GetGehalt(), true);
		this.guiMain_.CreatePerkIconsArbeitsmarkt(this.charAM_, this.uiObjects[7].transform);
		if (this.mS_.multiplayer && this.uiObjects[4].activeSelf)
		{
			this.uiObjects[4].SetActive(false);
		}
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00052640 File Offset: 0x00050840
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000526B4 File Offset: 0x000508B4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[31]);
		this.guiMain_.uiObjects[31].GetComponent<Menu_PersonalViewArbeitsmarkt>().Init(this.charAM_);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00052705 File Offset: 0x00050905
	public void BUTTON_Remove()
	{
		this.sfx_.PlaySound(3, true);
		if (this.charAM_)
		{
			UnityEngine.Object.Destroy(this.charAM_.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000AA3 RID: 2723
	public int characterID = -1;

	// Token: 0x04000AA4 RID: 2724
	public charArbeitsmarkt charAM_;

	// Token: 0x04000AA5 RID: 2725
	public GameObject[] uiObjects;

	// Token: 0x04000AA6 RID: 2726
	public mainScript mS_;

	// Token: 0x04000AA7 RID: 2727
	public textScript tS_;

	// Token: 0x04000AA8 RID: 2728
	public sfxScript sfx_;

	// Token: 0x04000AA9 RID: 2729
	public GUI_Main guiMain_;

	// Token: 0x04000AAA RID: 2730
	public tooltip tooltip_;
}
