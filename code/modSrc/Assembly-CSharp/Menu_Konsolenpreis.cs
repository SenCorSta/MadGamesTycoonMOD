using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Konsolenpreis : MonoBehaviour
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

	
	public void Init(platformScript plat_, taskKonsole t_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		this.task_ = t_;
		if (this.pS_.verkaufspreis <= 0)
		{
			this.uiObjects[2].GetComponent<Slider>().value = 25f;
			this.uiObjects[3].GetComponent<Slider>().value = (float)(this.pS_.GetAktuellProductionsCosts() + 10);
			this.uiObjects[11].GetComponent<Toggle>().isOn = true;
			this.uiObjects[12].GetComponent<Toggle>().isOn = false;
		}
		else
		{
			if (this.uiObjects[12].GetComponent<Toggle>().isOn)
			{
				this.verkaufspreis = this.pS_.GetAktuellProductionsCosts() + this.pS_.autoPreisGewinn;
			}
			else
			{
				this.verkaufspreis = this.pS_.GetVerkaufspreis();
			}
			this.devKitPreis = this.pS_.price;
			this.uiObjects[2].GetComponent<Slider>().value = (float)(this.pS_.price / 1000);
			this.uiObjects[3].GetComponent<Slider>().value = (float)this.verkaufspreis;
			this.uiObjects[11].GetComponent<Toggle>().isOn = this.pS_.thridPartyGames;
			this.uiObjects[12].GetComponent<Toggle>().isOn = this.pS_.autoPreis;
		}
		this.SLIDER_DevKitPreis();
		this.SLIDER_Verkaufspreis();
		this.TOGGLE_DevKit();
		this.TOGGLE_AutoPreis();
		this.SetData();
	}

	
	private void Update()
	{
		this.autoPreisGewinn = this.verkaufspreis - this.pS_.GetAktuellProductionsCosts();
		if (this.autoPreisGewinn < 0)
		{
			this.autoPreisGewinn = 0;
		}
		string text = this.tS_.GetText(1689);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.autoPreisGewinn, true));
		this.uiObjects[17].GetComponent<Text>().text = text;
		if (this.uiObjects[12].GetComponent<Toggle>().isOn)
		{
			if (this.autoPreisGewinn <= 0)
			{
				this.verkaufspreis = this.pS_.GetAktuellProductionsCosts();
			}
			this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.verkaufspreis, true);
			this.uiObjects[3].GetComponent<Slider>().value = (float)this.verkaufspreis;
		}
		if (this.pS_.IsOutdatet())
		{
			if (!this.uiObjects[18].activeSelf)
			{
				this.uiObjects[18].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[18].activeSelf)
		{
			this.uiObjects[18].SetActive(false);
		}
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		this.uiObjects[8].GetComponent<InputField>().text = this.devKitPreis.ToString();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetAktuellProductionsCosts(), true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.verkaufspreis, true);
		int num = this.verkaufspreis - this.pS_.GetAktuellProductionsCosts();
		this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		if (num < 0)
		{
			this.uiObjects[7].GetComponent<Text>().color = this.guiMain_.colors[18];
		}
		else
		{
			this.uiObjects[7].GetComponent<Text>().color = this.guiMain_.colors[13];
		}
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.Round(this.pS_.kostenreduktion, 1).ToString() + "%";
		this.uiObjects[10].GetComponent<Image>().fillAmount = this.pS_.kostenreduktion * 0.01f;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.task_)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[326]);
			this.guiMain_.uiObjects[326].GetComponent<Menu_Dev_KonsoleComplete>().Init(this.pS_, this.task_);
			this.guiMain_.OpenMenu(false);
		}
		this.task_ = null;
	}

	
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.price = this.devKitPreis;
		this.pS_.verkaufspreis = this.verkaufspreis;
		if (this.pS_.price < 0)
		{
			this.pS_.price = 0;
		}
		if (this.pS_.verkaufspreis < 59)
		{
			this.pS_.verkaufspreis = 59;
		}
		this.pS_.thridPartyGames = this.uiObjects[11].GetComponent<Toggle>().isOn;
		this.pS_.autoPreis = this.uiObjects[12].GetComponent<Toggle>().isOn;
		this.pS_.autoPreisGewinn = this.autoPreisGewinn;
		if (this.task_)
		{
			this.pS_.SetOnMarket();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[329]);
			this.guiMain_.uiObjects[329].GetComponent<Menu_KonsolenReview>().Init(this.pS_);
		}
		else if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Platform(this.pS_);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Platform(this.pS_);
			}
		}
		if (this.task_)
		{
			UnityEngine.Object.Destroy(this.task_.gameObject);
		}
		base.gameObject.SetActive(false);
	}

	
	private IEnumerator iMinusDevKit()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusDevKit();
		}
		yield break;
	}

	
	public void BUTTON_MinusDevKit()
	{
		int num = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num--;
		this.uiObjects[2].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iMinusDevKit());
		this.SetData();
	}

	
	private IEnumerator iPlusDevKit()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusDevKit();
		}
		yield break;
	}

	
	public void BUTTON_PlusDevKit()
	{
		int num = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num++;
		this.uiObjects[2].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iPlusDevKit());
		this.SetData();
	}

	
	private IEnumerator iMinusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusVerkaufspreis(i);
		}
		yield break;
	}

	
	public void BUTTON_MinusVerkaufspreis(int i)
	{
		int num = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num -= i;
		this.uiObjects[3].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iMinusVerkaufspreis(i));
		this.SetData();
	}

	
	private IEnumerator iPlusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusVerkaufspreis(i);
		}
		yield break;
	}

	
	public void BUTTON_PlusVerkaufspreis(int i)
	{
		int num = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num += i;
		this.uiObjects[3].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iPlusVerkaufspreis(i));
		this.SetData();
	}

	
	public void INPUTFIELD_DevKitPreis()
	{
		if (this.uiObjects[8].GetComponent<InputField>().text.Length <= 0)
		{
			this.devKitPreis = 0;
			return;
		}
		this.devKitPreis = int.Parse(this.uiObjects[8].GetComponent<InputField>().text);
		this.devKitPreis = this.devKitPreis / 1000 * 1000;
		this.uiObjects[2].GetComponent<Slider>().value = (float)(this.devKitPreis / 1000);
		this.SetData();
	}

	
	public void SLIDER_Verkaufspreis()
	{
		this.verkaufspreis = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.SetData();
	}

	
	public void SLIDER_DevKitPreis()
	{
		this.devKitPreis = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value) * 1000;
		this.SetData();
	}

	
	public void TOGGLE_DevKit()
	{
		if (this.uiObjects[11].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[8].GetComponent<InputField>().interactable = true;
			this.uiObjects[2].GetComponent<Slider>().interactable = true;
			this.uiObjects[13].GetComponent<Button>().interactable = true;
			this.uiObjects[14].GetComponent<Button>().interactable = true;
			return;
		}
		this.uiObjects[8].GetComponent<InputField>().interactable = false;
		this.uiObjects[2].GetComponent<Slider>().interactable = false;
		this.uiObjects[13].GetComponent<Button>().interactable = false;
		this.uiObjects[14].GetComponent<Button>().interactable = false;
	}

	
	public void TOGGLE_AutoPreis()
	{
	}

	
	public GameObject[] uiObjects;

	
	public int verkaufspreis;

	
	public int devKitPreis;

	
	public int autoPreisGewinn = 10;

	
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

	
	private platformScript pS_;

	
	private taskKonsole task_;
}
