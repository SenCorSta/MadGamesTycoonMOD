using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedColliders3D
{
	
	[AddComponentMenu("Physics/Extended Colliders 3D")]
	[Serializable]
	public class ExtendedColliders3D : MonoBehaviour
	{
		
		private void Reset()
		{
			this.autoSizeColliderToMeshFilter();
		}

		
		private void Awake()
		{
			MeshCollider meshCollider = base.gameObject.AddComponent<MeshCollider>();
			meshCollider.enabled = base.enabled;
			meshCollider.sharedMesh = this.generateMesh(false);
			meshCollider.convex = this.properties.convex;
			meshCollider.isTrigger = this.properties.isTrigger;
			meshCollider.material = this.properties.material;
			UnityEngine.Object.Destroy(this);
		}

		
		private void Start()
		{
		}

		
		private Mesh generateMesh(bool applyTransform)
		{
			Mesh mesh = new Mesh();
			mesh.name = "Extended Colliders 3D Mesh";
			Vector3[] vertices;
			int[] triangles;
			this.generateVerticesAndTriangles(applyTransform, out vertices, out triangles);
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();
			return mesh;
		}

		
		private void generateVerticesAndTriangles(bool applyTransform, out Vector3[] vertices, out int[] triangles)
		{
			int num = 0;
			if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Circle || this.properties.colliderType == ExtendedColliders3D.ColliderType.CircleHalf)
			{
				int num2 = this.properties.circleVertices + ((this.properties.colliderType == ExtendedColliders3D.ColliderType.Circle) ? 0 : 1);
				vertices = new Vector3[num2];
				triangles = new int[(num2 - 2) * (this.properties.circleTwoSided ? 6 : 3)];
				List<int> list = new List<int>();
				for (int i = 0; i < num2; i++)
				{
					float f = (float)i / (float)this.properties.circleVertices * 3.1415927f * (float)((this.properties.colliderType == ExtendedColliders3D.ColliderType.Circle) ? 2 : 1);
					vertices[i] = new Vector3(Mathf.Sin(f) / 2f, -0.5f, Mathf.Cos(f) / 2f);
					list.Add(i);
				}
				int num3 = list.Count / 2;
				bool flag = false;
				while (list.Count > 2)
				{
					triangles[num++] = list[(num3 + list.Count - 1) % list.Count];
					triangles[num++] = list[num3];
					triangles[num++] = list[(num3 + 1) % list.Count];
					list.RemoveAt(num3);
					if (flag)
					{
						num3 = (num3 + list.Count - 1) % list.Count;
					}
					flag = !flag;
				}
				if (this.properties.circleTwoSided)
				{
					for (int j = 0; j < num2 - 2; j++)
					{
						triangles[(j + num2 - 2) * 3] = triangles[j * 3];
						triangles[(j + num2 - 2) * 3 + 1] = triangles[j * 3 + 2];
						triangles[(j + num2 - 2) * 3 + 2] = triangles[j * 3 + 1];
					}
				}
			}
			else if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Cone || this.properties.colliderType == ExtendedColliders3D.ColliderType.ConeHalf)
			{
				int num4 = this.properties.coneFaces + ((this.properties.colliderType == ExtendedColliders3D.ColliderType.Cone) ? 0 : 1);
				vertices = new Vector3[num4 + 1];
				triangles = new int[num4 * 3 + (this.properties.coneCap ? ((num4 - 2) * 3) : 0) - ((this.properties.colliderType != ExtendedColliders3D.ColliderType.Cone && !this.properties.coneHalfCapFlatEnd) ? 3 : 0)];
				vertices[vertices.Length - 1] = new Vector3(0f, 0.5f, 0f);
				for (int k = 0; k < num4; k++)
				{
					float f2 = (float)k / (float)this.properties.coneFaces * 3.1415927f * (float)((this.properties.colliderType == ExtendedColliders3D.ColliderType.Cone) ? 2 : 1);
					vertices[k] = new Vector3(Mathf.Sin(f2) / 2f, -0.5f, Mathf.Cos(f2) / 2f);
					if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Cone || this.properties.coneHalfCapFlatEnd || k < num4 - 1)
					{
						triangles[num++] = k;
						triangles[num++] = (k + 1) % num4;
						triangles[num++] = vertices.Length - 1;
					}
				}
				if (this.properties.coneCap)
				{
					List<int> list2 = new List<int>();
					for (int l = 0; l < num4; l++)
					{
						list2.Add(l);
					}
					int num5 = list2.Count / 2;
					bool flag2 = false;
					while (list2.Count > 2)
					{
						triangles[num++] = list2[(num5 + list2.Count - 1) % list2.Count];
						triangles[num++] = list2[(num5 + 1) % list2.Count];
						triangles[num++] = list2[num5];
						list2.RemoveAt(num5);
						if (flag2)
						{
							num5 = (num5 + list2.Count - 1) % list2.Count;
						}
						flag2 = !flag2;
					}
				}
			}
			else if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Cube)
			{
				vertices = new Vector3[8];
				triangles = new int[(this.properties.cubeTopFace ? 6 : 0) + (this.properties.cubeBottomFace ? 6 : 0) + (this.properties.cubeLeftFace ? 6 : 0) + (this.properties.cubeRightFace ? 6 : 0) + (this.properties.cubeForwardFace ? 6 : 0) + (this.properties.cubeBackFace ? 6 : 0)];
				int num6 = 0;
				for (int m = -1; m <= 1; m += 2)
				{
					for (int n = -1; n <= 1; n += 2)
					{
						for (int num7 = -1; num7 <= 1; num7 += 2)
						{
							vertices[num6++] = new Vector3((float)n / 2f, (float)m / 2f, (float)num7 / 2f);
						}
					}
				}
				if (this.properties.cubeBottomFace)
				{
					triangles[num++] = 0;
					triangles[num++] = 2;
					triangles[num++] = 1;
					triangles[num++] = 1;
					triangles[num++] = 2;
					triangles[num++] = 3;
				}
				if (this.properties.cubeTopFace)
				{
					triangles[num++] = 4;
					triangles[num++] = 5;
					triangles[num++] = 6;
					triangles[num++] = 6;
					triangles[num++] = 5;
					triangles[num++] = 7;
				}
				if (this.properties.cubeLeftFace)
				{
					triangles[num++] = 0;
					triangles[num++] = 1;
					triangles[num++] = 4;
					triangles[num++] = 4;
					triangles[num++] = 1;
					triangles[num++] = 5;
				}
				if (this.properties.cubeRightFace)
				{
					triangles[num++] = 3;
					triangles[num++] = 2;
					triangles[num++] = 6;
					triangles[num++] = 3;
					triangles[num++] = 6;
					triangles[num++] = 7;
				}
				if (this.properties.cubeBackFace)
				{
					triangles[num++] = 0;
					triangles[num++] = 4;
					triangles[num++] = 2;
					triangles[num++] = 4;
					triangles[num++] = 6;
					triangles[num++] = 2;
				}
				if (this.properties.cubeForwardFace)
				{
					triangles[num++] = 1;
					triangles[num++] = 3;
					triangles[num++] = 5;
					triangles[num++] = 5;
					triangles[num++] = 3;
					triangles[num++] = 7;
				}
			}
			else if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Cylinder || this.properties.colliderType == ExtendedColliders3D.ColliderType.CylinderHalf)
			{
				int num8 = this.properties.cylinderFaces + ((this.properties.colliderType == ExtendedColliders3D.ColliderType.Cylinder) ? 0 : 1);
				vertices = new Vector3[num8 * 2];
				triangles = new int[num8 * 6 + (this.properties.cylinderCapTop ? ((num8 - 2) * 3) : 0) + (this.properties.cylinderCapBottom ? ((num8 - 2) * 3) : 0) - ((this.properties.colliderType != ExtendedColliders3D.ColliderType.Cylinder && !this.properties.cylinderHalfCapFlatEnd) ? 6 : 0)];
				for (int num9 = 0; num9 < num8; num9++)
				{
					float f3 = (float)num9 / (float)this.properties.cylinderFaces * 3.1415927f * (float)((this.properties.colliderType == ExtendedColliders3D.ColliderType.Cylinder) ? 2 : 1);
					vertices[num9] = new Vector3(Mathf.Sin(f3) / 2f, 0.5f, Mathf.Cos(f3) / 2f);
					vertices[num9 + num8] = vertices[num9] + new Vector3(0f, -1f, 0f);
					Vector3[] array = vertices;
					int num10 = num9;
					array[num10].x = array[num10].x * this.properties.cylinderTaperTop.x;
					Vector3[] array2 = vertices;
					int num11 = num9;
					array2[num11].z = array2[num11].z * this.properties.cylinderTaperTop.y;
					Vector3[] array3 = vertices;
					int num12 = num9 + num8;
					array3[num12].x = array3[num12].x * this.properties.cylinderTaperBottom.x;
					Vector3[] array4 = vertices;
					int num13 = num9 + num8;
					array4[num13].z = array4[num13].z * this.properties.cylinderTaperBottom.y;
					if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Cylinder || this.properties.cylinderHalfCapFlatEnd || num9 < num8 - 1)
					{
						triangles[num++] = num9;
						triangles[num++] = num9 + num8;
						triangles[num++] = (num9 + 1) % num8;
						triangles[num++] = (num9 + 1) % num8;
						triangles[num++] = num9 + num8;
						triangles[num++] = Mathf.Max((num9 + num8 + 1) % (num8 * 2), num8);
					}
				}
				for (int num14 = 0; num14 < 2; num14++)
				{
					if ((num14 == 0 && this.properties.cylinderCapTop) || (num14 == 1 && this.properties.cylinderCapBottom))
					{
						List<int> list3 = new List<int>();
						for (int num15 = 0; num15 < num8; num15++)
						{
							list3.Add(num15 + num8 * num14);
						}
						int num16 = list3.Count / 2;
						bool flag3 = false;
						while (list3.Count > 2)
						{
							triangles[num++] = list3[(num16 + list3.Count - 1) % list3.Count];
							triangles[num++] = list3[(num14 == 0) ? num16 : ((num16 + 1) % list3.Count)];
							triangles[num++] = list3[(num14 == 1) ? num16 : ((num16 + 1) % list3.Count)];
							list3.RemoveAt(num16);
							if (flag3)
							{
								num16 = (num16 + list3.Count - 1) % list3.Count;
							}
							flag3 = !flag3;
						}
					}
				}
			}
			else if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Quad)
			{
				vertices = new Vector3[]
				{
					new Vector3(-0.5f, 0f, -0.5f),
					new Vector3(-0.5f, 0f, 0.5f),
					new Vector3(0.5f, 0f, -0.5f),
					new Vector3(0.5f, 0f, 0.5f)
				};
				triangles = new int[this.properties.quadTwoSided ? 12 : 6];
				triangles[num++] = 0;
				triangles[num++] = 1;
				triangles[num++] = 2;
				triangles[num++] = 3;
				triangles[num++] = 2;
				triangles[num++] = 1;
				if (this.properties.quadTwoSided)
				{
					triangles[num++] = 0;
					triangles[num++] = 2;
					triangles[num++] = 1;
					triangles[num++] = 3;
					triangles[num++] = 1;
					triangles[num++] = 2;
				}
			}
			else if (this.properties.colliderType == ExtendedColliders3D.ColliderType.Triangle)
			{
				vertices = new Vector3[]
				{
					new Vector3(-0.5f, 0f, -0.5f),
					new Vector3(-0.5f, 0f, 0.5f),
					new Vector3(0.5f, 0f, -0.5f)
				};
				triangles = new int[this.properties.triangleTwoSided ? 6 : 3];
				triangles[num++] = 0;
				triangles[num++] = 1;
				triangles[num++] = 2;
				if (this.properties.triangleTwoSided)
				{
					triangles[num++] = 0;
					triangles[num++] = 2;
					triangles[num++] = 1;
				}
			}
			else
			{
				if (this.properties.colliderType != ExtendedColliders3D.ColliderType.Sphere)
				{
					throw new Exception("Extended Colliders 3D: Unknown collider type.");
				}
				vertices = new Vector3[(this.properties.sphereStacks - 1) * this.properties.sphereSlices + 2];
				triangles = new int[this.properties.sphereStacks * this.properties.sphereSlices * 6];
				vertices[0] = new Vector3(0f, 0.5f, 0f);
				vertices[vertices.Length - 1] = new Vector3(0f, -0.5f, 0f);
				for (int num17 = 1; num17 < this.properties.sphereStacks; num17++)
				{
					float num18 = Mathf.Sin((float)num17 / (float)this.properties.sphereStacks * 3.1415927f);
					float y = Mathf.Cos((float)num17 / (float)this.properties.sphereStacks * 3.1415927f) / 2f;
					for (int num19 = 0; num19 < this.properties.sphereSlices; num19++)
					{
						int num20 = (num17 - 1) * this.properties.sphereSlices + num19 + 1;
						int num21 = (num19 == this.properties.sphereSlices - 1) ? ((num17 - 1) * this.properties.sphereSlices + 1) : (num20 + 1);
						float f4 = (float)num19 / (float)this.properties.sphereSlices * 3.1415927f * 2f;
						vertices[num20] = new Vector3(Mathf.Sin(f4) / 2f * num18, y, Mathf.Cos(f4) / 2f * num18);
						if (num17 == 1)
						{
							triangles[num19 * 3] = 0;
							triangles[num19 * 3 + 1] = num20;
							triangles[num19 * 3 + 2] = num21;
						}
						else
						{
							int num22 = this.properties.sphereSlices * 3 + (num17 - 2) * this.properties.sphereSlices * 6 + num19 * 6;
							triangles[num22] = num20;
							triangles[num22 + 1] = num21;
							triangles[num22 + 2] = num20 - this.properties.sphereSlices;
							triangles[num22 + 3] = num21 - this.properties.sphereSlices;
							triangles[num22 + 4] = num20 - this.properties.sphereSlices;
							triangles[num22 + 5] = num21;
							if (num17 == this.properties.sphereStacks - 1)
							{
								num22 = this.properties.sphereSlices * 3 + (this.properties.sphereStacks - 2) * this.properties.sphereSlices * 6 + num19 * 3;
								triangles[num22] = num21;
								triangles[num22 + 1] = num20;
								triangles[num22 + 2] = vertices.Length - 1;
							}
						}
					}
				}
			}
			for (int num23 = 0; num23 < vertices.Length; num23++)
			{
				Vector3[] array5 = vertices;
				int num24 = num23;
				array5[num24].x = array5[num24].x * this.properties.size.x;
				Vector3[] array6 = vertices;
				int num25 = num23;
				array6[num25].y = array6[num25].y * this.properties.size.y;
				Vector3[] array7 = vertices;
				int num26 = num23;
				array7[num26].z = array7[num26].z * this.properties.size.z;
				vertices[num23] = Quaternion.Euler(this.properties.rotation) * vertices[num23];
				vertices[num23] += this.properties.centre;
				if (applyTransform)
				{
					Transform transform = base.transform;
					while (transform != null)
					{
						Vector3[] array8 = vertices;
						int num27 = num23;
						array8[num27].x = array8[num27].x * transform.localScale.x;
						Vector3[] array9 = vertices;
						int num28 = num23;
						array9[num28].y = array9[num28].y * transform.localScale.y;
						Vector3[] array10 = vertices;
						int num29 = num23;
						array10[num29].z = array10[num29].z * transform.localScale.z;
						vertices[num23] = transform.localRotation * vertices[num23];
						vertices[num23] += transform.localPosition;
						transform = transform.parent;
					}
				}
			}
			if (this.properties.flipFaces)
			{
				for (int num30 = 0; num30 < triangles.Length / 3; num30++)
				{
					int num31 = triangles[num30 * 3];
					triangles[num30 * 3] = triangles[num30 * 3 + 1];
					triangles[num30 * 3 + 1] = num31;
				}
			}
		}

		
		public void autoSizeColliderToMeshFilter()
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			Mesh mesh = null;
			if (component != null)
			{
				mesh = component.sharedMesh;
			}
			if (mesh != null)
			{
				this.properties.centre = mesh.bounds.center;
				this.properties.size = mesh.bounds.size;
			}
		}

		
		public ExtendedColliders3D.ExtendedCollders3DProperties properties = new ExtendedColliders3D.ExtendedCollders3DProperties();

		
		public enum ColliderType
		{
			
			Circle,
			
			CircleHalf,
			
			Cone,
			
			ConeHalf,
			
			Cube,
			
			Cylinder,
			
			CylinderHalf,
			
			Quad,
			
			Triangle,
			
			Sphere
		}

		
		[Serializable]
		public class ExtendedCollders3DProperties
		{
			
			public bool convex;

			
			public bool isTrigger;

			
			public PhysicMaterial material;

			
			public ExtendedColliders3D.ColliderType colliderType = ExtendedColliders3D.ColliderType.Cylinder;

			
			public Vector3 centre = Vector3.zero;

			
			public Vector3 rotation = Vector3.zero;

			
			public Vector3 size = Vector3.one;

			
			public bool flipFaces;

			
			public int circleVertices = 16;

			
			public bool circleTwoSided;

			
			public int coneFaces = 16;

			
			public bool coneCap = true;

			
			public bool coneHalfCapFlatEnd = true;

			
			public bool cubeTopFace = true;

			
			public bool cubeBottomFace = true;

			
			public bool cubeLeftFace = true;

			
			public bool cubeRightFace = true;

			
			public bool cubeForwardFace = true;

			
			public bool cubeBackFace = true;

			
			public int cylinderFaces = 16;

			
			public bool cylinderCapTop = true;

			
			public bool cylinderCapBottom = true;

			
			public Vector2 cylinderTaperTop = Vector2.one;

			
			public Vector2 cylinderTaperBottom = Vector2.one;

			
			public bool cylinderHalfCapFlatEnd = true;

			
			public bool quadTwoSided;

			
			public bool triangleTwoSided;

			
			public int sphereStacks = 8;

			
			public int sphereSlices = 16;

			
			public Color colour = new Color32(145, 244, 140, 239);
		}
	}
}
