using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class taskFankampagne : MonoBehaviour
{
	// Token: 0x06001B0D RID: 6925 RVA: 0x00012488 File Offset: 0x00010688
	private void Awake()
	{
		base.transform.position = new Vector3(190f, 0f, 0f);
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x000124A9 File Offset: 0x000106A9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x00113E34 File Offset: 0x00112034
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
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000124B1 File Offset: 0x000106B1
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000124E2 File Offset: 0x000106E2
	public float GetProzent()
	{
		return 100f / this.points * (this.points - this.pointsLeft);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000124FE File Offset: 0x000106FE
	public Sprite GetPic()
	{
		this.FindScripts();
		return this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>().sprites[this.kampagne];
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x00012528 File Offset: 0x00010728
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

	// Token: 0x06001B14 RID: 6932 RVA: 0x00113EDC File Offset: 0x001120DC
	private void Complete()
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
		string text = this.tS_.GetText(748);
		text = text.Replace("<NAME>", "<b><color=blue>" + this.tS_.GetText(740 + this.kampagne) + "</color></b>");
		this.guiMain_.CreateLeftNews(roomID_, this.GetPic(), text, this.rdS_.roomData_SPRITE[7]);
		Menu_Support_Fankampagne component2 = this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>();
		this.mS_.AddFans(component2.fans[this.kampagne], -1);
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x00113FDC File Offset: 0x001121DC
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		Menu_Support_Fankampagne component = this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>();
		if (this.mS_.money < (long)component.preise[this.kampagne])
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[7]);
			return false;
		}
		this.mS_.Pay((long)component.preise[this.kampagne], 16);
		this.pointsLeft = this.points;
		return true;
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x00114080 File Offset: 0x00112280
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

	// Token: 0x06001B17 RID: 6935 RVA: 0x00012563 File Offset: 0x00010763
	public int GetRueckgeld()
	{
		return Mathf.RoundToInt((float)this.guiMain_.uiObjects[139].GetComponent<Menu_Support_Fankampagne>().preise[this.kampagne] * ((100f - this.GetProzent()) * 0.01f));
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x001140E0 File Offset: 0x001122E0
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

	// Token: 0x04002250 RID: 8784
	public int myID = -1;

	// Token: 0x04002251 RID: 8785
	public int kampagne = -1;

	// Token: 0x04002252 RID: 8786
	public bool automatic;

	// Token: 0x04002253 RID: 8787
	public bool stopAutomatic;

	// Token: 0x04002254 RID: 8788
	public float points;

	// Token: 0x04002255 RID: 8789
	public float pointsLeft;

	// Token: 0x04002256 RID: 8790
	private GameObject main_;

	// Token: 0x04002257 RID: 8791
	public mainScript mS_;

	// Token: 0x04002258 RID: 8792
	private GUI_Main guiMain_;

	// Token: 0x04002259 RID: 8793
	private textScript tS_;

	// Token: 0x0400225A RID: 8794
	private roomDataScript rdS_;

	// Token: 0x0400225B RID: 8795
	public gameScript gS_;

	// Token: 0x0400225C RID: 8796
	public platformScript pS_;
}
