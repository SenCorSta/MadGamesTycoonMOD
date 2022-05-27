using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DD RID: 477
public class Menu_MOCAP_AnimationVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x060011F8 RID: 4600 RVA: 0x000BD27D File Offset: 0x000BB47D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000BD288 File Offset: 0x000BB488
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
		if (!this.menuMocap_)
		{
			this.menuMocap_ = this.guiMain_.uiObjects[178].GetComponent<Menu_MOCAP_AnimationVerbessern>();
		}
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x000BD379 File Offset: 0x000BB579
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x000BD3B4 File Offset: 0x000BB5B4
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

	// Token: 0x060011FC RID: 4604 RVA: 0x000BD400 File Offset: 0x000BB600
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MOCAP_AnimationVerbessern>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x000BD45C File Offset: 0x000BB65C
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000BD464 File Offset: 0x000BB664
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(273));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000BD4F4 File Offset: 0x000BB6F4
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000BD504 File Offset: 0x000BB704
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(0, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(1, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(2, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(3, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(4, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(5, component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MOCAP_AnimationVerbessern component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MOCAP_AnimationVerbessern>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x000BD66E File Offset: 0x000BB86E
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.inDevelopment;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000BD696 File Offset: 0x000BB896
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x000BD6B4 File Offset: 0x000BB8B4
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
				Item_MOCAP_AnimationVerbessern component = gameObject.GetComponent<Item_MOCAP_AnimationVerbessern>();
				if (value != 0)
				{
					if (value == 1)
					{
						gameObject.name = component.game_.maingenre.ToString();
					}
				}
				else
				{
					gameObject.name = component.game_.GetNameSimple();
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

	// Token: 0x04001678 RID: 5752
	private mainScript mS_;

	// Token: 0x04001679 RID: 5753
	private GameObject main_;

	// Token: 0x0400167A RID: 5754
	private GUI_Main guiMain_;

	// Token: 0x0400167B RID: 5755
	private sfxScript sfx_;

	// Token: 0x0400167C RID: 5756
	private textScript tS_;

	// Token: 0x0400167D RID: 5757
	private genres genres_;

	// Token: 0x0400167E RID: 5758
	private Menu_MOCAP_AnimationVerbessern menuMocap_;

	// Token: 0x0400167F RID: 5759
	public GameObject[] uiPrefabs;

	// Token: 0x04001680 RID: 5760
	public GameObject[] uiObjects;

	// Token: 0x04001681 RID: 5761
	private float updateTimer;
}
