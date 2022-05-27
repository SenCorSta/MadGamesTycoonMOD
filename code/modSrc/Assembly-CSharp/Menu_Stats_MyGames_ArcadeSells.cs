using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000245 RID: 581
public class Menu_Stats_MyGames_ArcadeSells : MonoBehaviour
{
	// Token: 0x06001673 RID: 5747 RVA: 0x000E39E2 File Offset: 0x000E1BE2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000E39EC File Offset: 0x000E1BEC
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

	// Token: 0x06001675 RID: 5749 RVA: 0x000E3AB4 File Offset: 0x000E1CB4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x000E3AEC File Offset: 0x000E1CEC
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

	// Token: 0x06001677 RID: 5751 RVA: 0x000E3B38 File Offset: 0x000E1D38
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

	// Token: 0x06001678 RID: 5752 RVA: 0x000E3B94 File Offset: 0x000E1D94
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000E3B9C File Offset: 0x000E1D9C
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000E3BAC File Offset: 0x000E1DAC
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

	// Token: 0x0600167B RID: 5755 RVA: 0x000E3D24 File Offset: 0x000E1F24
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID))
		{
			if (this.uiObjects[6].GetComponent<Toggle>().isOn && script_.developerID != this.mS_.myID)
			{
				return false;
			}
			if (!script_.inDevelopment && !script_.schublade && script_.arcade && script_.gameTyp != 2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000E3DAC File Offset: 0x000E1FAC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x000E3DC8 File Offset: 0x000E1FC8
	public void TOGGLE_OnlyMyGames()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x04001A60 RID: 6752
	private mainScript mS_;

	// Token: 0x04001A61 RID: 6753
	private GameObject main_;

	// Token: 0x04001A62 RID: 6754
	private GUI_Main guiMain_;

	// Token: 0x04001A63 RID: 6755
	private sfxScript sfx_;

	// Token: 0x04001A64 RID: 6756
	private textScript tS_;

	// Token: 0x04001A65 RID: 6757
	private genres genres_;

	// Token: 0x04001A66 RID: 6758
	public GameObject[] uiPrefabs;

	// Token: 0x04001A67 RID: 6759
	public GameObject[] uiObjects;

	// Token: 0x04001A68 RID: 6760
	private float updateTimer;
}
