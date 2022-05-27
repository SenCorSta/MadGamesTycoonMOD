using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F3 RID: 1011
	public class CheesyHeadTracking : MonoBehaviour
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x00170F20 File Offset: 0x0016F120
		private void Start()
		{
			this.startRot = base.transform.rotation;
			this.trackRate *= UnityEngine.Random.value + 0.25f;
			this.nextTrackStart = Time.time + UnityEngine.Random.value * this.trackDelay;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00170F70 File Offset: 0x0016F170
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

		// Token: 0x04002E15 RID: 11797
		public Quaternion startRot;

		// Token: 0x04002E16 RID: 11798
		private float trackRate = 0.1f;

		// Token: 0x04002E17 RID: 11799
		private float trackDelay = 2f;

		// Token: 0x04002E18 RID: 11800
		private float nextTrackStart;
	}
}
