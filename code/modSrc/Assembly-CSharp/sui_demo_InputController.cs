using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class sui_demo_InputController : MonoBehaviour
{
	// Token: 0x060000CD RID: 205 RVA: 0x000096C0 File Offset: 0x000078C0
	private void Update()
	{
		this.inputKeyW = Input.GetKey("w");
		this.inputKeyS = Input.GetKey("s");
		this.inputKeyA = Input.GetKey("a");
		this.inputKeyD = Input.GetKey("d");
		this.inputKeyQ = Input.GetKey("q");
		this.inputKeyE = Input.GetKey("e");
		this.inputMouseKey0 = Input.GetKey("mouse 0");
		this.inputMouseKey1 = Input.GetKey("mouse 1");
		this.inputMouseX = Input.GetAxisRaw("Mouse X");
		this.inputMouseY = Input.GetAxisRaw("Mouse Y");
		this.inputMouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");
		this.inputKeySHIFTL = Input.GetKey("left shift");
		this.inputKeySPACE = Input.GetKey("space");
		this.inputKeyF = Input.GetKey("f");
		this.inputKeyESC = Input.GetKey("escape");
	}

	// Token: 0x040001E2 RID: 482
	[HideInInspector]
	public bool inputMouseKey0;

	// Token: 0x040001E3 RID: 483
	[HideInInspector]
	public bool inputKeySHIFTL;

	// Token: 0x040001E4 RID: 484
	[HideInInspector]
	public bool inputKeySPACE;

	// Token: 0x040001E5 RID: 485
	[HideInInspector]
	public bool inputKeyW;

	// Token: 0x040001E6 RID: 486
	[HideInInspector]
	public bool inputKeyS;

	// Token: 0x040001E7 RID: 487
	[HideInInspector]
	public bool inputKeyA;

	// Token: 0x040001E8 RID: 488
	[HideInInspector]
	public bool inputKeyD;

	// Token: 0x040001E9 RID: 489
	[HideInInspector]
	public bool inputKeyF;

	// Token: 0x040001EA RID: 490
	[HideInInspector]
	public bool inputKeyQ;

	// Token: 0x040001EB RID: 491
	[HideInInspector]
	public bool inputKeyE;

	// Token: 0x040001EC RID: 492
	[HideInInspector]
	public bool inputKeyESC;

	// Token: 0x040001ED RID: 493
	[HideInInspector]
	public bool inputMouseKey1;

	// Token: 0x040001EE RID: 494
	[HideInInspector]
	public float inputMouseX;

	// Token: 0x040001EF RID: 495
	[HideInInspector]
	public float inputMouseY;

	// Token: 0x040001F0 RID: 496
	[HideInInspector]
	public float inputMouseWheel;
}
