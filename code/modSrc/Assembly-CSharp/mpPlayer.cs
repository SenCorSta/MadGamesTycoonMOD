﻿using System;
using Mirror;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class mpPlayer : NetworkBehaviour
{
	// Token: 0x060019F1 RID: 6641 RVA: 0x00108D4F File Offset: 0x00106F4F
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x00108D5C File Offset: 0x00106F5C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x00108D64 File Offset: 0x00106F64
	private void FindScripts()
	{
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x00002715 File Offset: 0x00000915
	private void Update()
	{
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x00108D88 File Offset: 0x00106F88
	public override void OnStartServer()
	{
		this.FindScripts();
		this.playerID = this.mpCalls_.AddPlayer(this);
		Debug.Log("OnStartServer()");
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x00108DAC File Offset: 0x00106FAC
	public override void OnStopServer()
	{
		this.FindScripts();
		this.mpCalls_.RemovePlayer(this.playerID);
		Debug.Log("OnStopServer()");
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x00002715 File Offset: 0x00000915
	private void MirrorProcessed()
	{
	}

	// Token: 0x040020E9 RID: 8425
	public mpCalls mpCalls_;

	// Token: 0x040020EA RID: 8426
	public int playerID = -1;
}
