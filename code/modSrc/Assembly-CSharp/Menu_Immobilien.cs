using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000197 RID: 407
public class Menu_Immobilien : MonoBehaviour
{
	// Token: 0x06000F70 RID: 3952 RVA: 0x000A424D File Offset: 0x000A244D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x000A4258 File Offset: 0x000A2458
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x000A4344 File Offset: 0x000A2544
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		if (!this.rS_)
		{
			this.BUTTON_Close();
			return;
		}
		int count = this.rS_.listGameObjects.Count;
		string text = this.tS_.GetText(1065);
		text = text.Replace("<NUM1>", count.ToString());
		text = text.Replace("<NUM2>", this.mS_.GetMoney((long)this.GetPreis(), true));
		this.uiObjects[0].GetComponent<Text>().text = text;
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x000A43D9 File Offset: 0x000A25D9
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x000A4400 File Offset: 0x000A2600
	public void BUTTON_OK()
	{
		int preis = this.GetPreis();
		if (this.mS_.NotEnoughMoney(preis))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.mS_.Pay((long)preis, 19);
		this.mS_.buildings[this.rS_.myID] = true;
		this.rS_.Demolish();
		this.mS_.SendSystemMessage("<IMMOBILIE>");
		this.BUTTON_Close();
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000A4478 File Offset: 0x000A2678
	private int GetPreis()
	{
		int count = this.rS_.listGameObjects.Count;
		int num = count * ((this.mS_.difficulty + 1) * 1600);
		if (count <= 100)
		{
			num = num;
		}
		if (count > 100 && count <= 200)
		{
			num *= 2;
		}
		if (count > 200 && count <= 300)
		{
			num *= 5;
		}
		if (count > 300 && count <= 400)
		{
			num *= 10;
		}
		if (count > 400 && count <= 500)
		{
			num *= 15;
		}
		if (count > 500 && count <= 600)
		{
			num *= 20;
		}
		if (count > 600)
		{
			num *= 30;
		}
		if (this.mS_.globalEvent == 6)
		{
			num *= 2;
		}
		if (this.mS_.globalEvent == 8)
		{
			num /= 2;
		}
		return num;
	}

	// Token: 0x040013C7 RID: 5063
	public GameObject[] uiObjects;

	// Token: 0x040013C8 RID: 5064
	private GameObject main_;

	// Token: 0x040013C9 RID: 5065
	private mainScript mS_;

	// Token: 0x040013CA RID: 5066
	private textScript tS_;

	// Token: 0x040013CB RID: 5067
	private GUI_Main guiMain_;

	// Token: 0x040013CC RID: 5068
	private sfxScript sfx_;

	// Token: 0x040013CD RID: 5069
	private cameraMovementScript cmS_;

	// Token: 0x040013CE RID: 5070
	private unlockScript unlock_;

	// Token: 0x040013CF RID: 5071
	private roomScript rS_;
}
