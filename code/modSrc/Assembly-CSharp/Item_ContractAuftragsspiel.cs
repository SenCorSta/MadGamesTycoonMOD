using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_ContractAuftragsspiel : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
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

	
	private void MultiplayerUpdate()
	{
		if (this.platformScript_ && this.platformScript_.inBesitz && !this.platformScript_.playerConsole && this.platformScript_.multiplaySlot == -1)
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
				if (!this.platformScript_.inBesitz && !this.platformScript_.playerConsole && this.platformScript_.multiplaySlot == -1)
				{
					this.uiObjects[8].GetComponent<Image>().color = Color.red;
				}
			}
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
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

	
	public void BUTTON_Remove()
	{
		this.sfx_.PlaySound(3, true);
		this.game_.auftragsspiel_Inivs = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public genres genres_;

	
	public roomScript rS_;

	
	public games games_;

	
	public gameScript game_;

	
	private platformScript platformScript_;

	
	private float updateTimer;
}
