using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000404 RID: 1028
	[ExecuteInEditMode]
	public class SetGlobalTime : MonoBehaviour
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x0001884D File Offset: 0x00016A4D
		private void Start()
		{
			this.globalTime = Shader.PropertyToID("globalUnscaledTime");
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0001885F File Offset: 0x00016A5F
		private void Update()
		{
			Shader.SetGlobalFloat(this.globalTime, Time.time / 20f);
		}

		// Token: 0x04002E5A RID: 11866
		private int globalTime;
	}
}
