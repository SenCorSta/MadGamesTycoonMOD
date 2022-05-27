using System;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class taskEngine : MonoBehaviour
{
	// Token: 0x06001B3B RID: 6971 RVA: 0x0010FF18 File Offset: 0x0010E118
	private void Awake()
	{
		base.transform.position = new Vector3(20f, 0f, 0f);
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x0010FF39 File Offset: 0x0010E139
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x0010FF44 File Offset: 0x0010E144
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

	// Token: 0x06001B3E RID: 6974 RVA: 0x00110008 File Offset: 0x0010E208
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001B3F RID: 6975 RVA: 0x00110039 File Offset: 0x0010E239
	private void Update()
	{
		this.FindMyEngine();
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x00110044 File Offset: 0x0010E244
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

	// Token: 0x06001B41 RID: 6977 RVA: 0x00110099 File Offset: 0x0010E299
	public float GetProzent()
	{
		this.FindScripts();
		if (!this.eS_)
		{
			return -1f;
		}
		return this.eS_.GetProzent();
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x001100C0 File Offset: 0x0010E2C0
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

	// Token: 0x06001B43 RID: 6979 RVA: 0x00110144 File Offset: 0x0010E344
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

	// Token: 0x06001B44 RID: 6980 RVA: 0x0011024C File Offset: 0x0010E44C
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

	// Token: 0x06001B45 RID: 6981 RVA: 0x00110294 File Offset: 0x0010E494
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

	// Token: 0x04002254 RID: 8788
	public int myID = -1;

	// Token: 0x04002255 RID: 8789
	public int engineID = -1;

	// Token: 0x04002256 RID: 8790
	public engineScript eS_;

	// Token: 0x04002257 RID: 8791
	private GameObject main_;

	// Token: 0x04002258 RID: 8792
	private mainScript mS_;

	// Token: 0x04002259 RID: 8793
	private engineFeatures eF_;

	// Token: 0x0400225A RID: 8794
	private GUI_Main guiMain_;

	// Token: 0x0400225B RID: 8795
	private textScript tS_;

	// Token: 0x0400225C RID: 8796
	private roomDataScript rdS_;
}
