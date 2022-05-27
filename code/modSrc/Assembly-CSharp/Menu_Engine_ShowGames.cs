using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000191 RID: 401
public class Menu_Engine_ShowGames : MonoBehaviour
{
	// Token: 0x06000F2E RID: 3886 RVA: 0x0000AC8B File Offset: 0x00008E8B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x000AEA80 File Offset: 0x000ACC80
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

	// Token: 0x06000F30 RID: 3888 RVA: 0x0000AC93 File Offset: 0x00008E93
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x000AEB48 File Offset: 0x000ACD48
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

	// Token: 0x06000F32 RID: 3890 RVA: 0x000AEB94 File Offset: 0x000ACD94
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

	// Token: 0x06000F33 RID: 3891 RVA: 0x0000ACCB File Offset: 0x00008ECB
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x000AEBD8 File Offset: 0x000ACDD8
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

	// Token: 0x06000F35 RID: 3893 RVA: 0x0000ACD3 File Offset: 0x00008ED3
	public void Init(engineScript s_)
	{
		this.FindScripts();
		this.eS_ = s_;
		this.SetData();
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x000AECD8 File Offset: 0x000ACED8
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

	// Token: 0x06000F37 RID: 3895 RVA: 0x0000ACE8 File Offset: 0x00008EE8
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x000AEE30 File Offset: 0x000AD030
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

	// Token: 0x04001386 RID: 4998
	private mainScript mS_;

	// Token: 0x04001387 RID: 4999
	private GameObject main_;

	// Token: 0x04001388 RID: 5000
	private GUI_Main guiMain_;

	// Token: 0x04001389 RID: 5001
	private sfxScript sfx_;

	// Token: 0x0400138A RID: 5002
	private textScript tS_;

	// Token: 0x0400138B RID: 5003
	private engineScript eS_;

	// Token: 0x0400138C RID: 5004
	private genres genres_;

	// Token: 0x0400138D RID: 5005
	public GameObject[] uiPrefabs;

	// Token: 0x0400138E RID: 5006
	public GameObject[] uiObjects;

	// Token: 0x0400138F RID: 5007
	private float updateTimer;
}
