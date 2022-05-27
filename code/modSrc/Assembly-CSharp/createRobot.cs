using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class createRobot : MonoBehaviour
{
	// Token: 0x060019F9 RID: 6649 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x0010E550 File Offset: 0x0010C750
	public void Init(int id_)
	{
		if (this.myRobot)
		{
			UnityEngine.Object.Destroy(this.myRobot);
		}
		this.myRobot = UnityEngine.Object.Instantiate<GameObject>(this.prefabRobot, base.transform.position, Quaternion.identity);
		this.myRobot.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, base.transform.eulerAngles.z + 180f);
		this.myRobot.GetComponent<Animation>().Play();
		this.myRobot.GetComponent<robotScript>().stationID = id_;
		UnityEngine.Object.Destroy(this.destroyThis);
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x0010E610 File Offset: 0x0010C810
	private void OnDestroy()
	{
		if (this.myRobot)
		{
			GameObject myTarget = this.myRobot.GetComponent<robotScript>().myTarget;
			if (myTarget && myTarget.tag == "Muell_InUse")
			{
				myTarget.tag = "Muell";
			}
			UnityEngine.Object.Destroy(this.myRobot);
		}
	}

	// Token: 0x04002133 RID: 8499
	public GameObject prefabRobot;

	// Token: 0x04002134 RID: 8500
	public GameObject destroyThis;

	// Token: 0x04002135 RID: 8501
	public GameObject myRobot;
}
