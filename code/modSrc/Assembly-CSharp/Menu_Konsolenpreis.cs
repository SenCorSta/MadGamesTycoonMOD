using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000164 RID: 356
public class Menu_Konsolenpreis : MonoBehaviour
{
	// Token: 0x06000D4F RID: 3407 RVA: 0x000914B3 File Offset: 0x0008F6B3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x000914BC File Offset: 0x0008F6BC
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

	// Token: 0x06000D51 RID: 3409 RVA: 0x0009165C File Offset: 0x0008F85C
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

	// Token: 0x06000D52 RID: 3410 RVA: 0x000917E0 File Offset: 0x0008F9E0
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

	// Token: 0x06000D53 RID: 3411 RVA: 0x00091914 File Offset: 0x0008FB14
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

	// Token: 0x06000D54 RID: 3412 RVA: 0x00091AA0 File Offset: 0x0008FCA0
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

	// Token: 0x06000D55 RID: 3413 RVA: 0x00091B2C File Offset: 0x0008FD2C
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

	// Token: 0x06000D56 RID: 3414 RVA: 0x00091CC8 File Offset: 0x0008FEC8
	private IEnumerator iMinusDevKit()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusDevKit();
		}
		yield break;
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00091CD8 File Offset: 0x0008FED8
	public void BUTTON_MinusDevKit()
	{
		int num = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num--;
		this.uiObjects[2].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iMinusDevKit());
		this.SetData();
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00091D35 File Offset: 0x0008FF35
	private IEnumerator iPlusDevKit()
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusDevKit();
		}
		yield break;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00091D44 File Offset: 0x0008FF44
	public void BUTTON_PlusDevKit()
	{
		int num = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num++;
		this.uiObjects[2].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iPlusDevKit());
		this.SetData();
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00091DA1 File Offset: 0x0008FFA1
	private IEnumerator iMinusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusVerkaufspreis(i);
		}
		yield break;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00091DB8 File Offset: 0x0008FFB8
	public void BUTTON_MinusVerkaufspreis(int i)
	{
		int num = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num -= i;
		this.uiObjects[3].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iMinusVerkaufspreis(i));
		this.SetData();
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00091E16 File Offset: 0x00090016
	private IEnumerator iPlusVerkaufspreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusVerkaufspreis(i);
		}
		yield break;
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00091E2C File Offset: 0x0009002C
	public void BUTTON_PlusVerkaufspreis(int i)
	{
		int num = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num += i;
		this.uiObjects[3].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iPlusVerkaufspreis(i));
		this.SetData();
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00091E8C File Offset: 0x0009008C
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

	// Token: 0x06000D5F RID: 3423 RVA: 0x00091F15 File Offset: 0x00090115
	public void SLIDER_Verkaufspreis()
	{
		this.verkaufspreis = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.SetData();
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00091F3A File Offset: 0x0009013A
	public void SLIDER_DevKitPreis()
	{
		this.devKitPreis = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value) * 1000;
		this.SetData();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x00091F68 File Offset: 0x00090168
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

	// Token: 0x06000D62 RID: 3426 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_AutoPreis()
	{
	}

	// Token: 0x040011E9 RID: 4585
	public GameObject[] uiObjects;

	// Token: 0x040011EA RID: 4586
	public int verkaufspreis;

	// Token: 0x040011EB RID: 4587
	public int devKitPreis;

	// Token: 0x040011EC RID: 4588
	public int autoPreisGewinn = 10;

	// Token: 0x040011ED RID: 4589
	private GameObject main_;

	// Token: 0x040011EE RID: 4590
	private mainScript mS_;

	// Token: 0x040011EF RID: 4591
	private textScript tS_;

	// Token: 0x040011F0 RID: 4592
	private GUI_Main guiMain_;

	// Token: 0x040011F1 RID: 4593
	private sfxScript sfx_;

	// Token: 0x040011F2 RID: 4594
	private genres genres_;

	// Token: 0x040011F3 RID: 4595
	private themes themes_;

	// Token: 0x040011F4 RID: 4596
	private licences licences_;

	// Token: 0x040011F5 RID: 4597
	private engineFeatures eF_;

	// Token: 0x040011F6 RID: 4598
	private cameraMovementScript cmS_;

	// Token: 0x040011F7 RID: 4599
	private unlockScript unlock_;

	// Token: 0x040011F8 RID: 4600
	private gameplayFeatures gF_;

	// Token: 0x040011F9 RID: 4601
	private games games_;

	// Token: 0x040011FA RID: 4602
	private platformScript pS_;

	// Token: 0x040011FB RID: 4603
	private taskKonsole task_;
}
