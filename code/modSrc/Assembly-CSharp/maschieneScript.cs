using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F1 RID: 753
public class maschieneScript : MonoBehaviour
{
	// Token: 0x06001A52 RID: 6738 RVA: 0x00011B41 File Offset: 0x0000FD41
	private void Start()
	{
		this.FindScripts();
		this.InitUI();
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x0010FE84 File Offset: 0x0010E084
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
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.myCamera)
		{
			this.myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.guiMain)
		{
			this.guiMain = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = GameObject.FindWithTag("Main").GetComponent<textScript>();
		}
		if (!this.oS_)
		{
			this.oS_ = base.GetComponent<objectScript>();
		}
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x00011B4F File Offset: 0x0000FD4F
	private void OnDestroy()
	{
		if (this.myUI)
		{
			UnityEngine.Object.Destroy(this.myUI);
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x0010FFAC File Offset: 0x0010E1AC
	private void InitUI()
	{
		this.myUI = UnityEngine.Object.Instantiate<GameObject>(this.uiMaschiene, new Vector3(99999f, 99999f, 0f), Quaternion.identity);
		this.myUI.transform.SetParent(this.mS_.guiPops.transform);
		this.myUI.transform.SetSiblingIndex(0);
		this.myUI_RectTransform = this.myUI.GetComponent<RectTransform>();
		this.uiIconMain = this.myUI.transform.Find("IconMain").gameObject;
		this.uiWorkProgress = this.uiIconMain.transform.Find("WorkProgress").gameObject;
		this.uiWorkProgress_Image = this.uiWorkProgress.GetComponent<Image>();
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x00110078 File Offset: 0x0010E278
	private void UpdateUI(bool show)
	{
		if (this.guiMain.menuOpen || this.oS_.picked || !show)
		{
			if (this.myUI.activeSelf)
			{
				this.myUI.SetActive(false);
			}
			return;
		}
		Vector3 position = base.gameObject.transform.position;
		position.y += 1f;
		if (!this.myUI.activeSelf)
		{
			this.invisibleTimer += Time.deltaTime;
			if (this.invisibleTimer < 0.1f)
			{
				return;
			}
			this.invisibleTimer = 0f;
		}
		Vector2 vector = this.myCamera.WorldToScreenPoint(position);
		if (vector.x >= 0f && vector.x <= (float)Screen.width && vector.y >= 0f && vector.y <= (float)Screen.height)
		{
			if (!this.myUI.activeSelf)
			{
				this.myUI.SetActive(true);
			}
			vector = new Vector2(vector.x, vector.y - (float)Screen.height);
			this.myUI_RectTransform.anchoredPosition = this.guiMain.GetAnchoredPosition(vector);
			this.uiWorkProgress_Image.fillAmount = this.oS_.maschieneTimer;
			return;
		}
		if (this.myUI.activeSelf)
		{
			this.myUI.SetActive(false);
		}
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x001101DC File Offset: 0x0010E3DC
	private void Update()
	{
		if (!this.oS_)
		{
			return;
		}
		this.UpdateDisketten();
		bool flag = this.UpdateMaschine();
		if (!flag)
		{
			this.oS_.maschieneTimer = 0f;
		}
		this.UpdateUI(flag);
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.2f)
		{
			return;
		}
		this.updateTimer = 0f;
		if (flag)
		{
			if (!this.myAnimation[0].isPlaying)
			{
				this.myAnimation[0].Play();
				this.myAnimation[1].Play();
			}
			this.bahn.material = this.mS_.specialMaterials[2];
			this.myAnimation[0]["maschine1"].speed = this.mS_.GetGameSpeed();
			this.myAnimation[1]["maschineLight"].speed = this.mS_.GetGameSpeed();
			return;
		}
		this.bahn.material = this.mS_.specialMaterials[5];
		this.myAnimation[0]["maschine1"].speed = 0f;
		this.myAnimation[1]["maschineLight"].speed = 0f;
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x00110328 File Offset: 0x0010E528
	private bool UpdateMaschine()
	{
		if (!this.oS_.isMaschine)
		{
			return false;
		}
		if (this.oS_.picked)
		{
			return false;
		}
		int num = Mathf.RoundToInt(this.oS_.gameObject.transform.position.x);
		int num2 = Mathf.RoundToInt(this.oS_.gameObject.transform.position.z);
		if (!this.mapS_.IsInMapLimit(num, num2))
		{
			return false;
		}
		roomScript roomScript = this.mapS_.mapRoomScript[num, num2];
		if (roomScript && roomScript.taskGameObject && roomScript.taskID != -1)
		{
			int num3 = this.oS_.qualitaet * 5000;
			taskProduction taskProduction = roomScript.GetTaskProduction();
			if (taskProduction)
			{
				if (this.games_.GetFreeLagerplatz() <= 0)
				{
					return false;
				}
				if (taskProduction.WaitForAutomatic())
				{
					return false;
				}
				this.oS_.maschieneTimer += this.mS_.GetDeltaTime() * 0.3f;
				if (this.oS_.maschieneTimer > 1f)
				{
					this.oS_.maschieneTimer = 0f;
					taskProduction.Work(num3, base.transform.position);
				}
				return true;
			}
			else
			{
				taskContractWork taskContractWork = roomScript.GetTaskContractWork();
				if (taskContractWork)
				{
					this.oS_.maschieneTimer += this.mS_.GetDeltaTime() * 0.3f;
					if (this.oS_.maschieneTimer > 1f)
					{
						this.oS_.maschieneTimer = 0f;
						taskContractWork.Work((float)num3);
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x001104D8 File Offset: 0x0010E6D8
	private void UpdateDisketten()
	{
		this.updateDisketteTimer += Time.deltaTime;
		if (this.updateDisketteTimer < 1f)
		{
			return;
		}
		this.updateDisketteTimer = 0f;
		if (this.mS_.year < 1985)
		{
			for (int i = 0; i < this.disketten1976.Length; i++)
			{
				if (this.disketten1976[i] && !this.disketten1976[i].activeSelf)
				{
					this.disketten1976[i].SetActive(true);
				}
			}
			for (int j = 0; j < this.disketten1985.Length; j++)
			{
				if (this.disketten1985[j] && this.disketten1985[j].activeSelf)
				{
					this.disketten1985[j].SetActive(false);
				}
			}
			for (int k = 0; k < this.disketten1995.Length; k++)
			{
				if (this.disketten1995[k] && this.disketten1995[k].activeSelf)
				{
					this.disketten1995[k].SetActive(false);
				}
			}
			return;
		}
		if (this.mS_.year >= 1985 && this.mS_.year < 1995)
		{
			for (int l = 0; l < this.disketten1976.Length; l++)
			{
				if (this.disketten1976[l])
				{
					UnityEngine.Object.Destroy(this.disketten1976[l]);
				}
			}
			for (int m = 0; m < this.disketten1985.Length; m++)
			{
				if (this.disketten1985[m] && !this.disketten1985[m].activeSelf)
				{
					this.disketten1985[m].SetActive(true);
				}
			}
			for (int n = 0; n < this.disketten1995.Length; n++)
			{
				if (this.disketten1995[n] && this.disketten1995[n].activeSelf)
				{
					this.disketten1995[n].SetActive(false);
				}
			}
			return;
		}
		if (this.mS_.year >= 1995)
		{
			for (int num = 0; num < this.disketten1976.Length; num++)
			{
				if (this.disketten1976[num])
				{
					UnityEngine.Object.Destroy(this.disketten1976[num]);
				}
			}
			for (int num2 = 0; num2 < this.disketten1985.Length; num2++)
			{
				if (this.disketten1985[num2])
				{
					UnityEngine.Object.Destroy(this.disketten1985[num2]);
				}
			}
			for (int num3 = 0; num3 < this.disketten1995.Length; num3++)
			{
				if (this.disketten1995[num3] && !this.disketten1995[num3].activeSelf)
				{
					this.disketten1995[num3].SetActive(true);
				}
			}
			return;
		}
	}

	// Token: 0x0400218B RID: 8587
	private GameObject main_;

	// Token: 0x0400218C RID: 8588
	private objectScript oS_;

	// Token: 0x0400218D RID: 8589
	public mainScript mS_;

	// Token: 0x0400218E RID: 8590
	private Camera myCamera;

	// Token: 0x0400218F RID: 8591
	private GUI_Main guiMain;

	// Token: 0x04002190 RID: 8592
	public sfxScript sfx_;

	// Token: 0x04002191 RID: 8593
	public mapScript mapS_;

	// Token: 0x04002192 RID: 8594
	public textScript tS_;

	// Token: 0x04002193 RID: 8595
	private games games_;

	// Token: 0x04002194 RID: 8596
	public Animation[] myAnimation;

	// Token: 0x04002195 RID: 8597
	public MeshRenderer bahn;

	// Token: 0x04002196 RID: 8598
	public GameObject[] disketten1976;

	// Token: 0x04002197 RID: 8599
	public GameObject[] disketten1985;

	// Token: 0x04002198 RID: 8600
	public GameObject[] disketten1995;

	// Token: 0x04002199 RID: 8601
	public GameObject uiMaschiene;

	// Token: 0x0400219A RID: 8602
	private GameObject myUI;

	// Token: 0x0400219B RID: 8603
	private GameObject uiIconMain;

	// Token: 0x0400219C RID: 8604
	private GameObject uiWorkProgress;

	// Token: 0x0400219D RID: 8605
	private RectTransform myUI_RectTransform;

	// Token: 0x0400219E RID: 8606
	private Image uiWorkProgress_Image;

	// Token: 0x0400219F RID: 8607
	private float invisibleTimer;

	// Token: 0x040021A0 RID: 8608
	private float updateTimer;

	// Token: 0x040021A1 RID: 8609
	private float updateDisketteTimer;
}
