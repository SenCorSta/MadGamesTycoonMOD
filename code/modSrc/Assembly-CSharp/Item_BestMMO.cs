using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E4 RID: 228
public class Item_BestMMO : MonoBehaviour
{
	// Token: 0x060007A2 RID: 1954 RVA: 0x0000618B File Offset: 0x0000438B
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0006852C File Offset: 0x0006672C
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && !this.game_.playerGame && this.game_.multiplayerSlot != -1)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (this.sort == 0)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.abonnements, false);
		}
		if (this.sort == 1)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.bestAbonnements, false);
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x000686A0 File Offset: 0x000668A0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000BAC RID: 2988
	public GameObject[] uiObjects;

	// Token: 0x04000BAD RID: 2989
	public mainScript mS_;

	// Token: 0x04000BAE RID: 2990
	public textScript tS_;

	// Token: 0x04000BAF RID: 2991
	public sfxScript sfx_;

	// Token: 0x04000BB0 RID: 2992
	public GUI_Main guiMain_;

	// Token: 0x04000BB1 RID: 2993
	public tooltip tooltip_;

	// Token: 0x04000BB2 RID: 2994
	public gameScript game_;

	// Token: 0x04000BB3 RID: 2995
	public genres genres_;

	// Token: 0x04000BB4 RID: 2996
	public int sort;
}
