using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000083 RID: 131
public class Item_DevGame_EngineFeature : MonoBehaviour
{
	// Token: 0x0600055A RID: 1370 RVA: 0x00048D19 File Offset: 0x00046F19
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00048D24 File Offset: 0x00046F24
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.engineFeatures_PICTYP[this.eF_.engineFeatures_TYP[this.myID]];
		this.uiObjects[3].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[this.myID].ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eF_.GetDevCosts(this.myID), true);
		this.guiMain_.DrawStars(this.uiObjects[4], this.eF_.engineFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.eF_.GetTooltip(this.myID);
		if (this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().g_GameEngineFeature[this.eF_.engineFeatures_TYP[this.myID]] == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00048E74 File Offset: 0x00047074
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetEngineFeature(this.eF_.engineFeatures_TYP[this.myID], this.myID);
		this.guiMain_.uiObjects[67].GetComponent<Menu_DevGame_EngineFeature>().BUTTON_Close();
	}

	// Token: 0x04000861 RID: 2145
	public int myID;

	// Token: 0x04000862 RID: 2146
	public GameObject[] uiObjects;

	// Token: 0x04000863 RID: 2147
	public mainScript mS_;

	// Token: 0x04000864 RID: 2148
	public textScript tS_;

	// Token: 0x04000865 RID: 2149
	public sfxScript sfx_;

	// Token: 0x04000866 RID: 2150
	public engineFeatures eF_;

	// Token: 0x04000867 RID: 2151
	public GUI_Main guiMain_;

	// Token: 0x04000868 RID: 2152
	public tooltip tooltip_;
}
