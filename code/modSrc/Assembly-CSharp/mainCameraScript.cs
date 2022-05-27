using System;
using ReachableGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x0200032B RID: 811
public class mainCameraScript : MonoBehaviour
{
	// Token: 0x06001CD0 RID: 7376 RVA: 0x0011E78D File Offset: 0x0011C98D
	private void Start()
	{
		this.cmS_ = base.transform.root.gameObject.GetComponent<cameraMovementScript>();
		this.cameraPosition = base.transform.localPosition;
		this.InitPostProcess();
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x0011E7C1 File Offset: 0x0011C9C1
	private void InitPostProcess()
	{
		this.postVolume.profile.TryGetSettings<PostLiner>(out this.postLiner);
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x0011E7DC File Offset: 0x0011C9DC
	public void SetOutlineColor(int fillColor_, float fillBlend_, int outlineColor_)
	{
		if (this.postLiner == null)
		{
			this.InitPostProcess();
			return;
		}
		this.postLiner.fillColor.Override(this.colorParameter[fillColor_]);
		this.postLiner.fillBlend.Override(fillBlend_);
		this.postLiner.outlineColor.Override(this.colorParameter[outlineColor_]);
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x0011E849 File Offset: 0x0011CA49
	private void Update()
	{
		this.CameraInput();
		this.LookAtCameraMovement();
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x0011E857 File Offset: 0x0011CA57
	private void LookAtCameraMovement()
	{
		base.transform.LookAt(base.transform.parent.transform);
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x0011E874 File Offset: 0x0011CA74
	private void CameraInput()
	{
		if (!this.cmS_.guiMain_)
		{
			return;
		}
		if (this.cmS_.disableMovement)
		{
			return;
		}
		if (Input.mouseScrollDelta.y != 0f)
		{
			if (!Input.GetMouseButton(1))
			{
				Vector3 localPosition = base.transform.localPosition;
				base.transform.localPosition = this.cameraPosition;
				if (!EventSystem.current.IsPointerOverGameObject() || this.cmS_.guiMain_.uiObjects[252].activeSelf)
				{
					base.transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * this.zoomSpeed);
				}
				this.cameraPosition = base.transform.localPosition;
				base.transform.localPosition = localPosition;
				if (this.cameraPosition.y < this.maxZoomIn)
				{
					this.cameraPosition = localPosition;
				}
				if (this.cameraPosition.y > this.maxZoomOut)
				{
					this.cameraPosition = localPosition;
				}
			}
		}
		else
		{
			float d = 0f;
			if (Input.GetKey(KeyCode.PageUp))
			{
				d = 0.2f;
			}
			if (Input.GetKey(KeyCode.PageDown))
			{
				d = -0.2f;
			}
			Vector3 localPosition2 = base.transform.localPosition;
			base.transform.localPosition = this.cameraPosition;
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				base.transform.Translate(Vector3.forward * d * this.zoomSpeed);
			}
			this.cameraPosition = base.transform.localPosition;
			base.transform.localPosition = localPosition2;
			if (this.cameraPosition.y < this.maxZoomIn)
			{
				this.cameraPosition = localPosition2;
			}
			if (this.cameraPosition.y > this.maxZoomOut)
			{
				this.cameraPosition = localPosition2;
			}
		}
		if (Vector3.Distance(base.transform.localPosition, this.cameraPosition) < 0.01f)
		{
			return;
		}
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.cameraPosition, 0.1f);
	}

	// Token: 0x040023F4 RID: 9204
	public bool startZoomOut = true;

	// Token: 0x040023F5 RID: 9205
	public float zoomSpeed = 2f;

	// Token: 0x040023F6 RID: 9206
	public float maxZoomIn = 3f;

	// Token: 0x040023F7 RID: 9207
	public float maxZoomOut = 20f;

	// Token: 0x040023F8 RID: 9208
	public PostProcessVolume postVolume;

	// Token: 0x040023F9 RID: 9209
	public ColorParameter[] colorParameter;

	// Token: 0x040023FA RID: 9210
	public GameObject[] additionalCamera;

	// Token: 0x040023FB RID: 9211
	private Vector3 cameraPosition;

	// Token: 0x040023FC RID: 9212
	private cameraMovementScript cmS_;

	// Token: 0x040023FD RID: 9213
	public PostLiner postLiner;
}
