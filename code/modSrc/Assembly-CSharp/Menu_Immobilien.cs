using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000196 RID: 406
public class Menu_Immobilien : MonoBehaviour
{
	// Token: 0x06000F58 RID: 3928 RVA: 0x0000AEB1 File Offset: 0x000090B1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x000B0F1C File Offset: 0x000AF11C
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

	// Token: 0x06000F5A RID: 3930 RVA: 0x000B1008 File Offset: 0x000AF208
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

	// Token: 0x06000F5B RID: 3931 RVA: 0x0000AEB9 File Offset: 0x000090B9
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x000B10A0 File Offset: 0x000AF2A0
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

	// Token: 0x06000F5D RID: 3933 RVA: 0x000B1118 File Offset: 0x000AF318
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

	// Token: 0x040013BE RID: 5054
	public GameObject[] uiObjects;

	// Token: 0x040013BF RID: 5055
	private GameObject main_;

	// Token: 0x040013C0 RID: 5056
	private mainScript mS_;

	// Token: 0x040013C1 RID: 5057
	private textScript tS_;

	// Token: 0x040013C2 RID: 5058
	private GUI_Main guiMain_;

	// Token: 0x040013C3 RID: 5059
	private sfxScript sfx_;

	// Token: 0x040013C4 RID: 5060
	private cameraMovementScript cmS_;

	// Token: 0x040013C5 RID: 5061
	private unlockScript unlock_;

	// Token: 0x040013C6 RID: 5062
	private roomScript rS_;
}
