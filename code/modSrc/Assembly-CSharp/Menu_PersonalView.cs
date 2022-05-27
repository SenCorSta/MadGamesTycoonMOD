using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_PersonalView : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void Update()
	{
		if (!this.cS_)
		{
			this.BUTTON_Close();
		}
		this.MultiplayerUpdate();
	}

	
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
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
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
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

	
	public void Init(characterScript charScript_)
	{
		this.FindScripts();
		this.cS_ = charScript_;
		this.mS_.CreateFoto(this.cS_, null);
		this.uiObjects[0].GetComponent<InputField>().text = this.cS_.myName;
		this.SetData();
	}

	
	private void SetData()
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[17].GetComponent<Text>().text = this.tS_.GetText(137 + this.cS_.beruf);
		this.SetBalken(this.uiObjects[1], this.cS_.s_gamedesign, 0);
		this.SetBalken(this.uiObjects[2], this.cS_.s_programmieren, 1);
		this.SetBalken(this.uiObjects[3], this.cS_.s_grafik, 2);
		this.SetBalken(this.uiObjects[4], this.cS_.s_sound, 3);
		this.SetBalken(this.uiObjects[5], this.cS_.s_pr, 4);
		this.SetBalken(this.uiObjects[6], this.cS_.s_gametests, 5);
		this.SetBalken(this.uiObjects[7], this.cS_.s_technik, 6);
		this.SetBalken(this.uiObjects[8], this.cS_.s_forschen, 7);
		this.guiMain_.CreatePerkIcons(this.cS_, this.uiObjects[9].transform);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cS_.GetGehalt(), true);
		this.uiObjects[11].GetComponent<Image>().fillAmount = this.cS_.s_motivation * 0.01f;
		this.uiObjects[11].GetComponent<Image>().color = this.GetValColor(this.cS_.s_motivation);
		this.uiObjects[12].GetComponent<Text>().text = Mathf.RoundToInt(this.cS_.s_motivation).ToString();
		this.uiObjects[13].GetComponent<Text>().text = this.cS_.GetGroupString("magenta");
		this.uiObjects[19].SetActive(false);
		this.uiObjects[20].SetActive(false);
		this.uiObjects[21].SetActive(false);
		this.uiObjects[22].SetActive(false);
		this.uiObjects[23].SetActive(false);
		if (this.cS_.krank > 0)
		{
			this.uiObjects[19].SetActive(true);
		}
		int num = Mathf.RoundToInt(this.cS_.transform.position.x);
		int num2 = Mathf.RoundToInt(this.cS_.transform.position.z);
		if (this.mapS_.IsInMapLimit(num, num2))
		{
			if (!this.cS_.perks[16])
			{
				if (this.mapS_.mapMuell[num, num2] > 0f)
				{
					this.uiObjects[22].SetActive(true);
				}
				else
				{
					this.uiObjects[22].SetActive(false);
				}
			}
			if (this.mapS_.mapRoomID[num, num2] != 0)
			{
				if (!this.cS_.perks[11])
				{
					if (this.mapS_.mapWaerme[num, num2] <= 0.2f)
					{
						this.uiObjects[21].SetActive(true);
					}
					else
					{
						this.uiObjects[21].SetActive(false);
					}
				}
				if (!this.cS_.perks[12])
				{
					if (this.mapS_.mapAusstattung[num, num2] <= 0.2f)
					{
						this.uiObjects[23].SetActive(true);
					}
					else
					{
						this.uiObjects[23].SetActive(false);
					}
				}
				if (!this.cS_.perks[17])
				{
					if (this.cS_.IsUeberfuellt())
					{
						this.uiObjects[20].SetActive(true);
					}
					else
					{
						this.uiObjects[20].SetActive(false);
					}
				}
			}
		}
		this.uiObjects[14].SetActive(true);
		this.uiObjects[15].SetActive(true);
		this.uiObjects[16].SetActive(false);
		this.uiObjects[18].SetActive(false);
		if (this.cS_.perks[0])
		{
			this.uiObjects[14].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[14].GetComponent<Button>().interactable = true;
		}
		if (this.guiMain_.uiObjects[196].activeSelf)
		{
			this.uiObjects[14].SetActive(false);
			this.uiObjects[15].SetActive(false);
			this.uiObjects[16].SetActive(true);
			return;
		}
		if (this.guiMain_.uiObjects[324].activeSelf)
		{
			this.uiObjects[14].SetActive(false);
			this.uiObjects[15].SetActive(false);
			this.uiObjects[18].SetActive(true);
			return;
		}
	}

	
	public void SetBalken(GameObject go, float val, int beruf_)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
		if (this.cS_.beruf == beruf_)
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 1f;
			return;
		}
		if (this.cS_.perks[15])
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.6f;
			return;
		}
		go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.5f;
	}

	
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

	
	public void INPUTFIELD_Name()
	{
		if (this.cS_)
		{
			this.cS_.myName = this.uiObjects[0].GetComponent<InputField>().text;
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, false);
		base.gameObject.SetActive(false);
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
		if (this.mS_.multiplayer)
		{
			this.uiObjects[0].GetComponent<InputField>().interactable = !this.mS_.multiplayer;
		}
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	
	public void BUTTON_Select()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		if (this.guiMain_.uiObjects[26].activeSelf)
		{
			this.guiMain_.uiObjects[26].SetActive(false);
		}
		if (this.guiMain_.uiObjects[29].activeSelf)
		{
			this.guiMain_.uiObjects[29].SetActive(false);
		}
		this.pcS_.PickFromExternObject(this.cS_.gameObject);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Entlassen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[27]);
		this.guiMain_.uiObjects[27].GetComponent<Menu_PersonalEntlassen>().AddCharacter(this.cS_);
	}

	
	public void BUTTON_LeitenderDesigner()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[73].activeSelf)
		{
			this.guiMain_.uiObjects[73].GetComponent<Menu_Dev_GameEntwicklungsbericht>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetLeitenderDesigner(this.cS_, true);
			this.guiMain_.uiObjects[196].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
	}

	
	public void BUTTON_LeitenderTechniker()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[318].activeSelf)
		{
			this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().SetLeitenderTechniker(this.cS_, true);
			this.guiMain_.uiObjects[324].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[325].activeSelf)
		{
			this.guiMain_.uiObjects[325].GetComponent<Menu_Dev_KonsoleEntwicklungsbericht>().SetLeitenderTechniker(this.cS_, true);
			this.guiMain_.uiObjects[324].SetActive(false);
			base.gameObject.SetActive(false);
			return;
		}
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private pickCharacterScript pcS_;

	
	private characterScript cS_;

	
	private cameraMovementScript cmS_;

	
	private mapScript mapS_;

	
	public GameObject[] uiObjects;

	
	private float updateTimer;
}
