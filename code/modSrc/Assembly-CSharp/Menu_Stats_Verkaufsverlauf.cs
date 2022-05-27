using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025E RID: 606
public class Menu_Stats_Verkaufsverlauf : MonoBehaviour
{
	// Token: 0x06001787 RID: 6023 RVA: 0x00010641 File Offset: 0x0000E841
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000F3C6C File Offset: 0x000F1E6C
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x00010649 File Offset: 0x0000E849
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x00010651 File Offset: 0x0000E851
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000F3D54 File Offset: 0x000F1F54
	public void Init()
	{
		this.FindScripts();
		long i = this.InitBalken();
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(703) + ": " + this.mS_.GetMoney(i, false);
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000F3DA8 File Offset: 0x000F1FA8
	private long InitBalken()
	{
		long num = 0L;
		float num2 = 400f;
		long num3 = 0L;
		for (int i = 0; i < this.mS_.verkaufsverlauf.Length; i++)
		{
			num += this.mS_.verkaufsverlauf[i];
			if (num3 < this.mS_.verkaufsverlauf[i])
			{
				num3 = this.mS_.verkaufsverlauf[i];
			}
		}
		float num4 = num2 / (float)num3;
		for (int j = 0; j < this.mS_.verkaufsverlauf.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.verkaufsverlauf[j] * num4 / num2;
			if (this.mS_.verkaufsverlauf[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.verkaufsverlauf[j], false);
				if (j < this.mS_.verkaufsverlauf.Length - 1)
				{
					long num5 = this.mS_.verkaufsverlauf[j] - this.mS_.verkaufsverlauf[j + 1];
					if (num5 > 0L)
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "+" + this.mS_.GetMoney(num5, false);
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().color = this.guiMain_.colors[4];
					}
					else
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = this.mS_.GetMoney(num5, false);
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().color = this.guiMain_.colors[8];
					}
				}
			}
			else
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = "";
				this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "";
			}
		}
		return num;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x00010667 File Offset: 0x0000E867
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001B5D RID: 7005
	public GameObject[] uiBalken;

	// Token: 0x04001B5E RID: 7006
	public GameObject[] uiObjects;

	// Token: 0x04001B5F RID: 7007
	private GameObject main_;

	// Token: 0x04001B60 RID: 7008
	private mainScript mS_;

	// Token: 0x04001B61 RID: 7009
	private textScript tS_;

	// Token: 0x04001B62 RID: 7010
	private GUI_Main guiMain_;

	// Token: 0x04001B63 RID: 7011
	private sfxScript sfx_;

	// Token: 0x04001B64 RID: 7012
	private genres genres_;

	// Token: 0x04001B65 RID: 7013
	private engineFeatures eF_;

	// Token: 0x04001B66 RID: 7014
	private engineScript eS_;
}
