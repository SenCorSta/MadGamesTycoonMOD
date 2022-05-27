using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

// Token: 0x02000042 RID: 66
public class characterScript : MonoBehaviour
{
	// Token: 0x060000F7 RID: 247 RVA: 0x00023430 File Offset: 0x00021630
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

	// Token: 0x060000F8 RID: 248 RVA: 0x000029C8 File Offset: 0x00000BC8
	private void OnDestroy()
	{
		if (this.mS_)
		{
			this.mS_.findCharacters = true;
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00023484 File Offset: 0x00021684
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

	// Token: 0x060000FA RID: 250 RVA: 0x00023628 File Offset: 0x00021828
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

	// Token: 0x060000FB RID: 251 RVA: 0x000029E3 File Offset: 0x00000BE3
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

	// Token: 0x060000FC RID: 252 RVA: 0x00023704 File Offset: 0x00021904
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

	// Token: 0x060000FD RID: 253 RVA: 0x00023CC8 File Offset: 0x00021EC8
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

	// Token: 0x060000FE RID: 254 RVA: 0x00023D20 File Offset: 0x00021F20
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

	// Token: 0x060000FF RID: 255 RVA: 0x00002A0F File Offset: 0x00000C0F
	public void HideChar()
	{
		if (this.picked)
		{
			return;
		}
		this.hided = true;
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00002A40 File Offset: 0x00000C40
	public void UnhideChar()
	{
		this.hided = false;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		this.HidePops();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00023DC4 File Offset: 0x00021FC4
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

	// Token: 0x06000102 RID: 258 RVA: 0x000243CC File Offset: 0x000225CC
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

	// Token: 0x06000103 RID: 259 RVA: 0x00024468 File Offset: 0x00022668
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

	// Token: 0x06000104 RID: 260 RVA: 0x00002A6E File Offset: 0x00000C6E
	private IEnumerator CreatePopInSeconds_SPRITE(Sprite sprite_, string text_, float waitTime, int sound)
	{
		if (!this.settings_.disableWorkIcons)
		{
			yield return new WaitForSeconds(waitTime);
			this.CreatePop_SPRITE(sprite_, text_, sound);
		}
		yield break;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00024614 File Offset: 0x00022814
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

	// Token: 0x06000106 RID: 262 RVA: 0x00002A9A File Offset: 0x00000C9A
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

	// Token: 0x06000107 RID: 263 RVA: 0x000246D0 File Offset: 0x000228D0
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

	// Token: 0x06000108 RID: 264 RVA: 0x00024774 File Offset: 0x00022974
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

	// Token: 0x06000109 RID: 265 RVA: 0x00024A0C File Offset: 0x00022C0C
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

	// Token: 0x0600010A RID: 266 RVA: 0x00024CA0 File Offset: 0x00022EA0
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

	// Token: 0x0600010B RID: 267 RVA: 0x00025364 File Offset: 0x00023564
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

	// Token: 0x0600010C RID: 268 RVA: 0x000255BC File Offset: 0x000237BC
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

	// Token: 0x0600010D RID: 269 RVA: 0x00025620 File Offset: 0x00023820
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

	// Token: 0x0600010E RID: 270 RVA: 0x000256D8 File Offset: 0x000238D8
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

	// Token: 0x0600010F RID: 271 RVA: 0x00002AC6 File Offset: 0x00000CC6
	private void DisableOutlineLayer()
	{
		if (this.outline)
		{
			this.outline = false;
			this.SetLayer(0, base.gameObject.transform.GetChild(0));
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0002572C File Offset: 0x0002392C
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

	// Token: 0x06000111 RID: 273 RVA: 0x000257A8 File Offset: 0x000239A8
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

	// Token: 0x06000112 RID: 274 RVA: 0x00025880 File Offset: 0x00023A80
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

	// Token: 0x06000113 RID: 275 RVA: 0x000259EC File Offset: 0x00023BEC
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

	// Token: 0x06000114 RID: 276 RVA: 0x00025B00 File Offset: 0x00023D00
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

	// Token: 0x06000115 RID: 277 RVA: 0x00025B5C File Offset: 0x00023D5C
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

	// Token: 0x06000116 RID: 278 RVA: 0x00025BB8 File Offset: 0x00023DB8
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

	// Token: 0x06000117 RID: 279 RVA: 0x00025C50 File Offset: 0x00023E50
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

	// Token: 0x06000118 RID: 280 RVA: 0x00025CE8 File Offset: 0x00023EE8
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

	// Token: 0x06000119 RID: 281 RVA: 0x00026398 File Offset: 0x00024598
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

	// Token: 0x0600011A RID: 282 RVA: 0x00002AEF File Offset: 0x00000CEF
	public void AddMotivation(float f)
	{
		this.s_motivation = 100f;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00002AFC File Offset: 0x00000CFC
	public void ResetKrank()
	{
		this.krank = 0;
		this.objectUsingS_.aufladungenAkt--;
		this.CreatePop(this.ePop_Arzt, 0f, 15);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00002B2B File Offset: 0x00000D2B
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

	// Token: 0x0600011D RID: 285 RVA: 0x000264D8 File Offset: 0x000246D8
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

	// Token: 0x0600011E RID: 286 RVA: 0x000265B0 File Offset: 0x000247B0
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

	// Token: 0x0600011F RID: 287 RVA: 0x00026674 File Offset: 0x00024874
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

	// Token: 0x06000120 RID: 288 RVA: 0x00026718 File Offset: 0x00024918
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

	// Token: 0x06000121 RID: 289 RVA: 0x000267C8 File Offset: 0x000249C8
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

	// Token: 0x06000122 RID: 290 RVA: 0x00002B52 File Offset: 0x00000D52
	public void ResetPause(bool erfuellt)
	{
		this.pause = 100f - (float)(this.mS_.personal_pausen * 20);
		if (erfuellt && this.objectUsingS_.isGhost)
		{
			UnityEngine.Object.Destroy(this.objectUsingS_.gameObject);
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00026878 File Offset: 0x00024A78
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

	// Token: 0x06000124 RID: 292 RVA: 0x00002B8F File Offset: 0x00000D8F
	public void ShowAddObject(int i)
	{
		if (this.addObjects[i] && !this.addObjects[i].activeSelf)
		{
			this.addObjects[i].SetActive(true);
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x000269A0 File Offset: 0x00024BA0
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

	// Token: 0x06000126 RID: 294 RVA: 0x00002BBD File Offset: 0x00000DBD
	public bool IsVisible()
	{
		return this.myRenderer.isVisible;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x000269EC File Offset: 0x00024BEC
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

	// Token: 0x06000128 RID: 296 RVA: 0x00026A4C File Offset: 0x00024C4C
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

	// Token: 0x06000129 RID: 297 RVA: 0x00027050 File Offset: 0x00025250
	public bool IsUeberfuellt()
	{
		return this.roomID != -1 && this.roomS_ && this.objectUsingID != -1 && this.objectUsingS_ && this.objectUsingS_.isArbeitsplatz && this.roomS_.IsUberberfuell();
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000270A4 File Offset: 0x000252A4
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

	// Token: 0x0600012B RID: 299 RVA: 0x00027104 File Offset: 0x00025304
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

	// Token: 0x0600012C RID: 300 RVA: 0x0002721C File Offset: 0x0002541C
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

	// Token: 0x0600012D RID: 301 RVA: 0x000272C4 File Offset: 0x000254C4
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

	// Token: 0x0600012E RID: 302 RVA: 0x000276C8 File Offset: 0x000258C8
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

	// Token: 0x0600012F RID: 303 RVA: 0x0002777C File Offset: 0x0002597C
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

	// Token: 0x06000130 RID: 304 RVA: 0x000277C0 File Offset: 0x000259C0
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

	// Token: 0x06000131 RID: 305 RVA: 0x0002794C File Offset: 0x00025B4C
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

	// Token: 0x06000132 RID: 306 RVA: 0x00027F2C File Offset: 0x0002612C
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

	// Token: 0x06000133 RID: 307 RVA: 0x0002850C File Offset: 0x0002670C
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

	// Token: 0x06000134 RID: 308 RVA: 0x00028AE8 File Offset: 0x00026CE8
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

	// Token: 0x06000135 RID: 309 RVA: 0x00028BD8 File Offset: 0x00026DD8
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

	// Token: 0x06000136 RID: 310 RVA: 0x00028CC8 File Offset: 0x00026EC8
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

	// Token: 0x06000137 RID: 311 RVA: 0x00028DB8 File Offset: 0x00026FB8
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

	// Token: 0x06000138 RID: 312 RVA: 0x00028EA8 File Offset: 0x000270A8
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

	// Token: 0x06000139 RID: 313 RVA: 0x00029144 File Offset: 0x00027344
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

	// Token: 0x0600013A RID: 314 RVA: 0x00029240 File Offset: 0x00027440
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

	// Token: 0x0600013B RID: 315 RVA: 0x00029330 File Offset: 0x00027530
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

	// Token: 0x0600013C RID: 316 RVA: 0x00029468 File Offset: 0x00027668
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

	// Token: 0x0600013D RID: 317 RVA: 0x00029574 File Offset: 0x00027774
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

	// Token: 0x0600013E RID: 318 RVA: 0x00029664 File Offset: 0x00027864
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

	// Token: 0x0600013F RID: 319 RVA: 0x00029754 File Offset: 0x00027954
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

	// Token: 0x06000140 RID: 320 RVA: 0x00029844 File Offset: 0x00027A44
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

	// Token: 0x06000141 RID: 321 RVA: 0x00029A08 File Offset: 0x00027C08
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

	// Token: 0x06000142 RID: 322 RVA: 0x00029AF8 File Offset: 0x00027CF8
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

	// Token: 0x06000143 RID: 323 RVA: 0x00029C08 File Offset: 0x00027E08
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

	// Token: 0x06000144 RID: 324 RVA: 0x00029D18 File Offset: 0x00027F18
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

	// Token: 0x06000145 RID: 325 RVA: 0x00029E08 File Offset: 0x00028008
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

	// Token: 0x06000146 RID: 326 RVA: 0x00029EF8 File Offset: 0x000280F8
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

	// Token: 0x06000147 RID: 327 RVA: 0x00029FE8 File Offset: 0x000281E8
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

	// Token: 0x06000148 RID: 328 RVA: 0x0002A0EC File Offset: 0x000282EC
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

	// Token: 0x06000149 RID: 329 RVA: 0x00002BCA File Offset: 0x00000DCA
	private float GetSkillCap()
	{
		if (!this.perks[15])
		{
			return 200f;
		}
		return 255f;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0002A6D8 File Offset: 0x000288D8
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

	// Token: 0x0600014B RID: 331 RVA: 0x0002A7C8 File Offset: 0x000289C8
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

	// Token: 0x0600014C RID: 332 RVA: 0x0002AC24 File Offset: 0x00028E24
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

	// Token: 0x0600014D RID: 333 RVA: 0x0002AC6C File Offset: 0x00028E6C
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

	// Token: 0x0600014E RID: 334 RVA: 0x0002ADC8 File Offset: 0x00028FC8
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

	// Token: 0x0600014F RID: 335 RVA: 0x0002AEB8 File Offset: 0x000290B8
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

	// Token: 0x06000150 RID: 336 RVA: 0x0002AF0C File Offset: 0x0002910C
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

	// Token: 0x06000151 RID: 337 RVA: 0x0002AF7C File Offset: 0x0002917C
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

	// Token: 0x06000152 RID: 338 RVA: 0x0002B084 File Offset: 0x00029284
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

	// Token: 0x0400029D RID: 669
	private mainScript mS_;

	// Token: 0x0400029E RID: 670
	private Camera camera_;

	// Token: 0x0400029F RID: 671
	private mainCameraScript mCamS_;

	// Token: 0x040002A0 RID: 672
	private GameObject main_;

	// Token: 0x040002A1 RID: 673
	private settingsScript settings_;

	// Token: 0x040002A2 RID: 674
	private GUI_Tooltip guiTooltip;

	// Token: 0x040002A3 RID: 675
	private GUI_Main guiMain_;

	// Token: 0x040002A4 RID: 676
	private sfxScript sfx_;

	// Token: 0x040002A5 RID: 677
	private textScript tS_;

	// Token: 0x040002A6 RID: 678
	private clipScript clipS_;

	// Token: 0x040002A7 RID: 679
	private roomDataScript rdS_;

	// Token: 0x040002A8 RID: 680
	public movementScript moveS_;

	// Token: 0x040002A9 RID: 681
	private mapScript mapS_;

	// Token: 0x040002AA RID: 682
	public SkinnedMeshRenderer myRenderer;

	// Token: 0x040002AB RID: 683
	public GameObject[] uiPrefabs;

	// Token: 0x040002AC RID: 684
	public Color[] colors;

	// Token: 0x040002AD RID: 685
	public int myID;

	// Token: 0x040002AE RID: 686
	public bool male;

	// Token: 0x040002AF RID: 687
	public string myName;

	// Token: 0x040002B0 RID: 688
	public int group = -1;

	// Token: 0x040002B1 RID: 689
	public int roomID = -1;

	// Token: 0x040002B2 RID: 690
	public roomScript roomS_;

	// Token: 0x040002B3 RID: 691
	public int objectUsingID = -1;

	// Token: 0x040002B4 RID: 692
	public objectScript objectUsingS_;

	// Token: 0x040002B5 RID: 693
	public int objectBelegtID = -1;

	// Token: 0x040002B6 RID: 694
	public objectScript objectBelegtS_;

	// Token: 0x040002B7 RID: 695
	public objectScript mainArbeitsplatzS_;

	// Token: 0x040002B8 RID: 696
	public int beruf;

	// Token: 0x040002B9 RID: 697
	public float s_motivation;

	// Token: 0x040002BA RID: 698
	public float s_gamedesign;

	// Token: 0x040002BB RID: 699
	public float s_programmieren;

	// Token: 0x040002BC RID: 700
	public float s_grafik;

	// Token: 0x040002BD RID: 701
	public float s_sound;

	// Token: 0x040002BE RID: 702
	public float s_pr;

	// Token: 0x040002BF RID: 703
	public float s_gametests;

	// Token: 0x040002C0 RID: 704
	public float s_technik;

	// Token: 0x040002C1 RID: 705
	public float s_forschen;

	// Token: 0x040002C2 RID: 706
	public bool[] perks;

	// Token: 0x040002C3 RID: 707
	public int legend = -1;

	// Token: 0x040002C4 RID: 708
	public float workProgress;

	// Token: 0x040002C5 RID: 709
	public float durst;

	// Token: 0x040002C6 RID: 710
	public float hunger;

	// Token: 0x040002C7 RID: 711
	public float klo;

	// Token: 0x040002C8 RID: 712
	public float waschbecken;

	// Token: 0x040002C9 RID: 713
	public float muell;

	// Token: 0x040002CA RID: 714
	public float giessen;

	// Token: 0x040002CB RID: 715
	public float pause;

	// Token: 0x040002CC RID: 716
	public float freezer;

	// Token: 0x040002CD RID: 717
	public int krank;

	// Token: 0x040002CE RID: 718
	private bool outline;

	// Token: 0x040002CF RID: 719
	private bool uiVisible;

	// Token: 0x040002D0 RID: 720
	public bool picked;

	// Token: 0x040002D1 RID: 721
	public bool hided;

	// Token: 0x040002D2 RID: 722
	public int model_body = -1;

	// Token: 0x040002D3 RID: 723
	public int model_eyes = -1;

	// Token: 0x040002D4 RID: 724
	public int model_hair = -1;

	// Token: 0x040002D5 RID: 725
	public int model_beard = -1;

	// Token: 0x040002D6 RID: 726
	public int model_skinColor = -1;

	// Token: 0x040002D7 RID: 727
	public int model_hairColor = -1;

	// Token: 0x040002D8 RID: 728
	public int model_beardColor = -1;

	// Token: 0x040002D9 RID: 729
	public int model_HoseColor = -1;

	// Token: 0x040002DA RID: 730
	public int model_ShirtColor = -1;

	// Token: 0x040002DB RID: 731
	public int model_Add1Color = -1;

	// Token: 0x040002DC RID: 732
	private GameObject myUI;

	// Token: 0x040002DD RID: 733
	private RectTransform myUIRect;

	// Token: 0x040002DE RID: 734
	private GameObject uiIconMain;

	// Token: 0x040002DF RID: 735
	private GameObject uiWorkProgress;

	// Token: 0x040002E0 RID: 736
	private Image uiWorkProgress_Image;

	// Token: 0x040002E1 RID: 737
	private GameObject uiIcon;

	// Token: 0x040002E2 RID: 738
	private Image uiIcon_Image;

	// Token: 0x040002E3 RID: 739
	private GameObject uiNoRoom;

	// Token: 0x040002E4 RID: 740
	private GameObject uiSprechblase;

	// Token: 0x040002E5 RID: 741
	private GameObject uiKrank;

	// Token: 0x040002E6 RID: 742
	private GameObject uiLeitenderDesigner;

	// Token: 0x040002E7 RID: 743
	private GameObject uiFrieren;

	// Token: 0x040002E8 RID: 744
	private GameObject uiGarbage;

	// Token: 0x040002E9 RID: 745
	private GameObject uiUeberfuellt;

	// Token: 0x040002EA RID: 746
	private GameObject uiLowQuality;

	// Token: 0x040002EB RID: 747
	private GameObject[] ePop_Object;

	// Token: 0x040002EC RID: 748
	private Animation[] ePopAnim;

	// Token: 0x040002ED RID: 749
	private Text[] ePopText;

	// Token: 0x040002EE RID: 750
	private int ePop_Gameplay;

	// Token: 0x040002EF RID: 751
	private int ePop_Grafik = 1;

	// Token: 0x040002F0 RID: 752
	private int ePop_Sound = 2;

	// Token: 0x040002F1 RID: 753
	private int ePop_Technik = 3;

	// Token: 0x040002F2 RID: 754
	private int ePop_Bug = 4;

	// Token: 0x040002F3 RID: 755
	private int ePop_BugRemove = 5;

	// Token: 0x040002F4 RID: 756
	private int ePop_Forschung = 6;

	// Token: 0x040002F5 RID: 757
	private int ePop_Marketing = 7;

	// Token: 0x040002F6 RID: 758
	private int ePop_Support = 8;

	// Token: 0x040002F7 RID: 759
	private int ePop_QA = 9;

	// Token: 0x040002F8 RID: 760
	private int ePop_Training = 10;

	// Token: 0x040002F9 RID: 761
	private int ePop_Hype = 11;

	// Token: 0x040002FA RID: 762
	private int ePop_BeduerfnisErfuellt = 12;

	// Token: 0x040002FB RID: 763
	private int ePop_BeduerfnisNichtErfuellt = 13;

	// Token: 0x040002FC RID: 764
	private int ePop_Arzt = 14;

	// Token: 0x040002FD RID: 765
	private int ePop_ProdArcade = 15;

	// Token: 0x040002FE RID: 766
	private int ePop_Hardware = 16;

	// Token: 0x040002FF RID: 767
	private int ePop_Misc = 17;

	// Token: 0x04000300 RID: 768
	public GameObject[] addObjects;

	// Token: 0x04000301 RID: 769
	public LayerMask layerMaskChar;

	// Token: 0x04000302 RID: 770
	public LayerMask layerMaskFloor;

	// Token: 0x04000303 RID: 771
	private float timerStopPopAnimations;

	// Token: 0x04000304 RID: 772
	private float invisibleTimer;

	// Token: 0x04000305 RID: 773
	private float timerForMovementActions;

	// Token: 0x04000306 RID: 774
	private float timerUpdateWork;

	// Token: 0x04000307 RID: 775
	public bool iDoWork;

	// Token: 0x04000308 RID: 776
	private Menu_Training_Select menuTrain_;
}
