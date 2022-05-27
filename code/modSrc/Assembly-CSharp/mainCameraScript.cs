using System;
using ReachableGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000328 RID: 808
public class mainCameraScript : MonoBehaviour
{
	// Token: 0x06001C86 RID: 7302 RVA: 0x00013903 File Offset: 0x00011B03
	private void Start()
	{
		this.cmS_ = base.transform.root.gameObject.GetComponent<cameraMovementScript>();
		this.cameraPosition = base.transform.localPosition;
		this.InitPostProcess();
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x00013937 File Offset: 0x00011B37
	private void InitPostProcess()
	{
		this.postVolume.profile.TryGetSettings<PostLiner>(out this.postLiner);
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x00120814 File Offset: 0x0011EA14
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

	// Token: 0x06001C89 RID: 7305 RVA: 0x00013950 File Offset: 0x00011B50
	private void Update()
	{
		this.CameraInput();
		this.LookAtCameraMovement();
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x0001395E File Offset: 0x00011B5E
	private void LookAtCameraMovement()
	{
		base.transform.LookAt(base.transform.parent.transform);
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x00120884 File Offset: 0x0011EA84
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

	// Token: 0x040023DA RID: 9178
	public bool startZoomOut = true;

	// Token: 0x040023DB RID: 9179
	public float zoomSpeed = 2f;

	// Token: 0x040023DC RID: 9180
	public float maxZoomIn = 3f;

	// Token: 0x040023DD RID: 9181
	public float maxZoomOut = 20f;

	// Token: 0x040023DE RID: 9182
	public PostProcessVolume postVolume;

	// Token: 0x040023DF RID: 9183
	public ColorParameter[] colorParameter;

	// Token: 0x040023E0 RID: 9184
	public GameObject[] additionalCamera;

	// Token: 0x040023E1 RID: 9185
	private Vector3 cameraPosition;

	// Token: 0x040023E2 RID: 9186
	private cameraMovementScript cmS_;

	// Token: 0x040023E3 RID: 9187
	public PostLiner postLiner;
}
