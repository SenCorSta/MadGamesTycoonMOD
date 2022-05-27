using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011C RID: 284
public class Menu_DevGame_Engine : MonoBehaviour
{
	// Token: 0x060009CC RID: 2508 RVA: 0x0006BAD0 File Offset: 0x00069CD0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0006BAD8 File Offset: 0x00069CD8
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0006BBE4 File Offset: 0x00069DE4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0006BC1C File Offset: 0x00069E1C
	public void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0006BC30 File Offset: 0x00069E30
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

	// Token: 0x060009D1 RID: 2513 RVA: 0x0006BC7C File Offset: 0x00069E7C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf && gameObject.GetComponent<Item_DevGame_Engine>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0006BCD0 File Offset: 0x00069ED0
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(245));
		list.Add(this.tS_.GetText(160));
		list.Add(this.tS_.GetText(261));
		list.Add(this.tS_.GetText(258));
		list.Add(this.tS_.GetText(260));
		list.Add(this.tS_.GetText(268));
		list.Add(this.tS_.GetText(1218));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0006BDF8 File Offset: 0x00069FF8
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0006BE4C File Offset: 0x0006A04C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && !component.archiv_engine && ((component.isUnlocked && component.gekauft) || (component.ownerID == this.mS_.myID && component.devPointsStart <= 0f) || (component.ownerID == this.mS_.myID && component.updating)))
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_DevGame_Engine component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_Engine>();
						component2.eS_ = component;
						component2.eF_ = this.eF_;
						component2.genres_ = this.genres_;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.mDevGame_ = this.mDevGame_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0006C00C File Offset: 0x0006A20C
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
				Item_DevGame_Engine component = gameObject.GetComponent<Item_DevGame_Engine>();
				switch (value)
				{
				case 0:
					gameObject.name = component.eS_.GetName();
					break;
				case 1:
					gameObject.name = (component.eS_.GetTechLevel() * 1000 + component.eS_.GetFeaturesAmount()).ToString();
					break;
				case 2:
					gameObject.name = component.eS_.spezialgenre.ToString();
					break;
				case 3:
					gameObject.name = component.eS_.GetFeaturesAmount().ToString();
					break;
				case 4:
					gameObject.name = component.eS_.GetGamesAmount().ToString();
					break;
				case 5:
					if (component.eS_.ownerID == this.mS_.myID)
					{
						gameObject.name = "2";
					}
					else
					{
						gameObject.name = "1";
					}
					break;
				case 6:
					gameObject.name = component.eS_.gewinnbeteiligung.ToString();
					break;
				case 7:
					gameObject.name = component.eS_.GetVerkaufteLizenzen().ToString();
					break;
				case 8:
					gameObject.name = component.eS_.spezialplatform.ToString();
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

	// Token: 0x060009D6 RID: 2518 RVA: 0x0006C20F File Offset: 0x0006A40F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0006C22C File Offset: 0x0006A42C
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
		this.searchStringA = this.uiObjects[7].GetComponent<InputField>().text;
		this.Init();
	}

	// Token: 0x04000E1B RID: 3611
	public GameObject[] uiPrefabs;

	// Token: 0x04000E1C RID: 3612
	public GameObject[] uiObjects;

	// Token: 0x04000E1D RID: 3613
	private mainScript mS_;

	// Token: 0x04000E1E RID: 3614
	private GameObject main_;

	// Token: 0x04000E1F RID: 3615
	private GUI_Main guiMain_;

	// Token: 0x04000E20 RID: 3616
	private sfxScript sfx_;

	// Token: 0x04000E21 RID: 3617
	private textScript tS_;

	// Token: 0x04000E22 RID: 3618
	private engineFeatures eF_;

	// Token: 0x04000E23 RID: 3619
	private genres genres_;

	// Token: 0x04000E24 RID: 3620
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E25 RID: 3621
	private float updateTimer;

	// Token: 0x04000E26 RID: 3622
	private string searchStringA = "";
}
