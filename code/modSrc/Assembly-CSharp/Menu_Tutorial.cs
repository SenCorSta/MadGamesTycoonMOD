using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000182 RID: 386
public class Menu_Tutorial : MonoBehaviour
{
	// Token: 0x06000E82 RID: 3714 RVA: 0x0009C106 File Offset: 0x0009A306
	private void Start()
	{
		this.FindScripts();
		this.BUTTON_Next(0);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0009C118 File Offset: 0x0009A318
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

	// Token: 0x06000E84 RID: 3716 RVA: 0x0009C1D0 File Offset: 0x0009A3D0
	public void BUTTON_Next(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.SetStep(this.step + i);
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0009C1F0 File Offset: 0x0009A3F0
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

	// Token: 0x06000E86 RID: 3718 RVA: 0x0009C310 File Offset: 0x0009A510
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

	// Token: 0x040012F2 RID: 4850
	public GameObject[] uiObjects;

	// Token: 0x040012F3 RID: 4851
	public GameObject[] arrows;

	// Token: 0x040012F4 RID: 4852
	public bool[] showNextButton;

	// Token: 0x040012F5 RID: 4853
	private GameObject main_;

	// Token: 0x040012F6 RID: 4854
	private mainScript mS_;

	// Token: 0x040012F7 RID: 4855
	private textScript tS_;

	// Token: 0x040012F8 RID: 4856
	private GUI_Main guiMain_;

	// Token: 0x040012F9 RID: 4857
	private sfxScript sfx_;

	// Token: 0x040012FA RID: 4858
	public int step;
}
