using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000192 RID: 402
public class Menu_Engine_ShowGames : MonoBehaviour
{
	// Token: 0x06000F46 RID: 3910 RVA: 0x000A1C1D File Offset: 0x0009FE1D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x000A1C28 File Offset: 0x0009FE28
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

	// Token: 0x06000F48 RID: 3912 RVA: 0x000A1CF0 File Offset: 0x0009FEF0
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000A1D28 File Offset: 0x0009FF28
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

	// Token: 0x06000F4A RID: 3914 RVA: 0x000A1D74 File Offset: 0x0009FF74
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_GamesList>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x000A1DB8 File Offset: 0x0009FFB8
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x000A1DC0 File Offset: 0x0009FFC0
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(274));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(276));
		list.Add(this.tS_.GetText(277));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x000A1EBE File Offset: 0x000A00BE
	public void Init(engineScript s_)
	{
		this.FindScripts();
		this.eS_ = s_;
		this.SetData();
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x000A1ED4 File Offset: 0x000A00D4
	private void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.GetGamesAmount(), false);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.engineID == this.eS_.myID && !component.inDevelopment && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_GamesList component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_GamesList>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.game_ = component;
					component2.genres_ = this.genres_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x000A202B File Offset: 0x000A022B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000A2048 File Offset: 0x000A0248
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
				Item_GamesList component = gameObject.GetComponent<Item_GamesList>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					component.SetData(component.game_.GetDeveloperName());
					break;
				case 1:
					gameObject.name = component.game_.maingenre.ToString();
					component.SetData(component.game_.GetGenreString());
					break;
				case 2:
					gameObject.name = component.game_.developerID.ToString();
					component.SetData(component.game_.GetDeveloperName());
					break;
				case 3:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					component.SetData(component.game_.GetReleaseDateString());
					break;
				}
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
					component.SetData(this.mS_.GetMoney(component.game_.sellsTotal, false));
					break;
				case 5:
					gameObject.name = component.game_.umsatzTotal.ToString();
					component.SetData(this.mS_.GetMoney(component.game_.umsatzTotal, true));
					break;
				case 6:
					gameObject.name = component.game_.reviewTotal.ToString();
					component.SetData(component.game_.reviewTotal.ToString() + "%");
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

	// Token: 0x0400138F RID: 5007
	private mainScript mS_;

	// Token: 0x04001390 RID: 5008
	private GameObject main_;

	// Token: 0x04001391 RID: 5009
	private GUI_Main guiMain_;

	// Token: 0x04001392 RID: 5010
	private sfxScript sfx_;

	// Token: 0x04001393 RID: 5011
	private textScript tS_;

	// Token: 0x04001394 RID: 5012
	private engineScript eS_;

	// Token: 0x04001395 RID: 5013
	private genres genres_;

	// Token: 0x04001396 RID: 5014
	public GameObject[] uiPrefabs;

	// Token: 0x04001397 RID: 5015
	public GameObject[] uiObjects;

	// Token: 0x04001398 RID: 5016
	private float updateTimer;
}
