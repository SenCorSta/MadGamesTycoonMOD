using System;
using UnityEngine;


public class sui_demo_ControllerMaster : MonoBehaviour
{
	
	private void Start()
	{
		this.characterController = base.gameObject.GetComponent<sui_demo_ControllerCharacter>();
		this.boatController = base.gameObject.GetComponent<sui_demo_ControllerBoat>();
		this.orbitController = base.gameObject.GetComponent<sui_demo_ControllerOrbit>();
	}

	
	private void LateUpdate()
	{
		if (this.currentControllerType != this.useController)
		{
			this.resetController = true;
		}
		else
		{
			this.resetController = false;
		}
		if (this.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.none)
		{
			if (this.characterController != null)
			{
				this.characterController.isActive = false;
			}
			if (this.boatController != null)
			{
				this.boatController.isActive = false;
			}
			if (this.orbitController != null)
			{
				this.orbitController.isActive = false;
			}
		}
		if (this.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.character)
		{
			if (this.boatController != null)
			{
				this.boatController.isActive = false;
			}
			if (this.orbitController != null)
			{
				this.orbitController.isActive = false;
			}
			if (this.characterController != null)
			{
				this.characterController.isActive = true;
			}
		}
		if (this.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.boat)
		{
			if (this.characterController != null)
			{
				this.characterController.isActive = false;
			}
			if (this.orbitController != null)
			{
				this.orbitController.isActive = false;
			}
			if (this.boatController != null)
			{
				this.boatController.isActive = true;
			}
		}
		if (this.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.orbit)
		{
			if (this.characterController != null)
			{
				this.characterController.isActive = false;
			}
			if (this.boatController != null)
			{
				this.boatController.isActive = false;
			}
			if (this.orbitController != null)
			{
				this.orbitController.isActive = true;
			}
		}
		if (this.characterController != null)
		{
			if (this.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.boat)
			{
				this.characterController.isInBoat = true;
				this.characterController.cameraTarget.transform.position = this.boatController.targetAnimator.playerPosition.transform.position;
				this.characterController.cameraTarget.transform.rotation = this.boatController.targetAnimator.playerPosition.transform.rotation;
				this.characterController.cameraTarget.gameObject.GetComponent<Collider>().enabled = false;
				this.characterController.cameraTarget.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			}
			if (this.currentControllerType == sui_demo_ControllerMaster.Sui_Demo_ControllerType.character && this.resetController)
			{
				this.characterController.isInBoat = false;
				this.characterController.cameraTarget.transform.position = this.boatController.targetAnimator.playerExit.transform.position;
				this.characterController.cameraTarget.gameObject.GetComponent<Collider>().enabled = true;
				this.characterController.cameraTarget.gameObject.GetComponent<Rigidbody>().useGravity = true;
				this.characterController.cameraTarget.gameObject.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		if (this.resetController)
		{
			this.resetController = false;
			this.useController = this.currentControllerType;
		}
	}

	
	public Transform cameraObject;

	
	public sui_demo_ControllerMaster.Sui_Demo_ControllerType currentControllerType = sui_demo_ControllerMaster.Sui_Demo_ControllerType.character;

	
	private sui_demo_ControllerCharacter characterController;

	
	private sui_demo_ControllerBoat boatController;

	
	private sui_demo_ControllerOrbit orbitController;

	
	private bool resetController;

	
	private sui_demo_ControllerMaster.Sui_Demo_ControllerType useController = sui_demo_ControllerMaster.Sui_Demo_ControllerType.character;

	
	public enum Sui_Demo_ControllerType
	{
		
		none,
		
		character,
		
		boat,
		
		orbit
	}
}
