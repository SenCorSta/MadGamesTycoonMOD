using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024C RID: 588
public class Menu_Stats_MyIPs : MonoBehaviour
{
	// Token: 0x060016D0 RID: 5840 RVA: 0x000E5902 File Offset: 0x000E3B02
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000E590C File Offset: 0x000E3B0C
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

	// Token: 0x060016D2 RID: 5842 RVA: 0x000E59D4 File Offset: 0x000E3BD4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x000E5A0C File Offset: 0x000E3C0C
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

	// Token: 0x060016D4 RID: 5844 RVA: 0x000E5A58 File Offset: 0x000E3C58
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_MyIPs>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x000E5AB4 File Offset: 0x000E3CB4
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x000E5AC4 File Offset: 0x000E3CC4
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		list.Add(this.tS_.GetText(1846));
		list.Add(this.tS_.GetText(1898));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000E5B96 File Offset: 0x000E3D96
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000E5BAC File Offset: 0x000E3DAC
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
				{
					string text = component.GetIpName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_MyGames_MyIPs component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_MyIPs>();
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.game_ = component;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x000E5D00 File Offset: 0x000E3F00
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.mainIP == script_.myID;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000E5D2E File Offset: 0x000E3F2E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000E5D4C File Offset: 0x000E3F4C
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
				Item_MyGames_MyIPs component = gameObject.GetComponent<Item_MyGames_MyIPs>();
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
				case 4:
					gameObject.name = component.game_.ipTime.ToString();
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

	// Token: 0x060016DC RID: 5852 RVA: 0x000E5ED0 File Offset: 0x000E40D0
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
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.Init();
	}

	// Token: 0x04001A9C RID: 6812
	private mainScript mS_;

	// Token: 0x04001A9D RID: 6813
	private GameObject main_;

	// Token: 0x04001A9E RID: 6814
	private GUI_Main guiMain_;

	// Token: 0x04001A9F RID: 6815
	private sfxScript sfx_;

	// Token: 0x04001AA0 RID: 6816
	private textScript tS_;

	// Token: 0x04001AA1 RID: 6817
	private genres genres_;

	// Token: 0x04001AA2 RID: 6818
	public GameObject[] uiPrefabs;

	// Token: 0x04001AA3 RID: 6819
	public GameObject[] uiObjects;

	// Token: 0x04001AA4 RID: 6820
	private float updateTimer;

	// Token: 0x04001AA5 RID: 6821
	private string searchStringA = "";
}
