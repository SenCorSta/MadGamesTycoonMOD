using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000078 RID: 120
public class Item_ContractAuftragsspiel : MonoBehaviour
{
	// Token: 0x0600050D RID: 1293 RVA: 0x00046A94 File Offset: 0x00044C94
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00046A9C File Offset: 0x00044C9C
	private void Update()
	{
		if (!this.game_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (this.game_.isOnMarket || !this.game_.auftragsspiel)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00046AF0 File Offset: 0x00044CF0
	private void MultiplayerUpdate()
	{
		if (this.platformScript_ && this.platformScript_.inBesitz && this.platformScript_.OwnerIsNPC())
		{
			this.uiObjects[8].GetComponent<Image>().color = Color.white;
		}
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

	// Token: 0x06000510 RID: 1296 RVA: 0x00046B7C File Offset: 0x00044D7C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetTooltip(this.game_.maingenre);
		this.uiObjects[2].GetComponent<Image>().sprite = this.games_.gameSizeSprites[this.game_.gameSize];
		GameObject gameObject = GameObject.Find("PUB_" + this.game_.publisherID.ToString());
		if (gameObject)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = gameObject.GetComponent<publisherScript>().GetLogo();
		}
		string text = this.tS_.GetText(605);
		text = text.Replace("<NUM>", this.game_.auftragsspiel_zeitInWochen.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
		text = this.tS_.GetText(626);
		text = text.Replace("<NUM>", this.game_.auftragsspiel_mindestbewertung.ToString());
		this.uiObjects[5].GetComponent<Text>().text = text;
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(600) + ": " + this.mS_.GetMoney((long)this.game_.auftragsspiel_gehalt, true);
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(627) + ": " + this.mS_.GetMoney((long)this.game_.auftragsspiel_bonus, true);
		if (!this.mS_.genres_.IsErforscht(this.game_.maingenre))
		{
			this.uiObjects[1].GetComponent<Image>().color = Color.red;
		}
		if (this.game_.gameSize == 1 && !this.mS_.forschungSonstiges_.IsErforscht(0))
		{
			this.uiObjects[2].GetComponent<Image>().color = Color.red;
		}
		if (this.game_.gameSize == 2 && !this.mS_.forschungSonstiges_.IsErforscht(1))
		{
			this.uiObjects[2].GetComponent<Image>().color = Color.red;
		}
		if (this.game_.gameSize == 3 && !this.mS_.forschungSonstiges_.IsErforscht(2))
		{
			this.uiObjects[2].GetComponent<Image>().color = Color.red;
		}
		if (this.game_.gameSize == 4 && !this.mS_.forschungSonstiges_.IsErforscht(3))
		{
			this.uiObjects[2].GetComponent<Image>().color = Color.red;
		}
		if (!this.platformScript_)
		{
			gameObject = GameObject.Find("PLATFORM_" + this.game_.gamePlatform[0].ToString());
			if (gameObject)
			{
				this.platformScript_ = gameObject.GetComponent<platformScript>();
				this.platformScript_.SetPic(this.uiObjects[8]);
				this.uiObjects[8].GetComponent<tooltip>().c = this.platformScript_.GetTooltip();
				if (!this.platformScript_.inBesitz && this.platformScript_.OwnerIsNPC())
				{
					this.uiObjects[8].GetComponent<Image>().color = Color.red;
				}
			}
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00046F2C File Offset: 0x0004512C
	public void BUTTON_Click()
	{
		if (!this.mS_.genres_.IsErforscht(this.game_.maingenre))
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1813), false);
			return;
		}
		if (this.game_.gameSize == 1 && !this.mS_.forschungSonstiges_.IsErforscht(0))
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1814), false);
			return;
		}
		if (this.game_.gameSize == 2 && !this.mS_.forschungSonstiges_.IsErforscht(1))
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1814), false);
			return;
		}
		if (this.game_.gameSize == 3 && !this.mS_.forschungSonstiges_.IsErforscht(2))
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1814), false);
			return;
		}
		if (this.game_.gameSize == 4 && !this.mS_.forschungSonstiges_.IsErforscht(3))
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1814), false);
			return;
		}
		if (this.platformScript_.inBesitz)
		{
			if (this.game_.auftragsspiel)
			{
				base.gameObject.SetActive(false);
				this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
				this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitContractGame(this.rS_, this.game_);
				this.guiMain_.uiObjects[99].SetActive(false);
				return;
			}
		}
		else
		{
			this.guiMain_.MessageBox(this.tS_.GetText(631), false);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x000470FF File Offset: 0x000452FF
	public void BUTTON_Remove()
	{
		this.sfx_.PlaySound(3, true);
		this.game_.auftragsspiel_Inivs = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040007F5 RID: 2037
	public GameObject[] uiObjects;

	// Token: 0x040007F6 RID: 2038
	public mainScript mS_;

	// Token: 0x040007F7 RID: 2039
	public textScript tS_;

	// Token: 0x040007F8 RID: 2040
	public sfxScript sfx_;

	// Token: 0x040007F9 RID: 2041
	public GUI_Main guiMain_;

	// Token: 0x040007FA RID: 2042
	public tooltip tooltip_;

	// Token: 0x040007FB RID: 2043
	public genres genres_;

	// Token: 0x040007FC RID: 2044
	public roomScript rS_;

	// Token: 0x040007FD RID: 2045
	public games games_;

	// Token: 0x040007FE RID: 2046
	public gameScript game_;

	// Token: 0x040007FF RID: 2047
	private platformScript platformScript_;

	// Token: 0x04000800 RID: 2048
	private float updateTimer;
}
