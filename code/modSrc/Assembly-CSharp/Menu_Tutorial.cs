using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000181 RID: 385
public class Menu_Tutorial : MonoBehaviour
{
	// Token: 0x06000E6A RID: 3690 RVA: 0x0000A1B4 File Offset: 0x000083B4
	private void Start()
	{
		this.FindScripts();
		this.BUTTON_Next(0);
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x000A9AC4 File Offset: 0x000A7CC4
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.main_)
		{
			return;
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

	// Token: 0x06000E6C RID: 3692 RVA: 0x0000A1C3 File Offset: 0x000083C3
	public void BUTTON_Next(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.SetStep(this.step + i);
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x000A9B7C File Offset: 0x000A7D7C
	public void SetStep(int s)
	{
		base.gameObject.GetComponent<Animation>().Play();
		this.step = s;
		if (this.step < 0)
		{
			this.step = 0;
		}
		if (this.step >= this.tS_.tutorial_GE.Length)
		{
			this.mS_.settings_TutorialOff = true;
			base.gameObject.SetActive(false);
			this.DisableAllArrows();
			return;
		}
		if (this.showNextButton[this.step])
		{
			this.uiObjects[2].SetActive(true);
		}
		else
		{
			this.uiObjects[2].SetActive(false);
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetTutorial(this.step);
		for (int i = 0; i < this.arrows.Length; i++)
		{
			if (this.arrows[i])
			{
				if (this.arrows[i].activeSelf)
				{
					this.arrows[i].SetActive(false);
				}
				if (i == this.step && !this.arrows[i].activeSelf)
				{
					this.arrows[i].SetActive(true);
				}
			}
		}
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x000A9C9C File Offset: 0x000A7E9C
	private void DisableAllArrows()
	{
		for (int i = 0; i < this.arrows.Length; i++)
		{
			if (this.arrows[i] && this.arrows[i].activeSelf)
			{
				this.arrows[i].SetActive(false);
			}
		}
	}

	// Token: 0x040012E9 RID: 4841
	public GameObject[] uiObjects;

	// Token: 0x040012EA RID: 4842
	public GameObject[] arrows;

	// Token: 0x040012EB RID: 4843
	public bool[] showNextButton;

	// Token: 0x040012EC RID: 4844
	private GameObject main_;

	// Token: 0x040012ED RID: 4845
	private mainScript mS_;

	// Token: 0x040012EE RID: 4846
	private textScript tS_;

	// Token: 0x040012EF RID: 4847
	private GUI_Main guiMain_;

	// Token: 0x040012F0 RID: 4848
	private sfxScript sfx_;

	// Token: 0x040012F1 RID: 4849
	public int step;
}
