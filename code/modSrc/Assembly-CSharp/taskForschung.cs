using System;
using UnityEngine;


public class taskForschung : MonoBehaviour
{
	
	private void Awake()
	{
		base.transform.position = new Vector3(10f, 0f, 0f);
	}

	
	private void Start()
	{
		this.FindScripts();
	}

	
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	
	public float GetProzent()
	{
		this.FindScripts();
		switch (this.typ)
		{
		case 0:
			return this.genres_.GetProzent(this.slot);
		case 1:
			return this.themes_.GetProzent(this.slot);
		case 2:
			return this.eF_.GetProzent(this.slot);
		case 3:
			return this.gF_.GetProzent(this.slot);
		case 4:
			return this.hardware_.GetProzent(this.slot);
		case 5:
			return this.fS_.GetProzent(this.slot);
		case 6:
			return this.hardwareFeatures_.GetProzent(this.slot);
		default:
			return -1f;
		}
	}

	
	public void Work(float f)
	{
		this.FindScripts();
		switch (this.typ)
		{
		case 0:
			if (float.IsNaN(this.genres_.genres_RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.genres_.genres_RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.genres_.genres_RES_POINTS_LEFT[this.slot] -= f;
				if (this.genres_.genres_RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.genres_.genres_RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
					return;
				}
			}
			break;
		case 1:
			if (float.IsNaN(this.themes_.themes_RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.themes_.themes_RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.themes_.themes_RES_POINTS_LEFT[this.slot] -= f;
				if (this.themes_.themes_RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.themes_.themes_RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
					return;
				}
			}
			break;
		case 2:
			if (float.IsNaN(this.eF_.engineFeatures_RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.eF_.engineFeatures_RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.eF_.engineFeatures_RES_POINTS_LEFT[this.slot] -= f;
				if (this.eF_.engineFeatures_RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.eF_.engineFeatures_RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
					return;
				}
			}
			break;
		case 3:
			if (float.IsNaN(this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.slot] -= f;
				if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.gF_.gameplayFeatures_RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
					return;
				}
			}
			break;
		case 4:
			if (float.IsNaN(this.hardware_.hardware_RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.hardware_.hardware_RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.hardware_.hardware_RES_POINTS_LEFT[this.slot] -= f;
				if (this.hardware_.hardware_RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.hardware_.hardware_RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
					return;
				}
			}
			break;
		case 5:
			if (float.IsNaN(this.fS_.RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.fS_.RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.fS_.RES_POINTS_LEFT[this.slot] -= f;
				if (this.fS_.RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.fS_.RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
					return;
				}
			}
			break;
		case 6:
			if (float.IsNaN(this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.slot]))
			{
				this.Complete();
			}
			if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.slot] > 0f)
			{
				this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.slot] -= f;
				if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.slot] <= 0f)
				{
					this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[this.slot] = 0f;
					this.Complete();
				}
			}
			break;
		default:
			return;
		}
	}

	
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
		switch (this.typ)
		{
		case 0:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.genres_.GetName(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.genres_.GetPic(this.slot), tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		case 1:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.tS_.GetThemes(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.themes_.icon, tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		case 2:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.eF_.GetName(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.eF_.GetTypPic(this.slot), tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		case 3:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.gF_.GetName(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.gF_.GetTypSprite(this.slot), tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		case 4:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.hardware_.GetName(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.hardware_.GetTypPic(this.slot), tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		case 5:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.fS_.GetName(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.fS_.RES_SPRITE[this.slot], tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		case 6:
		{
			string tooltip_ = this.tS_.GetText(165) + "\n<b>" + this.hardwareFeatures_.GetName(this.slot) + "</b>";
			this.guiMain_.CreateLeftNews(roomID_, this.hardwareFeatures_.GetSprite(this.slot), tooltip_, this.rdS_.roomData_SPRITE[2]);
			break;
		}
		}
		this.unlock_.CheckUnlock(true);
		if (this.mS_.multiplayer && this.mS_.mpCalls_)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Forschung(this.mS_.myID);
			}
			else
			{
				this.mS_.mpCalls_.CLIENT_Send_Forschung();
			}
		}
		if (!this.DoAutomatic())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private bool DoAutomatic()
	{
		if (!this.automatic)
		{
			return false;
		}
		bool flag = false;
		switch (this.typ)
		{
		case 0:
			for (int i = 0; i < this.genres_.genres_RES_POINTS_LEFT.Length; i++)
			{
				if (this.genres_.genres_UNLOCK[i])
				{
					if (this.genres_.genres_RES_POINTS_LEFT[i] > 0f)
					{
						flag = true;
					}
					if (this.genres_.genres_RES_POINTS_LEFT[i] > 0f && this.genres_.Pay(i) && !this.genres_.BereitsInAnderenRaumAktiv(i))
					{
						this.slot = i;
						return true;
					}
				}
			}
			break;
		case 1:
			for (int i = 0; i < this.themes_.themes_RES_POINTS_LEFT.Length; i++)
			{
				if (this.themes_.themes_RES_POINTS_LEFT[i] > 0f)
				{
					flag = true;
				}
				if (this.themes_.themes_RES_POINTS_LEFT[i] > 0f && this.themes_.Pay(i) && !this.themes_.BereitsInAnderenRaumAktiv(i))
				{
					this.slot = i;
					return true;
				}
			}
			break;
		case 2:
			for (int i = 0; i < this.eF_.engineFeatures_RES_POINTS_LEFT.Length; i++)
			{
				if (this.eF_.engineFeatures_UNLOCK[i])
				{
					if (this.eF_.engineFeatures_RES_POINTS_LEFT[i] > 0f)
					{
						flag = true;
					}
					if (this.eF_.engineFeatures_RES_POINTS_LEFT[i] > 0f && this.eF_.Pay(i) && !this.eF_.BereitsInAnderenRaumAktiv(i))
					{
						this.slot = i;
						return true;
					}
				}
			}
			break;
		case 3:
			for (int i = 0; i < this.gF_.gameplayFeatures_RES_POINTS_LEFT.Length; i++)
			{
				if (this.gF_.gameplayFeatures_UNLOCK[i])
				{
					if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[i] > 0f)
					{
						flag = true;
					}
					if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[i] > 0f && this.gF_.Pay(i) && !this.gF_.BereitsInAnderenRaumAktiv(i))
					{
						this.slot = i;
						return true;
					}
				}
			}
			break;
		case 4:
			for (int i = 0; i < this.hardware_.hardware_RES_POINTS_LEFT.Length; i++)
			{
				if (this.hardware_.hardware_UNLOCK[i])
				{
					if (this.hardware_.hardware_RES_POINTS_LEFT[i] > 0f)
					{
						flag = true;
					}
					if (this.hardware_.hardware_RES_POINTS_LEFT[i] > 0f && this.hardware_.Pay(i) && !this.hardware_.BereitsInAnderenRaumAktiv(i))
					{
						this.slot = i;
						return true;
					}
				}
			}
			break;
		case 5:
		{
			int amountForschung = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>().GetAmountForschung(5, true);
			if (amountForschung != -1 && !this.fS_.BereitsInAnderenRaumAktiv(amountForschung))
			{
				if (this.fS_.RES_POINTS_LEFT[amountForschung] > 0f)
				{
					flag = true;
				}
				if (this.fS_.RES_POINTS_LEFT[amountForschung] > 0f && this.fS_.Pay(amountForschung))
				{
					this.slot = amountForschung;
					return true;
				}
			}
			break;
		}
		case 6:
			for (int i = 0; i < this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT.Length; i++)
			{
				if (this.hardwareFeatures_.hardFeat_UNLOCK[i])
				{
					if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[i] > 0f)
					{
						flag = true;
					}
					if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[i] > 0f && this.hardwareFeatures_.Pay(i) && !this.hardwareFeatures_.BereitsInAnderenRaumAktiv(i))
					{
						this.slot = i;
						return true;
					}
				}
			}
			break;
		}
		if (flag)
		{
			this.LeftNews(this.tS_.GetText(728), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[2]);
		}
		else
		{
			this.LeftNews(this.tS_.GetText(1376), this.guiMain_.uiSprites[16], this.rdS_.roomData_SPRITE[2]);
		}
		return false;
	}

	
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

	
	public int GetRueckgeld()
	{
		int result = 0;
		switch (this.typ)
		{
		case 0:
			if (!this.genres_.ForschungGestartet(this.slot))
			{
				result = this.genres_.GetPrice(this.slot);
			}
			break;
		case 1:
			if (!this.themes_.ForschungGestartet(this.slot))
			{
				result = this.themes_.GetPrice(this.slot);
			}
			break;
		case 2:
			if (!this.eF_.ForschungGestartet(this.slot))
			{
				result = this.eF_.GetPrice(this.slot);
			}
			break;
		case 3:
			if (!this.gF_.ForschungGestartet(this.slot))
			{
				result = this.gF_.GetPrice(this.slot);
			}
			break;
		case 4:
			if (!this.hardware_.ForschungGestartet(this.slot))
			{
				result = this.hardware_.GetPrice(this.slot);
			}
			break;
		case 5:
			if (!this.fS_.ForschungGestartet(this.slot))
			{
				result = this.fS_.GetPrice(this.slot);
			}
			break;
		case 6:
			if (!this.hardwareFeatures_.ForschungGestartet(this.slot))
			{
				result = this.hardwareFeatures_.GetPrice(this.slot);
			}
			break;
		}
		return result;
	}

	
	public void Abbrechen()
	{
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
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public int myID = -1;

	
	public int typ = -1;

	
	public int slot = -1;

	
	public bool automatic;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private genres genres_;

	
	private themes themes_;

	
	private engineFeatures eF_;

	
	private gameplayFeatures gF_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private GUI_Main guiMain_;

	
	private textScript tS_;

	
	private roomDataScript rdS_;

	
	private unlockScript unlock_;

	
	private forschungSonstiges fS_;
}
