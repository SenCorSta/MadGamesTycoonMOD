using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006A RID: 106
public class DisableInputSearch : MonoBehaviour
{
	// Token: 0x060003FB RID: 1019 RVA: 0x0000430C File Offset: 0x0000250C
	private void OnDisable()
	{
		if (base.GetComponent<InputField>())
		{
			base.GetComponent<InputField>().text = "";
		}
	}
}
