using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class taskProduction : MonoBehaviour
{
	// Token: 0x06001BB6 RID: 7094 RVA: 0x00012F32 File Offset: 0x00011132
	private void Awake()
	{
		base.transform.position = new Vector3(160f, 0f, 0f);
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x00012F53 File Offset: 0x00011153
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x00118758 File Offset: 0x00116958
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00012F5B File Offset: 0x0001115B
	private void Update()
	{
		this.FindMyObject();
		this.GameRemovedFromMarket();
		if (this.automatic)
		{
			this.CheckAutomatic();
		}
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x00012F77 File Offset: 0x00011177
	private void GameRemovedFromMarket()
	{
		if (this.gS_ && !this.gS_.isOnMarket)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00012F99 File Offset: 0x00011199
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x0011881C File Offset: 0x00116A1C
	private void FindMyObject()
	{
		if (this.gS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GAME_" + this.targetID.ToString());
		if (gameObject)
		{
			this.gS_ = gameObject.GetComponent<gameScript>();
		}
		if (!this.gS_)
		{
			this.Abbrechen();
		}
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x0011887C File Offset: 0x00116A7C
	public float GetProzent()
	{
		float num = (float)this.gesamtProduktion;
		num *= 0.01f;
		return (float)(this.gesamtProduktion - (this.amountStandard + this.amountDeluxe + this.amountCollectors)) / num;
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x00012FCA File Offset: 0x000111CA
	public int GetAmount()
	{
		return this.amountStandard + this.amountDeluxe + this.amountCollectors;
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x00012FE0 File Offset: 0x000111E0
	public Sprite GetPic()
	{
		return this.guiMain_.uiSprites[26];
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x001188B8 File Offset: 0x00116AB8
	public void Work(int i, Vector3 pos)
	{
		if (!this.gS_)
		{
			return;
		}
		if (this.amountStandard <= 0 && this.amountDeluxe <= 0 && this.amountCollectors <= 0)
		{
			return;
		}
		if (this.games_.GetFreeLagerplatz() < i)
		{
			i = this.games_.GetFreeLagerplatz();
		}
		if (i > 0 && this.amountStandard > 0)
		{
			if (this.amountStandard >= i)
			{
				this.amountStandard -= i;
				this.gS_.lagerbestand[0] += i;
				this.games_.LagerplatzVerteilenEinGame(i);
				int num = Mathf.RoundToInt((float)i * this.gS_.GetProduktionskosten(0));
				this.gS_.costs_production += (long)num;
				this.mS_.Pay((long)num, 21);
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num, pos, false));
				i = 0;
			}
			else
			{
				i -= this.amountStandard;
				this.gS_.lagerbestand[0] += this.amountStandard;
				this.games_.LagerplatzVerteilenEinGame(this.amountStandard);
				int num2 = Mathf.RoundToInt((float)this.amountStandard * this.gS_.GetProduktionskosten(0));
				this.gS_.costs_production += (long)num2;
				this.mS_.Pay((long)num2, 21);
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num2, pos, false));
				this.amountStandard = 0;
			}
		}
		if (i > 0 && this.amountDeluxe > 0)
		{
			if (this.amountDeluxe >= i)
			{
				this.amountDeluxe -= i;
				this.gS_.lagerbestand[1] += i;
				this.games_.LagerplatzVerteilenEinGame(i);
				int num3 = Mathf.RoundToInt((float)i * this.gS_.GetProduktionskosten(1));
				this.gS_.costs_production += (long)num3;
				this.mS_.Pay((long)num3, 21);
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num3, pos, false));
				i = 0;
			}
			else
			{
				i -= this.amountDeluxe;
				this.gS_.lagerbestand[1] += this.amountDeluxe;
				this.games_.LagerplatzVerteilenEinGame(this.amountDeluxe);
				int num4 = Mathf.RoundToInt((float)this.amountDeluxe * this.gS_.GetProduktionskosten(1));
				this.gS_.costs_production += (long)num4;
				this.mS_.Pay((long)num4, 21);
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num4, pos, false));
				this.amountDeluxe = 0;
			}
		}
		if (i > 0 && this.amountCollectors > 0)
		{
			if (this.amountCollectors >= i)
			{
				this.amountCollectors -= i;
				this.gS_.lagerbestand[2] += i;
				this.games_.LagerplatzVerteilenEinGame(i);
				int num5 = Mathf.RoundToInt((float)i * this.gS_.GetProduktionskosten(2));
				this.gS_.costs_production += (long)num5;
				this.mS_.Pay((long)num5, 21);
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num5, pos, false));
				i = 0;
			}
			else
			{
				i -= this.amountCollectors;
				this.gS_.lagerbestand[2] += this.amountCollectors;
				this.games_.LagerplatzVerteilenEinGame(this.amountCollectors);
				int num6 = Mathf.RoundToInt((float)this.amountCollectors * this.gS_.GetProduktionskosten(2));
				this.gS_.costs_production += (long)num6;
				this.mS_.Pay((long)num6, 21);
				base.StartCoroutine(this.guiMain_.MoneyPopEnumerate(num6, pos, false));
				this.amountCollectors = 0;
			}
		}
		this.Complete();
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x00012FF0 File Offset: 0x000111F0
	public bool WaitForAutomatic()
	{
		return this.automatic && (this.amountStandard <= 0 && this.amountDeluxe <= 0 && this.amountCollectors <= 0);
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x00118CB4 File Offset: 0x00116EB4
	private void CheckAutomatic()
	{
		if (!this.automatic)
		{
			return;
		}
		if (this.amountStandard <= 0 && this.amountDeluxe <= 0 && this.amountCollectors <= 0)
		{
			this.FindMyObject();
			if (this.gS_.sellsPerWeek[0] > 0)
			{
				if (this.gS_.lagerbestand[0] < this.gS_.sellsPerWeek[0] * 3)
				{
					this.amountStandard = this.gS_.sellsPerWeek[0] * 3;
				}
				if (!this.gS_.typ_budget && !this.gS_.typ_bundle && !this.gS_.typ_goty && !this.gS_.typ_addon && !this.gS_.typ_mmoaddon && !this.gS_.typ_addonStandalone)
				{
					if (this.gS_.lagerbestand[1] < this.gS_.sellsPerWeek[0] / 5)
					{
						this.amountDeluxe = this.gS_.sellsPerWeek[0] / 5;
					}
					if (this.gS_.lagerbestand[2] < this.gS_.sellsPerWeek[0] / 10)
					{
						this.amountCollectors = this.gS_.sellsPerWeek[0] / 10;
					}
				}
			}
			else
			{
				int num = this.gS_.vorbestellungen;
				if (this.gS_.sellsTotal <= 0L && !this.gS_.typ_budget && num < 5000)
				{
					num = 5000;
				}
				if (this.gS_.sellsTotal <= 0L && (this.gS_.typ_addon || this.gS_.typ_addonStandalone || this.gS_.typ_mmoaddon) && num < 5000)
				{
					num = 4000;
				}
				if (this.gS_.sellsTotal <= 0L && this.gS_.typ_budget && num < 1000)
				{
					num = 1000;
				}
				if (this.gS_.sellsTotal <= 0L && this.gS_.typ_bundle && num < 1000)
				{
					num = 1000;
				}
				if (this.gS_.sellsTotal <= 0L && this.gS_.typ_bundleAddon && num < 1000)
				{
					num = 1000;
				}
				if (this.gS_.sellsTotal <= 0L && this.gS_.typ_goty && num < 1000)
				{
					num = 1000;
				}
				if (this.gS_.lagerbestand[0] < num * 3)
				{
					this.amountStandard = num * 3;
				}
				if (!this.gS_.typ_budget && !this.gS_.typ_bundle && !this.gS_.typ_goty && !this.gS_.typ_addon && !this.gS_.typ_mmoaddon && !this.gS_.typ_addonStandalone)
				{
					if (this.gS_.lagerbestand[1] < num / 5)
					{
						this.amountDeluxe = num / 5;
					}
					if (this.gS_.lagerbestand[2] < num / 10)
					{
						this.amountCollectors = num / 10;
					}
				}
			}
			this.gesamtProduktion = this.amountStandard + this.amountDeluxe + this.amountCollectors;
		}
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x00118FE8 File Offset: 0x001171E8
	private void Complete()
	{
		if (this.automatic)
		{
			return;
		}
		if (this.amountStandard <= 0 && this.amountDeluxe <= 0 && this.amountCollectors <= 0)
		{
			this.FindMyObject();
			int roomID_ = -1;
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					roomID_ = component.myID;
					break;
				}
			}
			string text = this.tS_.GetText(1137);
			text = text.Replace("<NAME1>", "<b><color=blue>" + this.gS_.GetNameWithTag() + "</color></b>");
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[14]);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000030EA File Offset: 0x000012EA
	public int GetRueckgeld()
	{
		return 0;
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040022F3 RID: 8947
	public int myID = -1;

	// Token: 0x040022F4 RID: 8948
	public int targetID = -1;

	// Token: 0x040022F5 RID: 8949
	public bool automatic;

	// Token: 0x040022F6 RID: 8950
	public int amountStandard;

	// Token: 0x040022F7 RID: 8951
	public int amountDeluxe;

	// Token: 0x040022F8 RID: 8952
	public int amountCollectors;

	// Token: 0x040022F9 RID: 8953
	public int gesamtProduktion;

	// Token: 0x040022FA RID: 8954
	private GameObject main_;

	// Token: 0x040022FB RID: 8955
	public mainScript mS_;

	// Token: 0x040022FC RID: 8956
	private GUI_Main guiMain_;

	// Token: 0x040022FD RID: 8957
	private textScript tS_;

	// Token: 0x040022FE RID: 8958
	private roomDataScript rdS_;

	// Token: 0x040022FF RID: 8959
	public gameScript gS_;

	// Token: 0x04002300 RID: 8960
	private games games_;

	// Token: 0x04002301 RID: 8961
	public roomScript rS_;
}
