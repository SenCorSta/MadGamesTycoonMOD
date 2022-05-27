using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013C RID: 316
public class Menu_Dev_NachfolgerSelect : MonoBehaviour
{
	// Token: 0x06000B7A RID: 2938 RVA: 0x00008245 File Offset: 0x00006445
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0008D298 File Offset: 0x0008B498
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

	// Token: 0x06000B7C RID: 2940 RVA: 0x0000824D File Offset: 0x0000644D
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0008D360 File Offset: 0x0008B560
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

	// Token: 0x06000B7E RID: 2942 RVA: 0x0008D3AC File Offset: 0x0008B5AC
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

	// Token: 0x06000B7F RID: 2943 RVA: 0x00008285 File Offset: 0x00006485
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0008D408 File Offset: 0x0008B608
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

	// Token: 0x06000B81 RID: 2945 RVA: 0x0008D4EC File Offset: 0x0008B6EC
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

	// Token: 0x06000B82 RID: 2946 RVA: 0x0008D548 File Offset: 0x0008B748
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
				if (component && (!isOn || (isOn && !component.isOnMarket)) && component.playerGame && !component.inDevelopment && !component.schublade && !component.typ_budget && !component.typ_goty && component.portID == -1 && !component.pubOffer && !component.auftragsspiel && !component.typ_bundle && !component.typ_bundleAddon && !component.f2pConverted && (component.typ_standard || component.typ_nachfolger || component.typ_spinoff) && !component.nachfolger_created)
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

	// Token: 0x06000B83 RID: 2947 RVA: 0x0008D778 File Offset: 0x0008B978
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

	// Token: 0x06000B84 RID: 2948 RVA: 0x00008293 File Offset: 0x00006493
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[57]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0008D92C File Offset: 0x0008BB2C
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

	// Token: 0x06000B86 RID: 2950 RVA: 0x000082C7 File Offset: 0x000064C7
	public void TOGGLE_VomMarktGenommen()
	{
		if (this.rS_)
		{
			this.Init(this.rS_);
		}
	}

	// Token: 0x04000FD1 RID: 4049
	public GameObject[] uiPrefabs;

	// Token: 0x04000FD2 RID: 4050
	public GameObject[] uiObjects;

	// Token: 0x04000FD3 RID: 4051
	private mainScript mS_;

	// Token: 0x04000FD4 RID: 4052
	private GameObject main_;

	// Token: 0x04000FD5 RID: 4053
	private GUI_Main guiMain_;

	// Token: 0x04000FD6 RID: 4054
	private sfxScript sfx_;

	// Token: 0x04000FD7 RID: 4055
	private textScript tS_;

	// Token: 0x04000FD8 RID: 4056
	private genres genres_;

	// Token: 0x04000FD9 RID: 4057
	public roomScript rS_;

	// Token: 0x04000FDA RID: 4058
	private float updateTimer;

	// Token: 0x04000FDB RID: 4059
	private string searchStringA = "";
}
