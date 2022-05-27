using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E0 RID: 224
public class Item_AllTimeCharts : MonoBehaviour
{
	// Token: 0x06000791 RID: 1937 RVA: 0x000559F2 File Offset: 0x00053BF2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x000559FC File Offset: 0x00053BFC
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && this.game_.GameFromMitspieler())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (this.guiMain_.uiObjects[375].activeSelf)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.umsatzTotal, true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		base.StartCoroutine(this.iSetTooltip());
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00055B78 File Offset: 0x00053D78
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		if (this.guiMain_.uiObjects[375].activeSelf)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.umsatzTotal, true);
			base.gameObject.name = this.game_.umsatzTotal.ToString();
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00055C60 File Offset: 0x00053E60
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00055CAC File Offset: 0x00053EAC
	private IEnumerator iSetTooltip()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 1f));
		if (this.game_)
		{
			this.tooltip_.c = this.game_.GetTooltip();
		}
		yield break;
	}

	// Token: 0x04000B8E RID: 2958
	public GameObject[] uiObjects;

	// Token: 0x04000B8F RID: 2959
	public mainScript mS_;

	// Token: 0x04000B90 RID: 2960
	public textScript tS_;

	// Token: 0x04000B91 RID: 2961
	public sfxScript sfx_;

	// Token: 0x04000B92 RID: 2962
	public GUI_Main guiMain_;

	// Token: 0x04000B93 RID: 2963
	public tooltip tooltip_;

	// Token: 0x04000B94 RID: 2964
	public gameScript game_;

	// Token: 0x04000B95 RID: 2965
	public genres genres_;
}
