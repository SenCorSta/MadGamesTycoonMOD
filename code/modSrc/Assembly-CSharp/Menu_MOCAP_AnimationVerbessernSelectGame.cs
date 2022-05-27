using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DC RID: 476
public class Menu_MOCAP_AnimationVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x060011DE RID: 4574 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000C82C4 File Offset: 0x000C64C4
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

	// Token: 0x060011E0 RID: 4576 RVA: 0x0000C7D9 File Offset: 0x0000A9D9
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000C83B8 File Offset: 0x000C65B8
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

	// Token: 0x060011E2 RID: 4578 RVA: 0x000C8404 File Offset: 0x000C6604
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

	// Token: 0x060011E3 RID: 4579 RVA: 0x0000C811 File Offset: 0x0000AA11
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x000C8460 File Offset: 0x000C6660
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

	// Token: 0x060011E5 RID: 4581 RVA: 0x0000C819 File Offset: 0x0000AA19
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x000C84F0 File Offset: 0x000C66F0
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && component.inDevelopment && !this.menuMocap_.WirdInAnderenRaumBearbeitet(0, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(1, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(2, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(3, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(4, component) && !this.menuMocap_.WirdInAnderenRaumBearbeitet(5, component) && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x060011E7 RID: 4583 RVA: 0x0000C827 File Offset: 0x0000AA27
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x000C8664 File Offset: 0x000C6864
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

	// Token: 0x0400166F RID: 5743
	private mainScript mS_;

	// Token: 0x04001670 RID: 5744
	private GameObject main_;

	// Token: 0x04001671 RID: 5745
	private GUI_Main guiMain_;

	// Token: 0x04001672 RID: 5746
	private sfxScript sfx_;

	// Token: 0x04001673 RID: 5747
	private textScript tS_;

	// Token: 0x04001674 RID: 5748
	private genres genres_;

	// Token: 0x04001675 RID: 5749
	private Menu_MOCAP_AnimationVerbessern menuMocap_;

	// Token: 0x04001676 RID: 5750
	public GameObject[] uiPrefabs;

	// Token: 0x04001677 RID: 5751
	public GameObject[] uiObjects;

	// Token: 0x04001678 RID: 5752
	private float updateTimer;
}
