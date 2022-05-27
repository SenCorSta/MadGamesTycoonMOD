using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006A RID: 106
public class DisableInputSearch : MonoBehaviour
{
	// Token: 0x06000403 RID: 1027 RVA: 0x0003E805 File Offset: 0x0003CA05
	private void OnDisable()
	{
		if (base.GetComponent<InputField>())
		{
			base.GetComponent<InputField>().text = "";
		}
	}
}
