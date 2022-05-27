using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	// Token: 0x02000407 RID: 1031
	[ExecuteInEditMode]
	public class SetGlobalTime : MonoBehaviour
	{
		// Token: 0x0600245C RID: 9308 RVA: 0x00175591 File Offset: 0x00173791
		private void Start()
		{
			this.globalTime = Shader.PropertyToID("globalUnscaledTime");
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x001755A3 File Offset: 0x001737A3
		private void Update()
		{
			Shader.SetGlobalFloat(this.globalTime, Time.time / 20f);
		}

		// Token: 0x04002E70 RID: 11888
		private int globalTime;
	}
}
