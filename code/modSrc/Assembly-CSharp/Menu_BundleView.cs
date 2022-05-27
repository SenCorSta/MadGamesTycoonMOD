using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F4 RID: 500
public class Menu_BundleView : MonoBehaviour
{
	// Token: 0x060012F4 RID: 4852 RVA: 0x0000D00D File Offset: 0x0000B20D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x000D461C File Offset: 0x000D281C
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
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

	// Token: 0x060012F6 RID: 4854 RVA: 0x000D4704 File Offset: 0x000D2904
	public void Init(gameScript script_)
	{
		this.FindScripts();
		this.gS_ = script_;
		for (int i = 0; i < this.gS_.bundleID.Length; i++)
		{
			this.SetGame(i, this.gS_.GetBundleGame(i));
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[27].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.sellsTotal, false);
		if (this.gS_.GetGesamtGewinn() >= 0L)
		{
			this.uiObjects[28].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true);
			return;
		}
		this.uiObjects[28].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x000D4804 File Offset: 0x000D2A04
	public void SetGame(int slot, gameScript script_)
	{
		if (!script_)
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = "";
			this.uiObjects[2 + slot].GetComponent<Text>().text = "";
			this.uiObjects[7 + slot].GetComponent<Text>().text = "";
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			this.uiObjects[17 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			this.uiObjects[29 + slot].GetComponent<Text>().text = "";
		}
		else
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = script_.GetTooltip();
			this.uiObjects[2 + slot].GetComponent<Text>().text = "<b>" + script_.GetNameWithTag() + "</b>";
			this.uiObjects[7 + slot].GetComponent<Text>().text = script_.GetReleaseDateString();
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.genres_.GetPic(script_.maingenre);
			this.uiObjects[17 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[30];
			this.uiObjects[29 + slot].GetComponent<Text>().text = Mathf.RoundToInt((float)script_.reviewTotal).ToString() + "%";
		}
		this.guiMain_.DrawStarsColor(this.uiObjects[1], Mathf.RoundToInt(this.GetQuality()), Color.white);
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x0000D015 File Offset: 0x0000B215
	public float GetQuality()
	{
		return (float)(this.gS_.reviewTotal / 20);
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x0000D026 File Offset: 0x0000B226
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400174A RID: 5962
	public GameObject[] uiObjects;

	// Token: 0x0400174B RID: 5963
	private roomScript rS_;

	// Token: 0x0400174C RID: 5964
	private GameObject main_;

	// Token: 0x0400174D RID: 5965
	private mainScript mS_;

	// Token: 0x0400174E RID: 5966
	private textScript tS_;

	// Token: 0x0400174F RID: 5967
	private GUI_Main guiMain_;

	// Token: 0x04001750 RID: 5968
	private sfxScript sfx_;

	// Token: 0x04001751 RID: 5969
	private genres genres_;

	// Token: 0x04001752 RID: 5970
	private games games_;

	// Token: 0x04001753 RID: 5971
	public gameScript gS_;
}
