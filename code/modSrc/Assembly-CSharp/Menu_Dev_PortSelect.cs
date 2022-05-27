using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000141 RID: 321
public class Menu_Dev_PortSelect : MonoBehaviour
{
	// Token: 0x06000BAE RID: 2990 RVA: 0x00008443 File Offset: 0x00006643
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0008E794 File Offset: 0x0008C994
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

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0000844B File Offset: 0x0000664B
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0008E85C File Offset: 0x0008CA5C
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

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0008E8A8 File Offset: 0x0008CAA8
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

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00008483 File Offset: 0x00006683
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0008E904 File Offset: 0x0008CB04
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

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0008E9FC File Offset: 0x0008CBFC
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

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0008EA58 File Offset: 0x0008CC58
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

	// Token: 0x06000BB7 RID: 2999 RVA: 0x0008EBCC File Offset: 0x0008CDCC
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && script_.playerGame && script_.portID == -1)
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

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0008ECAC File Offset: 0x0008CEAC
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

	// Token: 0x06000BB9 RID: 3001 RVA: 0x00008491 File Offset: 0x00006691
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0008EE7C File Offset: 0x0008D07C
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

	// Token: 0x04001000 RID: 4096
	public GameObject[] uiPrefabs;

	// Token: 0x04001001 RID: 4097
	public GameObject[] uiObjects;

	// Token: 0x04001002 RID: 4098
	private mainScript mS_;

	// Token: 0x04001003 RID: 4099
	private GameObject main_;

	// Token: 0x04001004 RID: 4100
	private GUI_Main guiMain_;

	// Token: 0x04001005 RID: 4101
	private sfxScript sfx_;

	// Token: 0x04001006 RID: 4102
	private textScript tS_;

	// Token: 0x04001007 RID: 4103
	private genres genres_;

	// Token: 0x04001008 RID: 4104
	public roomScript rS_;

	// Token: 0x04001009 RID: 4105
	private float updateTimer;

	// Token: 0x0400100A RID: 4106
	private string searchStringA = "";
}
