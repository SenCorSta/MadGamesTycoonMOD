using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EA RID: 234
public class Item_MyGames_Auftragsspiel : MonoBehaviour
{
	// Token: 0x060007C2 RID: 1986 RVA: 0x00006279 File Offset: 0x00004479
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00068E4C File Offset: 0x0006704C
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		base.gameObject.name = this.game_.reviewTotal.ToString();
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00068F3C File Offset: 0x0006713C
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		if (!this.game_.pS_)
		{
			this.game_.FindMyPublisher();
		}
		if (this.game_.pS_)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.game_.pS_.GetLogo();
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00069068 File Offset: 0x00067268
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000BDE RID: 3038
	public GameObject[] uiObjects;

	// Token: 0x04000BDF RID: 3039
	public mainScript mS_;

	// Token: 0x04000BE0 RID: 3040
	public textScript tS_;

	// Token: 0x04000BE1 RID: 3041
	public sfxScript sfx_;

	// Token: 0x04000BE2 RID: 3042
	public GUI_Main guiMain_;

	// Token: 0x04000BE3 RID: 3043
	public tooltip tooltip_;

	// Token: 0x04000BE4 RID: 3044
	public gameScript game_;

	// Token: 0x04000BE5 RID: 3045
	public genres genres_;
}
