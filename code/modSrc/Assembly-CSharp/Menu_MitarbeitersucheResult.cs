using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E1 RID: 481
public class Menu_MitarbeitersucheResult : MonoBehaviour
{
	// Token: 0x06001235 RID: 4661 RVA: 0x000C104B File Offset: 0x000BF24B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x000C1053 File Offset: 0x000BF253
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		if (!this.cA_)
		{
			this.BUTTON_Close();
		}
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x000C1084 File Offset: 0x000BF284
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x000C1170 File Offset: 0x000BF370
	public void Init(charArbeitsmarkt charArbeitsmarkt_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(31);
		this.mS_.CreateFoto(null, charArbeitsmarkt_);
		this.cA_ = charArbeitsmarkt_;
		string text = this.tS_.GetText(1717);
		text = text.Replace("<NAME>", this.cA_.myName);
		this.uiObjects[12].GetComponent<Text>().text = text;
		this.uiObjects[0].GetComponent<Text>().text = this.cA_.myName;
		this.uiObjects[11].GetComponent<Text>().text = this.tS_.GetText(137 + this.cA_.beruf);
		this.SetBalken(this.uiObjects[1], this.cA_.s_gamedesign, 0);
		this.SetBalken(this.uiObjects[2], this.cA_.s_programmieren, 1);
		this.SetBalken(this.uiObjects[3], this.cA_.s_grafik, 2);
		this.SetBalken(this.uiObjects[4], this.cA_.s_sound, 3);
		this.SetBalken(this.uiObjects[5], this.cA_.s_pr, 4);
		this.SetBalken(this.uiObjects[6], this.cA_.s_gametests, 5);
		this.SetBalken(this.uiObjects[7], this.cA_.s_technik, 6);
		this.SetBalken(this.uiObjects[8], this.cA_.s_forschen, 7);
		this.guiMain_.CreatePerkIconsArbeitsmarkt(this.cA_, this.uiObjects[9].transform);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cA_.GetGehalt(), true);
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x000C1348 File Offset: 0x000BF548
	public void SetBalken(GameObject go, float val, int beruf_)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
		if (this.cA_.beruf == beruf_)
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 1f;
			return;
		}
		if (this.cA_.perks[15])
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.6f;
			return;
		}
		go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.5f;
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000C1444 File Offset: 0x000BF644
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x000C14B8 File Offset: 0x000BF6B8
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, false);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x000C14E0 File Offset: 0x000BF6E0
	public void BUTTON_Einstellen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		if (this.guiMain_.uiObjects[30].activeSelf)
		{
			this.guiMain_.uiObjects[30].SetActive(false);
		}
		characterScript characterScript = this.main_.GetComponent<createCharScript>().CreateCharacter(this.cA_.myID, this.cA_.male, this.cA_.model_body);
		characterScript.model_body = this.cA_.model_body;
		characterScript.model_eyes = this.cA_.model_eyes;
		characterScript.model_hair = this.cA_.model_hair;
		characterScript.model_beard = this.cA_.model_beard;
		characterScript.model_skinColor = this.cA_.model_skinColor;
		characterScript.model_hairColor = this.cA_.model_hairColor;
		characterScript.model_beardColor = this.cA_.model_beardColor;
		characterScript.model_HoseColor = this.cA_.model_HoseColor;
		characterScript.model_ShirtColor = this.cA_.model_ShirtColor;
		characterScript.model_Add1Color = this.cA_.model_Add1Color;
		characterScript.gameObject.transform.GetChild(0).GetComponent<characterGFXScript>().Init(true);
		this.mS_.CopyArbeitsmarktCharacterData(this.cA_, characterScript);
		this.pcS_.PickFromExternObject(characterScript.gameObject);
		this.cA_.RemoveFromArbeitsmarkt(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040016A5 RID: 5797
	private mainScript mS_;

	// Token: 0x040016A6 RID: 5798
	private GameObject main_;

	// Token: 0x040016A7 RID: 5799
	private GUI_Main guiMain_;

	// Token: 0x040016A8 RID: 5800
	private sfxScript sfx_;

	// Token: 0x040016A9 RID: 5801
	private textScript tS_;

	// Token: 0x040016AA RID: 5802
	private pickCharacterScript pcS_;

	// Token: 0x040016AB RID: 5803
	private charArbeitsmarkt cA_;

	// Token: 0x040016AC RID: 5804
	private cameraMovementScript cmS_;

	// Token: 0x040016AD RID: 5805
	public GameObject[] uiObjects;
}
