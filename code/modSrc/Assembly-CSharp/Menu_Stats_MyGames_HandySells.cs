using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000247 RID: 583
public class Menu_Stats_MyGames_HandySells : MonoBehaviour
{
	// Token: 0x06001677 RID: 5751 RVA: 0x0000F95B File Offset: 0x0000DB5B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000EBC70 File Offset: 0x000E9E70
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

	// Token: 0x06001679 RID: 5753 RVA: 0x0000F963 File Offset: 0x0000DB63
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000EBD38 File Offset: 0x000E9F38
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

	// Token: 0x0600167B RID: 5755 RVA: 0x000EB3D8 File Offset: 0x000E95D8
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

	// Token: 0x0600167C RID: 5756 RVA: 0x0000F99B File Offset: 0x0000DB9B
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x0000F9A3 File Offset: 0x0000DBA3
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000EBD84 File Offset: 0x000E9F84
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

	// Token: 0x0600167F RID: 5759 RVA: 0x0000F9B1 File Offset: 0x0000DBB1
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && !script_.schublade && script_.handy && script_.gameTyp != 2;
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001A71 RID: 6769
	private mainScript mS_;

	// Token: 0x04001A72 RID: 6770
	private GameObject main_;

	// Token: 0x04001A73 RID: 6771
	private GUI_Main guiMain_;

	// Token: 0x04001A74 RID: 6772
	private sfxScript sfx_;

	// Token: 0x04001A75 RID: 6773
	private textScript tS_;

	// Token: 0x04001A76 RID: 6774
	private genres genres_;

	// Token: 0x04001A77 RID: 6775
	public GameObject[] uiPrefabs;

	// Token: 0x04001A78 RID: 6776
	public GameObject[] uiObjects;

	// Token: 0x04001A79 RID: 6777
	private float updateTimer;
}
