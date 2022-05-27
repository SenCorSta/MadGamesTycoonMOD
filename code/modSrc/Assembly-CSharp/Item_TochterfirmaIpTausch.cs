using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000101 RID: 257
public class Item_TochterfirmaIpTausch : MonoBehaviour
{
	// Token: 0x06000853 RID: 2131 RVA: 0x0005A025 File Offset: 0x00058225
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0005A030 File Offset: 0x00058230
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

	// Token: 0x06000855 RID: 2133 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0005A098 File Offset: 0x00058298
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

	// Token: 0x06000857 RID: 2135 RVA: 0x0005A2E0 File Offset: 0x000584E0
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

	// Token: 0x04000C9D RID: 3229
	public GameObject[] uiObjects;

	// Token: 0x04000C9E RID: 3230
	public mainScript mS_;

	// Token: 0x04000C9F RID: 3231
	public textScript tS_;

	// Token: 0x04000CA0 RID: 3232
	public sfxScript sfx_;

	// Token: 0x04000CA1 RID: 3233
	public GUI_Main guiMain_;

	// Token: 0x04000CA2 RID: 3234
	public tooltip tooltip_;

	// Token: 0x04000CA3 RID: 3235
	public gameScript game_;

	// Token: 0x04000CA4 RID: 3236
	public genres genres_;
}
