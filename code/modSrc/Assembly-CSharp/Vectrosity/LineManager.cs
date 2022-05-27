using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200037E RID: 894
	[AddComponentMenu("Vectrosity/LineManager")]
	public class LineManager : MonoBehaviour
	{
		// Token: 0x06002029 RID: 8233 RVA: 0x00015496 File Offset: 0x00013696
		private void Awake()
		{
			this.Initialize();
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0001549E File Offset: 0x0001369E
		private void Initialize()
		{
			LineManager.lines = new List<VectorLine>();
			LineManager.transforms = new List<Transform>();
			LineManager.lineCount = 0;
			base.enabled = false;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x001507C4 File Offset: 0x0014E9C4
		public void AddLine(VectorLine vectorLine, Transform thisTransform, float time)
		{
			if (time > 0f)
			{
				base.StartCoroutine(this.DisableLine(vectorLine, time, false));
			}
			for (int i = 0; i < LineManager.lineCount; i++)
			{
				if (vectorLine == LineManager.lines[i])
				{
					return;
				}
			}
			LineManager.lines.Add(vectorLine);
			LineManager.transforms.Add(thisTransform);
			if (++LineManager.lineCount == 1)
			{
				base.enabled = true;
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000154C1 File Offset: 0x000136C1
		public void DisableLine(VectorLine vectorLine, float time)
		{
			base.StartCoroutine(this.DisableLine(vectorLine, time, false));
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000154D3 File Offset: 0x000136D3
		private IEnumerator DisableLine(VectorLine vectorLine, float time, bool remove)
		{
			yield return new WaitForSeconds(time);
			if (remove)
			{
				this.RemoveLine(vectorLine);
			}
			else
			{
				this.RemoveLine(vectorLine);
				VectorLine.Destroy(ref vectorLine);
			}
			vectorLine = null;
			yield break;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00150838 File Offset: 0x0014EA38
		private void LateUpdate()
		{
			if (!VectorLine.camTransformExists)
			{
				return;
			}
			for (int i = 0; i < LineManager.lineCount; i++)
			{
				if (LineManager.lines[i].rectTransform != null)
				{
					LineManager.lines[i].Draw3D();
				}
				else
				{
					this.RemoveLine(i--);
				}
			}
			if (VectorLine.CameraHasMoved())
			{
				VectorManager.DrawArrayLines();
			}
			VectorLine.UpdateCameraInfo();
			VectorManager.DrawArrayLines2();
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000154F7 File Offset: 0x000136F7
		private void RemoveLine(int i)
		{
			LineManager.lines.RemoveAt(i);
			LineManager.transforms.RemoveAt(i);
			LineManager.lineCount--;
			this.DisableIfUnused();
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x001508A8 File Offset: 0x0014EAA8
		public void RemoveLine(VectorLine vectorLine)
		{
			for (int i = 0; i < LineManager.lineCount; i++)
			{
				if (vectorLine == LineManager.lines[i])
				{
					this.RemoveLine(i);
					return;
				}
			}
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00015521 File Offset: 0x00013721
		public void DisableIfUnused()
		{
			if (!this.destroyed && LineManager.lineCount == 0 && VectorManager.arrayCount == 0 && VectorManager.arrayCount2 == 0)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00015547 File Offset: 0x00013747
		public void EnableIfUsed()
		{
			if (VectorManager.arrayCount == 1 || VectorManager.arrayCount2 == 1)
			{
				base.enabled = true;
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00015560 File Offset: 0x00013760
		public void StartCheckDistance()
		{
			base.InvokeRepeating("CheckDistance", 0.01f, VectorManager.distanceCheckFrequency);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00015577 File Offset: 0x00013777
		private void CheckDistance()
		{
			VectorManager.CheckDistance();
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0001557E File Offset: 0x0001377E
		private void OnDestroy()
		{
			this.destroyed = true;
		}

		// Token: 0x040028AE RID: 10414
		private static List<VectorLine> lines;

		// Token: 0x040028AF RID: 10415
		private static List<Transform> transforms;

		// Token: 0x040028B0 RID: 10416
		private static int lineCount;

		// Token: 0x040028B1 RID: 10417
		private bool destroyed;
	}
}
