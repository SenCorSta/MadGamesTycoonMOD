using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000231 RID: 561
public class Menu_Stats_Developer_Games : MonoBehaviour
{
	// Token: 0x060015A0 RID: 5536 RVA: 0x000DC6BF File Offset: 0x000DA8BF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000DC6C8 File Offset: 0x000DA8C8
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

	// Token: 0x060015A2 RID: 5538 RVA: 0x000DC790 File Offset: 0x000DA990
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x000DC7C8 File Offset: 0x000DA9C8
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

	// Token: 0x060015A4 RID: 5540 RVA: 0x000DC814 File Offset: 0x000DAA14
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_Review>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x000DC858 File Offset: 0x000DAA58
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(1555));
		list.Add(this.tS_.GetText(1290));
		list.Add(this.tS_.GetText(1289));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x000DC96C File Offset: 0x000DAB6C
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000DC988 File Offset: 0x000DAB88
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
					Item_MyGames_Review component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_Review>();
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
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000DCADD File Offset: 0x000DACDD
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.pS_.myID && !script_.pubAngebot && !script_.auftragsspiel;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000DCB0D File Offset: 0x000DAD0D
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x000DCB28 File Offset: 0x000DAD28
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
				Item_MyGames_Review component = gameObject.GetComponent<Item_MyGames_Review>();
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
				case 6:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 7:
					gameObject.name = component.game_.GetUserReviewPercent().ToString();
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

	// Token: 0x04001992 RID: 6546
	private mainScript mS_;

	// Token: 0x04001993 RID: 6547
	private GameObject main_;

	// Token: 0x04001994 RID: 6548
	private GUI_Main guiMain_;

	// Token: 0x04001995 RID: 6549
	private sfxScript sfx_;

	// Token: 0x04001996 RID: 6550
	private textScript tS_;

	// Token: 0x04001997 RID: 6551
	private genres genres_;

	// Token: 0x04001998 RID: 6552
	public GameObject[] uiPrefabs;

	// Token: 0x04001999 RID: 6553
	public GameObject[] uiObjects;

	// Token: 0x0400199A RID: 6554
	private publisherScript pS_;

	// Token: 0x0400199B RID: 6555
	private float updateTimer;
}
