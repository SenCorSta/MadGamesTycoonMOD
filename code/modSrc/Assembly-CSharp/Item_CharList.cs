using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000AB RID: 171
public class Item_CharList : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000642 RID: 1602 RVA: 0x0000598C File Offset: 0x00003B8C
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00061E30 File Offset: 0x00060030
	private void Init()
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
		base.gameObject.GetComponent<tooltip>().c = this.cS_.GetTooltip();
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00061EA0 File Offset: 0x000600A0
	private void Update()
	{
		if (!this.cS_)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			this.guiMain_.uiObjects[15].GetComponent<Menu_PickCharacter>().UpdateData();
			return;
		}
		if (this.mS_.pickedChars.Count > 0)
		{
			if (this.mS_.pickedChars[0] == this.cS_.gameObject)
			{
				this.uiObjects[0].GetComponent<Text>().text = "• " + this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00005994 File Offset: 0x00003B94
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.uiObjects[0].GetComponent<Text>().color = this.colors[1];
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x000059B4 File Offset: 0x00003BB4
	public void OnPointerExit(PointerEventData eventData)
	{
		this.uiObjects[0].GetComponent<Text>().color = this.colors[0];
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00061F8C File Offset: 0x0006018C
	public void BUTTON_Click()
	{
		if (!this.cS_)
		{
			return;
		}
		for (int i = 0; i < this.mS_.pickedChars.Count; i++)
		{
			if (this.mS_.pickedChars[i] == this.cS_.gameObject)
			{
				this.mS_.pickedChars.RemoveAt(i);
				break;
			}
		}
		this.mS_.pickedChars.Insert(0, this.cS_.gameObject);
		this.guiMain_.uiObjects[15].GetComponent<Menu_PickCharacter>().UpdateData();
	}

	// Token: 0x040009DC RID: 2524
	public GameObject[] uiObjects;

	// Token: 0x040009DD RID: 2525
	public Color[] colors;

	// Token: 0x040009DE RID: 2526
	public int slot;

	// Token: 0x040009DF RID: 2527
	public characterScript cS_;

	// Token: 0x040009E0 RID: 2528
	public mainScript mS_;

	// Token: 0x040009E1 RID: 2529
	public GUI_Main guiMain_;
}
