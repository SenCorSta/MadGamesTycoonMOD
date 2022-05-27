using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CC RID: 460
public class Menu_Messe : MonoBehaviour
{
	// Token: 0x06001169 RID: 4457 RVA: 0x000B8205 File Offset: 0x000B6405
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x000B8210 File Offset: 0x000B6410
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

	// Token: 0x0600116B RID: 4459 RVA: 0x000B82BC File Offset: 0x000B64BC
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

	// Token: 0x0600116C RID: 4460 RVA: 0x000B8485 File Offset: 0x000B6685
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

	// Token: 0x0600116D RID: 4461 RVA: 0x000B84B0 File Offset: 0x000B66B0
	public int GetPrice(int i)
	{
		int num = this.mS_.year - 1975;
		if (num > 50)
		{
			num = 50;
		}
		return this.price[i] * num + 5000;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000B84E8 File Offset: 0x000B66E8
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

	// Token: 0x0600116F RID: 4463 RVA: 0x000B854C File Offset: 0x000B674C
	public void BUTTON_Stand(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[186]);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().Init(i);
	}

	// Token: 0x040015E3 RID: 5603
	public GameObject[] uiObjects;

	// Token: 0x040015E4 RID: 5604
	public int[] price;

	// Token: 0x040015E5 RID: 5605
	private GameObject main_;

	// Token: 0x040015E6 RID: 5606
	private mainScript mS_;

	// Token: 0x040015E7 RID: 5607
	private textScript tS_;

	// Token: 0x040015E8 RID: 5608
	private GUI_Main guiMain_;

	// Token: 0x040015E9 RID: 5609
	private sfxScript sfx_;
}
