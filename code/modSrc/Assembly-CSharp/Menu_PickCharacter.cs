using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B6 RID: 438
public class Menu_PickCharacter : MonoBehaviour
{
	// Token: 0x0600107A RID: 4218 RVA: 0x0000BA86 File Offset: 0x00009C86
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x000BB9BC File Offset: 0x000B9BBC
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.pcS_)
		{
			this.pcS_ = this.main_.GetComponent<pickCharacterScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x000BBAA8 File Offset: 0x000B9CA8
	private void Update()
	{
		if (!Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKeyUp(KeyCode.F1))
			{
				this.SetAllToGroup(1);
			}
			if (Input.GetKeyUp(KeyCode.F2))
			{
				this.SetAllToGroup(2);
			}
			if (Input.GetKeyUp(KeyCode.F3))
			{
				this.SetAllToGroup(3);
			}
			if (Input.GetKeyUp(KeyCode.F4))
			{
				this.SetAllToGroup(4);
			}
			if (Input.GetKeyUp(KeyCode.F5))
			{
				this.SetAllToGroup(5);
			}
			if (Input.GetKeyUp(KeyCode.F6))
			{
				this.SetAllToGroup(6);
			}
			if (Input.GetKeyUp(KeyCode.F7))
			{
				this.SetAllToGroup(7);
			}
			if (Input.GetKeyUp(KeyCode.F8))
			{
				this.SetAllToGroup(8);
			}
			if (Input.GetKeyUp(KeyCode.F9))
			{
				this.SetAllToGroup(9);
			}
			if (Input.GetKeyUp(KeyCode.F10))
			{
				this.SetAllToGroup(10);
			}
			if (Input.GetKeyUp(KeyCode.F11))
			{
				this.SetAllToGroup(11);
			}
			if (Input.GetKeyUp(KeyCode.F12))
			{
				this.SetAllToGroup(12);
			}
		}
		for (int i = 0; i < this.mS_.pickedChars.Count; i++)
		{
			if (!this.mS_.pickedChars[i])
			{
				this.mS_.pickedChars.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0000BA8E File Offset: 0x00009C8E
	private void OnDisable()
	{
		this.FindScripts();
		this.guiMain_.DeselectInputField();
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0000BAA1 File Offset: 0x00009CA1
	private void OnEnable()
	{
		this.FindScripts();
		this.UpdateData();
		if (this.mS_.multiplayer)
		{
			this.uiObjects[0].GetComponent<InputField>().interactable = !this.mS_.multiplayer;
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x000BBBF4 File Offset: 0x000B9DF4
	private void SetAllToGroup(int g)
	{
		if (this.mS_.pickedChars.Count > 0)
		{
			this.sfx_.PlaySound(30, false);
			for (int i = 0; i < this.mS_.pickedChars.Count; i++)
			{
				if (this.mS_.pickedChars[i])
				{
					this.mS_.pickedChars[i].GetComponent<characterScript>().group = g;
				}
			}
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x000BBC74 File Offset: 0x000B9E74
	public void UpdateData()
	{
		if (this.mS_.pickedChars.Count > 0 && this.mS_.pickedChars[0])
		{
			characterScript component = this.mS_.pickedChars[0].GetComponent<characterScript>();
			this.charS_ = component;
			this.mS_.CreateFoto(component, null);
			this.uiObjects[15].GetComponent<Animation>().Play();
			this.uiObjects[0].GetComponent<InputField>().text = component.myName;
			this.uiObjects[16].GetComponent<Text>().text = this.tS_.GetText(137 + component.beruf);
			this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetGehalt(), true);
			this.SetBalken(this.uiObjects[1], component.s_gamedesign, 0);
			this.SetBalken(this.uiObjects[2], component.s_programmieren, 1);
			this.SetBalken(this.uiObjects[3], component.s_grafik, 2);
			this.SetBalken(this.uiObjects[4], component.s_sound, 3);
			this.SetBalken(this.uiObjects[5], component.s_pr, 4);
			this.SetBalken(this.uiObjects[6], component.s_gametests, 5);
			this.SetBalken(this.uiObjects[7], component.s_technik, 6);
			this.SetBalken(this.uiObjects[8], component.s_forschen, 7);
			this.guiMain_.CreatePerkIcons(component, this.uiObjects[10].transform);
			this.uiObjects[12].GetComponent<Image>().fillAmount = component.s_motivation * 0.01f;
			this.uiObjects[13].GetComponent<Text>().text = this.mS_.Round(component.s_motivation, 0).ToString();
			this.uiObjects[12].GetComponent<Image>().color = this.GetValColor(component.s_motivation);
			if (component.perks[0])
			{
				this.uiObjects[14].GetComponent<Button>().interactable = false;
				return;
			}
			this.uiObjects[14].GetComponent<Button>().interactable = true;
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000BBEBC File Offset: 0x000BA0BC
	public void SetBalken(GameObject go, float val, int beruf_)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
		if (this.charS_.beruf == beruf_)
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 1f;
			return;
		}
		if (this.charS_.perks[15])
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.6f;
			return;
		}
		go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.5f;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000BBFB8 File Offset: 0x000BA1B8
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

	// Token: 0x06001083 RID: 4227 RVA: 0x000BC02C File Offset: 0x000BA22C
	public void INPUTFIELD_Name()
	{
		if (this.mS_.pickedChars.Count > 0)
		{
			characterScript component = this.mS_.pickedChars[0].GetComponent<characterScript>();
			if (component.myName != this.uiObjects[0].GetComponent<InputField>().text)
			{
				this.cmS_.disableMovement = true;
			}
			component.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0000BADC File Offset: 0x00009CDC
	public void INPUTFIELD_NameEnd()
	{
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x000BC0A4 File Offset: 0x000BA2A4
	public void BUTTON_Entlassen()
	{
		if (this.mS_.pickedChars.Count > 0 && this.mS_.pickedChars[0])
		{
			this.mS_.pickedChars[0].GetComponent<characterScript>().Entlassen(true);
			this.mS_.pickedChars.RemoveAt(0);
			this.sfx_.PlaySound(3, true);
			if (this.mS_.pickedChars.Count == 0)
			{
				this.guiMain_.DeactivateMenu(this.guiMain_.uiObjects[15]);
				this.guiMain_.CloseMenu();
			}
		}
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000BC150 File Offset: 0x000BA350
	public void AddCharToList(characterScript cS_)
	{
		this.FindScripts();
		Item_CharList component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], this.uiObjects[11].transform).GetComponent<Item_CharList>();
		component.mS_ = this.mS_;
		component.guiMain_ = this.guiMain_;
		component.cS_ = cS_;
		component.BUTTON_Click();
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0000BAEA File Offset: 0x00009CEA
	public void BUTTON_Abbrechen()
	{
		this.pcS_.ESC_DropChar();
	}

	// Token: 0x040014F9 RID: 5369
	public GameObject[] uiPrefabs;

	// Token: 0x040014FA RID: 5370
	public GameObject[] uiObjects;

	// Token: 0x040014FB RID: 5371
	private GameObject main_;

	// Token: 0x040014FC RID: 5372
	private mainScript mS_;

	// Token: 0x040014FD RID: 5373
	private textScript tS_;

	// Token: 0x040014FE RID: 5374
	private GUI_Main guiMain_;

	// Token: 0x040014FF RID: 5375
	private sfxScript sfx_;

	// Token: 0x04001500 RID: 5376
	private cameraMovementScript cmS_;

	// Token: 0x04001501 RID: 5377
	private characterScript charS_;

	// Token: 0x04001502 RID: 5378
	private pickCharacterScript pcS_;
}
