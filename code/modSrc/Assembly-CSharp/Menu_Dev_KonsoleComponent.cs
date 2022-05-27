using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015E RID: 350
public class Menu_Dev_KonsoleComponent : MonoBehaviour
{
	// Token: 0x06000D04 RID: 3332 RVA: 0x000091AC File Offset: 0x000073AC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0009E348 File Offset: 0x0009C548
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>();
		}
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x000091B4 File Offset: 0x000073B4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0009E458 File Offset: 0x0009C658
	public void Init(int compTyp_, int platTyp_)
	{
		this.typ = compTyp_;
		this.platformTyp = platTyp_;
		this.FindScripts();
		this.InitDropdowns();
		switch (this.typ)
		{
		case 0:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1588);
			break;
		case 1:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1590);
			break;
		case 2:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1589);
			break;
		case 3:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1592);
			break;
		case 4:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1591);
			break;
		case 5:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1593);
			break;
		case 6:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1594);
			break;
		case 7:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1597);
			break;
		case 8:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1598);
			break;
		case 9:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1595);
			break;
		}
		this.CreateItems(compTyp_);
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0009E65C File Offset: 0x0009C85C
	private void CreateItems(int typ_)
	{
		for (int i = 0; i < this.hardware_.hardware_UNLOCK.Length; i++)
		{
			if (this.hardware_.hardware_UNLOCK[i] && this.hardware_.hardware_RES_POINTS_LEFT[i] <= 0f && this.hardware_.hardware_TYP[i] == typ_ && ((this.platformTyp == 1 && !this.hardware_.hardware_ONLYHANDHELD[i]) || (this.platformTyp == 2 && !this.hardware_.hardware_ONLYSTATIONARY[i])))
			{
				Item_DevKonsole_Componente component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevKonsole_Componente>();
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.hardware_ = this.hardware_;
			}
		}
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0009E770 File Offset: 0x0009C970
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1604));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(4));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0009E808 File Offset: 0x0009CA08
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[5].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[5].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevKonsole_Componente component = gameObject.GetComponent<Item_DevKonsole_Componente>();
				switch (value)
				{
				case 0:
					gameObject.name = this.hardware_.GetPerformance(component.myID).ToString();
					break;
				case 1:
					gameObject.name = this.hardware_.GetDevCosts(component.myID).ToString();
					break;
				case 2:
					gameObject.name = this.hardware_.hardware_TECH[component.myID].ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x000091E6 File Offset: 0x000073E6
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400119B RID: 4507
	private mainScript mS_;

	// Token: 0x0400119C RID: 4508
	private GameObject main_;

	// Token: 0x0400119D RID: 4509
	private GUI_Main guiMain_;

	// Token: 0x0400119E RID: 4510
	private sfxScript sfx_;

	// Token: 0x0400119F RID: 4511
	private textScript tS_;

	// Token: 0x040011A0 RID: 4512
	private engineFeatures eF_;

	// Token: 0x040011A1 RID: 4513
	private Menu_Dev_Konsole menu_;

	// Token: 0x040011A2 RID: 4514
	private hardware hardware_;

	// Token: 0x040011A3 RID: 4515
	public GameObject[] uiPrefabs;

	// Token: 0x040011A4 RID: 4516
	public GameObject[] uiObjects;

	// Token: 0x040011A5 RID: 4517
	public int typ;

	// Token: 0x040011A6 RID: 4518
	public int platformTyp;
}
