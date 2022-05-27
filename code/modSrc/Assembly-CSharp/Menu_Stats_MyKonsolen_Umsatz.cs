using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000251 RID: 593
public class Menu_Stats_MyKonsolen_Umsatz : MonoBehaviour
{
	// Token: 0x0600170A RID: 5898 RVA: 0x000E6BF2 File Offset: 0x000E4DF2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x000E6BFC File Offset: 0x000E4DFC
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
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x000E6CC4 File Offset: 0x000E4EC4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x000E6CFC File Offset: 0x000E4EFC
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

	// Token: 0x0600170E RID: 5902 RVA: 0x000E6D48 File Offset: 0x000E4F48
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyKonsolen_Umsatz>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000E6D8C File Offset: 0x000E4F8C
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000E6D94 File Offset: 0x000E4F94
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x000E6DA8 File Offset: 0x000E4FA8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[4].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(489));
		list.Add(this.tS_.GetText(1238));
		list.Add(this.tS_.GetText(6));
		this.uiObjects[4].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[4].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[4].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x000E6E4C File Offset: 0x000E504C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.ownerID == this.mS_.myID && component.isUnlocked && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MyKonsolen_Umsatz component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyKonsolen_Umsatz>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.pS_ = component;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1662);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x000E6FC0 File Offset: 0x000E51C0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x000E6FDC File Offset: 0x000E51DC
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[4].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[4].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_MyKonsolen_Umsatz component = gameObject.GetComponent<Item_MyKonsolen_Umsatz>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetGesamtGewinn().ToString();
					break;
				case 1:
					gameObject.name = component.pS_.umsatzTotal.ToString();
					break;
				case 2:
					gameObject.name = component.pS_.GetEntwicklungskosten().ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x04001AC7 RID: 6855
	private mainScript mS_;

	// Token: 0x04001AC8 RID: 6856
	private GameObject main_;

	// Token: 0x04001AC9 RID: 6857
	private GUI_Main guiMain_;

	// Token: 0x04001ACA RID: 6858
	private sfxScript sfx_;

	// Token: 0x04001ACB RID: 6859
	private textScript tS_;

	// Token: 0x04001ACC RID: 6860
	private genres genres_;

	// Token: 0x04001ACD RID: 6861
	public GameObject[] uiPrefabs;

	// Token: 0x04001ACE RID: 6862
	public GameObject[] uiObjects;

	// Token: 0x04001ACF RID: 6863
	private float updateTimer;
}
