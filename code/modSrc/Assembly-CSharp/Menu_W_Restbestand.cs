using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000207 RID: 519
public class Menu_W_Restbestand : MonoBehaviour
{
	// Token: 0x060013D6 RID: 5078 RVA: 0x000CFA95 File Offset: 0x000CDC95
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000CFAA0 File Offset: 0x000CDCA0
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

	// Token: 0x060013D8 RID: 5080 RVA: 0x000CFB91 File Offset: 0x000CDD91
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x000CFB9C File Offset: 0x000CDD9C
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

	// Token: 0x060013DA RID: 5082 RVA: 0x000CFBE8 File Offset: 0x000CDDE8
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		gS_;
		this.game_ = gS_;
		this.SetData();
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x000CFC04 File Offset: 0x000CDE04
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

	// Token: 0x060013DC RID: 5084 RVA: 0x000CFD11 File Offset: 0x000CDF11
	private int GetSumme(gameScript script_)
	{
		if (!script_)
		{
			return 0;
		}
		int reviewTotal = script_.reviewTotal;
		return Mathf.RoundToInt(0.029000001f * (float)script_.reviewTotal * (float)script_.GetLagerbestand());
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x000CFD41 File Offset: 0x000CDF41
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x000CFD5C File Offset: 0x000CDF5C
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

	// Token: 0x040017F4 RID: 6132
	public GameObject[] uiObjects;

	// Token: 0x040017F5 RID: 6133
	private GameObject main_;

	// Token: 0x040017F6 RID: 6134
	private mainScript mS_;

	// Token: 0x040017F7 RID: 6135
	private textScript tS_;

	// Token: 0x040017F8 RID: 6136
	private GUI_Main guiMain_;

	// Token: 0x040017F9 RID: 6137
	private sfxScript sfx_;

	// Token: 0x040017FA RID: 6138
	private games games_;

	// Token: 0x040017FB RID: 6139
	private gameScript game_;

	// Token: 0x040017FC RID: 6140
	private Menu_LagerSelect menu_LagerSelect;

	// Token: 0x040017FD RID: 6141
	private int money;

	// Token: 0x040017FE RID: 6142
	private float updateTimer;
}
