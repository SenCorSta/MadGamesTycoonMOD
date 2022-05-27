using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class sui_demo_ControllerMaster : MonoBehaviour
{
	// Token: 0x060000C7 RID: 199 RVA: 0x0000287A File Offset: 0x00000A7A
	private void Start()
	{
		this.characterController = base.gameObject.GetComponent<sui_demo_ControllerCharacter>();
		this.boatController = base.gameObject.GetComponent<sui_demo_ControllerBoat>();
		this.orbitController = base.gameObject.GetComponent<sui_demo_ControllerOrbit>();
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0001EF2C File Offset: 0x0001D12C
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

	// Token: 0x040001A3 RID: 419
	public Transform cameraObject;

	// Token: 0x040001A4 RID: 420
	public sui_demo_ControllerMaster.Sui_Demo_ControllerType currentControllerType = sui_demo_ControllerMaster.Sui_Demo_ControllerType.character;

	// Token: 0x040001A5 RID: 421
	private sui_demo_ControllerCharacter characterController;

	// Token: 0x040001A6 RID: 422
	private sui_demo_ControllerBoat boatController;

	// Token: 0x040001A7 RID: 423
	private sui_demo_ControllerOrbit orbitController;

	// Token: 0x040001A8 RID: 424
	private bool resetController;

	// Token: 0x040001A9 RID: 425
	private sui_demo_ControllerMaster.Sui_Demo_ControllerType useController = sui_demo_ControllerMaster.Sui_Demo_ControllerType.character;

	// Token: 0x02000036 RID: 54
	public enum Sui_Demo_ControllerType
	{
		// Token: 0x040001AB RID: 427
		none,
		// Token: 0x040001AC RID: 428
		character,
		// Token: 0x040001AD RID: 429
		boat,
		// Token: 0x040001AE RID: 430
		orbit
	}
}
