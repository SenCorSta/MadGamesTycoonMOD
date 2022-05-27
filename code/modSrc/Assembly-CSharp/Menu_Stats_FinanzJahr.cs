using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000238 RID: 568
public class Menu_Stats_FinanzJahr : MonoBehaviour
{
	// Token: 0x060015EB RID: 5611 RVA: 0x000DED6F File Offset: 0x000DCF6F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000DED77 File Offset: 0x000DCF77
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x000DED80 File Offset: 0x000DCF80
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

	// Token: 0x060015EE RID: 5614 RVA: 0x000DEF1E File Offset: 0x000DD11E
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x000DEF2C File Offset: 0x000DD12C
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.SetData();
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x000DEF44 File Offset: 0x000DD144
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
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[0], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[1], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[2], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[3], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[4], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[5], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[12], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[13], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[14], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[6], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[7], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[15], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[8], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[10], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[11], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[16], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[9], true) + "\n";
		text += "</color>\n";
		text += "<color=green>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[50], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[57], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[58], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[51], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[52], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[53], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[59], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[54], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[55], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[60], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[61], true) + "\n";
		text += this.mS_.GetMoney(this.mS_.finanzenJahr[56], true);
		text += "</color>";
		this.uiObjects[1].GetComponent<Text>().text = text;
		text = "<color=red>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[0], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[1], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[2], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[3], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[4], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[5], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[12], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[13], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[14], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[6], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[7], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[15], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[8], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[10], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[11], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[16], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[9], true) + "\n";
		text += "</color>\n";
		text += "<color=green>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[50], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[57], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[58], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[51], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[52], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[53], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[59], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[54], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[55], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[60], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[61], true) + "\n";
		text += this.mS_.GetMoney(this.mS_.finanzenJahrLast[56], true);
		text += "</color>";
		this.uiObjects[3].GetComponent<Text>().text = text;
		if (this.mS_.finanzenJahr_GetGewinn() < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.finanzenJahr_GetGewinn(), true) + "</color>";
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.mS_.finanzenJahr_GetGewinn(), true) + "</color>";
		}
		if (this.mS_.finanzenJahrLast_GetGewinn() < 0L)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.finanzenJahrLast_GetGewinn(), true) + "</color>";
			return;
		}
		this.uiObjects[4].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.mS_.finanzenJahrLast_GetGewinn(), true) + "</color>";
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x000DFCD1 File Offset: 0x000DDED1
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019DA RID: 6618
	public GameObject[] uiObjects;

	// Token: 0x040019DB RID: 6619
	private GameObject main_;

	// Token: 0x040019DC RID: 6620
	private mainScript mS_;

	// Token: 0x040019DD RID: 6621
	private textScript tS_;

	// Token: 0x040019DE RID: 6622
	private GUI_Main guiMain_;

	// Token: 0x040019DF RID: 6623
	private sfxScript sfx_;

	// Token: 0x040019E0 RID: 6624
	private genres genres_;

	// Token: 0x040019E1 RID: 6625
	private themes themes_;

	// Token: 0x040019E2 RID: 6626
	private licences licences_;

	// Token: 0x040019E3 RID: 6627
	private engineFeatures eF_;

	// Token: 0x040019E4 RID: 6628
	private cameraMovementScript cmS_;

	// Token: 0x040019E5 RID: 6629
	private unlockScript unlock_;

	// Token: 0x040019E6 RID: 6630
	private gameplayFeatures gF_;

	// Token: 0x040019E7 RID: 6631
	private games games_;
}
