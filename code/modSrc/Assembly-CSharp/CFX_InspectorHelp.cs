using System;
using UnityEngine;


public class CFX_InspectorHelp : MonoBehaviour
{
	
	[ContextMenu("Unlock editing")]
	private void Unlock()
	{
		this.Locked = false;
	}

	
	public bool Locked;

	
	public string Title;

	
	public string HelpText;

	
	public int MsgType;
}
