using System;
using UnityEngine;


public class utility_AdjustCulling : MonoBehaviour
{
	
	private void Start()
	{
		this.cam = base.gameObject.GetComponent<Camera>();
	}

	
	private void LateUpdate()
	{
		if (this.cam != null)
		{
			if (base.transform.position.y > this.altitudeLowerThreshold)
			{
				this.useThreshold = Mathf.Clamp01((base.transform.position.y - this.altitudeLowerThreshold) / (this.altitudeUpperThreshold - this.altitudeLowerThreshold));
			}
			else
			{
				this.useThreshold = 0f;
			}
			this.cam.nearClipPlane = Mathf.Lerp(this.nearCullAtBase, this.nearCullAtAltitude, this.useThreshold);
		}
	}

	
	public float nearCullAtBase = 0.3f;

	
	public float nearCullAtAltitude = 5f;

	
	public float altitudeLowerThreshold = 50f;

	
	public float altitudeUpperThreshold = 250f;

	
	private Camera cam;

	
	private float useThreshold;
}
