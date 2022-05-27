using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CB RID: 459
public class Menu_Messe : MonoBehaviour
{
	// Token: 0x0600114F RID: 4431 RVA: 0x0000C1FA File Offset: 0x0000A3FA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000C38A8 File Offset: 0x000C1AA8
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

	// Token: 0x06001151 RID: 4433 RVA: 0x000C3954 File Offset: 0x000C1B54
	public void Init()
	{
		this.FindScripts();
		this.guiMain_.OpenMenu(false);
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.guiMain_.BUTTON_GameSpeed(0f);
			this.mS_.mpCalls_.SetPlayersUnready();
		}
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(0), true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(1), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(2), true);
		Menu_MesseErgebnis component = this.guiMain_.uiObjects[188].GetComponent<Menu_MesseErgebnis>();
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		int num2 = Mathf.RoundToInt((float)(Mathf.RoundToInt(350000f * component.curveBesucher.Evaluate(num / 600f)) + 1000 + UnityEngine.Random.Range(0, 1000)) * 1.5f);
		num2 = num2 / 1000 * 1000;
		string text = this.tS_.GetText(953);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)Mathf.RoundToInt((float)num2), false));
		this.uiObjects[3].GetComponent<Text>().text = text;
		if (this.mS_.settings_ && this.mS_.settings_.hideConvention)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.sfx_.PlaySound(50, false);
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0000C202 File Offset: 0x0000A402
	private void Update()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x000C3B20 File Offset: 0x000C1D20
	public int GetPrice(int i)
	{
		int num = this.mS_.year - 1975;
		if (num > 50)
		{
			num = 50;
		}
		return this.price[i] * num + 5000;
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x000C3B58 File Offset: 0x000C1D58
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			this.mS_.mpCalls_.CLIENT_Send_Command(1);
		}
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x000C3BBC File Offset: 0x000C1DBC
	public void BUTTON_Stand(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[186]);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().Init(i);
	}

	// Token: 0x040015DA RID: 5594
	public GameObject[] uiObjects;

	// Token: 0x040015DB RID: 5595
	public int[] price;

	// Token: 0x040015DC RID: 5596
	private GameObject main_;

	// Token: 0x040015DD RID: 5597
	private mainScript mS_;

	// Token: 0x040015DE RID: 5598
	private textScript tS_;

	// Token: 0x040015DF RID: 5599
	private GUI_Main guiMain_;

	// Token: 0x040015E0 RID: 5600
	private sfxScript sfx_;
}
