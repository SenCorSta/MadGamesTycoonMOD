using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CD RID: 461
public class Menu_MesseErgebnis : MonoBehaviour
{
	// Token: 0x06001171 RID: 4465 RVA: 0x000B85AA File Offset: 0x000B67AA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000B85B4 File Offset: 0x000B67B4
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
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>();
		}
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000B8688 File Offset: 0x000B6888
	public void Init()
	{
		this.FindScripts();
		float num = (float)this.mS_.PassedMonth();
		if (num > 600f)
		{
			num = 600f;
		}
		float f = 350000f * this.curveBesucher.Evaluate(num / 600f);
		this.besucherSoll = (float)(Mathf.RoundToInt(f) + 1000 + UnityEngine.Random.Range(0, 1000));
		this.besucherIst = 0f;
		this.besucherGesamt = this.besucherSoll * 1.5f * UnityEngine.Random.Range(1f, 1.1f);
		int standGroesse = this.menu_.standGroesse;
		if (standGroesse != 0)
		{
			if (standGroesse == 1)
			{
				this.besucherSoll *= 0.4f;
			}
		}
		else
		{
			this.besucherSoll *= 0.1f;
		}
		this.neueFans = Mathf.RoundToInt(this.besucherSoll * 0.15f);
		this.uiObjects[2].GetComponent<Text>().text = "";
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).GetComponent<Image>().sprite = this.besucherSprites[0];
		}
		string text = this.tS_.GetText(954);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)Mathf.RoundToInt(this.besucherGesamt), false));
		this.uiObjects[6].GetComponent<Text>().text = text;
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000B881C File Offset: 0x000B6A1C
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
		if (this.besucherSoll != this.besucherIst)
		{
			this.besucherIst = Mathf.Lerp(this.besucherIst, this.besucherSoll, 0.1f);
			if (this.besucherSoll - this.besucherIst < 10f)
			{
				this.besucherIst = this.besucherSoll;
				this.uiObjects[2].GetComponent<Text>().text = "+" + this.mS_.GetMoney((long)this.neueFans, false) + " " + this.tS_.GetText(97);
				this.sfx_.PlaySound(51, false);
			}
			int childCount = this.uiObjects[0].transform.childCount;
			int num = 350000 / childCount;
			for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
			{
				if (this.besucherIst >= (float)(i * num))
				{
					this.uiObjects[0].transform.GetChild(this.uiObjects[0].transform.childCount - 1 - i).GetComponent<Image>().sprite = this.besucherSprites[1];
				}
			}
		}
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.besucherIst), false);
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x000B8988 File Offset: 0x000B6B88
	public void BUTTON_Abbrechen()
	{
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			this.mS_.mpCalls_.CLIENT_Send_Command(1);
		}
		this.sfx_.PlaySound(3, true);
		this.mS_.AddFans(this.neueFans, -1);
		int num = this.mS_.difficulty + 1;
		if (this.menu_.games[0])
		{
			this.menu_.games[0].AddHype((float)UnityEngine.Random.Range(50 / num, 100 / num));
		}
		if (this.menu_.games[1])
		{
			this.menu_.games[1].AddHype((float)UnityEngine.Random.Range(50 / num, 100 / num));
		}
		if (this.menu_.games[2])
		{
			this.menu_.games[2].AddHype((float)UnityEngine.Random.Range(50 / num, 100 / num));
		}
		if (this.menu_.konsolen[0])
		{
			this.menu_.konsolen[0].AddHype((float)UnityEngine.Random.Range(50 / num, 100 / num));
		}
		if (this.menu_.konsolen[1])
		{
			this.menu_.konsolen[1].AddHype((float)UnityEngine.Random.Range(50 / num, 100 / num));
		}
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x040015EA RID: 5610
	public GameObject[] uiObjects;

	// Token: 0x040015EB RID: 5611
	public AnimationCurve curveBesucher;

	// Token: 0x040015EC RID: 5612
	public Sprite[] besucherSprites;

	// Token: 0x040015ED RID: 5613
	private GameObject main_;

	// Token: 0x040015EE RID: 5614
	private mainScript mS_;

	// Token: 0x040015EF RID: 5615
	private textScript tS_;

	// Token: 0x040015F0 RID: 5616
	private GUI_Main guiMain_;

	// Token: 0x040015F1 RID: 5617
	private sfxScript sfx_;

	// Token: 0x040015F2 RID: 5618
	private Menu_MesseSelect menu_;

	// Token: 0x040015F3 RID: 5619
	private float besucherGesamt;

	// Token: 0x040015F4 RID: 5620
	private float besucherSoll;

	// Token: 0x040015F5 RID: 5621
	private float besucherIst;

	// Token: 0x040015F6 RID: 5622
	private int neueFans;
}
