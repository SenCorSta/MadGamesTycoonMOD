using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000097 RID: 151
public class Item_LeitenderDesigner : MonoBehaviour
{
	// Token: 0x060005D7 RID: 1495 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0004BCC4 File Offset: 0x00049EC4
	private void Update()
	{
		if (!this.cS_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
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

	// Token: 0x060005D9 RID: 1497 RVA: 0x0004BDC0 File Offset: 0x00049FC0
	public void SetData(string s, float val)
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = s;
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		this.uiObjects[3].GetComponent<Image>().fillAmount = val * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(val);
		this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(137 + this.cS_.beruf);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cS_.GetGehalt(), true);
		this.guiMain_.CreatePerkIcons(this.cS_, this.uiObjects[7].transform);
		if (this.cS_.roomS_)
		{
			this.uiObjects[9].GetComponent<Image>().sprite = this.rdS_.roomData_SPRITE[this.cS_.roomS_.typ];
		}
		if (this.cS_.krank > 0)
		{
			this.uiObjects[10].SetActive(true);
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0004BF1C File Offset: 0x0004A11C
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

	// Token: 0x060005DC RID: 1500 RVA: 0x0004BF90 File Offset: 0x0004A190
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[28]);
		this.guiMain_.uiObjects[28].GetComponent<Menu_PersonalView>().Init(this.cS_);
	}

	// Token: 0x04000911 RID: 2321
	public int characterID = -1;

	// Token: 0x04000912 RID: 2322
	public characterScript cS_;

	// Token: 0x04000913 RID: 2323
	public GameObject[] uiObjects;

	// Token: 0x04000914 RID: 2324
	public mainScript mS_;

	// Token: 0x04000915 RID: 2325
	public textScript tS_;

	// Token: 0x04000916 RID: 2326
	public sfxScript sfx_;

	// Token: 0x04000917 RID: 2327
	public GUI_Main guiMain_;

	// Token: 0x04000918 RID: 2328
	public tooltip tooltip_;

	// Token: 0x04000919 RID: 2329
	public roomDataScript rdS_;
}
