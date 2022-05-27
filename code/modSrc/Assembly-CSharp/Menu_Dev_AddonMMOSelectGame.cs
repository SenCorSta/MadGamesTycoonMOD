using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000128 RID: 296
public class Menu_Dev_AddonMMOSelectGame : MonoBehaviour
{
	// Token: 0x06000A73 RID: 2675 RVA: 0x00071E07 File Offset: 0x00070007
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00071E10 File Offset: 0x00070010
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

	// Token: 0x06000A75 RID: 2677 RVA: 0x00071ED8 File Offset: 0x000700D8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00071F10 File Offset: 0x00070110
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

	// Token: 0x06000A77 RID: 2679 RVA: 0x00071F5C File Offset: 0x0007015C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_MMOAddon>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x00071FB8 File Offset: 0x000701B8
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00071FC8 File Offset: 0x000701C8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1236));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x000720AC File Offset: 0x000702AC
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

	// Token: 0x06000A7B RID: 2683 RVA: 0x00072108 File Offset: 0x00070308
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
						Item_DevGame_MMOAddon component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_MMOAddon>();
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

	// Token: 0x06000A7C RID: 2684 RVA: 0x0007227C File Offset: 0x0007047C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.developerID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp == 1 && !script_.pubOffer && !script_.handy && !script_.arcade && !script_.typ_goty && !script_.typ_budget && !script_.typ_bundle && !script_.typ_bundleAddon && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff);
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00072328 File Offset: 0x00070528
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
				Item_DevGame_MMOAddon component = gameObject.GetComponent<Item_DevGame_MMOAddon>();
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
					gameObject.name = component.game_.abonnements.ToString();
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

	// Token: 0x06000A7E RID: 2686 RVA: 0x000724B0 File Offset: 0x000706B0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[104]);
		this.guiMain_.uiObjects[104].GetComponent<Menu_Dev_Addon>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00072510 File Offset: 0x00070710
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

	// Token: 0x04000EAB RID: 3755
	public GameObject[] uiPrefabs;

	// Token: 0x04000EAC RID: 3756
	public GameObject[] uiObjects;

	// Token: 0x04000EAD RID: 3757
	private mainScript mS_;

	// Token: 0x04000EAE RID: 3758
	private GameObject main_;

	// Token: 0x04000EAF RID: 3759
	private GUI_Main guiMain_;

	// Token: 0x04000EB0 RID: 3760
	private sfxScript sfx_;

	// Token: 0x04000EB1 RID: 3761
	private textScript tS_;

	// Token: 0x04000EB2 RID: 3762
	private genres genres_;

	// Token: 0x04000EB3 RID: 3763
	public roomScript rS_;

	// Token: 0x04000EB4 RID: 3764
	private float updateTimer;

	// Token: 0x04000EB5 RID: 3765
	private string searchStringA = "";
}
