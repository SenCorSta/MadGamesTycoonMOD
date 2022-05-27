using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024A RID: 586
public class Menu_Stats_MyGames_Sells : MonoBehaviour
{
	// Token: 0x060016B6 RID: 5814 RVA: 0x000E4E7D File Offset: 0x000E307D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x000E4E88 File Offset: 0x000E3088
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

	// Token: 0x060016B8 RID: 5816 RVA: 0x000E4F50 File Offset: 0x000E3150
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000E4F88 File Offset: 0x000E3188
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

	// Token: 0x060016BA RID: 5818 RVA: 0x000E4FD4 File Offset: 0x000E31D4
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

	// Token: 0x060016BB RID: 5819 RVA: 0x000E5030 File Offset: 0x000E3230
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x000E5038 File Offset: 0x000E3238
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000E5048 File Offset: 0x000E3248
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

	// Token: 0x060016BE RID: 5822 RVA: 0x000E51C0 File Offset: 0x000E33C0
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID))
		{
			if (this.uiObjects[6].GetComponent<Toggle>().isOn && script_.developerID != this.mS_.myID)
			{
				return false;
			}
			if (!script_.inDevelopment && !script_.schublade && script_.gameTyp != 2 && !script_.handy && !script_.arcade)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000E5250 File Offset: 0x000E3450
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000E526C File Offset: 0x000E346C
	public void TOGGLE_OnlyMyGames()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x04001A8A RID: 6794
	private mainScript mS_;

	// Token: 0x04001A8B RID: 6795
	private GameObject main_;

	// Token: 0x04001A8C RID: 6796
	private GUI_Main guiMain_;

	// Token: 0x04001A8D RID: 6797
	private sfxScript sfx_;

	// Token: 0x04001A8E RID: 6798
	private textScript tS_;

	// Token: 0x04001A8F RID: 6799
	private genres genres_;

	// Token: 0x04001A90 RID: 6800
	public GameObject[] uiPrefabs;

	// Token: 0x04001A91 RID: 6801
	public GameObject[] uiObjects;

	// Token: 0x04001A92 RID: 6802
	private float updateTimer;
}
