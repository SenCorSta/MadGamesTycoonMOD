using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000110 RID: 272
public class Menu_Dev_Engine_SelectOld : MonoBehaviour
{
	// Token: 0x060008D7 RID: 2263 RVA: 0x000068CA File Offset: 0x00004ACA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00071E78 File Offset: 0x00070078
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
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x000068D2 File Offset: 0x00004AD2
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00071F60 File Offset: 0x00070160
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
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(260));
		list.Add(this.tS_.GetText(268));
		list.Add(this.tS_.GetText(1218));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x000068DA File Offset: 0x00004ADA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00072084 File Offset: 0x00070284
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

	// Token: 0x060008DD RID: 2269 RVA: 0x000720D0 File Offset: 0x000702D0
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevEngine_Engine>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00006912 File Offset: 0x00004B12
	public void Init(roomScript s_)
	{
		this.rS_ = s_;
		this.SetData();
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0007212C File Offset: 0x0007032C
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.playerEngine && component.Complete() && !component.archiv_engine)
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_DevEngine_Engine component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevEngine_Engine>();
						component2.eS_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.eF_ = this.eF_;
						component2.genres_ = this.genres_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x000722B4 File Offset: 0x000704B4
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
				Item_DevEngine_Engine component = gameObject.GetComponent<Item_DevEngine_Engine>();
				switch (value)
				{
				case 0:
					gameObject.name = component.eS_.GetName();
					break;
				case 1:
					gameObject.name = component.eS_.GetTechLevel().ToString();
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
					gameObject.name = component.eS_.preis.ToString();
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

	// Token: 0x060008E1 RID: 2273 RVA: 0x00006921 File Offset: 0x00004B21
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[36]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00072488 File Offset: 0x00070688
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

	// Token: 0x04000D57 RID: 3415
	public roomScript rS_;

	// Token: 0x04000D58 RID: 3416
	private mainScript mS_;

	// Token: 0x04000D59 RID: 3417
	private GameObject main_;

	// Token: 0x04000D5A RID: 3418
	private GUI_Main guiMain_;

	// Token: 0x04000D5B RID: 3419
	private sfxScript sfx_;

	// Token: 0x04000D5C RID: 3420
	private textScript tS_;

	// Token: 0x04000D5D RID: 3421
	private engineFeatures eF_;

	// Token: 0x04000D5E RID: 3422
	private genres genres_;

	// Token: 0x04000D5F RID: 3423
	public GameObject[] uiPrefabs;

	// Token: 0x04000D60 RID: 3424
	public GameObject[] uiObjects;

	// Token: 0x04000D61 RID: 3425
	private float updateTimer;

	// Token: 0x04000D62 RID: 3426
	private string searchStringA = "";
}
