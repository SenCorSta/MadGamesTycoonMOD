using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E2 RID: 482
public class Menu_PersonalEntlassen : MonoBehaviour
{
	// Token: 0x0600122B RID: 4651 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000CC684 File Offset: 0x000CA884
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

	// Token: 0x0600122D RID: 4653 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
	public void BUTTON_Abbrechen()
	{
		this.listPersonal.Clear();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x000CC730 File Offset: 0x000CA930
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

	// Token: 0x0600122F RID: 4655 RVA: 0x000CC78C File Offset: 0x000CA98C
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

	// Token: 0x040016AD RID: 5805
	public GameObject[] uiObjects;

	// Token: 0x040016AE RID: 5806
	private GameObject main_;

	// Token: 0x040016AF RID: 5807
	private mainScript mS_;

	// Token: 0x040016B0 RID: 5808
	private textScript tS_;

	// Token: 0x040016B1 RID: 5809
	private GUI_Main guiMain_;

	// Token: 0x040016B2 RID: 5810
	private sfxScript sfx_;

	// Token: 0x040016B3 RID: 5811
	private List<characterScript> listPersonal = new List<characterScript>();
}
