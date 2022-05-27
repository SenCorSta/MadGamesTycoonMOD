using System;
using UnityEngine;
using UnityEngine.UI;


public class Component_Aufwertungen : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void Init(gameScript gS_)
	{
		if (!gS_)
		{
			return;
		}
		for (int i = 0; i < gS_.gameplayStudio.Length; i++)
		{
			if (gS_.gameplayStudio[i])
			{
				this.uiObjects[i].GetComponent<Image>().color = this.colors[0];
			}
			else
			{
				this.uiObjects[i].GetComponent<Image>().color = this.colors[1];
			}
		}
		for (int j = 0; j < gS_.grafikStudio.Length; j++)
		{
			if (gS_.grafikStudio[j])
			{
				this.uiObjects[12 + j].GetComponent<Image>().color = this.colors[0];
			}
			else
			{
				this.uiObjects[12 + j].GetComponent<Image>().color = this.colors[1];
			}
		}
		for (int k = 0; k < gS_.soundStudio.Length; k++)
		{
			if (gS_.soundStudio[k])
			{
				this.uiObjects[6 + k].GetComponent<Image>().color = this.colors[0];
			}
			else
			{
				this.uiObjects[6 + k].GetComponent<Image>().color = this.colors[1];
			}
		}
		if (!gS_.motionCaptureStudio[0])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[18];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[1];
		}
		if (gS_.motionCaptureStudio[0])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[18];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[1])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[19];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[2])
		{
			this.uiObjects[18].GetComponent<Image>().sprite = this.uiSprites[20];
			this.uiObjects[18].GetComponent<Image>().color = this.colors[0];
		}
		if (!gS_.motionCaptureStudio[3])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[21];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[1];
		}
		if (gS_.motionCaptureStudio[3])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[21];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[4])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[22];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
		}
		if (gS_.motionCaptureStudio[5])
		{
			this.uiObjects[19].GetComponent<Image>().sprite = this.uiSprites[23];
			this.uiObjects[19].GetComponent<Image>().color = this.colors[0];
		}
	}

	
	public GameObject[] uiObjects;

	
	public Sprite[] uiSprites;

	
	public Color[] colors;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;
}
