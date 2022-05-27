using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x02000381 RID: 897
	[AddComponentMenu("Vectrosity/LineManager")]
	public class LineManager : MonoBehaviour
	{
		// Token: 0x0600207C RID: 8316 RVA: 0x00150390 File Offset: 0x0014E590
		private void Awake()
		{
			this.Initialize();
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00150398 File Offset: 0x0014E598
		private void Initialize()
		{
			LineManager.lines = new List<VectorLine>();
			LineManager.transforms = new List<Transform>();
			LineManager.lineCount = 0;
			base.enabled = false;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x001503BC File Offset: 0x0014E5BC
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

		// Token: 0x0600207F RID: 8319 RVA: 0x0015042D File Offset: 0x0014E62D
		public void DisableLine(VectorLine vectorLine, float time)
		{
			base.StartCoroutine(this.DisableLine(vectorLine, time, false));
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0015043F File Offset: 0x0014E63F
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

		// Token: 0x06002081 RID: 8321 RVA: 0x00150464 File Offset: 0x0014E664
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

		// Token: 0x06002082 RID: 8322 RVA: 0x001504D4 File Offset: 0x0014E6D4
		private void RemoveLine(int i)
		{
			LineManager.lines.RemoveAt(i);
			LineManager.transforms.RemoveAt(i);
			LineManager.lineCount--;
			this.DisableIfUnused();
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00150500 File Offset: 0x0014E700
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

		// Token: 0x06002084 RID: 8324 RVA: 0x00150533 File Offset: 0x0014E733
		public void DisableIfUnused()
		{
			if (!this.destroyed && LineManager.lineCount == 0 && VectorManager.arrayCount == 0 && VectorManager.arrayCount2 == 0)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00150559 File Offset: 0x0014E759
		public void EnableIfUsed()
		{
			if (VectorManager.arrayCount == 1 || VectorManager.arrayCount2 == 1)
			{
				base.enabled = true;
			}
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00150572 File Offset: 0x0014E772
		public void StartCheckDistance()
		{
			base.InvokeRepeating("CheckDistance", 0.01f, VectorManager.distanceCheckFrequency);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00150589 File Offset: 0x0014E789
		private void CheckDistance()
		{
			VectorManager.CheckDistance();
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00150590 File Offset: 0x0014E790
		private void OnDestroy()
		{
			this.destroyed = true;
		}

		// Token: 0x040028C4 RID: 10436
		private static List<VectorLine> lines;

		// Token: 0x040028C5 RID: 10437
		private static List<Transform> transforms;

		// Token: 0x040028C6 RID: 10438
		private static int lineCount;

		// Token: 0x040028C7 RID: 10439
		private bool destroyed;
	}
}
