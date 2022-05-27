using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B1 RID: 433
public class Menu_DemolishRoom : MonoBehaviour
{
	// Token: 0x06001057 RID: 4183 RVA: 0x0000B935 File Offset: 0x00009B35
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x000BA830 File Offset: 0x000B8A30
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

	// Token: 0x06001059 RID: 4185 RVA: 0x000BA8FC File Offset: 0x000B8AFC
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

	// Token: 0x0600105A RID: 4186 RVA: 0x0000B93D File Offset: 0x00009B3D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.cmS_.disableMovement = false;
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x000BAA08 File Offset: 0x000B8C08
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

	// Token: 0x040014D4 RID: 5332
	public GameObject[] uiObjects;

	// Token: 0x040014D5 RID: 5333
	private roomScript rS_;

	// Token: 0x040014D6 RID: 5334
	private GameObject main_;

	// Token: 0x040014D7 RID: 5335
	private mainScript mS_;

	// Token: 0x040014D8 RID: 5336
	private textScript tS_;

	// Token: 0x040014D9 RID: 5337
	private GUI_Main guiMain_;

	// Token: 0x040014DA RID: 5338
	private sfxScript sfx_;

	// Token: 0x040014DB RID: 5339
	private cameraMovementScript cmS_;

	// Token: 0x040014DC RID: 5340
	public int money;
}
