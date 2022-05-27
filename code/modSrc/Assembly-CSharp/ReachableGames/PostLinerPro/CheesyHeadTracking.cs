using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F6 RID: 1014
	public class CheesyHeadTracking : MonoBehaviour
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x00173DA0 File Offset: 0x00171FA0
		private void Start()
		{
			this.startRot = base.transform.rotation;
			this.trackRate *= UnityEngine.Random.value + 0.25f;
			this.nextTrackStart = Time.time + UnityEngine.Random.value * this.trackDelay;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00173DF0 File Offset: 0x00171FF0
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

		// Token: 0x04002E2B RID: 11819
		public Quaternion startRot;

		// Token: 0x04002E2C RID: 11820
		private float trackRate = 0.1f;

		// Token: 0x04002E2D RID: 11821
		private float trackDelay = 2f;

		// Token: 0x04002E2E RID: 11822
		private float nextTrackStart;
	}
}
