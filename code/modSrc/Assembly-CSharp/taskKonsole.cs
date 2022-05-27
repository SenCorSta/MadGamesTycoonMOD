using System;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class taskKonsole : MonoBehaviour
{
	// Token: 0x06001B62 RID: 7010 RVA: 0x0001295E File Offset: 0x00010B5E
	private void Awake()
	{
		base.transform.position = new Vector3(180f, 0f, 0f);
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x0001297F File Offset: 0x00010B7F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x00116D88 File Offset: 0x00114F88
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x00012987 File Offset: 0x00010B87
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000129B8 File Offset: 0x00010BB8
	private void Update()
	{
		this.FindMyKonsole();
		this.FindMyLeitenderTechniker();
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x00116E88 File Offset: 0x00115088
	private void FindMyKonsole()
	{
		if (!this.pS_)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + this.konsoleID.ToString());
			if (gameObject)
			{
				this.pS_ = gameObject.GetComponent<platformScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x00116EE0 File Offset: 0x001150E0
	private void FindMyLeitenderTechniker()
	{
		if (this.leitenderTechnikerID == -1)
		{
			return;
		}
		if (this.techniker_)
		{
			if (this.techniker_.roomS_)
			{
				if (this.techniker_.roomS_.taskID != this.myID)
				{
					this.leitenderTechnikerID = -1;
					this.techniker_ = null;
					return;
				}
			}
			else
			{
				this.leitenderTechnikerID = -1;
				this.techniker_ = null;
			}
			return;
		}
		GameObject gameObject = GameObject.Find("CHAR_" + this.leitenderTechnikerID.ToString());
		if (gameObject)
		{
			this.techniker_ = gameObject.GetComponent<characterScript>();
			return;
		}
		this.leitenderTechnikerID = -1;
		this.techniker_ = null;
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x000129C6 File Offset: 0x00010BC6
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			return -1f;
		}
		return this.pS_.GetProzent();
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x00116F8C File Offset: 0x0011518C
	public void Work(float f)
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
		if (this.pS_.devPoints > 0f)
		{
			this.pS_.devPoints -= f;
			if (this.pS_.devPoints <= 0f)
			{
				this.pS_.devPoints = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x00117010 File Offset: 0x00115210
	private void Complete()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
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
		string tooltip_ = this.tS_.GetText(1635) + "\n<b>" + this.pS_.GetName() + "</b>";
		this.guiMain_.CreateLeftNews(roomID_, this.pS_.GetTypSprite(), tooltip_, this.rdS_.roomData_SPRITE[8]);
		if (!this.mS_.multiplayer)
		{
			this.CompleteOpenMenue();
		}
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x001170F0 File Offset: 0x001152F0
	public void CompleteOpenMenue()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[326]);
		this.guiMain_.uiObjects[326].GetComponent<Menu_Dev_KonsoleComplete>().Init(this.pS_, this);
		this.guiMain_.OpenMenu(false);
		if (this.mS_.sfx_)
		{
			this.mS_.sfx_.PlaySound(37, false);
		}
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x00117194 File Offset: 0x00115394
	public int GetRueckgeld()
	{
		if (this.pS_)
		{
			long num = this.pS_.entwicklungsKosten / 100L * (long)(100 - Mathf.RoundToInt(this.GetProzent()));
			if (num > 2000000000L)
			{
				num = 2000000000L;
			}
			return Mathf.RoundToInt((float)num);
		}
		return 0;
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x001171E8 File Offset: 0x001153E8
	public void Abbrechen()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			this.FindMyKonsole();
		}
		if (!this.pS_)
		{
			return;
		}
		int rueckgeld = this.GetRueckgeld();
		if (rueckgeld > 0)
		{
			this.mS_.Earn((long)rueckgeld, 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			for (int i = 0; i < array.Length; i++)
			{
				roomScript component = array[i].GetComponent<roomScript>();
				if (component && component.taskID == this.myID)
				{
					this.guiMain_.MoneyPop(rueckgeld, new Vector3(component.uiPos.x, component.uiPos.y + 3f, component.uiPos.z), true);
					break;
				}
			}
		}
		UnityEngine.Object.Destroy(this.pS_.gameObject);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040022A7 RID: 8871
	public int myID = -1;

	// Token: 0x040022A8 RID: 8872
	public int konsoleID = -1;

	// Token: 0x040022A9 RID: 8873
	public int leitenderTechnikerID = -1;

	// Token: 0x040022AA RID: 8874
	public characterScript techniker_;

	// Token: 0x040022AB RID: 8875
	public platformScript pS_;

	// Token: 0x040022AC RID: 8876
	private GameObject main_;

	// Token: 0x040022AD RID: 8877
	public mainScript mS_;

	// Token: 0x040022AE RID: 8878
	private engineFeatures eF_;

	// Token: 0x040022AF RID: 8879
	private GUI_Main guiMain_;

	// Token: 0x040022B0 RID: 8880
	private textScript tS_;

	// Token: 0x040022B1 RID: 8881
	private roomDataScript rdS_;

	// Token: 0x040022B2 RID: 8882
	private hardware hardware_;

	// Token: 0x040022B3 RID: 8883
	private hardwareFeatures hardwareFeatures_;
}
