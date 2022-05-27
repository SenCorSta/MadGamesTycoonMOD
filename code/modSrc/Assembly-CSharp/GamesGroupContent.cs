using System;
using UnityEngine;
using UnityEngine.UI;


public class GamesGroupContent : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
	}

	
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

	
	public GameObject[] uiTabs;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	public float timer;

	
	private int oldAmount;

	
	private bool noStandardGame = true;

	
	private bool noHandy = true;

	
	private bool noEigeneKonsole = true;

	
	private bool noArcade = true;

	
	private bool noSchublade = true;
}
