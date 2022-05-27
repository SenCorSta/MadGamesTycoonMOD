using System;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class Menu_GameDesign : MonoBehaviour
{
	// Token: 0x06001077 RID: 4215 RVA: 0x000AEA40 File Offset: 0x000ACC40
	private void Start()
	{
		this.FindScripts();
		this.CreateEngineFeatures();
		this.CreateGameplayFeatures();
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x000AEA54 File Offset: 0x000ACC54
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		this.main_ = GameObject.Find("Main");
		this.engineFeatures_ = this.main_.GetComponent<engineFeatures>();
		this.gameplayFeatures_ = this.main_.GetComponent<gameplayFeatures>();
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x000AEAA4 File Offset: 0x000ACCA4
	private void CreateGameplayFeatures()
	{
		int num = 1;
		Transform transform = this.uiObjects[1].transform;
		int num2 = 0;
		this.NewItem(this.uiPrefabs[7], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int i = 0; i < this.gameplayFeatures_.gameplayFeatures_TYP.Length; i++)
		{
			if (this.gameplayFeatures_.gameplayFeatures_TYP[i] == this.gameplayFeatures_.GetTypGameplay())
			{
				this.NewItem(this.uiPrefabs[6], transform).GetComponent<Item_GameplayFeatures_GameDesign>().myID = i;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[2], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int j = 0; j < this.gameplayFeatures_.gameplayFeatures_TYP.Length; j++)
		{
			if (this.gameplayFeatures_.gameplayFeatures_TYP[j] == this.gameplayFeatures_.GetTypGrafik())
			{
				this.NewItem(this.uiPrefabs[6], transform).GetComponent<Item_GameplayFeatures_GameDesign>().myID = j;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[9], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int k = 0; k < this.gameplayFeatures_.gameplayFeatures_TYP.Length; k++)
		{
			if (this.gameplayFeatures_.gameplayFeatures_TYP[k] == this.gameplayFeatures_.GetTypSteuerung())
			{
				this.NewItem(this.uiPrefabs[6], transform).GetComponent<Item_GameplayFeatures_GameDesign>().myID = k;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[3], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int l = 0; l < this.gameplayFeatures_.gameplayFeatures_TYP.Length; l++)
		{
			if (this.gameplayFeatures_.gameplayFeatures_TYP[l] == this.gameplayFeatures_.GetTypSound())
			{
				this.NewItem(this.uiPrefabs[6], transform).GetComponent<Item_GameplayFeatures_GameDesign>().myID = l;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[8], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int m = 0; m < this.gameplayFeatures_.gameplayFeatures_TYP.Length; m++)
		{
			if (this.gameplayFeatures_.gameplayFeatures_TYP[m] == this.gameplayFeatures_.GetTypMultiplayer())
			{
				this.NewItem(this.uiPrefabs[6], transform).GetComponent<Item_GameplayFeatures_GameDesign>().myID = m;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		this.NewItems(this.uiPrefabs[1], transform, num + 1);
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000AED98 File Offset: 0x000ACF98
	private void CreateEngineFeatures()
	{
		int num = 1;
		Transform transform = this.uiObjects[0].transform;
		int num2 = 0;
		this.NewItem(this.uiPrefabs[2], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int i = 0; i < this.engineFeatures_.engineFeatures_TYP.Length; i++)
		{
			if (this.engineFeatures_.engineFeatures_TYP[i] == this.engineFeatures_.GetTypGrafik())
			{
				this.NewItem(this.uiPrefabs[0], transform).GetComponent<Item_EngineFeatures_GameDesign>().myID = i;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[3], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int j = 0; j < this.engineFeatures_.engineFeatures_TYP.Length; j++)
		{
			if (this.engineFeatures_.engineFeatures_TYP[j] == this.engineFeatures_.GetTypSound())
			{
				this.NewItem(this.uiPrefabs[0], transform).GetComponent<Item_EngineFeatures_GameDesign>().myID = j;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[4], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int k = 0; k < this.engineFeatures_.engineFeatures_TYP.Length; k++)
		{
			if (this.engineFeatures_.engineFeatures_TYP[k] == this.engineFeatures_.GetTypKI())
			{
				this.NewItem(this.uiPrefabs[0], transform).GetComponent<Item_EngineFeatures_GameDesign>().myID = k;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		num2 = 0;
		this.NewItem(this.uiPrefabs[5], transform);
		this.NewItem(this.uiPrefabs[1], transform);
		for (int l = 0; l < this.engineFeatures_.engineFeatures_TYP.Length; l++)
		{
			if (this.engineFeatures_.engineFeatures_TYP[l] == this.engineFeatures_.GetTypPhysik())
			{
				this.NewItem(this.uiPrefabs[0], transform).GetComponent<Item_EngineFeatures_GameDesign>().myID = l;
				num2++;
				if (num2 > num)
				{
					num2 = 0;
				}
			}
		}
		this.NewItems(this.uiPrefabs[1], transform, num2);
		this.NewItems(this.uiPrefabs[1], transform, num + 1);
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x000AEFFB File Offset: 0x000AD1FB
	private GameObject NewItem(GameObject newGO, Transform parent)
	{
		return UnityEngine.Object.Instantiate<GameObject>(newGO, new Vector3(0f, 0f, 0f), Quaternion.identity, parent);
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x000AF020 File Offset: 0x000AD220
	private void NewItems(GameObject newGO, Transform parent, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			UnityEngine.Object.Instantiate<GameObject>(newGO, new Vector3(0f, 0f, 0f), Quaternion.identity, parent);
		}
	}

	// Token: 0x040014E8 RID: 5352
	public GameObject[] uiObjects;

	// Token: 0x040014E9 RID: 5353
	public GameObject[] uiPrefabs;

	// Token: 0x040014EA RID: 5354
	private GameObject main_;

	// Token: 0x040014EB RID: 5355
	private engineFeatures engineFeatures_;

	// Token: 0x040014EC RID: 5356
	private gameplayFeatures gameplayFeatures_;
}
