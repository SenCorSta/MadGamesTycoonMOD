using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MultiplayerView : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
		if (!this.cameraMovement)
		{
			this.cameraMovement = GameObject.Find("CamMovement");
		}
	}

	
	public void Init(int p)
	{
		this.FindScripts();
		this.cameraRotation = this.cameraMovement.transform.eulerAngles;
		this.cameraMovement.transform.eulerAngles = new Vector3(this.cameraMovement.transform.eulerAngles.x, (float)UnityEngine.Random.Range(0, 359), this.cameraMovement.transform.eulerAngles.z);
		this.playerID = p;
		this.SetMainGuiToggles(false);
		this.guiMain_.CameraBlend();
		this.sfx_.PlaySound(58);
		if (p != -1)
		{
			string text = this.tS_.GetText(1277);
			text = text.Replace("<NAME>", this.mpCalls_.GetPlayerName(this.playerID));
			this.uiObjects[1].GetComponent<Text>().text = text;
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(true);
		}
		for (int i = 0; i < this.mS_.arrayCharacters.Length; i++)
		{
			if (this.mS_.arrayCharacters[i])
			{
				this.mS_.arrayCharacters[i].transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(58);
		this.guiMain_.CameraBlend();
		this.playerID = -1;
		this.SetMainGuiToggles(true);
		this.mS_.CloseMultiplayerView();
		this.guiMain_.CloseMenu();
		this.sfx_.PlaySound(3, true);
		this.cameraMovement.transform.eulerAngles = this.cameraRotation;
		base.gameObject.SetActive(false);
	}

	
	private void SetMainGuiToggles(bool b)
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			if (this.uiObjects[0].transform.GetChild(i) && this.uiObjects[0].transform.GetChild(i).GetComponent<Toggle>())
			{
				this.uiObjects[0].transform.GetChild(i).GetComponent<Toggle>().interactable = b;
			}
		}
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private mpCalls mpCalls_;

	
	private GameObject cameraMovement;

	
	public int playerID = -1;

	
	private Vector3 cameraRotation;
}
