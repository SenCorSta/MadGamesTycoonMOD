using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F5 RID: 501
public class Menu_BundleView : MonoBehaviour
{
	// Token: 0x0600130F RID: 4879 RVA: 0x000C9EC6 File Offset: 0x000C80C6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000C9ED0 File Offset: 0x000C80D0
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

	// Token: 0x06001311 RID: 4881 RVA: 0x000C9FB8 File Offset: 0x000C81B8
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

	// Token: 0x06001312 RID: 4882 RVA: 0x000CA0B8 File Offset: 0x000C82B8
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

	// Token: 0x06001313 RID: 4883 RVA: 0x000CA27E File Offset: 0x000C847E
	public float GetQuality()
	{
		return (float)(this.gS_.reviewTotal / 20);
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x000CA28F File Offset: 0x000C848F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001753 RID: 5971
	public GameObject[] uiObjects;

	// Token: 0x04001754 RID: 5972
	private roomScript rS_;

	// Token: 0x04001755 RID: 5973
	private GameObject main_;

	// Token: 0x04001756 RID: 5974
	private mainScript mS_;

	// Token: 0x04001757 RID: 5975
	private textScript tS_;

	// Token: 0x04001758 RID: 5976
	private GUI_Main guiMain_;

	// Token: 0x04001759 RID: 5977
	private sfxScript sfx_;

	// Token: 0x0400175A RID: 5978
	private genres genres_;

	// Token: 0x0400175B RID: 5979
	private games games_;

	// Token: 0x0400175C RID: 5980
	public gameScript gS_;
}
