using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020B RID: 523
public class Menu_QA_ShowSpielberichtSelectGame : MonoBehaviour
{
	// Token: 0x06001403 RID: 5123 RVA: 0x0000DA1B File Offset: 0x0000BC1B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x000DB808 File Offset: 0x000D9A08
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

	// Token: 0x06001405 RID: 5125 RVA: 0x0000DA23 File Offset: 0x0000BC23
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x000DB8D0 File Offset: 0x000D9AD0
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

	// Token: 0x06001407 RID: 5127 RVA: 0x000DB91C File Offset: 0x000D9B1C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_QA_ShowSpielbericht>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0000DA5B File Offset: 0x0000BC5B
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x000DB978 File Offset: 0x000D9B78
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x000DBA30 File Offset: 0x000D9C30
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000DBA84 File Offset: 0x000D9C84
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
						Item_QA_ShowSpielbericht component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_QA_ShowSpielbericht>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x0000DA69 File Offset: 0x0000BC69
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.playerGame || script_.IsMyAuftragsspiel()) && !script_.archiv_spielbericht && script_.spielbericht && !script_.typ_budget && !script_.typ_goty;
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x000DBBD8 File Offset: 0x000D9DD8
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
				Item_QA_ShowSpielbericht component = gameObject.GetComponent<Item_QA_ShowSpielbericht>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
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

	// Token: 0x0600140E RID: 5134 RVA: 0x0000DAA6 File Offset: 0x0000BCA6
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x000DBD50 File Offset: 0x000D9F50
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

	// Token: 0x0400182B RID: 6187
	public GameObject[] uiPrefabs;

	// Token: 0x0400182C RID: 6188
	public GameObject[] uiObjects;

	// Token: 0x0400182D RID: 6189
	private mainScript mS_;

	// Token: 0x0400182E RID: 6190
	private GameObject main_;

	// Token: 0x0400182F RID: 6191
	private GUI_Main guiMain_;

	// Token: 0x04001830 RID: 6192
	private sfxScript sfx_;

	// Token: 0x04001831 RID: 6193
	private textScript tS_;

	// Token: 0x04001832 RID: 6194
	private genres genres_;

	// Token: 0x04001833 RID: 6195
	private float updateTimer;

	// Token: 0x04001834 RID: 6196
	private string searchStringA = "";
}
