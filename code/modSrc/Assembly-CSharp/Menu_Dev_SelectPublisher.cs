using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000144 RID: 324
public class Menu_Dev_SelectPublisher : MonoBehaviour
{
	// Token: 0x06000BDE RID: 3038 RVA: 0x0007FCC1 File Offset: 0x0007DEC1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x0007FCCC File Offset: 0x0007DECC
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

	// Token: 0x06000BE0 RID: 3040 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x0007FE0C File Offset: 0x0007E00C
	public void Init(gameScript game_, taskGame t_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.task_ = t_;
		this.InitDropdowns();
		this.uiObjects[1].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[6].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.maingenre);
		this.uiObjects[8].GetComponent<Text>().text = this.genres_.GetName(this.gS_.maingenre);
		if (this.gS_.subgenre != -1)
		{
			this.uiObjects[7].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.subgenre);
			this.uiObjects[9].GetComponent<Text>().text = this.genres_.GetName(this.gS_.subgenre);
		}
		else
		{
			this.uiObjects[7].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[9].GetComponent<Text>().text = "---";
		}
		publisherScript publisherScript = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && !component.TochterfirmaGeschlossen() && !component.isPlayer && component.publisher && !component.onlyMobile)
				{
					if ((float)this.gS_.reviewTotal >= component.GetMinimalReviewPoints() || component.IsMyTochterfirma())
					{
						Item_SelectPublisher component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_SelectPublisher>();
						component2.pS_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
					}
					if (publisherScript == null)
					{
						publisherScript = component;
					}
					if (publisherScript && publisherScript.GetMinimalReviewPoints() > component.GetMinimalReviewPoints())
					{
						publisherScript = component;
					}
				}
			}
		}
		if (this.uiObjects[0].transform.childCount <= 0)
		{
			Item_SelectPublisher component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_SelectPublisher>();
			component3.pS_ = publisherScript;
			component3.mS_ = this.mS_;
			component3.tS_ = this.tS_;
			component3.sfx_ = this.sfx_;
			component3.guiMain_ = this.guiMain_;
			component3.genres_ = this.genres_;
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00080110 File Offset: 0x0007E310
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(434));
		list.Add(this.tS_.GetText(435));
		list.Add(this.tS_.GetText(436));
		list.Add(this.tS_.GetText(437));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x000801E4 File Offset: 0x0007E3E4
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
				Item_SelectPublisher component = gameObject.GetComponent<Item_SelectPublisher>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.stars.ToString();
					break;
				case 2:
					gameObject.name = component.pS_.GetRelation().ToString();
					break;
				case 3:
					gameObject.name = component.pS_.GetShare().ToString();
					break;
				case 4:
					gameObject.name = component.pS_.fanGenre.ToString();
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

	// Token: 0x06000BE4 RID: 3044 RVA: 0x0008032C File Offset: 0x0007E52C
	public void BUTTON_Close()
	{
		if (!this.task_ || !this.gS_)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.gS_.ClearReview();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[69]);
		this.guiMain_.uiObjects[69].GetComponent<Menu_DevGame_Complete>().Init(this.gS_, this.task_);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x000803C4 File Offset: 0x0007E5C4
	public void SelectPublisher(int id_)
	{
		UnityEngine.Object.Destroy(this.task_.gameObject);
		this.gS_.SetPublisher(id_);
		this.gS_.SetOnMarket();
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
		this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
	}

	// Token: 0x0400101F RID: 4127
	private mainScript mS_;

	// Token: 0x04001020 RID: 4128
	private GameObject main_;

	// Token: 0x04001021 RID: 4129
	private GUI_Main guiMain_;

	// Token: 0x04001022 RID: 4130
	private sfxScript sfx_;

	// Token: 0x04001023 RID: 4131
	private textScript tS_;

	// Token: 0x04001024 RID: 4132
	private themes themes_;

	// Token: 0x04001025 RID: 4133
	private Menu_DevGame mDevGame_;

	// Token: 0x04001026 RID: 4134
	private genres genres_;

	// Token: 0x04001027 RID: 4135
	private gameScript gS_;

	// Token: 0x04001028 RID: 4136
	private taskGame task_;

	// Token: 0x04001029 RID: 4137
	public GameObject[] uiPrefabs;

	// Token: 0x0400102A RID: 4138
	public GameObject[] uiObjects;
}
