using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000244 RID: 580
public class Menu_Stats_MyGames_ArcadeSells : MonoBehaviour
{
	// Token: 0x06001654 RID: 5716 RVA: 0x0000F780 File Offset: 0x0000D980
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x000EB2C4 File Offset: 0x000E94C4
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

	// Token: 0x06001656 RID: 5718 RVA: 0x0000F788 File Offset: 0x0000D988
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000EB38C File Offset: 0x000E958C
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

	// Token: 0x06001658 RID: 5720 RVA: 0x000EB3D8 File Offset: 0x000E95D8
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

	// Token: 0x06001659 RID: 5721 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000EB41C File Offset: 0x000E961C
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

	// Token: 0x0600165C RID: 5724 RVA: 0x0000F7D6 File Offset: 0x0000D9D6
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.schublade && script_.arcade && script_.gameTyp != 2;
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x0000F80C File Offset: 0x0000DA0C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001A57 RID: 6743
	private mainScript mS_;

	// Token: 0x04001A58 RID: 6744
	private GameObject main_;

	// Token: 0x04001A59 RID: 6745
	private GUI_Main guiMain_;

	// Token: 0x04001A5A RID: 6746
	private sfxScript sfx_;

	// Token: 0x04001A5B RID: 6747
	private textScript tS_;

	// Token: 0x04001A5C RID: 6748
	private genres genres_;

	// Token: 0x04001A5D RID: 6749
	public GameObject[] uiPrefabs;

	// Token: 0x04001A5E RID: 6750
	public GameObject[] uiObjects;

	// Token: 0x04001A5F RID: 6751
	private float updateTimer;
}
