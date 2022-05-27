using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000265 RID: 613
public class Menu_W_FirmaKaufen : MonoBehaviour
{
	// Token: 0x060017BA RID: 6074 RVA: 0x0001084D File Offset: 0x0000EA4D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x000F4DB4 File Offset: 0x000F2FB4
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

	// Token: 0x060017BC RID: 6076 RVA: 0x000F4E9C File Offset: 0x000F309C
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		if (this.pS_.lockToBuy > 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1975), false);
			this.BUTTON_Abbrechen();
			return;
		}
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].publisherID == this.pS_.myID && this.games_.arrayGamesScripts[i].auftragsspiel)
			{
				this.guiMain_.MessageBox(this.tS_.GetText(1975), false);
				this.BUTTON_Abbrechen();
				return;
			}
		}
		if (this.pS_)
		{
			string text = this.tS_.GetText(1928);
			text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
			text = text.Replace("<NUM>", "<color=blue>" + this.pS_.GetFirmenwertString() + "</color>");
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			return;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x00010855 File Offset: 0x0000EA55
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x000F5010 File Offset: 0x000F3210
	public void BUTTON_Yes()
	{
		if (this.pS_)
		{
			if (this.mS_.money < this.pS_.GetFirmenwert())
			{
				this.guiMain_.ShowNoMoney();
				return;
			}
			this.mS_.Pay(this.pS_.GetFirmenwert(), 28);
			this.pS_.SetAsTochterfirma();
			this.pS_.firmenwert = this.pS_.firmenwert / 100L * 80L;
			this.pS_.exklusive = false;
			this.pS_.onlyMobile = false;
			this.pS_.relation = 100f;
			this.pS_.tf_geschlossen = false;
			this.pS_.tf_autoRelease = false;
			this.pS_.tf_onlyPlayerConsole = false;
			this.pS_.tf_allowMMO = true;
			this.pS_.tf_allowF2P = true;
			this.pS_.tf_allowAddon = true;
			this.pS_.tf_noArcade = false;
			this.pS_.tf_noHandy = false;
			this.pS_.tf_noRetro = false;
			this.pS_.tf_noPorts = false;
			this.pS_.tf_noBudget = false;
			this.pS_.tf_noGOTY = false;
			this.pS_.tf_noRemaster = false;
			this.pS_.tf_noSpinoffs = false;
			this.pS_.tf_gameGenre = 0;
			this.pS_.tf_gameSize = 0;
			this.pS_.tf_entwicklungsdauer = 1;
			this.pS_.tf_publisher = this.pS_.publisher;
			this.pS_.tf_developer = this.pS_.developer;
			this.pS_.tf_ownPublisher = true;
			this.pS_.tf_gameTopic = -1;
			this.pS_.tf_autoReleaseVal = 0;
			for (int i = 0; i < this.pS_.tf_ipFocus.Length; i++)
			{
				this.pS_.tf_ipFocus[i] = -1;
			}
			this.pS_.tf_engine = -1;
			for (int j = 0; j < this.pS_.tf_platformFocus.Length; j++)
			{
				this.pS_.tf_platformFocus[j] = -1;
			}
			if (this.mS_.exklusivVertrag_ID == this.pS_.myID)
			{
				this.mS_.exklusivVertrag_ID = -1;
				this.mS_.exklusivVertrag_laufzeit = 0;
			}
			if (this.guiMain_.uiObjects[359].activeSelf)
			{
				this.guiMain_.uiObjects[359].SetActive(false);
			}
			if (this.guiMain_.uiObjects[373].activeSelf)
			{
				this.guiMain_.uiObjects[373].SetActive(false);
			}
			if (this.guiMain_.uiObjects[119].activeSelf)
			{
				this.guiMain_.uiObjects[119].GetComponent<Menu_Statistics_Publisher>().BUTTON_Search();
			}
			if (this.guiMain_.uiObjects[120].activeSelf)
			{
				this.guiMain_.uiObjects[120].GetComponent<Menu_Statistics_Developer>().BUTTON_Search();
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001B9A RID: 7066
	public GameObject[] uiObjects;

	// Token: 0x04001B9B RID: 7067
	private publisherScript pS_;

	// Token: 0x04001B9C RID: 7068
	private GameObject main_;

	// Token: 0x04001B9D RID: 7069
	private mainScript mS_;

	// Token: 0x04001B9E RID: 7070
	private textScript tS_;

	// Token: 0x04001B9F RID: 7071
	private GUI_Main guiMain_;

	// Token: 0x04001BA0 RID: 7072
	private sfxScript sfx_;

	// Token: 0x04001BA1 RID: 7073
	private genres genres_;

	// Token: 0x04001BA2 RID: 7074
	private games games_;
}
