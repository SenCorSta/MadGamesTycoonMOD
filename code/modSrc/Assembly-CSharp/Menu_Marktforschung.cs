using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Marktforschung : MonoBehaviour
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
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	private void Update()
	{
	}

	
	public void Init(roomScript roomS_)
	{
		this.FindScripts();
		this.rS_ = roomS_;
		if (!this.rS_)
		{
			this.uiObjects[13].SetActive(false);
			this.uiObjects[14].SetActive(true);
		}
		else
		{
			this.uiObjects[13].SetActive(true);
			this.uiObjects[14].SetActive(false);
		}
		if (this.mS_.marktforschung_retail <= 0f)
		{
			this.uiObjects[0].SetActive(true);
			this.uiObjects[1].SetActive(false);
			return;
		}
		this.uiObjects[0].SetActive(false);
		this.uiObjects[1].SetActive(true);
		string text = this.tS_.GetText(1167);
		text = text.Replace("<NAME>", this.mS_.marktforschung_datum);
		this.uiObjects[6].GetComponent<Text>().text = text;
		this.SetPlatform(this.mS_.marktforschung_bestPlattform, 15);
		this.SetPlatform(this.mS_.marktforschung_bestPlattformKonsole, 16);
		this.SetPlatform(this.mS_.marktforschung_bestPlattformHandheld, 17);
		this.SetPlatform(this.mS_.marktforschung_bestPlattformHandy, 18);
		this.SetPlatform(-1, 19);
		this.SetPlatform(-1, 20);
		this.SetPlatform(-1, 21);
		this.SetPlatform(-1, 22);
		if (this.mS_.marktforschung_bestPlattform != this.mS_.marktforschung_badPlattform)
		{
			this.SetPlatform(this.mS_.marktforschung_badPlattform, 19);
		}
		if (this.mS_.marktforschung_badPlattformKonsole != this.mS_.marktforschung_badPlattformKonsole)
		{
			this.SetPlatform(this.mS_.marktforschung_badPlattformKonsole, 20);
		}
		if (this.mS_.marktforschung_bestPlattformHandheld != this.mS_.marktforschung_badPlattformHandheld)
		{
			this.SetPlatform(this.mS_.marktforschung_badPlattformHandheld, 21);
		}
		if (this.mS_.marktforschung_bestPlattformHandy != this.mS_.marktforschung_badPlattformHandy)
		{
			this.SetPlatform(this.mS_.marktforschung_badPlattformHandy, 22);
		}
		text = this.tS_.GetText(1169);
		text = text.Replace("<NUM1>", this.mS_.Round(this.mS_.marktforschung_retail * 100f, 1) + "%");
		text = text.Replace("<NUM2>", this.mS_.Round(this.mS_.marktforschung_digtal * 100f, 1) + "%");
		this.uiObjects[7].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1170);
		text = text.Replace("<NUM1>", this.mS_.Round(this.mS_.marktforschung_deluxe * 100f, 1) + "%");
		text = text.Replace("<NUM2>", this.mS_.Round(this.mS_.marktforschung_collectors * 100f, 1) + "%");
		this.uiObjects[8].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1171);
		text = text.Replace("<NAME1>", this.genres_.GetName(this.mS_.marktforschung_nextGenre));
		text = text.Replace("<NAME2>", this.tS_.GetThemes(this.mS_.marktforschung_nextTopic));
		this.uiObjects[10].GetComponent<Text>().text = text;
		this.uiObjects[9].GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.marktforschung_nextGenre);
		text = this.tS_.GetText(1172);
		text = text.Replace("<NAME1>", this.genres_.GetName(this.mS_.marktforschung_nextBadGenre));
		text = text.Replace("<NAME2>", this.tS_.GetThemes(this.mS_.marktforschung_nextBadTopic));
		this.uiObjects[12].GetComponent<Text>().text = text;
		this.uiObjects[11].GetComponent<Image>().sprite = this.genres_.GetPic(this.mS_.marktforschung_nextBadGenre);
		text = this.tS_.GetText(1533);
		text = text.Replace("<NUM>", Mathf.RoundToInt(this.mS_.marktforschung_arcade * 100f).ToString() + "%");
		this.uiObjects[23].GetComponent<Text>().text = text;
	}

	
	private void SetPlatform(int id, int objectSlot)
	{
		GameObject gameObject = null;
		if (id != -1)
		{
			gameObject = GameObject.Find("PLATFORM_" + id.ToString());
		}
		if (gameObject)
		{
			this.uiObjects[objectSlot].SetActive(true);
			platformScript component = gameObject.GetComponent<platformScript>();
			component.SetPic(this.uiObjects[objectSlot]);
			this.uiObjects[objectSlot].GetComponent<tooltip>().c = component.GetTooltip();
			return;
		}
		this.uiObjects[objectSlot].SetActive(false);
		this.uiObjects[objectSlot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
		this.uiObjects[objectSlot].GetComponent<tooltip>().c = "";
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_StarteMarktforschung()
	{
		if (!this.rS_)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		taskMarktforschung taskMarktforschung = this.guiMain_.AddTask_Marktforschung();
		taskMarktforschung.Init(false);
		taskMarktforschung.points = 100f;
		taskMarktforschung.pointsLeft = 100f;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskMarktforschung.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private unlockScript unlock_;

	
	private genres genres_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private roomScript rS_;
}
