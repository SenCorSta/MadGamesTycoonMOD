using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BC RID: 188
public class Item_GamesList : MonoBehaviour
{
	// Token: 0x0600069E RID: 1694 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00005C01 File Offset: 0x00003E01
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00063BC8 File Offset: 0x00061DC8
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData(this.uiObjects[2].GetComponent<Text>().text);
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x00063C28 File Offset: 0x00061E28
	public void SetData(string c)
	{
		this.uiObjects[2].GetComponent<Text>().text = c;
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00063CF8 File Offset: 0x00061EF8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[46]);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000A4D RID: 2637
	public GameObject[] uiObjects;

	// Token: 0x04000A4E RID: 2638
	public mainScript mS_;

	// Token: 0x04000A4F RID: 2639
	public textScript tS_;

	// Token: 0x04000A50 RID: 2640
	public sfxScript sfx_;

	// Token: 0x04000A51 RID: 2641
	public GUI_Main guiMain_;

	// Token: 0x04000A52 RID: 2642
	public tooltip tooltip_;

	// Token: 0x04000A53 RID: 2643
	public gameScript game_;

	// Token: 0x04000A54 RID: 2644
	public genres genres_;

	// Token: 0x04000A55 RID: 2645
	private float updateTimer;
}
