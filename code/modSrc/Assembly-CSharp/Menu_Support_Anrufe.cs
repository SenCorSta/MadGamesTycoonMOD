using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026B RID: 619
public class Menu_Support_Anrufe : MonoBehaviour
{
	// Token: 0x060017FD RID: 6141 RVA: 0x00010A1A File Offset: 0x0000EC1A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x000F65C4 File Offset: 0x000F47C4
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
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

	// Token: 0x060017FF RID: 6143 RVA: 0x00010A22 File Offset: 0x0000EC22
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x000F668C File Offset: 0x000F488C
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

	// Token: 0x06001801 RID: 6145 RVA: 0x00010A2A File Offset: 0x0000EC2A
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.SetData();
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x000F66D8 File Offset: 0x000F48D8
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		float anrufe100Prozent = this.mS_.GetAnrufe100Prozent();
		this.uiObjects[0].GetComponent<Text>().text = Mathf.RoundToInt(anrufe100Prozent).ToString() + "%";
		this.uiObjects[1].GetComponent<Image>().fillAmount = anrufe100Prozent * 0.01f;
		this.uiObjects[1].GetComponent<Image>().color = this.colors[this.mS_.GetAnrufeAmount()];
		this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(754 + this.mS_.GetAnrufeAmount());
		string text = this.tS_.GetText(758);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.mS_.anrufe, false));
		this.uiObjects[3].GetComponent<Text>().text = text;
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x00010A3F File Offset: 0x0000EC3F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000F67E0 File Offset: 0x000F49E0
	public void BUTTON_Start()
	{
		this.sfx_.PlaySound(3, true);
		taskSupport taskSupport = this.guiMain_.AddTask_Support();
		taskSupport.Init(false);
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskSupport.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001BCC RID: 7116
	public GameObject[] uiObjects;

	// Token: 0x04001BCD RID: 7117
	private roomScript rS_;

	// Token: 0x04001BCE RID: 7118
	private GameObject main_;

	// Token: 0x04001BCF RID: 7119
	private mainScript mS_;

	// Token: 0x04001BD0 RID: 7120
	private textScript tS_;

	// Token: 0x04001BD1 RID: 7121
	private GUI_Main guiMain_;

	// Token: 0x04001BD2 RID: 7122
	private sfxScript sfx_;

	// Token: 0x04001BD3 RID: 7123
	private unlockScript unlock_;

	// Token: 0x04001BD4 RID: 7124
	public Color[] colors;

	// Token: 0x04001BD5 RID: 7125
	private float updateTimer;
}
