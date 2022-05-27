using System;
using Mirror;
using UnityEngine;


public class mpPlayer : NetworkBehaviour
{
	
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void Update()
	{
	}

	
	public override void OnStartServer()
	{
		this.FindScripts();
		this.playerID = this.mpCalls_.AddPlayer(this);
		Debug.Log("OnStartServer()");
	}

	
	public override void OnStopServer()
	{
		this.FindScripts();
		this.mpCalls_.RemovePlayer(this.playerID);
		Debug.Log("OnStopServer()");
	}

	
	private void MirrorProcessed()
	{
	}

	
	public mpCalls mpCalls_;

	
	public int playerID = -1;
}
