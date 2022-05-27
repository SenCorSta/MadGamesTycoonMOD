using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000279 RID: 633
public class stars : MonoBehaviour
{
	// Token: 0x060018BB RID: 6331 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x000FDAE8 File Offset: 0x000FBCE8
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

	// Token: 0x04001C44 RID: 7236
	public Color[] myColors;

	// Token: 0x04001C45 RID: 7237
	public GameObject[] myObjects;

	// Token: 0x04001C46 RID: 7238
	public int amount;
}
