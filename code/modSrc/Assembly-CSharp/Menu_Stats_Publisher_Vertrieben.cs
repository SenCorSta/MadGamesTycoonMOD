using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000253 RID: 595
public class Menu_Stats_Publisher_Vertrieben : MonoBehaviour
{
	// Token: 0x06001720 RID: 5920 RVA: 0x000E76E7 File Offset: 0x000E58E7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x000E76F0 File Offset: 0x000E58F0
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

	// Token: 0x06001722 RID: 5922 RVA: 0x000E77B8 File Offset: 0x000E59B8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000E77F0 File Offset: 0x000E59F0
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

	// Token: 0x06001724 RID: 5924 RVA: 0x000E783C File Offset: 0x000E5A3C
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

	// Token: 0x06001725 RID: 5925 RVA: 0x000E7880 File Offset: 0x000E5A80
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

	// Token: 0x06001726 RID: 5926 RVA: 0x000E7994 File Offset: 0x000E5B94
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000E79B0 File Offset: 0x000E5BB0
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

	// Token: 0x06001728 RID: 5928 RVA: 0x000E7B08 File Offset: 0x000E5D08
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.pS_.myID && script_.developerID != this.pS_.myID && !script_.pubAngebot && !script_.auftragsspiel;
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000E7B56 File Offset: 0x000E5D56
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x000E7B74 File Offset: 0x000E5D74
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

	// Token: 0x04001AD9 RID: 6873
	private mainScript mS_;

	// Token: 0x04001ADA RID: 6874
	private GameObject main_;

	// Token: 0x04001ADB RID: 6875
	private GUI_Main guiMain_;

	// Token: 0x04001ADC RID: 6876
	private sfxScript sfx_;

	// Token: 0x04001ADD RID: 6877
	private textScript tS_;

	// Token: 0x04001ADE RID: 6878
	private genres genres_;

	// Token: 0x04001ADF RID: 6879
	public GameObject[] uiPrefabs;

	// Token: 0x04001AE0 RID: 6880
	public GameObject[] uiObjects;

	// Token: 0x04001AE1 RID: 6881
	private publisherScript pS_;

	// Token: 0x04001AE2 RID: 6882
	private float updateTimer;
}
