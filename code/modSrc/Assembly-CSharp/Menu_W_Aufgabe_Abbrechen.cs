using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019D RID: 413
public class Menu_W_Aufgabe_Abbrechen : MonoBehaviour
{
	// Token: 0x06000FA5 RID: 4005 RVA: 0x000A69B5 File Offset: 0x000A4BB5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x000A69C0 File Offset: 0x000A4BC0
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

	// Token: 0x06000FA7 RID: 4007 RVA: 0x000A6A6C File Offset: 0x000A4C6C
	public void Init(roomScript script_)
	{
		this.FindScripts();
		if (!script_)
		{
			return;
		}
		this.rS_ = script_;
		int num = 0;
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			if (gameObject.GetComponent<taskEngine>())
			{
				num = gameObject.GetComponent<taskEngine>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskMarketing>())
			{
				num = gameObject.GetComponent<taskMarketing>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskMarketingSpezial>())
			{
				num = gameObject.GetComponent<taskMarketingSpezial>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskForschung>())
			{
				num = gameObject.GetComponent<taskForschung>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskGame>())
			{
				num = gameObject.GetComponent<taskGame>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskTraining>())
			{
				num = gameObject.GetComponent<taskTraining>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskContractWork>())
			{
				num = 0 - gameObject.GetComponent<taskContractWork>().GetStrafe();
			}
			if (gameObject.GetComponent<taskContractWait>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskUpdate>())
			{
				num = gameObject.GetComponent<taskUpdate>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskF2PUpdate>())
			{
				num = gameObject.GetComponent<taskF2PUpdate>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskFankampagne>())
			{
				num = gameObject.GetComponent<taskFankampagne>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskMitarbeitersuche>())
			{
				num = gameObject.GetComponent<taskMitarbeitersuche>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskBugfixing>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskSupport>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskFanshop>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskGameplayVerbessern>())
			{
				num = gameObject.GetComponent<taskGameplayVerbessern>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskGrafikVerbessern>())
			{
				num = gameObject.GetComponent<taskGrafikVerbessern>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskSoundVerbessern>())
			{
				num = gameObject.GetComponent<taskSoundVerbessern>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskAnimationVerbessern>())
			{
				num = gameObject.GetComponent<taskAnimationVerbessern>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskMarktforschung>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskPolishing>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskKonsole>())
			{
				num = gameObject.GetComponent<taskKonsole>().GetRueckgeld();
			}
			if (gameObject.GetComponent<taskArcadeProduction>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskProduction>())
			{
				num = 0;
			}
			if (gameObject.GetComponent<taskSpielbericht>())
			{
				num = 0;
			}
		}
		this.uiObjects[0].GetComponent<Text>().text = "";
		if (num > 0)
		{
			string text = this.tS_.GetText(569);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)num, true));
			this.uiObjects[0].GetComponent<Text>().text = text;
		}
		if (num < 0)
		{
			string text2 = this.tS_.GetText(608);
			text2 = text2.Replace("<NUM>", this.mS_.GetMoney((long)Mathf.Abs(num), true));
			this.uiObjects[0].GetComponent<Text>().text = text2;
		}
		if (this.mS_.settings_.dontAsk_TaskAbbrechen)
		{
			this.BUTTON_Yes();
		}
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x000A6D90 File Offset: 0x000A4F90
	public void BUTTON_Abbrechen()
	{
		if (this.uiObjects[1].GetComponent<Toggle>().isOn)
		{
			this.mS_.settings_.dontAsk_TaskAbbrechen = true;
			this.mS_.settings_.SaveSettings();
		}
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x000A6DF8 File Offset: 0x000A4FF8
	public void BUTTON_Yes()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID);
		if (gameObject)
		{
			if (gameObject.GetComponent<taskEngine>())
			{
				gameObject.GetComponent<taskEngine>().Abbrechen();
			}
			if (gameObject.GetComponent<taskMarketing>())
			{
				gameObject.GetComponent<taskMarketing>().Abbrechen();
			}
			if (gameObject.GetComponent<taskMarketingSpezial>())
			{
				gameObject.GetComponent<taskMarketingSpezial>().Abbrechen();
			}
			if (gameObject.GetComponent<taskForschung>())
			{
				gameObject.GetComponent<taskForschung>().Abbrechen();
			}
			if (gameObject.GetComponent<taskGame>())
			{
				gameObject.GetComponent<taskGame>().Abbrechen();
			}
			if (gameObject.GetComponent<taskTraining>())
			{
				gameObject.GetComponent<taskTraining>().Abbrechen();
			}
			if (gameObject.GetComponent<taskContractWork>())
			{
				gameObject.GetComponent<taskContractWork>().Abbrechen();
			}
			if (gameObject.GetComponent<taskContractWait>())
			{
				gameObject.GetComponent<taskContractWait>().Abbrechen();
			}
			if (gameObject.GetComponent<taskUpdate>())
			{
				gameObject.GetComponent<taskUpdate>().Abbrechen();
			}
			if (gameObject.GetComponent<taskF2PUpdate>())
			{
				gameObject.GetComponent<taskF2PUpdate>().Abbrechen();
			}
			if (gameObject.GetComponent<taskFankampagne>())
			{
				gameObject.GetComponent<taskFankampagne>().Abbrechen();
			}
			if (gameObject.GetComponent<taskMitarbeitersuche>())
			{
				gameObject.GetComponent<taskMitarbeitersuche>().Abbrechen();
			}
			if (gameObject.GetComponent<taskBugfixing>())
			{
				gameObject.GetComponent<taskBugfixing>().Abbrechen();
			}
			if (gameObject.GetComponent<taskSupport>())
			{
				gameObject.GetComponent<taskSupport>().Abbrechen();
			}
			if (gameObject.GetComponent<taskFanshop>())
			{
				gameObject.GetComponent<taskFanshop>().Abbrechen();
			}
			if (gameObject.GetComponent<taskGameplayVerbessern>())
			{
				gameObject.GetComponent<taskGameplayVerbessern>().Abbrechen();
			}
			if (gameObject.GetComponent<taskGrafikVerbessern>())
			{
				gameObject.GetComponent<taskGrafikVerbessern>().Abbrechen();
			}
			if (gameObject.GetComponent<taskSoundVerbessern>())
			{
				gameObject.GetComponent<taskSoundVerbessern>().Abbrechen();
			}
			if (gameObject.GetComponent<taskAnimationVerbessern>())
			{
				gameObject.GetComponent<taskAnimationVerbessern>().Abbrechen();
			}
			if (gameObject.GetComponent<taskMarktforschung>())
			{
				gameObject.GetComponent<taskMarktforschung>().Abbrechen();
			}
			if (gameObject.GetComponent<taskPolishing>())
			{
				gameObject.GetComponent<taskPolishing>().Abbrechen();
			}
			if (gameObject.GetComponent<taskKonsole>())
			{
				gameObject.GetComponent<taskKonsole>().Abbrechen();
			}
			if (gameObject.GetComponent<taskArcadeProduction>())
			{
				gameObject.GetComponent<taskArcadeProduction>().Abbrechen();
			}
			if (gameObject.GetComponent<taskProduction>())
			{
				gameObject.GetComponent<taskProduction>().Abbrechen();
			}
			if (gameObject.GetComponent<taskSpielbericht>())
			{
				gameObject.GetComponent<taskSpielbericht>().Abbrechen();
			}
		}
		this.rS_.taskID = -1;
		this.rS_.taskGameObject = null;
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001412 RID: 5138
	public GameObject[] uiObjects;

	// Token: 0x04001413 RID: 5139
	private platformScript pS_;

	// Token: 0x04001414 RID: 5140
	private GameObject main_;

	// Token: 0x04001415 RID: 5141
	private mainScript mS_;

	// Token: 0x04001416 RID: 5142
	private textScript tS_;

	// Token: 0x04001417 RID: 5143
	private GUI_Main guiMain_;

	// Token: 0x04001418 RID: 5144
	private sfxScript sfx_;

	// Token: 0x04001419 RID: 5145
	private roomScript rS_;
}
