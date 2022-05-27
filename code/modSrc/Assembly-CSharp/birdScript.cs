using System;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class birdScript : MonoBehaviour
{
	// Token: 0x06001A24 RID: 6692 RVA: 0x00109B85 File Offset: 0x00107D85
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x00109B8D File Offset: 0x00107D8D
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

	// Token: 0x06001A26 RID: 6694 RVA: 0x00109BCC File Offset: 0x00107DCC
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

	// Token: 0x0400212A RID: 8490
	public mainScript mS_;

	// Token: 0x0400212B RID: 8491
	public GameObject main_;

	// Token: 0x0400212C RID: 8492
	public Animation myAnim;

	// Token: 0x0400212D RID: 8493
	private float updateTimer;

	// Token: 0x0400212E RID: 8494
	public float speed = 4f;

	// Token: 0x0400212F RID: 8495
	private float targetRotY;

	// Token: 0x04002130 RID: 8496
	private float flughoehe = 5f;

	// Token: 0x04002131 RID: 8497
	public bool resetPossible = true;
}
