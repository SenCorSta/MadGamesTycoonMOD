using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_FirmenlogoTochterfirma : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		if (this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().pS_.logoID == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.myID);
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().SetLogo(this.myID);
		this.guiMain_.uiObjects[392].SetActive(false);
	}

	
	public int myID = -1;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;
}
