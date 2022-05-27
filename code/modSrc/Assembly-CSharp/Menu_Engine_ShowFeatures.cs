using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000190 RID: 400
public class Menu_Engine_ShowFeatures : MonoBehaviour
{
	// Token: 0x06000F25 RID: 3877 RVA: 0x0000AC1B File Offset: 0x00008E1B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x000AE674 File Offset: 0x000AC874
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0000AC23 File Offset: 0x00008E23
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x000AE73C File Offset: 0x000AC93C
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

	// Token: 0x06000F29 RID: 3881 RVA: 0x0000AC5B File Offset: 0x00008E5B
	public void Init(engineScript s)
	{
		this.eS_ = s;
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x000AE788 File Offset: 0x000AC988
	private void SetData()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(4) + " " + this.eS_.GetTechLevel().ToString();
		this.CreateItems(this.eF_.GetTypGrafik(), "<color=white>" + this.tS_.GetText(9) + "</color>");
		this.CreateItems(this.eF_.GetTypSound(), "<color=white>" + this.tS_.GetText(10) + "</color>");
		this.CreateItems(this.eF_.GetTypKI(), "<color=white>" + this.tS_.GetText(11) + "</color>");
		this.CreateItems(this.eF_.GetTypPhysik(), "<color=white>" + this.tS_.GetText(12) + "</color>");
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x000AE8DC File Offset: 0x000ACADC
	private void CreateItems(int typ_, string title_)
	{
		this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).transform.GetChild(0).GetComponent<Text>().text = title_;
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
		for (int i = 0; i < this.eS_.features.Length; i++)
		{
			if (this.eS_.features[i] && this.eF_.engineFeatures_TYP[i] == typ_)
			{
				Item_EngineFeature component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_EngineFeature>();
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.eF_ = this.eF_;
			}
		}
		if (this.uiObjects[0].transform.childCount % 2 != 0)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
		}
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0000AC70 File Offset: 0x00008E70
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400137C RID: 4988
	public engineScript eS_;

	// Token: 0x0400137D RID: 4989
	private mainScript mS_;

	// Token: 0x0400137E RID: 4990
	private GameObject main_;

	// Token: 0x0400137F RID: 4991
	private GUI_Main guiMain_;

	// Token: 0x04001380 RID: 4992
	private sfxScript sfx_;

	// Token: 0x04001381 RID: 4993
	private textScript tS_;

	// Token: 0x04001382 RID: 4994
	private engineFeatures eF_;

	// Token: 0x04001383 RID: 4995
	public GameObject[] uiPrefabs;

	// Token: 0x04001384 RID: 4996
	public GameObject[] uiObjects;

	// Token: 0x04001385 RID: 4997
	private float updateTimer;
}
