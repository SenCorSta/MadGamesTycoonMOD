using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000266 RID: 614
public class Menu_W_FirmaVerkaufen : MonoBehaviour
{
	// Token: 0x060017C0 RID: 6080 RVA: 0x00010870 File Offset: 0x0000EA70
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x000F531C File Offset: 0x000F351C
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

	// Token: 0x060017C2 RID: 6082 RVA: 0x000F5404 File Offset: 0x000F3604
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		if (this.pS_)
		{
			string text = this.tS_.GetText(1974);
			text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
			text = text.Replace("<NUM>", "<color=blue>" + this.pS_.GetFirmenwertString() + "</color>");
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
			return;
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x00010878 File Offset: 0x0000EA78
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000F54C4 File Offset: 0x000F36C4
	public void BUTTON_Yes()
	{
		if (this.pS_)
		{
			this.mS_.Earn(this.pS_.GetFirmenwert(), 12);
			this.pS_.RemoveTochterfirma();
			this.pS_.firmenwert = this.pS_.firmenwert / 100L * 110L;
			this.pS_.exklusive = false;
			this.pS_.onlyMobile = false;
			this.pS_.relation = 0f;
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
			this.pS_.publisher = this.pS_.tf_publisher;
			this.pS_.developer = this.pS_.tf_developer;
			this.pS_.tf_ownPublisher = false;
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
			if (this.guiMain_.uiObjects[387].activeSelf)
			{
				this.guiMain_.uiObjects[387].SetActive(false);
			}
			if (this.guiMain_.uiObjects[385].activeSelf)
			{
				this.guiMain_.uiObjects[385].GetComponent<Menu_Statistics_Tochterfirmen>().BUTTON_Search();
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001BA3 RID: 7075
	public GameObject[] uiObjects;

	// Token: 0x04001BA4 RID: 7076
	private publisherScript pS_;

	// Token: 0x04001BA5 RID: 7077
	private GameObject main_;

	// Token: 0x04001BA6 RID: 7078
	private mainScript mS_;

	// Token: 0x04001BA7 RID: 7079
	private textScript tS_;

	// Token: 0x04001BA8 RID: 7080
	private GUI_Main guiMain_;

	// Token: 0x04001BA9 RID: 7081
	private sfxScript sfx_;

	// Token: 0x04001BAA RID: 7082
	private genres genres_;

	// Token: 0x04001BAB RID: 7083
	private games games_;
}
