using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000237 RID: 567
public class Menu_Stats_Fanverlauf : MonoBehaviour
{
	// Token: 0x060015E2 RID: 5602 RVA: 0x000DE796 File Offset: 0x000DC996
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x000DE7A0 File Offset: 0x000DC9A0
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

	// Token: 0x060015E4 RID: 5604 RVA: 0x000DE886 File Offset: 0x000DCA86
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x000DE88E File Offset: 0x000DCA8E
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x000DE898 File Offset: 0x000DCA98
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		for (int i = 0; i < this.uiObjects[1].transform.childCount; i++)
		{
			this.uiObjects[1].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.Init();
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x000DE920 File Offset: 0x000DCB20
	public void Init()
	{
		this.FindScripts();
		this.InitBalken();
		long num = this.mS_.fansverlauf[0] - this.mS_.fansverlauf[23];
		if (num >= 0L)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(700) + ": <color=black>+" + this.mS_.GetMoney(num, false) + "</color>";
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(700) + ": <color=red>" + this.mS_.GetMoney(num, false) + "</color>";
		}
		for (int i = 0; i < this.genres_.genres_FANS.Length; i++)
		{
			if (this.genres_.genres_FANS[i] > 0)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[1].transform);
				gameObject.transform.GetChild(0).GetComponent<Text>().text = string.Concat(new string[]
				{
					this.genres_.GetName(i),
					"\n<color=blue>",
					this.tS_.GetText(97),
					": ",
					this.mS_.GetMoney((long)this.genres_.genres_FANS[i], false),
					"</color>"
				});
				gameObject.transform.GetChild(1).GetComponent<Image>().sprite = this.genres_.GetPic(i);
				gameObject.name = this.genres_.genres_FANS[i].ToString();
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[1]);
		this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(1162) + ": <color=blue>" + this.mS_.GetMoney((long)this.genres_.GetAmountFans(), false) + "</color>";
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x000DEB50 File Offset: 0x000DCD50
	private void InitBalken()
	{
		float num = 400f;
		long num2 = 0L;
		for (int i = 0; i < this.mS_.fansverlauf.Length; i++)
		{
			if (num2 < this.mS_.fansverlauf[i])
			{
				num2 = this.mS_.fansverlauf[i];
			}
		}
		float num3 = num / (float)num2;
		for (int j = 0; j < this.mS_.fansverlauf.Length; j++)
		{
			this.uiBalken[j].GetComponent<Image>().fillAmount = (float)this.mS_.fansverlauf[j] * num3 / num;
			if (this.mS_.fansverlauf[j] > 0L)
			{
				this.uiBalken[j].transform.GetChild(0).GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.fansverlauf[j], false);
				if (j < this.mS_.fansverlauf.Length - 1)
				{
					long num4 = this.mS_.fansverlauf[j] - this.mS_.fansverlauf[j + 1];
					if (num4 > 0L)
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = "+" + this.mS_.GetMoney(num4, false);
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().color = this.guiMain_.colors[4];
					}
					else
					{
						this.uiBalken[j].transform.GetChild(1).GetComponent<Text>().text = this.mS_.GetMoney(num4, false);
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
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x000DED80 File Offset: 0x000DCF80
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019CE RID: 6606
	public GameObject[] uiPrefabs;

	// Token: 0x040019CF RID: 6607
	public GameObject[] uiBalken;

	// Token: 0x040019D0 RID: 6608
	public GameObject[] uiObjects;

	// Token: 0x040019D1 RID: 6609
	private GameObject main_;

	// Token: 0x040019D2 RID: 6610
	private mainScript mS_;

	// Token: 0x040019D3 RID: 6611
	private textScript tS_;

	// Token: 0x040019D4 RID: 6612
	private GUI_Main guiMain_;

	// Token: 0x040019D5 RID: 6613
	private sfxScript sfx_;

	// Token: 0x040019D6 RID: 6614
	private genres genres_;

	// Token: 0x040019D7 RID: 6615
	private engineFeatures eF_;

	// Token: 0x040019D8 RID: 6616
	private engineScript eS_;

	// Token: 0x040019D9 RID: 6617
	private float updateTimer;
}
