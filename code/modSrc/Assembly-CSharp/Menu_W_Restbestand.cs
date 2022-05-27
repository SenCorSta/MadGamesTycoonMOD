using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000206 RID: 518
public class Menu_W_Restbestand : MonoBehaviour
{
	// Token: 0x060013BB RID: 5051 RVA: 0x0000D7AD File Offset: 0x0000B9AD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x000D99C8 File Offset: 0x000D7BC8
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
		if (!this.menu_LagerSelect)
		{
			this.menu_LagerSelect = this.guiMain_.uiObjects[225].GetComponent<Menu_LagerSelect>();
		}
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x0000D7B5 File Offset: 0x0000B9B5
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x000D9ABC File Offset: 0x000D7CBC
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0000D7BD File Offset: 0x0000B9BD
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		gS_;
		this.game_ = gS_;
		this.SetData();
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000D9B08 File Offset: 0x000D7D08
	private void SetData()
	{
		if (this.game_)
		{
			this.money = this.GetSumme(this.game_);
			this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
			this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.money, true);
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = "";
		this.money = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			gameScript component = array[i].GetComponent<gameScript>();
			if (component && this.menu_LagerSelect.CheckGameData(component))
			{
				this.money += this.GetSumme(component);
			}
		}
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.money, true);
		if (this.money <= 0)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x0000D7D9 File Offset: 0x0000B9D9
	private int GetSumme(gameScript script_)
	{
		if (!script_)
		{
			return 0;
		}
		int reviewTotal = script_.reviewTotal;
		return Mathf.RoundToInt(0.029000001f * (float)script_.reviewTotal * (float)script_.GetLagerbestand());
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x0000D809 File Offset: 0x0000BA09
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x000D9C18 File Offset: 0x000D7E18
	public void BUTTON_Yes()
	{
		if (this.game_)
		{
			this.mS_.Earn((long)this.money, 1);
			this.game_.umsatzTotal += (long)this.money;
			for (int i = 0; i < this.game_.lagerbestand.Length; i++)
			{
				this.game_.lagerbestand[i] = 0;
			}
			this.games_.LagerplatzVerteilen();
			this.BUTTON_Abbrechen();
			return;
		}
		this.mS_.Earn((long)this.money, 1);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int j = 0; j < array.Length; j++)
		{
			gameScript component = array[j].GetComponent<gameScript>();
			if (component && this.menu_LagerSelect.CheckGameData(component))
			{
				component.umsatzTotal += (long)this.money;
				for (int k = 0; k < component.lagerbestand.Length; k++)
				{
					component.lagerbestand[k] = 0;
				}
			}
		}
		this.games_.LagerplatzVerteilen();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040017EB RID: 6123
	public GameObject[] uiObjects;

	// Token: 0x040017EC RID: 6124
	private GameObject main_;

	// Token: 0x040017ED RID: 6125
	private mainScript mS_;

	// Token: 0x040017EE RID: 6126
	private textScript tS_;

	// Token: 0x040017EF RID: 6127
	private GUI_Main guiMain_;

	// Token: 0x040017F0 RID: 6128
	private sfxScript sfx_;

	// Token: 0x040017F1 RID: 6129
	private games games_;

	// Token: 0x040017F2 RID: 6130
	private gameScript game_;

	// Token: 0x040017F3 RID: 6131
	private Menu_LagerSelect menu_LagerSelect;

	// Token: 0x040017F4 RID: 6132
	private int money;

	// Token: 0x040017F5 RID: 6133
	private float updateTimer;
}
