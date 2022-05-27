using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022D RID: 557
public class Menu_Statistics_Tochterfirmen : MonoBehaviour
{
	// Token: 0x06001579 RID: 5497 RVA: 0x000DB3FB File Offset: 0x000D95FB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000DB404 File Offset: 0x000D9604
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
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000DB510 File Offset: 0x000D9710
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000DB544 File Offset: 0x000D9744
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Stats_Tochterfirma>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x000DB5A0 File Offset: 0x000D97A0
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000DB5AE File Offset: 0x000D97AE
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x000DB5BC File Offset: 0x000D97BC
	private void SetData()
	{
		long num = 0L;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.IsMyTochterfirma())
				{
					string text = component.GetName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_Stats_Tochterfirma component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Stats_Tochterfirma>();
						component2.pS_ = component;
						component2.playerID = -1;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						num += component.GetVerwaltungskosten();
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(1934) + ": <b><color=red>" + this.mS_.GetMoney(num, true) + "</color></b>";
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000DB768 File Offset: 0x000D9968
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(687));
		list.Add(this.tS_.GetText(685));
		list.Add(this.tS_.GetText(271));
		list.Add(this.tS_.GetText(1944));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000DB83C File Offset: 0x000D9A3C
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[5].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[5].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Stats_Tochterfirma component = gameObject.GetComponent<Item_Stats_Tochterfirma>();
				if (component.pS_)
				{
					switch (value)
					{
					case 0:
						gameObject.name = component.pS_.GetName();
						break;
					case 1:
						gameObject.name = component.pS_.stars.ToString();
						break;
					case 2:
						gameObject.name = component.pS_.GetFirmenwert().ToString();
						break;
					case 3:
						gameObject.name = component.pS_.GetAmountGames().ToString();
						break;
					case 4:
						gameObject.name = (0 - component.pS_.newGameInWeeks).ToString();
						break;
					}
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

	// Token: 0x06001582 RID: 5506 RVA: 0x000DB99E File Offset: 0x000D9B9E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[118].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000DB9DC File Offset: 0x000D9BDC
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

	// Token: 0x04001967 RID: 6503
	private mainScript mS_;

	// Token: 0x04001968 RID: 6504
	private GameObject main_;

	// Token: 0x04001969 RID: 6505
	private GUI_Main guiMain_;

	// Token: 0x0400196A RID: 6506
	private sfxScript sfx_;

	// Token: 0x0400196B RID: 6507
	private textScript tS_;

	// Token: 0x0400196C RID: 6508
	private themes themes_;

	// Token: 0x0400196D RID: 6509
	private Menu_DevGame mDevGame_;

	// Token: 0x0400196E RID: 6510
	private genres genres_;

	// Token: 0x0400196F RID: 6511
	private gameScript gS_;

	// Token: 0x04001970 RID: 6512
	private taskGame task_;

	// Token: 0x04001971 RID: 6513
	public GameObject[] uiPrefabs;

	// Token: 0x04001972 RID: 6514
	public GameObject[] uiObjects;

	// Token: 0x04001973 RID: 6515
	private string searchStringA = "";
}
