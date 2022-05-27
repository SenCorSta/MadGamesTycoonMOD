using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013D RID: 317
public class Menu_Dev_NachfolgerSelect : MonoBehaviour
{
	// Token: 0x06000B8D RID: 2957 RVA: 0x0007D5EE File Offset: 0x0007B7EE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0007D5F8 File Offset: 0x0007B7F8
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

	// Token: 0x06000B8F RID: 2959 RVA: 0x0007D6C0 File Offset: 0x0007B8C0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0007D6F8 File Offset: 0x0007B8F8
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

	// Token: 0x06000B91 RID: 2961 RVA: 0x0007D744 File Offset: 0x0007B944
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Nachfolger>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0007D7A0 File Offset: 0x0007B9A0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0007D7B0 File Offset: 0x0007B9B0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1555));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0007D894 File Offset: 0x0007BA94
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

	// Token: 0x06000B95 RID: 2965 RVA: 0x0007D8F0 File Offset: 0x0007BAF0
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		bool isOn = this.uiObjects[4].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && (!isOn || (isOn && !component.isOnMarket)) && this.CheckGameData(component))
				{
					string text = component.GetNameSimple();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_DevGame_Nachfolger component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Nachfolger>();
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

	// Token: 0x06000B96 RID: 2966 RVA: 0x0007DA8C File Offset: 0x0007BC8C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.inDevelopment && !script_.schublade && !script_.typ_budget && !script_.typ_goty && script_.portID == -1 && !script_.pubOffer && !script_.auftragsspiel && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.f2pConverted && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && !script_.nachfolger_created;
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0007DB2C File Offset: 0x0007BD2C
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
				Item_DevGame_Nachfolger component = gameObject.GetComponent<Item_DevGame_Nachfolger>();
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

	// Token: 0x06000B98 RID: 2968 RVA: 0x0007DCDF File Offset: 0x0007BEDF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0007DD14 File Offset: 0x0007BF14
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

	// Token: 0x06000B9A RID: 2970 RVA: 0x0007DD8E File Offset: 0x0007BF8E
	public void TOGGLE_VomMarktGenommen()
	{
		if (this.rS_)
		{
			this.Init(this.rS_);
		}
	}

	// Token: 0x04000FD9 RID: 4057
	public GameObject[] uiPrefabs;

	// Token: 0x04000FDA RID: 4058
	public GameObject[] uiObjects;

	// Token: 0x04000FDB RID: 4059
	private mainScript mS_;

	// Token: 0x04000FDC RID: 4060
	private GameObject main_;

	// Token: 0x04000FDD RID: 4061
	private GUI_Main guiMain_;

	// Token: 0x04000FDE RID: 4062
	private sfxScript sfx_;

	// Token: 0x04000FDF RID: 4063
	private textScript tS_;

	// Token: 0x04000FE0 RID: 4064
	private genres genres_;

	// Token: 0x04000FE1 RID: 4065
	public roomScript rS_;

	// Token: 0x04000FE2 RID: 4066
	private float updateTimer;

	// Token: 0x04000FE3 RID: 4067
	private string searchStringA = "";
}
