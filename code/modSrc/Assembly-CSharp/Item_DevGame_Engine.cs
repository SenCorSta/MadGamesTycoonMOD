using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000082 RID: 130
public class Item_DevGame_Engine : MonoBehaviour
{
	// Token: 0x0600054B RID: 1355 RVA: 0x0000540C File Offset: 0x0000360C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0005BC1C File Offset: 0x00059E1C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.eS_.SetSpezialPlatformSprite(this.uiObjects[4]);
		this.uiObjects[3].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
		string text = this.tS_.GetText(160) + ": " + this.eS_.GetFeaturesAmount().ToString();
		if (this.eS_.playerEngine)
		{
			text = text + "\n" + this.tS_.GetText(262);
			this.uiObjects[0].GetComponent<Text>().color = Color.green;
		}
		else
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(260),
				": ",
				this.eS_.gewinnbeteiligung.ToString(),
				"%"
			});
		}
		this.uiObjects[2].GetComponent<Text>().text = text;
		if (this.eS_.myID == this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().g_GameEngine)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0005BDD0 File Offset: 0x00059FD0
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

	// Token: 0x0600054F RID: 1359 RVA: 0x0005BE1C File Offset: 0x0005A01C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetEngine(this.eS_.myID);
		this.guiMain_.uiObjects[65].GetComponent<Menu_DevGame_Engine>().BUTTON_Close();
	}

	// Token: 0x04000856 RID: 2134
	public engineScript eS_;

	// Token: 0x04000857 RID: 2135
	public GameObject[] uiObjects;

	// Token: 0x04000858 RID: 2136
	public mainScript mS_;

	// Token: 0x04000859 RID: 2137
	public textScript tS_;

	// Token: 0x0400085A RID: 2138
	public sfxScript sfx_;

	// Token: 0x0400085B RID: 2139
	public engineFeatures eF_;

	// Token: 0x0400085C RID: 2140
	public genres genres_;

	// Token: 0x0400085D RID: 2141
	public GUI_Main guiMain_;

	// Token: 0x0400085E RID: 2142
	public tooltip tooltip_;

	// Token: 0x0400085F RID: 2143
	public Menu_DevGame mDevGame_;

	// Token: 0x04000860 RID: 2144
	private float updateTimer;
}
