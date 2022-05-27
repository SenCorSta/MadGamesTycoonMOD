using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C7 RID: 199
public class Item_PersonalGroup : MonoBehaviour
{
	// Token: 0x060006EC RID: 1772 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0005274C File Offset: 0x0005094C
	private void Update()
	{
		if (!this.cS_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
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
		}
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x000527D8 File Offset: 0x000509D8
	public void EnableObjects()
	{
		if (this.hasEnabled)
		{
			return;
		}
		this.hasEnabled = true;
		for (int i = 0; i < this.uiObjects.Length; i++)
		{
			if (this.uiObjects[i] && !this.uiObjects[i].activeSelf)
			{
				this.uiObjects[i].SetActive(true);
			}
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00052834 File Offset: 0x00050A34
	public void SetData(string s, float val)
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.cS_.myName;
		this.uiObjects[10].GetComponent<Text>().text = this.tS_.GetText(137 + this.cS_.beruf);
		this.uiObjects[1].GetComponent<Text>().text = s;
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		this.uiObjects[3].GetComponent<Image>().fillAmount = val * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(val);
		this.uiObjects[4].GetComponent<Image>().fillAmount = this.cS_.s_motivation * 0.01f;
		this.uiObjects[4].GetComponent<Image>().color = this.GetValColor(this.cS_.s_motivation);
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.cS_.s_motivation).ToString();
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cS_.GetGehalt(), true);
		this.guiMain_.CreatePerkIcons(this.cS_, this.uiObjects[7].transform);
		if (this.cS_.roomS_)
		{
			this.uiObjects[9].GetComponent<Image>().sprite = this.rdS_.roomData_SPRITE[this.cS_.roomS_.typ];
		}
		if (this.cS_.krank > 0)
		{
			this.uiObjects[8].SetActive(true);
			return;
		}
		this.uiObjects[8].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00052A40 File Offset: 0x00050C40
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

	// Token: 0x060006F2 RID: 1778 RVA: 0x00052AB4 File Offset: 0x00050CB4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		Menu_PersonalGroups component = this.guiMain_.uiObjects[32].GetComponent<Menu_PersonalGroups>();
		if (this.cS_.group == -1)
		{
			this.cS_.group = component.uiObjects[6].GetComponent<Dropdown>().value + 1;
			base.gameObject.transform.parent = component.uiObjects[4].transform;
		}
		else
		{
			this.cS_.group = -1;
			base.gameObject.transform.parent = component.uiObjects[0].transform;
		}
		component.DROPDOWN_Sort();
	}

	// Token: 0x04000AAB RID: 2731
	public int characterID = -1;

	// Token: 0x04000AAC RID: 2732
	public characterScript cS_;

	// Token: 0x04000AAD RID: 2733
	public GameObject[] uiObjects;

	// Token: 0x04000AAE RID: 2734
	public mainScript mS_;

	// Token: 0x04000AAF RID: 2735
	public textScript tS_;

	// Token: 0x04000AB0 RID: 2736
	public sfxScript sfx_;

	// Token: 0x04000AB1 RID: 2737
	public GUI_Main guiMain_;

	// Token: 0x04000AB2 RID: 2738
	public tooltip tooltip_;

	// Token: 0x04000AB3 RID: 2739
	public roomDataScript rdS_;

	// Token: 0x04000AB4 RID: 2740
	private RectTransform myRect_;

	// Token: 0x04000AB5 RID: 2741
	private int frames;

	// Token: 0x04000AB6 RID: 2742
	private bool hasEnabled;
}
