using System;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class birdScript : MonoBehaviour
{
	// Token: 0x060019DA RID: 6618 RVA: 0x00011707 File Offset: 0x0000F907
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x0001170F File Offset: 0x0000F90F
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x0010DE20 File Offset: 0x0010C020
	private void Update()
	{
		base.transform.Translate(Vector3.forward * this.mS_.GetDeltaTime() * this.speed);
		float y = Mathf.LerpAngle(base.transform.eulerAngles.y, this.targetRotY, 0.05f);
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
		float y2 = Mathf.Lerp(base.transform.position.y, this.flughoehe, 0.05f);
		base.transform.position = new Vector3(base.transform.position.x, y2, base.transform.position.z);
		if (base.transform.position.x < -15f || base.transform.position.x > 90f || base.transform.position.z < -15f || base.transform.position.z > 65f)
		{
			if (this.resetPossible)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("Floor");
				if (array.Length != 0)
				{
					this.resetPossible = false;
					int num = UnityEngine.Random.Range(0, array.Length);
					if (array[num])
					{
						Vector3 eulerAngles = base.gameObject.transform.eulerAngles;
						base.gameObject.transform.LookAt(array[num].transform);
						base.gameObject.transform.eulerAngles = new Vector3(0f, base.gameObject.transform.eulerAngles.y, 0f);
						this.targetRotY = base.gameObject.transform.eulerAngles.y;
						base.gameObject.transform.eulerAngles = eulerAngles;
						this.flughoehe = UnityEngine.Random.Range(5f, 8f);
					}
				}
			}
		}
		else
		{
			this.resetPossible = true;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.2f)
		{
			return;
		}
		this.updateTimer = 0f;
		if (this.myAnim)
		{
			this.myAnim["BirdFly"].speed = this.mS_.GetGameSpeed();
		}
	}

	// Token: 0x04002110 RID: 8464
	public mainScript mS_;

	// Token: 0x04002111 RID: 8465
	public GameObject main_;

	// Token: 0x04002112 RID: 8466
	public Animation myAnim;

	// Token: 0x04002113 RID: 8467
	private float updateTimer;

	// Token: 0x04002114 RID: 8468
	public float speed = 4f;

	// Token: 0x04002115 RID: 8469
	private float targetRotY;

	// Token: 0x04002116 RID: 8470
	private float flughoehe = 5f;

	// Token: 0x04002117 RID: 8471
	public bool resetPossible = true;
}
