using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class billboardScript : MonoBehaviour
{
	// Token: 0x060019D6 RID: 6614 RVA: 0x000116D0 File Offset: 0x0000F8D0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x000116D8 File Offset: 0x0000F8D8
	private void FindScripts()
	{
		this.camera_ = GameObject.Find("Camera");
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x000116EA File Offset: 0x0000F8EA
	private void Update()
	{
		base.gameObject.transform.LookAt(this.camera_.transform);
	}

	// Token: 0x0400210F RID: 8463
	private GameObject camera_;
}
