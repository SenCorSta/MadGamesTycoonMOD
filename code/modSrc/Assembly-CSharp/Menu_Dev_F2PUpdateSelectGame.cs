using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000134 RID: 308
public class Menu_Dev_F2PUpdateSelectGame : MonoBehaviour
{
	// Token: 0x06000B05 RID: 2821 RVA: 0x0007753B File Offset: 0x0007573B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00077544 File Offset: 0x00075744
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

	// Token: 0x06000B07 RID: 2823 RVA: 0x0007760C File Offset: 0x0007580C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00077644 File Offset: 0x00075844
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

	// Token: 0x06000B09 RID: 2825 RVA: 0x00077690 File Offset: 0x00075890
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_f2PUpdate>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x000776EC File Offset: 0x000758EC
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x000776FC File Offset: 0x000758FC
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(697));
		list.Add(this.tS_.GetText(1392));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x000777E0 File Offset: 0x000759E0
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

	// Token: 0x06000B0D RID: 2829 RVA: 0x0007783C File Offset: 0x00075A3C
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
						Item_DevGame_f2PUpdate component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_f2PUpdate>();
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

	// Token: 0x06000B0E RID: 2830 RVA: 0x000779B0 File Offset: 0x00075BB0
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.developerID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp == 2 && !script_.pubOffer && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00077A28 File Offset: 0x00075C28
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
				Item_DevGame_f2PUpdate component = gameObject.GetComponent<Item_DevGame_f2PUpdate>();
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
					gameObject.name = component.game_.sellsTotalOnline.ToString();
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

	// Token: 0x06000B10 RID: 2832 RVA: 0x00077BB0 File Offset: 0x00075DB0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[104]);
		this.guiMain_.uiObjects[104].GetComponent<Menu_Dev_Addon>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00077C10 File Offset: 0x00075E10
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

	// Token: 0x04000F5B RID: 3931
	public GameObject[] uiPrefabs;

	// Token: 0x04000F5C RID: 3932
	public GameObject[] uiObjects;

	// Token: 0x04000F5D RID: 3933
	private mainScript mS_;

	// Token: 0x04000F5E RID: 3934
	private GameObject main_;

	// Token: 0x04000F5F RID: 3935
	private GUI_Main guiMain_;

	// Token: 0x04000F60 RID: 3936
	private sfxScript sfx_;

	// Token: 0x04000F61 RID: 3937
	private textScript tS_;

	// Token: 0x04000F62 RID: 3938
	private genres genres_;

	// Token: 0x04000F63 RID: 3939
	public roomScript rS_;

	// Token: 0x04000F64 RID: 3940
	private float updateTimer;

	// Token: 0x04000F65 RID: 3941
	private string searchStringA = "";
}
