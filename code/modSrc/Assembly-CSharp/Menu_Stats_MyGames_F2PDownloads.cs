using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000245 RID: 581
public class Menu_Stats_MyGames_F2PDownloads : MonoBehaviour
{
	// Token: 0x0600165F RID: 5727 RVA: 0x0000F827 File Offset: 0x0000DA27
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000EB594 File Offset: 0x000E9794
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

	// Token: 0x06001661 RID: 5729 RVA: 0x0000F82F File Offset: 0x0000DA2F
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000EB65C File Offset: 0x000E985C
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

	// Token: 0x06001663 RID: 5731 RVA: 0x000EB3D8 File Offset: 0x000E95D8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_Sells>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0000F867 File Offset: 0x0000DA67
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0000F86F File Offset: 0x0000DA6F
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000EB6A8 File Offset: 0x000E98A8
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

	// Token: 0x06001667 RID: 5735 RVA: 0x0000F87D File Offset: 0x0000DA7D
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.schublade && script_.gameTyp == 2;
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x0000F8AB File Offset: 0x0000DAAB
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
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
