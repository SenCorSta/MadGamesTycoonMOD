using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AB RID: 427
public class Menu_Marketing_SpezialSelectGame : MonoBehaviour
{
	// Token: 0x06001012 RID: 4114 RVA: 0x0000B5CF File Offset: 0x000097CF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x000B742C File Offset: 0x000B562C
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

	// Token: 0x06001014 RID: 4116 RVA: 0x0000B5D7 File Offset: 0x000097D7
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x000B74F4 File Offset: 0x000B56F4
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

	// Token: 0x06001016 RID: 4118 RVA: 0x000B7540 File Offset: 0x000B5740
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MarketingSpezial_Game>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x0000B60F File Offset: 0x0000980F
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x000B759C File Offset: 0x000B579C
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(433));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0000B617 File Offset: 0x00009817
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x000B7658 File Offset: 0x000B5858
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && (component.inDevelopment || component.isOnMarket || component.schublade) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MarketingSpezial_Game component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MarketingSpezial_Game>();
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
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0000B625 File Offset: 0x00009825
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000B777C File Offset: 0x000B597C
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
				Item_MarketingSpezial_Game component = gameObject.GetComponent<Item_MarketingSpezial_Game>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.hype.ToString();
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

	// Token: 0x04001499 RID: 5273
	private mainScript mS_;

	// Token: 0x0400149A RID: 5274
	private GameObject main_;

	// Token: 0x0400149B RID: 5275
	private GUI_Main guiMain_;

	// Token: 0x0400149C RID: 5276
	private sfxScript sfx_;

	// Token: 0x0400149D RID: 5277
	private textScript tS_;

	// Token: 0x0400149E RID: 5278
	private genres genres_;

	// Token: 0x0400149F RID: 5279
	public GameObject[] uiPrefabs;

	// Token: 0x040014A0 RID: 5280
	public GameObject[] uiObjects;

	// Token: 0x040014A1 RID: 5281
	private float updateTimer;
}
