using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_KonsolenReview : MonoBehaviour
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
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
	}

	
	public void Init(platformScript plat_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		this.techDifferenceShow = -5f;
		this.techDifference = this.pS_.GetTechDifference();
		this.reviewPointsShow = 0f;
		this.reviewPoints = this.GetGesamtbewertung();
		this.pS_.review = this.reviewPoints;
		this.SetData();
	}

	
	private void Update()
	{
		this.reviewPointsShow = Mathf.Lerp(this.reviewPointsShow, this.reviewPoints, 0.04f);
		this.techDifferenceShow = Mathf.Lerp(this.techDifferenceShow, this.techDifference, 0.04f);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.Round(this.reviewPointsShow, 1) + " / 10";
		if (this.techDifferenceShow >= 0f)
		{
			this.guiMain_.DrawStars(this.uiObjects[2], 5);
			return;
		}
		int value = Mathf.RoundToInt(this.techDifferenceShow);
		switch (Mathf.Abs(value))
		{
		case 0:
			this.guiMain_.DrawStars(this.uiObjects[2], 4);
			return;
		case 1:
			this.guiMain_.DrawStars(this.uiObjects[2], 3);
			return;
		case 2:
			this.guiMain_.DrawStars(this.uiObjects[2], 2);
			return;
		case 3:
			this.guiMain_.DrawStars(this.uiObjects[2], 1);
			return;
		case 4:
			this.guiMain_.DrawStars(this.uiObjects[2], 0);
			return;
		default:
			return;
		}
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (Mathf.RoundToInt(this.techDifferenceShow) != Mathf.RoundToInt(this.techDifference) || Mathf.RoundToInt(this.reviewPointsShow) != Mathf.RoundToInt(this.reviewPoints))
		{
			this.techDifferenceShow = this.pS_.GetTechDifference();
			this.reviewPointsShow = this.GetGesamtbewertung();
			return;
		}
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	private float GetGesamtbewertung()
	{
		float num = this.pS_.GetTechDifference();
		float num2;
		if (num >= 0f)
		{
			num2 = 9f + num;
		}
		else
		{
			num2 = 9f - Mathf.Abs(num) * 2f;
		}
		for (int i = 0; i < this.pS_.hwFeatures.Length; i++)
		{
			if (this.hardwareFeatures_.hardFeat_UNLOCK[i] && !this.pS_.hwFeatures[i])
			{
				num2 -= this.hardwareFeatures_.hardFeat_QUALITY[i] * 0.1f;
			}
		}
		if (this.pS_.typ == 1)
		{
			switch (this.pS_.anzController)
			{
			case 1:
				num2 -= 0.6f;
				break;
			case 2:
				num2 -= 0.4f;
				break;
			case 3:
				num2 -= 0.2f;
				break;
			}
		}
		int num3 = 0;
		for (int j = 0; j < this.hardware_.hardware_UNLOCK.Length; j++)
		{
			if (this.pS_.typ == 1)
			{
				if (this.hardware_.hardware_TYP[j] == 8 && this.hardware_.hardware_ONLYSTATIONARY[j])
				{
					num3++;
				}
			}
			else if (this.hardware_.hardware_TYP[j] == 8 && this.hardware_.hardware_ONLYHANDHELD[j])
			{
				num3++;
			}
		}
		float num4 = (float)(num3 - this.hardware_.hardware_TECH[this.pS_.component_case]) * 0.1f;
		num2 -= num4;
		if (num2 > 10f)
		{
			num2 = 10f;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		return num2;
	}

	
	public GameObject[] uiObjects;

	
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

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private platformScript pS_;

	
	private float techDifference;

	
	private float techDifferenceShow;

	
	private float reviewPoints;

	
	private float reviewPointsShow;
}
