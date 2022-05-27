using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vectrosity
{
	
	public class VectorManager
	{
		
		public static void SetBrightnessParameters(float fadeOutDistance, float fullBrightDistance, int levels, float frequency, Color color)
		{
			VectorManager.minBrightnessDistance = fadeOutDistance * fadeOutDistance;
			VectorManager.maxBrightnessDistance = fullBrightDistance * fullBrightDistance;
			VectorManager.brightnessLevels = levels;
			VectorManager.distanceCheckFrequency = frequency;
			VectorManager.fogColor = color;
		}

		
		public static float GetBrightnessValue(Vector3 pos)
		{
			if (!VectorLine.camTransformExists)
			{
				VectorLine.SetCamera3D();
			}
			return Mathf.InverseLerp(VectorManager.minBrightnessDistance, VectorManager.maxBrightnessDistance, (pos - VectorLine.camTransformPosition).sqrMagnitude);
		}

		
		public static void ObjectSetup(GameObject go, VectorLine line, Visibility visibility, Brightness brightness)
		{
			VectorManager.ObjectSetup(go, line, visibility, brightness, true);
		}

		
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

		
		private static void ResetLinePoints(VisibilityControlStatic vcs, VectorLine line)
		{
			Matrix4x4 inverse = vcs.GetMatrix().inverse;
			for (int i = 0; i < line.points3.Count; i++)
			{
				line.points3[i] = inverse.MultiplyPoint3x4(line.points3[i]);
			}
		}

		
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0015EC5D File Offset: 0x0015CE5D
		public static int arrayCount
		{
			get
			{
				return VectorManager._arrayCount;
			}
		}

		
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

		
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0015ED3A File Offset: 0x0015CF3A
		public static int arrayCount2
		{
			get
			{
				return VectorManager._arrayCount2;
			}
		}

		
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

		
		public static void CheckDistance()
		{
			for (int i = 0; i < VectorManager._arrayCount3; i++)
			{
				VectorManager.SetDistanceColor(i);
			}
		}

		
		public static void SetOldDistance(int objectNumber, int val)
		{
			VectorManager.oldDistances[objectNumber] = val;
		}

		
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

		
		public static void DrawArrayLine(int i)
		{
			if (VectorManager.useDraw3D)
			{
				VectorManager.vectorLines[i].Draw3D();
				return;
			}
			VectorManager.vectorLines[i].Draw();
		}

		
		public static void DrawArrayLine2(int i)
		{
			if (VectorManager.useDraw3D)
			{
				VectorManager.vectorLines2[i].Draw3D();
				return;
			}
			VectorManager.vectorLines2[i].Draw();
		}

		
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

		
		public static Bounds GetBounds(VectorLine line)
		{
			if (line.points3 == null)
			{
				Debug.LogError("VectorManager: GetBounds can only be used with a Vector3 array");
				return default(Bounds);
			}
			return VectorManager.GetBounds(line.points3);
		}

		
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

		
		public static float minBrightnessDistance = 500f;

		
		public static float maxBrightnessDistance = 250f;

		
		private static int brightnessLevels = 32;

		
		public static float distanceCheckFrequency = 0.2f;

		
		private static Color fogColor;

		
		public static bool useDraw3D = false;

		
		private static List<VectorLine> vectorLines;

		
		private static List<RefInt> objectNumbers;

		
		public static int _arrayCount = 0;

		
		private static List<VectorLine> vectorLines2;

		
		private static List<RefInt> objectNumbers2;

		
		private static int _arrayCount2 = 0;

		
		private static List<Transform> transforms3;

		
		private static List<VectorLine> vectorLines3;

		
		private static List<int> oldDistances;

		
		private static List<Color> colors;

		
		private static List<RefInt> objectNumbers3;

		
		private static int _arrayCount3 = 0;

		
		private static Dictionary<string, Mesh> meshTable;
	}
}
