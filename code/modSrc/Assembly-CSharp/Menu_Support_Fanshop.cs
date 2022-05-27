using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000271 RID: 625
public class Menu_Support_Fanshop : MonoBehaviour
{
	// Token: 0x06001853 RID: 6227 RVA: 0x000F1ABA File Offset: 0x000EFCBA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x000F1AC4 File Offset: 0x000EFCC4
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

	// Token: 0x06001855 RID: 6229 RVA: 0x000F1BAA File Offset: 0x000EFDAA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000F1BB4 File Offset: 0x000EFDB4
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

	// Token: 0x06001857 RID: 6231 RVA: 0x000F1C00 File Offset: 0x000EFE00
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		this.SetData();
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000F1C18 File Offset: 0x000EFE18
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

	// Token: 0x06001859 RID: 6233 RVA: 0x000F1D18 File Offset: 0x000EFF18
	public void BUTTON_Fanshop()
	{
		this.sfx_.PlaySound(3, false);
		this.guiMain_.uiObjects[366].SetActive(true);
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x000F1D3E File Offset: 0x000EFF3E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x000F1D64 File Offset: 0x000EFF64
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

	// Token: 0x04001BFE RID: 7166
	public GameObject[] uiObjects;

	// Token: 0x04001BFF RID: 7167
	private roomScript rS_;

	// Token: 0x04001C00 RID: 7168
	private GameObject main_;

	// Token: 0x04001C01 RID: 7169
	private mainScript mS_;

	// Token: 0x04001C02 RID: 7170
	private textScript tS_;

	// Token: 0x04001C03 RID: 7171
	private GUI_Main guiMain_;

	// Token: 0x04001C04 RID: 7172
	private sfxScript sfx_;

	// Token: 0x04001C05 RID: 7173
	private unlockScript unlock_;

	// Token: 0x04001C06 RID: 7174
	private games games_;

	// Token: 0x04001C07 RID: 7175
	public Color[] colors;

	// Token: 0x04001C08 RID: 7176
	private float updateTimer;
}
