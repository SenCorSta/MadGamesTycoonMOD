using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000159 RID: 345
public class Menu_Forschung : MonoBehaviour
{
	// Token: 0x06000CB3 RID: 3251 RVA: 0x00089D87 File Offset: 0x00087F87
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00089D90 File Offset: 0x00087F90
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeature_)
		{
			this.hardwareFeature_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.copyProtect_)
		{
			this.copyProtect_ = this.main_.GetComponent<copyProtect>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.myInputField)
		{
			this.myInputField = this.uiObjects[7].GetComponent<InputField>();
		}
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00089F68 File Offset: 0x00088168
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00089F9A File Offset: 0x0008819A
	private void OnEnable()
	{
		this.uiObjects[7].GetComponent<InputField>().text = "";
		this.FindScripts();
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00089FBC File Offset: 0x000881BC
	public int GetAmountForschung(int i, bool getUnerforschtesObjekt)
	{
		this.FindScripts();
		int num = 0;
		switch (i)
		{
		case 0:
			for (int j = 0; j < this.genres_.genres_RES_POINTS.Length; j++)
			{
				if (this.genres_.genres_UNLOCK[j] && !this.genres_.IsErforscht(j) && !this.genres_.BereitsInAnderenRaumAktiv(j))
				{
					num++;
					if (getUnerforschtesObjekt)
					{
						return j;
					}
				}
			}
			break;
		case 1:
			for (int k = 0; k < this.themes_.themes_RES_POINTS_LEFT.Length; k++)
			{
				if (!this.themes_.IsErforscht(k) && !this.themes_.BereitsInAnderenRaumAktiv(k))
				{
					num++;
					if (getUnerforschtesObjekt)
					{
						return k;
					}
				}
			}
			break;
		case 2:
			for (int l = 0; l < this.eF_.engineFeatures_RES_POINTS.Length; l++)
			{
				if (this.eF_.engineFeatures_UNLOCK[l] && !this.eF_.IsErforscht(l) && !this.eF_.BereitsInAnderenRaumAktiv(l))
				{
					num++;
					if (getUnerforschtesObjekt)
					{
						return l;
					}
				}
			}
			break;
		case 3:
			for (int m = 0; m < this.gF_.gameplayFeatures_RES_POINTS.Length; m++)
			{
				if (this.gF_.gameplayFeatures_UNLOCK[m] && !this.gF_.IsErforscht(m) && !this.gF_.BereitsInAnderenRaumAktiv(m))
				{
					num++;
					if (getUnerforschtesObjekt)
					{
						return m;
					}
				}
			}
			break;
		case 4:
			for (int n = 0; n < this.hardware_.hardware_RES_POINTS.Length; n++)
			{
				if (this.hardware_.hardware_UNLOCK[n] && !this.hardware_.IsErforscht(n) && !this.hardware_.BereitsInAnderenRaumAktiv(n))
				{
					num++;
					if (getUnerforschtesObjekt)
					{
						return n;
					}
				}
			}
			break;
		case 5:
			if (!this.fS_.IsErforscht(0) && !this.fS_.BereitsInAnderenRaumAktiv(0))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 0;
				}
			}
			if (!this.fS_.IsErforscht(1) && this.fS_.IsErforscht(0) && !this.fS_.BereitsInAnderenRaumAktiv(1))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 1;
				}
			}
			if (!this.fS_.IsErforscht(2) && this.fS_.IsErforscht(1) && !this.fS_.BereitsInAnderenRaumAktiv(2))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 2;
				}
			}
			if (!this.fS_.IsErforscht(3) && this.fS_.IsErforscht(2) && !this.fS_.BereitsInAnderenRaumAktiv(3))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 3;
				}
			}
			if (!this.fS_.IsErforscht(35) && !this.fS_.BereitsInAnderenRaumAktiv(35))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 35;
				}
			}
			if (!this.fS_.IsErforscht(36) && !this.fS_.BereitsInAnderenRaumAktiv(36))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 36;
				}
			}
			if (!this.fS_.IsErforscht(28) && !this.fS_.BereitsInAnderenRaumAktiv(28))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 28;
				}
			}
			if (!this.fS_.IsErforscht(29) && !this.fS_.BereitsInAnderenRaumAktiv(29))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 29;
				}
			}
			if (!this.fS_.IsErforscht(30) && !this.fS_.BereitsInAnderenRaumAktiv(30))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 30;
				}
			}
			if (!this.fS_.IsErforscht(31) && !this.fS_.BereitsInAnderenRaumAktiv(31))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 31;
				}
			}
			if (!this.fS_.IsErforscht(32) && !this.fS_.BereitsInAnderenRaumAktiv(32))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 32;
				}
			}
			if (!this.fS_.IsErforscht(33) && !this.fS_.BereitsInAnderenRaumAktiv(33))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 33;
				}
			}
			if (!this.fS_.IsErforscht(34) && !this.fS_.BereitsInAnderenRaumAktiv(34))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 34;
				}
			}
			if (!this.fS_.IsErforscht(38) && !this.fS_.BereitsInAnderenRaumAktiv(38))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 38;
				}
			}
			if (!this.fS_.IsErforscht(39) && !this.fS_.BereitsInAnderenRaumAktiv(39))
			{
				num++;
				if (getUnerforschtesObjekt)
				{
					return 39;
				}
			}
			break;
		case 6:
			for (int num2 = 0; num2 < this.hardwareFeature_.hardFeat_RES_POINTS.Length; num2++)
			{
				if (this.hardwareFeature_.hardFeat_UNLOCK[num2] && !this.hardwareFeature_.IsErforscht(num2) && !this.hardwareFeature_.BereitsInAnderenRaumAktiv(num2))
				{
					num++;
					if (getUnerforschtesObjekt)
					{
						return num2;
					}
				}
			}
			break;
		}
		if (getUnerforschtesObjekt)
		{
			return -1;
		}
		return num;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0008A460 File Offset: 0x00088660
	public void Init(int roomID_, int i)
	{
		this.FindScripts();
		this.roomID = roomID_;
		this.forschungsTyp = i;
		this.InitDropdowns();
		switch (i)
		{
		case 0:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(158);
			break;
		case 1:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(159);
			break;
		case 2:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(160);
			break;
		case 3:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(161);
			break;
		case 4:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(162);
			break;
		case 5:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(163);
			break;
		case 6:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1617);
			break;
		}
		switch (i)
		{
		case 0:
			for (int j = 0; j < this.genres_.genres_RES_POINTS.Length; j++)
			{
				if (this.genres_.genres_UNLOCK[j] && !this.genres_.IsErforscht(j) && !this.genres_.BereitsInAnderenRaumAktiv(j))
				{
					string text = this.genres_.GetName(j);
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if (this.myInputField.text.Length <= 0 || text.Contains(this.searchStringA))
					{
						Item_Genre_Forschung component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Genre_Forschung>();
						component.myID = j;
						component.mS_ = this.mS_;
						component.tS_ = this.tS_;
						component.sfx_ = this.sfx_;
						component.guiMain_ = this.guiMain_;
						component.genres_ = this.genres_;
					}
				}
			}
			break;
		case 1:
			for (int k = 0; k < this.themes_.themes_RES_POINTS_LEFT.Length; k++)
			{
				if (!this.themes_.IsErforscht(k) && !this.themes_.BereitsInAnderenRaumAktiv(k))
				{
					bool flag = false;
					if (this.myInputField.text.Length > 0)
					{
						string themes = this.tS_.GetThemes(k);
						this.searchStringA = this.searchStringA.ToLower();
						if (themes.ToLower().Contains(this.searchStringA))
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						Item_Themes_Forschung component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Themes_Forschung>();
						component2.myID = k;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.themes_ = this.themes_;
					}
				}
			}
			break;
		case 2:
			for (int l = 0; l < this.eF_.engineFeatures_RES_POINTS.Length; l++)
			{
				if (this.eF_.engineFeatures_UNLOCK[l] && !this.eF_.IsErforscht(l) && !this.eF_.BereitsInAnderenRaumAktiv(l))
				{
					string text2 = this.eF_.GetName(l);
					this.searchStringA = this.searchStringA.ToLower();
					text2 = text2.ToLower();
					if (this.myInputField.text.Length <= 0 || text2.Contains(this.searchStringA))
					{
						Item_EngineFeatures_Forschung component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[3], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_EngineFeatures_Forschung>();
						component3.myID = l;
						component3.mS_ = this.mS_;
						component3.tS_ = this.tS_;
						component3.sfx_ = this.sfx_;
						component3.guiMain_ = this.guiMain_;
						component3.eF_ = this.eF_;
					}
				}
			}
			break;
		case 3:
			for (int m = 0; m < this.gF_.gameplayFeatures_RES_POINTS.Length; m++)
			{
				if (this.gF_.gameplayFeatures_UNLOCK[m] && !this.gF_.IsErforscht(m) && !this.gF_.BereitsInAnderenRaumAktiv(m))
				{
					string text3 = this.gF_.GetName(m);
					this.searchStringA = this.searchStringA.ToLower();
					text3 = text3.ToLower();
					if (this.myInputField.text.Length <= 0 || text3.Contains(this.searchStringA))
					{
						Item_GameplayFeatures_Forschung component4 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[4], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_GameplayFeatures_Forschung>();
						component4.myID = m;
						component4.mS_ = this.mS_;
						component4.tS_ = this.tS_;
						component4.sfx_ = this.sfx_;
						component4.guiMain_ = this.guiMain_;
						component4.gF_ = this.gF_;
					}
				}
			}
			break;
		case 4:
			for (int n = 0; n < this.hardware_.hardware_RES_POINTS.Length; n++)
			{
				if (this.hardware_.hardware_UNLOCK[n] && !this.hardware_.IsErforscht(n) && !this.hardware_.BereitsInAnderenRaumAktiv(n))
				{
					string text4 = this.hardware_.GetName(n);
					this.searchStringA = this.searchStringA.ToLower();
					text4 = text4.ToLower();
					if (this.myInputField.text.Length <= 0 || text4.Contains(this.searchStringA))
					{
						Item_Hardware_Forschung component5 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[5], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Hardware_Forschung>();
						component5.myID = n;
						component5.mS_ = this.mS_;
						component5.tS_ = this.tS_;
						component5.sfx_ = this.sfx_;
						component5.guiMain_ = this.guiMain_;
						component5.hardware_ = this.hardware_;
					}
				}
			}
			break;
		case 5:
			if (!this.fS_.IsErforscht(0) && !this.fS_.BereitsInAnderenRaumAktiv(0))
			{
				string text5 = this.fS_.GetName(0);
				this.searchStringA = this.searchStringA.ToLower();
				text5 = text5.ToLower();
				if (this.myInputField.text.Length <= 0 || text5.Contains(this.searchStringA))
				{
					this.CreateItem(0);
				}
			}
			if (!this.fS_.IsErforscht(1) && this.fS_.IsErforscht(0) && !this.fS_.BereitsInAnderenRaumAktiv(1))
			{
				string text6 = this.fS_.GetName(1);
				this.searchStringA = this.searchStringA.ToLower();
				text6 = text6.ToLower();
				if (this.myInputField.text.Length <= 0 || text6.Contains(this.searchStringA))
				{
					this.CreateItem(1);
				}
			}
			if (!this.fS_.IsErforscht(2) && this.fS_.IsErforscht(1) && !this.fS_.BereitsInAnderenRaumAktiv(2))
			{
				string text7 = this.fS_.GetName(2);
				this.searchStringA = this.searchStringA.ToLower();
				text7 = text7.ToLower();
				if (this.myInputField.text.Length <= 0 || text7.Contains(this.searchStringA))
				{
					this.CreateItem(2);
				}
			}
			if (!this.fS_.IsErforscht(3) && this.fS_.IsErforscht(2) && !this.fS_.BereitsInAnderenRaumAktiv(3))
			{
				string text8 = this.fS_.GetName(3);
				this.searchStringA = this.searchStringA.ToLower();
				text8 = text8.ToLower();
				if (this.myInputField.text.Length <= 0 || text8.Contains(this.searchStringA))
				{
					this.CreateItem(3);
				}
			}
			if (!this.fS_.IsErforscht(35) && !this.fS_.BereitsInAnderenRaumAktiv(35))
			{
				string text9 = this.fS_.GetName(35);
				this.searchStringA = this.searchStringA.ToLower();
				text9 = text9.ToLower();
				if (this.myInputField.text.Length <= 0 || text9.Contains(this.searchStringA))
				{
					this.CreateItem(35);
				}
			}
			if (!this.fS_.IsErforscht(36) && !this.fS_.BereitsInAnderenRaumAktiv(36))
			{
				string text10 = this.fS_.GetName(36);
				this.searchStringA = this.searchStringA.ToLower();
				text10 = text10.ToLower();
				if (this.myInputField.text.Length <= 0 || text10.Contains(this.searchStringA))
				{
					this.CreateItem(36);
				}
			}
			if (!this.fS_.IsErforscht(28) && !this.fS_.BereitsInAnderenRaumAktiv(28))
			{
				string text11 = this.fS_.GetName(28);
				this.searchStringA = this.searchStringA.ToLower();
				text11 = text11.ToLower();
				if (this.myInputField.text.Length <= 0 || text11.Contains(this.searchStringA))
				{
					this.CreateItem(28);
				}
			}
			if (!this.fS_.IsErforscht(29) && !this.fS_.BereitsInAnderenRaumAktiv(29))
			{
				string text12 = this.fS_.GetName(29);
				this.searchStringA = this.searchStringA.ToLower();
				text12 = text12.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text12.Contains(this.searchStringA))
				{
					this.CreateItem(29);
				}
			}
			if (!this.fS_.IsErforscht(30) && !this.fS_.BereitsInAnderenRaumAktiv(30))
			{
				string text13 = this.fS_.GetName(30);
				this.searchStringA = this.searchStringA.ToLower();
				text13 = text13.ToLower();
				if (this.myInputField.text.Length <= 0 || text13.Contains(this.searchStringA))
				{
					this.CreateItem(30);
				}
			}
			if (!this.fS_.IsErforscht(31) && !this.fS_.BereitsInAnderenRaumAktiv(31))
			{
				string text14 = this.fS_.GetName(31);
				this.searchStringA = this.searchStringA.ToLower();
				text14 = text14.ToLower();
				if (this.myInputField.text.Length <= 0 || text14.Contains(this.searchStringA))
				{
					this.CreateItem(31);
				}
			}
			if (!this.fS_.IsErforscht(32) && !this.fS_.BereitsInAnderenRaumAktiv(32))
			{
				string text15 = this.fS_.GetName(32);
				this.searchStringA = this.searchStringA.ToLower();
				text15 = text15.ToLower();
				if (this.myInputField.text.Length <= 0 || text15.Contains(this.searchStringA))
				{
					this.CreateItem(32);
				}
			}
			if (!this.fS_.IsErforscht(33) && !this.fS_.BereitsInAnderenRaumAktiv(33))
			{
				string text16 = this.fS_.GetName(33);
				this.searchStringA = this.searchStringA.ToLower();
				text16 = text16.ToLower();
				if (this.myInputField.text.Length <= 0 || text16.Contains(this.searchStringA))
				{
					this.CreateItem(33);
				}
			}
			if (!this.fS_.IsErforscht(34) && !this.fS_.BereitsInAnderenRaumAktiv(34))
			{
				string text17 = this.fS_.GetName(34);
				this.searchStringA = this.searchStringA.ToLower();
				text17 = text17.ToLower();
				if (this.myInputField.text.Length <= 0 || text17.Contains(this.searchStringA))
				{
					this.CreateItem(34);
				}
			}
			if (!this.fS_.IsErforscht(38) && !this.fS_.BereitsInAnderenRaumAktiv(38))
			{
				string text18 = this.fS_.GetName(38);
				this.searchStringA = this.searchStringA.ToLower();
				text18 = text18.ToLower();
				if (this.myInputField.text.Length <= 0 || text18.Contains(this.searchStringA))
				{
					this.CreateItem(38);
				}
			}
			if (!this.fS_.IsErforscht(39) && !this.fS_.BereitsInAnderenRaumAktiv(39))
			{
				string text19 = this.fS_.GetName(39);
				this.searchStringA = this.searchStringA.ToLower();
				text19 = text19.ToLower();
				if (this.myInputField.text.Length <= 0 || text19.Contains(this.searchStringA))
				{
					this.CreateItem(39);
				}
			}
			break;
		case 6:
			for (int num = 0; num < this.hardwareFeature_.hardFeat_RES_POINTS.Length; num++)
			{
				if (this.hardwareFeature_.hardFeat_UNLOCK[num] && !this.hardwareFeature_.IsErforscht(num) && !this.hardwareFeature_.BereitsInAnderenRaumAktiv(num))
				{
					string text20 = this.hardwareFeature_.GetName(num);
					this.searchStringA = this.searchStringA.ToLower();
					text20 = text20.ToLower();
					if (this.myInputField.text.Length <= 0 || text20.Contains(this.searchStringA))
					{
						Item_HardFeat_Forschung component6 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[7], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_HardFeat_Forschung>();
						component6.myID = num;
						component6.mS_ = this.mS_;
						component6.tS_ = this.tS_;
						component6.sfx_ = this.sfx_;
						component6.guiMain_ = this.guiMain_;
						component6.hardwareFeatures_ = this.hardwareFeature_;
					}
				}
			}
			break;
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0008B31C File Offset: 0x0008951C
	private void CreateItem(int id_)
	{
		if (!this.fS_.IsErforscht(id_))
		{
			Item_Sonstiges_Forschung component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[6], Vector3.zero, Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Sonstiges_Forschung>();
			component.myID = id_;
			component.mS_ = this.mS_;
			component.tS_ = this.tS_;
			component.sfx_ = this.sfx_;
			component.guiMain_ = this.guiMain_;
			component.unlock_ = this.unlock_;
			component.fS_ = this.fS_;
		}
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0008B3B0 File Offset: 0x000895B0
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[7].GetComponent<InputField>().text;
		this.Init(this.roomID, this.forschungsTyp);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0008B430 File Offset: 0x00089630
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x0008B458 File Offset: 0x00089658
	public void InitDropdowns()
	{
		int value = 0;
		switch (this.forschungsTyp)
		{
		case 0:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_0");
			break;
		case 1:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_1");
			break;
		case 2:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_2");
			break;
		case 3:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_3");
			break;
		case 4:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_4");
			break;
		case 5:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_5");
			break;
		case 6:
			value = PlayerPrefs.GetInt("DD_Menu_Forschung_6");
			break;
		}
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(414));
		list.Add(this.tS_.GetText(415));
		if (this.forschungsTyp == 4)
		{
			list.Add(this.tS_.GetText(1605));
		}
		if (this.forschungsTyp == 4)
		{
			list.Add(this.tS_.GetText(4));
		}
		this.uiObjects[6].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[6].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[6].GetComponent<Dropdown>().value = value;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0008B5A8 File Offset: 0x000897A8
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[6].GetComponent<Dropdown>().value;
		switch (this.forschungsTyp)
		{
		case 0:
			PlayerPrefs.SetInt("DD_Menu_Forschung_0", value);
			break;
		case 1:
			PlayerPrefs.SetInt("DD_Menu_Forschung_1", value);
			break;
		case 2:
			PlayerPrefs.SetInt("DD_Menu_Forschung_2", value);
			break;
		case 3:
			PlayerPrefs.SetInt("DD_Menu_Forschung_3", value);
			break;
		case 4:
			PlayerPrefs.SetInt("DD_Menu_Forschung_4", value);
			break;
		case 5:
			PlayerPrefs.SetInt("DD_Menu_Forschung_5", value);
			break;
		case 6:
			PlayerPrefs.SetInt("DD_Menu_Forschung_6", value);
			break;
		}
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Genre_Forschung component = gameObject.GetComponent<Item_Genre_Forschung>();
				Item_Themes_Forschung component2 = gameObject.GetComponent<Item_Themes_Forschung>();
				Item_EngineFeatures_Forschung component3 = gameObject.GetComponent<Item_EngineFeatures_Forschung>();
				Item_GameplayFeatures_Forschung component4 = gameObject.GetComponent<Item_GameplayFeatures_Forschung>();
				Item_Hardware_Forschung component5 = gameObject.GetComponent<Item_Hardware_Forschung>();
				Item_Sonstiges_Forschung component6 = gameObject.GetComponent<Item_Sonstiges_Forschung>();
				Item_HardFeat_Forschung component7 = gameObject.GetComponent<Item_HardFeat_Forschung>();
				switch (value)
				{
				case 0:
					if (component)
					{
						gameObject.name = this.genres_.GetName(component.myID);
					}
					if (component2)
					{
						gameObject.name = this.tS_.GetThemes(component2.myID);
					}
					if (component3)
					{
						gameObject.name = this.eF_.GetName(component3.myID);
					}
					if (component4)
					{
						gameObject.name = this.gF_.GetName(component4.myID);
					}
					if (component5)
					{
						gameObject.name = this.hardware_.GetName(component5.myID);
					}
					if (component6)
					{
						gameObject.name = this.fS_.GetName(component6.myID);
					}
					if (component7)
					{
						gameObject.name = this.hardwareFeature_.GetName(component7.myID);
					}
					break;
				case 1:
					if (component)
					{
						gameObject.name = this.genres_.GetPrice(component.myID).ToString();
					}
					if (component2)
					{
						gameObject.name = this.themes_.GetPrice(component2.myID).ToString();
					}
					if (component3)
					{
						gameObject.name = this.eF_.GetPrice(component3.myID).ToString();
					}
					if (component4)
					{
						gameObject.name = this.gF_.GetPrice(component4.myID).ToString();
					}
					if (component5)
					{
						gameObject.name = this.hardware_.GetPrice(component5.myID).ToString();
					}
					if (component6)
					{
						gameObject.name = this.fS_.RES_PRICE[component6.myID].ToString();
					}
					if (component7)
					{
						gameObject.name = this.hardwareFeature_.GetPrice(component7.myID).ToString();
					}
					break;
				case 2:
					if (component)
					{
						gameObject.name = this.genres_.genres_RES_POINTS_LEFT[component.myID].ToString();
					}
					if (component2)
					{
						gameObject.name = this.themes_.themes_RES_POINTS_LEFT[component2.myID].ToString();
					}
					if (component3)
					{
						gameObject.name = this.eF_.engineFeatures_RES_POINTS_LEFT[component3.myID].ToString();
					}
					if (component4)
					{
						gameObject.name = this.gF_.gameplayFeatures_RES_POINTS_LEFT[component4.myID].ToString();
					}
					if (component5)
					{
						gameObject.name = this.hardware_.hardware_RES_POINTS_LEFT[component5.myID].ToString();
					}
					if (component6)
					{
						gameObject.name = this.fS_.RES_POINTS_LEFT[component6.myID].ToString();
					}
					if (component7)
					{
						gameObject.name = this.hardwareFeature_.hardFeat_RES_POINTS_LEFT[component7.myID].ToString();
					}
					break;
				case 3:
					if (this.forschungsTyp == 4)
					{
						gameObject.name = this.hardware_.hardware_TYP[component5.myID].ToString();
					}
					break;
				case 4:
					if (this.forschungsTyp == 4)
					{
						gameObject.name = this.hardware_.hardware_TECH[component5.myID].ToString();
					}
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x0400112C RID: 4396
	private mainScript mS_;

	// Token: 0x0400112D RID: 4397
	private GameObject main_;

	// Token: 0x0400112E RID: 4398
	private GUI_Main guiMain_;

	// Token: 0x0400112F RID: 4399
	private sfxScript sfx_;

	// Token: 0x04001130 RID: 4400
	private textScript tS_;

	// Token: 0x04001131 RID: 4401
	private genres genres_;

	// Token: 0x04001132 RID: 4402
	private themes themes_;

	// Token: 0x04001133 RID: 4403
	private engineFeatures eF_;

	// Token: 0x04001134 RID: 4404
	private gameplayFeatures gF_;

	// Token: 0x04001135 RID: 4405
	private hardware hardware_;

	// Token: 0x04001136 RID: 4406
	private hardwareFeatures hardwareFeature_;

	// Token: 0x04001137 RID: 4407
	private unlockScript unlock_;

	// Token: 0x04001138 RID: 4408
	private forschungSonstiges fS_;

	// Token: 0x04001139 RID: 4409
	private copyProtect copyProtect_;

	// Token: 0x0400113A RID: 4410
	public int roomID = -1;

	// Token: 0x0400113B RID: 4411
	private int forschungsTyp;

	// Token: 0x0400113C RID: 4412
	public GameObject[] uiPrefabs;

	// Token: 0x0400113D RID: 4413
	public GameObject[] uiObjects;

	// Token: 0x0400113E RID: 4414
	public InputField myInputField;

	// Token: 0x0400113F RID: 4415
	private string searchStringA = "";
}
