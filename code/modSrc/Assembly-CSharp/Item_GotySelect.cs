using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CF RID: 207
public class Item_GotySelect : MonoBehaviour
{
	// Token: 0x0600071F RID: 1823 RVA: 0x00053BD6 File Offset: 0x00051DD6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00053BDE File Offset: 0x00051DDE
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00053BE8 File Offset: 0x00051DE8
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00053C34 File Offset: 0x00051E34
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00053D90 File Offset: 0x00051F90
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.isOnMarket && component.typ_budget && component.originalGameID == this.game_.myID)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(1404), false);
					return;
				}
			}
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[275]);
		this.guiMain_.uiObjects[275].GetComponent<Menu_GOTYGamename>().Init(this.game_);
	}

	// Token: 0x04000AF3 RID: 2803
	public gameScript game_;

	// Token: 0x04000AF4 RID: 2804
	public GameObject[] uiObjects;

	// Token: 0x04000AF5 RID: 2805
	public mainScript mS_;

	// Token: 0x04000AF6 RID: 2806
	public textScript tS_;

	// Token: 0x04000AF7 RID: 2807
	public sfxScript sfx_;

	// Token: 0x04000AF8 RID: 2808
	public GUI_Main guiMain_;

	// Token: 0x04000AF9 RID: 2809
	public tooltip tooltip_;

	// Token: 0x04000AFA RID: 2810
	public genres genres_;

	// Token: 0x04000AFB RID: 2811
	public Menu_GOTYSelect menu_;

	// Token: 0x04000AFC RID: 2812
	private float updateTimer;
}
