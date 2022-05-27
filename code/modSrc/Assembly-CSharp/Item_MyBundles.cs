using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E9 RID: 233
public class Item_MyBundles : MonoBehaviour
{
	// Token: 0x060007BC RID: 1980 RVA: 0x00006271 File Offset: 0x00004471
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00068BBC File Offset: 0x00066DBC
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

	// Token: 0x060007BE RID: 1982 RVA: 0x00068CAC File Offset: 0x00066EAC
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
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00068DA4 File Offset: 0x00066FA4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_.typ_bundle)
		{
			this.guiMain_.uiObjects[269].SetActive(true);
			this.guiMain_.uiObjects[269].GetComponent<Menu_BundleView>().Init(this.game_);
			return;
		}
		if (this.game_.typ_bundleAddon)
		{
			this.guiMain_.uiObjects[273].SetActive(true);
			this.guiMain_.uiObjects[273].GetComponent<Menu_BundleView>().Init(this.game_);
			return;
		}
	}

	// Token: 0x04000BD6 RID: 3030
	public GameObject[] uiObjects;

	// Token: 0x04000BD7 RID: 3031
	public mainScript mS_;

	// Token: 0x04000BD8 RID: 3032
	public textScript tS_;

	// Token: 0x04000BD9 RID: 3033
	public sfxScript sfx_;

	// Token: 0x04000BDA RID: 3034
	public GUI_Main guiMain_;

	// Token: 0x04000BDB RID: 3035
	public tooltip tooltip_;

	// Token: 0x04000BDC RID: 3036
	public gameScript game_;

	// Token: 0x04000BDD RID: 3037
	public genres genres_;
}
