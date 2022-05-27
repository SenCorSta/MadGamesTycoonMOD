using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000232 RID: 562
public class Menu_Stats_Developer_IPs : MonoBehaviour
{
	// Token: 0x060015AC RID: 5548 RVA: 0x000DCD4E File Offset: 0x000DAF4E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x000DCD58 File Offset: 0x000DAF58
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

	// Token: 0x060015AE RID: 5550 RVA: 0x000DCE20 File Offset: 0x000DB020
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x000DCE58 File Offset: 0x000DB058
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

	// Token: 0x060015B0 RID: 5552 RVA: 0x000DCEA4 File Offset: 0x000DB0A4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_MyIPs>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000DCEE8 File Offset: 0x000DB0E8
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x000DCF8E File Offset: 0x000DB18E
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x000DCFAC File Offset: 0x000DB1AC
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MyGames_MyIPs component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_MyIPs>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1783) + ": " + this.uiObjects[0].transform.childCount.ToString();
		this.uiObjects[4].GetComponent<Text>().text = text;
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000DD0FF File Offset: 0x000DB2FF
	public bool CheckGameData(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.IsMyIP(this.pS_) && script_.mainIP == script_.myID;
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000DD138 File Offset: 0x000DB338
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000DD154 File Offset: 0x000DB354
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
				Item_MyGames_MyIPs component = gameObject.GetComponent<Item_MyGames_MyIPs>();
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
					if (component.game_.inDevelopment)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.ipPunkte.ToString();
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

	// Token: 0x0400199C RID: 6556
	private mainScript mS_;

	// Token: 0x0400199D RID: 6557
	private GameObject main_;

	// Token: 0x0400199E RID: 6558
	private GUI_Main guiMain_;

	// Token: 0x0400199F RID: 6559
	private sfxScript sfx_;

	// Token: 0x040019A0 RID: 6560
	private textScript tS_;

	// Token: 0x040019A1 RID: 6561
	private genres genres_;

	// Token: 0x040019A2 RID: 6562
	public GameObject[] uiPrefabs;

	// Token: 0x040019A3 RID: 6563
	public GameObject[] uiObjects;

	// Token: 0x040019A4 RID: 6564
	private publisherScript pS_;

	// Token: 0x040019A5 RID: 6565
	private float updateTimer;
}
