using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000143 RID: 323
public class Menu_Dev_SelectPublisher : MonoBehaviour
{
	// Token: 0x06000BC9 RID: 3017 RVA: 0x0000856D File Offset: 0x0000676D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x0008F61C File Offset: 0x0008D81C
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

	// Token: 0x06000BCB RID: 3019 RVA: 0x00008575 File Offset: 0x00006775
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x0008F728 File Offset: 0x0008D928
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
				if (component.isUnlocked && !component.TochterfirmaGeschlossen() && component.publisher && !component.onlyMobile)
				{
					if ((float)this.gS_.reviewTotal >= component.GetMinimalReviewPoints() || component.tochterfirma)
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

	// Token: 0x06000BCD RID: 3021 RVA: 0x0008FA20 File Offset: 0x0008DC20
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

	// Token: 0x06000BCE RID: 3022 RVA: 0x0008FAF4 File Offset: 0x0008DCF4
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

	// Token: 0x06000BCF RID: 3023 RVA: 0x0008FC3C File Offset: 0x0008DE3C
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

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0008FCD4 File Offset: 0x0008DED4
	public void SelectPublisher(int id_)
	{
		UnityEngine.Object.Destroy(this.task_.gameObject);
		this.gS_.SetPublisher(id_);
		this.gS_.SetOnMarket();
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
		this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
	}

	// Token: 0x04001017 RID: 4119
	private mainScript mS_;

	// Token: 0x04001018 RID: 4120
	private GameObject main_;

	// Token: 0x04001019 RID: 4121
	private GUI_Main guiMain_;

	// Token: 0x0400101A RID: 4122
	private sfxScript sfx_;

	// Token: 0x0400101B RID: 4123
	private textScript tS_;

	// Token: 0x0400101C RID: 4124
	private themes themes_;

	// Token: 0x0400101D RID: 4125
	private Menu_DevGame mDevGame_;

	// Token: 0x0400101E RID: 4126
	private genres genres_;

	// Token: 0x0400101F RID: 4127
	private gameScript gS_;

	// Token: 0x04001020 RID: 4128
	private taskGame task_;

	// Token: 0x04001021 RID: 4129
	public GameObject[] uiPrefabs;

	// Token: 0x04001022 RID: 4130
	public GameObject[] uiObjects;
}
