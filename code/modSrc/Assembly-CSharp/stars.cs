using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027D RID: 637
public class stars : MonoBehaviour
{
	// Token: 0x06001900 RID: 6400 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x000F8D20 File Offset: 0x000F6F20
	private void Update()
	{
		for (int i = 0; i < this.myObjects.Length; i++)
		{
			if (this.amount > i)
			{
				this.myObjects[i].GetComponent<Image>().color = this.myColors[0];
			}
			else
			{
				this.myObjects[i].GetComponent<Image>().color = this.myColors[1];
			}
		}
	}

	// Token: 0x04001C5F RID: 7263
	public Color[] myColors;

	// Token: 0x04001C60 RID: 7264
	public GameObject[] myObjects;

	// Token: 0x04001C61 RID: 7265
	public int amount;
}
