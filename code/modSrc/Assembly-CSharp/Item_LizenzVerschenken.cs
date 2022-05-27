﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BD RID: 189
public class Item_LizenzVerschenken : MonoBehaviour
{
	// Token: 0x060006AE RID: 1710 RVA: 0x0005158D File Offset: 0x0004F78D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00051598 File Offset: 0x0004F798
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.licences_.GetSellPrice(this.myID), true);
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.licences_.licence_QUALITY[this.myID] / 20f));
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.licences_.licence_GEKAUFT[this.myID].ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.licences_.GetTypString(this.myID);
		this.tooltip_.c = this.licences_.GetTooltip(this.myID);
		if (this.menu_.selectedLizenz == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x000516F2 File Offset: 0x0004F8F2
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00051725 File Offset: 0x0004F925
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.menu_.selectedLizenz = this.myID;
		this.SetData();
	}

	// Token: 0x04000A56 RID: 2646
	public int myID = -1;

	// Token: 0x04000A57 RID: 2647
	public licences licences_;

	// Token: 0x04000A58 RID: 2648
	public GameObject[] uiObjects;

	// Token: 0x04000A59 RID: 2649
	public mainScript mS_;

	// Token: 0x04000A5A RID: 2650
	public textScript tS_;

	// Token: 0x04000A5B RID: 2651
	public sfxScript sfx_;

	// Token: 0x04000A5C RID: 2652
	public GUI_Main guiMain_;

	// Token: 0x04000A5D RID: 2653
	public tooltip tooltip_;

	// Token: 0x04000A5E RID: 2654
	public Menu_MP_LizenzSchenken menu_;

	// Token: 0x04000A5F RID: 2655
	private float updateTimer;
}
