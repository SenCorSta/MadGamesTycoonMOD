using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C2 RID: 194
public class Item_Marketing_Konsole : MonoBehaviour
{
	// Token: 0x060006C3 RID: 1731 RVA: 0x00005CD8 File Offset: 0x00003ED8
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00005CE0 File Offset: 0x00003EE0
	private void Update()
	{
		if (this.pS_ && this.pS_.vomMarktGenommen)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0006456C File Offset: 0x0006276C
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
		this.SetData();
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x000645B8 File Offset: 0x000627B8
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		if (this.pS_.isUnlocked)
		{
			this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetDateString();
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(528);
		}
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00064680 File Offset: 0x00062880
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[322].SetActive(false);
		this.guiMain_.uiObjects[321].GetComponent<Menu_Marketing_KonsoleKampagne>().SetKonsole(this.pS_);
	}

	// Token: 0x04000A82 RID: 2690
	public GameObject[] uiObjects;

	// Token: 0x04000A83 RID: 2691
	public mainScript mS_;

	// Token: 0x04000A84 RID: 2692
	public textScript tS_;

	// Token: 0x04000A85 RID: 2693
	public sfxScript sfx_;

	// Token: 0x04000A86 RID: 2694
	public GUI_Main guiMain_;

	// Token: 0x04000A87 RID: 2695
	public tooltip tooltip_;

	// Token: 0x04000A88 RID: 2696
	public platformScript pS_;

	// Token: 0x04000A89 RID: 2697
	private float updateTimer;
}
