using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	
	public class CheesyHeadTracking : MonoBehaviour
	{
		
		private void Start()
		{
			this.startRot = base.transform.rotation;
			this.trackRate *= UnityEngine.Random.value + 0.25f;
			this.nextTrackStart = Time.time + UnityEngine.Random.value * this.trackDelay;
		}

		
		private void Update()
		{
			if (Time.time > this.nextTrackStart)
			{
				Quaternion quaternion = Quaternion.LookRotation(base.transform.position - Camera.main.transform.position, Vector3.up) * this.startRot;
				if (Quaternion.Angle(quaternion, base.transform.rotation) > 0.01f)
				{
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, quaternion, this.trackRate);
					return;
				}
				this.nextTrackStart = Time.time + UnityEngine.Random.value * this.trackDelay;
			}
		}

		
		public Quaternion startRot;

		
		private float trackRate = 0.1f;

		
		private float trackDelay = 2f;

		
		private float nextTrackStart;
	}
}
