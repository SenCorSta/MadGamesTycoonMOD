using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000182 RID: 386
public class Menu_Achivements : MonoBehaviour
{
	// Token: 0x06000E70 RID: 3696 RVA: 0x0000A1E0 File Offset: 0x000083E0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x000A9CE8 File Offset: 0x000A7EE8
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

	// Token: 0x06000E72 RID: 3698 RVA: 0x0000A1E8 File Offset: 0x000083E8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0000A21A File Offset: 0x0000841A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0000A222 File Offset: 0x00008422
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x000A9DB0 File Offset: 0x000A7FB0
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

	// Token: 0x06000E76 RID: 3702 RVA: 0x0000A230 File Offset: 0x00008430
	public void TOGGLE_Ausblenden()
	{
		this.SetData();
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0000A238 File Offset: 0x00008438
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040012F2 RID: 4850
	private mainScript mS_;

	// Token: 0x040012F3 RID: 4851
	private GameObject main_;

	// Token: 0x040012F4 RID: 4852
	private GUI_Main guiMain_;

	// Token: 0x040012F5 RID: 4853
	private sfxScript sfx_;

	// Token: 0x040012F6 RID: 4854
	private textScript tS_;

	// Token: 0x040012F7 RID: 4855
	private genres genres_;

	// Token: 0x040012F8 RID: 4856
	public GameObject[] uiPrefabs;

	// Token: 0x040012F9 RID: 4857
	public GameObject[] uiObjects;
}
