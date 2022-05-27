using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BC RID: 188
public class Item_GamesList : MonoBehaviour
{
	// Token: 0x060006A7 RID: 1703 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x000513DD File Offset: 0x0004F5DD
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x000513E8 File Offset: 0x0004F5E8
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

	// Token: 0x060006AA RID: 1706 RVA: 0x00051448 File Offset: 0x0004F648
	public void SetData(string c)
	{
		this.uiObjects[2].GetComponent<Text>().text = c;
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0005153C File Offset: 0x0004F73C
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
