using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E0 RID: 480
public class Menu_Mitarbeitersuche : MonoBehaviour
{
	// Token: 0x06001229 RID: 4649 RVA: 0x000C0917 File Offset: 0x000BEB17
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x000C0920 File Offset: 0x000BEB20
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
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x000C0A08 File Offset: 0x000BEC08
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[0].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(137));
		list.Add(this.tS_.GetText(138));
		list.Add(this.tS_.GetText(139));
		list.Add(this.tS_.GetText(140));
		list.Add(this.tS_.GetText(141));
		list.Add(this.tS_.GetText(142));
		list.Add(this.tS_.GetText(143));
		list.Add(this.tS_.GetText(144));
		this.uiObjects[0].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[0].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[0].GetComponent<Dropdown>().value = @int;
		@int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		list = new List<string>();
		list.Add("<b>[30-35]</b> " + this.tS_.GetText(1710));
		list.Add("<b>[50-55]</b> " + this.tS_.GetText(1711));
		list.Add("<b>[70-75]</b> " + this.tS_.GetText(1712));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000C0BCD File Offset: 0x000BEDCD
	public void Init(roomScript room_)
	{
		this.rS_ = room_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000C0BE8 File Offset: 0x000BEDE8
	private void SetData()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		string text = this.tS_.GetText(1716);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.price[value], true));
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.GetChance(value)).ToString();
		this.uiObjects[5].GetComponent<Image>().fillAmount = this.GetChance(value) * 0.01f;
		this.uiObjects[5].GetComponent<Image>().color = this.GetValColor(this.GetChance(value));
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.workPoints[value]).ToString();
		this.uiObjects[6].GetComponent<Image>().fillAmount = this.workPoints[value] * 0.01f;
		this.uiObjects[6].GetComponent<Image>().color = this.GetValColor(this.workPoints[value]);
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x000C0D18 File Offset: 0x000BEF18
	public float GetChance(int i)
	{
		this.FindScripts();
		float num = this.chance[i];
		num += (float)this.mS_.GetStudioLevel(this.mS_.studioPoints) * 1.5f;
		num += (float)(this.mS_.year - 1976) * 0.3f;
		num -= (float)this.mS_.difficulty;
		if (num < 1f)
		{
			num = 1f;
		}
		if (num > 100f)
		{
			num = 100f;
		}
		return num;
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x000C0D9C File Offset: 0x000BEF9C
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

	// Token: 0x06001230 RID: 4656 RVA: 0x000C0E10 File Offset: 0x000BF010
	public void DROPDOWN_Erfahrung()
	{
		PlayerPrefs.SetInt(this.uiObjects[0].name, this.uiObjects[0].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt(this.uiObjects[1].name, this.uiObjects[1].GetComponent<Dropdown>().value);
		this.SetData();
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x000C0E6C File Offset: 0x000BF06C
	public void DROPDOWN_Profession()
	{
		PlayerPrefs.SetInt(this.uiObjects[0].name, this.uiObjects[0].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt(this.uiObjects[1].name, this.uiObjects[1].GetComponent<Dropdown>().value);
		this.SetData();
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000C0EC7 File Offset: 0x000BF0C7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x000C0EF0 File Offset: 0x000BF0F0
	public void BUTTON_OK()
	{
		if (!this.rS_)
		{
			return;
		}
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		if (this.mS_.NotEnoughMoney(this.price[value]))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.price[value], 24);
		taskMitarbeitersuche taskMitarbeitersuche = this.guiMain_.AddTask_Mitarbeitersuche();
		taskMitarbeitersuche.Init(false);
		taskMitarbeitersuche.beruf = this.uiObjects[0].GetComponent<Dropdown>().value;
		taskMitarbeitersuche.automatic = this.uiObjects[7].GetComponent<Toggle>().isOn;
		taskMitarbeitersuche.points = this.workPoints[this.uiObjects[1].GetComponent<Dropdown>().value];
		taskMitarbeitersuche.pointsLeft = this.workPoints[this.uiObjects[1].GetComponent<Dropdown>().value];
		taskMitarbeitersuche.berufserfahrung = this.uiObjects[1].GetComponent<Dropdown>().value;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskMitarbeitersuche.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001698 RID: 5784
	private mainScript mS_;

	// Token: 0x04001699 RID: 5785
	private GameObject main_;

	// Token: 0x0400169A RID: 5786
	private GUI_Main guiMain_;

	// Token: 0x0400169B RID: 5787
	private sfxScript sfx_;

	// Token: 0x0400169C RID: 5788
	private textScript tS_;

	// Token: 0x0400169D RID: 5789
	private pickCharacterScript pcS_;

	// Token: 0x0400169E RID: 5790
	private roomDataScript rdS_;

	// Token: 0x0400169F RID: 5791
	private roomScript rS_;

	// Token: 0x040016A0 RID: 5792
	public GameObject[] uiPrefabs;

	// Token: 0x040016A1 RID: 5793
	public GameObject[] uiObjects;

	// Token: 0x040016A2 RID: 5794
	public int[] price;

	// Token: 0x040016A3 RID: 5795
	public float[] chance;

	// Token: 0x040016A4 RID: 5796
	public float[] workPoints;
}
