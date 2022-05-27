using System;
using System.Collections;
using UnityEngine;


public class dartPfeil : MonoBehaviour
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	
	private void OnEnable()
	{
		this.timer = 0.72700006f;
	}

	
	private void Update()
	{
		this.timer += this.mS_.GetDeltaTime();
		if ((double)this.timer >= 1.417)
		{
			this.timer = 0f;
			if (this.myDart)
			{
				UnityEngine.Object.Destroy(this.myDart);
			}
			this.myDart = UnityEngine.Object.Instantiate<GameObject>(this.prefabFlyingDart);
			this.myDart.transform.position = base.gameObject.transform.position;
			this.myDart.transform.rotation = base.gameObject.transform.root.transform.rotation;
			this.myDart.transform.eulerAngles = new Vector3(this.myDart.transform.eulerAngles.x + (float)UnityEngine.Random.Range(-15, 15), this.myDart.transform.eulerAngles.y + (float)UnityEngine.Random.Range(-30, -15), this.myDart.transform.eulerAngles.z);
			base.StartCoroutine(this.Fly());
		}
	}

	
	private IEnumerator Fly()
	{
		base.gameObject.GetComponent<MeshRenderer>().enabled = false;
		Vector3 startPos = this.myDart.transform.position;
		while (Vector3.Distance(startPos, this.myDart.transform.position) < 0.65f)
		{
			this.myDart.transform.Translate(Vector3.forward * this.mS_.GetDeltaTime() * 5f);
			yield return new WaitForEndOfFrame();
		}
		UnityEngine.Object.Destroy(this.myDart);
		if (this.myDart.GetComponent<MeshRenderer>().isVisible)
		{
			this.sfx_.PlaySound(42, true);
		}
		base.gameObject.GetComponent<MeshRenderer>().enabled = true;
		yield break;
	}

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private sfxScript sfx_;

	
	public GameObject prefabFlyingDart;

	
	private GameObject myDart;

	
	public float timer;
}
