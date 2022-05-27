using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000273 RID: 627
public class Menu_ProductionArcadeSelect : MonoBehaviour
{
	// Token: 0x06001863 RID: 6243 RVA: 0x000F1EFD File Offset: 0x000F00FD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x000F1F08 File Offset: 0x000F0108
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

	// Token: 0x06001865 RID: 6245 RVA: 0x000F1FD0 File Offset: 0x000F01D0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x000F2008 File Offset: 0x000F0208
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

	// Token: 0x06001867 RID: 6247 RVA: 0x000F2054 File Offset: 0x000F0254
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_WerkstattSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x000F20B0 File Offset: 0x000F02B0
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x000F20C0 File Offset: 0x000F02C0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(1511));
		list.Add(this.tS_.GetText(1125));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x000F21A4 File Offset: 0x000F03A4
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

	// Token: 0x0600186B RID: 6251 RVA: 0x000F2200 File Offset: 0x000F0400
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
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_WerkstattSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_WerkstattSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.rS_ = this.rS_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x000F2330 File Offset: 0x000F0530
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.arcade && script_.publisherID == this.mS_.myID && script_.gameTyp != 2;
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x000F2390 File Offset: 0x000F0590
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
				Item_WerkstattSelect component = gameObject.GetComponent<Item_WerkstattSelect>();
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
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 5:
					gameObject.name = component.game_.vorbestellungen.ToString();
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

	// Token: 0x0600186E RID: 6254 RVA: 0x000F2531 File Offset: 0x000F0731
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001C16 RID: 7190
	public GameObject[] uiPrefabs;

	// Token: 0x04001C17 RID: 7191
	public GameObject[] uiObjects;

	// Token: 0x04001C18 RID: 7192
	private mainScript mS_;

	// Token: 0x04001C19 RID: 7193
	private GameObject main_;

	// Token: 0x04001C1A RID: 7194
	private GUI_Main guiMain_;

	// Token: 0x04001C1B RID: 7195
	private sfxScript sfx_;

	// Token: 0x04001C1C RID: 7196
	private textScript tS_;

	// Token: 0x04001C1D RID: 7197
	private genres genres_;

	// Token: 0x04001C1E RID: 7198
	public roomScript rS_;

	// Token: 0x04001C1F RID: 7199
	private float updateTimer;
}
