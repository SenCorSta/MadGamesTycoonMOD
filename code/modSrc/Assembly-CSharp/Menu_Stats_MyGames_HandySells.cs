using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000248 RID: 584
public class Menu_Stats_MyGames_HandySells : MonoBehaviour
{
	// Token: 0x06001698 RID: 5784 RVA: 0x000E4727 File Offset: 0x000E2927
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x000E4730 File Offset: 0x000E2930
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

	// Token: 0x0600169A RID: 5786 RVA: 0x000E47F8 File Offset: 0x000E29F8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x000E4830 File Offset: 0x000E2A30
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

	// Token: 0x0600169C RID: 5788 RVA: 0x000E487C File Offset: 0x000E2A7C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_Sells>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x000E48D8 File Offset: 0x000E2AD8
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x000E48E0 File Offset: 0x000E2AE0
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x000E48F0 File Offset: 0x000E2AF0
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
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_MyGames_Sells component2 = gameObject.GetComponent<Item_MyGames_Sells>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					gameObject.name = component.sellsTotal.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x000E4A68 File Offset: 0x000E2C68
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID))
		{
			if (this.uiObjects[6].GetComponent<Toggle>().isOn && script_.developerID != this.mS_.myID)
			{
				return false;
			}
			if (!script_.inDevelopment && !script_.schublade && script_.handy && script_.gameTyp != 2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x000E4AF0 File Offset: 0x000E2CF0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x000E4B0C File Offset: 0x000E2D0C
	public void TOGGLE_OnlyMyGames()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x04001A7A RID: 6778
	private mainScript mS_;

	// Token: 0x04001A7B RID: 6779
	private GameObject main_;

	// Token: 0x04001A7C RID: 6780
	private GUI_Main guiMain_;

	// Token: 0x04001A7D RID: 6781
	private sfxScript sfx_;

	// Token: 0x04001A7E RID: 6782
	private textScript tS_;

	// Token: 0x04001A7F RID: 6783
	private genres genres_;

	// Token: 0x04001A80 RID: 6784
	public GameObject[] uiPrefabs;

	// Token: 0x04001A81 RID: 6785
	public GameObject[] uiObjects;

	// Token: 0x04001A82 RID: 6786
	private float updateTimer;
}
