using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B6 RID: 438
public class Menu_MoveObject : MonoBehaviour
{
	// Token: 0x0600108B RID: 4235 RVA: 0x000AF3EA File Offset: 0x000AD5EA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x000AF3F4 File Offset: 0x000AD5F4
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

	// Token: 0x0600108D RID: 4237 RVA: 0x000AF4DA File Offset: 0x000AD6DA
	private void Update()
	{
		this.mS_.snapObject = this.uiObjects[0].GetComponent<Toggle>().isOn;
		this.mS_.snapRotation = this.uiObjects[4].GetComponent<Toggle>().isOn;
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x000AF518 File Offset: 0x000AD718
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

	// Token: 0x0600108F RID: 4239 RVA: 0x000AF608 File Offset: 0x000AD808
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

	// Token: 0x06001090 RID: 4240 RVA: 0x000AF720 File Offset: 0x000AD920
	private void OnDisable()
	{
		if (this.mS_)
		{
			this.mS_.UpdatePathfindingNextFrameExtern();
		}
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000AF73C File Offset: 0x000AD93C
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

	// Token: 0x06001092 RID: 4242 RVA: 0x000AFA9C File Offset: 0x000ADC9C
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

	// Token: 0x040014FC RID: 5372
	public GameObject[] uiObjects;

	// Token: 0x040014FD RID: 5373
	private GameObject main_;

	// Token: 0x040014FE RID: 5374
	private mainScript mS_;

	// Token: 0x040014FF RID: 5375
	private textScript tS_;

	// Token: 0x04001500 RID: 5376
	private mapScript mapS_;

	// Token: 0x04001501 RID: 5377
	private unlockScript unlock_;

	// Token: 0x04001502 RID: 5378
	private GUI_Main guiMain_;

	// Token: 0x04001503 RID: 5379
	private sfxScript sfx_;
}
