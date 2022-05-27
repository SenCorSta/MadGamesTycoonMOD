using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class taskKonsole : MonoBehaviour
{
	// Token: 0x06001BAC RID: 7084 RVA: 0x00113D10 File Offset: 0x00111F10
	private void Awake()
	{
		base.transform.position = new Vector3(180f, 0f, 0f);
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x00113D31 File Offset: 0x00111F31
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x00113D3C File Offset: 0x00111F3C
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

	// Token: 0x06001BAF RID: 7087 RVA: 0x00113E3C File Offset: 0x0011203C
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x00113E6D File Offset: 0x0011206D
	private void Update()
	{
		this.FindMyKonsole();
		this.FindMyLeitenderTechniker();
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00113E7C File Offset: 0x0011207C
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

	// Token: 0x06001BB2 RID: 7090 RVA: 0x00113ED4 File Offset: 0x001120D4
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

	// Token: 0x06001BB3 RID: 7091 RVA: 0x00113F7E File Offset: 0x0011217E
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.pS_)
		{
			return -1f;
		}
		return this.pS_.GetProzent();
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x00113FA4 File Offset: 0x001121A4
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

	// Token: 0x06001BB5 RID: 7093 RVA: 0x00114028 File Offset: 0x00112228
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

	// Token: 0x06001BB6 RID: 7094 RVA: 0x00114108 File Offset: 0x00112308
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

	// Token: 0x06001BB7 RID: 7095 RVA: 0x001141AC File Offset: 0x001123AC
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

	// Token: 0x06001BB8 RID: 7096 RVA: 0x00114200 File Offset: 0x00112400
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

	// Token: 0x040022C1 RID: 8897
	public int myID = -1;

	// Token: 0x040022C2 RID: 8898
	public int konsoleID = -1;

	// Token: 0x040022C3 RID: 8899
	public int leitenderTechnikerID = -1;

	// Token: 0x040022C4 RID: 8900
	public characterScript techniker_;

	// Token: 0x040022C5 RID: 8901
	public platformScript pS_;

	// Token: 0x040022C6 RID: 8902
	private GameObject main_;

	// Token: 0x040022C7 RID: 8903
	public mainScript mS_;

	// Token: 0x040022C8 RID: 8904
	private engineFeatures eF_;

	// Token: 0x040022C9 RID: 8905
	private GUI_Main guiMain_;

	// Token: 0x040022CA RID: 8906
	private textScript tS_;

	// Token: 0x040022CB RID: 8907
	private roomDataScript rdS_;

	// Token: 0x040022CC RID: 8908
	private hardware hardware_;

	// Token: 0x040022CD RID: 8909
	private hardwareFeatures hardwareFeatures_;
}
