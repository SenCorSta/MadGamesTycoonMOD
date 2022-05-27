using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026D RID: 621
public class Menu_Support_Fanshop : MonoBehaviour
{
	// Token: 0x06001810 RID: 6160 RVA: 0x00010AF3 File Offset: 0x0000ECF3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000F6E30 File Offset: 0x000F5030
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
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

	// Token: 0x06001812 RID: 6162 RVA: 0x00010AFB File Offset: 0x0000ECFB
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x000F6F18 File Offset: 0x000F5118
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

	// Token: 0x06001814 RID: 6164 RVA: 0x00010B03 File Offset: 0x0000ED03
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.SetData();
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x000F6F64 File Offset: 0x000F5164
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		float num = 0f;
		for (int i = 0; i < this.games_.arrayMyIpScripts.Count; i++)
		{
			if (this.games_.arrayMyIpScripts[i])
			{
				for (int j = 0; j < this.games_.arrayMyIpScripts[i].merchBestellungen.Length; j++)
				{
					num += (float)this.games_.arrayMyIpScripts[i].merchBestellungen[j];
				}
			}
		}
		num /= 500f;
		this.guiMain_.DrawStars10_Color(this.uiObjects[0], Mathf.RoundToInt(num), Color.white);
		if (num <= 10f)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.Round(num, 2).ToString();
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = ">10.0";
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x00010B18 File Offset: 0x0000ED18
	public void BUTTON_Fanshop()
	{
		this.sfx_.PlaySound(3, false);
		this.guiMain_.uiObjects[366].SetActive(true);
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x00010B3E File Offset: 0x0000ED3E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x000F7064 File Offset: 0x000F5264
	public void BUTTON_Start()
	{
		this.sfx_.PlaySound(3, true);
		taskFanshop taskFanshop = this.guiMain_.AddTask_Fanshop();
		taskFanshop.Init(false);
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskFanshop.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001BE4 RID: 7140
	public GameObject[] uiObjects;

	// Token: 0x04001BE5 RID: 7141
	private roomScript rS_;

	// Token: 0x04001BE6 RID: 7142
	private GameObject main_;

	// Token: 0x04001BE7 RID: 7143
	private mainScript mS_;

	// Token: 0x04001BE8 RID: 7144
	private textScript tS_;

	// Token: 0x04001BE9 RID: 7145
	private GUI_Main guiMain_;

	// Token: 0x04001BEA RID: 7146
	private sfxScript sfx_;

	// Token: 0x04001BEB RID: 7147
	private unlockScript unlock_;

	// Token: 0x04001BEC RID: 7148
	private games games_;

	// Token: 0x04001BED RID: 7149
	public Color[] colors;

	// Token: 0x04001BEE RID: 7150
	private float updateTimer;
}
