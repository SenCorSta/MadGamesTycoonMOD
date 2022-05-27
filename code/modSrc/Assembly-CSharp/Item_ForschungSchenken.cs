using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A2 RID: 162
public class Item_ForschungSchenken : MonoBehaviour
{
	// Token: 0x06000619 RID: 1561 RVA: 0x0004DC8D File Offset: 0x0004BE8D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0004DC98 File Offset: 0x0004BE98
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

	// Token: 0x0600061B RID: 1563 RVA: 0x0004E17E File Offset: 0x0004C37E
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

	// Token: 0x0600061C RID: 1564 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0004E1B1 File Offset: 0x0004C3B1
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.menu_.selectedForschung = this.myID;
		this.SetData();
	}

	// Token: 0x04000986 RID: 2438
	public int myID;

	// Token: 0x04000987 RID: 2439
	public int art;

	// Token: 0x04000988 RID: 2440
	public GameObject[] uiObjects;

	// Token: 0x04000989 RID: 2441
	public mainScript mS_;

	// Token: 0x0400098A RID: 2442
	public textScript tS_;

	// Token: 0x0400098B RID: 2443
	public sfxScript sfx_;

	// Token: 0x0400098C RID: 2444
	public genres genres_;

	// Token: 0x0400098D RID: 2445
	public themes themes_;

	// Token: 0x0400098E RID: 2446
	public engineFeatures eF_;

	// Token: 0x0400098F RID: 2447
	public gameplayFeatures gF_;

	// Token: 0x04000990 RID: 2448
	public hardware hardware_;

	// Token: 0x04000991 RID: 2449
	public hardwareFeatures hardwareFeature_;

	// Token: 0x04000992 RID: 2450
	public forschungSonstiges fS_;

	// Token: 0x04000993 RID: 2451
	public GUI_Main guiMain_;

	// Token: 0x04000994 RID: 2452
	public tooltip tooltip_;

	// Token: 0x04000995 RID: 2453
	public Menu_MP_ForschungSchenken menu_;

	// Token: 0x04000996 RID: 2454
	private float updateTimer;
}
