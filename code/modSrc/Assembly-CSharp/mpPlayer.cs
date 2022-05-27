using System;
using Mirror;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class mpPlayer : NetworkBehaviour
{
	// Token: 0x060019A7 RID: 6567 RVA: 0x000114DD File Offset: 0x0000F6DD
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x000114EA File Offset: 0x0000F6EA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x000114F2 File Offset: 0x0000F6F2
	private void FindScripts()
	{
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x00002098 File Offset: 0x00000298
	private void Update()
	{
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x00011516 File Offset: 0x0000F716
	public override void OnStartServer()
	{
		this.FindScripts();
		this.playerID = this.mpCalls_.AddPlayer(this);
		Debug.Log("OnStartServer()");
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x0001153A File Offset: 0x0000F73A
	public override void OnStopServer()
	{
		this.FindScripts();
		this.mpCalls_.RemovePlayer(this.playerID);
		Debug.Log("OnStopServer()");
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x00002098 File Offset: 0x00000298
	private void MirrorProcessed()
	{
	}

	// Token: 0x040020CF RID: 8399
	public mpCalls mpCalls_;

	// Token: 0x040020D0 RID: 8400
	public int playerID = -1;
}
