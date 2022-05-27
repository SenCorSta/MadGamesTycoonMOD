using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026E RID: 622
public class Menu_FanshopSelect : MonoBehaviour
{
	// Token: 0x0600182F RID: 6191 RVA: 0x000F0B30 File Offset: 0x000EED30
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x000F0B38 File Offset: 0x000EED38
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

	// Token: 0x06001831 RID: 6193 RVA: 0x000F0C00 File Offset: 0x000EEE00
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000F0C38 File Offset: 0x000EEE38
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

	// Token: 0x06001833 RID: 6195 RVA: 0x000F0C84 File Offset: 0x000EEE84
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_Fanshop>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000F0CC8 File Offset: 0x000EEEC8
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x000F0CD0 File Offset: 0x000EEED0
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		list.Add(this.tS_.GetText(1846));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x000F0D8C File Offset: 0x000EEF8C
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
		this.UpdateAmountAutomatic();
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000F0DA8 File Offset: 0x000EEFA8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Fanshop component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Fanshop>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000F0EB4 File Offset: 0x000EF0B4
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.pubOffer && !script_.auftragsspiel && !script_.typ_contractGame && script_.mainIP == script_.myID;
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000F0F05 File Offset: 0x000EF105
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[368].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000F0F44 File Offset: 0x000EF144
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
				Item_Fanshop component = gameObject.GetComponent<Item_Fanshop>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.ipPunkte.ToString();
					break;
				case 3:
					gameObject.name = component.game_.merchGesamtGewinn.ToString();
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

	// Token: 0x0600183B RID: 6203 RVA: 0x00002715 File Offset: 0x00000915
	public void TOGGLE_AllAuto()
	{
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000F10AC File Offset: 0x000EF2AC
	public void BUTTON_SellAll()
	{
		bool flag = false;
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Fanshop component = gameObject.GetComponent<Item_Fanshop>();
				if (component && component.game_)
				{
					if (i == 0)
					{
						flag = component.game_.merchKeinVerkauf;
						flag = !flag;
					}
					component.game_.merchKeinVerkauf = flag;
				}
			}
		}
		this.UpdateAmountAutomatic();
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x00002715 File Offset: 0x00000915
	public void BUTTON_AllAuto()
	{
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x00002715 File Offset: 0x00000915
	public void UpdateAmountAutomatic()
	{
	}

	// Token: 0x04001BDD RID: 7133
	private mainScript mS_;

	// Token: 0x04001BDE RID: 7134
	private GameObject main_;

	// Token: 0x04001BDF RID: 7135
	private GUI_Main guiMain_;

	// Token: 0x04001BE0 RID: 7136
	private sfxScript sfx_;

	// Token: 0x04001BE1 RID: 7137
	private textScript tS_;

	// Token: 0x04001BE2 RID: 7138
	private genres genres_;

	// Token: 0x04001BE3 RID: 7139
	public GameObject[] uiPrefabs;

	// Token: 0x04001BE4 RID: 7140
	public GameObject[] uiObjects;

	// Token: 0x04001BE5 RID: 7141
	private float updateTimer;
}
