using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CD RID: 205
public class Item_BudgetSelect : MonoBehaviour
{
	// Token: 0x06000708 RID: 1800 RVA: 0x00005E3A File Offset: 0x0000403A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00005E42 File Offset: 0x00004042
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00065BE0 File Offset: 0x00063DE0
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00065C2C File Offset: 0x00063E2C
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[6].GetComponent<Text>().text = Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		if (this.game_.freigabeBudget > 0)
		{
			string text = this.tS_.GetText(1151);
			text = text.Replace("<NUM>", this.game_.freigabeBudget.ToString());
			this.uiObjects[5].GetComponent<Text>().text = text;
		}
		else
		{
			this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(1158);
		}
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.game_.freigabeBudget > 0)
		{
			base.gameObject.GetComponent<Button>().interactable = false;
		}
		else
		{
			base.gameObject.GetComponent<Button>().interactable = true;
		}
		if (this.mS_.multiplayer && !this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00065E4C File Offset: 0x0006404C
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
				if (component && component.isOnMarket && component.typ_goty && component.originalGameID == this.game_.myID)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(1405), false);
					return;
				}
			}
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[228]);
		this.guiMain_.uiObjects[228].GetComponent<Menu_BudgetGamename>().Init(this.game_);
	}

	// Token: 0x04000ADF RID: 2783
	public gameScript game_;

	// Token: 0x04000AE0 RID: 2784
	public GameObject[] uiObjects;

	// Token: 0x04000AE1 RID: 2785
	public mainScript mS_;

	// Token: 0x04000AE2 RID: 2786
	public textScript tS_;

	// Token: 0x04000AE3 RID: 2787
	public sfxScript sfx_;

	// Token: 0x04000AE4 RID: 2788
	public GUI_Main guiMain_;

	// Token: 0x04000AE5 RID: 2789
	public tooltip tooltip_;

	// Token: 0x04000AE6 RID: 2790
	public genres genres_;

	// Token: 0x04000AE7 RID: 2791
	public Menu_BudgetSelect menu_;

	// Token: 0x04000AE8 RID: 2792
	private float updateTimer;
}
