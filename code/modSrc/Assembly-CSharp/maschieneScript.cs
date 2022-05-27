using System;
using UnityEngine;
using UnityEngine.UI;


public class maschieneScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.InitUI();
	}

	
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

	
	private void OnDestroy()
	{
		if (this.myUI)
		{
			UnityEngine.Object.Destroy(this.myUI);
		}
	}

	
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

	
	private GameObject main_;

	
	private objectScript oS_;

	
	public mainScript mS_;

	
	private Camera myCamera;

	
	private GUI_Main guiMain;

	
	public sfxScript sfx_;

	
	public mapScript mapS_;

	
	public textScript tS_;

	
	private games games_;

	
	public Animation[] myAnimation;

	
	public MeshRenderer bahn;

	
	public GameObject[] disketten1976;

	
	public GameObject[] disketten1985;

	
	public GameObject[] disketten1995;

	
	public GameObject uiMaschiene;

	
	private GameObject myUI;

	
	private GameObject uiIconMain;

	
	private GameObject uiWorkProgress;

	
	private RectTransform myUI_RectTransform;

	
	private Image uiWorkProgress_Image;

	
	private float invisibleTimer;

	
	private float updateTimer;

	
	private float updateDisketteTimer;
}
