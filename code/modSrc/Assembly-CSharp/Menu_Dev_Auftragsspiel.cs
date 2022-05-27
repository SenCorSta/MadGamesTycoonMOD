using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010A RID: 266
public class Menu_Dev_Auftragsspiel : MonoBehaviour
{
	// Token: 0x06000889 RID: 2185 RVA: 0x000064D6 File Offset: 0x000046D6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0006F9B4 File Offset: 0x0006DBB4
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
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x000064DE File Offset: 0x000046DE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0006FA9C File Offset: 0x0006DC9C
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0006FAE8 File Offset: 0x0006DCE8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ContractAuftragsspiel>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00006516 File Offset: 0x00004716
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0006FB44 File Offset: 0x0006DD44
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(600));
		list.Add(this.tS_.GetText(627));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(602));
		list.Add(this.tS_.GetText(327));
		list.Add(this.tS_.GetText(604));
		list.Add(this.tS_.GetText(625));
		list.Add(this.tS_.GetText(273));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0006FC54 File Offset: 0x0006DE54
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0006FCB0 File Offset: 0x0006DEB0
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.auftragsspiel && !component.auftragsspiel_Inivs)
				{
					if (!component.pS_)
					{
						component.FindMyPublisher();
					}
					if (component.pS_ && !component.pS_.tochterfirma && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_ContractAuftragsspiel component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ContractAuftragsspiel>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.games_ = this.games_;
						component2.rS_ = this.rS_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0006FE14 File Offset: 0x0006E014
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_ContractAuftragsspiel component = gameObject.GetComponent<Item_ContractAuftragsspiel>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.auftragsspiel_gehalt.ToString();
					break;
				case 1:
					gameObject.name = component.game_.auftragsspiel_bonus.ToString();
					break;
				case 2:
					gameObject.name = component.game_.auftragsspiel_mindestbewertung.ToString();
					break;
				case 3:
					gameObject.name = component.game_.auftragsspiel_zeitInWochen.ToString();
					break;
				case 4:
					gameObject.name = component.game_.gameSize.ToString();
					break;
				case 5:
					gameObject.name = component.game_.publisherID.ToString();
					break;
				case 6:
					gameObject.name = component.game_.gamePlatform[0].ToString();
					break;
				case 7:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00006524 File Offset: 0x00004724
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[95]);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00006558 File Offset: 0x00004758
	public void BUTTON_PlatformKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[33]);
	}

	// Token: 0x04000D16 RID: 3350
	public GameObject[] uiPrefabs;

	// Token: 0x04000D17 RID: 3351
	public GameObject[] uiObjects;

	// Token: 0x04000D18 RID: 3352
	private mainScript mS_;

	// Token: 0x04000D19 RID: 3353
	private GameObject main_;

	// Token: 0x04000D1A RID: 3354
	private GUI_Main guiMain_;

	// Token: 0x04000D1B RID: 3355
	private sfxScript sfx_;

	// Token: 0x04000D1C RID: 3356
	private textScript tS_;

	// Token: 0x04000D1D RID: 3357
	private genres genres_;

	// Token: 0x04000D1E RID: 3358
	private games games_;

	// Token: 0x04000D1F RID: 3359
	public roomScript rS_;

	// Token: 0x04000D20 RID: 3360
	private float updateTimer;
}
