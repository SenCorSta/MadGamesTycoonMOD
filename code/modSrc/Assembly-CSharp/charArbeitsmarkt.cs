using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class charArbeitsmarkt : MonoBehaviour
{
	// Token: 0x060000E6 RID: 230 RVA: 0x0000B217 File Offset: 0x00009417
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000B220 File Offset: 0x00009420
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
		if (!this.clothScript_)
		{
			this.clothScript_ = this.main_.GetComponent<clothScript>();
		}
		if (!this.cCS_)
		{
			this.cCS_ = this.main_.GetComponent<createCharScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x0000B2E4 File Offset: 0x000094E4
	public void Create(taskMitarbeitersuche task_)
	{
		this.FindScripts();
		this.myID = UnityEngine.Random.Range(1, 99999999);
		base.name = "AA_" + this.myID.ToString();
		this.male = true;
		if (UnityEngine.Random.Range(0, 100) < 33)
		{
			this.male = false;
		}
		this.myName = this.tS_.GetRandomCharName(this.male);
		this.s_gamedesign = UnityEngine.Random.Range(10f, 20f);
		this.s_programmieren = UnityEngine.Random.Range(10f, 20f);
		this.s_grafik = UnityEngine.Random.Range(10f, 20f);
		this.s_sound = UnityEngine.Random.Range(10f, 20f);
		this.s_pr = UnityEngine.Random.Range(10f, 20f);
		this.s_gametests = UnityEngine.Random.Range(10f, 20f);
		this.s_technik = UnityEngine.Random.Range(10f, 20f);
		this.s_forschen = UnityEngine.Random.Range(10f, 20f);
		float num = 0f;
		if (!this.mS_.multiplayer)
		{
			num = (float)(this.mS_.GetStudioLevel(this.mS_.studioPoints) * 3);
		}
		if (!task_)
		{
			switch (UnityEngine.Random.Range(0, 8))
			{
			case 0:
				this.s_gamedesign = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 0;
				break;
			case 1:
				this.s_programmieren = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 1;
				break;
			case 2:
				this.s_grafik = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 2;
				break;
			case 3:
				this.s_sound = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 3;
				break;
			case 4:
				this.s_pr = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 4;
				break;
			case 5:
				this.s_gametests = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 5;
				break;
			case 6:
				this.s_technik = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 6;
				break;
			case 7:
				this.s_forschen = UnityEngine.Random.Range(30f, 40f + num);
				this.beruf = 7;
				break;
			}
		}
		else
		{
			float num2 = UnityEngine.Random.Range(30f, 35f);
			switch (task_.berufserfahrung)
			{
			case 0:
				num2 = UnityEngine.Random.Range(30f, 35f);
				break;
			case 1:
				num2 = UnityEngine.Random.Range(50f, 55f);
				break;
			case 2:
				num2 = UnityEngine.Random.Range(70f, 75f);
				break;
			}
			switch (task_.beruf)
			{
			case 0:
				this.s_gamedesign = num2;
				this.beruf = 0;
				break;
			case 1:
				this.s_programmieren = num2;
				this.beruf = 1;
				break;
			case 2:
				this.s_grafik = num2;
				this.beruf = 2;
				break;
			case 3:
				this.s_sound = num2;
				this.beruf = 3;
				break;
			case 4:
				this.s_pr = num2;
				this.beruf = 4;
				break;
			case 5:
				this.s_gametests = num2;
				this.beruf = 5;
				break;
			case 6:
				this.s_technik = num2;
				this.beruf = 6;
				break;
			case 7:
				this.s_forschen = num2;
				this.beruf = 7;
				break;
			}
		}
		int num3 = 0;
		if (this.mS_.year > 1976 && !task_ && (UnityEngine.Random.Range(0, 50) == 1 || (this.mS_.globalEvent == 5 && UnityEngine.Random.Range(0, 25) == 1)))
		{
			int devLegend = this.tS_.GetDevLegend();
			if (devLegend != -1)
			{
				this.legend = devLegend;
				this.mS_.devLegendsInUse[devLegend] = true;
				this.myName = this.tS_.devLegends[devLegend];
				this.male = true;
				if (this.mS_.devLegendsFemale[devLegend])
				{
					this.male = false;
				}
				this.s_gamedesign = UnityEngine.Random.Range(10f, 20f);
				this.s_programmieren = UnityEngine.Random.Range(10f, 20f);
				this.s_grafik = UnityEngine.Random.Range(10f, 20f);
				this.s_sound = UnityEngine.Random.Range(10f, 20f);
				this.s_pr = UnityEngine.Random.Range(10f, 20f);
				this.s_gametests = UnityEngine.Random.Range(10f, 20f);
				this.s_technik = UnityEngine.Random.Range(10f, 20f);
				this.s_forschen = UnityEngine.Random.Range(10f, 20f);
				if (this.mS_.devLegendsDesigner.Length >= 0)
				{
					if (this.mS_.devLegendsDesigner[devLegend])
					{
						this.s_gamedesign = UnityEngine.Random.Range(80f, 95f);
						this.beruf = 0;
					}
					if (this.mS_.devLegendsProgrammierer[devLegend])
					{
						this.s_programmieren = UnityEngine.Random.Range(80f, 95f);
						this.beruf = 1;
					}
					if (this.mS_.devLegendsGrafiker[devLegend])
					{
						this.s_grafik = UnityEngine.Random.Range(80f, 95f);
						this.beruf = 2;
					}
					if (this.mS_.devLegendsMusiker[devLegend])
					{
						this.s_sound = UnityEngine.Random.Range(80f, 95f);
						this.beruf = 3;
					}
					if (this.mS_.devLegendsForscher[devLegend])
					{
						this.s_forschen = UnityEngine.Random.Range(80f, 95f);
						this.beruf = 7;
					}
					if (this.mS_.devLegendsHardware[devLegend])
					{
						this.s_technik = UnityEngine.Random.Range(80f, 95f);
						this.beruf = 6;
					}
				}
				else
				{
					this.s_gamedesign = UnityEngine.Random.Range(80f, 95f);
					this.beruf = 0;
				}
				this.perks[1] = true;
				this.tS_.GetText(427);
				this.guiMain_.CreateTopNewsDevLegend(this.myName, this.beruf);
				num3++;
			}
		}
		for (int i = 0; i < 20; i++)
		{
			int num4 = UnityEngine.Random.Range(0, this.perks.Length);
			if (num4 != 0 && num4 != 1 && this.guiMain_.uiPerks[num4] && UnityEngine.Random.Range(0, 5) == 1 && num3 < 4)
			{
				this.perks[num4] = true;
				num3++;
				if (14 == num4 && this.beruf != 0)
				{
					this.perks[14] = false;
				}
				if (3 == num4 && this.beruf > 1)
				{
					this.perks[3] = false;
				}
				if (21 == num4 && this.beruf != 1)
				{
					this.perks[21] = false;
				}
				if (23 == num4 && this.beruf != 2)
				{
					this.perks[23] = false;
				}
				if (24 == num4 && this.beruf != 1)
				{
					this.perks[24] = false;
				}
				if (25 == num4 && this.beruf != 0)
				{
					this.perks[25] = false;
				}
				if (26 == num4 && this.beruf != 1)
				{
					this.perks[26] = false;
				}
				if (num4 == 10)
				{
					this.perks[19] = false;
				}
				if (num4 == 19)
				{
					this.perks[10] = false;
				}
				if (num4 == 3)
				{
					this.perks[21] = false;
				}
				if (num4 == 21)
				{
					this.perks[3] = false;
				}
				if (num4 == 2)
				{
					this.perks[20] = false;
				}
				if (num4 == 20)
				{
					this.perks[2] = false;
				}
				if (num4 == 27)
				{
					this.perks[6] = false;
				}
				if (num4 == 6)
				{
					this.perks[27] = false;
				}
				if (num4 == 5)
				{
					this.perks[22] = false;
				}
				if (num4 == 22)
				{
					this.perks[5] = false;
				}
				if (this.perks[1])
				{
					if (this.perks[18])
					{
						this.perks[18] = false;
					}
					if (this.perks[19])
					{
						this.perks[19] = false;
					}
					if (this.perks[20])
					{
						this.perks[20] = false;
					}
					if (this.perks[21])
					{
						this.perks[21] = false;
					}
					if (this.perks[22])
					{
						this.perks[22] = false;
					}
					if (this.perks[27])
					{
						this.perks[27] = false;
					}
				}
			}
		}
		int num5 = 0;
		if (this.male)
		{
			this.model_body = UnityEngine.Random.Range(1, this.cCS_.charGfxMales.Length);
			if (UnityEngine.Random.Range(0, 100) < 20)
			{
				num5 = UnityEngine.Random.Range(1, this.clothScript_.prefabMaleEyes.Length);
			}
			this.model_eyes = num5;
		}
		else
		{
			this.model_body = UnityEngine.Random.Range(1, this.cCS_.charGfxFemales.Length);
			if (UnityEngine.Random.Range(0, 100) < 20)
			{
				num5 = UnityEngine.Random.Range(1, this.clothScript_.prefabFemaleEyes.Length);
			}
			this.model_eyes = num5;
		}
		if (this.male)
		{
			this.model_hair = -1;
			if (UnityEngine.Random.Range(0, 100) > 10)
			{
				num5 = UnityEngine.Random.Range(0, this.clothScript_.prefabMaleHairs.Length);
				this.model_hair = num5;
			}
		}
		else
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.prefabFemaleHairs.Length);
			this.model_hair = num5;
		}
		this.model_beard = -1;
		if (this.male && UnityEngine.Random.Range(0, 100) < 33)
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.prefabBeards.Length);
			this.model_beard = num5;
		}
		if (UnityEngine.Random.Range(0, 100) < 60)
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.matColor_Skin.Length);
			this.model_skinColor = num5;
		}
		else
		{
			this.model_skinColor = 0;
		}
		if (this.male)
		{
			int num6 = UnityEngine.Random.Range(0, this.clothScript_.matColor_MaleHair.Length);
			this.model_hairColor = num6;
			this.model_beardColor = num6;
		}
		else
		{
			int num7 = UnityEngine.Random.Range(0, this.clothScript_.matColor_FemaleHair.Length);
			this.model_hairColor = num7;
			this.model_beardColor = num7;
		}
		if (this.male)
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.matColor_MaleHose.Length);
			this.model_HoseColor = num5;
		}
		else
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.matColor_FemaleHose.Length);
			this.model_HoseColor = num5;
		}
		if (this.male)
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.matColor_MaleShirt.Length);
			this.model_ShirtColor = num5;
		}
		else
		{
			num5 = UnityEngine.Random.Range(0, this.clothScript_.matColor_FemaleShirt.Length);
			this.model_ShirtColor = num5;
		}
		num5 = UnityEngine.Random.Range(0, this.clothScript_.matColor_AllColors.Length);
		this.model_Add1Color = num5;
		if (!task_ && this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.mS_.mpCalls_.SERVER_Send_CreateArbeitsmarkt(this);
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000BE04 File Offset: 0x0000A004
	public void RemoveFromArbeitsmarkt(bool eingestellt)
	{
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_DeleteArbeitsmarkt(this.myID, eingestellt);
			}
			else if (!this.mS_.mpCalls_.disableSend)
			{
				this.mS_.mpCalls_.CLIENT_Send_DeleteArbeitsmarkt(this.myID, eingestellt);
			}
		}
		if (!eingestellt && this.legend != -1)
		{
			this.mS_.devLegendsInUse[this.legend] = false;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000BE9C File Offset: 0x0000A09C
	public int GetGehalt()
	{
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
							goto IL_10A;
						case 15:
							num += 2000;
							goto IL_10A;
						case 18:
							num -= 500;
							goto IL_10A;
						case 19:
							num -= 500;
							goto IL_10A;
						case 20:
							num -= 500;
							goto IL_10A;
						case 21:
							num -= 500;
							goto IL_10A;
						case 22:
							num -= 500;
							goto IL_10A;
						case 27:
							num -= 500;
							goto IL_10A;
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
			IL_10A:;
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

	// Token: 0x060000EB RID: 235 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
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

	// Token: 0x060000EC RID: 236 RVA: 0x0000C280 File Offset: 0x0000A480
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

	// Token: 0x04000265 RID: 613
	public GameObject main_;

	// Token: 0x04000266 RID: 614
	public mainScript mS_;

	// Token: 0x04000267 RID: 615
	public textScript tS_;

	// Token: 0x04000268 RID: 616
	public GUI_Main guiMain_;

	// Token: 0x04000269 RID: 617
	public clothScript clothScript_;

	// Token: 0x0400026A RID: 618
	public createCharScript cCS_;

	// Token: 0x0400026B RID: 619
	public int myID;

	// Token: 0x0400026C RID: 620
	public bool male;

	// Token: 0x0400026D RID: 621
	public string myName;

	// Token: 0x0400026E RID: 622
	public int wochenAmArbeitsmarkt;

	// Token: 0x0400026F RID: 623
	public int legend = -1;

	// Token: 0x04000270 RID: 624
	public int beruf;

	// Token: 0x04000271 RID: 625
	public float s_gamedesign;

	// Token: 0x04000272 RID: 626
	public float s_programmieren;

	// Token: 0x04000273 RID: 627
	public float s_grafik;

	// Token: 0x04000274 RID: 628
	public float s_sound;

	// Token: 0x04000275 RID: 629
	public float s_pr;

	// Token: 0x04000276 RID: 630
	public float s_gametests;

	// Token: 0x04000277 RID: 631
	public float s_technik;

	// Token: 0x04000278 RID: 632
	public float s_forschen;

	// Token: 0x04000279 RID: 633
	public bool[] perks;

	// Token: 0x0400027A RID: 634
	public int model_body;

	// Token: 0x0400027B RID: 635
	public int model_eyes;

	// Token: 0x0400027C RID: 636
	public int model_hair;

	// Token: 0x0400027D RID: 637
	public int model_beard;

	// Token: 0x0400027E RID: 638
	public int model_skinColor;

	// Token: 0x0400027F RID: 639
	public int model_hairColor;

	// Token: 0x04000280 RID: 640
	public int model_beardColor;

	// Token: 0x04000281 RID: 641
	public int model_HoseColor;

	// Token: 0x04000282 RID: 642
	public int model_ShirtColor;

	// Token: 0x04000283 RID: 643
	public int model_Add1Color;
}
