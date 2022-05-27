using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014A RID: 330
public class Menu_Dev_SpinoffSelect : MonoBehaviour
{
	// Token: 0x06000C14 RID: 3092 RVA: 0x00082076 File Offset: 0x00080276
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00082080 File Offset: 0x00080280
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

	// Token: 0x06000C16 RID: 3094 RVA: 0x00082148 File Offset: 0x00080348
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00082180 File Offset: 0x00080380
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

	// Token: 0x06000C18 RID: 3096 RVA: 0x000821CC File Offset: 0x000803CC
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Spinoff>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00082228 File Offset: 0x00080428
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00082238 File Offset: 0x00080438
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
		list.Add(this.tS_.GetText(1898));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00082330 File Offset: 0x00080530
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

	// Token: 0x06000C1C RID: 3100 RVA: 0x0008238C File Offset: 0x0008058C
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
						Item_DevGame_Spinoff component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Spinoff>();
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

	// Token: 0x06000C1D RID: 3101 RVA: 0x00082500 File Offset: 0x00080700
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.inDevelopment && script_.mainIP == script_.myID && !script_.typ_nachfolger && !script_.pubOffer && !script_.auftragsspiel;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0008255C File Offset: 0x0008075C
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
				Item_DevGame_Spinoff component = gameObject.GetComponent<Item_DevGame_Spinoff>();
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
				case 6:
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

	// Token: 0x06000C1F RID: 3103 RVA: 0x00082705 File Offset: 0x00080905
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0008273C File Offset: 0x0008093C
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

	// Token: 0x04001058 RID: 4184
	public GameObject[] uiPrefabs;

	// Token: 0x04001059 RID: 4185
	public GameObject[] uiObjects;

	// Token: 0x0400105A RID: 4186
	private mainScript mS_;

	// Token: 0x0400105B RID: 4187
	private GameObject main_;

	// Token: 0x0400105C RID: 4188
	private GUI_Main guiMain_;

	// Token: 0x0400105D RID: 4189
	private sfxScript sfx_;

	// Token: 0x0400105E RID: 4190
	private textScript tS_;

	// Token: 0x0400105F RID: 4191
	private genres genres_;

	// Token: 0x04001060 RID: 4192
	public roomScript rS_;

	// Token: 0x04001061 RID: 4193
	private float updateTimer;

	// Token: 0x04001062 RID: 4194
	private string searchStringA = "";
}
