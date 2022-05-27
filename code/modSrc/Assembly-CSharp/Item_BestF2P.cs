using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E2 RID: 226
public class Item_BestF2P : MonoBehaviour
{
	// Token: 0x06000795 RID: 1941 RVA: 0x00006183 File Offset: 0x00004383
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00067F9C File Offset: 0x0006619C
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
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x000680D4 File Offset: 0x000662D4
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00068160 File Offset: 0x00066360
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000B99 RID: 2969
	public GameObject[] uiObjects;

	// Token: 0x04000B9A RID: 2970
	public mainScript mS_;

	// Token: 0x04000B9B RID: 2971
	public textScript tS_;

	// Token: 0x04000B9C RID: 2972
	public sfxScript sfx_;

	// Token: 0x04000B9D RID: 2973
	public GUI_Main guiMain_;

	// Token: 0x04000B9E RID: 2974
	public tooltip tooltip_;

	// Token: 0x04000B9F RID: 2975
	public gameScript game_;

	// Token: 0x04000BA0 RID: 2976
	public genres genres_;
}
