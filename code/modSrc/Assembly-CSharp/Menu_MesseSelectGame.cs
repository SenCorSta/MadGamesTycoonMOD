using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CE RID: 462
public class Menu_MesseSelectGame : MonoBehaviour
{
	// Token: 0x06001168 RID: 4456 RVA: 0x0000C271 File Offset: 0x0000A471
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x000C4818 File Offset: 0x000C2A18
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
		if (!this.menu_MesseSelect_)
		{
			this.menu_MesseSelect_ = this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>();
		}
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0000C279 File Offset: 0x0000A479
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0000C2AB File Offset: 0x0000A4AB
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x000C490C File Offset: 0x000C2B0C
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(325));
		list.Add(this.tS_.GetText(433));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x000C49D8 File Offset: 0x000C2BD8
	public void Init(int slot_)
	{
		this.FindScripts();
		this.slot = slot_;
		if (this.menu_MesseSelect_.games[this.slot] == null)
		{
			this.uiObjects[6].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[6].GetComponent<Button>().interactable = true;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j])
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
				{
					Item_MesseGame component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MesseGame>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.slot = this.slot;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && (script_.inDevelopment || script_.isOnMarket || script_.schublade) && !script_.typ_contractGame;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x000C4B64 File Offset: 0x000C2D64
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
				Item_MesseGame component = gameObject.GetComponent<Item_MesseGame>();
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
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
					gameObject.name = component.game_.gameTyp.ToString();
					break;
				case 4:
					gameObject.name = component.game_.GetHype().ToString();
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

	// Token: 0x06001170 RID: 4464 RVA: 0x0000C2EE File Offset: 0x0000A4EE
	public void BUTTON_Entfernen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetGame(this.slot, null);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0000C32B File Offset: 0x0000A52B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040015F7 RID: 5623
	public GameObject[] uiPrefabs;

	// Token: 0x040015F8 RID: 5624
	public GameObject[] uiObjects;

	// Token: 0x040015F9 RID: 5625
	private mainScript mS_;

	// Token: 0x040015FA RID: 5626
	private GameObject main_;

	// Token: 0x040015FB RID: 5627
	private GUI_Main guiMain_;

	// Token: 0x040015FC RID: 5628
	private sfxScript sfx_;

	// Token: 0x040015FD RID: 5629
	private textScript tS_;

	// Token: 0x040015FE RID: 5630
	private genres genres_;

	// Token: 0x040015FF RID: 5631
	private Menu_MesseSelect menu_MesseSelect_;

	// Token: 0x04001600 RID: 5632
	private int slot;
}
