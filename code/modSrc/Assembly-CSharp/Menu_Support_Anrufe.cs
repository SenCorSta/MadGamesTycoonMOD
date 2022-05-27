using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026F RID: 623
public class Menu_Support_Anrufe : MonoBehaviour
{
	// Token: 0x06001840 RID: 6208 RVA: 0x000F1142 File Offset: 0x000EF342
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x000F114C File Offset: 0x000EF34C
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

	// Token: 0x06001842 RID: 6210 RVA: 0x000F1214 File Offset: 0x000EF414
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x000F121C File Offset: 0x000EF41C
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

	// Token: 0x06001844 RID: 6212 RVA: 0x000F1268 File Offset: 0x000EF468
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.SetData();
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x000F1280 File Offset: 0x000EF480
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

	// Token: 0x06001846 RID: 6214 RVA: 0x000F1388 File Offset: 0x000EF588
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000F13B0 File Offset: 0x000EF5B0
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

	// Token: 0x04001BE6 RID: 7142
	public GameObject[] uiObjects;

	// Token: 0x04001BE7 RID: 7143
	private roomScript rS_;

	// Token: 0x04001BE8 RID: 7144
	private GameObject main_;

	// Token: 0x04001BE9 RID: 7145
	private mainScript mS_;

	// Token: 0x04001BEA RID: 7146
	private textScript tS_;

	// Token: 0x04001BEB RID: 7147
	private GUI_Main guiMain_;

	// Token: 0x04001BEC RID: 7148
	private sfxScript sfx_;

	// Token: 0x04001BED RID: 7149
	private unlockScript unlock_;

	// Token: 0x04001BEE RID: 7150
	public Color[] colors;

	// Token: 0x04001BEF RID: 7151
	private float updateTimer;
}
