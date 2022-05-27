using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A8 RID: 168
public class Item_DevKonsole_HardwareFeature : MonoBehaviour
{
	// Token: 0x06000633 RID: 1587 RVA: 0x0000596C File Offset: 0x00003B6C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x00061900 File Offset: 0x0005FB00
	private void Update()
	{
		if (this.menu_.hwFeatures[this.myID])
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00061950 File Offset: 0x0005FB50
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeatures_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.hardwareFeatures_.GetDevCosts(this.myID), true);
		if (this.hardwareFeatures_.hardFeat_NEEDINTERNET[this.myID])
		{
			this.uiObjects[2].SetActive(true);
			if (!this.menu_.uiObjects[53].GetComponent<Toggle>().isOn)
			{
				base.GetComponent<Button>().interactable = false;
				this.menu_.hwFeatures[this.myID] = false;
			}
		}
		if (this.hardwareFeatures_.hardFeat_ONLYSTATIONARY[this.myID])
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			if (this.menu_.platformTyp == 2)
			{
				base.GetComponent<Button>().interactable = false;
				this.menu_.hwFeatures[this.myID] = false;
			}
		}
		if (this.hardwareFeatures_.hardFeat_ONLYHANDHELD[this.myID])
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			if (this.menu_.platformTyp == 1)
			{
				base.GetComponent<Button>().interactable = false;
				this.menu_.hwFeatures[this.myID] = false;
			}
		}
		this.tooltip_.c = this.hardwareFeatures_.GetTooltip(this.myID);
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x00061AEC File Offset: 0x0005FCEC
	public void BUTTON_Click()
	{
		if (base.GetComponent<Button>().interactable)
		{
			this.sfx_.PlaySound(3, true);
			this.menu_.hwFeatures[this.myID] = !this.menu_.hwFeatures[this.myID];
			this.menu_.UpdateGUI();
		}
	}

	// Token: 0x040009C1 RID: 2497
	public int myID;

	// Token: 0x040009C2 RID: 2498
	public GameObject[] uiObjects;

	// Token: 0x040009C3 RID: 2499
	public mainScript mS_;

	// Token: 0x040009C4 RID: 2500
	public textScript tS_;

	// Token: 0x040009C5 RID: 2501
	public sfxScript sfx_;

	// Token: 0x040009C6 RID: 2502
	public hardwareFeatures hardwareFeatures_;

	// Token: 0x040009C7 RID: 2503
	public GUI_Main guiMain_;

	// Token: 0x040009C8 RID: 2504
	public tooltip tooltip_;

	// Token: 0x040009C9 RID: 2505
	public Menu_Dev_Konsole menu_;
}
