using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class uiAnimation : MonoBehaviour
{
	// Token: 0x06001A9B RID: 6811 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x00111D78 File Offset: 0x0010FF78
	private void OnEnable()
	{
		Debug.Log("LKJK" + UnityEngine.Random.Range(0, 100000).ToString() + " " + base.gameObject.name);
		base.GetComponent<Animator>().Play(this.anim);
		base.GetComponent<characterScript>().male = true;
	}

	// Token: 0x040021F0 RID: 8688
	public string anim = "";
}
