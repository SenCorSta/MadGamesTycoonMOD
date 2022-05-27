using System;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class taskEngine : MonoBehaviour
{
	// Token: 0x06001AF1 RID: 6897 RVA: 0x000122E4 File Offset: 0x000104E4
	private void Awake()
	{
		base.transform.position = new Vector3(20f, 0f, 0f);
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x00012305 File Offset: 0x00010505
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x001135E0 File Offset: 0x001117E0
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
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x0001230D File Offset: 0x0001050D
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0001233E File Offset: 0x0001053E
	private void Update()
	{
		this.FindMyEngine();
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x001136A4 File Offset: 0x001118A4
	private void FindMyEngine()
	{
		if (!this.eS_)
		{
			GameObject gameObject = GameObject.Find("ENGINE_" + this.engineID.ToString());
			if (gameObject)
			{
				this.eS_ = gameObject.GetComponent<engineScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x00012346 File Offset: 0x00010546
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			return -1f;
		}
		return this.eS_.GetProzent();
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x001136FC File Offset: 0x001118FC
	public void Work(float f)
	{
		this.FindScripts();
		if (!this.eS_)
		{
			this.FindMyEngine();
		}
		if (!this.eS_)
		{
			return;
		}
		if (this.eS_.devPoints > 0f)
		{
			this.eS_.devPoints -= f;
			if (this.eS_.devPoints <= 0f)
			{
				this.eS_.devPoints = 0f;
				this.Complete();
			}
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x00113780 File Offset: 0x00111980
	private void Complete()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			this.FindMyEngine();
		}
		if (!this.eS_)
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
		string tooltip_ = this.tS_.GetText(284) + "\n<b>" + this.eS_.GetName() + "</b>";
		this.guiMain_.CreateLeftNews(roomID_, this.guiMain_.uiSprites[4], tooltip_, this.rdS_.roomData_SPRITE[1]);
		this.eS_.SetComplete();
		if (this.mS_.achScript_)
		{
			this.mS_.achScript_.SetAchivement(23);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x00113888 File Offset: 0x00111A88
	public int GetRueckgeld()
	{
		int num = 0;
		for (int i = 0; i < this.eS_.featuresInDev.Length; i++)
		{
			if (this.eS_.featuresInDev[i])
			{
				num += this.eF_.GetDevCostsForEngine(i);
			}
		}
		return num;
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x001138D0 File Offset: 0x00111AD0
	public void Abbrechen()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			this.FindMyEngine();
		}
		if (!this.eS_)
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
		if (!this.eS_.updating)
		{
			UnityEngine.Object.Destroy(this.eS_.gameObject);
		}
		else
		{
			this.eS_.EntwicklungBeenden();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400223A RID: 8762
	public int myID = -1;

	// Token: 0x0400223B RID: 8763
	public int engineID = -1;

	// Token: 0x0400223C RID: 8764
	public engineScript eS_;

	// Token: 0x0400223D RID: 8765
	private GameObject main_;

	// Token: 0x0400223E RID: 8766
	private mainScript mS_;

	// Token: 0x0400223F RID: 8767
	private engineFeatures eF_;

	// Token: 0x04002240 RID: 8768
	private GUI_Main guiMain_;

	// Token: 0x04002241 RID: 8769
	private textScript tS_;

	// Token: 0x04002242 RID: 8770
	private roomDataScript rdS_;
}
