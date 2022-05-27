using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class CFX_InspectorHelp : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x00003A27 File Offset: 0x00001C27
	[ContextMenu("Unlock editing")]
	private void Unlock()
	{
		this.Locked = false;
	}

	// Token: 0x04000049 RID: 73
	public bool Locked;

	// Token: 0x0400004A RID: 74
	public string Title;

	// Token: 0x0400004B RID: 75
	public string HelpText;

	// Token: 0x0400004C RID: 76
	public int MsgType;
}
