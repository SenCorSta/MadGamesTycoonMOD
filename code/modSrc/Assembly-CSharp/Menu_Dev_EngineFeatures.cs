using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010D RID: 269
public class Menu_Dev_EngineFeatures : MonoBehaviour
{
	// Token: 0x060008C0 RID: 2240 RVA: 0x0005F610 File Offset: 0x0005D810
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0005F618 File Offset: 0x0005D818
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

	// Token: 0x060008C2 RID: 2242 RVA: 0x0005F6E0 File Offset: 0x0005D8E0
	private void Update()
	{
		Menu_Dev_Engine component = this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		int num = 0;
		int num2 = 0;
		if (this.uiObjects[0].transform.childCount > 0)
		{
			for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
			{
				if (this.uiObjects[0].transform.GetChild(i).tag != "NoItem")
				{
					Item_DevEngine_Feature component2 = this.uiObjects[0].transform.GetChild(i).gameObject.GetComponent<Item_DevEngine_Feature>();
					if (component2 && component2.activ)
					{
						if (!component.featuresLock[component2.myID])
						{
							num += this.eF_.GetDevCostsForEngine(component2.myID);
						}
						if (num2 < this.eF_.engineFeatures_TECH[component2.myID])
						{
							num2 = this.eF_.engineFeatures_TECH[component2.myID];
						}
					}
				}
			}
		}
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(4) + " " + num2.ToString();
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0005F868 File Offset: 0x0005DA68
	public void Init()
	{
		this.FindScripts();
		this.CreateItems(this.eF_.GetTypGrafik(), "<color=white>" + this.tS_.GetText(9) + "</color>");
		this.CreateItems(this.eF_.GetTypSound(), "<color=white>" + this.tS_.GetText(10) + "</color>");
		this.CreateItems(this.eF_.GetTypKI(), "<color=white>" + this.tS_.GetText(11) + "</color>");
		this.CreateItems(this.eF_.GetTypPhysik(), "<color=white>" + this.tS_.GetText(12) + "</color>");
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0005F94C File Offset: 0x0005DB4C
	private void CreateItems(int typ_, string title_)
	{
		Menu_Dev_Engine component = this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).transform.GetChild(0).GetComponent<Text>().text = title_;
		UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
		for (int i = 0; i < this.eF_.engineFeatures_RES_POINTS.Length; i++)
		{
			if (this.eF_.engineFeatures_UNLOCK[i] && this.eF_.IsErforscht(i) && this.eF_.engineFeatures_TYP[i] == typ_)
			{
				Item_DevEngine_Feature component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevEngine_Feature>();
				component2.myID = i;
				component2.mS_ = this.mS_;
				component2.tS_ = this.tS_;
				component2.sfx_ = this.sfx_;
				component2.guiMain_ = this.guiMain_;
				component2.eF_ = this.eF_;
				if (component.features[i] || component.featuresLock[i])
				{
					component2.activ = true;
				}
			}
		}
		if (this.uiObjects[0].transform.childCount % 2 != 0)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0005FB1F File Offset: 0x0005DD1F
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0005FB1F File Offset: 0x0005DD1F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0005FB3C File Offset: 0x0005DD3C
	public void BUTTON_All_Nothing()
	{
		Menu_Dev_Engine component = this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		this.sfx_.PlaySound(3, true);
		if (this.uiObjects[0].transform.childCount > 0)
		{
			this.activ_All_Nothing = !this.activ_All_Nothing;
			for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
			{
				if (this.uiObjects[0].transform.GetChild(i).tag != "NoItem")
				{
					Item_DevEngine_Feature component2 = this.uiObjects[0].transform.GetChild(i).gameObject.GetComponent<Item_DevEngine_Feature>();
					if (component2 && !component.featuresLock[component2.myID])
					{
						component2.activ = !this.activ_All_Nothing;
						component2.BUTTON_Click();
					}
				}
			}
		}
	}

	// Token: 0x04000D3B RID: 3387
	private mainScript mS_;

	// Token: 0x04000D3C RID: 3388
	private GameObject main_;

	// Token: 0x04000D3D RID: 3389
	private GUI_Main guiMain_;

	// Token: 0x04000D3E RID: 3390
	private sfxScript sfx_;

	// Token: 0x04000D3F RID: 3391
	private textScript tS_;

	// Token: 0x04000D40 RID: 3392
	private engineFeatures eF_;

	// Token: 0x04000D41 RID: 3393
	public int roomID = -1;

	// Token: 0x04000D42 RID: 3394
	private bool activ_All_Nothing;

	// Token: 0x04000D43 RID: 3395
	public GameObject[] uiPrefabs;

	// Token: 0x04000D44 RID: 3396
	public GameObject[] uiObjects;
}
