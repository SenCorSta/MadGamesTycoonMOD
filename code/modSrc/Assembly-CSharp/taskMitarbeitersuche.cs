using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class taskMitarbeitersuche : MonoBehaviour
{
	// Token: 0x06001BE4 RID: 7140 RVA: 0x001152B2 File Offset: 0x001134B2
	private void Awake()
	{
		base.transform.position = new Vector3(80f, 0f, 0f);
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x001152D3 File Offset: 0x001134D3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x001152DC File Offset: 0x001134DC
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
		if (!this.arbeitsmarkt_)
		{
			this.arbeitsmarkt_ = this.main_.GetComponent<arbeitsmarkt>();
		}
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x001153A0 File Offset: 0x001135A0
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x001153D1 File Offset: 0x001135D1
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x001153ED File Offset: 0x001135ED
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiSprites[44];
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x00115403 File Offset: 0x00113603
	public void Work(float f)
	{
		if (this.pointsLeft > 0f)
		{
			this.pointsLeft -= f;
			if (this.pointsLeft <= 0f)
			{
				this.pointsLeft = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x00115440 File Offset: 0x00113640
	private void Complete()
	{
		if (this.mS_.multiplayer && this.guiMain_.menuOpen)
		{
			this.pointsLeft = 0.1f;
			return;
		}
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
		float chance = this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>().GetChance(this.berufserfahrung);
		float num = UnityEngine.Random.Range(0f, 100f);
		Debug.Log(string.Concat(new object[]
		{
			"AA: ",
			num,
			" / ",
			chance
		}));
		if (num < chance)
		{
			this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), this.tS_.GetText(1719), this.rdS_.roomData_SPRITE[6]);
			charArbeitsmarkt charArbeitsmarkt = this.arbeitsmarkt_.CreateArbeitsmarktItem();
			if (charArbeitsmarkt)
			{
				charArbeitsmarkt.Create(this);
			}
			this.guiMain_.uiObjects[345].SetActive(true);
			this.guiMain_.uiObjects[345].GetComponent<Menu_MitarbeitersucheResult>().Init(charArbeitsmarkt);
			this.guiMain_.OpenMenu(false);
		}
		else
		{
			this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[48], this.tS_.GetText(1718), this.rdS_.roomData_SPRITE[6]);
		}
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x00115604 File Offset: 0x00113804
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		Menu_Mitarbeitersuche component = this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>();
		if (this.mS_.money < (long)component.price[this.berufserfahrung])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[6]);
			return false;
		}
		this.mS_.Pay((long)component.price[this.berufserfahrung], 24);
		this.pointsLeft = this.points;
		return true;
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x001156A8 File Offset: 0x001138A8
	private void LeftNews(string c, Sprite icon, Sprite iconRoom)
	{
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
		this.guiMain_.CreateLeftNews(roomID_, icon, c, iconRoom);
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x00115707 File Offset: 0x00113907
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>().price[this.berufserfahrung] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x00115744 File Offset: 0x00113944
	public void Abbrechen()
	{
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)Mathf.RoundToInt((float)rueckgeld), 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(Mathf.RoundToInt((float)rueckgeld), new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040022F3 RID: 8947
	public int myID = -1;

	// Token: 0x040022F4 RID: 8948
	public int beruf = -1;

	// Token: 0x040022F5 RID: 8949
	public int berufserfahrung;

	// Token: 0x040022F6 RID: 8950
	public bool automatic;

	// Token: 0x040022F7 RID: 8951
	public float points;

	// Token: 0x040022F8 RID: 8952
	public float pointsLeft;

	// Token: 0x040022F9 RID: 8953
	private GameObject main_;

	// Token: 0x040022FA RID: 8954
	public mainScript mS_;

	// Token: 0x040022FB RID: 8955
	private GUI_Main guiMain_;

	// Token: 0x040022FC RID: 8956
	private textScript tS_;

	// Token: 0x040022FD RID: 8957
	private roomDataScript rdS_;

	// Token: 0x040022FE RID: 8958
	public gameScript gS_;

	// Token: 0x040022FF RID: 8959
	public platformScript pS_;

	// Token: 0x04002300 RID: 8960
	private arbeitsmarkt arbeitsmarkt_;
}
