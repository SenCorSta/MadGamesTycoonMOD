using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200038A RID: 906
	public class VectorManager
	{
		// Token: 0x06002173 RID: 8563 RVA: 0x00016562 File Offset: 0x00014762
		public static void SetBrightnessParameters(float fadeOutDistance, float fullBrightDistance, int levels, float frequency, Color color)
		{
			VectorManager.minBrightnessDistance = fadeOutDistance * fadeOutDistance;
			VectorManager.maxBrightnessDistance = fullBrightDistance * fullBrightDistance;
			VectorManager.brightnessLevels = levels;
			VectorManager.distanceCheckFrequency = frequency;
			VectorManager.fogColor = color;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0015DCAC File Offset: 0x0015BEAC
		public static float GetBrightnessValue(Vector3 pos)
		{
			if (!VectorLine.camTransformExists)
			{
				VectorLine.SetCamera3D();
			}
			return Mathf.InverseLerp(VectorManager.minBrightnessDistance, VectorManager.maxBrightnessDistance, (pos - VectorLine.camTransformPosition).sqrMagnitude);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00016587 File Offset: 0x00014787
		public static void ObjectSetup(GameObject go, VectorLine line, Visibility visibility, Brightness brightness)
		{
			VectorManager.ObjectSetup(go, line, visibility, brightness, true);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0015DCE8 File Offset: 0x0015BEE8
		public static void ObjectSetup(GameObject go, VectorLine line, Visibility visibility, Brightness brightness, bool makeBounds)
		{
			VisibilityControl visibilityControl = go.GetComponent(typeof(VisibilityControl)) as VisibilityControl;
			VisibilityControlStatic visibilityControlStatic = go.GetComponent(typeof(VisibilityControlStatic)) as VisibilityControlStatic;
			VisibilityControlAlways visibilityControlAlways = go.GetComponent(typeof(VisibilityControlAlways)) as VisibilityControlAlways;
			BrightnessControl brightnessControl = go.GetComponent(typeof(BrightnessControl)) as BrightnessControl;
			if (go.GetComponent(typeof(MeshFilter)) as MeshFilter == null)
			{
				go.AddComponent<MeshFilter>();
			}
			if (go.GetComponent(typeof(MeshRenderer)) as MeshRenderer == null)
			{
				go.AddComponent<MeshRenderer>();
			}
			if (visibility == Visibility.Dynamic)
			{
				if (visibilityControlStatic)
				{
					visibilityControlStatic.DontDestroyLine();
					UnityEngine.Object.Destroy(visibilityControlStatic);
					VectorManager.ResetLinePoints(visibilityControlStatic, line);
				}
				if (visibilityControlAlways)
				{
					visibilityControlAlways.DontDestroyLine();
					UnityEngine.Object.Destroy(visibilityControlAlways);
				}
				if (visibilityControl == null)
				{
					visibilityControl = (go.AddComponent(typeof(VisibilityControl)) as VisibilityControl);
					visibilityControl.Setup(line, makeBounds);
					if (brightnessControl != null)
					{
						brightnessControl.SetUseLine(false);
					}
				}
			}
			else if (visibility == Visibility.Static)
			{
				if (visibilityControl)
				{
					visibilityControl.DontDestroyLine();
					UnityEngine.Object.Destroy(visibilityControl);
				}
				if (visibilityControlAlways)
				{
					visibilityControlAlways.DontDestroyLine();
					UnityEngine.Object.Destroy(visibilityControlAlways);
				}
				if (visibilityControlStatic == null)
				{
					visibilityControlStatic = (go.AddComponent(typeof(VisibilityControlStatic)) as VisibilityControlStatic);
					visibilityControlStatic.Setup(line, makeBounds);
					if (brightnessControl != null)
					{
						brightnessControl.SetUseLine(false);
					}
				}
			}
			else if (visibility == Visibility.Always)
			{
				if (visibilityControl)
				{
					visibilityControl.DontDestroyLine();
					UnityEngine.Object.Destroy(visibilityControl);
				}
				if (visibilityControlStatic)
				{
					visibilityControlStatic.DontDestroyLine();
					UnityEngine.Object.Destroy(visibilityControlStatic);
					VectorManager.ResetLinePoints(visibilityControlStatic, line);
				}
				if (visibilityControlAlways == null)
				{
					visibilityControlAlways = (go.AddComponent(typeof(VisibilityControlAlways)) as VisibilityControlAlways);
					visibilityControlAlways.Setup(line);
					if (brightnessControl != null)
					{
						brightnessControl.SetUseLine(false);
					}
				}
			}
			if (brightness == Brightness.Fog)
			{
				if (brightnessControl == null)
				{
					brightnessControl = (go.AddComponent(typeof(BrightnessControl)) as BrightnessControl);
					if (visibilityControl == null && visibilityControlStatic == null && visibilityControlAlways == null)
					{
						brightnessControl.Setup(line, true);
						return;
					}
					brightnessControl.Setup(line, false);
					return;
				}
			}
			else if (brightnessControl)
			{
				UnityEngine.Object.Destroy(brightnessControl);
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0015DF3C File Offset: 0x0015C13C
		private static void ResetLinePoints(VisibilityControlStatic vcs, VectorLine line)
		{
			Matrix4x4 inverse = vcs.GetMatrix().inverse;
			for (int i = 0; i < line.points3.Count; i++)
			{
				line.points3[i] = inverse.MultiplyPoint3x4(line.points3[i]);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x00016593 File Offset: 0x00014793
		public static int arrayCount
		{
			get
			{
				return VectorManager._arrayCount;
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0015DF90 File Offset: 0x0015C190
		public static void VisibilityStaticSetup(VectorLine line, out RefInt objectNum)
		{
			if (VectorManager.vectorLines == null)
			{
				VectorManager.vectorLines = new List<VectorLine>();
				VectorManager.objectNumbers = new List<RefInt>();
			}
			line.drawTransform = null;
			VectorManager.vectorLines.Add(line);
			objectNum = new RefInt(VectorManager._arrayCount++);
			VectorManager.objectNumbers.Add(objectNum);
			VectorLine.LineManagerEnable();
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0015DFF0 File Offset: 0x0015C1F0
		public static void VisibilityStaticRemove(int objectNumber)
		{
			if (objectNumber >= VectorManager.vectorLines.Count)
			{
				Debug.LogError("VectorManager: object number exceeds array length in VisibilityStaticRemove");
				return;
			}
			for (int i = objectNumber + 1; i < VectorManager._arrayCount; i++)
			{
				VectorManager.objectNumbers[i].i--;
			}
			VectorManager.vectorLines.RemoveAt(objectNumber);
			VectorManager.objectNumbers.RemoveAt(objectNumber);
			VectorManager._arrayCount--;
			VectorLine.LineManagerDisable();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0001659A File Offset: 0x0001479A
		public static int arrayCount2
		{
			get
			{
				return VectorManager._arrayCount2;
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0015E068 File Offset: 0x0015C268
		public static void VisibilitySetup(Transform thisTransform, VectorLine line, out RefInt objectNum)
		{
			if (VectorManager.vectorLines2 == null)
			{
				VectorManager.vectorLines2 = new List<VectorLine>();
				VectorManager.objectNumbers2 = new List<RefInt>();
			}
			line.drawTransform = thisTransform;
			VectorManager.vectorLines2.Add(line);
			objectNum = new RefInt(VectorManager._arrayCount2++);
			VectorManager.objectNumbers2.Add(objectNum);
			VectorLine.LineManagerEnable();
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0015E0C8 File Offset: 0x0015C2C8
		public static void VisibilityRemove(int objectNumber)
		{
			if (objectNumber >= VectorManager.vectorLines2.Count)
			{
				Debug.LogError("VectorManager: object number exceeds array length in VisibilityRemove");
				return;
			}
			for (int i = objectNumber + 1; i < VectorManager._arrayCount2; i++)
			{
				VectorManager.objectNumbers2[i].i--;
			}
			VectorManager.vectorLines2.RemoveAt(objectNumber);
			VectorManager.objectNumbers2.RemoveAt(objectNumber);
			VectorManager._arrayCount2--;
			VectorLine.LineManagerDisable();
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0015E140 File Offset: 0x0015C340
		public static void CheckDistanceSetup(Transform thisTransform, VectorLine line, Color color, RefInt objectNum)
		{
			VectorLine.LineManagerEnable();
			if (VectorManager.vectorLines3 == null)
			{
				VectorManager.vectorLines3 = new List<VectorLine>();
				VectorManager.transforms3 = new List<Transform>();
				VectorManager.oldDistances = new List<int>();
				VectorManager.colors = new List<Color>();
				VectorManager.objectNumbers3 = new List<RefInt>();
				VectorLine.LineManagerCheckDistance();
			}
			VectorManager.transforms3.Add(thisTransform);
			VectorManager.vectorLines3.Add(line);
			VectorManager.oldDistances.Add(-1);
			VectorManager.colors.Add(color);
			objectNum.i = VectorManager._arrayCount3++;
			VectorManager.objectNumbers3.Add(objectNum);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0015E1DC File Offset: 0x0015C3DC
		public static void DistanceRemove(int objectNumber)
		{
			if (objectNumber >= VectorManager.vectorLines3.Count)
			{
				Debug.LogError("VectorManager: object number exceeds array length in DistanceRemove");
				return;
			}
			for (int i = objectNumber + 1; i < VectorManager._arrayCount3; i++)
			{
				VectorManager.objectNumbers3[i].i--;
			}
			VectorManager.transforms3.RemoveAt(objectNumber);
			VectorManager.vectorLines3.RemoveAt(objectNumber);
			VectorManager.oldDistances.RemoveAt(objectNumber);
			VectorManager.colors.RemoveAt(objectNumber);
			VectorManager.objectNumbers3.RemoveAt(objectNumber);
			VectorManager._arrayCount3--;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0015E270 File Offset: 0x0015C470
		public static void CheckDistance()
		{
			for (int i = 0; i < VectorManager._arrayCount3; i++)
			{
				VectorManager.SetDistanceColor(i);
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000165A1 File Offset: 0x000147A1
		public static void SetOldDistance(int objectNumber, int val)
		{
			VectorManager.oldDistances[objectNumber] = val;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0015E294 File Offset: 0x0015C494
		public static void SetDistanceColor(int i)
		{
			if (!VectorManager.vectorLines3[i].active)
			{
				return;
			}
			float brightnessValue = VectorManager.GetBrightnessValue(VectorManager.transforms3[i].position);
			int num = (int)(brightnessValue * (float)VectorManager.brightnessLevels);
			if (num != VectorManager.oldDistances[i])
			{
				VectorManager.vectorLines3[i].SetColor(Color.Lerp(VectorManager.fogColor, VectorManager.colors[i], brightnessValue));
			}
			VectorManager.oldDistances[i] = num;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000165AF File Offset: 0x000147AF
		public static void DrawArrayLine(int i)
		{
			if (VectorManager.useDraw3D)
			{
				VectorManager.vectorLines[i].Draw3D();
				return;
			}
			VectorManager.vectorLines[i].Draw();
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000165D9 File Offset: 0x000147D9
		public static void DrawArrayLine2(int i)
		{
			if (VectorManager.useDraw3D)
			{
				VectorManager.vectorLines2[i].Draw3D();
				return;
			}
			VectorManager.vectorLines2[i].Draw();
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0015E31C File Offset: 0x0015C51C
		public static void DrawArrayLines()
		{
			if (VectorManager.useDraw3D)
			{
				for (int i = 0; i < VectorManager._arrayCount; i++)
				{
					VectorManager.vectorLines[i].Draw3D();
				}
				return;
			}
			for (int j = 0; j < VectorManager._arrayCount; j++)
			{
				VectorManager.vectorLines[j].Draw();
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0015E374 File Offset: 0x0015C574
		public static void DrawArrayLines2()
		{
			if (VectorManager.useDraw3D)
			{
				for (int i = 0; i < VectorManager._arrayCount2; i++)
				{
					VectorManager.vectorLines2[i].Draw3D();
				}
				return;
			}
			for (int j = 0; j < VectorManager._arrayCount2; j++)
			{
				VectorManager.vectorLines2[j].Draw();
			}
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0015E3CC File Offset: 0x0015C5CC
		public static Bounds GetBounds(VectorLine line)
		{
			if (line.points3 == null)
			{
				Debug.LogError("VectorManager: GetBounds can only be used with a Vector3 array");
				return default(Bounds);
			}
			return VectorManager.GetBounds(line.points3);
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0015E400 File Offset: 0x0015C600
		public static Bounds GetBounds(List<Vector3> points3)
		{
			Bounds result = default(Bounds);
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			int count = points3.Count;
			for (int i = 0; i < count; i++)
			{
				if (points3[i].x < vector.x)
				{
					vector.x = points3[i].x;
				}
				else if (points3[i].x > vector2.x)
				{
					vector2.x = points3[i].x;
				}
				if (points3[i].y < vector.y)
				{
					vector.y = points3[i].y;
				}
				else if (points3[i].y > vector2.y)
				{
					vector2.y = points3[i].y;
				}
				if (points3[i].z < vector.z)
				{
					vector.z = points3[i].z;
				}
				else if (points3[i].z > vector2.z)
				{
					vector2.z = points3[i].z;
				}
			}
			result.min = vector;
			result.max = vector2;
			return result;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0015E56C File Offset: 0x0015C76C
		private static Mesh MakeBoundsMesh(Bounds bounds)
		{
			return new Mesh
			{
				vertices = new Vector3[]
				{
					bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z),
					bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z),
					bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z),
					bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z),
					bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z),
					bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z),
					bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z),
					bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z)
				}
			};
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0015E778 File Offset: 0x0015C978
		public static void SetupBoundsMesh(GameObject go, VectorLine line)
		{
			MeshFilter meshFilter = go.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = go.AddComponent<MeshFilter>();
			}
			MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = go.AddComponent<MeshRenderer>();
			}
			meshRenderer.enabled = true;
			if (VectorManager.meshTable == null)
			{
				VectorManager.meshTable = new Dictionary<string, Mesh>();
			}
			if (!VectorManager.meshTable.ContainsKey(line.name))
			{
				VectorManager.meshTable.Add(line.name, VectorManager.MakeBoundsMesh(VectorManager.GetBounds(line)));
				VectorManager.meshTable[line.name].name = line.name + " Bounds";
			}
			meshFilter.mesh = VectorManager.meshTable[line.name];
		}

		// Token: 0x04002926 RID: 10534
		public static float minBrightnessDistance = 500f;

		// Token: 0x04002927 RID: 10535
		public static float maxBrightnessDistance = 250f;

		// Token: 0x04002928 RID: 10536
		private static int brightnessLevels = 32;

		// Token: 0x04002929 RID: 10537
		public static float distanceCheckFrequency = 0.2f;

		// Token: 0x0400292A RID: 10538
		private static Color fogColor;

		// Token: 0x0400292B RID: 10539
		public static bool useDraw3D = false;

		// Token: 0x0400292C RID: 10540
		private static List<VectorLine> vectorLines;

		// Token: 0x0400292D RID: 10541
		private static List<RefInt> objectNumbers;

		// Token: 0x0400292E RID: 10542
		public static int _arrayCount = 0;

		// Token: 0x0400292F RID: 10543
		private static List<VectorLine> vectorLines2;

		// Token: 0x04002930 RID: 10544
		private static List<RefInt> objectNumbers2;

		// Token: 0x04002931 RID: 10545
		private static int _arrayCount2 = 0;

		// Token: 0x04002932 RID: 10546
		private static List<Transform> transforms3;

		// Token: 0x04002933 RID: 10547
		private static List<VectorLine> vectorLines3;

		// Token: 0x04002934 RID: 10548
		private static List<int> oldDistances;

		// Token: 0x04002935 RID: 10549
		private static List<Color> colors;

		// Token: 0x04002936 RID: 10550
		private static List<RefInt> objectNumbers3;

		// Token: 0x04002937 RID: 10551
		private static int _arrayCount3 = 0;

		// Token: 0x04002938 RID: 10552
		private static Dictionary<string, Mesh> meshTable;
	}
}
