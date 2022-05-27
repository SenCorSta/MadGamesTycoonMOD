using System;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class uiAnimation : MonoBehaviour
{
	// Token: 0x06001AE5 RID: 6885 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x0010E1C0 File Offset: 0x0010C3C0
	private void OnEnable()
	{
		Debug.Log("LKJK" + UnityEngine.Random.Range(0, 100000).ToString() + " " + base.gameObject.name);
		base.GetComponent<Animator>().Play(this.anim);
		base.GetComponent<characterScript>().male = true;
	}

	// Token: 0x0400220A RID: 8714
	public string anim = "";
}
