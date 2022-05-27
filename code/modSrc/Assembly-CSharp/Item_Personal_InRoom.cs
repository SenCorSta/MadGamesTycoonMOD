﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C8 RID: 200
public class Item_Personal_InRoom : MonoBehaviour
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x00052B6C File Offset: 0x00050D6C
	private void Update()
	{
		if (!this.cS_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.frames++;
		if (this.frames < 3)
		{
			return;
		}
		if (!this.myRect_)
		{
			this.myRect_ = base.GetComponent<RectTransform>();
		}
		if (this.myRect_.position.y >= 0f && this.myRect_.position.y <= (float)Screen.height)
		{
			this.EnableObjects();
			if (this.cS_.group != -1)
			{
				this.uiObjects[0].GetComponent<Text>().text = this.cS_.GetGroupString("magenta") + " " + this.cS_.myName;
			}
			else
			{
				this.uiObjects[0].GetComponent<Text>().text = this.cS_.myName;
			}
			this.uiObjects[4].GetComponent<Image>().fillAmount = this.cS_.s_motivation * 0.01f;
			this.uiObjects[4].GetComponent<Image>().color = this.GetValColor(this.cS_.s_motivation);
			this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.cS_.s_motivation).ToString();
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00052CD4 File Offset: 0x00050ED4
	public void EnableObjects()
	{
		for (int i = 0; i < this.uiObjects.Length; i++)
		{
			if (this.uiObjects[i] && !this.uiObjects[i].activeSelf)
			{
				this.uiObjects[i].SetActive(true);
			}
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00052D20 File Offset: 0x00050F20
	public void SetData(string s, float val)
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[11].GetComponent<Text>().text = this.tS_.GetText(137 + this.cS_.beruf);
		this.uiObjects[1].GetComponent<Text>().text = s;
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		this.uiObjects[3].GetComponent<Image>().fillAmount = val * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(val);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cS_.GetGehalt(), true);
		this.guiMain_.CreatePerkIcons(this.cS_, this.uiObjects[7].transform);
		if (this.cS_.roomS_)
		{
			this.uiObjects[9].GetComponent<Image>().sprite = this.rdS_.roomData_SPRITE[this.cS_.roomS_.typ];
		}
		if (this.cS_.krank <= 0)
		{
			this.uiObjects[10].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
		}
		this.Update();
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00052E94 File Offset: 0x00051094
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

	// Token: 0x060006F9 RID: 1785 RVA: 0x00052F08 File Offset: 0x00051108
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[28]);
		this.guiMain_.uiObjects[28].GetComponent<Menu_PersonalView>().Init(this.cS_);
	}

	// Token: 0x04000AB7 RID: 2743
	public int characterID = -1;

	// Token: 0x04000AB8 RID: 2744
	public characterScript cS_;

	// Token: 0x04000AB9 RID: 2745
	public GameObject[] uiObjects;

	// Token: 0x04000ABA RID: 2746
	public mainScript mS_;

	// Token: 0x04000ABB RID: 2747
	public textScript tS_;

	// Token: 0x04000ABC RID: 2748
	public sfxScript sfx_;

	// Token: 0x04000ABD RID: 2749
	public GUI_Main guiMain_;

	// Token: 0x04000ABE RID: 2750
	public tooltip tooltip_;

	// Token: 0x04000ABF RID: 2751
	public roomDataScript rdS_;

	// Token: 0x04000AC0 RID: 2752
	private RectTransform myRect_;

	// Token: 0x04000AC1 RID: 2753
	private int frames;
}
