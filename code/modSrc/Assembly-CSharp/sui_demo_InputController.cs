using System;
using UnityEngine;


public class sui_demo_InputController : MonoBehaviour
{
	
	private void Update()
	{
		this.inputKeyW = Input.GetKey("w");
		this.inputKeyS = Input.GetKey("s");
		this.inputKeyA = Input.GetKey("a");
		this.inputKeyD = Input.GetKey("d");
		this.inputKeyQ = Input.GetKey("q");
		this.inputKeyE = Input.GetKey("e");
		this.inputMouseKey0 = Input.GetKey("mouse 0");
		this.inputMouseKey1 = Input.GetKey("mouse 1");
		this.inputMouseX = Input.GetAxisRaw("Mouse X");
		this.inputMouseY = Input.GetAxisRaw("Mouse Y");
		this.inputMouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");
		this.inputKeySHIFTL = Input.GetKey("left shift");
		this.inputKeySPACE = Input.GetKey("space");
		this.inputKeyF = Input.GetKey("f");
		this.inputKeyESC = Input.GetKey("escape");
	}

	
	[HideInInspector]
	public bool inputMouseKey0;

	
	[HideInInspector]
	public bool inputKeySHIFTL;

	
	[HideInInspector]
	public bool inputKeySPACE;

	
	[HideInInspector]
	public bool inputKeyW;

	
	[HideInInspector]
	public bool inputKeyS;

	
	[HideInInspector]
	public bool inputKeyA;

	
	[HideInInspector]
	public bool inputKeyD;

	
	[HideInInspector]
	public bool inputKeyF;

	
	[HideInInspector]
	public bool inputKeyQ;

	
	[HideInInspector]
	public bool inputKeyE;

	
	[HideInInspector]
	public bool inputKeyESC;

	
	[HideInInspector]
	public bool inputMouseKey1;

	
	[HideInInspector]
	public float inputMouseX;

	
	[HideInInspector]
	public float inputMouseY;

	
	[HideInInspector]
	public float inputMouseWheel;
}
