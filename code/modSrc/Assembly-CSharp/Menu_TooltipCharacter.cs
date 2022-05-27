using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_TooltipCharacter : MonoBehaviour
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
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
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

	
	private void Update()
	{
		if (!this.charS_)
		{
			base.gameObject.SetActive(false);
		}
	}

	
	public void Init(characterScript script_)
	{
		this.charS_ = script_;
		if (this.charS_)
		{
			this.FindScripts();
			this.UpdateData();
		}
	}

	
	public void UpdateData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.charS_.myName;
		this.uiObjects[16].GetComponent<Text>().text = this.tS_.GetText(137 + this.charS_.beruf);
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney((long)this.charS_.GetGehalt(), true);
		this.SetBalken(this.uiObjects[1], this.charS_.s_gamedesign, 0);
		this.SetBalken(this.uiObjects[2], this.charS_.s_programmieren, 1);
		this.SetBalken(this.uiObjects[3], this.charS_.s_grafik, 2);
		this.SetBalken(this.uiObjects[4], this.charS_.s_sound, 3);
		this.SetBalken(this.uiObjects[5], this.charS_.s_pr, 4);
		this.SetBalken(this.uiObjects[6], this.charS_.s_gametests, 5);
		this.SetBalken(this.uiObjects[7], this.charS_.s_technik, 6);
		this.SetBalken(this.uiObjects[8], this.charS_.s_forschen, 7);
		this.guiMain_.CreatePerkIcons(this.charS_, this.uiObjects[10].transform);
		this.uiObjects[12].GetComponent<Image>().fillAmount = this.charS_.s_motivation * 0.01f;
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.Round(this.charS_.s_motivation, 0).ToString();
		this.uiObjects[12].GetComponent<Image>().color = this.GetValColor(this.charS_.s_motivation);
		this.uiObjects[19].SetActive(false);
		this.uiObjects[20].SetActive(false);
		this.uiObjects[21].SetActive(false);
		this.uiObjects[22].SetActive(false);
		this.uiObjects[23].SetActive(false);
		if (this.charS_.krank > 0)
		{
			this.uiObjects[19].SetActive(true);
		}
		int num = Mathf.RoundToInt(this.charS_.transform.position.x);
		int num2 = Mathf.RoundToInt(this.charS_.transform.position.z);
		if (this.mapS_.IsInMapLimit(num, num2))
		{
			if (!this.charS_.perks[16])
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
				if (!this.charS_.perks[11])
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
				if (!this.charS_.perks[12])
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
				if (!this.charS_.perks[17])
				{
					if (this.charS_.IsUeberfuellt())
					{
						this.uiObjects[20].SetActive(true);
						return;
					}
					this.uiObjects[20].SetActive(false);
				}
			}
		}
	}

	
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

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private characterScript charS_;

	
	private mapScript mapS_;
}
