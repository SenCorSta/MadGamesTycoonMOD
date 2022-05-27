using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000129 RID: 297
public class Menu_Dev_AddonSelectGame : MonoBehaviour
{
	// Token: 0x06000A81 RID: 2689 RVA: 0x0007259D File Offset: 0x0007079D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x000725A8 File Offset: 0x000707A8
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

	// Token: 0x06000A83 RID: 2691 RVA: 0x00072670 File Offset: 0x00070870
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x000726A8 File Offset: 0x000708A8
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

	// Token: 0x06000A85 RID: 2693 RVA: 0x000726F4 File Offset: 0x000708F4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_Addon>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00072750 File Offset: 0x00070950
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00072760 File Offset: 0x00070960
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0007282C File Offset: 0x00070A2C
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

	// Token: 0x06000A89 RID: 2697 RVA: 0x00072888 File Offset: 0x00070A88
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
						Item_DevGame_Addon component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Addon>();
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

	// Token: 0x06000A8A RID: 2698 RVA: 0x000729FC File Offset: 0x00070BFC
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.developerID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp == 0 && !script_.handy && !script_.arcade && !script_.typ_goty && !script_.typ_budget && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.pubOffer && (script_.typ_standard || script_.typ_nachfolger || script_.typ_remaster || script_.typ_spinoff);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x00072AB0 File Offset: 0x00070CB0
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
				Item_DevGame_Addon component = gameObject.GetComponent<Item_DevGame_Addon>();
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

	// Token: 0x06000A8C RID: 2700 RVA: 0x00072C1C File Offset: 0x00070E1C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[104]);
		this.guiMain_.uiObjects[104].GetComponent<Menu_Dev_Addon>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00072C7C File Offset: 0x00070E7C
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

	// Token: 0x04000EB6 RID: 3766
	public GameObject[] uiPrefabs;

	// Token: 0x04000EB7 RID: 3767
	public GameObject[] uiObjects;

	// Token: 0x04000EB8 RID: 3768
	private mainScript mS_;

	// Token: 0x04000EB9 RID: 3769
	private GameObject main_;

	// Token: 0x04000EBA RID: 3770
	private GUI_Main guiMain_;

	// Token: 0x04000EBB RID: 3771
	private sfxScript sfx_;

	// Token: 0x04000EBC RID: 3772
	private textScript tS_;

	// Token: 0x04000EBD RID: 3773
	private genres genres_;

	// Token: 0x04000EBE RID: 3774
	public roomScript rS_;

	// Token: 0x04000EBF RID: 3775
	private float updateTimer;

	// Token: 0x04000EC0 RID: 3776
	private string searchStringA = "";
}
