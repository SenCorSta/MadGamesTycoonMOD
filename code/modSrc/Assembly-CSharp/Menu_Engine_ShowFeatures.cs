using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000191 RID: 401
public class Menu_Engine_ShowFeatures : MonoBehaviour
{
	// Token: 0x06000F3D RID: 3901 RVA: 0x000A17A0 File Offset: 0x0009F9A0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x000A17A8 File Offset: 0x0009F9A8
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

	// Token: 0x06000F3F RID: 3903 RVA: 0x000A1870 File Offset: 0x0009FA70
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x000A18A8 File Offset: 0x0009FAA8
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

	// Token: 0x06000F41 RID: 3905 RVA: 0x000A18F4 File Offset: 0x0009FAF4
	public void Init(engineScript s)
	{
		this.eS_ = s;
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x000A190C File Offset: 0x0009FB0C
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

	// Token: 0x06000F43 RID: 3907 RVA: 0x000A1A60 File Offset: 0x0009FC60
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

	// Token: 0x06000F44 RID: 3908 RVA: 0x000A1C02 File Offset: 0x0009FE02
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001385 RID: 4997
	public engineScript eS_;

	// Token: 0x04001386 RID: 4998
	private mainScript mS_;

	// Token: 0x04001387 RID: 4999
	private GameObject main_;

	// Token: 0x04001388 RID: 5000
	private GUI_Main guiMain_;

	// Token: 0x04001389 RID: 5001
	private sfxScript sfx_;

	// Token: 0x0400138A RID: 5002
	private textScript tS_;

	// Token: 0x0400138B RID: 5003
	private engineFeatures eF_;

	// Token: 0x0400138C RID: 5004
	public GameObject[] uiPrefabs;

	// Token: 0x0400138D RID: 5005
	public GameObject[] uiObjects;

	// Token: 0x0400138E RID: 5006
	private float updateTimer;
}
