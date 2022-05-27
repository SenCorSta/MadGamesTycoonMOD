using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000374 RID: 884
public class Line : MonoBehaviour
{
	// Token: 0x06002047 RID: 8263 RVA: 0x0014F404 File Offset: 0x0014D604
	private void Start()
	{
		new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, 2f).Draw();
	}
}
