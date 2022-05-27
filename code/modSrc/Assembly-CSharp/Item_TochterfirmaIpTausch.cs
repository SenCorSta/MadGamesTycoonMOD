using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_TochterfirmaIpTausch : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
		this.guiMain_.DrawIpBekanntheit(this.uiObjects[1], this.game_);
		this.tooltip_.c = this.game_.GetTooltipIP();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		Menu_Stats_TochterfirmaIpTausch component = this.guiMain_.uiObjects[403].GetComponent<Menu_Stats_TochterfirmaIpTausch>();
		if (component)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					gameScript component2 = array[i].GetComponent<gameScript>();
					if (component2 && component2.mainIP == this.game_.mainIP)
					{
						if (component2.inDevelopment || component2.schublade)
						{
							this.guiMain_.MessageBox(this.tS_.GetText(2009), false);
							return;
						}
						if (component2.pubAngebot || component2.auftragsspiel)
						{
							this.guiMain_.MessageBox(this.tS_.GetText(2010), false);
							return;
						}
					}
				}
			}
			if (this.game_.ownerID == component.GetRightPublisher().myID)
			{
				component.GetRightPublisher().firmenwert -= this.game_.GetIpWert();
				component.GetLeftPublisher().firmenwert += this.game_.GetIpWert();
				if (component.GetRightPublisher().firmenwert < 0L)
				{
					component.GetRightPublisher().firmenwert = 0L;
				}
			}
			else
			{
				component.GetLeftPublisher().firmenwert -= this.game_.GetIpWert();
				component.GetRightPublisher().firmenwert += this.game_.GetIpWert();
				if (component.GetLeftPublisher().firmenwert < 0L)
				{
					component.GetLeftPublisher().firmenwert = 0L;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j])
				{
					gameScript component3 = array[j].GetComponent<gameScript>();
					if (component3 && component3.mainIP == this.game_.mainIP)
					{
						if (component3.ownerID == component.GetRightPublisher().myID)
						{
							component3.ownerID = component.GetLeftPublisher().myID;
						}
						else
						{
							component3.ownerID = component.GetRightPublisher().myID;
						}
					}
				}
			}
			component.SetDataLeft();
			component.SetDataRight();
			this.RemoveIpFokus(this.game_.mainIP);
		}
	}

	
	public void RemoveIpFokus(int ipID)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component)
				{
					for (int j = 0; j < component.tf_ipFocus.Length; j++)
					{
						if (component.tf_ipFocus[j] == ipID)
						{
							component.tf_ipFocus[j] = -1;
						}
					}
				}
			}
		}
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public gameScript game_;

	
	public genres genres_;
}
