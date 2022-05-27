using System;
using ReachableGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;


public class mainCameraScript : MonoBehaviour
{
	
	private void Start()
	{
		this.cmS_ = base.transform.root.gameObject.GetComponent<cameraMovementScript>();
		this.cameraPosition = base.transform.localPosition;
		this.InitPostProcess();
	}

	
	private void InitPostProcess()
	{
		this.postVolume.profile.TryGetSettings<PostLiner>(out this.postLiner);
	}

	
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

	
	private void Update()
	{
		this.CameraInput();
		this.LookAtCameraMovement();
	}

	
	private void LookAtCameraMovement()
	{
		base.transform.LookAt(base.transform.parent.transform);
	}

	
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

	
	public bool startZoomOut = true;

	
	public float zoomSpeed = 2f;

	
	public float maxZoomIn = 3f;

	
	public float maxZoomOut = 20f;

	
	public PostProcessVolume postVolume;

	
	public ColorParameter[] colorParameter;

	
	public GameObject[] additionalCamera;

	
	private Vector3 cameraPosition;

	
	private cameraMovementScript cmS_;

	
	public PostLiner postLiner;
}
