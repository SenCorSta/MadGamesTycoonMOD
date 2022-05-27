using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;


public class characterScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.InitUI();
		this.mS_.findCharacters = true;
		if (this.mS_.achScript_ && this.legend != -1)
		{
			this.mS_.achScript_.SetAchivement(68);
		}
	}

	
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findCharacters = true;
		}
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
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.clipS_)
		{
			this.clipS_ = this.main_.GetComponent<clipScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.camera_)
		{
			this.camera_ = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.mCamS_)
		{
			this.mCamS_ = GameObject.FindWithTag("MainCamera").GetComponent<mainCameraScript>();
		}
		if (!this.guiTooltip)
		{
			this.guiTooltip = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Tooltip>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.moveS_)
		{
			this.moveS_ = base.GetComponent<movementScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	
	public void Init()
	{
		base.name = "CHAR_" + this.myID.ToString();
		this.s_motivation = 100f;
		this.durst = UnityEngine.Random.Range(15f, 100f);
		this.hunger = UnityEngine.Random.Range(15f, 100f);
		this.klo = UnityEngine.Random.Range(15f, 100f);
		this.waschbecken = UnityEngine.Random.Range(15f, 100f);
		this.muell = UnityEngine.Random.Range(15f, 100f);
		this.giessen = UnityEngine.Random.Range(15f, 100f);
		this.pause = UnityEngine.Random.Range(15f, 100f);
		this.freezer = UnityEngine.Random.Range(15f, 100f);
	}

	
	private void Update()
	{
		this.UpdateMyRoom();
		this.UpdateUsingObject();
		this.UpdateBelegtObject();
		this.UpdateBeduerfnisse();
		this.UpdateWork();
		this.UpdateUI();
		this.StopPopAnimations();
	}

	
	private void InitUI()
	{
		this.myUI = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(99999f, 99999f, 0f), Quaternion.identity);
		this.myUI.transform.SetParent(this.mS_.guiPops.transform);
		this.myUI.transform.SetSiblingIndex(0);
		this.myUIRect = this.myUI.GetComponent<RectTransform>();
		this.uiIconMain = this.myUI.transform.Find("IconMain").gameObject;
		this.uiWorkProgress = this.uiIconMain.transform.Find("WorkProgress").gameObject;
		if (this.uiWorkProgress)
		{
			this.uiWorkProgress_Image = this.uiWorkProgress.GetComponent<Image>();
		}
		this.uiIcon = this.uiIconMain.transform.Find("Icon").gameObject;
		if (this.uiIcon)
		{
			this.uiIcon_Image = this.uiIcon.GetComponent<Image>();
		}
		this.uiNoRoom = this.myUI.transform.Find("iNoRoom").gameObject;
		this.uiSprechblase = this.myUI.transform.Find("Sprechblase").gameObject;
		this.uiSprechblase.SetActive(false);
		this.uiKrank = this.uiIconMain.transform.Find("IconKrank").gameObject;
		this.uiKrank.SetActive(false);
		this.uiLeitenderDesigner = this.uiIconMain.transform.Find("IconLeitenderDesigner").gameObject;
		this.uiLeitenderDesigner.SetActive(false);
		this.uiFrieren = this.uiIconMain.transform.Find("IconFrieren").gameObject;
		this.uiFrieren.SetActive(false);
		this.uiGarbage = this.uiIconMain.transform.Find("IconGarbage").gameObject;
		this.uiGarbage.SetActive(false);
		this.uiUeberfuellt = this.uiIconMain.transform.Find("IconUeberfuellt").gameObject;
		this.uiUeberfuellt.SetActive(false);
		this.uiLowQuality = this.uiIconMain.transform.Find("IconLowQuality").gameObject;
		this.uiLowQuality.SetActive(false);
		this.ePop_Object = new GameObject[20];
		this.ePopAnim = new Animation[20];
		this.ePopText = new Text[20];
		this.ePop_Object[this.ePop_Gameplay] = this.myUI.transform.Find("eGameplay").gameObject;
		this.ePop_Object[this.ePop_Grafik] = this.myUI.transform.Find("eGraphic").gameObject;
		this.ePop_Object[this.ePop_Sound] = this.myUI.transform.Find("eSound").gameObject;
		this.ePop_Object[this.ePop_Technik] = this.myUI.transform.Find("eTechnik").gameObject;
		this.ePop_Object[this.ePop_Bug] = this.myUI.transform.Find("eBug").gameObject;
		this.ePop_Object[this.ePop_BugRemove] = this.myUI.transform.Find("eBugRemove").gameObject;
		this.ePop_Object[this.ePop_Forschung] = this.myUI.transform.Find("eForschung").gameObject;
		this.ePop_Object[this.ePop_Marketing] = this.myUI.transform.Find("eMarketing").gameObject;
		this.ePop_Object[this.ePop_Support] = this.myUI.transform.Find("eSupport").gameObject;
		this.ePop_Object[this.ePop_QA] = this.myUI.transform.Find("eQA").gameObject;
		this.ePop_Object[this.ePop_Training] = this.myUI.transform.Find("eTraining").gameObject;
		this.ePop_Object[this.ePop_Hype] = this.myUI.transform.Find("eHype").gameObject;
		this.ePop_Object[this.ePop_BeduerfnisErfuellt] = this.myUI.transform.Find("eBeduerfnisErfuellt").gameObject;
		this.ePop_Object[this.ePop_BeduerfnisNichtErfuellt] = this.myUI.transform.Find("eBeduerfnisNichtErfuellt").gameObject;
		this.ePop_Object[this.ePop_Arzt] = this.myUI.transform.Find("eArzt").gameObject;
		this.ePop_Object[this.ePop_ProdArcade] = this.myUI.transform.Find("eProdArcade").gameObject;
		this.ePop_Object[this.ePop_Hardware] = this.myUI.transform.Find("eHardware").gameObject;
		this.ePop_Object[this.ePop_Misc] = this.myUI.transform.Find("eMisc").gameObject;
		for (int i = 0; i < this.ePop_Object.Length; i++)
		{
			if (this.ePop_Object[i])
			{
				this.ePopAnim[i] = this.ePop_Object[i].GetComponent<Animation>();
				if (this.ePop_Object[i].transform.childCount > 0)
				{
					this.ePopText[i] = this.ePop_Object[i].transform.GetChild(0).GetComponent<Text>();
				}
			}
		}
		this.HidePops();
	}

	
	private void HidePops()
	{
		Vector3 localScale = new Vector3(0f, 0f, 0f);
		for (int i = 0; i < this.ePop_Object.Length; i++)
		{
			if (this.ePop_Object[i])
			{
				this.ePop_Object[i].transform.localScale = localScale;
			}
		}
	}

	
	private void StopPopAnimations()
	{
		this.timerStopPopAnimations += Time.deltaTime;
		if (this.timerStopPopAnimations < 5f)
		{
			return;
		}
		this.timerStopPopAnimations = 0f;
		for (int i = 0; i < this.ePop_Object.Length; i++)
		{
			if (this.ePop_Object[i] && this.ePop_Object[i].activeSelf && this.ePop_Object[i].transform.localScale.x <= 0f && this.ePopAnim[i].enabled)
			{
				this.ePopAnim[i].enabled = false;
			}
		}
	}

	
	public void HideChar()
	{
		if (this.picked)
		{
			return;
		}
		this.hided = true;
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	
	public void UnhideChar()
	{
		this.hided = false;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		this.HidePops();
	}

	
	public void UpdateKI(bool roomSpecific)
	{
		this.FindScripts();
		if (!this.mS_.personal_ki)
		{
			return;
		}
		if (this.picked)
		{
			return;
		}
		if (this.roomID != -1 && this.roomS_ && this.roomS_.lockKI)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		if (this.roomID != -1 && this.roomS_)
		{
			if (this.roomS_.taskID == -1)
			{
				flag = true;
			}
			if (!this.mainArbeitsplatzS_ && this.objectBelegtID == -1)
			{
				flag2 = true;
			}
		}
		if (this.roomID == -1 || flag || flag2)
		{
			List<roomScript> list = new List<roomScript>();
			for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
			{
				if (this.mS_.arrayRooms[i])
				{
					roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
					if (!component.lockKI && component.GetArbeitsplaetze() > 0 && component.typ != 13 && component.GetArbeitsplaetze() > component.GetMitarbeiter() && component.taskID != -1)
					{
						int num = Mathf.RoundToInt(base.transform.position.x);
						int num2 = Mathf.RoundToInt(base.transform.position.z);
						if (this.mapS_.IsInMapLimit(num, num2) && (!this.mS_.personal_dontLeaveBuilding || this.mapS_.mapBuilding[num, num2] == this.mapS_.mapBuilding[Mathf.RoundToInt(component.uiPos.x), Mathf.RoundToInt(component.uiPos.z)]))
						{
							list.Add(component);
						}
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j])
				{
					if (!roomSpecific)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 4 && this.beruf == 2)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 5 && this.beruf == 3)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 3 && this.beruf == 5)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 10 && this.beruf == 1)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 2 && this.beruf == 7)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 6 && this.beruf == 4)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 7 && this.beruf == 4)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 17 && this.beruf == 6)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 8 && this.beruf == 6)
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
					if (list[j].typ == 1 && (this.beruf == 2 || this.beruf == 3 || this.beruf == 1 || this.beruf == 0))
					{
						list[j].mitarbeiterZugeteilt++;
						this.roomS_ = list[j];
						this.roomID = list[j].myID;
						this.RemoveObjectUsing();
						this.mainArbeitsplatzS_ = null;
						return;
					}
				}
			}
		}
	}

	
	private void UpdateMyRoom()
	{
		if (this.roomID == -1)
		{
			this.roomS_ = null;
			return;
		}
		if (!this.roomS_)
		{
			GameObject gameObject = GameObject.Find("Room_" + this.roomID.ToString());
			if (gameObject)
			{
				this.roomS_ = gameObject.GetComponent<roomScript>();
				if (!this.mS_.settings_TutorialOff && this.roomS_.typ == 1)
				{
					this.guiMain_.SetTutorialStep(9);
					return;
				}
			}
			else
			{
				this.roomID = -1;
				this.moveS_.waitForceAnimation = 0.01f;
			}
		}
	}

	
	private void UpdateUI()
	{
		bool flag = false;
		if (this.guiMain_.menuOpen && !this.guiMain_.uiObjects[15].activeSelf)
		{
			flag = true;
		}
		if (this.hided || this.picked || flag)
		{
			this.uiVisible = false;
			if (this.myUI.activeSelf)
			{
				this.myUI.SetActive(false);
			}
			return;
		}
		if (!this.uiVisible)
		{
			this.invisibleTimer += Time.deltaTime;
			if (this.invisibleTimer < 0.1f)
			{
				return;
			}
			this.invisibleTimer = 0f;
		}
		Vector3 position = base.gameObject.transform.position;
		position.y += 1f;
		Vector2 vector = this.camera_.WorldToScreenPoint(position);
		if (vector.x >= 0f && vector.x <= (float)Screen.width && vector.y >= 0f && vector.y <= (float)Screen.height)
		{
			this.uiVisible = true;
			if (!this.myUI.activeSelf)
			{
				this.myUI.SetActive(true);
				this.myUI.GetComponent<Animation>().enabled = true;
			}
			vector = new Vector2(vector.x, vector.y - (float)Screen.height);
			this.myUIRect.anchoredPosition = this.guiMain_.GetAnchoredPosition(vector);
			this.UpdateIcon();
			this.UpdateSprechblase();
			this.DrawRoomLine();
			return;
		}
		this.uiVisible = false;
		if (this.myUI.activeSelf)
		{
			this.myUI.SetActive(false);
			this.HidePops();
		}
	}

	
	private IEnumerator CreatePopInSeconds_SPRITE(Sprite sprite_, string text_, float waitTime, int sound)
	{
		if (!this.settings_.disableWorkIcons)
		{
			yield return new WaitForSeconds(waitTime);
			this.CreatePop_SPRITE(sprite_, text_, sound);
		}
		yield break;
	}

	
	private void CreatePop_SPRITE(Sprite sprite_, string text_, int sound)
	{
		if (!this.uiVisible)
		{
			return;
		}
		if (!this.ePop_Object[this.ePop_Misc].activeSelf)
		{
			this.ePop_Object[this.ePop_Misc].SetActive(true);
		}
		this.ePopAnim[this.ePop_Misc].enabled = true;
		this.ePopAnim[this.ePop_Misc].Stop();
		this.ePopAnim[this.ePop_Misc].Play();
		if (!this.settings_.disableWorkIconSound)
		{
			this.sfx_.PlaySound(sound, true);
		}
		this.ePop_Object[this.ePop_Misc].GetComponent<Image>().sprite = sprite_;
		this.ePopText[this.ePop_Misc].text = text_;
	}

	
	private IEnumerator CreatePopInSeconds(int popID, float f, float waitTime, int sound)
	{
		if (!this.settings_.disableWorkIcons)
		{
			yield return new WaitForSeconds(waitTime);
			f = this.mS_.Round(f, 2);
			this.CreatePop(popID, f, sound);
		}
		yield break;
	}

	
	private void CreatePop(int popID, float f, int sound)
	{
		if (!this.uiVisible)
		{
			return;
		}
		if (!this.ePop_Object[popID].activeSelf)
		{
			this.ePop_Object[popID].SetActive(true);
		}
		this.ePopAnim[popID].enabled = true;
		this.ePopAnim[popID].Stop();
		this.ePopAnim[popID].Play();
		if (!this.settings_.disableWorkIconSound)
		{
			this.sfx_.PlaySound(sound, true);
		}
		if (this.ePopText[popID])
		{
			this.ePopText[popID].text = "+" + f.ToString();
		}
	}

	
	public int GetBestSkill()
	{
		float num = this.s_gamedesign;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 0;
		}
		num = this.s_programmieren;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 1;
		}
		num = this.s_grafik;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 2;
		}
		num = this.s_sound;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 3;
		}
		num = this.s_pr;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 4;
		}
		num = this.s_gametests;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 5;
		}
		num = this.s_technik;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 6;
		}
		num = this.s_forschen;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return 7;
		}
		return 0;
	}

	
	public float GetBestSkillValue()
	{
		float num = this.s_gamedesign;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_programmieren;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_grafik;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_sound;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_pr;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_gametests;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_technik;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik && num >= this.s_forschen)
		{
			return num;
		}
		num = this.s_forschen;
		if (num >= this.s_gamedesign && num >= this.s_programmieren && num >= this.s_grafik && num >= this.s_sound && num >= this.s_pr && num >= this.s_gametests && num >= this.s_technik)
		{
			float num2 = this.s_forschen;
			return num;
		}
		return num;
	}

	
	public string GetTooltip()
	{
		if (!this.tS_)
		{
			return "";
		}
		string text = string.Concat(new string[]
		{
			"<b>",
			this.GetGroupString("magenta"),
			" ",
			this.myName,
			"</b>"
		});
		if (this.group != -1 && this.mS_.personal_group_names[this.group - 1].Length > 0)
		{
			text = text + "\n" + this.GetGroupStringWithName("magenta");
		}
		if (this.perks[0])
		{
			text = text + "\n<b><color=green>" + this.tS_.GetText(506) + "</color></b>";
		}
		text = text + "\n<color=blue>" + this.tS_.GetText(137 + this.beruf) + "</color>";
		if (this.GetBestSkill() == 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(119),
				"[",
				this.mS_.Round(this.s_gamedesign, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 1)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(120),
				" [",
				this.mS_.Round(this.s_programmieren, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 2)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(121),
				" [",
				this.mS_.Round(this.s_grafik, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 3)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(122),
				" [",
				this.mS_.Round(this.s_sound, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 4)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(123),
				" [",
				this.mS_.Round(this.s_pr, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 5)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(124),
				" [",
				this.mS_.Round(this.s_gametests, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 6)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(125),
				" [",
				this.mS_.Round(this.s_technik, 1).ToString(),
				"]"
			});
		}
		if (this.GetBestSkill() == 7)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(126),
				" [",
				this.mS_.Round(this.s_forschen, 1).ToString(),
				"]"
			});
		}
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(109),
			" [",
			this.mS_.Round(this.s_motivation, 1).ToString(),
			"]"
		});
		if (this.objectBelegtS_ && !this.objectUsingS_ && !this.objectBelegtS_.isGhost)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n<b><color=blue>",
				this.tS_.GetText(314),
				" ",
				this.tS_.GetObjects(this.objectBelegtS_.typ),
				"</color></b>"
			});
		}
		if (this.objectUsingS_ && !this.objectUsingS_.isGhost)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n<b><color=blue>",
				this.tS_.GetText(315),
				" ",
				this.tS_.GetObjects(this.objectUsingS_.typ),
				"</color></b>"
			});
		}
		if (this.roomID == -1)
		{
			text = text + "\n<b><color=red>" + this.tS_.GetText(507) + "</color></b>";
		}
		if (this.krank > 0)
		{
			text = text + "\n<b><color=red>" + this.tS_.GetText(773) + "</color></b>";
		}
		int num = Mathf.RoundToInt(base.transform.position.x);
		int num2 = Mathf.RoundToInt(base.transform.position.z);
		if (this.mapS_.IsInMapLimit(num, num2))
		{
			if (!this.perks[16] && this.mapS_.mapMuell[num, num2] > 0f)
			{
				text = text + "\n<b><color=red>" + this.tS_.GetText(1287) + "</color></b>";
			}
			if (this.mapS_.mapRoomID[num, num2] != 0)
			{
				if (!this.perks[11] && this.mapS_.mapWaerme[num, num2] <= 0.2f)
				{
					text = text + "\n<b><color=red>" + this.tS_.GetText(1285) + "</color></b>";
				}
				if (!this.perks[12] && this.mapS_.mapAusstattung[num, num2] <= 0.2f)
				{
					text = text + "\n<b><color=red>" + this.tS_.GetText(1288) + "</color></b>";
				}
				if (!this.perks[17] && this.IsUeberfuellt())
				{
					text = text + "\n<b><color=red>" + this.tS_.GetText(1303) + "</color></b>";
				}
			}
		}
		return text;
	}

	
	private void DrawRoomLine()
	{
		if (this.guiMain_.menuOpen)
		{
			return;
		}
		if (base.gameObject.transform.GetChild(0).gameObject.layer != 11)
		{
			return;
		}
		if (this.roomS_)
		{
			if (!this.mS_.roomLine.active)
			{
				this.mS_.roomLine = new VectorLine("mainGUI_RoomLine", new List<Vector3>(2), 20f, LineType.Continuous, Joins.Weld);
				this.mS_.roomLine.endCap = "ArrowsCharRoom";
				return;
			}
			GameObject gameObject = this.mS_.roomLine.rectTransform.gameObject;
			if (gameObject.GetComponent<MeshRenderer>())
			{
				gameObject.GetComponent<MeshRenderer>().material.shader = this.mS_.shaders[0];
			}
			this.mS_.gameObject.transform.position = this.roomS_.uiPos;
			this.mS_.gameObject.transform.LookAt(base.transform.position);
			this.mS_.gameObject.transform.Translate(Vector3.forward * 0.7f);
			Vector3 position = this.mS_.gameObject.transform.position;
			this.mS_.gameObject.transform.position = base.transform.position;
			this.mS_.gameObject.transform.LookAt(this.roomS_.uiPos);
			this.mS_.gameObject.transform.Translate(Vector3.forward * 0.7f);
			Vector3 position2 = this.mS_.gameObject.transform.position;
			this.mS_.gameObject.transform.position = new Vector3(0f, 0f, 0f);
			this.mS_.roomLine.points3[0] = position;
			this.mS_.roomLine.points3[1] = position2;
			this.mS_.roomLine.color = this.guiMain_.colors[12];
			this.mS_.roomLine.Draw3D();
		}
	}

	
	public void MouseOver()
	{
		this.SetOutlineLayer();
		this.guiTooltip.SetActive(this.GetTooltip());
		if (this.guiMain_)
		{
			this.guiMain_.uiObjects[214].SetActive(true);
			this.guiMain_.uiObjects[214].GetComponent<Menu_TooltipCharacter>().Init(this);
		}
	}

	
	public void MouseLeave()
	{
		if (this.mS_.roomLine.active)
		{
			this.mS_.roomLine.points3[0] = new Vector3(0f, 0f, 0f);
			this.mS_.roomLine.points3[1] = new Vector3(0f, 0f, 0f);
			this.mS_.roomLine.Draw3D();
		}
		this.DisableOutlineLayer();
		this.guiTooltip.SetInactive();
		if (this.guiMain_)
		{
			this.guiMain_.uiObjects[214].SetActive(false);
		}
	}

	
	public void SetOutlineLayer()
	{
		if (!this.outline)
		{
			this.outline = true;
			if (this.mCamS_)
			{
				this.mCamS_.SetOutlineColor(2, 0.3f, 4);
			}
			this.SetLayer(11, base.gameObject.transform.GetChild(0));
		}
	}

	
	private void DisableOutlineLayer()
	{
		if (this.outline)
		{
			this.outline = false;
			this.SetLayer(0, base.gameObject.transform.GetChild(0));
		}
	}

	
	private void SetLayer(int newLayer, Transform trans)
	{
		trans.gameObject.layer = newLayer;
		foreach (object obj in trans)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.layer = newLayer;
			if (transform.childCount > 0)
			{
				this.SetLayer(newLayer, transform.transform);
			}
		}
	}

	
	public void PickUp()
	{
		if (this.objectUsingS_)
		{
			if (this.moveS_.currentAnimation == "wc" && this.objectUsingS_ && this.objectUsingS_.isWC)
			{
				this.objectUsingS_.gfxAnimation.Play("wcKabine1Auf");
			}
			if (this.objectUsingS_.gfxShow)
			{
				this.objectUsingS_.gfxShow.SetActive(false);
			}
		}
		this.picked = true;
		base.gameObject.layer = 2;
		this.roomID = -1;
		this.roomS_ = null;
		this.mS_.AddPickedChar(base.gameObject);
		this.RemoveObjectUsing();
		this.guiMain_.uiObjects[15].GetComponent<Menu_PickCharacter>().AddCharToList(this);
	}

	
	public void DropChar(Vector3 v)
	{
		this.picked = false;
		base.gameObject.layer = 12;
		this.mS_.RemovePickedChar(base.gameObject);
		this.guiMain_.CloseMenu();
		base.transform.position = new Vector3(v.x, 0f, v.z);
		this.sfx_.PlaySound(10, true);
		this.moveS_.SetAnimationForce("drop", this.clipS_.clip_drop);
		this.moveS_.RecalculatePath();
		this.mainArbeitsplatzS_ = null;
		int num = Mathf.RoundToInt(v.x);
		int num2 = Mathf.RoundToInt(v.z);
		if (this.mapS_.mapRoomID[num, num2] > 1)
		{
			if (this.mapS_.mapRoomScript[num, num2])
			{
				if (!this.rdS_.KeineMitarbeiter(this.mapS_.mapRoomScript[num, num2].typ))
				{
					this.roomID = this.mapS_.mapRoomID[num, num2];
				}
			}
			else
			{
				this.roomID = -1;
				this.roomS_ = null;
			}
		}
		else
		{
			this.roomID = -1;
			this.roomS_ = null;
		}
		this.moveS_.DeleteTarget();
		this.guiMain_.DeactivateMenu(this.guiMain_.uiObjects[15]);
		this.guiMain_.CloseMenu();
	}

	
	public void RemoveObjectUsing()
	{
		this.FindScripts();
		if (this.objectUsingID != -1 && this.objectUsingS_ && (this.objectUsingS_.isArbeitsplatz || this.objectUsingS_.isSeat || this.objectUsingS_.isSeatAufenthalt))
		{
			base.transform.position = new Vector3(this.objectUsingS_.waypoint.transform.position.x, 0f, this.objectUsingS_.waypoint.transform.position.z);
		}
		this.moveS_.waitForceAnimation = 0f;
		this.moveS_.DeleteTarget();
		if (this.objectUsingID != -1)
		{
			this.objectUsingID = -1;
			this.objectUsingS_ = null;
		}
		if (this.objectBelegtID != -1)
		{
			GameObject gameObject = GameObject.Find("O_" + this.objectBelegtID.ToString());
			if (gameObject)
			{
				gameObject.GetComponent<objectScript>().besetztCharID = -1;
			}
			this.objectBelegtID = -1;
			this.objectBelegtS_ = null;
		}
	}

	
	private void UpdateUsingObject()
	{
		if (this.objectUsingID != -1 && !this.objectUsingS_)
		{
			GameObject gameObject = GameObject.Find("O_" + this.objectUsingID.ToString());
			if (gameObject)
			{
				this.objectUsingS_ = gameObject.GetComponent<objectScript>();
				return;
			}
			this.RemoveObjectUsing();
		}
	}

	
	private void UpdateBelegtObject()
	{
		if (this.objectBelegtID != -1 && !this.objectBelegtS_)
		{
			GameObject gameObject = GameObject.Find("O_" + this.objectBelegtID.ToString());
			if (gameObject)
			{
				this.objectBelegtS_ = gameObject.GetComponent<objectScript>();
				return;
			}
			this.RemoveObjectUsing();
		}
	}

	
	public void UpdateKuendigen(bool force)
	{
		if (this.perks[4])
		{
			return;
		}
		if (this.perks[0])
		{
			return;
		}
		if (this.mS_.GetGameSpeed() <= 0f)
		{
			return;
		}
		if (!force)
		{
			if (this.s_motivation > 0f)
			{
				return;
			}
			if (this.objectUsingID != -1)
			{
				return;
			}
			if (UnityEngine.Random.Range(0, 500) == 1)
			{
				return;
			}
		}
		this.RemoveObjectUsing();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[84]);
		this.guiMain_.uiObjects[84].GetComponent<Menu_MitarberKuendigt>().Init(this);
	}

	
	public void UpdateKrank()
	{
		if (!this.mS_)
		{
			return;
		}
		if (this.perks[10])
		{
			return;
		}
		if (this.krank <= 0)
		{
			int num = 0;
			if (this.perks[19])
			{
				num = 1;
			}
			if (UnityEngine.Random.Range(0, 100) <= num || (this.mS_.globalEvent == 7 && UnityEngine.Random.Range(0, 100) >= 98))
			{
				this.krank = UnityEngine.Random.Range(2, 8);
				if (this.IsVisible())
				{
					this.sfx_.PlaySound(46);
					return;
				}
			}
		}
		else
		{
			this.krank--;
		}
	}

	
	private void UpdateBeduerfnisse()
	{
		if (this.mS_.GetDeltaTime() <= 0f)
		{
			return;
		}
		if (!this.moveS_)
		{
			return;
		}
		if (!this.mapS_)
		{
			return;
		}
		if (this.picked)
		{
			return;
		}
		if (this.moveS_.waitForceAnimation > 0f)
		{
			return;
		}
		float num = 0f;
		int num2 = Mathf.RoundToInt(base.transform.position.x);
		int num3 = Mathf.RoundToInt(base.transform.position.z);
		if (!this.mapS_.IsInMapLimit(num2, num3))
		{
			return;
		}
		float num4 = 0f;
		if (!this.perks[16])
		{
			num4 += this.mapS_.mapMuell[num2, num3] * 0.02f;
		}
		if (this.mapS_.mapRoomID[num2, num3] != 0)
		{
			if (!this.perks[12] && this.mapS_.mapAusstattung[num2, num3] <= 0.2f)
			{
				num4 += 0.02f;
			}
			if (!this.perks[11] && this.mapS_.mapWaerme[num2, num3] <= 0.2f)
			{
				num4 += 0.02f;
			}
			if (!this.perks[17] && this.IsUeberfuellt())
			{
				num4 += 0.02f;
			}
		}
		this.AddMotivation(-num4 * this.mS_.GetDeltaTime());
		if (this.roomID <= -1)
		{
			return;
		}
		if (this.objectUsingID == -1)
		{
			return;
		}
		if (!this.objectUsingS_)
		{
			return;
		}
		if (!this.objectUsingS_.isArbeitsplatz)
		{
			return;
		}
		this.AddMotivation(-0.02f * this.mS_.GetDeltaTime());
		int personal_pausen = this.mS_.personal_pausen;
		if (personal_pausen != 1)
		{
			if (personal_pausen == 2)
			{
				this.AddMotivation(-0.02f * this.mS_.GetDeltaTime());
			}
		}
		else
		{
			this.AddMotivation(-0.01f * this.mS_.GetDeltaTime());
		}
		if (!this.perks[20] && this.roomS_ && this.roomS_.IsCrunchtimeRead())
		{
			return;
		}
		this.durst -= this.mS_.GetDeltaTime() * num;
		this.klo -= this.mS_.GetDeltaTime() * num;
		this.waschbecken -= this.mS_.GetDeltaTime() * num;
		this.giessen -= this.mS_.GetDeltaTime() * num;
		this.pause -= this.mS_.GetDeltaTime() * num;
		if (this.durst < 0f)
		{
			this.durst = 0f;
		}
		if (this.klo < 0f)
		{
			this.klo = 0f;
		}
		if (this.waschbecken < 0f)
		{
			this.waschbecken = 0f;
		}
		if (this.giessen < 0f)
		{
			this.giessen = 0f;
		}
		if (this.pause < 0f)
		{
			this.pause = 0f;
		}
		this.timerForMovementActions += Time.deltaTime;
		if (this.timerForMovementActions < 1f)
		{
			return;
		}
		this.timerForMovementActions = 0f;
		if (this.krank > 0 && this.moveS_.FindObjectInRoom(11, null, false))
		{
			return;
		}
		if (this.s_motivation <= (float)this.mS_.personal_motivation && UnityEngine.Random.Range(0f, this.s_motivation) < 1f)
		{
			switch (UnityEngine.Random.Range(0, 5))
			{
			case 0:
				if (this.moveS_.FindObjectInRoom(8, null, false))
				{
					return;
				}
				break;
			case 1:
				if (this.moveS_.FindObjectInRoom(9, null, false))
				{
					return;
				}
				break;
			case 2:
				if (this.moveS_.FindObjectInRoom(10, null, false))
				{
					return;
				}
				break;
			case 3:
				if (this.moveS_.FindObjectInRoom(12, null, false))
				{
					return;
				}
				break;
			case 4:
				if (this.moveS_.FindObjectInRoom(16, null, false))
				{
					return;
				}
				break;
			}
			if (this.moveS_.FindObjectInRoom(8, null, false))
			{
				return;
			}
			if (this.moveS_.FindObjectInRoom(9, null, false))
			{
				return;
			}
			if (this.moveS_.FindObjectInRoom(10, null, false))
			{
				return;
			}
			if (this.moveS_.FindObjectInRoom(12, null, false))
			{
				return;
			}
			if (this.moveS_.FindObjectInRoom(16, null, false))
			{
				return;
			}
		}
		if (this.durst <= 0f)
		{
			this.durst = 0f;
			if (!this.moveS_.FindObjectInRoom(1, null, false))
			{
				this.GoToGhostObject(2, true);
			}
			return;
		}
		if (!this.perks[13] && this.klo <= 0f)
		{
			this.klo = 0f;
			if (!this.moveS_.FindObjectInRoom(4, null, false))
			{
				this.GoToGhostObject(8, false);
			}
			return;
		}
		if (this.waschbecken <= 0f)
		{
			this.waschbecken = 0f;
			if (!this.moveS_.FindObjectInRoom(5, null, false))
			{
				this.GoToGhostObject(10, false);
			}
			return;
		}
		if (!this.perks[8])
		{
			this.muell -= this.mS_.GetDeltaTime() * num;
			if (this.muell <= 0f)
			{
				this.muell = 0f;
				if (!this.moveS_.FindObjectInRoom(2, null, false))
				{
					this.GoToGhostObject(1, true);
				}
				return;
			}
		}
		if (this.perks[9] && this.giessen <= 0f)
		{
			this.giessen = 0f;
			if (!this.moveS_.FindObjectInRoom(3, null, false))
			{
				this.GoToGhostObject(3, true);
			}
			return;
		}
		if (this.perks[2] || this.pause > 0f)
		{
			return;
		}
		this.pause = 0f;
		bool flag = false;
		switch (UnityEngine.Random.Range(0, 4))
		{
		case 0:
			flag = this.moveS_.FindObjectInRoom(7, null, false);
			break;
		case 1:
			flag = this.moveS_.FindObjectInRoom(13, null, false);
			break;
		case 2:
			flag = this.moveS_.FindObjectInRoom(14, null, false);
			break;
		case 3:
			flag = this.moveS_.FindObjectInRoom(15, null, false);
			break;
		}
		if (this.moveS_.FindObjectInRoom(7, null, false))
		{
			return;
		}
		if (this.moveS_.FindObjectInRoom(13, null, false))
		{
			return;
		}
		if (this.moveS_.FindObjectInRoom(14, null, false))
		{
			return;
		}
		if (this.moveS_.FindObjectInRoom(15, null, false))
		{
			return;
		}
		if (flag)
		{
			return;
		}
		switch (UnityEngine.Random.Range(0, 4))
		{
		case 0:
			this.GoToGhostObject(4, false);
			return;
		case 1:
			this.GoToGhostObject(5, false);
			return;
		case 2:
			this.GoToGhostObject(6, false);
			return;
		case 3:
			this.GoToGhostObject(7, false);
			return;
		default:
			return;
		}
	}

	
	private void GoToGhostObject(int i, bool inRoom_)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mS_.miscGamePrefabs[i]);
		if (inRoom_)
		{
			gameObject.transform.position = this.roomS_.myDoor.transform.position;
			gameObject.GetComponent<objectScript>().InitGhostObject(i);
			this.moveS_.FindObjectInRoom(-1, gameObject, false);
			return;
		}
		int num = Mathf.RoundToInt(base.transform.position.x);
		int num2 = Mathf.RoundToInt(base.transform.position.z);
		Vector2 vector;
		if (this.mapS_.IsInMapLimit(num, num2))
		{
			vector = this.mapS_.FindRandomFloorInMyBuilding(this.mapS_.mapBuilding[num, num2]);
			gameObject.transform.position = new Vector3(vector.x, 0f, vector.y);
			gameObject.GetComponent<objectScript>().InitGhostObject(i);
			this.moveS_.FindObjectInRoom(-1, gameObject, false);
			return;
		}
		vector = this.mapS_.FindRandomFloor();
		gameObject.transform.position = new Vector3(vector.x, 0f, vector.y);
		gameObject.GetComponent<objectScript>().InitGhostObject(i);
		this.moveS_.FindObjectInRoom(-1, gameObject, false);
	}

	
	public void AddMotivation(float f)
	{
		this.s_motivation = 100f;
	}

	
	public void ResetKrank()
	{
		this.krank = 0;
		this.objectUsingS_.aufladungenAkt--;
		this.CreatePop(this.ePop_Arzt, 0f, 15);
	}

	
	public void ResetMotivation(bool erfuellt, float motivationRegen)
	{
		if (erfuellt)
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisErfuellt, 2.5f, 15);
			}
			this.AddMotivation(motivationRegen);
		}
	}

	
	public void ResetGiessen(bool erfuellt)
	{
		this.giessen = 100f;
		if (erfuellt)
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisErfuellt, 2.5f, 15);
			}
			this.AddMotivation(25f);
			if (UnityEngine.Random.Range(0, 100) < 10)
			{
				GameObject gameObject = this.mS_.CreateMuell(9, 9);
				gameObject.transform.position = base.transform.position;
				gameObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
				return;
			}
		}
		else
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisNichtErfuellt, 2.5f, 16);
			}
			this.AddMotivation(-5f);
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	
	public void ResetDurst(bool erfuellt)
	{
		this.durst = 100f;
		if (erfuellt)
		{
			this.objectUsingS_.aufladungenAkt--;
			if (UnityEngine.Random.Range(0, 100) < 10)
			{
				GameObject gameObject = this.mS_.CreateMuell(9, 9);
				gameObject.transform.position = base.transform.position;
				gameObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
				return;
			}
		}
		else
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisNichtErfuellt, 2.5f, 16);
			}
			this.AddMotivation(-5f);
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	
	public void ResetWC(bool erfuellt)
	{
		this.klo = 100f;
		if (!erfuellt)
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisNichtErfuellt, 2.5f, 16);
			}
			this.AddMotivation(-40f);
			GameObject gameObject = this.mS_.CreateMuell(9, 9);
			gameObject.transform.position = base.transform.position;
			gameObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	
	public void ResetWaschbecken(bool erfuellt)
	{
		this.waschbecken = 100f;
		if (erfuellt)
		{
			if (UnityEngine.Random.Range(0, 100) < 10)
			{
				GameObject gameObject = this.mS_.CreateMuell(9, 9);
				gameObject.transform.position = base.transform.position;
				gameObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
				return;
			}
		}
		else
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisNichtErfuellt, 2.5f, 16);
			}
			this.AddMotivation(-5f);
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	
	public void ResetFreezer(bool erfuellt)
	{
		this.freezer = 100f;
		if (erfuellt)
		{
			if (UnityEngine.Random.Range(0, 100) < 10)
			{
				GameObject gameObject = this.mS_.CreateMuell(9, 9);
				gameObject.transform.position = base.transform.position;
				gameObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
				return;
			}
		}
		else
		{
			if (this.IsVisible())
			{
				this.CreatePop(this.ePop_BeduerfnisNichtErfuellt, 2.5f, 16);
			}
			this.AddMotivation(-5f);
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	
	public void ResetPause(bool erfuellt)
	{
		this.pause = 100f - (float)(this.mS_.personal_pausen * 20);
		if (erfuellt && this.objectUsingS_.isGhost)
		{
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	
	public void ResetMuell(bool erfuellt)
	{
		this.muell = 100f;
		if (erfuellt)
		{
			this.objectUsingS_.aufladungenAkt--;
			return;
		}
		if (this.IsVisible())
		{
			this.CreatePop(this.ePop_BeduerfnisNichtErfuellt, 2.5f, 16);
		}
		GameObject gameObject = null;
		switch (UnityEngine.Random.Range(0, 5))
		{
		case 0:
			gameObject = this.mS_.CreateMuell(0, 0);
			break;
		case 1:
			gameObject = this.mS_.CreateMuell(11, 11);
			break;
		case 2:
			gameObject = this.mS_.CreateMuell(12, 12);
			break;
		case 3:
			gameObject = this.mS_.CreateMuell(13, 13);
			break;
		case 4:
			gameObject = this.mS_.CreateMuell(14, 14);
			break;
		}
		if (gameObject)
		{
			gameObject.transform.position = base.transform.position;
			gameObject.transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
		}
		UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		this.AddMotivation(-5f);
	}

	
	public void ShowAddObject(int i)
	{
		if (this.addObjects[i] && !this.addObjects[i].activeSelf)
		{
			this.addObjects[i].SetActive(true);
		}
	}

	
	public void HideAddObjects()
	{
		for (int i = 0; i < this.addObjects.Length; i++)
		{
			if (this.addObjects[i] && this.addObjects[i].activeSelf)
			{
				this.addObjects[i].SetActive(false);
			}
		}
	}

	
	public bool IsVisible()
	{
		return this.myRenderer.isVisible;
	}

	
	private Color GetMotivationColor(float val)
	{
		if (val < 30f)
		{
			return this.colors[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.colors[1];
		}
		if (val >= 70f)
		{
			return this.colors[2];
		}
		return this.colors[0];
	}

	
	private void UpdateIcon()
	{
		if (this.roomID == -1)
		{
			if (this.uiIconMain.activeSelf)
			{
				this.uiIconMain.SetActive(false);
			}
			if (!this.uiNoRoom.activeSelf)
			{
				this.uiNoRoom.SetActive(true);
				return;
			}
		}
		else
		{
			if (!this.uiIconMain.activeSelf)
			{
				this.uiIconMain.SetActive(true);
			}
			if (this.uiNoRoom.activeSelf)
			{
				this.uiNoRoom.SetActive(false);
			}
			if (this.uiIconMain.activeSelf)
			{
				this.uiWorkProgress_Image.fillAmount = this.workProgress;
				this.uiWorkProgress_Image.color = this.GetMotivationColor(this.s_motivation);
			}
			bool flag = false;
			if (this.krank > 0)
			{
				flag = true;
				if (!this.uiKrank.activeSelf)
				{
					this.uiKrank.SetActive(true);
				}
			}
			else if (this.uiKrank.activeSelf)
			{
				this.uiKrank.SetActive(false);
			}
			if (this.mapS_)
			{
				if (!this.settings_.disableArbeiterBeschwerden)
				{
					int num = Mathf.RoundToInt(base.transform.position.x);
					int num2 = Mathf.RoundToInt(base.transform.position.z);
					if (this.mapS_.IsInMapLimit(num, num2))
					{
						if (!this.perks[11])
						{
							if (!flag && this.mapS_.mapWaerme[num, num2] <= 0.2f && this.mapS_.mapRoomID[num, num2] != 0)
							{
								flag = true;
								if (!this.uiFrieren.activeSelf)
								{
									this.uiFrieren.SetActive(true);
								}
							}
							else if (this.uiFrieren.activeSelf)
							{
								this.uiFrieren.SetActive(false);
							}
						}
						if (!this.perks[16])
						{
							if (!flag && this.mapS_.mapMuell[num, num2] > 0f)
							{
								flag = true;
								if (!this.uiGarbage.activeSelf)
								{
									this.uiGarbage.SetActive(true);
								}
							}
							else if (this.uiGarbage.activeSelf)
							{
								this.uiGarbage.SetActive(false);
							}
						}
						if (!this.perks[12])
						{
							if (!flag && this.mapS_.mapAusstattung[num, num2] <= 0.2f && this.mapS_.mapRoomID[num, num2] != 0)
							{
								flag = true;
								if (!this.uiLowQuality.activeSelf)
								{
									this.uiLowQuality.SetActive(true);
								}
							}
							else if (this.uiLowQuality.activeSelf)
							{
								this.uiLowQuality.SetActive(false);
							}
						}
						if (!this.perks[17])
						{
							if (!flag && this.IsUeberfuellt())
							{
								if (!this.uiUeberfuellt.activeSelf)
								{
									this.uiUeberfuellt.SetActive(true);
								}
							}
							else if (this.uiUeberfuellt.activeSelf)
							{
								this.uiUeberfuellt.SetActive(false);
							}
						}
					}
				}
				else
				{
					if (this.uiFrieren.activeSelf)
					{
						this.uiFrieren.SetActive(false);
					}
					if (this.uiGarbage.activeSelf)
					{
						this.uiGarbage.SetActive(false);
					}
					if (this.uiLowQuality.activeSelf)
					{
						this.uiLowQuality.SetActive(false);
					}
					if (this.uiUeberfuellt.activeSelf)
					{
						this.uiUeberfuellt.SetActive(false);
					}
				}
			}
			this.UpdateLeitenderEntwicklerIcon();
			if (this.objectBelegtS_)
			{
				if (this.objectBelegtS_.canDrink || this.objectBelegtS_.isGhostDrink)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[2];
					return;
				}
				if (this.objectBelegtS_.isWC || this.objectBelegtS_.isGhostWC)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[7];
					return;
				}
				if (this.objectBelegtS_.isSink || this.objectBelegtS_.isGhostSink || this.objectBelegtS_.isHandtrockner)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[8];
					return;
				}
				if (this.objectBelegtS_.isGhostMuelleimer || this.objectBelegtS_.isMuelleimer)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[3];
					return;
				}
				if (this.objectBelegtS_.isPlant || this.objectBelegtS_.isGhostPlant)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[5];
					return;
				}
				if (this.objectBelegtS_.isGhostPause1 || this.objectBelegtS_.isGhostPause2 || this.objectBelegtS_.isGhostPause3 || this.objectBelegtS_.isGhostPause4 || this.objectBelegtS_.isSeat || this.objectBelegtS_.isFreezer || this.objectBelegtS_.isRadio || this.objectBelegtS_.isTV)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[6];
					return;
				}
				if (this.objectBelegtS_.isArcade || this.objectBelegtS_.isDart || this.objectBelegtS_.isMinigolf || this.objectBelegtS_.isPiano || this.objectBelegtS_.isSeatAufenthalt || this.objectBelegtS_.isGhostPause4 || this.objectBelegtS_.isSeat)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[9];
					return;
				}
			}
			if (!this.mainArbeitsplatzS_ && this.objectBelegtID == -1)
			{
				this.uiIcon_Image.sprite = this.guiMain_.charIcons[1];
				return;
			}
			roomScript workRoomScript = this.GetWorkRoomScript();
			if (!workRoomScript)
			{
				return;
			}
			if (workRoomScript.taskID != -1)
			{
				if (this.TrainingState(this.roomS_) == 2)
				{
					this.uiIcon_Image.sprite = this.guiMain_.charIcons[10];
					return;
				}
				this.uiIcon_Image.sprite = this.guiMain_.charIcons[0];
				return;
			}
			else
			{
				this.uiIcon_Image.sprite = this.guiMain_.charIcons[4];
			}
		}
	}

	
	public bool IsUeberfuellt()
	{
		return this.roomID != -1 && this.roomS_ && this.objectUsingID != -1 && this.objectUsingS_ && this.objectUsingS_.isArbeitsplatz && this.roomS_.IsUberberfuell();
	}

	
	public roomScript GetWorkRoomScript()
	{
		roomScript result = null;
		if (this.roomS_)
		{
			result = this.roomS_;
		}
		if (this.roomS_ && this.roomS_.taskGameObject)
		{
			taskUnterstuetzen taskUnterstuetzen = this.roomS_.GetTaskUnterstuetzen();
			if (taskUnterstuetzen)
			{
				result = taskUnterstuetzen.rS_;
			}
		}
		return result;
	}

	
	private void UpdateSprechblase()
	{
		if (!this.settings_.sprechblasen)
		{
			this.mS_.anzSprechblasen = 0;
			if (this.uiSprechblase.activeSelf)
			{
				this.uiSprechblase.SetActive(false);
			}
			return;
		}
		if (this.uiSprechblase.activeSelf && !this.uiSprechblase.GetComponent<Animation>().isPlaying)
		{
			this.uiSprechblase.SetActive(false);
			this.mS_.anzSprechblasen--;
		}
		if (this.mS_.GetGameSpeed() == 0f)
		{
			return;
		}
		if (this.mS_.anzSprechblasen >= 5)
		{
			return;
		}
		if (UnityEngine.Random.Range(0, 2500) == 1 && !this.uiSprechblase.activeSelf)
		{
			this.uiSprechblase.SetActive(true);
			this.mS_.anzSprechblasen++;
			if (this.mS_.tS_)
			{
				this.uiSprechblase.transform.GetChild(0).GetComponent<Text>().text = this.mS_.tS_.GetQuotes();
			}
		}
	}

	
	public bool IsNoWork_Unterstuetzen()
	{
		if (this.roomS_.taskGameObject)
		{
			taskUnterstuetzen taskUnterstuetzen = this.roomS_.GetTaskUnterstuetzen();
			if (taskUnterstuetzen)
			{
				if (!taskUnterstuetzen.rS_)
				{
					return true;
				}
				if (!taskUnterstuetzen.rS_.taskGameObject)
				{
					return true;
				}
				if (taskUnterstuetzen.rS_.IsGameDevComplete())
				{
					return true;
				}
				if (taskUnterstuetzen.rS_.WaitForMinimumHype())
				{
					return true;
				}
				if (taskUnterstuetzen.rS_.KeineAnrufe())
				{
					return true;
				}
				if (taskUnterstuetzen.rS_.QA_GameHasNoBugs())
				{
					return true;
				}
				if (taskUnterstuetzen.rS_.KeineAutomatenBestellungen())
				{
					return true;
				}
			}
		}
		return false;
	}

	
	private void UpdateWork()
	{
		if (this.mS_.GetDeltaTime() <= 0f)
		{
			return;
		}
		this.iDoWork = false;
		if (!this.roomS_)
		{
			this.workProgress = 0f;
			return;
		}
		if (this.roomS_.taskID == -1)
		{
			this.workProgress = 0f;
			return;
		}
		if (!this.roomS_.taskGameObject)
		{
			this.workProgress = 0f;
			return;
		}
		if (this.objectUsingID == -1)
		{
			return;
		}
		if (!this.objectUsingS_)
		{
			return;
		}
		if (!this.objectUsingS_.isArbeitsplatz)
		{
			return;
		}
		if (this.moveS_.waitForceAnimation > 0f)
		{
			return;
		}
		if (this.roomS_.pause)
		{
			return;
		}
		if (this.IsNoWork_Unterstuetzen())
		{
			return;
		}
		if (this.roomS_.WaitForMinimumHype())
		{
			return;
		}
		if (this.TrainingState(this.roomS_) == 2)
		{
			return;
		}
		if (this.roomS_.KeineAnrufe())
		{
			return;
		}
		if (this.roomS_.QA_GameHasNoBugs())
		{
			return;
		}
		if (this.roomS_.KeineAutomatenBestellungen())
		{
			return;
		}
		if (this.roomS_.IsKonsoleDevCompleteOrg())
		{
			return;
		}
		if (this.roomS_.IstContractWorkWait())
		{
			return;
		}
		if (this.roomS_.IstTaskWait())
		{
			return;
		}
		this.iDoWork = true;
		float num = 1f;
		if (this.roomS_.GameIsPort())
		{
			num = 2f;
		}
		if (this.roomS_.GameIsMMO())
		{
			num = 0.5f;
		}
		this.workProgress += this.mS_.GetDeltaTime() * this.GetWorkSpeed() * num;
		if (this.workProgress >= 1f)
		{
			this.workProgress = 0f;
			this.uiWorkProgress_Image.fillAmount = 0f;
			if (this.roomS_ && this.mS_.personal_crunch < 85 && this.roomS_.IsCrunchtimeRead() && UnityEngine.Random.Range(0, 500 + this.mS_.personal_crunch * 10) == 1)
			{
				this.UpdateKuendigen(true);
				return;
			}
			if (this.roomS_.typ == 1)
			{
				if (this.WORK_Engine(this.roomS_))
				{
					return;
				}
				if (this.WORK_Game(this.roomS_))
				{
					return;
				}
				if (this.WORK_Update(this.roomS_))
				{
					return;
				}
				if (this.WORK_F2PUpdate(this.roomS_))
				{
					return;
				}
			}
			if (this.roomS_.typ == 6)
			{
				if (this.WORK_Marketing(this.roomS_))
				{
					return;
				}
				if (this.WORK_MarketingSpezial(this.roomS_))
				{
					return;
				}
				if (this.WORK_Marktforschung(this.roomS_))
				{
					return;
				}
				if (this.WORK_Mitarbeitersuche(this.roomS_))
				{
					return;
				}
			}
			if (this.roomS_.typ == 7)
			{
				if (this.WORK_Fankampagne(this.roomS_))
				{
					return;
				}
				if (this.WORK_Support(this.roomS_))
				{
					return;
				}
				if (this.WORK_Fanshop(this.roomS_))
				{
					return;
				}
			}
			if (this.roomS_.typ == 3)
			{
				if (this.WORK_Bugfixing(this.roomS_))
				{
					return;
				}
				if (this.WORK_GameplayVerbessern(this.roomS_))
				{
					return;
				}
				if (this.WORK_Spielbericht(this.roomS_))
				{
					return;
				}
			}
			if (this.roomS_.typ == 2 && this.WORK_Forschung(this.roomS_))
			{
				return;
			}
			if (this.roomS_.typ == 13 && this.WORK_Training(this.roomS_))
			{
				return;
			}
			if (this.roomS_.typ == 4 && this.WORK_GrafikVerbessern(this.roomS_))
			{
				return;
			}
			if (this.roomS_.typ == 5 && this.WORK_SoundVerbessern(this.roomS_))
			{
				return;
			}
			if (this.roomS_.typ == 10 && this.WORK_AnimationVerbessern(this.roomS_))
			{
				return;
			}
			if (this.roomS_.typ == 17 && this.WORK_ArcadeProduction(this.roomS_))
			{
				return;
			}
			if (this.roomS_.typ == 8 && this.WORK_Hardware(this.roomS_))
			{
				return;
			}
			if (this.WORK_Polishing(this.roomS_))
			{
				return;
			}
			if (this.WORK_ContractWork(this.roomS_))
			{
				return;
			}
			this.WORK_Untersteutzen();
		}
	}

	
	private float GetWorkSpeed()
	{
		float num = 0.01f * ((this.s_motivation + 10f) * 0.5f);
		int personal_druck = this.mS_.personal_druck;
		if (personal_druck != 1)
		{
			if (personal_druck == 2)
			{
				num *= 1.5f;
			}
		}
		else
		{
			num *= 1.25f;
		}
		if (this.perks[29])
		{
			num *= 1.1f;
		}
		float num2 = (float)this.mS_.GetAchivementBonus(9);
		num2 *= 0.01f;
		num += num * num2;
		if (this.krank > 0)
		{
			num *= 0.25f;
		}
		if (this.mS_.settings_arbeitsgeschwindigkeitAnpassen)
		{
			return num * (this.mS_.speedSetting * 20f);
		}
		return num;
	}

	
	private float GetWorkResult(float f)
	{
		int personal_druck = this.mS_.personal_druck;
		if (personal_druck != 1)
		{
			if (personal_druck == 2)
			{
				f *= 0.6f;
			}
		}
		else
		{
			f *= 0.8f;
		}
		return this.mS_.Round(f, 1);
	}

	
	private void WORK_Untersteutzen()
	{
		taskUnterstuetzen taskUnterstuetzen = this.roomS_.GetTaskUnterstuetzen();
		if (!taskUnterstuetzen)
		{
			return;
		}
		if (!taskUnterstuetzen.rS_)
		{
			return;
		}
		if (!taskUnterstuetzen.rS_.taskGameObject)
		{
			return;
		}
		if (this.WORK_Forschung(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Engine(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Game(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Marketing(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_MarketingSpezial(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Fankampagne(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Mitarbeitersuche(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Support(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Fanshop(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_ContractWork(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Update(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_F2PUpdate(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Bugfixing(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_GameplayVerbessern(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_GrafikVerbessern(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_SoundVerbessern(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_AnimationVerbessern(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Spielbericht(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Marktforschung(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_Polishing(taskUnterstuetzen.rS_))
		{
			return;
		}
		if (this.WORK_ArcadeProduction(taskUnterstuetzen.rS_))
		{
			return;
		}
		this.WORK_Hardware(taskUnterstuetzen.rS_);
	}

	
	public int TrainingState(roomScript rS_)
	{
		if (!rS_)
		{
			return 0;
		}
		if (rS_.typ != 13)
		{
			return 0;
		}
		if (!rS_.taskGameObject)
		{
			return 0;
		}
		if (!this.guiMain_)
		{
			return 0;
		}
		taskTraining taskTraining = rS_.GetTaskTraining();
		if (!taskTraining)
		{
			return 0;
		}
		if (!this.menuTrain_)
		{
			this.menuTrain_ = this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		}
		int result = 2;
		switch (taskTraining.slot)
		{
		case 0:
			if (this.s_gamedesign < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(0) > this.s_gamedesign)
			{
				result = 1;
			}
			break;
		case 1:
			if (this.s_gamedesign < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(0) > this.s_gamedesign)
			{
				result = 1;
			}
			break;
		case 2:
			if (this.s_gamedesign < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(0) > this.s_gamedesign)
			{
				result = 1;
			}
			break;
		case 3:
			if (this.s_programmieren < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(1) > this.s_programmieren)
			{
				result = 1;
			}
			break;
		case 4:
			if (this.s_programmieren < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(1) > this.s_programmieren)
			{
				result = 1;
			}
			break;
		case 5:
			if (this.s_programmieren < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(1) > this.s_programmieren)
			{
				result = 1;
			}
			break;
		case 6:
			if (this.s_grafik < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(2) > this.s_grafik)
			{
				result = 1;
			}
			break;
		case 7:
			if (this.s_grafik < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(2) > this.s_grafik)
			{
				result = 1;
			}
			break;
		case 8:
			if (this.s_grafik < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(2) > this.s_grafik)
			{
				result = 1;
			}
			break;
		case 9:
			if (this.s_sound < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(3) > this.s_sound)
			{
				result = 1;
			}
			break;
		case 10:
			if (this.s_sound < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(3) > this.s_sound)
			{
				result = 1;
			}
			break;
		case 11:
			if (this.s_sound < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(3) > this.s_sound)
			{
				result = 1;
			}
			break;
		case 12:
			if (this.s_pr < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(4) > this.s_pr)
			{
				result = 1;
			}
			break;
		case 13:
			if (this.s_pr < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(4) > this.s_pr)
			{
				result = 1;
			}
			break;
		case 14:
			if (this.s_pr < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(4) > this.s_pr)
			{
				result = 1;
			}
			break;
		case 15:
			if (this.s_gametests < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(5) > this.s_gametests)
			{
				result = 1;
			}
			break;
		case 16:
			if (this.s_gametests < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(5) > this.s_gametests)
			{
				result = 1;
			}
			break;
		case 17:
			if (this.s_gametests < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(5) > this.s_gametests)
			{
				result = 1;
			}
			break;
		case 18:
			if (this.s_technik < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(6) > this.s_technik)
			{
				result = 1;
			}
			break;
		case 19:
			if (this.s_technik < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(6) > this.s_technik)
			{
				result = 1;
			}
			break;
		case 20:
			if (this.s_technik < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(6) > this.s_technik)
			{
				result = 1;
			}
			break;
		case 21:
			if (this.s_forschen < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(7) > this.s_forschen)
			{
				result = 1;
			}
			break;
		case 22:
			if (this.s_forschen < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(7) > this.s_forschen)
			{
				result = 1;
			}
			break;
		case 23:
			if (this.s_forschen < this.menuTrain_.trainingMaxLearn[taskTraining.slot] && this.GetSkillCap_Skill(7) > this.s_forschen)
			{
				result = 1;
			}
			break;
		}
		return result;
	}

	
	private bool WORK_Training(roomScript rS_)
	{
		if (!this.guiMain_)
		{
			return false;
		}
		if (!rS_)
		{
			return false;
		}
		taskTraining taskTraining = rS_.GetTaskTraining();
		if (!taskTraining)
		{
			return false;
		}
		if (!this.menuTrain_)
		{
			this.menuTrain_ = this.guiMain_.uiObjects[92].GetComponent<Menu_Training_Select>();
		}
		int num = this.objectUsingS_.qualitaet - 1;
		bool flag = false;
		int num2 = 10 + num * 2 + this.menuTrain_.trainingEffekt[taskTraining.slot] * 10;
		for (int i = 0; i < num2; i++)
		{
			switch (taskTraining.slot)
			{
			case 0:
				if (this.s_gamedesign < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(true, false, false, false, false, false, false, false);
					flag = true;
				}
				break;
			case 1:
				if (this.s_gamedesign < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(true, false, false, false, false, false, false, false);
					flag = true;
				}
				break;
			case 2:
				if (this.s_gamedesign < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(true, false, false, false, false, false, false, false);
					flag = true;
				}
				break;
			case 3:
				if (this.s_programmieren < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, true, false, false, false, false, false, false);
					flag = true;
				}
				break;
			case 4:
				if (this.s_programmieren < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, true, false, false, false, false, false, false);
					flag = true;
				}
				break;
			case 5:
				if (this.s_programmieren < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, true, false, false, false, false, false, false);
					flag = true;
				}
				break;
			case 6:
				if (this.s_grafik < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, true, false, false, false, false, false);
					flag = true;
				}
				break;
			case 7:
				if (this.s_grafik < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, true, false, false, false, false, false);
					flag = true;
				}
				break;
			case 8:
				if (this.s_grafik < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, true, false, false, false, false, false);
					flag = true;
				}
				break;
			case 9:
				if (this.s_sound < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, true, false, false, false, false);
					flag = true;
				}
				break;
			case 10:
				if (this.s_sound < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, true, false, false, false, false);
					flag = true;
				}
				break;
			case 11:
				if (this.s_sound < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, true, false, false, false, false);
					flag = true;
				}
				break;
			case 12:
				if (this.s_pr < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, true, false, false, false);
					flag = true;
				}
				break;
			case 13:
				if (this.s_pr < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, true, false, false, false);
					flag = true;
				}
				break;
			case 14:
				if (this.s_pr < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, true, false, false, false);
					flag = true;
				}
				break;
			case 15:
				if (this.s_gametests < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, true, false, false);
					flag = true;
				}
				break;
			case 16:
				if (this.s_gametests < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, true, false, false);
					flag = true;
				}
				break;
			case 17:
				if (this.s_gametests < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, true, false, false);
					flag = true;
				}
				break;
			case 18:
				if (this.s_technik < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, false, true, false);
					flag = true;
				}
				break;
			case 19:
				if (this.s_technik < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, false, true, false);
					flag = true;
				}
				break;
			case 20:
				if (this.s_technik < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, false, true, false);
					flag = true;
				}
				break;
			case 21:
				if (this.s_forschen < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, false, false, true);
					flag = true;
				}
				break;
			case 22:
				if (this.s_forschen < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, false, false, true);
					flag = true;
				}
				break;
			case 23:
				if (this.s_forschen < this.menuTrain_.trainingMaxLearn[taskTraining.slot])
				{
					this.Learn(false, false, false, false, false, false, false, true);
					flag = true;
				}
				break;
			}
		}
		if (flag)
		{
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Training, 1f, 0f, 13));
			taskTraining.Work(1f);
		}
		return true;
	}

	
	private bool WORK_ContractWork(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskContractWork taskContractWork = rS_.GetTaskContractWork();
		if (!taskContractWork)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		if (rS_.typ == 1)
		{
			float num3 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num2;
			num3 = this.GetWorkResult(num3);
			if (this.perks[28])
			{
				num3 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num3, num, 13));
			num += 0.4f;
			taskContractWork.Work(num3);
			this.Learn(false, true, false, false, false, false, false, false);
			if (critic)
			{
				num3 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f);
				num3 = this.GetWorkResult(num3);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num3, num, 13));
				num += 0.4f;
				taskContractWork.Work(num3);
			}
			return true;
		}
		if (rS_.typ == 3)
		{
			float num4 = UnityEngine.Random.Range(0.1f, this.s_gamedesign * 0.1f) * num2;
			num4 = this.GetWorkResult(num4);
			if (this.perks[28])
			{
				num4 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Gameplay, num4, num, 13));
			num += 0.4f;
			taskContractWork.Work(num4);
			this.Learn(true, false, false, false, false, false, false, false);
			if (critic)
			{
				num4 = UnityEngine.Random.Range(0.1f, this.s_gamedesign * 0.1f);
				num4 = this.GetWorkResult(num4);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Gameplay, num4, num, 13));
				num += 0.4f;
				taskContractWork.Work(num4);
			}
			return true;
		}
		if (rS_.typ == 4)
		{
			float num5 = UnityEngine.Random.Range(0.1f, this.s_grafik * 0.1f) * num2;
			num5 = this.GetWorkResult(num5);
			if (this.perks[28])
			{
				num5 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Grafik, num5, num, 13));
			num += 0.4f;
			taskContractWork.Work(num5);
			this.Learn(false, false, true, false, false, false, false, false);
			if (critic)
			{
				num5 = UnityEngine.Random.Range(0.1f, this.s_grafik * 0.1f);
				num5 = this.GetWorkResult(num5);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Grafik, num5, num, 13));
				num += 0.4f;
				taskContractWork.Work(num5);
			}
			return true;
		}
		if (rS_.typ == 5)
		{
			float num6 = UnityEngine.Random.Range(0.1f, this.s_sound * 0.1f) * num2;
			num6 = this.GetWorkResult(num6);
			if (this.perks[28])
			{
				num6 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Sound, num6, num, 13));
			num += 0.4f;
			taskContractWork.Work(num6);
			this.Learn(false, false, false, true, false, false, false, false);
			if (critic)
			{
				num6 = UnityEngine.Random.Range(0.1f, this.s_sound * 0.1f);
				num6 = this.GetWorkResult(num6);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Sound, num6, num, 13));
				num += 0.4f;
				taskContractWork.Work(num6);
			}
			return true;
		}
		if (rS_.typ == 10)
		{
			float num7 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num2;
			num7 = this.GetWorkResult(num7);
			if (this.perks[28])
			{
				num7 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num7, num, 13));
			num += 0.4f;
			taskContractWork.Work(num7);
			this.Learn(false, true, false, false, false, false, false, false);
			if (critic)
			{
				num7 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f);
				num7 = this.GetWorkResult(num7);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num7, num, 13));
				num += 0.4f;
				taskContractWork.Work(num7);
			}
			return true;
		}
		if (rS_.typ == 17)
		{
			float num8 = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f) * num2;
			num8 = this.GetWorkResult(num8);
			if (this.perks[28])
			{
				num8 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_ProdArcade, num8, num, 13));
			num += 0.4f;
			taskContractWork.Work(num8);
			this.Learn(false, false, false, false, false, false, true, false);
			if (critic)
			{
				num8 = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f);
				num8 = this.GetWorkResult(num8);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_ProdArcade, num8, num, 13));
				num += 0.4f;
				taskContractWork.Work(num8);
			}
			return true;
		}
		if (rS_.typ == 8)
		{
			float num9 = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f) * num2;
			num9 = this.GetWorkResult(num9);
			if (this.perks[28])
			{
				num9 *= 2f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Hardware, num9, num, 13));
			num += 0.4f;
			taskContractWork.Work(num9);
			this.Learn(false, false, false, false, false, false, true, false);
			if (critic)
			{
				num9 = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f);
				num9 = this.GetWorkResult(num9);
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Hardware, num9, num, 13));
				num += 0.4f;
				taskContractWork.Work(num9);
			}
			return true;
		}
		return true;
	}

	
	private bool WORK_Fankampagne(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskFankampagne taskFankampagne = rS_.GetTaskFankampagne();
		if (!taskFankampagne)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Support, f, num, 13));
		num += 0.4f;
		taskFankampagne.Work(f);
		this.Learn(false, false, false, false, true, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Support, f, num, 13));
			num += 0.4f;
			taskFankampagne.Work(f);
		}
		return true;
	}

	
	private bool WORK_Mitarbeitersuche(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskMitarbeitersuche taskMitarbeitersuche = rS_.GetTaskMitarbeitersuche();
		if (!taskMitarbeitersuche)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Marketing, f, num, 13));
		num += 0.4f;
		taskMitarbeitersuche.Work(f);
		this.Learn(false, false, false, false, true, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Marketing, f, num, 13));
			num += 0.4f;
			taskMitarbeitersuche.Work(f);
		}
		return true;
	}

	
	private bool WORK_Marktforschung(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskMarktforschung taskMarktforschung = rS_.GetTaskMarktforschung();
		if (!taskMarktforschung)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Support, f, num, 13));
		num += 0.4f;
		taskMarktforschung.Work(f);
		this.Learn(false, false, false, false, true, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Support, f, num, 13));
			num += 0.4f;
			taskMarktforschung.Work(f);
		}
		return true;
	}

	
	private bool WORK_Support(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskSupport taskSupport = rS_.GetTaskSupport();
		if (!taskSupport)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Support, f, num, 13));
		num += 0.4f;
		taskSupport.Work(f);
		this.Learn(false, false, false, false, true, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Support, f, num, 13));
			num += 0.4f;
			taskSupport.Work(f);
		}
		return true;
	}

	
	private bool WORK_Fanshop(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskFanshop taskFanshop = rS_.GetTaskFanshop();
		if (!taskFanshop)
		{
			return false;
		}
		float num = (float)(this.objectUsingS_.qualitaet - 1);
		num = num / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num;
		f = this.GetWorkResult(f);
		bool flag = true;
		if (taskFanshop.menuFanshop_)
		{
			for (int i = UnityEngine.Random.Range(0, this.mS_.games_.arrayMyIpScripts.Count); i < this.mS_.games_.arrayMyIpScripts.Count; i++)
			{
				gameScript gameScript = this.mS_.games_.arrayMyIpScripts[i];
				if (gameScript && !gameScript.merchKeinVerkauf)
				{
					for (int j = UnityEngine.Random.Range(0, gameScript.merchBestellungen.Length); j < gameScript.merchBestellungen.Length; j++)
					{
						if (gameScript.merchBestellungen[j] > 0)
						{
							flag = false;
							if (gameScript.merchVerkaufspreis[j] <= 0f)
							{
								gameScript.merchVerkaufspreis[j] = taskFanshop.menuFanshop_.GetMindestVerkaufspreis(j);
							}
							int num2 = 1 + Mathf.RoundToInt(f) * 10;
							if (gameScript.merchBestellungen[j] < num2)
							{
								num2 = gameScript.merchBestellungen[j];
							}
							gameScript.merchBestellungen[j] -= num2;
							gameScript.merchDiesenMonat[j] += num2;
							gameScript.merchGesamtSells[j] += num2;
							float f2 = (float)num2 * (gameScript.merchVerkaufspreis[j] - taskFanshop.menuFanshop_.einkaufspreis[j]);
							int num3 = Mathf.RoundToInt(f2);
							gameScript.merchGesamtGewinn += (long)Mathf.RoundToInt(f2);
							this.mS_.Earn((long)num3, 11);
							this.mS_.AddFanshopverlauf((long)num3);
							base.StartCoroutine(this.CreatePopInSeconds_SPRITE(this.guiMain_.uiSprites[50 + j], this.mS_.GetMoney((long)num3, true), 0f, 13));
							taskFanshop.Work(j, num2, num3);
							this.Learn(false, false, false, false, true, false, false, false);
							break;
						}
					}
				}
			}
		}
		if (flag)
		{
			base.StartCoroutine(this.CreatePopInSeconds_SPRITE(this.guiMain_.uiSprites[58], "", 0f, 13));
			this.Learn(false, false, false, false, true, false, false, false);
		}
		return true;
	}

	
	private bool WORK_Marketing(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskMarketing taskMarketing = rS_.GetTaskMarketing();
		if (!taskMarketing)
		{
			return false;
		}
		if (taskMarketing.WaitForMinimumHype())
		{
			return true;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Marketing, f, num, 13));
		num += 0.4f;
		taskMarketing.Work(f);
		this.Learn(false, false, false, false, true, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Marketing, f, num, 13));
			num += 0.4f;
			taskMarketing.Work(f);
		}
		return true;
	}

	
	private bool WORK_MarketingSpezial(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskMarketingSpezial taskMarketingSpezial = rS_.GetTaskMarketingSpezial();
		if (!taskMarketingSpezial)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Marketing, f, num, 13));
		num += 0.4f;
		taskMarketingSpezial.Work(f);
		this.Learn(false, false, false, false, true, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_pr * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Marketing, f, num, 13));
			num += 0.4f;
			taskMarketingSpezial.Work(f);
		}
		return true;
	}

	
	private bool WORK_Hardware(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskKonsole taskKonsole = rS_.GetTaskKonsole();
		if (!taskKonsole)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float num3 = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f) * num2;
		if (taskKonsole.techniker_)
		{
			if (taskKonsole.techniker_.perks[14])
			{
				num3 += taskKonsole.techniker_.s_technik * 0.02f;
			}
			else
			{
				num3 += taskKonsole.techniker_.s_technik * 0.01f;
			}
		}
		num3 = this.GetWorkResult(num3);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Hardware, num3, num, 13));
		num += 0.4f;
		taskKonsole.Work(num3);
		this.Learn(false, false, false, false, false, false, true, false);
		if (critic)
		{
			num3 = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f);
			num3 = this.GetWorkResult(num3);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Hardware, num3, num, 13));
			num += 0.4f;
			taskKonsole.Work(num3);
		}
		return true;
	}

	
	private bool WORK_ArcadeProduction(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskArcadeProduction taskArcadeProduction = rS_.GetTaskArcadeProduction();
		if (!taskArcadeProduction)
		{
			return false;
		}
		float num = 0f;
		if (this.uiVisible)
		{
			this.sfx_.PlaySound(59, false);
		}
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(this.s_technik * 0.05f, this.s_technik * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_ProdArcade, f, num, 13));
		num += 0.4f;
		taskArcadeProduction.Work(f);
		this.Learn(false, false, false, false, false, false, true, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_technik * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_ProdArcade, f, num, 13));
			num += 0.4f;
			taskArcadeProduction.Work(f);
		}
		return true;
	}

	
	private bool WORK_Update(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskUpdate taskUpdate = rS_.GetTaskUpdate();
		if (!taskUpdate)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, f, num, 13));
		num += 0.4f;
		taskUpdate.Work(f);
		this.Learn(true, true, true, true, false, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, f, num, 13));
			num += 0.4f;
			taskUpdate.Work(f);
		}
		return true;
	}

	
	private bool WORK_F2PUpdate(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskF2PUpdate taskF2PUpdate = rS_.GetTaskF2PUpdate();
		if (!taskF2PUpdate)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, f, num, 13));
		num += 0.4f;
		taskF2PUpdate.Work(f);
		this.Learn(true, true, true, true, false, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, f, num, 13));
			num += 0.4f;
			taskF2PUpdate.Work(f);
		}
		return true;
	}

	
	private bool WORK_Bugfixing(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskBugfixing taskBugfixing = rS_.GetTaskBugfixing();
		if (!taskBugfixing)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_QA, f, num, 13));
		num += 0.4f;
		taskBugfixing.Work(f);
		this.Learn(false, false, false, false, false, true, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_QA, f, num, 13));
			num += 0.4f;
			taskBugfixing.Work(f);
		}
		return true;
	}

	
	private bool WORK_Polishing(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskPolishing taskPolishing = rS_.GetTaskPolishing();
		if (!taskPolishing)
		{
			return false;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = (float)(this.objectUsingS_.qualitaet - 1);
		num3 = num3 / 10f + 1f;
		if (rS_.typ == 4)
		{
			num2 = UnityEngine.Random.Range(0.1f, this.s_grafik * 0.1f) * num3;
			num2 = this.GetWorkResult(num2);
			num2 *= 0.1f;
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Grafik, num2, num, 13));
			this.Learn(false, false, true, false, false, false, false, false);
		}
		if (rS_.typ == 5)
		{
			num2 = UnityEngine.Random.Range(0.1f, this.s_sound * 0.1f) * num3;
			num2 = this.GetWorkResult(num2);
			num2 *= 0.1f;
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Sound, num2, num, 13));
			this.Learn(false, false, false, true, false, false, false, false);
		}
		if (rS_.typ == 10)
		{
			num2 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num3;
			num2 = this.GetWorkResult(num2);
			num2 *= 0.1f;
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num2, num, 13));
			this.Learn(false, true, false, false, false, false, false, false);
		}
		if (rS_.typ == 3)
		{
			num2 = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f) * num3;
			num2 = this.GetWorkResult(num2);
			num2 *= 0.1f;
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Gameplay, num2, num, 13));
			this.Learn(false, false, false, false, false, true, false, false);
		}
		num += 0.4f;
		taskPolishing.Work(num2, this.roomS_);
		return true;
	}

	
	private bool WORK_Spielbericht(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskSpielbericht taskSpielbericht = rS_.GetTaskSpielbericht();
		if (!taskSpielbericht)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_QA, f, num, 13));
		num += 0.4f;
		taskSpielbericht.Work(f);
		this.Learn(false, false, false, false, false, true, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_QA, f, num, 13));
			num += 0.4f;
			taskSpielbericht.Work(f);
		}
		return true;
	}

	
	private bool WORK_GameplayVerbessern(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskGameplayVerbessern taskGameplayVerbessern = rS_.GetTaskGameplayVerbessern();
		if (!taskGameplayVerbessern)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float num3 = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f) * num2;
		num3 = this.GetWorkResult(num3);
		if (this.perks[25] && taskGameplayVerbessern.gS_.typ_nachfolger)
		{
			num3 *= 2f;
		}
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Gameplay, num3, num, 13));
		num += 0.4f;
		taskGameplayVerbessern.Work(num3);
		this.Learn(false, false, false, false, false, true, false, false);
		if (critic)
		{
			num3 = UnityEngine.Random.Range(0.1f, this.s_gametests * 0.1f);
			num3 = this.GetWorkResult(num3);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Gameplay, num3, num, 13));
			num += 0.4f;
			taskGameplayVerbessern.Work(num3);
		}
		return true;
	}

	
	private bool WORK_GrafikVerbessern(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskGrafikVerbessern taskGrafikVerbessern = rS_.GetTaskGrafikVerbessern();
		if (!taskGrafikVerbessern)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float num3 = UnityEngine.Random.Range(0.1f, this.s_grafik * 0.1f) * num2;
		num3 = this.GetWorkResult(num3);
		if (this.perks[23] && taskGrafikVerbessern.gS_.retro)
		{
			num3 *= 2f;
		}
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Grafik, num3, num, 13));
		num += 0.4f;
		taskGrafikVerbessern.Work(num3);
		this.Learn(false, false, true, false, false, false, false, false);
		if (critic)
		{
			num3 = UnityEngine.Random.Range(0.1f, this.s_grafik * 0.1f);
			num3 = this.GetWorkResult(num3);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Grafik, num3, num, 13));
			num += 0.4f;
			taskGrafikVerbessern.Work(num3);
		}
		return true;
	}

	
	private bool WORK_SoundVerbessern(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskSoundVerbessern taskSoundVerbessern = rS_.GetTaskSoundVerbessern();
		if (!taskSoundVerbessern)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_sound * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Sound, f, num, 13));
		num += 0.4f;
		taskSoundVerbessern.Work(f);
		this.Learn(false, false, false, true, false, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_sound * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Sound, f, num, 13));
			num += 0.4f;
			taskSoundVerbessern.Work(f);
		}
		return true;
	}

	
	private bool WORK_AnimationVerbessern(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskAnimationVerbessern taskAnimationVerbessern = rS_.GetTaskAnimationVerbessern();
		if (!taskAnimationVerbessern)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, f, num, 13));
		num += 0.4f;
		taskAnimationVerbessern.Work(f);
		this.Learn(false, true, false, false, false, false, false, false);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, f, num, 13));
			num += 0.4f;
			taskAnimationVerbessern.Work(f);
		}
		return true;
	}

	
	private bool WORK_Forschung(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskForschung taskForschung = rS_.GetTaskForschung();
		if (!taskForschung)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float f = UnityEngine.Random.Range(0.1f, this.s_forschen * 0.1f) * num2;
		f = this.GetWorkResult(f);
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Forschung, f, num, 13));
		num += 0.4f;
		taskForschung.Work(f);
		this.Learn(false, false, false, false, false, false, false, true);
		if (critic)
		{
			f = UnityEngine.Random.Range(0.1f, this.s_forschen * 0.1f);
			f = this.GetWorkResult(f);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Forschung, f, num, 13));
			num += 0.4f;
			taskForschung.Work(f);
		}
		return true;
	}

	
	private bool WORK_Engine(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskEngine taskEngine = rS_.GetTaskEngine();
		if (!taskEngine)
		{
			return false;
		}
		float num = 0f;
		bool critic = this.GetCritic(0);
		float num2 = (float)(this.objectUsingS_.qualitaet - 1);
		num2 = num2 / 10f + 1f;
		float num3 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num2;
		num3 = this.GetWorkResult(num3);
		if (this.perks[26])
		{
			num3 *= 2f;
		}
		base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num3, num, 13));
		num += 0.4f;
		taskEngine.Work(num3);
		this.Learn(false, true, false, false, false, false, false, false);
		if (critic)
		{
			num3 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f);
			num3 = this.GetWorkResult(num3);
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num3, num, 13));
			num += 0.4f;
			taskEngine.Work(num3);
		}
		return true;
	}

	
	private bool WORK_Game(roomScript rS_)
	{
		if (!rS_)
		{
			return false;
		}
		taskGame taskGame = rS_.GetTaskGame();
		if (!taskGame)
		{
			return false;
		}
		if (!taskGame.gS_)
		{
			return true;
		}
		float devPoints_Gesamt = taskGame.gS_.devPoints_Gesamt;
		float num = 0f;
		int num2 = 0;
		engineScript engineScript = taskGame.gS_.GetEngineScript();
		if (engineScript)
		{
			if (taskGame.gS_.maingenre == engineScript.spezialgenre)
			{
				num2++;
			}
			if (taskGame.gS_.gamePlatform[0] == engineScript.spezialplatform)
			{
				num2++;
			}
			if (taskGame.gS_.gamePlatform[1] == engineScript.spezialplatform)
			{
				num2++;
			}
			if (taskGame.gS_.gamePlatform[2] == engineScript.spezialplatform)
			{
				num2++;
			}
			if (taskGame.gS_.gamePlatform[3] == engineScript.spezialplatform)
			{
				num2++;
			}
		}
		bool critic = this.GetCritic(num2);
		int num3 = UnityEngine.Random.Range(0, 4);
		if (UnityEngine.Random.Range(0, taskGame.gS_.gameAP_Gameplay * 5) > UnityEngine.Random.Range(0, 100))
		{
			num3 = 0;
		}
		if (UnityEngine.Random.Range(0, taskGame.gS_.gameAP_Grafik * 5) > UnityEngine.Random.Range(0, 100))
		{
			num3 = 1;
		}
		if (UnityEngine.Random.Range(0, taskGame.gS_.gameAP_Sound * 5) > UnityEngine.Random.Range(0, 100))
		{
			num3 = 2;
		}
		if (UnityEngine.Random.Range(0, taskGame.gS_.gameAP_Technik * 5) > UnityEngine.Random.Range(0, 100))
		{
			num3 = 3;
		}
		int num4 = -1;
		if (critic)
		{
			num4 = UnityEngine.Random.Range(0, 4);
			if (num3 == num4)
			{
				if (num4 > 0)
				{
					num4--;
				}
				else
				{
					num4++;
				}
			}
		}
		float num5 = (float)(this.objectUsingS_.qualitaet - 1);
		num5 = num5 / 10f + 1f;
		if (num3 == 0 || num4 == 0)
		{
			float num6 = UnityEngine.Random.Range(0.1f, this.s_gamedesign * 0.1f) * num5;
			if (taskGame.designer_)
			{
				if (taskGame.designer_.perks[14])
				{
					num6 += taskGame.designer_.s_gamedesign * 0.02f;
				}
				else
				{
					num6 += taskGame.designer_.s_gamedesign * 0.01f;
				}
			}
			num6 = this.GetWorkResult(num6);
			if (this.perks[25] && taskGame.gS_.typ_nachfolger)
			{
				num6 *= 2f;
			}
			if (taskGame.gS_.devPoints <= 0f)
			{
				num6 *= 0.1f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Gameplay, num6, num, 13));
			num += 0.4f;
			taskGame.Work(num6, 0);
			this.Learn(true, false, false, false, false, false, false, false);
		}
		if (num3 == 1 || num4 == 1)
		{
			float num7 = UnityEngine.Random.Range(0.1f, this.s_grafik * 0.1f) * num5;
			num7 = this.GetWorkResult(num7);
			if (this.perks[23] && taskGame.gS_.retro)
			{
				num7 *= 2f;
			}
			if (taskGame.gS_.devPoints <= 0f)
			{
				num7 *= 0.1f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Grafik, num7, num, 13));
			num += 0.4f;
			taskGame.Work(num7, 1);
			this.Learn(false, false, true, false, false, false, false, false);
		}
		if (num3 == 2 || num4 == 2)
		{
			float num8 = UnityEngine.Random.Range(0.1f, this.s_sound * 0.1f) * num5;
			num8 = this.GetWorkResult(num8);
			if (taskGame.gS_.devPoints <= 0f)
			{
				num8 *= 0.1f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Sound, num8, num, 13));
			num += 0.4f;
			taskGame.Work(num8, 2);
			this.Learn(false, false, false, true, false, false, false, false);
		}
		if (num3 == 3 || num4 == 3)
		{
			float num9 = UnityEngine.Random.Range(0.1f, this.s_programmieren * 0.1f) * num5;
			num9 = this.GetWorkResult(num9);
			if (this.perks[24] && taskGame.gS_.portID != -1)
			{
				num9 *= 2f;
			}
			if (taskGame.gS_.devPoints <= 0f)
			{
				num9 *= 0.1f;
			}
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Technik, num9, num, 13));
			num += 0.4f;
			taskGame.Work(num9, 3);
			this.Learn(false, true, false, false, false, false, false, false);
		}
		if (this.perks[1] && UnityEngine.Random.Range(0, 30) == 1)
		{
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Hype, 1f, num, 13));
			taskGame.Work(1f, 5);
		}
		if (!this.perks[3] && taskGame.gS_.devPoints_Gesamt > 0f)
		{
			if (UnityEngine.Random.Range(0f, 10f + this.s_programmieren * 0.1f) < 3f)
			{
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Bug, 1f, num, 24));
				num += 0.4f;
				taskGame.Work(1f, 4);
			}
			if (this.perks[21] && UnityEngine.Random.Range(0f, 10f + this.s_programmieren * 0.1f) < 3f)
			{
				base.StartCoroutine(this.CreatePopInSeconds(this.ePop_Bug, 1f, num, 24));
				num += 0.4f;
				taskGame.Work(1f, 4);
			}
		}
		if (taskGame.gS_.devPoints_Gesamt <= 0f && taskGame.gS_.points_bugs > 0f && UnityEngine.Random.Range(0f, 100f) > 90f)
		{
			base.StartCoroutine(this.CreatePopInSeconds(this.ePop_BugRemove, 1f, num, 13));
			num += 0.4f;
			taskGame.Work(1f, 6);
		}
		return true;
	}

	
	private float GetSkillCap()
	{
		if (!this.perks[15])
		{
			return 200f;
		}
		return 255f;
	}

	
	private float GetSkillCap_Skill(int i)
	{
		switch (i)
		{
		case 0:
			if (this.beruf != 0)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 1:
			if (this.beruf != 1)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 2:
			if (this.beruf != 2)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 3:
			if (this.beruf != 3)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 4:
			if (this.beruf != 4)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 5:
			if (this.beruf != 5)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 6:
			if (this.beruf != 6)
			{
				return this.GetSkillCap();
			}
			return 255f;
		case 7:
			if (this.beruf != 7)
			{
				return this.GetSkillCap();
			}
			return 255f;
		default:
			return this.GetSkillCap();
		}
	}

	
	private void Learn(bool gamedesign_, bool programmieren_, bool grafik_, bool sound_, bool pr_, bool gametests_, bool technik_, bool forschen_)
	{
		float num = UnityEngine.Random.Range(0.001f, 0.002f);
		if (this.perks[5])
		{
			num *= 2f;
		}
		if (this.perks[22])
		{
			num *= 0.5f;
		}
		float num2 = (float)this.mS_.GetAchivementBonus(8);
		num2 *= 0.01f;
		num += num * num2;
		if (gamedesign_)
		{
			this.s_gamedesign += num;
			if (this.beruf != 0 && this.s_gamedesign > this.GetSkillCap())
			{
				this.s_gamedesign = this.GetSkillCap();
			}
			if (this.s_gamedesign > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_gamedesign = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (programmieren_)
		{
			this.s_programmieren += num;
			if (this.beruf != 1 && this.s_programmieren > this.GetSkillCap())
			{
				this.s_programmieren = this.GetSkillCap();
			}
			if (this.s_programmieren > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_programmieren = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (grafik_)
		{
			this.s_grafik += num;
			if (this.beruf != 2 && this.s_grafik > this.GetSkillCap())
			{
				this.s_grafik = this.GetSkillCap();
			}
			if (this.s_grafik > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_grafik = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (sound_)
		{
			this.s_sound += num;
			if (this.beruf != 3 && this.s_sound > this.GetSkillCap())
			{
				this.s_sound = this.GetSkillCap();
			}
			if (this.s_sound > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_sound = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (pr_)
		{
			this.s_pr += num;
			if (this.beruf != 4 && this.s_pr > this.GetSkillCap())
			{
				this.s_pr = this.GetSkillCap();
			}
			if (this.s_pr > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_pr = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (gametests_)
		{
			this.s_gametests += num;
			if (this.beruf != 5 && this.s_gametests > this.GetSkillCap())
			{
				this.s_gametests = this.GetSkillCap();
			}
			if (this.s_gametests > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_gametests = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (technik_)
		{
			this.s_technik += num;
			if (this.beruf != 6 && this.s_technik > this.GetSkillCap())
			{
				this.s_technik = this.GetSkillCap();
			}
			if (this.s_technik > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_technik = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
		if (forschen_)
		{
			this.s_forschen += num;
			if (this.beruf != 7 && this.s_forschen > this.GetSkillCap())
			{
				this.s_forschen = this.GetSkillCap();
			}
			if (this.s_forschen > this.GetSkillCap_Skill(this.beruf))
			{
				this.s_forschen = this.GetSkillCap_Skill(this.beruf);
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(59);
				}
			}
			return;
		}
	}

	
	private bool GetCritic(int criticBonus)
	{
		if (this.perks[27])
		{
			return false;
		}
		bool result = false;
		if (!this.perks[6])
		{
			if (UnityEngine.Random.Range(0, 20 - criticBonus) == 1)
			{
				result = true;
			}
		}
		else if (UnityEngine.Random.Range(0, 10 - criticBonus) == 1)
		{
			result = true;
		}
		return result;
	}

	
	public int GetGehalt()
	{
		if (this.perks[0])
		{
			return 0;
		}
		int num = Mathf.RoundToInt(0f + this.s_gamedesign + this.s_programmieren + this.s_grafik + this.s_sound + this.s_pr + this.s_gametests + this.s_technik + this.s_forschen) * 10;
		for (int i = 0; i < this.perks.Length; i++)
		{
			if (this.perks[i])
			{
				if (i != 0)
				{
					if (i != 1)
					{
						switch (i)
						{
						case 14:
							num += 1000;
							goto IL_11C;
						case 15:
							num += 2000;
							goto IL_11C;
						case 18:
							num -= 500;
							goto IL_11C;
						case 19:
							num -= 500;
							goto IL_11C;
						case 20:
							num -= 500;
							goto IL_11C;
						case 21:
							num -= 500;
							goto IL_11C;
						case 22:
							num -= 500;
							goto IL_11C;
						case 27:
							num -= 500;
							goto IL_11C;
						}
						num += 500;
					}
					else
					{
						num += 10000;
					}
				}
				else
				{
					num = num;
				}
			}
			IL_11C:;
		}
		if (num < 1000)
		{
			num = 1000;
		}
		if (this.perks[18])
		{
			num *= 2;
		}
		return num;
	}

	
	public void Entlassen(bool eventMitarbeiterMotivation)
	{
		if (this.perks[0])
		{
			return;
		}
		if (this.legend != -1)
		{
			this.mS_.devLegendsInUse[this.legend] = false;
		}
		if (this.myUI)
		{
			UnityEngine.Object.Destroy(this.myUI);
		}
		if (this.objectUsingID != -1)
		{
			GameObject gameObject = GameObject.Find("O_" + this.objectUsingID.ToString());
			if (gameObject)
			{
				gameObject.GetComponent<objectScript>().besetztCharID = -1;
			}
		}
		if (this.objectBelegtID != -1)
		{
			GameObject gameObject2 = GameObject.Find("O_" + this.objectBelegtID.ToString());
			if (gameObject2)
			{
				gameObject2.GetComponent<objectScript>().besetztCharID = -1;
			}
		}
		if (eventMitarbeiterMotivation && UnityEngine.Random.Range(0, 100) > 90)
		{
			this.guiMain_.EVENT_MitarbeiterMotivation();
		}
		this.mS_.findCharacters = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public string GetGroupString(string farbe)
	{
		if (this.group != -1)
		{
			return string.Concat(new string[]
			{
				"<color=",
				farbe,
				">[",
				this.group.ToString(),
				"]</color>"
			});
		}
		return "";
	}

	
	public string GetGroupStringWithName(string farbe)
	{
		if (this.group != -1)
		{
			return string.Concat(new string[]
			{
				"<color=",
				farbe,
				">",
				this.mS_.personal_group_names[this.group - 1],
				" (",
				this.group.ToString(),
				")</color>"
			});
		}
		return "";
	}

	
	public void Monatskosten()
	{
		int gehalt = this.GetGehalt();
		if (gehalt <= 0)
		{
			return;
		}
		this.mS_.Pay((long)gehalt, 9);
		base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(gehalt, base.transform.position, false));
		if (!this.roomS_)
		{
			return;
		}
		roomScript rS_ = this.roomS_;
		if (this.roomS_.taskGameObject && this.roomS_.GetTaskUnterstuetzen())
		{
			rS_ = this.roomS_.GetTaskUnterstuetzen().rS_;
		}
		if (rS_ && rS_.taskGameObject)
		{
			taskGame taskGame = rS_.GetTaskGame();
			if (taskGame)
			{
				if (taskGame.gS_)
				{
					taskGame.gS_.costs_mitarbeiter += (long)gehalt;
				}
				return;
			}
			taskKonsole taskKonsole = rS_.GetTaskKonsole();
			if (taskKonsole)
			{
				if (taskKonsole.pS_)
				{
					taskKonsole.pS_.costs_mitarbeiter += gehalt;
				}
				return;
			}
		}
	}

	
	private void UpdateLeitenderEntwicklerIcon()
	{
		if (this.roomS_)
		{
			if (this.roomS_.typ == 1 && this.roomS_.taskGameObject)
			{
				taskGame taskGame = this.roomS_.GetTaskGame();
				if (taskGame && taskGame.leitenderDesignerID == this.myID)
				{
					if (!this.uiLeitenderDesigner.activeSelf)
					{
						this.uiLeitenderDesigner.SetActive(true);
					}
					return;
				}
			}
			if (this.roomS_.typ == 8 && this.roomS_.taskGameObject)
			{
				taskKonsole taskKonsole = this.roomS_.GetTaskKonsole();
				if (taskKonsole && taskKonsole.leitenderTechnikerID == this.myID)
				{
					if (!this.uiLeitenderDesigner.activeSelf)
					{
						this.uiLeitenderDesigner.SetActive(true);
					}
					return;
				}
			}
		}
		if (this.uiLeitenderDesigner.activeSelf)
		{
			this.uiLeitenderDesigner.SetActive(false);
		}
	}

	
	private mainScript mS_;

	
	private Camera camera_;

	
	private mainCameraScript mCamS_;

	
	private GameObject main_;

	
	private settingsScript settings_;

	
	private GUI_Tooltip guiTooltip;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private clipScript clipS_;

	
	private roomDataScript rdS_;

	
	public movementScript moveS_;

	
	private mapScript mapS_;

	
	public SkinnedMeshRenderer myRenderer;

	
	public GameObject[] uiPrefabs;

	
	public Color[] colors;

	
	public int myID;

	
	public bool male;

	
	public string myName;

	
	public int group = -1;

	
	public int roomID = -1;

	
	public roomScript roomS_;

	
	public int objectUsingID = -1;

	
	public objectScript objectUsingS_;

	
	public int objectBelegtID = -1;

	
	public objectScript objectBelegtS_;

	
	public objectScript mainArbeitsplatzS_;

	
	public int beruf;

	
	public float s_motivation;

	
	public float s_gamedesign;

	
	public float s_programmieren;

	
	public float s_grafik;

	
	public float s_sound;

	
	public float s_pr;

	
	public float s_gametests;

	
	public float s_technik;

	
	public float s_forschen;

	
	public bool[] perks;

	
	public int legend = -1;

	
	public float workProgress;

	
	public float durst;

	
	public float hunger;

	
	public float klo;

	
	public float waschbecken;

	
	public float muell;

	
	public float giessen;

	
	public float pause;

	
	public float freezer;

	
	public int krank;

	
	private bool outline;

	
	private bool uiVisible;

	
	public bool picked;

	
	public bool hided;

	
	public int model_body = -1;

	
	public int model_eyes = -1;

	
	public int model_hair = -1;

	
	public int model_beard = -1;

	
	public int model_skinColor = -1;

	
	public int model_hairColor = -1;

	
	public int model_beardColor = -1;

	
	public int model_HoseColor = -1;

	
	public int model_ShirtColor = -1;

	
	public int model_Add1Color = -1;

	
	private GameObject myUI;

	
	private RectTransform myUIRect;

	
	private GameObject uiIconMain;

	
	private GameObject uiWorkProgress;

	
	private Image uiWorkProgress_Image;

	
	private GameObject uiIcon;

	
	private Image uiIcon_Image;

	
	private GameObject uiNoRoom;

	
	private GameObject uiSprechblase;

	
	private GameObject uiKrank;

	
	private GameObject uiLeitenderDesigner;

	
	private GameObject uiFrieren;

	
	private GameObject uiGarbage;

	
	private GameObject uiUeberfuellt;

	
	private GameObject uiLowQuality;

	
	private GameObject[] ePop_Object;

	
	private Animation[] ePopAnim;

	
	private Text[] ePopText;

	
	private int ePop_Gameplay;

	
	private int ePop_Grafik = 1;

	
	private int ePop_Sound = 2;

	
	private int ePop_Technik = 3;

	
	private int ePop_Bug = 4;

	
	private int ePop_BugRemove = 5;

	
	private int ePop_Forschung = 6;

	
	private int ePop_Marketing = 7;

	
	private int ePop_Support = 8;

	
	private int ePop_QA = 9;

	
	private int ePop_Training = 10;

	
	private int ePop_Hype = 11;

	
	private int ePop_BeduerfnisErfuellt = 12;

	
	private int ePop_BeduerfnisNichtErfuellt = 13;

	
	private int ePop_Arzt = 14;

	
	private int ePop_ProdArcade = 15;

	
	private int ePop_Hardware = 16;

	
	private int ePop_Misc = 17;

	
	public GameObject[] addObjects;

	
	public LayerMask layerMaskChar;

	
	public LayerMask layerMaskFloor;

	
	private float timerStopPopAnimations;

	
	private float invisibleTimer;

	
	private float timerForMovementActions;

	
	private float timerUpdateWork;

	
	public bool iDoWork;

	
	private Menu_Training_Select menuTrain_;
}
