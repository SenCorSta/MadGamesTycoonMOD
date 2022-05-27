using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	
	[AddComponentMenu("Vectrosity/LineManager")]
	public class LineManager : MonoBehaviour
	{
		
		private void Awake()
		{
			this.Initialize();
		}

		
		private void Initialize()
		{
			LineManager.lines = new List<VectorLine>();
			LineManager.transforms = new List<Transform>();
			LineManager.lineCount = 0;
			base.enabled = false;
		}

		
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

		
		public void DisableLine(VectorLine vectorLine, float time)
		{
			base.StartCoroutine(this.DisableLine(vectorLine, time, false));
		}

		
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

		
		private void RemoveLine(int i)
		{
			LineManager.lines.RemoveAt(i);
			LineManager.transforms.RemoveAt(i);
			LineManager.lineCount--;
			this.DisableIfUnused();
		}

		
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

		
		public void DisableIfUnused()
		{
			if (!this.destroyed && LineManager.lineCount == 0 && VectorManager.arrayCount == 0 && VectorManager.arrayCount2 == 0)
			{
				base.enabled = false;
			}
		}

		
		public void EnableIfUsed()
		{
			if (VectorManager.arrayCount == 1 || VectorManager.arrayCount2 == 1)
			{
				base.enabled = true;
			}
		}

		
		public void StartCheckDistance()
		{
			base.InvokeRepeating("CheckDistance", 0.01f, VectorManager.distanceCheckFrequency);
		}

		
		private void CheckDistance()
		{
			VectorManager.CheckDistance();
		}

		
		private void OnDestroy()
		{
			this.destroyed = true;
		}

		
		private static List<VectorLine> lines;

		
		private static List<Transform> transforms;

		
		private static int lineCount;

		
		private bool destroyed;
	}
}
