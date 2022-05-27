using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AB RID: 427
public class Menu_Marketing_SelectKonsole : MonoBehaviour
{
	// Token: 0x0600101F RID: 4127 RVA: 0x000AAAB8 File Offset: 0x000A8CB8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x000AAAC0 File Offset: 0x000A8CC0
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

	// Token: 0x06001021 RID: 4129 RVA: 0x000AAB88 File Offset: 0x000A8D88
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x000AABC0 File Offset: 0x000A8DC0
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

	// Token: 0x06001023 RID: 4131 RVA: 0x000AAC0C File Offset: 0x000A8E0C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Marketing_Konsole>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x000AAC68 File Offset: 0x000A8E68
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x000AAC70 File Offset: 0x000A8E70
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(433));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x000AAD16 File Offset: 0x000A8F16
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x000AAD24 File Offset: 0x000A8F24
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.ownerID == this.mS_.myID && !component.vomMarktGenommen && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Marketing_Konsole component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Marketing_Konsole>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.pS_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x000AAE37 File Offset: 0x000A9037
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x000AAE54 File Offset: 0x000A9054
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Marketing_Konsole component = gameObject.GetComponent<Item_Marketing_Konsole>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.GetHype().ToString();
					break;
				case 2:
				{
					float num = (float)component.pS_.date_month;
					num /= 13f;
					gameObject.name = component.pS_.date_year.ToString() + num.ToString();
					if (!component.pS_.isUnlocked)
					{
						gameObject.name = "999999";
					}
					break;
				}
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

	// Token: 0x04001499 RID: 5273
	private mainScript mS_;

	// Token: 0x0400149A RID: 5274
	private GameObject main_;

	// Token: 0x0400149B RID: 5275
	private GUI_Main guiMain_;

	// Token: 0x0400149C RID: 5276
	private sfxScript sfx_;

	// Token: 0x0400149D RID: 5277
	private textScript tS_;

	// Token: 0x0400149E RID: 5278
	private genres genres_;

	// Token: 0x0400149F RID: 5279
	public GameObject[] uiPrefabs;

	// Token: 0x040014A0 RID: 5280
	public GameObject[] uiObjects;

	// Token: 0x040014A1 RID: 5281
	private float updateTimer;
}
