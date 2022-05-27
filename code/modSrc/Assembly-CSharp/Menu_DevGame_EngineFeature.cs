using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_EngineFeature : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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
		if (!this.devGame_)
		{
			this.devGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	public void Init(int featureArt_)
	{
		this.featureArt = featureArt_;
		this.FindScripts();
		if (this.devGame_.g_GameEngineScript_)
		{
			this.uiObjects[7].GetComponent<Text>().text = this.devGame_.g_GameEngineScript_.GetTechLevel().ToString();
		}
		this.uiObjects[8].GetComponent<Text>().text = this.devGame_.GetLowestPlatformTechLevel().ToString();
		switch (featureArt_)
		{
		case 0:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(9);
			break;
		case 1:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(10);
			break;
		case 2:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(11);
			break;
		case 3:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(12);
			break;
		}
		this.CreateItems(featureArt_);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	
	private void CreateItems(int typ_)
	{
		if (!this.devGame_.g_GameEngineScript_)
		{
			return;
		}
		for (int i = 0; i < this.devGame_.g_GameEngineScript_.features.Length; i++)
		{
			if (this.devGame_.g_GameEngineScript_.features[i] && this.eF_.engineFeatures_TYP[i] == typ_)
			{
				Item_DevGame_EngineFeature component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_EngineFeature>();
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.eF_ = this.eF_;
			}
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private engineFeatures eF_;

	
	private Menu_DevGame devGame_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private int featureArt;
}
