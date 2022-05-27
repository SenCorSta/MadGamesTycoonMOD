using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class dartPfeil : MonoBehaviour
{
	// Token: 0x06001A47 RID: 6727 RVA: 0x0010A503 File Offset: 0x00108703
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x0010A50C File Offset: 0x0010870C
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

	// Token: 0x06001A49 RID: 6729 RVA: 0x0010A576 File Offset: 0x00108776
	private void OnEnable()
	{
		this.timer = 0.72700006f;
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x0010A584 File Offset: 0x00108784
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

	// Token: 0x06001A4B RID: 6731 RVA: 0x0010A6B3 File Offset: 0x001088B3
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

	// Token: 0x04002150 RID: 8528
	private GameObject main_;

	// Token: 0x04002151 RID: 8529
	private mainScript mS_;

	// Token: 0x04002152 RID: 8530
	private sfxScript sfx_;

	// Token: 0x04002153 RID: 8531
	public GameObject prefabFlyingDart;

	// Token: 0x04002154 RID: 8532
	private GameObject myDart;

	// Token: 0x04002155 RID: 8533
	public float timer;
}
