using System;
using UnityEngine;


public class birdScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public mainScript mS_;

	
	public GameObject main_;

	
	public Animation myAnim;

	
	private float updateTimer;

	
	public float speed = 4f;

	
	private float targetRotY;

	
	private float flughoehe = 5f;

	
	public bool resetPossible = true;
}
