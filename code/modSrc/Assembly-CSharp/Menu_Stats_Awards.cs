using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000230 RID: 560
public class Menu_Stats_Awards : MonoBehaviour
{
	// Token: 0x06001598 RID: 5528 RVA: 0x000DC337 File Offset: 0x000DA537
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000DC340 File Offset: 0x000DA540
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000DC3EA File Offset: 0x000DA5EA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000DC3F4 File Offset: 0x000DA5F4
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

	// Token: 0x0600159C RID: 5532 RVA: 0x000DC440 File Offset: 0x000DA640
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x000DC45B File Offset: 0x000DA65B
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x000DC470 File Offset: 0x000DA670
	private void SetData()
	{
		if (this.pS_)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.pS_.awards[4].ToString() + "x";
			this.uiObjects[1].GetComponent<Text>().text = this.pS_.awards[2].ToString() + "x";
			this.uiObjects[2].GetComponent<Text>().text = this.pS_.awards[3].ToString() + "x";
			this.uiObjects[3].GetComponent<Text>().text = this.pS_.awards[0].ToString() + "x";
			this.uiObjects[4].GetComponent<Text>().text = this.pS_.awards[1].ToString() + "x";
			this.uiObjects[5].GetComponent<Text>().text = this.pS_.awards[8].ToString() + "x";
			this.uiObjects[6].GetComponent<Text>().text = this.pS_.awards[7].ToString() + "x";
			this.uiObjects[7].GetComponent<Text>().text = this.pS_.awards[6].ToString() + "x";
			this.uiObjects[8].GetComponent<Text>().text = this.pS_.awards[5].ToString() + "x";
			this.uiObjects[9].GetComponent<Text>().text = this.pS_.awards[9].ToString() + "x";
			this.uiObjects[10].GetComponent<Text>().text = this.pS_.awards[10].ToString() + "x";
			this.uiObjects[11].GetComponent<Text>().text = this.pS_.awards[11].ToString() + "x";
		}
	}

	// Token: 0x04001989 RID: 6537
	public GameObject[] uiObjects;

	// Token: 0x0400198A RID: 6538
	private roomScript rS_;

	// Token: 0x0400198B RID: 6539
	private GameObject main_;

	// Token: 0x0400198C RID: 6540
	private mainScript mS_;

	// Token: 0x0400198D RID: 6541
	private textScript tS_;

	// Token: 0x0400198E RID: 6542
	private GUI_Main guiMain_;

	// Token: 0x0400198F RID: 6543
	private sfxScript sfx_;

	// Token: 0x04001990 RID: 6544
	private publisherScript pS_;

	// Token: 0x04001991 RID: 6545
	private float updateTimer;
}
