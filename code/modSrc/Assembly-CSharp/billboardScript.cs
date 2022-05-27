using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class billboardScript : MonoBehaviour
{
	// Token: 0x06001A20 RID: 6688 RVA: 0x00109B4E File Offset: 0x00107D4E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x00109B56 File Offset: 0x00107D56
	private void FindScripts()
	{
		this.camera_ = GameObject.Find("Camera");
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x00109B68 File Offset: 0x00107D68
	private void Update()
	{
		base.gameObject.transform.LookAt(this.camera_.transform);
	}

	// Token: 0x04002129 RID: 8489
	private GameObject camera_;
}
