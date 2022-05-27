using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E3 RID: 483
public class Menu_PersonalEntlassen : MonoBehaviour
{
	// Token: 0x06001246 RID: 4678 RVA: 0x000C18D1 File Offset: 0x000BFAD1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000C18DC File Offset: 0x000BFADC
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
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000C1986 File Offset: 0x000BFB86
	public void BUTTON_Abbrechen()
	{
		this.listPersonal.Clear();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x000C19AC File Offset: 0x000BFBAC
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 0; i < this.listPersonal.Count; i++)
		{
			if (this.listPersonal[i])
			{
				this.listPersonal[i].Entlassen(true);
			}
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x000C1A08 File Offset: 0x000BFC08
	public void AddCharacter(characterScript cS_)
	{
		this.FindScripts();
		this.listPersonal.Add(cS_);
		string text = "";
		for (int i = 0; i < this.listPersonal.Count; i++)
		{
			if (this.listPersonal[i])
			{
				text += this.listPersonal[i].myName;
				if (i + 1 < this.listPersonal.Count)
				{
					text += ", ";
				}
			}
		}
		string text2 = this.tS_.GetText(186);
		text2 = text2.Replace("<NAME>", "<color=blue>" + text + "</color>");
		this.uiObjects[0].GetComponent<Text>().text = text2;
	}

	// Token: 0x040016B6 RID: 5814
	public GameObject[] uiObjects;

	// Token: 0x040016B7 RID: 5815
	private GameObject main_;

	// Token: 0x040016B8 RID: 5816
	private mainScript mS_;

	// Token: 0x040016B9 RID: 5817
	private textScript tS_;

	// Token: 0x040016BA RID: 5818
	private GUI_Main guiMain_;

	// Token: 0x040016BB RID: 5819
	private sfxScript sfx_;

	// Token: 0x040016BC RID: 5820
	private List<characterScript> listPersonal = new List<characterScript>();
}
