using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class dartPfeil : MonoBehaviour
{
	// Token: 0x060019FD RID: 6653 RVA: 0x0001183A File Offset: 0x0000FA3A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x0010E66C File Offset: 0x0010C86C
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

	// Token: 0x060019FF RID: 6655 RVA: 0x00011842 File Offset: 0x0000FA42
	private void OnEnable()
	{
		this.timer = 0.72700006f;
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x0010E6D8 File Offset: 0x0010C8D8
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

	// Token: 0x06001A01 RID: 6657 RVA: 0x0001184F File Offset: 0x0000FA4F
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

	// Token: 0x04002136 RID: 8502
	private GameObject main_;

	// Token: 0x04002137 RID: 8503
	private mainScript mS_;

	// Token: 0x04002138 RID: 8504
	private sfxScript sfx_;

	// Token: 0x04002139 RID: 8505
	public GameObject prefabFlyingDart;

	// Token: 0x0400213A RID: 8506
	private GameObject myDart;

	// Token: 0x0400213B RID: 8507
	public float timer;
}
