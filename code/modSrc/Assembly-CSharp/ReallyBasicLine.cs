using System;
using UnityEngine;
using Vectrosity;

// Token: 0x02000375 RID: 885
public class ReallyBasicLine : MonoBehaviour
{
	// Token: 0x06002049 RID: 8265 RVA: 0x0014F468 File Offset: 0x0014D668
	private void Start()
	{
		VectorLine.SetLine(Color.white, new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2((float)(Screen.width - 1), (float)(Screen.height - 1))
		});
	}
}
