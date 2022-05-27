using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000AB RID: 171
public class Item_CharList : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600064B RID: 1611 RVA: 0x0004F31B File Offset: 0x0004D51B
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0004F324 File Offset: 0x0004D524
	private void Init()
	{
		if (!this.cS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.cS_.GetGroupString("orange") + " " + this.cS_.myName;
		base.gameObject.GetComponent<tooltip>().c = this.cS_.GetTooltip();
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0004F394 File Offset: 0x0004D594
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

	// Token: 0x0600064E RID: 1614 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0004F47F File Offset: 0x0004D67F
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.uiObjects[0].GetComponent<Text>().color = this.colors[1];
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0004F49F File Offset: 0x0004D69F
	public void OnPointerExit(PointerEventData eventData)
	{
		this.uiObjects[0].GetComponent<Text>().color = this.colors[0];
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0004F4C0 File Offset: 0x0004D6C0
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
