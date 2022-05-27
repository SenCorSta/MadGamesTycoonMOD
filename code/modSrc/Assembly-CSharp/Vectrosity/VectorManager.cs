using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	// Token: 0x0200038D RID: 909
	public class VectorManager
	{
		// Token: 0x060021C6 RID: 8646 RVA: 0x0015E948 File Offset: 0x0015CB48
		public static void SetBrightnessParameters(float fadeOutDistance, float fullBrightDistance, int levels, float frequency, Color color)
		{
			VectorManager.minBrightnessDistance = fadeOutDistance * fadeOutDistance;
			VectorManager.maxBrightnessDistance = fullBrightDistance * fullBrightDistance;
			VectorManager.brightnessLevels = levels;
			VectorManager.distanceCheckFrequency = frequency;
			VectorManager.fogColor = color;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0015E970 File Offset: 0x0015CB70
		public static float GetBrightnessValue(Vector3 pos)
		{
			if (!VectorLine.camTransformExists)
			{
				VectorLine.SetCamera3D();
			}
			return Mathf.InverseLerp(VectorManager.minBrightnessDistance, VectorManager.maxBrightnessDistance, (pos - VectorLine.camTransformPosition).sqrMagnitude);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0015E9AB File Offset: 0x0015CBAB
		public static void ObjectSetup(GameObject go, VectorLine line, Visibility visibility, Brightness brightness)
		{
			VectorManager.ObjectSetup(go, line, visibility, brightness, true);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0015E9B8 File Offset: 0x0015CBB8
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

		// Token: 0x060021CA RID: 8650 RVA: 0x0015EC0C File Offset: 0x0015CE0C
		private static void ResetLinePoints(VisibilityControlStatic vcs, VectorLine line)
		{
			Matrix4x4 inverse = vcs.GetMatrix().inverse;
			for (int i = 0; i < line.points3.Count; i++)
			{
				line.points3[i] = inverse.MultiplyPoint3x4(line.points3[i]);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0015EC5D File Offset: 0x0015CE5D
		public static int arrayCount
		{
			get
			{
				return VectorManager._arrayCount;
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0015EC64 File Offset: 0x0015CE64
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

		// Token: 0x060021CD RID: 8653 RVA: 0x0015ECC4 File Offset: 0x0015CEC4
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0015ED3A File Offset: 0x0015CF3A
		public static int arrayCount2
		{
			get
			{
				return VectorManager._arrayCount2;
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0015ED44 File Offset: 0x0015CF44
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

		// Token: 0x060021D0 RID: 8656 RVA: 0x0015EDA4 File Offset: 0x0015CFA4
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

		// Token: 0x060021D1 RID: 8657 RVA: 0x0015EE1C File Offset: 0x0015D01C
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

		// Token: 0x060021D2 RID: 8658 RVA: 0x0015EEB8 File Offset: 0x0015D0B8
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

		// Token: 0x060021D3 RID: 8659 RVA: 0x0015EF4C File Offset: 0x0015D14C
		public static void CheckDistance()
		{
			for (int i = 0; i < VectorManager._arrayCount3; i++)
			{
				VectorManager.SetDistanceColor(i);
			}
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0015EF6F File Offset: 0x0015D16F
		public static void SetOldDistance(int objectNumber, int val)
		{
			VectorManager.oldDistances[objectNumber] = val;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0015EF80 File Offset: 0x0015D180
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

		// Token: 0x060021D6 RID: 8662 RVA: 0x0015F005 File Offset: 0x0015D205
		public static void DrawArrayLine(int i)
		{
			if (VectorManager.useDraw3D)
			{
				VectorManager.vectorLines[i].Draw3D();
				return;
			}
			VectorManager.vectorLines[i].Draw();
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0015F02F File Offset: 0x0015D22F
		public static void DrawArrayLine2(int i)
		{
			if (VectorManager.useDraw3D)
			{
				VectorManager.vectorLines2[i].Draw3D();
				return;
			}
			VectorManager.vectorLines2[i].Draw();
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0015F05C File Offset: 0x0015D25C
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

		// Token: 0x060021D9 RID: 8665 RVA: 0x0015F0B4 File Offset: 0x0015D2B4
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

		// Token: 0x060021DA RID: 8666 RVA: 0x0015F10C File Offset: 0x0015D30C
		public static Bounds GetBounds(VectorLine line)
		{
			if (line.points3 == null)
			{
				Debug.LogError("VectorManager: GetBounds can only be used with a Vector3 array");
				return default(Bounds);
			}
			return VectorManager.GetBounds(line.points3);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0015F140 File Offset: 0x0015D340
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

		// Token: 0x060021DC RID: 8668 RVA: 0x0015F2AC File Offset: 0x0015D4AC
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

		// Token: 0x060021DD RID: 8669 RVA: 0x0015F4B8 File Offset: 0x0015D6B8
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

		// Token: 0x0400293C RID: 10556
		public static float minBrightnessDistance = 500f;

		// Token: 0x0400293D RID: 10557
		public static float maxBrightnessDistance = 250f;

		// Token: 0x0400293E RID: 10558
		private static int brightnessLevels = 32;

		// Token: 0x0400293F RID: 10559
		public static float distanceCheckFrequency = 0.2f;

		// Token: 0x04002940 RID: 10560
		private static Color fogColor;

		// Token: 0x04002941 RID: 10561
		public static bool useDraw3D = false;

		// Token: 0x04002942 RID: 10562
		private static List<VectorLine> vectorLines;

		// Token: 0x04002943 RID: 10563
		private static List<RefInt> objectNumbers;

		// Token: 0x04002944 RID: 10564
		public static int _arrayCount = 0;

		// Token: 0x04002945 RID: 10565
		private static List<VectorLine> vectorLines2;

		// Token: 0x04002946 RID: 10566
		private static List<RefInt> objectNumbers2;

		// Token: 0x04002947 RID: 10567
		private static int _arrayCount2 = 0;

		// Token: 0x04002948 RID: 10568
		private static List<Transform> transforms3;

		// Token: 0x04002949 RID: 10569
		private static List<VectorLine> vectorLines3;

		// Token: 0x0400294A RID: 10570
		private static List<int> oldDistances;

		// Token: 0x0400294B RID: 10571
		private static List<Color> colors;

		// Token: 0x0400294C RID: 10572
		private static List<RefInt> objectNumbers3;

		// Token: 0x0400294D RID: 10573
		private static int _arrayCount3 = 0;

		// Token: 0x0400294E RID: 10574
		private static Dictionary<string, Mesh> meshTable;
	}
}
