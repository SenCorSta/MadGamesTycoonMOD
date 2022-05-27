using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class taskMitarbeitersuche : MonoBehaviour
{
	// Token: 0x06001B9A RID: 7066 RVA: 0x00012D56 File Offset: 0x00010F56
	private void Awake()
	{
		base.transform.position = new Vector3(80f, 0f, 0f);
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x00012D77 File Offset: 0x00010F77
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x00117F1C File Offset: 0x0011611C
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

	// Token: 0x06001B9D RID: 7069 RVA: 0x00012D7F File Offset: 0x00010F7F
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x00012DB0 File Offset: 0x00010FB0
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x00012DCC File Offset: 0x00010FCC
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiSprites[44];
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x00012DE2 File Offset: 0x00010FE2
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

	// Token: 0x06001BA1 RID: 7073 RVA: 0x00117FE0 File Offset: 0x001161E0
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

	// Token: 0x06001BA2 RID: 7074 RVA: 0x001181A4 File Offset: 0x001163A4
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

	// Token: 0x06001BA3 RID: 7075 RVA: 0x00118248 File Offset: 0x00116448
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

	// Token: 0x06001BA4 RID: 7076 RVA: 0x00012E1D File Offset: 0x0001101D
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[344].GetComponent<Menu_Mitarbeitersuche>().price[this.berufserfahrung] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x001182A8 File Offset: 0x001164A8
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

	// Token: 0x040022D9 RID: 8921
	public int myID = -1;

	// Token: 0x040022DA RID: 8922
	public int beruf = -1;

	// Token: 0x040022DB RID: 8923
	public int berufserfahrung;

	// Token: 0x040022DC RID: 8924
	public bool automatic;

	// Token: 0x040022DD RID: 8925
	public float points;

	// Token: 0x040022DE RID: 8926
	public float pointsLeft;

	// Token: 0x040022DF RID: 8927
	private GameObject main_;

	// Token: 0x040022E0 RID: 8928
	public mainScript mS_;

	// Token: 0x040022E1 RID: 8929
	private GUI_Main guiMain_;

	// Token: 0x040022E2 RID: 8930
	private textScript tS_;

	// Token: 0x040022E3 RID: 8931
	private roomDataScript rdS_;

	// Token: 0x040022E4 RID: 8932
	public gameScript gS_;

	// Token: 0x040022E5 RID: 8933
	public platformScript pS_;

	// Token: 0x040022E6 RID: 8934
	private arbeitsmarkt arbeitsmarkt_;
}
