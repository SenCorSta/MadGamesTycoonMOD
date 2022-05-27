using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class particleFollowCamera : MonoBehaviour
{
	// Token: 0x06001ABD RID: 6845 RVA: 0x0010D110 File Offset: 0x0010B310
	private void Start()
	{
		base.transform.SetParent(null);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x0010D120 File Offset: 0x0010B320
	private void Update()
	{
		base.transform.position = new Vector3(this.cameraObject.transform.position.x, base.transform.position.y, this.cameraObject.transform.position.z);
	}

	// Token: 0x040021DE RID: 8670
	public GameObject cameraObject;
}
