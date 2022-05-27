using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000183 RID: 387
public class Menu_Achivements : MonoBehaviour
{
	// Token: 0x06000E88 RID: 3720 RVA: 0x0009C35C File Offset: 0x0009A55C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0009C364 File Offset: 0x0009A564
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0009C42C File Offset: 0x0009A62C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0009C45E File Offset: 0x0009A65E
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0009C466 File Offset: 0x0009A666
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0009C474 File Offset: 0x0009A674
	private void SetData()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[0].transform.GetChild(i).gameObject);
		}
		int num = 0;
		int num2 = 0;
		bool isOn = this.uiObjects[6].GetComponent<Toggle>().isOn;
		for (int j = 0; j < this.mS_.achivements.Length; j++)
		{
			if (!this.mS_.achivementsDisabled[j])
			{
				num++;
				if (this.mS_.achivements[j])
				{
					num2++;
				}
				if (this.guiMain_.iconAchivements[j] && (!isOn || !this.mS_.achivements[j]))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_Achivement component = gameObject.GetComponent<Item_Achivement>();
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.guiMain_ = this.guiMain_;
					component.SetData(j);
					if (this.mS_.achivements[j])
					{
						gameObject.name = "1";
					}
					else
					{
						gameObject.name = "0";
					}
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1800);
		text = text.Replace("<NUM1>", num2.ToString());
		text = text.Replace("<NUM2>", num.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
		text = "";
		if (this.mS_.GetAchivementBonus(0) > 0)
		{
			text = this.tS_.GetText(1801) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(0).ToString());
		}
		if (this.mS_.GetAchivementBonus(1) > 0)
		{
			text = text + this.tS_.GetText(1802) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(1).ToString());
		}
		if (this.mS_.GetAchivementBonus(2) > 0)
		{
			text = text + this.tS_.GetText(1803) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(2).ToString());
		}
		if (this.mS_.GetAchivementBonus(3) > 0)
		{
			text = text + this.tS_.GetText(1804) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(3).ToString());
		}
		if (this.mS_.GetAchivementBonus(4) > 0)
		{
			text = text + this.tS_.GetText(1805) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(4).ToString());
		}
		if (this.mS_.GetAchivementBonus(5) > 0)
		{
			text = text + this.tS_.GetText(1806) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(5).ToString());
		}
		if (this.mS_.GetAchivementBonus(6) > 0)
		{
			text = text + this.tS_.GetText(1807) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(6).ToString());
		}
		if (this.mS_.GetAchivementBonus(7) > 0)
		{
			text = text + this.tS_.GetText(1808) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(7).ToString());
		}
		if (this.mS_.GetAchivementBonus(8) > 0)
		{
			text = text + this.tS_.GetText(1809) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(8).ToString());
		}
		if (this.mS_.GetAchivementBonus(9) > 0)
		{
			text = text + this.tS_.GetText(1810) + "\n";
			text = text.Replace("<NUM>", this.mS_.GetAchivementBonus(9).ToString());
		}
		this.uiObjects[7].GetComponent<Text>().text = text;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0009C96D File Offset: 0x0009AB6D
	public void TOGGLE_Ausblenden()
	{
		this.SetData();
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x0009C975 File Offset: 0x0009AB75
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040012FB RID: 4859
	private mainScript mS_;

	// Token: 0x040012FC RID: 4860
	private GameObject main_;

	// Token: 0x040012FD RID: 4861
	private GUI_Main guiMain_;

	// Token: 0x040012FE RID: 4862
	private sfxScript sfx_;

	// Token: 0x040012FF RID: 4863
	private textScript tS_;

	// Token: 0x04001300 RID: 4864
	private genres genres_;

	// Token: 0x04001301 RID: 4865
	public GameObject[] uiPrefabs;

	// Token: 0x04001302 RID: 4866
	public GameObject[] uiObjects;
}
