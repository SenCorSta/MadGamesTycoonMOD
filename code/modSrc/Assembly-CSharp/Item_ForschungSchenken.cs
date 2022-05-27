using System;
using UnityEngine;
using UnityEngine.UI;


public class Item_ForschungSchenken : MonoBehaviour
{
	
	private void Start()
	{
		this.SetData();
	}

	
	private void SetData()
	{
		switch (this.art)
		{
		case 0:
			this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
			this.tooltip_.c = this.genres_.GetTooltip(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].genres[this.myID];
			break;
		case 1:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetThemes(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.uiSprites[6];
			this.tooltip_.c = this.tS_.GetThemes(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].themes[this.myID];
			break;
		case 2:
			this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.engineFeatures_PICTYP[this.eF_.engineFeatures_TYP[this.myID]];
			this.tooltip_.c = this.eF_.GetDesc(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].engineFeatures[this.myID];
			break;
		case 3:
			this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
			this.tooltip_.c = this.gF_.GetDesc(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].gameplayFeatures[this.myID];
			break;
		case 4:
			this.uiObjects[0].GetComponent<Text>().text = this.hardware_.GetName(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.hardware_.GetTypPic(this.myID);
			this.tooltip_.c = this.hardware_.GetTooltip(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].hardware[this.myID];
			break;
		case 5:
			this.uiObjects[0].GetComponent<Text>().text = this.fS_.GetName(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.fS_.GetPic(this.myID);
			this.tooltip_.c = this.fS_.GetTooltip(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].forschungSonstiges[this.myID];
			break;
		case 6:
			this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeature_.GetName(this.myID);
			this.uiObjects[1].GetComponent<Image>().sprite = this.hardwareFeature_.GetSprite(this.myID);
			this.tooltip_.c = this.hardwareFeature_.GetTooltip(this.myID);
			base.GetComponent<Button>().interactable = !this.mS_.mpCalls_.playersMP[this.menu_.selectedPlayer].hardwareFeatures[this.myID];
			break;
		}
		if (this.menu_.selectedForschung == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.menu_.selectedForschung = this.myID;
		this.SetData();
	}

	
	public int myID;

	
	public int art;

	
	public GameObject[] uiObjects;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public sfxScript sfx_;

	
	public genres genres_;

	
	public themes themes_;

	
	public engineFeatures eF_;

	
	public gameplayFeatures gF_;

	
	public hardware hardware_;

	
	public hardwareFeatures hardwareFeature_;

	
	public forschungSonstiges fS_;

	
	public GUI_Main guiMain_;

	
	public tooltip tooltip_;

	
	public Menu_MP_ForschungSchenken menu_;

	
	private float updateTimer;
}
