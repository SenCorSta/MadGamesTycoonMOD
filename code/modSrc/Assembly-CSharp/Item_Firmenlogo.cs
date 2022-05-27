using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_Firmenlogo : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		if (this.guiMain_.uiObjects[47].activeSelf && this.mS_.GetCompanyLogoID() == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		if (this.guiMain_.uiObjects[159].activeSelf && this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().logo == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		if (this.guiMain_.uiObjects[201].activeSelf && this.mS_.GetCompanyLogoID() == this.myID)
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
		if (this.guiMain_.uiObjects[47].activeSelf)
		{
			this.guiMain_.uiObjects[47].GetComponent<Menu_Firmenname>().SetLogo(this.myID);
			this.guiMain_.uiObjects[48].SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[159].activeSelf)
		{
			this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().SetLogo(this.myID);
			this.guiMain_.uiObjects[48].SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().SetLogo(this.myID);
			this.guiMain_.uiObjects[48].SetActive(false);
			return;
		}
	}

	
	public int myID = -1;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;
}
