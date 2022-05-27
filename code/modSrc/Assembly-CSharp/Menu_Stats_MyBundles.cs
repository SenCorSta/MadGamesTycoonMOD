using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000240 RID: 576
public class Menu_Stats_MyBundles : MonoBehaviour
{
	// Token: 0x06001620 RID: 5664 RVA: 0x0000F500 File Offset: 0x0000D700
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000EA260 File Offset: 0x000E8460
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

	// Token: 0x06001622 RID: 5666 RVA: 0x0000F508 File Offset: 0x0000D708
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000EA328 File Offset: 0x000E8528
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

	// Token: 0x06001624 RID: 5668 RVA: 0x000EA374 File Offset: 0x000E8574
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyBundles>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x0000F540 File Offset: 0x0000D740
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000EA3B8 File Offset: 0x000E85B8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x0000F548 File Offset: 0x0000D748
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000EA458 File Offset: 0x000E8658
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
					Item_MyBundles component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyBundles>();
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

	// Token: 0x06001629 RID: 5673 RVA: 0x0000F55C File Offset: 0x0000D75C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && (script_.typ_bundle || script_.typ_bundleAddon);
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x0000F589 File Offset: 0x0000D789
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000EA564 File Offset: 0x000E8764
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
				Item_MyBundles component = gameObject.GetComponent<Item_MyBundles>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
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

	// Token: 0x04001A33 RID: 6707
	private mainScript mS_;

	// Token: 0x04001A34 RID: 6708
	private GameObject main_;

	// Token: 0x04001A35 RID: 6709
	private GUI_Main guiMain_;

	// Token: 0x04001A36 RID: 6710
	private sfxScript sfx_;

	// Token: 0x04001A37 RID: 6711
	private textScript tS_;

	// Token: 0x04001A38 RID: 6712
	private genres genres_;

	// Token: 0x04001A39 RID: 6713
	public GameObject[] uiPrefabs;

	// Token: 0x04001A3A RID: 6714
	public GameObject[] uiObjects;

	// Token: 0x04001A3B RID: 6715
	private float updateTimer;
}
