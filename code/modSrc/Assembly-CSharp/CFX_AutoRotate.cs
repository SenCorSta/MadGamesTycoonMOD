using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class CFX_AutoRotate : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x000039F5 File Offset: 0x00001BF5
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime, this.space);
	}

	// Token: 0x04000047 RID: 71
	public Vector3 rotation;

	// Token: 0x04000048 RID: 72
	public Space space = Space.Self;
}
