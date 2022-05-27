using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000143 RID: 323
public class Menu_Dev_RemasterSelect : MonoBehaviour
{
	// Token: 0x06000BD0 RID: 3024 RVA: 0x0007F4F9 File Offset: 0x0007D6F9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0007F504 File Offset: 0x0007D704
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0007F5EA File Offset: 0x0007D7EA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0007F624 File Offset: 0x0007D824
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

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0007F670 File Offset: 0x0007D870
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Remaster>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0007F6CC File Offset: 0x0007D8CC
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0007F6DC File Offset: 0x0007D8DC
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

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0007F7C0 File Offset: 0x0007D9C0
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

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0007F81C File Offset: 0x0007DA1C
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
					string text = component.GetNameSimple();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_DevGame_Remaster component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Remaster>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.games_ = this.games_;
						component2.rS_ = this.rS_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0007F98C File Offset: 0x0007DB8C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.inDevelopment && !script_.schublade && script_.gameTyp == 0 && script_.portID == -1 && !script_.pubOffer && !script_.auftragsspiel && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && !script_.remaster_created && !script_.isOnMarket && !script_.typ_budget && !script_.typ_goty && !script_.typ_remaster && !script_.typ_mmoaddon && !script_.typ_bundleAddon && !script_.typ_bundle;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0007FA4C File Offset: 0x0007DC4C
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
				Item_DevGame_Remaster component = gameObject.GetComponent<Item_DevGame_Remaster>();
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

	// Token: 0x06000BDB RID: 3035 RVA: 0x0007FBFF File Offset: 0x0007DDFF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0007FC34 File Offset: 0x0007DE34
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

	// Token: 0x04001013 RID: 4115
	public GameObject[] uiPrefabs;

	// Token: 0x04001014 RID: 4116
	public GameObject[] uiObjects;

	// Token: 0x04001015 RID: 4117
	private mainScript mS_;

	// Token: 0x04001016 RID: 4118
	private GameObject main_;

	// Token: 0x04001017 RID: 4119
	private GUI_Main guiMain_;

	// Token: 0x04001018 RID: 4120
	private sfxScript sfx_;

	// Token: 0x04001019 RID: 4121
	private textScript tS_;

	// Token: 0x0400101A RID: 4122
	private genres genres_;

	// Token: 0x0400101B RID: 4123
	private games games_;

	// Token: 0x0400101C RID: 4124
	public roomScript rS_;

	// Token: 0x0400101D RID: 4125
	private float updateTimer;

	// Token: 0x0400101E RID: 4126
	private string searchStringA = "";
}
