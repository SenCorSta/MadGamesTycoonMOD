using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000160 RID: 352
public class Menu_Dev_KonsoleGame : MonoBehaviour
{
	// Token: 0x06000D18 RID: 3352 RVA: 0x00009296 File Offset: 0x00007496
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0009EED8 File Offset: 0x0009D0D8
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
		if (!this.menuDevKonsole_)
		{
			this.menuDevKonsole_ = this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>();
		}
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0000929E File Offset: 0x0000749E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0009EFCC File Offset: 0x0009D1CC
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

	// Token: 0x06000D1C RID: 3356 RVA: 0x0009F018 File Offset: 0x0009D218
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_KonsoleGame>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x000092D6 File Offset: 0x000074D6
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0009F074 File Offset: 0x0009D274
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

	// Token: 0x06000D1F RID: 3359 RVA: 0x0009F158 File Offset: 0x0009D358
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0009F1AC File Offset: 0x0009D3AC
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
						Item_KonsoleGame component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_KonsoleGame>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.menu_ = this.menuDevKonsole_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0009F310 File Offset: 0x0009D510
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_contractGame && !script_.typ_mmoaddon && !script_.typ_goty && !script_.inDevelopment && !script_.schublade && script_.gameTyp == 0 && !script_.arcade && !script_.handy;
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0009F390 File Offset: 0x0009D590
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
				Item_KonsoleGame component = gameObject.GetComponent<Item_KonsoleGame>();
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

	// Token: 0x06000D23 RID: 3363 RVA: 0x000092EA File Offset: 0x000074EA
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00009305 File Offset: 0x00007505
	public void BUTTON_RemoveGame()
	{
		this.sfx_.PlaySound(3, true);
		this.menuDevKonsole_.SetGame(-1);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0009F544 File Offset: 0x0009D744
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
		this.Init();
	}

	// Token: 0x040011B8 RID: 4536
	public GameObject[] uiPrefabs;

	// Token: 0x040011B9 RID: 4537
	public GameObject[] uiObjects;

	// Token: 0x040011BA RID: 4538
	private mainScript mS_;

	// Token: 0x040011BB RID: 4539
	private GameObject main_;

	// Token: 0x040011BC RID: 4540
	private GUI_Main guiMain_;

	// Token: 0x040011BD RID: 4541
	private sfxScript sfx_;

	// Token: 0x040011BE RID: 4542
	private textScript tS_;

	// Token: 0x040011BF RID: 4543
	private genres genres_;

	// Token: 0x040011C0 RID: 4544
	private Menu_Dev_Konsole menuDevKonsole_;

	// Token: 0x040011C1 RID: 4545
	private float updateTimer;

	// Token: 0x040011C2 RID: 4546
	private string searchStringA = "";
}
