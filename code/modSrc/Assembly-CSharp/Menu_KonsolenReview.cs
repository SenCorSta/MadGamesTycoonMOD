using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000163 RID: 355
public class Menu_KonsolenReview : MonoBehaviour
{
	// Token: 0x06000D47 RID: 3399 RVA: 0x00090EDF File Offset: 0x0008F0DF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x00090EE8 File Offset: 0x0008F0E8
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

	// Token: 0x06000D49 RID: 3401 RVA: 0x000910C4 File Offset: 0x0008F2C4
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

	// Token: 0x06000D4A RID: 3402 RVA: 0x00091128 File Offset: 0x0008F328
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

	// Token: 0x06000D4B RID: 3403 RVA: 0x0009125B File Offset: 0x0008F45B
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x00091290 File Offset: 0x0008F490
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

	// Token: 0x06000D4D RID: 3405 RVA: 0x00091310 File Offset: 0x0008F510
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

	// Token: 0x040011D4 RID: 4564
	public GameObject[] uiObjects;

	// Token: 0x040011D5 RID: 4565
	private GameObject main_;

	// Token: 0x040011D6 RID: 4566
	private mainScript mS_;

	// Token: 0x040011D7 RID: 4567
	private textScript tS_;

	// Token: 0x040011D8 RID: 4568
	private GUI_Main guiMain_;

	// Token: 0x040011D9 RID: 4569
	private sfxScript sfx_;

	// Token: 0x040011DA RID: 4570
	private genres genres_;

	// Token: 0x040011DB RID: 4571
	private themes themes_;

	// Token: 0x040011DC RID: 4572
	private licences licences_;

	// Token: 0x040011DD RID: 4573
	private engineFeatures eF_;

	// Token: 0x040011DE RID: 4574
	private cameraMovementScript cmS_;

	// Token: 0x040011DF RID: 4575
	private unlockScript unlock_;

	// Token: 0x040011E0 RID: 4576
	private gameplayFeatures gF_;

	// Token: 0x040011E1 RID: 4577
	private games games_;

	// Token: 0x040011E2 RID: 4578
	private hardware hardware_;

	// Token: 0x040011E3 RID: 4579
	private hardwareFeatures hardwareFeatures_;

	// Token: 0x040011E4 RID: 4580
	private platformScript pS_;

	// Token: 0x040011E5 RID: 4581
	private float techDifference;

	// Token: 0x040011E6 RID: 4582
	private float techDifferenceShow;

	// Token: 0x040011E7 RID: 4583
	private float reviewPoints;

	// Token: 0x040011E8 RID: 4584
	private float reviewPointsShow;
}
