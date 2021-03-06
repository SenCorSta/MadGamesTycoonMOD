using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_TochterfirmaEngine : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.eS_.SetSpezialPlatformSprite(this.uiObjects[4]);
		this.uiObjects[3].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
		string text = this.tS_.GetText(160) + ": <b>" + this.eS_.GetFeaturesAmount().ToString() + "</b>";
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(261),
			": <b>",
			this.mS_.GetMoney((long)this.eS_.GetGamesAmount(), false),
			"</b>"
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		if (this.eS_.myID == this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().g_GameEngine)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	private void Update()
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

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_engine = this.eS_.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[401].GetComponent<Menu_Stats_TochterfirmaEngine>().BUTTON_Close();
	}

	
	public engineScript eS_;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public engineFeatures eF_;

	
	public genres genres_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public publisherScript pS_;

	
	private float updateTimer;
}
