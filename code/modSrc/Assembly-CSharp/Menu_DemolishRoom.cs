using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B2 RID: 434
public class Menu_DemolishRoom : MonoBehaviour
{
	// Token: 0x06001071 RID: 4209 RVA: 0x000AE7BB File Offset: 0x000AC9BB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x000AE7C4 File Offset: 0x000AC9C4
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
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x000AE890 File Offset: 0x000ACA90
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
		this.money = 0;
		if (script_)
		{
			this.rS_ = script_;
			if (this.rS_.taskID == -1)
			{
				for (int i = 0; i < this.rS_.listInventar.Count; i++)
				{
					if (this.rS_.listInventar[i])
					{
						this.money += this.rS_.listInventar[i].GetComponent<objectScript>().GetVerkaufspreis();
					}
				}
				string text = this.tS_.GetText(153);
				text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.money, true));
				this.uiObjects[0].GetComponent<Text>().text = text;
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(638);
		}
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x000AE99A File Offset: 0x000ACB9A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000AE9CC File Offset: 0x000ACBCC
	public void BUTTON_Yes()
	{
		if (this.rS_ && this.rS_.taskID == -1)
		{
			this.mS_.Earn((long)this.money, 0);
			this.rS_.Demolish();
			this.sfx_.PlaySound(25, true);
			this.guiMain_.ShowWalls(this.guiMain_.uiObjects[241].GetComponent<Toggle>().isOn);
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040014DF RID: 5343
	public GameObject[] uiObjects;

	// Token: 0x040014E0 RID: 5344
	private roomScript rS_;

	// Token: 0x040014E1 RID: 5345
	private GameObject main_;

	// Token: 0x040014E2 RID: 5346
	private mainScript mS_;

	// Token: 0x040014E3 RID: 5347
	private textScript tS_;

	// Token: 0x040014E4 RID: 5348
	private GUI_Main guiMain_;

	// Token: 0x040014E5 RID: 5349
	private sfxScript sfx_;

	// Token: 0x040014E6 RID: 5350
	private cameraMovementScript cmS_;

	// Token: 0x040014E7 RID: 5351
	public int money;
}
