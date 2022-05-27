using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000270 RID: 624
public class keyInfo : MonoBehaviour
{
	// Token: 0x0600182D RID: 6189 RVA: 0x000F7758 File Offset: 0x000F5958
	private void Start()
	{
		string text = "";
		for (int i = 0; i < this.keys.Length; i++)
		{
			text += this.keys[i].ToString();
			if (i < this.keys.Length - 1)
			{
				text += " & ";
			}
		}
		this.uiObjects[0].GetComponent<Text>().text = text;
	}

	// Token: 0x04001C06 RID: 7174
	public KeyCode[] keys;

	// Token: 0x04001C07 RID: 7175
	public GameObject[] uiObjects;
}
