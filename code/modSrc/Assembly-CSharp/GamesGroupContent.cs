using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001BC RID: 444
public class GamesGroupContent : MonoBehaviour
{
	// Token: 0x060010AE RID: 4270 RVA: 0x0000BC9C File Offset: 0x00009E9C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x000BD684 File Offset: 0x000BB884
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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

	// Token: 0x060010B0 RID: 4272 RVA: 0x0000BC9C File Offset: 0x00009E9C
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x000BD73C File Offset: 0x000BB93C
	private void Update()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		this.timer += Time.deltaTime;
		if (base.gameObject.transform.childCount != this.oldAmount)
		{
			this.timer = 10f;
			this.oldAmount = base.gameObject.transform.childCount;
		}
		if (this.timer < 1f)
		{
			return;
		}
		this.timer = 0f;
		this.noStandardGame = true;
		this.noHandy = true;
		this.noArcade = true;
		this.noEigeneKonsole = true;
		this.noSchublade = true;
		for (int i = 0; i < base.gameObject.transform.childCount; i++)
		{
			GameObject gameObject = base.gameObject.transform.GetChild(i).gameObject;
			gameTab component = gameObject.GetComponent<gameTab>();
			if (component && component.gS_)
			{
				if (component.gS_.schublade)
				{
					this.noSchublade = false;
				}
				if (component.gS_.arcade && !component.gS_.schublade)
				{
					this.noArcade = false;
				}
				if (component.gS_.handy && !component.gS_.schublade)
				{
					this.noHandy = false;
				}
				if (!component.gS_.arcade && !component.gS_.handy && !component.gS_.schublade)
				{
					this.noStandardGame = false;
				}
				this.SetTab(gameObject, component);
			}
			else
			{
				konsoleTab component2 = gameObject.GetComponent<konsoleTab>();
				if (component2 && component2.pS_)
				{
					this.noEigeneKonsole = false;
					this.SetTabKonsole(gameObject, component2);
				}
			}
		}
		if (this.noStandardGame)
		{
			if (this.uiTabs[1].activeSelf)
			{
				this.uiTabs[1].SetActive(false);
			}
		}
		else if (!this.uiTabs[1].activeSelf)
		{
			this.uiTabs[1].SetActive(true);
		}
		if (this.noArcade)
		{
			if (this.uiTabs[2].activeSelf)
			{
				this.uiTabs[2].SetActive(false);
			}
		}
		else if (!this.uiTabs[2].activeSelf)
		{
			this.uiTabs[2].SetActive(true);
		}
		if (this.noHandy)
		{
			if (this.uiTabs[3].activeSelf)
			{
				this.uiTabs[3].SetActive(false);
			}
		}
		else if (!this.uiTabs[3].activeSelf)
		{
			this.uiTabs[3].SetActive(true);
		}
		if (this.noEigeneKonsole)
		{
			if (this.uiTabs[4].activeSelf)
			{
				this.uiTabs[4].SetActive(false);
			}
		}
		else if (!this.uiTabs[4].activeSelf)
		{
			this.uiTabs[4].SetActive(true);
		}
		if (this.noSchublade)
		{
			if (this.uiTabs[5].activeSelf)
			{
				this.uiTabs[5].SetActive(false);
			}
		}
		else if (!this.uiTabs[5].activeSelf)
		{
			this.uiTabs[5].SetActive(true);
		}
		if (this.noStandardGame && this.noArcade && this.noHandy && this.noEigeneKonsole && this.noSchublade)
		{
			if (this.uiTabs[0].activeSelf)
			{
				this.uiTabs[0].SetActive(false);
			}
		}
		else if (!this.uiTabs[0].activeSelf)
		{
			this.uiTabs[0].SetActive(true);
		}
		if (this.uiTabs[1].GetComponent<Toggle>().isOn && !this.uiTabs[1].activeSelf)
		{
			this.uiTabs[1].GetComponent<Toggle>().isOn = false;
			this.uiTabs[0].GetComponent<Toggle>().isOn = true;
		}
		if (this.uiTabs[2].GetComponent<Toggle>().isOn && !this.uiTabs[2].activeSelf)
		{
			this.uiTabs[2].GetComponent<Toggle>().isOn = false;
			this.uiTabs[0].GetComponent<Toggle>().isOn = true;
		}
		if (this.uiTabs[3].GetComponent<Toggle>().isOn && !this.uiTabs[3].activeSelf)
		{
			this.uiTabs[3].GetComponent<Toggle>().isOn = false;
			this.uiTabs[0].GetComponent<Toggle>().isOn = true;
		}
		if (this.uiTabs[4].GetComponent<Toggle>().isOn && !this.uiTabs[4].activeSelf)
		{
			this.uiTabs[4].GetComponent<Toggle>().isOn = false;
			this.uiTabs[0].GetComponent<Toggle>().isOn = true;
		}
		if (this.uiTabs[5].GetComponent<Toggle>().isOn && !this.uiTabs[5].activeSelf)
		{
			this.uiTabs[5].GetComponent<Toggle>().isOn = false;
			this.uiTabs[0].GetComponent<Toggle>().isOn = true;
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x000BDC1C File Offset: 0x000BBE1C
	private void SetTabKonsole(GameObject go, konsoleTab script_)
	{
		if (this.uiTabs[0].GetComponent<Toggle>().isOn || this.uiTabs[4].GetComponent<Toggle>().isOn)
		{
			if (!go.activeSelf)
			{
				go.SetActive(true);
				return;
			}
		}
		else if (go.activeSelf)
		{
			go.SetActive(false);
		}
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x000BDC70 File Offset: 0x000BBE70
	private void SetTab(GameObject go, gameTab script_)
	{
		if (!this.uiTabs[0].GetComponent<Toggle>().isOn)
		{
			if (!script_.gS_.arcade && !script_.gS_.handy && !script_.gS_.schublade && !this.uiTabs[1].GetComponent<Toggle>().isOn)
			{
				if (go.activeSelf)
				{
					go.SetActive(false);
				}
				return;
			}
			if (script_.gS_.arcade && !script_.gS_.schublade && !this.uiTabs[2].GetComponent<Toggle>().isOn)
			{
				if (go.activeSelf)
				{
					go.SetActive(false);
				}
				return;
			}
			if (script_.gS_.handy && !script_.gS_.schublade && !this.uiTabs[3].GetComponent<Toggle>().isOn)
			{
				if (go.activeSelf)
				{
					go.SetActive(false);
				}
				return;
			}
			if (script_.gS_.schublade && !this.uiTabs[5].GetComponent<Toggle>().isOn)
			{
				if (go.activeSelf)
				{
					go.SetActive(false);
				}
				return;
			}
		}
		if (script_.gS_.typ_bundle)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[9])
			{
				go.SetActive(this.mS_.gameTabFilter[9]);
			}
			return;
		}
		if (script_.gS_.typ_bundleAddon)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[10])
			{
				go.SetActive(this.mS_.gameTabFilter[10]);
			}
			return;
		}
		if (script_.gS_.typ_remaster)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[2])
			{
				go.SetActive(this.mS_.gameTabFilter[2]);
			}
			return;
		}
		if (script_.gS_.typ_spinoff)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[3])
			{
				go.SetActive(this.mS_.gameTabFilter[3]);
			}
			return;
		}
		if (script_.gS_.typ_addon)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[4])
			{
				go.SetActive(this.mS_.gameTabFilter[4]);
			}
			return;
		}
		if (script_.gS_.typ_addonStandalone)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[5])
			{
				go.SetActive(this.mS_.gameTabFilter[5]);
			}
			return;
		}
		if (script_.gS_.typ_mmoaddon)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[6])
			{
				go.SetActive(this.mS_.gameTabFilter[6]);
			}
			return;
		}
		if (script_.gS_.typ_budget)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[7])
			{
				go.SetActive(this.mS_.gameTabFilter[7]);
			}
			return;
		}
		if (script_.gS_.typ_goty)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[8])
			{
				go.SetActive(this.mS_.gameTabFilter[8]);
			}
			return;
		}
		if (script_.gS_.typ_nachfolger)
		{
			if (go.activeSelf != this.mS_.gameTabFilter[1])
			{
				go.SetActive(this.mS_.gameTabFilter[1]);
			}
			return;
		}
		if (go.activeSelf != this.mS_.gameTabFilter[0])
		{
			go.SetActive(this.mS_.gameTabFilter[0]);
		}
	}

	// Token: 0x04001541 RID: 5441
	public GameObject[] uiTabs;

	// Token: 0x04001542 RID: 5442
	public GameObject[] uiObjects;

	// Token: 0x04001543 RID: 5443
	private GameObject main_;

	// Token: 0x04001544 RID: 5444
	private mainScript mS_;

	// Token: 0x04001545 RID: 5445
	private textScript tS_;

	// Token: 0x04001546 RID: 5446
	private GUI_Main guiMain_;

	// Token: 0x04001547 RID: 5447
	private sfxScript sfx_;

	// Token: 0x04001548 RID: 5448
	public float timer;

	// Token: 0x04001549 RID: 5449
	private int oldAmount;

	// Token: 0x0400154A RID: 5450
	private bool noStandardGame = true;

	// Token: 0x0400154B RID: 5451
	private bool noHandy = true;

	// Token: 0x0400154C RID: 5452
	private bool noEigeneKonsole = true;

	// Token: 0x0400154D RID: 5453
	private bool noArcade = true;

	// Token: 0x0400154E RID: 5454
	private bool noSchublade = true;
}
