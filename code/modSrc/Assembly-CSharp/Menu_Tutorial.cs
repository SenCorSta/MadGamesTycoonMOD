using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Tutorial : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.BUTTON_Next(0);
	}

	
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

	
	public void BUTTON_Next(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.SetStep(this.step + i);
	}

	
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

	
	public GameObject[] uiObjects;

	
	public GameObject[] arrows;

	
	public bool[] showNextButton;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	public int step;
}
