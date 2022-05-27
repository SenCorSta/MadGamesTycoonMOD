using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F6 RID: 502
public class Menu_GOTYSelect : MonoBehaviour
{
	// Token: 0x06001303 RID: 4867 RVA: 0x0000D07F File Offset: 0x0000B27F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x000D4EE0 File Offset: 0x000D30E0
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

	// Token: 0x06001305 RID: 4869 RVA: 0x0000D087 File Offset: 0x0000B287
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x000D4FA8 File Offset: 0x000D31A8
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

	// Token: 0x06001307 RID: 4871 RVA: 0x000D4FF4 File Offset: 0x000D31F4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_GotySelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0000D0BF File Offset: 0x0000B2BF
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x000D5050 File Offset: 0x000D3250
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000D511C File Offset: 0x000D331C
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000D5170 File Offset: 0x000D3370
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
					Item_GotySelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_GotySelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000D5284 File Offset: 0x000D3484
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.isOnMarket && script_.gameTyp == 0 && script_.goty && !script_.goty_created && !script_.pubOffer && (script_.typ_standard || script_.typ_nachfolger || script_.typ_remaster || script_.typ_spinoff) && !script_.handy && !script_.arcade;
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000D5304 File Offset: 0x000D3504
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
				Item_GotySelect component = gameObject.GetComponent<Item_GotySelect>();
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

	// Token: 0x0600130E RID: 4878 RVA: 0x0000D0CD File Offset: 0x0000B2CD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400175D RID: 5981
	public GameObject[] uiPrefabs;

	// Token: 0x0400175E RID: 5982
	public GameObject[] uiObjects;

	// Token: 0x0400175F RID: 5983
	private mainScript mS_;

	// Token: 0x04001760 RID: 5984
	private GameObject main_;

	// Token: 0x04001761 RID: 5985
	private GUI_Main guiMain_;

	// Token: 0x04001762 RID: 5986
	private sfxScript sfx_;

	// Token: 0x04001763 RID: 5987
	private textScript tS_;

	// Token: 0x04001764 RID: 5988
	private genres genres_;

	// Token: 0x04001765 RID: 5989
	private float updateTimer;
}
