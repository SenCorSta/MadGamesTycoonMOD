using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000371 RID: 881
public class Line : MonoBehaviour
{
	// Token: 0x06001FF4 RID: 8180 RVA: 0x0014FA18 File Offset: 0x0014DC18
	private void Start()
	{
		new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, 2f).Draw();
	}
}
