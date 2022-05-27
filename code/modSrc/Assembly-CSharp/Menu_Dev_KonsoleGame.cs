using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000161 RID: 353
public class Menu_Dev_KonsoleGame : MonoBehaviour
{
	// Token: 0x06000D30 RID: 3376 RVA: 0x000904A3 File Offset: 0x0008E6A3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x000904AC File Offset: 0x0008E6AC
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

	// Token: 0x06000D32 RID: 3378 RVA: 0x0009059D File Offset: 0x0008E79D
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x000905D8 File Offset: 0x0008E7D8
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

	// Token: 0x06000D34 RID: 3380 RVA: 0x00090624 File Offset: 0x0008E824
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

	// Token: 0x06000D35 RID: 3381 RVA: 0x00090680 File Offset: 0x0008E880
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x00090694 File Offset: 0x0008E894
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

	// Token: 0x06000D37 RID: 3383 RVA: 0x00090778 File Offset: 0x0008E978
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x000907CC File Offset: 0x0008E9CC
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

	// Token: 0x06000D39 RID: 3385 RVA: 0x00090930 File Offset: 0x0008EB30
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_contractGame && !script_.typ_mmoaddon && !script_.typ_goty && !script_.inDevelopment && !script_.schublade && script_.gameTyp == 0 && !script_.arcade && !script_.handy;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x000909BC File Offset: 0x0008EBBC
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

	// Token: 0x06000D3B RID: 3387 RVA: 0x00090B6F File Offset: 0x0008ED6F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x00090B8A File Offset: 0x0008ED8A
	public void BUTTON_RemoveGame()
	{
		this.sfx_.PlaySound(3, true);
		this.menuDevKonsole_.SetGame(-1);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x00090BB4 File Offset: 0x0008EDB4
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

	// Token: 0x040011C0 RID: 4544
	public GameObject[] uiPrefabs;

	// Token: 0x040011C1 RID: 4545
	public GameObject[] uiObjects;

	// Token: 0x040011C2 RID: 4546
	private mainScript mS_;

	// Token: 0x040011C3 RID: 4547
	private GameObject main_;

	// Token: 0x040011C4 RID: 4548
	private GUI_Main guiMain_;

	// Token: 0x040011C5 RID: 4549
	private sfxScript sfx_;

	// Token: 0x040011C6 RID: 4550
	private textScript tS_;

	// Token: 0x040011C7 RID: 4551
	private genres genres_;

	// Token: 0x040011C8 RID: 4552
	private Menu_Dev_Konsole menuDevKonsole_;

	// Token: 0x040011C9 RID: 4553
	private float updateTimer;

	// Token: 0x040011CA RID: 4554
	private string searchStringA = "";
}
