using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E5 RID: 229
public class Item_EngineSchenken : MonoBehaviour
{
	// Token: 0x060007B0 RID: 1968 RVA: 0x000564E0 File Offset: 0x000546E0
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x000564E8 File Offset: 0x000546E8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.eS_.SetSpezialPlatformSprite(this.uiObjects[6]);
		this.uiObjects[3].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
		string text = this.tS_.GetText(160) + ": " + this.eS_.GetFeaturesAmount().ToString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(261),
			": ",
			this.eS_.GetGamesAmount().ToString()
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = "";
		if (!this.eS_.sellEngine || this.eS_.OwnerIsNPC())
		{
			this.uiObjects[5].SetActive(false);
		}
		if (this.eS_.sellEngine && this.eS_.myID == this.mS_.myID)
		{
			this.uiObjects[5].SetActive(true);
		}
		if (!this.menu_.selectedEngine)
		{
			base.GetComponent<Image>().color = Color.white;
			return;
		}
		if (this.menu_.selectedEngine.myID == this.eS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x000566F1 File Offset: 0x000548F1
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

	// Token: 0x060007B3 RID: 1971 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00056724 File Offset: 0x00054924
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.menu_.selectedEngine = this.eS_;
		this.SetData();
	}

	// Token: 0x04000BB5 RID: 2997
	public engineScript eS_;

	// Token: 0x04000BB6 RID: 2998
	public GameObject[] uiObjects;

	// Token: 0x04000BB7 RID: 2999
	public mainScript mS_;

	// Token: 0x04000BB8 RID: 3000
	public textScript tS_;

	// Token: 0x04000BB9 RID: 3001
	public sfxScript sfx_;

	// Token: 0x04000BBA RID: 3002
	public engineFeatures eF_;

	// Token: 0x04000BBB RID: 3003
	public genres genres_;

	// Token: 0x04000BBC RID: 3004
	public GUI_Main guiMain_;

	// Token: 0x04000BBD RID: 3005
	public tooltip tooltip_;

	// Token: 0x04000BBE RID: 3006
	public Menu_MP_EngineSchenken menu_;

	// Token: 0x04000BBF RID: 3007
	private float updateTimer;
}
