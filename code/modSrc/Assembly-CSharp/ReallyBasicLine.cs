using System;
using UnityEngine;
using Vectrosity;

// Token: 0x02000372 RID: 882
public class ReallyBasicLine : MonoBehaviour
{
	// Token: 0x06001FF6 RID: 8182 RVA: 0x0014FA7C File Offset: 0x0014DC7C
	private void Start()
	{
		VectorLine.SetLine(Color.white, new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2((float)(Screen.width - 1), (float)(Screen.height - 1))
		});
	}
}
