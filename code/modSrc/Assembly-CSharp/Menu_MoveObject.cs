using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B5 RID: 437
public class Menu_MoveObject : MonoBehaviour
{
	// Token: 0x06001071 RID: 4209 RVA: 0x0000BA28 File Offset: 0x00009C28
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x000BB36C File Offset: 0x000B956C
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
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0000BA30 File Offset: 0x00009C30
	private void Update()
	{
		this.mS_.snapObject = this.uiObjects[0].GetComponent<Toggle>().isOn;
		this.mS_.snapRotation = this.uiObjects[4].GetComponent<Toggle>().isOn;
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x000BB454 File Offset: 0x000B9654
	public void BUTTON_Sell()
	{
		bool flag = false;
		if (this.mS_.pickedObject)
		{
			objectScript component = this.mS_.pickedObject.GetComponent<objectScript>();
			int num = Mathf.RoundToInt((float)component.GetVerkaufspreis());
			this.mS_.Earn((long)num, 0);
			if (!this.mS_.settings_TutorialOff && component.typ == 92)
			{
				this.guiMain_.SetTutorialStep(4);
			}
			flag = this.mS_.pickedObject.GetComponent<objectScript>().ReOpenBuyInventarMenu();
			UnityEngine.Object.Destroy(this.mS_.pickedObject);
			this.mS_.pickedObject = null;
			this.mS_.ResetAllColliderLayer();
		}
		this.sfx_.PlaySound(11, true);
		this.mS_.UpdatePathfindingNextFrameExtern();
		this.guiMain_.DeactivateMenu(this.guiMain_.uiObjects[0]);
		if (!flag)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000BB544 File Offset: 0x000B9744
	private void OnEnable()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Toggle>().isOn = this.mS_.snapObject;
		this.uiObjects[4].GetComponent<Toggle>().isOn = this.mS_.snapRotation;
		if (this.mS_.pickedObject)
		{
			objectScript component = this.mS_.pickedObject.GetComponent<objectScript>();
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetObjects(component.typ);
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.mS_.pickedObject.GetComponent<objectScript>().preis * 0.5f), true);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.inventarSprites[component.typ];
			this.uiObjects[5].GetComponent<tooltip>().c = this.SetTooltip(component.typ);
		}
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0000BA6C File Offset: 0x00009C6C
	private void OnDisable()
	{
		if (this.mS_)
		{
			this.mS_.UpdatePathfindingNextFrameExtern();
		}
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x000BB65C File Offset: 0x000B985C
	private string SetTooltip(int typ)
	{
		objectScript component = this.mapS_.prefabsInventar[typ].GetComponent<objectScript>();
		string text = "<b>" + this.tS_.GetObjects(typ) + "</b>";
		text = text + "\n" + this.tS_.GetObjectsTooltip(typ) + "\n";
		if (component.qualitaet > 0)
		{
			text = text + "\n" + this.tS_.GetText(532);
			text = text + "\n<size=21>" + this.GetQualitatStars(Mathf.RoundToInt((float)component.qualitaet)) + "</size>\n";
			if (component.isArbeitsplatz)
			{
				string text2 = this.tS_.GetText(533);
				text2 = text2.Replace("<NUM>", Mathf.RoundToInt((float)((component.qualitaet - 1) * 10)).ToString());
				text = text + "\n" + text2;
			}
		}
		if (component.ausstattung > 0f)
		{
			text = text + "\n" + this.tS_.GetText(311);
			text = text + "\n<size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.ausstattung)) + "</size>\n";
		}
		if (component.motivationRegen > 0f)
		{
			text = text + "\n" + this.tS_.GetText(313);
			text = text + "\n<size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.motivationRegen / 20f)) + "</size>\n";
		}
		if (component.waerme > 0f)
		{
			text = text + "\n" + this.tS_.GetText(312);
			text = text + "\n<size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.waerme)) + "</size>\n";
		}
		if (component.kaelte > 0f)
		{
			text = text + "\n" + this.tS_.GetText(510);
			text = text + "\n<size=21>" + this.GetQualitatStars(Mathf.RoundToInt(component.kaelte)) + "</size>\n";
		}
		if (component.monatsKosten > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(310),
				": ",
				this.mS_.GetMoney((long)component.monatsKosten, true)
			});
		}
		if (component.preis > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(218),
				": ",
				this.mS_.GetMoney((long)component.preis, true)
			});
		}
		if (component.aufladungenMax > 0)
		{
			string text3 = this.tS_.GetText(775);
			text3 = text3.Replace("<NUM>", component.aufladungenMax.ToString());
			text = text + "\n" + text3;
		}
		if (this.mS_.year < component.unlockYear)
		{
			string text4 = this.tS_.GetText(535);
			text4 = text4.Replace("<NUM>", component.unlockYear.ToString());
			text = text + "\n\n<color=red><b>" + text4 + "</b></color>";
		}
		return text;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0003D590 File Offset: 0x0003B790
	private string GetQualitatStars(int i)
	{
		string result;
		switch (i)
		{
		case 0:
			result = "☆☆☆☆☆";
			break;
		case 1:
			result = "★☆☆☆☆";
			break;
		case 2:
			result = "★★☆☆☆";
			break;
		case 3:
			result = "★★★☆☆";
			break;
		case 4:
			result = "★★★★☆";
			break;
		case 5:
			result = "★★★★★";
			break;
		default:
			result = "☆☆☆☆☆";
			break;
		}
		return result;
	}

	// Token: 0x040014F1 RID: 5361
	public GameObject[] uiObjects;

	// Token: 0x040014F2 RID: 5362
	private GameObject main_;

	// Token: 0x040014F3 RID: 5363
	private mainScript mS_;

	// Token: 0x040014F4 RID: 5364
	private textScript tS_;

	// Token: 0x040014F5 RID: 5365
	private mapScript mapS_;

	// Token: 0x040014F6 RID: 5366
	private unlockScript unlock_;

	// Token: 0x040014F7 RID: 5367
	private GUI_Main guiMain_;

	// Token: 0x040014F8 RID: 5368
	private sfxScript sfx_;
}
