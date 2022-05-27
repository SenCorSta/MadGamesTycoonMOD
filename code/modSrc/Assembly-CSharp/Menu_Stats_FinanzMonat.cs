using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000239 RID: 569
public class Menu_Stats_FinanzMonat : MonoBehaviour
{
	// Token: 0x060015F3 RID: 5619 RVA: 0x000DFD18 File Offset: 0x000DDF18
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x000DFD20 File Offset: 0x000DDF20
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x000DFD28 File Offset: 0x000DDF28
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x000DFEC6 File Offset: 0x000DE0C6
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x000DFED4 File Offset: 0x000DE0D4
	private void SetData()
	{
		string text = "";
		text = text + "<b>" + this.tS_.GetText(711) + "</b>\n";
		text = text + this.tS_.GetText(707) + "\n";
		text = text + this.tS_.GetText(708) + "\n";
		text = text + this.tS_.GetText(19) + "\n";
		text = text + this.tS_.GetText(20) + "\n";
		text = text + this.tS_.GetText(117) + "\n";
		text = text + this.tS_.GetText(747) + "\n";
		text = text + this.tS_.GetText(530) + "\n";
		text = text + this.tS_.GetText(1656) + "\n";
		text = text + this.tS_.GetText(1655) + "\n";
		text = text + this.tS_.GetText(709) + "\n";
		text = text + this.tS_.GetText(570) + "\n";
		text = text + this.tS_.GetText(1842) + "\n";
		text = text + this.tS_.GetText(710) + "\n";
		text = text + this.tS_.GetText(211) + "\n";
		text = text + this.tS_.GetText(734) + "\n";
		text = text + this.tS_.GetText(1923) + "\n";
		text = text + this.tS_.GetText(163) + "\n";
		text += "\n";
		text = text + "<b>" + this.tS_.GetText(712) + "</b>\n";
		text = text + this.tS_.GetText(713) + "\n";
		text = text + this.tS_.GetText(1236) + "\n";
		text = text + this.tS_.GetText(1177) + "\n";
		text = text + this.tS_.GetText(714) + "\n";
		text = text + this.tS_.GetText(715) + "\n";
		text = text + this.tS_.GetText(716) + "\n";
		text = text + this.tS_.GetText(943) + "\n";
		text = text + this.tS_.GetText(708) + "\n";
		text = text + this.tS_.GetText(570) + "\n";
		text = text + this.tS_.GetText(1842) + "\n";
		text = text + this.tS_.GetText(1923) + "\n";
		text += this.tS_.GetText(163);
		this.uiObjects[0].GetComponent<Text>().text = text;
		text = "<color=red>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[0], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[1], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[2], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[3], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[4], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[5], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[12], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[13], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[14], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[6], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[7], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[15], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[8], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[10], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[11], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[16], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[9], true) + "\n";
		text += "</color>\n";
		text += "<color=green>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[50], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[57], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[58], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[51], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[52], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[53], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[59], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[54], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[55], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[60], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonat[61], true) + "\n";
		text += this.mS_.GetMoney(this.mS_.finanzenMonat[56], true);
		text += "</color>";
		this.uiObjects[1].GetComponent<Text>().text = text;
		text = "<color=red>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[0], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[1], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[2], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[3], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[4], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[5], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[12], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[13], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[14], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[6], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[7], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[15], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[8], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[10], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[11], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[16], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[9], true) + "\n";
		text += "</color>\n";
		text += "<color=green>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[50], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[57], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[58], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[51], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[52], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[53], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[59], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[54], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[55], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[60], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenMonatLast[61], true) + "\n";
		text += this.mS_.GetMoney(this.mS_.finanzenMonatLast[56], true);
		text += "</color>";
		this.uiObjects[3].GetComponent<Text>().text = text;
		if (this.mS_.finanzenMonat_GetGewinn() < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.finanzenMonat_GetGewinn(), true) + "</color>";
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.mS_.finanzenMonat_GetGewinn(), true) + "</color>";
		}
		if (this.mS_.finanzenMonatLast_GetGewinn() < 0L)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.finanzenMonatLast_GetGewinn(), true) + "</color>";
			return;
		}
		this.uiObjects[4].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.mS_.finanzenMonatLast_GetGewinn(), true) + "</color>";
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x000E0C61 File Offset: 0x000DEE61
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.SetData();
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x000E0C77 File Offset: 0x000DEE77
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[131].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019E8 RID: 6632
	public GameObject[] uiObjects;

	// Token: 0x040019E9 RID: 6633
	private GameObject main_;

	// Token: 0x040019EA RID: 6634
	private mainScript mS_;

	// Token: 0x040019EB RID: 6635
	private textScript tS_;

	// Token: 0x040019EC RID: 6636
	private GUI_Main guiMain_;

	// Token: 0x040019ED RID: 6637
	private sfxScript sfx_;

	// Token: 0x040019EE RID: 6638
	private genres genres_;

	// Token: 0x040019EF RID: 6639
	private themes themes_;

	// Token: 0x040019F0 RID: 6640
	private licences licences_;

	// Token: 0x040019F1 RID: 6641
	private engineFeatures eF_;

	// Token: 0x040019F2 RID: 6642
	private cameraMovementScript cmS_;

	// Token: 0x040019F3 RID: 6643
	private unlockScript unlock_;

	// Token: 0x040019F4 RID: 6644
	private gameplayFeatures gF_;

	// Token: 0x040019F5 RID: 6645
	private games games_;
}
