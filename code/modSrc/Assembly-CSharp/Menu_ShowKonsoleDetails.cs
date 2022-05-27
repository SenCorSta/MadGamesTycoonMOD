using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_ShowKonsoleDetails : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		if (this.uiObjects[35].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[36].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	public void Init(platformScript plat_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.pS_.SetPic(this.uiObjects[2]);
		if (!this.pS_.internet)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
		}
		else
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
		}
		this.uiObjects[4].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		this.uiObjects[5].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(1612) + ": <b><color=blue>" + this.mS_.GetMoney((long)this.pS_.performancePoints, false) + "</color></b>";
		this.uiObjects[7].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_cpu);
		this.uiObjects[8].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_cpu].ToString();
		this.uiObjects[9].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_ram);
		this.uiObjects[10].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_ram].ToString();
		this.uiObjects[11].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_gfx);
		this.uiObjects[12].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_gfx].ToString();
		this.uiObjects[13].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_sfx);
		this.uiObjects[14].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_sfx].ToString();
		this.uiObjects[15].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_hdd);
		this.uiObjects[16].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_hdd].ToString();
		this.uiObjects[17].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_disc);
		this.uiObjects[18].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_disc].ToString();
		if (this.pS_.component_cooling != -1)
		{
			this.uiObjects[19].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_cooling);
			this.uiObjects[20].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_cooling].ToString();
			this.uiObjects[25].SetActive(false);
		}
		else
		{
			this.uiObjects[19].GetComponent<Text>().text = "-";
			this.uiObjects[20].GetComponent<Text>().text = "-";
			this.uiObjects[25].SetActive(true);
		}
		if (this.pS_.component_monitor != -1)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_monitor);
			this.uiObjects[22].GetComponent<Text>().text = this.hardware_.hardware_TECH[this.pS_.component_monitor].ToString();
			this.uiObjects[26].SetActive(false);
		}
		else
		{
			this.uiObjects[21].GetComponent<Text>().text = "-";
			this.uiObjects[22].GetComponent<Text>().text = "-";
			this.uiObjects[26].SetActive(true);
		}
		this.uiObjects[23].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_case);
		if (this.pS_.typ == 1)
		{
			this.uiObjects[29].GetComponent<Image>().sprite = this.guiMain_.uiSprites[42];
		}
		if (this.pS_.typ == 2)
		{
			this.uiObjects[29].GetComponent<Image>().sprite = this.guiMain_.uiSprites[43];
		}
		if (this.pS_.component_controller != -1)
		{
			this.uiObjects[24].GetComponent<Text>().text = this.hardware_.GetName(this.pS_.component_controller);
			this.uiObjects[28].GetComponent<Text>().text = this.pS_.anzController.ToString();
			this.uiObjects[27].SetActive(false);
		}
		else
		{
			this.uiObjects[24].GetComponent<Text>().text = "-";
			this.uiObjects[28].GetComponent<Text>().text = "-";
			this.uiObjects[27].SetActive(true);
		}
		if (this.pS_.gameID != -1)
		{
			this.pS_.FindMyGame();
			if (this.pS_.vorinstalledGame_)
			{
				this.uiObjects[30].GetComponent<Text>().text = this.pS_.vorinstalledGame_.GetNameWithTag();
			}
		}
		else
		{
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(1615);
		}
		for (int i = 0; i < this.uiObjects[34].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[34].transform.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.pS_.hwFeatures.Length; j++)
		{
			if (this.pS_.hwFeatures[j])
			{
				Item_HardwareFeatureShow component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[34].transform).GetComponent<Item_HardwareFeatureShow>();
				component.myID = j;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.hardwareFeatures_ = this.hardwareFeatures_;
			}
		}
		base.StartCoroutine(this.ResizeKonsolenFeatures());
	}

	
	private IEnumerator ResizeKonsolenFeatures()
	{
		this.uiObjects[34].SetActive(false);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.uiObjects[34].SetActive(true);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.uiObjects[38].SetActive(false);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.uiObjects[38].SetActive(true);
		yield break;
	}

	
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[340].SetActive(true);
		this.guiMain_.uiObjects[340].GetComponent<Menu_ShowKonsoleGames>().Init(this.pS_);
	}

	
	public void BUTTON_Close()
	{
		for (int i = 0; i < this.uiObjects[34].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[34].transform.GetChild(i).gameObject);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private platforms platforms_;

	
	private platformScript pS_;
}
