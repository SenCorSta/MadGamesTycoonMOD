using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000249 RID: 585
public class Menu_Stats_MyGames_Sells : MonoBehaviour
{
	// Token: 0x06001694 RID: 5780 RVA: 0x0000FC53 File Offset: 0x0000DE53
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x000EBFFC File Offset: 0x000EA1FC
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

	// Token: 0x06001696 RID: 5782 RVA: 0x0000FC5B File Offset: 0x0000DE5B
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x000EC0C4 File Offset: 0x000EA2C4
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

	// Token: 0x06001698 RID: 5784 RVA: 0x000EB3D8 File Offset: 0x000E95D8
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

	// Token: 0x06001699 RID: 5785 RVA: 0x0000FC93 File Offset: 0x0000DE93
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x0000FC9B File Offset: 0x0000DE9B
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x000EC110 File Offset: 0x000EA310
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

	// Token: 0x0600169C RID: 5788 RVA: 0x0000FCA9 File Offset: 0x0000DEA9
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.schublade && script_.gameTyp != 2 && !script_.handy && !script_.arcade;
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x0000FCE7 File Offset: 0x0000DEE7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001A81 RID: 6785
	private mainScript mS_;

	// Token: 0x04001A82 RID: 6786
	private GameObject main_;

	// Token: 0x04001A83 RID: 6787
	private GUI_Main guiMain_;

	// Token: 0x04001A84 RID: 6788
	private sfxScript sfx_;

	// Token: 0x04001A85 RID: 6789
	private textScript tS_;

	// Token: 0x04001A86 RID: 6790
	private genres genres_;

	// Token: 0x04001A87 RID: 6791
	public GameObject[] uiPrefabs;

	// Token: 0x04001A88 RID: 6792
	public GameObject[] uiObjects;

	// Token: 0x04001A89 RID: 6793
	private float updateTimer;
}
