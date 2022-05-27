using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000142 RID: 322
public class Menu_Dev_PortSelect : MonoBehaviour
{
	// Token: 0x06000BC2 RID: 3010 RVA: 0x0007ECF2 File Offset: 0x0007CEF2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0007ECFC File Offset: 0x0007CEFC
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

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0007EDC4 File Offset: 0x0007CFC4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0007EDFC File Offset: 0x0007CFFC
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

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0007EE48 File Offset: 0x0007D048
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Port>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0007EEA4 File Offset: 0x0007D0A4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x0007EEB4 File Offset: 0x0007D0B4
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1484));
		list.Add(this.tS_.GetText(1555));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0007EFAC File Offset: 0x0007D1AC
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x0007F008 File Offset: 0x0007D208
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
				{
					string text = component.GetNameSimple();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_DevGame_Port component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Port>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.rS_ = this.rS_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x0007F17C File Offset: 0x0007D37C
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && script_.ownerID == this.mS_.myID && script_.portID == -1)
		{
			int num = 0;
			for (int i = 0; i < script_.portExist.Length; i++)
			{
				if (script_.portExist[i])
				{
					num++;
				}
			}
			if (!this.mS_.unlock_.Get(65))
			{
				num++;
			}
			if (script_.gameTyp == 1 || script_.gameTyp == 2)
			{
				num++;
			}
			if (num < 2 && !script_.typ_contractGame && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_budget && !script_.typ_bundleAddon && !script_.typ_goty && !script_.pubOffer && !script_.auftragsspiel && !script_.f2pConverted)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x0007F268 File Offset: 0x0007D468
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
				Item_DevGame_Port component = gameObject.GetComponent<Item_DevGame_Port>();
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
					if (component.game_.inDevelopment || component.game_.schublade)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 5:
					gameObject.name = component.game_.GetPlatformTypString();
					break;
				case 6:
					gameObject.name = component.game_.GetIpBekanntheit().ToString();
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

	// Token: 0x06000BCD RID: 3021 RVA: 0x0007F436 File Offset: 0x0007D636
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0007F46C File Offset: 0x0007D66C
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
		this.Init(this.rS_);
	}

	// Token: 0x04001008 RID: 4104
	public GameObject[] uiPrefabs;

	// Token: 0x04001009 RID: 4105
	public GameObject[] uiObjects;

	// Token: 0x0400100A RID: 4106
	private mainScript mS_;

	// Token: 0x0400100B RID: 4107
	private GameObject main_;

	// Token: 0x0400100C RID: 4108
	private GUI_Main guiMain_;

	// Token: 0x0400100D RID: 4109
	private sfxScript sfx_;

	// Token: 0x0400100E RID: 4110
	private textScript tS_;

	// Token: 0x0400100F RID: 4111
	private genres genres_;

	// Token: 0x04001010 RID: 4112
	public roomScript rS_;

	// Token: 0x04001011 RID: 4113
	private float updateTimer;

	// Token: 0x04001012 RID: 4114
	private string searchStringA = "";
}
