using System;
using UnityEngine;


public class createRobot : MonoBehaviour
{
	
	private void Start()
	{
	}

	
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

	
	public GameObject prefabRobot;

	
	public GameObject destroyThis;

	
	public GameObject myRobot;
}
