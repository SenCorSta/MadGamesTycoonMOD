using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000274 RID: 628
public class keyInfo : MonoBehaviour
{
	// Token: 0x06001870 RID: 6256 RVA: 0x000F252C File Offset: 0x000F072C
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

	// Token: 0x04001C20 RID: 7200
	public KeyCode[] keys;

	// Token: 0x04001C21 RID: 7201
	public GameObject[] uiObjects;
}
