using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000111 RID: 273
public class Menu_Dev_Engine_SelectOld : MonoBehaviour
{
	// Token: 0x060008E6 RID: 2278 RVA: 0x00060813 File Offset: 0x0005EA13
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0006081C File Offset: 0x0005EA1C
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

	// Token: 0x060008E8 RID: 2280 RVA: 0x00060902 File Offset: 0x0005EB02
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0006090C File Offset: 0x0005EB0C
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

	// Token: 0x060008EA RID: 2282 RVA: 0x00060A2F File Offset: 0x0005EC2F
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00060A68 File Offset: 0x0005EC68
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

	// Token: 0x060008EC RID: 2284 RVA: 0x00060AB4 File Offset: 0x0005ECB4
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

	// Token: 0x060008ED RID: 2285 RVA: 0x00060B10 File Offset: 0x0005ED10
	public void Init(roomScript s_)
	{
		this.rS_ = s_;
		this.SetData();
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00060B20 File Offset: 0x0005ED20
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
				if (component && component.ownerID == this.mS_.myID && component.Complete() && !component.archiv_engine)
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

	// Token: 0x060008EF RID: 2287 RVA: 0x00060CB4 File Offset: 0x0005EEB4
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

	// Token: 0x060008F0 RID: 2288 RVA: 0x00060E87 File Offset: 0x0005F087
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[36]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00060EBC File Offset: 0x0005F0BC
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

	// Token: 0x04000D5F RID: 3423
	public roomScript rS_;

	// Token: 0x04000D60 RID: 3424
	private mainScript mS_;

	// Token: 0x04000D61 RID: 3425
	private GameObject main_;

	// Token: 0x04000D62 RID: 3426
	private GUI_Main guiMain_;

	// Token: 0x04000D63 RID: 3427
	private sfxScript sfx_;

	// Token: 0x04000D64 RID: 3428
	private textScript tS_;

	// Token: 0x04000D65 RID: 3429
	private engineFeatures eF_;

	// Token: 0x04000D66 RID: 3430
	private genres genres_;

	// Token: 0x04000D67 RID: 3431
	public GameObject[] uiPrefabs;

	// Token: 0x04000D68 RID: 3432
	public GameObject[] uiObjects;

	// Token: 0x04000D69 RID: 3433
	private float updateTimer;

	// Token: 0x04000D6A RID: 3434
	private string searchStringA = "";
}
