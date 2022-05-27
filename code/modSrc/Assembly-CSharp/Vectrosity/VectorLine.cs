using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vectrosity
{
	
	[Serializable]
	public class VectorLine
	{
		
		public static string Version()
		{
			return "Vectrosity version 5.6";
		}

		
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00153E64 File Offset: 0x00152064
		public Vector3[] lineVertices
		{
			get
			{
				return this.m_lineVertices;
			}
		}

		
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x00153E6C File Offset: 0x0015206C
		public Vector2[] lineUVs
		{
			get
			{
				return this.m_lineUVs;
			}
		}

		
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00153E74 File Offset: 0x00152074
		public Color32[] lineColors
		{
			get
			{
				return this.m_lineColors;
			}
		}

		
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x00153E7C File Offset: 0x0015207C
		public List<int> lineTriangles
		{
			get
			{
				return this.m_lineTriangles;
			}
		}

		
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00153E84 File Offset: 0x00152084
		public RectTransform rectTransform
		{
			get
			{
				if (this.m_go != null)
				{
					return this.m_rectTransform;
				}
				return null;
			}
		}

		
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00153E9C File Offset: 0x0015209C
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x00153EA4 File Offset: 0x001520A4
		public Color32 color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
				this.SetColor(value);
			}
		}

		
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x00153EB4 File Offset: 0x001520B4
		public bool is2D
		{
			get
			{
				return this.m_is2D;
			}
		}

		
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00153EBC File Offset: 0x001520BC
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x00153EE8 File Offset: 0x001520E8
		public List<Vector2> points2
		{
			get
			{
				if (!this.m_is2D)
				{
					Debug.LogError("Line \"" + this.name + "\" uses points3 rather than points2");
					return null;
				}
				return this.m_points2;
			}
			set
			{
				if (value == null)
				{
					Debug.LogError("List for Line \"" + this.name + "\" must not be null");
					return;
				}
				this.m_points2 = value;
			}
		}

		
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x00153F0F File Offset: 0x0015210F
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x00153F3B File Offset: 0x0015213B
		public List<Vector3> points3
		{
			get
			{
				if (this.m_is2D)
				{
					Debug.LogError("Line \"" + this.name + "\" uses points2 rather than points3");
					return null;
				}
				return this.m_points3;
			}
			set
			{
				if (value == null)
				{
					Debug.LogError("List for Line \"" + this.name + "\" must not be null");
					return;
				}
				this.m_points3 = value;
			}
		}

		
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x00153F62 File Offset: 0x00152162
		private int pointsCount
		{
			get
			{
				if (!this.m_is2D)
				{
					return this.m_points3.Count;
				}
				return this.m_points2.Count;
			}
		}

		
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00153F83 File Offset: 0x00152183
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x00153F8C File Offset: 0x0015218C
		public float lineWidth
		{
			get
			{
				return this.m_lineWidth;
			}
			set
			{
				this.m_lineWidth = value;
				float num = value * 0.5f;
				for (int i = 0; i < this.m_lineWidths.Length; i++)
				{
					this.m_lineWidths[i] = num;
				}
				this.m_maxWeldDistance = value * 2f * (value * 2f);
			}
		}

		
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00153FD9 File Offset: 0x001521D9
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x00153FE6 File Offset: 0x001521E6
		public float maxWeldDistance
		{
			get
			{
				return Mathf.Sqrt(this.m_maxWeldDistance);
			}
			set
			{
				this.m_maxWeldDistance = value * value;
			}
		}

		
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x00153FF1 File Offset: 0x001521F1
		// (set) Token: 0x060020A7 RID: 8359 RVA: 0x00153FF9 File Offset: 0x001521F9
		public string name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
				if (this.m_go != null)
				{
					this.m_go.name = value;
				}
				if (this.m_vectorObject != null)
				{
					this.m_vectorObject.SetName(value);
				}
			}
		}

		
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00154030 File Offset: 0x00152230
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x00154038 File Offset: 0x00152238
		public Material material
		{
			get
			{
				return this.m_material;
			}
			set
			{
				if (this.m_vectorObject != null)
				{
					this.m_vectorObject.SetMaterial(value);
				}
				this.m_material = value;
			}
		}

		
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00154055 File Offset: 0x00152255
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x0015405D File Offset: 0x0015225D
		public Texture texture
		{
			get
			{
				return this.m_texture;
			}
			set
			{
				if (this.m_capType != EndCap.None)
				{
					this.m_originalTexture = value;
					return;
				}
				if (this.m_vectorObject != null)
				{
					this.m_vectorObject.SetTexture(value);
				}
				this.m_texture = value;
			}
		}

		
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0015408B File Offset: 0x0015228B
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x001540A8 File Offset: 0x001522A8
		public int layer
		{
			get
			{
				if (this.m_go != null)
				{
					return this.m_go.layer;
				}
				return 0;
			}
			set
			{
				if (this.m_go != null)
				{
					this.m_go.layer = Mathf.Clamp(value, 0, 31);
				}
			}
		}

		
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x001540CC File Offset: 0x001522CC
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x001540D4 File Offset: 0x001522D4
		public bool active
		{
			get
			{
				return this.m_active;
			}
			set
			{
				this.m_active = value;
				if (this.m_vectorObject != null)
				{
					this.m_vectorObject.Enable(value);
				}
			}
		}

		
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x001540F1 File Offset: 0x001522F1
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x001540FC File Offset: 0x001522FC
		public LineType lineType
		{
			get
			{
				return this.m_lineType;
			}
			set
			{
				if (value != this.m_lineType)
				{
					this.m_lineType = value;
					if (value == LineType.Points || (value == LineType.Discrete && this.m_joins == Joins.Fill))
					{
						this.m_joins = Joins.None;
					}
					if (value == LineType.Discrete)
					{
						this.drawStart = this.m_drawStart;
						this.drawEnd = this.m_drawEnd;
					}
					if (value != LineType.Continuous && ((this.m_points2 != null && this.m_points2.Count > 16383) || (this.m_points3 != null && this.m_points3.Count > 16383)))
					{
						this.Resize(16383);
					}
					if (this.collider)
					{
						Collider2D component = this.m_go.GetComponent<Collider2D>();
						if (component != null)
						{
							UnityEngine.Object.DestroyImmediate(component);
						}
						this.AddColliderIfNeeded();
					}
					this.ResetLine();
				}
			}
		}

		
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x001541C0 File Offset: 0x001523C0
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x001541C8 File Offset: 0x001523C8
		public float capLength
		{
			get
			{
				return this.m_capLength;
			}
			set
			{
				if (this.m_lineType == LineType.Points)
				{
					Debug.LogError("LineType.Points can't use capLength");
					return;
				}
				this.m_capLength = value;
			}
		}

		
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x001541E5 File Offset: 0x001523E5
		// (set) Token: 0x060020B5 RID: 8373 RVA: 0x001541ED File Offset: 0x001523ED
		public bool smoothWidth
		{
			get
			{
				return this.m_smoothWidth;
			}
			set
			{
				this.m_smoothWidth = (this.m_lineType != LineType.Points && value);
			}
		}

		
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x00154202 File Offset: 0x00152402
		// (set) Token: 0x060020B7 RID: 8375 RVA: 0x0015420C File Offset: 0x0015240C
		public bool smoothColor
		{
			get
			{
				return this.m_smoothColor;
			}
			set
			{
				bool smoothColor = this.m_smoothColor;
				this.m_smoothColor = (this.m_lineType != LineType.Points && value);
				if (this.m_smoothColor != smoothColor)
				{
					int segmentNumber = this.GetSegmentNumber();
					for (int i = 0; i < segmentNumber; i++)
					{
						this.SetColor(this.GetColor(i), i);
					}
				}
			}
		}

		
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0015425D File Offset: 0x0015245D
		// (set) Token: 0x060020B9 RID: 8377 RVA: 0x00154268 File Offset: 0x00152468
		public Joins joins
		{
			get
			{
				return this.m_joins;
			}
			set
			{
				if (this.m_lineType == LineType.Points || (this.m_lineType == LineType.Discrete && value == Joins.Fill))
				{
					return;
				}
				if ((this.m_joins == Joins.Fill && value != Joins.Fill) || (this.m_joins != Joins.Fill && value == Joins.Fill))
				{
					this.m_joins = value;
					this.ClearTriangles();
					this.SetupTriangles(0);
				}
				this.m_joins = value;
				if (this.m_joins == Joins.Weld)
				{
					if (this.m_canvasState == CanvasState.OnCanvas)
					{
						this.Draw();
						return;
					}
					if (this.m_canvasState == CanvasState.OffCanvas)
					{
						this.Draw3D();
					}
				}
			}
		}

		
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x001542E4 File Offset: 0x001524E4
		public bool isAutoDrawing
		{
			get
			{
				return this.m_isAutoDrawing;
			}
		}

		
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x001542EC File Offset: 0x001524EC
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x001542F4 File Offset: 0x001524F4
		public int drawStart
		{
			get
			{
				return this.m_drawStart;
			}
			set
			{
				if (this.m_lineType == LineType.Discrete && (value & 1) != 0)
				{
					value++;
				}
				this.m_drawStart = Mathf.Clamp(value, 0, this.pointsCount - 1);
			}
		}

		
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0015431E File Offset: 0x0015251E
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x00154326 File Offset: 0x00152526
		public int drawEnd
		{
			get
			{
				return this.m_drawEnd;
			}
			set
			{
				if (this.m_lineType == LineType.Discrete && value != 0 && (value & 1) == 0)
				{
					value++;
				}
				this.m_drawEnd = Mathf.Clamp(value, 0, this.pointsCount - 1);
			}
		}

		
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x00154353 File Offset: 0x00152553
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x00154377 File Offset: 0x00152577
		public int endPointsUpdate
		{
			get
			{
				if (this.m_lineType != LineType.Discrete)
				{
					return this.m_endPointsUpdate;
				}
				if (this.m_endPointsUpdate != 0)
				{
					return this.m_endPointsUpdate + 1;
				}
				return 0;
			}
			set
			{
				if (this.m_lineType == LineType.Discrete && value > 1 && (value & 1) == 0)
				{
					value--;
				}
				this.m_endPointsUpdate = Mathf.Max(0, value);
			}
		}

		
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0015439D File Offset: 0x0015259D
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x001543A8 File Offset: 0x001525A8
		public string endCap
		{
			get
			{
				return this.m_endCap;
			}
			set
			{
				if (this.m_lineType == LineType.Points)
				{
					Debug.LogError("LineType.Points can't use end caps");
					return;
				}
				if (this.m_endCap == value)
				{
					return;
				}
				if (value == null || value == "")
				{
					this.RemoveEndCap();
					return;
				}
				if (VectorLine.capDictionary == null || !VectorLine.capDictionary.ContainsKey(value))
				{
					Debug.LogError("End cap \"" + value + "\" is not set up");
					return;
				}
				if (this.m_capType != EndCap.None)
				{
					this.RemoveEndCap();
				}
				this.m_endCap = value;
				this.m_capType = VectorLine.capDictionary[value].capType;
				if (this.m_capType != EndCap.None)
				{
					this.SetupEndCap(VectorLine.capDictionary[value].uvHeights);
				}
			}
		}

		
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x00154464 File Offset: 0x00152664
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x0015446C File Offset: 0x0015266C
		public bool continuousTexture
		{
			get
			{
				return this.m_continuousTexture;
			}
			set
			{
				this.m_continuousTexture = value;
				if (!value)
				{
					this.ResetTextureScale();
				}
			}
		}

		
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0015447E File Offset: 0x0015267E
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x00154486 File Offset: 0x00152686
		public Transform drawTransform
		{
			get
			{
				return this.m_drawTransform;
			}
			set
			{
				this.m_drawTransform = value;
			}
		}

		
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0015448F File Offset: 0x0015268F
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x00154497 File Offset: 0x00152697
		public bool useViewportCoords
		{
			get
			{
				return this.m_viewportDraw;
			}
			set
			{
				if (this.m_is2D)
				{
					this.m_viewportDraw = value;
					return;
				}
				Debug.LogError("Line must use Vector2 points in order to use viewport coords");
			}
		}

		
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x001544B3 File Offset: 0x001526B3
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x001544BB File Offset: 0x001526BB
		[SerializeField]
		public float textureScale
		{
			get
			{
				return this.m_textureScale;
			}
			set
			{
				this.m_textureScale = value;
				if (this.m_textureScale == 0f)
				{
					this.m_useTextureScale = false;
					this.ResetTextureScale();
					return;
				}
				this.m_useTextureScale = true;
			}
		}

		
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x001544E6 File Offset: 0x001526E6
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x001544EE File Offset: 0x001526EE
		public float textureOffset
		{
			get
			{
				return this.m_textureOffset;
			}
			set
			{
				this.m_textureOffset = value;
				this.SetTextureScale();
			}
		}

		
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x001544FD File Offset: 0x001526FD
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x00154505 File Offset: 0x00152705
		public Matrix4x4 matrix
		{
			get
			{
				return this.m_matrix;
			}
			set
			{
				this.m_matrix = value;
				this.m_useMatrix = (this.m_matrix != Matrix4x4.identity);
			}
		}

		
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x00154524 File Offset: 0x00152724
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x0015454B File Offset: 0x0015274B
		public int drawDepth
		{
			get
			{
				if (this.m_canvasState == CanvasState.OffCanvas)
				{
					Debug.LogError("VectorLine.drawDepth can't be used with lines made with Draw3D");
					return 0;
				}
				return this.m_go.transform.GetSiblingIndex();
			}
			set
			{
				if (this.m_canvasState == CanvasState.OffCanvas)
				{
					Debug.LogError("VectorLine.drawDepth can't be used with lines made with Draw3D");
					return;
				}
				this.m_go.transform.SetSiblingIndex(value);
			}
		}

		
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00154572 File Offset: 0x00152772
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x0015457A File Offset: 0x0015277A
		public bool collider
		{
			get
			{
				return this.m_collider;
			}
			set
			{
				this.m_collider = value;
				this.AddColliderIfNeeded();
				this.m_go.GetComponent<Collider2D>().enabled = value;
			}
		}

		
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0015459A File Offset: 0x0015279A
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x001545A2 File Offset: 0x001527A2
		public bool trigger
		{
			get
			{
				return this.m_trigger;
			}
			set
			{
				this.m_trigger = value;
				if (this.m_go.GetComponent<Collider2D>() != null)
				{
					this.m_go.GetComponent<Collider2D>().isTrigger = value;
				}
			}
		}

		
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x001545CF File Offset: 0x001527CF
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x001545D7 File Offset: 0x001527D7
		public PhysicsMaterial2D physicsMaterial
		{
			get
			{
				return this.m_physicsMaterial;
			}
			set
			{
				this.AddColliderIfNeeded();
				this.m_physicsMaterial = value;
				this.m_go.GetComponent<Collider2D>().sharedMaterial = value;
			}
		}

		
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x001545F7 File Offset: 0x001527F7
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x00154600 File Offset: 0x00152800
		public bool alignOddWidthToPixels
		{
			get
			{
				return this.m_alignOddWidthToPixels;
			}
			set
			{
				float num = value ? 0.5f : 0f;
				this.m_rectTransform.anchoredPosition = new Vector2(num, num);
				this.m_alignOddWidthToPixels = value;
			}
		}

		
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00154636 File Offset: 0x00152836
		public static Canvas canvas
		{
			get
			{
				if (VectorLine.m_canvas == null)
				{
					VectorLine.SetupVectorCanvas();
				}
				return VectorLine.m_canvas;
			}
		}

		
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00154650 File Offset: 0x00152850
		public static Vector3 camTransformPosition
		{
			get
			{
				return VectorLine.camTransform.position;
			}
		}

		
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0015465C File Offset: 0x0015285C
		public static bool camTransformExists
		{
			get
			{
				return VectorLine.camTransform != null;
			}
		}

		
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00154669 File Offset: 0x00152869
		public static LineManager lineManager
		{
			get
			{
				if (!VectorLine.lineManagerCreated)
				{
					VectorLine.lineManagerCreated = true;
					VectorLine.m_lineManager = new GameObject("LineManager").AddComponent<LineManager>();
					VectorLine.m_lineManager.enabled = false;
					UnityEngine.Object.DontDestroyOnLoad(VectorLine.m_lineManager);
				}
				return VectorLine.m_lineManager;
			}
		}

		
		private void AddColliderIfNeeded()
		{
			if (this.m_go.GetComponent<Collider2D>() == null)
			{
				this.m_go.AddComponent((this.m_lineType == LineType.Continuous) ? typeof(EdgeCollider2D) : typeof(PolygonCollider2D));
				this.m_go.GetComponent<Collider2D>().isTrigger = this.m_trigger;
				this.m_go.GetComponent<Collider2D>().sharedMaterial = this.m_physicsMaterial;
			}
		}

		
		public VectorLine(string name, List<Vector3> points, float width)
		{
			this.m_points3 = points;
			this.SetupLine(name, null, width, LineType.Discrete, Joins.None, false);
		}

		
		public VectorLine(string name, List<Vector3> points, Texture texture, float width)
		{
			this.m_points3 = points;
			this.SetupLine(name, texture, width, LineType.Discrete, Joins.None, false);
		}

		
		public VectorLine(string name, List<Vector3> points, float width, LineType lineType)
		{
			this.m_points3 = points;
			this.SetupLine(name, null, width, lineType, Joins.None, false);
		}

		
		public VectorLine(string name, List<Vector3> points, Texture texture, float width, LineType lineType)
		{
			this.m_points3 = points;
			this.SetupLine(name, texture, width, lineType, Joins.None, false);
		}

		
		public VectorLine(string name, List<Vector3> points, float width, LineType lineType, Joins joins)
		{
			this.m_points3 = points;
			this.SetupLine(name, null, width, lineType, joins, false);
		}

		
		public VectorLine(string name, List<Vector3> points, Texture texture, float width, LineType lineType, Joins joins)
		{
			this.m_points3 = points;
			this.SetupLine(name, texture, width, lineType, joins, false);
		}

		
		public VectorLine(string name, List<Vector2> points, float width)
		{
			this.m_points2 = points;
			this.SetupLine(name, null, width, LineType.Discrete, Joins.None, true);
		}

		
		public VectorLine(string name, List<Vector2> points, Texture texture, float width)
		{
			this.m_points2 = points;
			this.SetupLine(name, texture, width, LineType.Discrete, Joins.None, true);
		}

		
		public VectorLine(string name, List<Vector2> points, float width, LineType lineType)
		{
			this.m_points2 = points;
			this.SetupLine(name, null, width, lineType, Joins.None, true);
		}

		
		public VectorLine(string name, List<Vector2> points, Texture texture, float width, LineType lineType)
		{
			this.m_points2 = points;
			this.SetupLine(name, texture, width, lineType, Joins.None, true);
		}

		
		public VectorLine(string name, List<Vector2> points, float width, LineType lineType, Joins joins)
		{
			this.m_points2 = points;
			this.SetupLine(name, null, width, lineType, joins, true);
		}

		
		public VectorLine(string name, List<Vector2> points, Texture texture, float width, LineType lineType, Joins joins)
		{
			this.m_points2 = points;
			this.SetupLine(name, texture, width, lineType, joins, true);
		}

		
		protected void SetupLine(string lineName, Texture texture, float width, LineType lineType, Joins joins, bool use2D)
		{
			this.m_is2D = use2D;
			this.m_lineType = lineType;
			if (joins == Joins.Fill && this.m_lineType != LineType.Continuous)
			{
				Debug.LogError("VectorLine: Must use LineType.Continuous if using Joins.Fill for \"" + lineName + "\"");
				return;
			}
			if (joins == Joins.Weld && this.m_lineType == LineType.Points)
			{
				Debug.LogError("VectorLine: LineType.Points can't use Joins.Weld for \"" + lineName + "\"");
				return;
			}
			if ((this.m_is2D && this.m_points2 == null) || (!this.m_is2D && this.m_points3 == null))
			{
				Debug.LogError("VectorLine: the points array is null for \"" + lineName + "\"");
				return;
			}
			if (this.m_is2D)
			{
				this.m_pointsCount = ((this.m_points2.Capacity > 0 && this.m_points2.Count == 0) ? this.m_points2.Capacity : this.m_points2.Count);
				int num = this.m_pointsCount - this.m_points2.Count;
				for (int i = 0; i < num; i++)
				{
					this.m_points2.Add(Vector2.zero);
				}
			}
			else
			{
				this.m_pointsCount = ((this.m_points3.Capacity > 0 && this.m_points3.Count == 0) ? this.m_points3.Capacity : this.m_points3.Count);
				int num2 = this.m_pointsCount - this.m_points3.Count;
				for (int j = 0; j < num2; j++)
				{
					this.m_points3.Add(Vector3.zero);
				}
			}
			this.name = lineName;
			if (!this.SetVertexCount())
			{
				return;
			}
			this.m_go = new GameObject(this.name);
			this.m_canvasState = CanvasState.None;
			this.layer = LayerMask.NameToLayer("UI");
			this.m_rectTransform = this.m_go.AddComponent<RectTransform>();
			VectorLine.SetupTransform(this.m_rectTransform);
			this.m_texture = texture;
			this.m_lineVertices = new Vector3[this.m_vertexCount];
			this.m_lineUVs = new Vector2[this.m_vertexCount];
			this.m_lineColors = new Color32[this.m_vertexCount];
			this.m_lineUVBottom = 0f;
			this.m_lineUVTop = 1f;
			this.SetUVs(0, this.GetSegmentNumber());
			this.m_lineTriangles = new List<int>();
			this.color = Color.white;
			this.m_maxWeldDistance = width * 2f * (width * 2f);
			this.m_joins = joins;
			this.m_lineWidths = new float[1];
			this.m_lineWidths[0] = width * 0.5f;
			this.m_lineWidth = width;
			if (!this.m_is2D)
			{
				this.m_screenPoints = new Vector3[this.m_vertexCount];
			}
			this.m_drawStart = 0;
			this.m_drawEnd = this.m_pointsCount - 1;
			this.SetupTriangles(0);
		}

		
		private void SetupTriangles(int startVert)
		{
			int num = 0;
			int num2 = 0;
			if (this.pointsCount > 0)
			{
				if (this.m_lineType == LineType.Points)
				{
					num = this.pointsCount * 6;
					num2 = this.pointsCount * 4;
				}
				else if (this.m_lineType == LineType.Continuous)
				{
					num = ((this.m_joins == Joins.Fill) ? ((this.pointsCount - 1) * 12) : ((this.pointsCount - 1) * 6));
					num2 = (this.pointsCount - 1) * 4;
				}
				else
				{
					num = this.pointsCount / 2 * 6;
					num2 = this.pointsCount * 2;
				}
			}
			if (this.m_capType != EndCap.None)
			{
				num += 12;
			}
			if (this.m_lineTriangles.Count <= num)
			{
				if (this.m_joins == Joins.Fill)
				{
					if (startVert >= 4)
					{
						int num3 = this.m_lineTriangles.Count - 6;
						this.m_lineTriangles[num3] = startVert - 3;
						this.m_lineTriangles[num3 + 1] = startVert;
						this.m_lineTriangles[num3 + 2] = startVert + 3;
						this.m_lineTriangles[num3 + 3] = startVert - 2;
						this.m_lineTriangles[num3 + 4] = startVert;
						this.m_lineTriangles[num3 + 5] = startVert + 3;
					}
					for (int i = startVert; i < num2; i += 4)
					{
						this.m_lineTriangles.Add(i);
						this.m_lineTriangles.Add(i + 1);
						this.m_lineTriangles.Add(i + 3);
						this.m_lineTriangles.Add(i + 1);
						this.m_lineTriangles.Add(i + 2);
						this.m_lineTriangles.Add(i + 3);
						this.m_lineTriangles.Add(i + 1);
						this.m_lineTriangles.Add(i + 4);
						this.m_lineTriangles.Add(i + 7);
						this.m_lineTriangles.Add(i + 2);
						this.m_lineTriangles.Add(i + 4);
						this.m_lineTriangles.Add(i + 7);
					}
					this.SetLastFillTriangles();
				}
				else
				{
					for (int j = startVert; j < num2; j += 4)
					{
						this.m_lineTriangles.Add(j);
						this.m_lineTriangles.Add(j + 1);
						this.m_lineTriangles.Add(j + 3);
						this.m_lineTriangles.Add(j + 1);
						this.m_lineTriangles.Add(j + 2);
						this.m_lineTriangles.Add(j + 3);
					}
				}
				if (this.m_vectorObject != null)
				{
					this.m_vectorObject.UpdateTris();
				}
				return;
			}
			this.m_lineTriangles.RemoveRange(num, this.m_lineTriangles.Count - num);
			if (this.m_joins == Joins.Fill)
			{
				this.SetLastFillTriangles();
				return;
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateTris();
			}
		}

		
		private void SetLastFillTriangles()
		{
			if (this.pointsCount < 2)
			{
				return;
			}
			int num = (this.pointsCount - 1) * 12 + ((this.m_capType != EndCap.None) ? 12 : 0);
			bool flag = false;
			if ((this.m_is2D && this.m_points2[0] == this.m_points2[this.points2.Count - 1]) || (!this.m_is2D && this.m_points3[0] == this.m_points3[this.points3.Count - 1]))
			{
				if (this.m_lineTriangles[num - 4] != 3 && this.m_lineTriangles[num - 1] != 3)
				{
					flag = true;
				}
				this.m_lineTriangles[num - 6] = this.m_vertexCount - 3;
				this.m_lineTriangles[num - 5] = 0;
				this.m_lineTriangles[num - 4] = 3;
				this.m_lineTriangles[num - 3] = this.m_vertexCount - 2;
				this.m_lineTriangles[num - 2] = 0;
				this.m_lineTriangles[num - 1] = 3;
			}
			else
			{
				if (this.m_lineTriangles[num - 4] == 3 && this.m_lineTriangles[num - 1] == 3)
				{
					flag = true;
				}
				this.m_lineTriangles[num - 6] = 0;
				this.m_lineTriangles[num - 5] = 0;
				this.m_lineTriangles[num - 4] = 0;
				this.m_lineTriangles[num - 3] = 0;
				this.m_lineTriangles[num - 2] = 0;
				this.m_lineTriangles[num - 1] = 0;
			}
			if (flag && this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateTris();
			}
		}

		
		private void SetupEndCap(float[] uvHeights)
		{
			int num = this.m_vertexCount + 8;
			if (num > 65534)
			{
				Debug.LogError("VectorLine: exceeded maximum vertex count of 65534 for \"" + this.m_name + "\"...use fewer points");
				return;
			}
			this.ResizeMeshArrays(num);
			int num2 = 0;
			if (this.m_joins == Joins.Fill)
			{
				for (int i = num - 8; i < num; i += 4)
				{
					this.m_lineTriangles.Insert(num2, i);
					this.m_lineTriangles.Insert(1 + num2, i + 1);
					this.m_lineTriangles.Insert(2 + num2, i + 3);
					this.m_lineTriangles.Insert(3 + num2, i + 1);
					this.m_lineTriangles.Insert(4 + num2, i + 2);
					this.m_lineTriangles.Insert(5 + num2, i + 3);
					num2 += 6;
				}
			}
			else
			{
				for (int j = num - 8; j < num; j += 4)
				{
					this.m_lineTriangles.Insert(num2, j);
					this.m_lineTriangles.Insert(1 + num2, j + 1);
					this.m_lineTriangles.Insert(2 + num2, j + 3);
					this.m_lineTriangles.Insert(3 + num2, j + 1);
					this.m_lineTriangles.Insert(4 + num2, j + 2);
					this.m_lineTriangles.Insert(5 + num2, j + 3);
					num2 += 6;
				}
			}
			int num3 = (num >= 12) ? (num - 12) : 0;
			for (int k = num - 8; k < num - 4; k++)
			{
				this.m_lineColors[k] = this.m_lineColors[0];
				this.m_lineColors[k + 4] = this.m_lineColors[num3];
			}
			this.m_lineUVBottom = uvHeights[0];
			this.m_lineUVTop = uvHeights[1];
			this.m_backCapUVBottom = uvHeights[2];
			this.m_backCapUVTop = uvHeights[3];
			this.m_frontCapUVBottom = uvHeights[4];
			this.m_frontCapUVTop = uvHeights[5];
			this.SetUVs(0, this.GetSegmentNumber());
			this.SetEndCapUVs();
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateTris();
				this.m_vectorObject.UpdateUVs();
			}
			this.SetEndCapColors();
			this.m_originalTexture = this.m_texture;
			this.m_texture = VectorLine.capDictionary[this.m_endCap].texture;
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.SetTexture(this.m_texture);
			}
		}

		
		private void ResetLine()
		{
			this.SetVertexCount();
			this.m_lineVertices = new Vector3[this.m_vertexCount];
			this.m_lineUVs = new Vector2[this.m_vertexCount];
			this.m_lineColors = new Color32[this.m_vertexCount];
			if (!this.m_is2D)
			{
				this.m_screenPoints = new Vector3[this.m_vertexCount];
			}
			this.SetUVs(0, this.GetSegmentNumber());
			this.SetColor(this.m_color);
			int segmentNumber = this.GetSegmentNumber();
			this.SetupWidths(segmentNumber);
			this.ClearTriangles();
			this.SetupTriangles(0);
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateMeshAttributes();
			}
			if (this.m_canvasState == CanvasState.OnCanvas)
			{
				this.Draw();
				return;
			}
			if (this.m_canvasState == CanvasState.OffCanvas)
			{
				this.Draw3D();
			}
		}

		
		private void SetEndCapUVs()
		{
			this.m_lineUVs[this.m_vertexCount + 3] = new Vector2(0f, this.m_frontCapUVTop);
			this.m_lineUVs[this.m_vertexCount] = new Vector2(1f, this.m_frontCapUVTop);
			this.m_lineUVs[this.m_vertexCount + 1] = new Vector2(1f, this.m_frontCapUVBottom);
			this.m_lineUVs[this.m_vertexCount + 2] = new Vector2(0f, this.m_frontCapUVBottom);
			if (VectorLine.capDictionary[this.m_endCap].capType == EndCap.Mirror)
			{
				this.m_lineUVs[this.m_vertexCount + 7] = new Vector2(0f, this.m_frontCapUVBottom);
				this.m_lineUVs[this.m_vertexCount + 4] = new Vector2(1f, this.m_frontCapUVBottom);
				this.m_lineUVs[this.m_vertexCount + 5] = new Vector2(1f, this.m_frontCapUVTop);
				this.m_lineUVs[this.m_vertexCount + 6] = new Vector2(0f, this.m_frontCapUVTop);
				return;
			}
			this.m_lineUVs[this.m_vertexCount + 7] = new Vector2(0f, this.m_backCapUVTop);
			this.m_lineUVs[this.m_vertexCount + 4] = new Vector2(1f, this.m_backCapUVTop);
			this.m_lineUVs[this.m_vertexCount + 5] = new Vector2(1f, this.m_backCapUVBottom);
			this.m_lineUVs[this.m_vertexCount + 6] = new Vector2(0f, this.m_backCapUVBottom);
		}

		
		private void RemoveEndCap()
		{
			if (this.m_capType == EndCap.None)
			{
				return;
			}
			this.m_endCap = null;
			this.m_capType = EndCap.None;
			this.ResizeMeshArrays(this.m_vertexCount);
			this.m_lineTriangles.RemoveRange(0, 12);
			this.m_lineUVBottom = 0f;
			this.m_lineUVTop = 1f;
			this.SetUVs(0, this.GetSegmentNumber());
			if (this.m_useTextureScale)
			{
				this.SetTextureScale();
			}
			this.texture = this.m_originalTexture;
			this.m_vectorObject.UpdateMeshAttributes();
			if (this.m_collider)
			{
				this.SetCollider(this.m_canvasState == CanvasState.OnCanvas);
			}
		}

		
		private static void SetupTransform(RectTransform rectTransform)
		{
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.zero;
			rectTransform.pivot = Vector2.zero;
			rectTransform.anchoredPosition = Vector2.zero;
		}

		
		private void ResizeMeshArrays(int newCount)
		{
			Array.Resize<Vector3>(ref this.m_lineVertices, newCount);
			Array.Resize<Vector2>(ref this.m_lineUVs, newCount);
			Array.Resize<Color32>(ref this.m_lineColors, newCount);
		}

		
		public void Resize(int newCount)
		{
			if (newCount < 0)
			{
				Debug.LogError("VectorLine.Resize: the new count must be >= 0");
				return;
			}
			if (newCount == this.pointsCount)
			{
				return;
			}
			if (this.m_is2D)
			{
				if (newCount > this.m_pointsCount)
				{
					for (int i = 0; i < newCount - this.m_pointsCount; i++)
					{
						this.m_points2.Add(Vector2.zero);
					}
				}
				else
				{
					this.m_points2.RemoveRange(newCount, this.m_pointsCount - newCount);
				}
			}
			else if (newCount > this.m_pointsCount)
			{
				for (int j = 0; j < newCount - this.m_pointsCount; j++)
				{
					this.m_points3.Add(VectorLine.v3zero);
				}
			}
			else
			{
				this.m_points3.RemoveRange(newCount, this.m_pointsCount - newCount);
			}
			this.Resize();
		}

		
		private void Resize()
		{
			int pointsCount = this.m_pointsCount;
			int num = this.m_pointsCount;
			if (this.m_lineType != LineType.Points)
			{
				num = ((this.m_lineType == LineType.Continuous) ? Mathf.Max(0, this.m_pointsCount - 1) : (this.m_pointsCount / 2));
			}
			bool flag = this.m_drawEnd == this.m_pointsCount - 1 || this.m_drawEnd < 1;
			if (!this.SetVertexCount())
			{
				return;
			}
			this.m_pointsCount = this.pointsCount;
			int i = this.m_lineVertices.Length - ((this.m_capType == EndCap.None) ? 0 : 8);
			if (i < this.m_vertexCount)
			{
				if (i == 0)
				{
					i = 4;
				}
				while (i < this.m_pointsCount)
				{
					i *= 2;
				}
				i = Mathf.Min(i, this.MaxPoints());
				this.ResizeMeshArrays((this.m_capType == EndCap.None) ? (i * 4) : (i * 4 + 8));
				if (!this.m_is2D)
				{
					Array.Resize<Vector3>(ref this.m_screenPoints, i * 4);
				}
			}
			if (this.m_lineWidths.Length > 1)
			{
				if (this.m_lineType != LineType.Points)
				{
					i = ((this.m_lineType == LineType.Continuous) ? (i - 1) : (i / 2));
				}
				if (i > this.m_lineWidths.Length)
				{
					this.ResizeLineWidths(i);
				}
			}
			if (flag)
			{
				this.m_drawEnd = this.m_pointsCount - 1;
			}
			this.m_drawStart = Mathf.Clamp(this.m_drawStart, 0, this.m_pointsCount - 1);
			this.m_drawEnd = Mathf.Clamp(this.m_drawEnd, 0, this.m_pointsCount - 1);
			if (this.m_pointsCount > num)
			{
				this.SetColor(this.m_color, num, this.GetSegmentNumber());
				this.SetUVs(num, this.GetSegmentNumber());
			}
			if (this.m_pointsCount < pointsCount)
			{
				this.ZeroVertices(this.m_pointsCount, pointsCount);
			}
			if (this.m_capType != EndCap.None)
			{
				this.SetEndCapUVs();
				this.SetEndCapColors();
			}
			this.SetupTriangles(num * 4);
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateMeshAttributes();
			}
		}

		
		private void ResizeLineWidths(int newSize)
		{
			if (newSize > this.m_lineWidths.Length)
			{
				float[] array = new float[newSize];
				for (int i = 0; i < this.m_lineWidths.Length; i++)
				{
					array[i] = this.m_lineWidths[i];
				}
				for (int j = this.m_lineWidths.Length; j < newSize; j++)
				{
					array[j] = this.m_lineWidth * 0.5f;
				}
				this.m_lineWidths = array;
			}
		}

		
		private void SetUVs(int startIndex, int endIndex)
		{
			Vector2 vector = new Vector2(0f, this.m_lineUVTop);
			Vector2 vector2 = new Vector2(1f, this.m_lineUVTop);
			Vector2 vector3 = new Vector2(1f, this.m_lineUVBottom);
			Vector2 vector4 = new Vector2(0f, this.m_lineUVBottom);
			int num = startIndex * 4;
			for (int i = startIndex; i < endIndex; i++)
			{
				this.m_lineUVs[num] = vector;
				this.m_lineUVs[num + 1] = vector2;
				this.m_lineUVs[num + 2] = vector3;
				this.m_lineUVs[num + 3] = vector4;
				num += 4;
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateUVs();
			}
		}

		
		private bool SetVertexCount()
		{
			this.m_vertexCount = Mathf.Max(0, this.GetSegmentNumber() * 4);
			if (this.m_lineType == LineType.Discrete && (this.pointsCount & 1) != 0)
			{
				this.m_vertexCount += 4;
			}
			int num = 65534;
			if (this.m_capType != EndCap.None)
			{
				num -= 8;
			}
			if (this.m_vertexCount > num)
			{
				Debug.LogError("VectorLine: exceeded maximum vertex count of 65534 for \"" + this.name + "\"...use fewer points (maximum is 16383 points for continuous lines and points, and 32767 points for discrete lines, minus two if end caps are used)");
				return false;
			}
			return true;
		}

		
		private int MaxPoints()
		{
			if (this.m_capType != EndCap.None)
			{
				return 16381;
			}
			return 16383;
		}

		
		public void AddNormals()
		{
			this.m_useNormals = true;
			this.m_normalsCalculated = false;
		}

		
		public void AddTangents()
		{
			if (!this.m_useNormals)
			{
				this.m_useNormals = true;
				this.m_normalsCalculated = false;
			}
			this.m_useTangents = true;
			this.m_tangentsCalculated = false;
		}

		
		public Vector4[] CalculateTangents(Vector3[] normals)
		{
			if (!this.m_useNormals)
			{
				this.m_vectorObject.UpdateNormals();
				this.m_useNormals = true;
				this.m_normalsCalculated = true;
			}
			int num = this.m_vectorObject.VertexCount();
			Vector3[] array = new Vector3[num];
			Vector3[] array2 = new Vector3[num];
			int count = this.m_lineTriangles.Count;
			for (int i = 0; i < count; i += 3)
			{
				int num2 = this.m_lineTriangles[i];
				int num3 = this.m_lineTriangles[i + 1];
				int num4 = this.m_lineTriangles[i + 2];
				Vector3 vector = this.m_lineVertices[num2];
				Vector3 vector2 = this.m_lineVertices[num3];
				Vector3 vector3 = this.m_lineVertices[num4];
				Vector2 vector4 = this.m_lineUVs[num2];
				Vector2 vector5 = this.m_lineUVs[num3];
				Vector2 vector6 = this.m_lineUVs[num4];
				float num5 = vector2.x - vector.x;
				float num6 = vector3.x - vector.x;
				float num7 = vector2.y - vector.y;
				float num8 = vector3.y - vector.y;
				float num9 = vector2.z - vector.z;
				float num10 = vector3.z - vector.z;
				float num11 = vector5.x - vector4.x;
				float num12 = vector6.x - vector4.x;
				float num13 = vector5.y - vector4.y;
				float num14 = vector6.y - vector4.y;
				float num15 = 1f / (num11 * num14 - num12 * num13);
				Vector3 b = new Vector3((num14 * num5 - num13 * num6) * num15, (num14 * num7 - num13 * num8) * num15, (num14 * num9 - num13 * num10) * num15);
				Vector3 b2 = new Vector3((num11 * num6 - num12 * num5) * num15, (num11 * num8 - num12 * num7) * num15, (num11 * num10 - num12 * num9) * num15);
				array[num2] += b;
				array[num3] += b;
				array[num4] += b;
				array2[num2] += b2;
				array2[num3] += b2;
				array2[num4] += b2;
			}
			Vector4[] array3 = new Vector4[num];
			for (int j = 0; j < this.m_vertexCount; j++)
			{
				Vector3 vector7 = normals[j];
				Vector3 vector8 = array[j];
				array3[j] = (vector8 - vector7 * Vector3.Dot(vector7, vector8)).normalized;
				array3[j].w = ((Vector3.Dot(Vector3.Cross(vector7, vector8), array2[j]) < 0f) ? -1f : 1f);
			}
			return array3;
		}

		
		public static GameObject SetupVectorCanvas()
		{
			GameObject gameObject = GameObject.Find("VectorCanvas");
			Canvas canvas;
			if (gameObject != null)
			{
				canvas = gameObject.GetComponent<Canvas>();
			}
			else
			{
				gameObject = new GameObject("VectorCanvas");
				gameObject.layer = LayerMask.NameToLayer("UI");
				canvas = gameObject.AddComponent<Canvas>();
			}
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = 1;
			VectorLine.m_canvas = canvas;
			return gameObject;
		}

		
		public static void SetCanvasCamera(Camera cam)
		{
			if (VectorLine.m_canvas == null)
			{
				VectorLine.SetupVectorCanvas();
			}
			VectorLine.m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
			VectorLine.m_canvas.worldCamera = cam;
		}

		
		public void SetCanvas(GameObject canvasObject)
		{
			this.SetCanvas(canvasObject, true);
		}

		
		public void SetCanvas(GameObject canvasObject, bool worldPositionStays)
		{
			Canvas component = canvasObject.GetComponent<Canvas>();
			if (component == null)
			{
				Debug.LogError("VectorLine.SetCanvas: canvas object must have a Canvas component");
				return;
			}
			this.SetCanvas(component, worldPositionStays);
		}

		
		public void SetCanvas(Canvas canvas)
		{
			this.SetCanvas(canvas, true);
		}

		
		public void SetCanvas(Canvas canvas, bool worldPositionStays)
		{
			if (this.m_canvasState == CanvasState.OffCanvas)
			{
				Debug.LogError("VectorLine.SetCanvas only works with lines made with Draw, not Draw3D.");
				return;
			}
			if (canvas == null)
			{
				Debug.LogError("VectorLine.SetCanvas: canvas must not be null");
				return;
			}
			if (canvas.renderMode == RenderMode.WorldSpace)
			{
				Debug.LogError("VectorLine.SetCanvas: canvas must be screen space overlay or screen space camera");
				return;
			}
			this.m_go.transform.SetParent(canvas.transform, worldPositionStays);
		}

		
		public void SetMask(GameObject maskObject)
		{
			this.SetMask(maskObject, true);
		}

		
		public void SetMask(GameObject maskObject, bool worldPositionStays)
		{
			Mask component = maskObject.GetComponent<Mask>();
			if (component == null)
			{
				Debug.LogError("VectorLine.SetMask: mask object must have a Mask component");
				return;
			}
			this.SetMask(component, worldPositionStays);
		}

		
		public void SetMask(Mask mask)
		{
			this.SetMask(mask, true);
		}

		
		public void SetMask(Mask mask, bool worldPositionStays)
		{
			if (this.m_canvasState == CanvasState.OffCanvas)
			{
				Debug.LogError("VectorLine.SetMask only works with lines made with Draw, not Draw3D.");
				return;
			}
			if (mask == null)
			{
				Debug.LogError("VectorLine.SetMask: mask must not be null");
				return;
			}
			this.m_go.transform.SetParent(mask.transform, worldPositionStays);
		}

		
		private bool CheckCamera3D()
		{
			if (!this.m_is2D && !VectorLine.cam3D)
			{
				VectorLine.SetCamera3D();
				if (!VectorLine.cam3D)
				{
					Debug.LogError("No camera available...use VectorLine.SetCamera3D to assign a camera");
					return false;
				}
			}
			return true;
		}

		
		public static void SetCamera3D()
		{
			if (Camera.main == null)
			{
				Debug.LogError("VectorLine.SetCamera3D: no camera tagged \"Main Camera\" found. Please call SetCamera3D with a specific camera instead.");
				return;
			}
			VectorLine.SetCamera3D(Camera.main);
		}

		
		public static void SetCamera3D(GameObject cameraObject)
		{
			Camera component = cameraObject.GetComponent<Camera>();
			if (component == null)
			{
				Debug.LogError("VectorLine.SetCamera3D: camera object must have a Camera component");
				return;
			}
			VectorLine.SetCamera3D(component);
		}

		
		public static void SetCamera3D(Camera camera)
		{
			VectorLine.camTransform = camera.transform;
			VectorLine.cam3D = camera;
			VectorLine.oldPosition = VectorLine.camTransform.position + Vector3.one;
			VectorLine.oldRotation = VectorLine.camTransform.eulerAngles + Vector3.one;
		}

		
		public static bool CameraHasMoved()
		{
			return VectorLine.oldPosition != VectorLine.camTransform.position || VectorLine.oldRotation != VectorLine.camTransform.eulerAngles;
		}

		
		public static void UpdateCameraInfo()
		{
			VectorLine.oldPosition = VectorLine.camTransform.position;
			VectorLine.oldRotation = VectorLine.camTransform.eulerAngles;
		}

		
		public int GetSegmentNumber()
		{
			if (this.m_lineType == LineType.Points)
			{
				return this.pointsCount;
			}
			if (this.m_lineType != LineType.Continuous)
			{
				return this.pointsCount / 2;
			}
			if (this.pointsCount != 0)
			{
				return this.pointsCount - 1;
			}
			return 0;
		}

		
		private void SetEndCapColors()
		{
			if (this.m_lineVertices.Length < 4)
			{
				return;
			}
			if (this.m_capType <= EndCap.Mirror)
			{
				int num = (this.m_lineType == LineType.Continuous) ? (this.m_drawStart * 4) : (this.m_drawStart * 2);
				for (int i = 0; i < 4; i++)
				{
					this.m_lineColors[i + this.m_vertexCount] = (this.m_useCapColors ? this.m_frontColor : this.m_lineColors[i + num]);
				}
			}
			if (this.m_capType >= EndCap.Both)
			{
				int num2 = this.m_drawEnd;
				if (this.m_lineType == LineType.Continuous)
				{
					if (this.m_drawEnd == this.pointsCount)
					{
						num2--;
					}
				}
				else if (num2 < this.pointsCount)
				{
					num2++;
				}
				int num3 = num2 * ((this.m_lineType == LineType.Continuous) ? 4 : 2) - 2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				for (int j = 4; j < 8; j++)
				{
					this.m_lineColors[j + this.m_vertexCount] = (this.m_useCapColors ? this.m_backColor : this.m_lineColors[num3]);
				}
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateColors();
			}
		}

		
		public void SetEndCapColor(Color32 color)
		{
			this.SetEndCapColor(color, color);
		}

		
		public void SetEndCapColor(Color32 frontColor, Color32 backColor)
		{
			if (this.m_capType == EndCap.None)
			{
				Debug.LogError("VectorLine.SetEndCapColor: the line \"" + this.name + "\" does not have any end caps");
				return;
			}
			this.m_useCapColors = true;
			this.m_frontColor = frontColor;
			this.m_backColor = backColor;
			this.SetEndCapColors();
		}

		
		public void SetEndCapIndex(EndCap endCap, int index)
		{
			if (this.m_capType == EndCap.None)
			{
				Debug.LogError("VectorLine.SetEndCapIndex: the line \"" + this.name + "\" does not have any end caps");
				return;
			}
			if (endCap != EndCap.Front && endCap != EndCap.Back)
			{
				Debug.LogError("VectorLine.SetEndCapIndex: endCap must be EndCap.Front or EndCap.Back");
				return;
			}
			if (index < 0)
			{
				index = 0;
			}
			if (endCap == EndCap.Front)
			{
				this.m_frontEndCapIndex = index;
				return;
			}
			if (endCap == EndCap.Back)
			{
				this.m_backEndCapIndex = index;
			}
		}

		
		public void SetColor(Color32 color)
		{
			this.SetColor(color, 0, this.pointsCount);
		}

		
		public void SetColor(Color32 color, int index)
		{
			this.SetColor(color, index, index);
		}

		
		public void SetColor(Color32 color, int startIndex, int endIndex)
		{
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			int segmentNumber = this.GetSegmentNumber();
			startIndex = Mathf.Clamp(startIndex * 4, 0, segmentNumber * 4);
			endIndex = Mathf.Clamp((endIndex + 1) * 4, 0, segmentNumber * 4);
			if (!this.m_smoothColor)
			{
				for (int i = startIndex; i < endIndex; i++)
				{
					this.m_lineColors[i] = color;
				}
			}
			else
			{
				if (startIndex == 0)
				{
					this.m_lineColors[0] = color;
					this.m_lineColors[3] = color;
				}
				for (int j = startIndex; j < endIndex; j += 4)
				{
					this.m_lineColors[j + 1] = color;
					this.m_lineColors[j + 2] = color;
					if (j + 4 < this.m_vertexCount)
					{
						this.m_lineColors[j + 4] = color;
						this.m_lineColors[j + 7] = color;
					}
				}
			}
			if (this.m_capType != EndCap.None && (startIndex <= 0 || endIndex >= segmentNumber - 1))
			{
				this.SetEndCapColors();
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateColors();
			}
		}

		
		public void SetColors(List<Color32> lineColors)
		{
			if (lineColors == null)
			{
				Debug.LogError("VectorLine.SetColors: lineColors list must not be null");
				return;
			}
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			if (this.m_lineType != LineType.Points)
			{
				if (this.WrongArrayLength(lineColors.Count, VectorLine.FunctionName.SetColors))
				{
					return;
				}
			}
			else if (lineColors.Count != this.pointsCount)
			{
				Debug.LogError("VectorLine.SetColors: Length of lineColors list in \"" + this.name + "\" must be same length as points list");
				return;
			}
			int num;
			int num2;
			this.SetSegmentStartEnd(out num, out num2);
			if (num == 0 && num2 == 0)
			{
				return;
			}
			int num3 = num * 4;
			if (this.m_lineType == LineType.Points)
			{
				num2++;
			}
			if (this.smoothColor)
			{
				this.m_lineColors[num3] = lineColors[num];
				this.m_lineColors[num3 + 3] = lineColors[num];
				this.m_lineColors[num3 + 2] = lineColors[num];
				this.m_lineColors[num3 + 1] = lineColors[num];
				num3 += 4;
				for (int i = num + 1; i < num2; i++)
				{
					this.m_lineColors[num3] = lineColors[i - 1];
					this.m_lineColors[num3 + 3] = lineColors[i - 1];
					this.m_lineColors[num3 + 2] = lineColors[i];
					this.m_lineColors[num3 + 1] = lineColors[i];
					num3 += 4;
				}
			}
			else
			{
				for (int j = num; j < num2; j++)
				{
					this.m_lineColors[num3] = lineColors[j];
					this.m_lineColors[num3 + 1] = lineColors[j];
					this.m_lineColors[num3 + 2] = lineColors[j];
					this.m_lineColors[num3 + 3] = lineColors[j];
					num3 += 4;
				}
			}
			if (this.m_capType != EndCap.None)
			{
				this.SetEndCapColors();
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateColors();
			}
		}

		
		private void SetSegmentStartEnd(out int start, out int end)
		{
			start = ((this.m_lineType != LineType.Discrete) ? this.m_drawStart : (this.m_drawStart / 2));
			end = this.m_drawEnd;
			if (this.m_lineType == LineType.Discrete)
			{
				end = this.m_drawEnd / 2;
				if (this.m_drawEnd % 2 != 0)
				{
					end++;
				}
			}
		}

		
		public Color32 GetColor(int index)
		{
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			if (this.m_vertexCount == 0)
			{
				return this.m_color;
			}
			int num = index * 4 + 2;
			if (num < 0 || num >= this.m_vertexCount)
			{
				Debug.LogError("VectorLine.GetColor: index " + index + " out of range");
				return Color.clear;
			}
			return this.m_lineColors[num];
		}

		
		private void SetupWidths(int max)
		{
			if ((max >= 2 && this.m_lineWidths.Length == 1) || (max >= 2 && this.m_lineWidths.Length != max))
			{
				this.ResizeLineWidths(max);
			}
		}

		
		public void SetWidth(float width)
		{
			this.m_lineWidth = width;
			this.SetWidth(width, 0, this.pointsCount);
		}

		
		public void SetWidth(float width, int index)
		{
			this.SetWidth(width, index, index);
		}

		
		public void SetWidth(float width, int startIndex, int endIndex)
		{
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			int segmentNumber = this.GetSegmentNumber();
			this.SetupWidths(segmentNumber);
			startIndex = Mathf.Clamp(startIndex, 0, Mathf.Max(segmentNumber - 1, 0));
			endIndex = Mathf.Clamp(endIndex, 0, Mathf.Max(segmentNumber - 1, 0));
			for (int i = startIndex; i <= endIndex; i++)
			{
				this.m_lineWidths[i] = width * 0.5f;
			}
		}

		
		public void SetWidths(List<float> lineWidths)
		{
			this.SetWidths(lineWidths, null, lineWidths.Count, true);
		}

		
		public void SetWidths(List<int> lineWidths)
		{
			this.SetWidths(null, lineWidths, lineWidths.Count, false);
		}

		
		private void SetWidths(List<float> lineWidthsFloat, List<int> lineWidthsInt, int arrayLength, bool doFloat)
		{
			if ((doFloat && lineWidthsFloat == null) || (!doFloat && lineWidthsInt == null))
			{
				Debug.LogError("VectorLine.SetWidths: line widths list must not be null");
				return;
			}
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			if (this.m_lineType == LineType.Points)
			{
				if (arrayLength != this.pointsCount)
				{
					Debug.LogError("VectorLine.SetWidths: line widths list must be the same length as the points list for \"" + this.name + "\"");
					return;
				}
			}
			else if (this.WrongArrayLength(arrayLength, VectorLine.FunctionName.SetWidths))
			{
				return;
			}
			if (this.m_lineWidths.Length != arrayLength)
			{
				Array.Resize<float>(ref this.m_lineWidths, arrayLength);
			}
			if (doFloat)
			{
				for (int i = 0; i < arrayLength; i++)
				{
					this.m_lineWidths[i] = lineWidthsFloat[i] * 0.5f;
				}
				return;
			}
			for (int j = 0; j < arrayLength; j++)
			{
				this.m_lineWidths[j] = (float)lineWidthsInt[j] * 0.5f;
			}
		}

		
		public float GetWidth(int index)
		{
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			int segmentNumber = this.GetSegmentNumber();
			if (index < 0 || index >= segmentNumber)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"VectorLine.GetWidth: index ",
					index,
					" out of range...must be >= 0 and < ",
					segmentNumber
				}));
				return 0f;
			}
			if (index >= this.m_lineWidths.Length)
			{
				return this.m_lineWidth;
			}
			return this.m_lineWidths[index] * 2f;
		}

		
		public static VectorLine SetLine(Color color, params Vector2[] points)
		{
			return VectorLine.SetLine(color, 0f, points);
		}

		
		public static VectorLine SetLine(Color color, float time, params Vector2[] points)
		{
			if (points.Length < 2)
			{
				Debug.LogError("VectorLine.SetLine needs at least two points");
				return null;
			}
			VectorLine vectorLine = new VectorLine("Line", new List<Vector2>(points), null, 1f, LineType.Continuous, Joins.None);
			vectorLine.color = color;
			if (time > 0f)
			{
				VectorLine.lineManager.DisableLine(vectorLine, time);
			}
			vectorLine.Draw();
			return vectorLine;
		}

		
		public static VectorLine SetLine(Color color, params Vector3[] points)
		{
			return VectorLine.SetLine(color, 0f, points);
		}

		
		public static VectorLine SetLine(Color color, float time, params Vector3[] points)
		{
			if (points.Length < 2)
			{
				Debug.LogError("VectorLine.SetLine needs at least two points");
				return null;
			}
			VectorLine vectorLine = new VectorLine("SetLine", new List<Vector3>(points), null, 1f, LineType.Continuous, Joins.None);
			vectorLine.color = color;
			if (time > 0f)
			{
				VectorLine.lineManager.DisableLine(vectorLine, time);
			}
			vectorLine.Draw();
			return vectorLine;
		}

		
		public static VectorLine SetLine3D(Color color, params Vector3[] points)
		{
			return VectorLine.SetLine3D(color, 0f, points);
		}

		
		public static VectorLine SetLine3D(Color color, float time, params Vector3[] points)
		{
			if (points.Length < 2)
			{
				Debug.LogError("VectorLine.SetLine3D needs at least two points");
				return null;
			}
			VectorLine vectorLine = new VectorLine("SetLine3D", new List<Vector3>(points), null, 1f, LineType.Continuous, Joins.None);
			vectorLine.color = color;
			vectorLine.Draw3DAuto(time);
			return vectorLine;
		}

		
		public static VectorLine SetRay(Color color, Vector3 origin, Vector3 direction)
		{
			return VectorLine.SetRay(color, 0f, origin, direction);
		}

		
		public static VectorLine SetRay(Color color, float time, Vector3 origin, Vector3 direction)
		{
			VectorLine vectorLine = new VectorLine("SetRay", new List<Vector3>(new Vector3[]
			{
				origin,
				new Ray(origin, direction).GetPoint(direction.magnitude)
			}), null, 1f, LineType.Continuous, Joins.None);
			vectorLine.color = color;
			if (time > 0f)
			{
				VectorLine.lineManager.DisableLine(vectorLine, time);
			}
			vectorLine.Draw();
			return vectorLine;
		}

		
		public static VectorLine SetRay3D(Color color, Vector3 origin, Vector3 direction)
		{
			return VectorLine.SetRay3D(color, 0f, origin, direction);
		}

		
		public static VectorLine SetRay3D(Color color, float time, Vector3 origin, Vector3 direction)
		{
			VectorLine vectorLine = new VectorLine("SetRay3D", new List<Vector3>(new Vector3[]
			{
				origin,
				new Ray(origin, direction).GetPoint(direction.magnitude)
			}), null, 1f, LineType.Continuous, Joins.None);
			vectorLine.color = color;
			vectorLine.Draw3DAuto(time);
			return vectorLine;
		}

		
		private void CheckNormals()
		{
			if (this.m_useNormals && !this.m_normalsCalculated)
			{
				this.m_vectorObject.UpdateNormals();
				this.m_normalsCalculated = true;
			}
			if (this.m_useTangents && !this.m_tangentsCalculated)
			{
				this.m_vectorObject.UpdateTangents();
				this.m_tangentsCalculated = true;
			}
		}

		
		private void CheckLine(bool draw3D)
		{
			if (this.m_capType != EndCap.None)
			{
				this.DrawEndCap(draw3D);
			}
			if (this.m_continuousTexture)
			{
				this.SetContinuousTexture();
			}
			if (this.m_joins == Joins.Fill)
			{
				this.SetLastFillTriangles();
			}
		}

		
		private void DrawEndCap(bool draw3D)
		{
			if (this.m_capType <= EndCap.Mirror)
			{
				int num;
				if (this.m_frontEndCapIndex != -1)
				{
					num = this.m_frontEndCapIndex;
					if (this.m_lineType == LineType.Discrete && (num & 1) != 0)
					{
						num++;
					}
					num = Mathf.Clamp(num, this.drawStart, this.drawEnd) * 4;
				}
				else
				{
					num = this.m_drawStart * 4;
				}
				int num2 = (this.m_lineWidths.Length > 1) ? this.m_drawStart : 0;
				if (this.m_lineType == LineType.Discrete)
				{
					num2 /= 2;
					num /= 2;
				}
				if (!draw3D)
				{
					Vector3 vector = (this.m_lineVertices[num] - this.m_lineVertices[num + 1]).normalized * this.m_lineWidths[num2] * 2f * VectorLine.capDictionary[this.m_endCap].ratio1;
					Vector3 b = vector * VectorLine.capDictionary[this.m_endCap].offset1;
					this.m_lineVertices[this.m_vertexCount] = this.m_lineVertices[num] + vector + b;
					this.m_lineVertices[this.m_vertexCount + 3] = this.m_lineVertices[num + 3] + vector + b;
					this.m_lineVertices[num] += b;
					this.m_lineVertices[num + 3] += b;
				}
				else
				{
					Vector3 vector2 = (this.m_screenPoints[num] - this.m_screenPoints[num + 1]).normalized * this.m_lineWidths[num2] * 2f * VectorLine.capDictionary[this.m_endCap].ratio1;
					Vector3 b2 = vector2 * VectorLine.capDictionary[this.m_endCap].offset1;
					this.m_lineVertices[this.m_vertexCount] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num] + vector2 + b2);
					this.m_lineVertices[this.m_vertexCount + 3] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num + 3] + vector2 + b2);
					this.m_lineVertices[num] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num] + b2);
					this.m_lineVertices[num + 3] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num + 3] + b2);
				}
				this.m_lineVertices[this.m_vertexCount + 2] = this.m_lineVertices[num + 3];
				this.m_lineVertices[this.m_vertexCount + 1] = this.m_lineVertices[num];
				if (VectorLine.capDictionary[this.m_endCap].scale1 != 1f)
				{
					this.ScaleCapVertices(this.m_vertexCount, VectorLine.capDictionary[this.m_endCap].scale1, (this.m_lineVertices[this.m_vertexCount + 1] + this.m_lineVertices[this.m_vertexCount + 2]) / 2f);
				}
				this.m_lineTriangles[0] = this.m_vertexCount;
				this.m_lineTriangles[1] = this.m_vertexCount + 1;
				this.m_lineTriangles[2] = this.m_vertexCount + 3;
				this.m_lineTriangles[3] = this.m_vertexCount + 1;
				this.m_lineTriangles[4] = this.m_vertexCount + 2;
				this.m_lineTriangles[5] = this.m_vertexCount + 3;
			}
			if (this.m_capType >= EndCap.Both)
			{
				int num3 = this.m_drawEnd;
				if (this.m_lineType == LineType.Continuous)
				{
					if (this.m_drawEnd == this.pointsCount)
					{
						num3--;
					}
				}
				else if (num3 < this.pointsCount)
				{
					num3++;
				}
				int num;
				if (this.m_backEndCapIndex != -1)
				{
					num = this.m_backEndCapIndex;
					if (this.m_lineType == LineType.Discrete && (num & 1) != 0)
					{
						num++;
					}
					num = Mathf.Clamp(num, this.drawStart, num3) * 4;
				}
				else
				{
					num = num3 * 4;
				}
				int num4 = (this.m_lineWidths.Length > 1) ? (num3 - 1) : 0;
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (this.m_lineType == LineType.Discrete)
				{
					num4 /= 2;
					num /= 2;
				}
				if (num < 4)
				{
					num = 4;
				}
				if (!draw3D)
				{
					Vector3 vector3 = (this.m_lineVertices[num - 2] - this.m_lineVertices[num - 1]).normalized * this.m_lineWidths[num4] * 2f * VectorLine.capDictionary[this.m_endCap].ratio2;
					Vector3 b3 = vector3 * VectorLine.capDictionary[this.m_endCap].offset2;
					this.m_lineVertices[this.m_vertexCount + 6] = this.m_lineVertices[num - 2] + vector3 + b3;
					this.m_lineVertices[this.m_vertexCount + 5] = this.m_lineVertices[num - 3] + vector3 + b3;
					this.m_lineVertices[num - 3] += b3;
					this.m_lineVertices[num - 2] += b3;
				}
				else
				{
					Vector3 vector4 = (this.m_screenPoints[num - 2] - this.m_screenPoints[num - 1]).normalized * this.m_lineWidths[num4] * 2f * VectorLine.capDictionary[this.m_endCap].ratio2;
					Vector3 b4 = vector4 * VectorLine.capDictionary[this.m_endCap].offset2;
					this.m_lineVertices[this.m_vertexCount + 6] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num - 2] + vector4 + b4);
					this.m_lineVertices[this.m_vertexCount + 5] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num - 3] + vector4 + b4);
					this.m_lineVertices[num - 3] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num - 3] + b4);
					this.m_lineVertices[num - 2] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num - 2] + b4);
				}
				this.m_lineVertices[this.m_vertexCount + 4] = this.m_lineVertices[num - 3];
				this.m_lineVertices[this.m_vertexCount + 7] = this.m_lineVertices[num - 2];
				if (VectorLine.capDictionary[this.m_endCap].scale2 != 1f)
				{
					this.ScaleCapVertices(this.m_vertexCount + 4, VectorLine.capDictionary[this.m_endCap].scale2, (this.m_lineVertices[this.m_vertexCount + 4] + this.m_lineVertices[this.m_vertexCount + 7]) / 2f);
				}
				this.m_lineTriangles[6] = this.m_vertexCount + 4;
				this.m_lineTriangles[7] = this.m_vertexCount + 5;
				this.m_lineTriangles[8] = this.m_vertexCount + 7;
				this.m_lineTriangles[9] = this.m_vertexCount + 5;
				this.m_lineTriangles[10] = this.m_vertexCount + 6;
				this.m_lineTriangles[11] = this.m_vertexCount + 7;
			}
			if (this.m_drawStart > 0 || this.m_drawEnd < this.pointsCount)
			{
				this.SetEndCapColors();
			}
		}

		
		private void ScaleCapVertices(int offset, float scale, Vector3 center)
		{
			this.m_lineVertices[offset] = (this.m_lineVertices[offset] - center) * scale + center;
			this.m_lineVertices[offset + 1] = (this.m_lineVertices[offset + 1] - center) * scale + center;
			this.m_lineVertices[offset + 2] = (this.m_lineVertices[offset + 2] - center) * scale + center;
			this.m_lineVertices[offset + 3] = (this.m_lineVertices[offset + 3] - center) * scale + center;
		}

		
		private void SetContinuousTexture()
		{
			int num = 0;
			float x = 0f;
			this.SetDistances();
			int num2 = this.m_distances.Length - 1;
			float num3 = this.m_distances[num2];
			for (int i = 0; i < num2; i++)
			{
				this.m_lineUVs[num].x = x;
				this.m_lineUVs[num + 3].x = x;
				x = 1f / (num3 / this.m_distances[i + 1]);
				this.m_lineUVs[num + 1].x = x;
				this.m_lineUVs[num + 2].x = x;
				num += 4;
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateUVs();
			}
		}

		
		private bool UseMatrix(out Matrix4x4 thisMatrix)
		{
			if (this.m_drawTransform != null)
			{
				thisMatrix = this.m_drawTransform.localToWorldMatrix;
				return true;
			}
			if (this.m_useMatrix)
			{
				thisMatrix = this.m_matrix;
				return true;
			}
			thisMatrix = Matrix4x4.identity;
			return false;
		}

		
		private bool CheckPointCount()
		{
			if (this.pointsCount < ((this.m_lineType == LineType.Points) ? 1 : 2))
			{
				this.ClearTriangles();
				this.m_vectorObject.ClearMesh();
				this.m_pointsCount = this.pointsCount;
				this.m_drawEnd = 0;
				return false;
			}
			return true;
		}

		
		private void ClearTriangles()
		{
			if (this.m_capType == EndCap.None)
			{
				this.m_lineTriangles.Clear();
				return;
			}
			this.m_lineTriangles.RemoveRange(12, this.m_lineTriangles.Count - 12);
		}

		
		private void SetupDrawStartEnd(out int start, out int end, bool clearVertices)
		{
			start = 0;
			end = this.m_pointsCount - 1;
			if (this.m_drawStart > 0)
			{
				start = this.m_drawStart;
				if (this.m_lineType == LineType.Discrete && start == this.pointsCount - 1)
				{
					start++;
				}
				if (clearVertices)
				{
					this.ZeroVertices(0, start);
				}
			}
			if (this.m_drawEnd < this.m_pointsCount - 1)
			{
				end = this.m_drawEnd;
				if (end < 0)
				{
					end = 0;
				}
				if (clearVertices)
				{
					this.ZeroVertices(end, this.m_pointsCount);
				}
			}
			if (this.m_endPointsUpdate > 0)
			{
				start = Mathf.Max(0, end - this.m_endPointsUpdate);
			}
		}

		
		private void ZeroVertices(int startIndex, int endIndex)
		{
			if (this.m_lineType != LineType.Discrete)
			{
				startIndex *= 4;
				endIndex *= 4;
				if (endIndex > this.m_vertexCount)
				{
					endIndex -= 4;
				}
				for (int i = startIndex; i < endIndex; i += 4)
				{
					this.m_lineVertices[i] = VectorLine.v3zero;
					this.m_lineVertices[i + 1] = VectorLine.v3zero;
					this.m_lineVertices[i + 2] = VectorLine.v3zero;
					this.m_lineVertices[i + 3] = VectorLine.v3zero;
				}
				return;
			}
			startIndex *= 2;
			endIndex *= 2;
			for (int j = startIndex; j < endIndex; j += 2)
			{
				this.m_lineVertices[j] = VectorLine.v3zero;
				this.m_lineVertices[j + 1] = VectorLine.v3zero;
			}
		}

		
		private void SetupCanvasState(CanvasState wantedState)
		{
			if (wantedState == CanvasState.OnCanvas)
			{
				if (this.m_go == null)
				{
					return;
				}
				Transform parent = this.m_go.transform.parent;
				bool flag = true;
				while (parent != null)
				{
					if (parent.GetComponent<Canvas>() != null)
					{
						flag = false;
						break;
					}
					parent = parent.parent;
				}
				if (flag)
				{
					if (VectorLine.m_canvas == null)
					{
						VectorLine.SetupVectorCanvas();
					}
					this.m_go.transform.SetParent(VectorLine.m_canvas.transform, true);
				}
				this.m_canvasState = CanvasState.OnCanvas;
				if (this.m_go.GetComponent<VectorObject3D>() != null)
				{
					UnityEngine.Object.DestroyImmediate(this.m_go.GetComponent<VectorObject3D>());
					UnityEngine.Object.DestroyImmediate(this.m_go.GetComponent<MeshFilter>());
					UnityEngine.Object.DestroyImmediate(this.m_go.GetComponent<MeshRenderer>());
				}
				if (this.m_go.GetComponent<VectorObject2D>() == null)
				{
					this.m_vectorObject = this.m_go.AddComponent<VectorObject2D>();
				}
				else
				{
					this.m_vectorObject = this.m_go.GetComponent<VectorObject2D>();
				}
				this.m_vectorObject.SetVectorLine(this, this.m_texture, this.m_material, false);
				return;
			}
			else
			{
				if (this.m_go == null)
				{
					return;
				}
				this.m_go.transform.SetParent(null);
				this.m_canvasState = CanvasState.OffCanvas;
				if (this.m_go.GetComponent<VectorObject2D>() != null)
				{
					this.m_go.GetComponent<VectorObject2D>().DestroyNow();
					UnityEngine.Object.DestroyImmediate(this.m_go.GetComponent<VectorObject2D>());
				}
				this.m_vectorObject = this.m_go.GetComponent<VectorObject3D>();
				if (this.m_vectorObject == null)
				{
					this.m_vectorObject = this.m_go.AddComponent<VectorObject3D>();
				}
				bool useCustomMaterial = true;
				if (this.m_material == null)
				{
					Material material = Resources.Load("DefaultLine3D") as Material;
					if (material == null)
					{
						Debug.LogError("No DefaultLine3D material found in Resources");
						return;
					}
					this.m_material = new Material(material);
					useCustomMaterial = false;
				}
				this.m_vectorObject.SetVectorLine(this, this.m_texture, this.m_material, useCustomMaterial);
				return;
			}
		}

		
		public void Draw()
		{
			if (!this.m_active)
			{
				return;
			}
			if (this.m_canvasState != CanvasState.OnCanvas)
			{
				this.SetupCanvasState(CanvasState.OnCanvas);
			}
			if (this.m_vectorObject == null)
			{
				this.m_vectorObject = this.m_go.GetComponent<VectorObject2D>();
			}
			if (!this.CheckPointCount() || this.m_lineWidths == null)
			{
				return;
			}
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			if (this.m_lineType == LineType.Points)
			{
				this.DrawPoints();
				return;
			}
			Matrix4x4 thisMatrix;
			bool useTransformMatrix = this.UseMatrix(out thisMatrix);
			int start = 0;
			int end = 0;
			this.SetupDrawStartEnd(out start, out end, true);
			if (this.m_is2D)
			{
				this.Line2D(start, end, thisMatrix, useTransformMatrix);
			}
			else
			{
				this.Line3D(start, end, thisMatrix, useTransformMatrix);
			}
			this.CheckNormals();
			this.CheckLine(false);
			if (this.m_useTextureScale)
			{
				this.SetTextureScale();
			}
			this.m_vectorObject.UpdateVerts();
			if (this.m_collider)
			{
				this.SetCollider(true);
			}
		}

		
		private void Line2D(int start, int end, Matrix4x4 thisMatrix, bool useTransformMatrix)
		{
			Vector3 vector = VectorLine.v3zero;
			Vector3 vector2 = VectorLine.v3zero;
			Vector3 a = VectorLine.v3zero;
			Vector3 vector3 = VectorLine.v3zero;
			Vector2 vector4 = new Vector2((float)Screen.width, (float)Screen.height);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (this.m_lineWidths.Length > 1)
			{
				num2 = start;
				num3 = 1;
			}
			int num4;
			if (this.m_lineType == LineType.Continuous)
			{
				num4 = 1;
				num = start * 4;
			}
			else
			{
				num4 = 2;
				num2 /= 2;
				num = start * 2;
			}
			bool flag = this.smoothWidth && this.m_lineWidths.Length > 1;
			for (int i = start; i < end; i += num4)
			{
				if (useTransformMatrix)
				{
					vector = thisMatrix.MultiplyPoint3x4(this.m_points2[i]);
					vector2 = thisMatrix.MultiplyPoint3x4(this.m_points2[i + 1]);
				}
				else
				{
					vector.x = this.m_points2[i].x;
					vector.y = this.m_points2[i].y;
					vector2.x = this.m_points2[i + 1].x;
					vector2.y = this.m_points2[i + 1].y;
				}
				if (this.m_viewportDraw)
				{
					vector.x *= vector4.x;
					vector.y *= vector4.y;
					vector2.x *= vector4.x;
					vector2.y *= vector4.y;
				}
				if (vector.x == vector2.x && vector.y == vector2.y)
				{
					this.SkipQuad(ref num, ref num2, ref num3);
				}
				else
				{
					if (this.m_capLength == 0f)
					{
						vector3.x = vector2.y - vector.y;
						vector3.y = vector.x - vector2.x;
						float num5 = 1f / (float)Math.Sqrt((double)(vector3.x * vector3.x + vector3.y * vector3.y));
						vector3 *= num5 * this.m_lineWidths[num2];
						this.m_lineVertices[num].x = vector.x - vector3.x;
						this.m_lineVertices[num].y = vector.y - vector3.y;
						this.m_lineVertices[num + 3].x = vector.x + vector3.x;
						this.m_lineVertices[num + 3].y = vector.y + vector3.y;
						if (flag && i < end - num4)
						{
							vector3.x = vector2.y - vector.y;
							vector3.y = vector.x - vector2.x;
							vector3 *= num5 * this.m_lineWidths[num2 + 1];
						}
					}
					else
					{
						vector3.x = vector2.x - vector.x;
						vector3.y = vector2.y - vector.y;
						vector3 *= 1f / (float)Math.Sqrt((double)(vector3.x * vector3.x + vector3.y * vector3.y));
						vector -= vector3 * this.m_capLength;
						vector2 += vector3 * this.m_capLength;
						a.x = vector3.y;
						a.y = -vector3.x;
						vector3 = a * this.m_lineWidths[num2];
						this.m_lineVertices[num].x = vector.x - vector3.x;
						this.m_lineVertices[num].y = vector.y - vector3.y;
						this.m_lineVertices[num + 3].x = vector.x + vector3.x;
						this.m_lineVertices[num + 3].y = vector.y + vector3.y;
						if (flag && i < end - num4)
						{
							vector3 = a * this.m_lineWidths[num2 + 1];
						}
					}
					this.m_lineVertices[num + 2].x = vector2.x + vector3.x;
					this.m_lineVertices[num + 2].y = vector2.y + vector3.y;
					this.m_lineVertices[num + 1].x = vector2.x - vector3.x;
					this.m_lineVertices[num + 1].y = vector2.y - vector3.y;
					num += 4;
					num2 += num3;
				}
			}
			if (this.m_joins == Joins.Weld)
			{
				if (this.m_lineType == LineType.Continuous)
				{
					this.WeldJoins(start * 4 + ((start == 0) ? 4 : 0), end * 4, this.Approximately(this.m_points2[0], this.m_points2[this.m_pointsCount - 1]));
				}
				else
				{
					if ((end & 1) == 0)
					{
						end--;
					}
					this.WeldJoinsDiscrete(start + 1, end, this.Approximately(this.m_points2[0], this.m_points2[this.m_pointsCount - 1]));
				}
			}
			this.CheckDrawStartFill(start);
		}

		
		private void Line3D(int start, int end, Matrix4x4 thisMatrix, bool useTransformMatrix)
		{
			if (!this.CheckCamera3D())
			{
				return;
			}
			Vector3 vector = VectorLine.v3zero;
			Vector3 vector2 = VectorLine.v3zero;
			Vector3 a = VectorLine.v3zero;
			Vector3 vector3 = VectorLine.v3zero;
			Vector3 position = VectorLine.v3zero;
			Vector3 position2 = VectorLine.v3zero;
			int num = 0;
			int num2 = 0;
			if (this.m_lineWidths.Length > 1)
			{
				num = start;
				num2 = 1;
			}
			int num3 = start * 2;
			int num4 = 2;
			if (this.m_lineType == LineType.Continuous)
			{
				num3 = start * 4;
				num4 = 1;
			}
			Plane plane = new Plane(VectorLine.camTransform.forward, VectorLine.camTransform.position + VectorLine.camTransform.forward * VectorLine.cam3D.nearClipPlane);
			Ray ray = new Ray(VectorLine.v3zero, VectorLine.v3zero);
			float num5 = (float)Screen.height;
			bool flag = this.smoothWidth && this.m_lineWidths.Length > 1;
			for (int i = start; i < end; i += num4)
			{
				if (useTransformMatrix)
				{
					position = thisMatrix.MultiplyPoint3x4(this.m_points3[i]);
					position2 = thisMatrix.MultiplyPoint3x4(this.m_points3[i + 1]);
				}
				else
				{
					position = this.m_points3[i];
					position2 = this.m_points3[i + 1];
				}
				vector = VectorLine.cam3D.WorldToScreenPoint(position);
				vector2 = VectorLine.cam3D.WorldToScreenPoint(position2);
				if ((vector.x == vector2.x && vector.y == vector2.y) || this.IntersectAndDoSkip(ref vector, ref vector2, ref position, ref position2, ref num5, ref ray, ref plane))
				{
					this.SkipQuad(ref num3, ref num, ref num2);
				}
				else
				{
					if (this.m_capLength == 0f)
					{
						vector3.x = vector2.y - vector.y;
						vector3.y = vector.x - vector2.x;
						float num6 = 1f / (float)Math.Sqrt((double)(vector3.x * vector3.x + vector3.y * vector3.y));
						vector3.x *= num6 * this.m_lineWidths[num];
						vector3.y *= num6 * this.m_lineWidths[num];
						this.m_lineVertices[num3].x = vector.x - vector3.x;
						this.m_lineVertices[num3].y = vector.y - vector3.y;
						this.m_lineVertices[num3 + 3].x = vector.x + vector3.x;
						this.m_lineVertices[num3 + 3].y = vector.y + vector3.y;
						if (flag && i < end - num4)
						{
							vector3.x = vector2.y - vector.y;
							vector3.y = vector.x - vector2.x;
							vector3.x *= num6 * this.m_lineWidths[num + 1];
							vector3.y *= num6 * this.m_lineWidths[num + 1];
						}
					}
					else
					{
						vector3.x = vector2.x - vector.x;
						vector3.y = vector2.y - vector.y;
						vector3 *= 1f / (float)Math.Sqrt((double)(vector3.x * vector3.x + vector3.y * vector3.y));
						vector -= vector3 * this.m_capLength;
						vector2 += vector3 * this.m_capLength;
						a.x = vector3.y;
						a.y = -vector3.x;
						vector3 = a * this.m_lineWidths[num];
						this.m_lineVertices[num3].x = vector.x - vector3.x;
						this.m_lineVertices[num3].y = vector.y - vector3.y;
						this.m_lineVertices[num3 + 3].x = vector.x + vector3.x;
						this.m_lineVertices[num3 + 3].y = vector.y + vector3.y;
						if (flag && i < end - num4)
						{
							vector3 = a * this.m_lineWidths[num + 1];
						}
					}
					this.m_lineVertices[num3 + 2].x = vector2.x + vector3.x;
					this.m_lineVertices[num3 + 2].y = vector2.y + vector3.y;
					this.m_lineVertices[num3 + 1].x = vector2.x - vector3.x;
					this.m_lineVertices[num3 + 1].y = vector2.y - vector3.y;
					num3 += 4;
					num += num2;
				}
			}
			if (this.m_joins == Joins.Weld && end - start > 1)
			{
				if (this.m_lineType == LineType.Continuous)
				{
					this.WeldJoins(start * 4 + ((start == 0) ? 4 : 0), end * 4, start == 0 && end == this.m_pointsCount - 1 && this.Approximately(this.m_points3[0], this.m_points3[this.m_pointsCount - 1]));
				}
				else
				{
					if ((end & 1) == 0)
					{
						end--;
					}
					this.WeldJoinsDiscrete(start + 1, end, start == 0 && end == this.m_pointsCount - 1 && this.Approximately(this.m_points3[0], this.m_points3[this.m_pointsCount - 1]));
				}
			}
			this.CheckDrawStartFill(start);
		}

		
		private void CheckDrawStartFill(int start)
		{
			if (this.m_joins == Joins.Fill)
			{
				int num = start * 4;
				if (this.m_drawStart > 0 && this.m_lineVertices.Length > num && num - 3 >= 0)
				{
					this.m_lineVertices[num - 1] = this.m_lineVertices[num];
					this.m_lineVertices[num - 2] = this.m_lineVertices[num];
					this.m_lineVertices[num - 3] = this.m_lineVertices[num];
				}
			}
		}

		
		public void Draw3D()
		{
			if (!this.m_active)
			{
				return;
			}
			if (this.m_is2D)
			{
				Debug.LogError("VectorLine.Draw3D can only be used with a Vector3 array, which \"" + this.name + "\" doesn't have");
				return;
			}
			if (this.m_canvasState != CanvasState.OffCanvas)
			{
				this.SetupCanvasState(CanvasState.OffCanvas);
			}
			if (!this.CheckPointCount() || this.m_lineWidths == null)
			{
				return;
			}
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			if (!this.CheckCamera3D())
			{
				return;
			}
			if (this.m_lineType == LineType.Points)
			{
				this.DrawPoints3D();
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.SetupDrawStartEnd(out num, out num2, true);
			Matrix4x4 matrix4x;
			bool flag = this.UseMatrix(out matrix4x);
			bool flag2 = this.smoothWidth && this.m_lineWidths.Length > 1;
			int num4 = 0;
			int num5 = 0;
			if (this.m_lineWidths.Length > 1)
			{
				num3 = num;
				num5 = 1;
			}
			int num6;
			if (this.m_lineType == LineType.Continuous)
			{
				num6 = 1;
				num4 = num * 4;
			}
			else
			{
				num3 /= 2;
				num6 = 2;
				num4 = num * 2;
			}
			Vector3 vector = VectorLine.v3zero;
			Vector3 vector2 = VectorLine.v3zero;
			Vector3 vector3 = VectorLine.v3zero;
			Vector3 vector4 = VectorLine.v3zero;
			Vector3 position = VectorLine.v3zero;
			Vector3 position2 = VectorLine.v3zero;
			Plane plane = new Plane(VectorLine.camTransform.forward, VectorLine.camTransform.position + VectorLine.camTransform.forward * VectorLine.cam3D.nearClipPlane);
			Ray ray = new Ray(VectorLine.v3zero, VectorLine.v3zero);
			float num7 = (float)Screen.height;
			for (int i = num; i < num2; i += num6)
			{
				if (flag)
				{
					position = matrix4x.MultiplyPoint3x4(this.m_points3[i]);
					position2 = matrix4x.MultiplyPoint3x4(this.m_points3[i + 1]);
				}
				else
				{
					position = this.m_points3[i];
					position2 = this.m_points3[i + 1];
				}
				vector3 = VectorLine.cam3D.WorldToScreenPoint(position);
				vector4 = VectorLine.cam3D.WorldToScreenPoint(position2);
				if ((vector3.x == vector4.x && vector3.y == vector4.y) || this.IntersectAndDoSkip(ref vector3, ref vector4, ref position, ref position2, ref num7, ref ray, ref plane))
				{
					this.SkipQuad3D(ref num4, ref num3, ref num5);
				}
				else
				{
					vector2.x = vector4.y - vector3.y;
					vector2.y = vector3.x - vector4.x;
					vector = vector2 / (float)Math.Sqrt((double)(vector2.x * vector2.x + vector2.y * vector2.y));
					vector2.x = vector.x * this.m_lineWidths[num3];
					vector2.y = vector.y * this.m_lineWidths[num3];
					this.m_screenPoints[num4].x = vector3.x - vector2.x;
					this.m_screenPoints[num4].y = vector3.y - vector2.y;
					this.m_screenPoints[num4].z = vector3.z - vector2.z;
					this.m_screenPoints[num4 + 3].x = vector3.x + vector2.x;
					this.m_screenPoints[num4 + 3].y = vector3.y + vector2.y;
					this.m_screenPoints[num4 + 3].z = vector3.z + vector2.z;
					this.m_lineVertices[num4] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num4]);
					this.m_lineVertices[num4 + 3] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num4 + 3]);
					if (flag2 && i < num2 - num6)
					{
						vector2.x = vector.x * this.m_lineWidths[num3 + 1];
						vector2.y = vector.y * this.m_lineWidths[num3 + 1];
					}
					this.m_screenPoints[num4 + 2].x = vector4.x + vector2.x;
					this.m_screenPoints[num4 + 2].y = vector4.y + vector2.y;
					this.m_screenPoints[num4 + 2].z = vector4.z + vector2.z;
					this.m_screenPoints[num4 + 1].x = vector4.x - vector2.x;
					this.m_screenPoints[num4 + 1].y = vector4.y - vector2.y;
					this.m_screenPoints[num4 + 1].z = vector4.z - vector2.z;
					this.m_lineVertices[num4 + 2] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num4 + 2]);
					this.m_lineVertices[num4 + 1] = VectorLine.cam3D.ScreenToWorldPoint(this.m_screenPoints[num4 + 1]);
					num4 += 4;
					num3 += num5;
				}
			}
			if (this.m_joins == Joins.Weld && num2 - num > 1)
			{
				if (this.m_lineType == LineType.Continuous)
				{
					this.WeldJoins3D(num * 4 + ((num == 0) ? 4 : 0), num2 * 4, num == 0 && num2 == this.m_pointsCount - 1 && this.Approximately(this.m_points3[0], this.m_points3[this.m_pointsCount - 1]));
				}
				else
				{
					if ((num2 & 1) == 0)
					{
						num2--;
					}
					this.WeldJoinsDiscrete3D(num + 1, num2, num == 0 && num2 == this.m_pointsCount - 1 && this.Approximately(this.m_points3[0], this.m_points3[this.m_pointsCount - 1]));
				}
			}
			this.CheckDrawStartFill(num);
			this.CheckLine(true);
			if (this.m_useTextureScale)
			{
				this.SetTextureScale();
			}
			this.m_vectorObject.UpdateVerts();
			this.CheckNormals();
			if (this.m_collider)
			{
				this.SetCollider(false);
			}
		}

		
		private bool IntersectAndDoSkip(ref Vector3 pos1, ref Vector3 pos2, ref Vector3 p1, ref Vector3 p2, ref float screenHeight, ref Ray ray, ref Plane cameraPlane)
		{
			if (pos1.z < 0f)
			{
				if (pos2.z < 0f)
				{
					return true;
				}
				pos1 = VectorLine.cam3D.WorldToScreenPoint(this.PlaneIntersectionPoint(ref ray, ref cameraPlane, ref p2, ref p1));
				Vector3 vector = VectorLine.camTransform.InverseTransformPoint(p1);
				if ((vector.y < -1f && pos1.y > screenHeight) || (vector.y > 1f && pos1.y < 0f))
				{
					return true;
				}
			}
			if (pos2.z < 0f)
			{
				pos2 = VectorLine.cam3D.WorldToScreenPoint(this.PlaneIntersectionPoint(ref ray, ref cameraPlane, ref p1, ref p2));
				Vector3 vector2 = VectorLine.camTransform.InverseTransformPoint(p2);
				if ((vector2.y < -1f && pos2.y > screenHeight) || (vector2.y > 1f && pos2.y < 0f))
				{
					return true;
				}
			}
			return false;
		}

		
		private Vector3 PlaneIntersectionPoint(ref Ray ray, ref Plane plane, ref Vector3 p1, ref Vector3 p2)
		{
			ray.origin = p1;
			ray.direction = p2 - p1;
			float distance = 0f;
			plane.Raycast(ray, out distance);
			return ray.GetPoint(distance);
		}

		
		private void DrawPoints()
		{
			if (!this.CheckCamera3D())
			{
				return;
			}
			Matrix4x4 matrix4x;
			bool flag = this.UseMatrix(out matrix4x);
			int num;
			int num2;
			this.SetupDrawStartEnd(out num, out num2, true);
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			int num3 = num * 4;
			int num4 = (this.m_lineWidths.Length > 1) ? 1 : 0;
			int num5 = num;
			Vector3 vector2 = new Vector3(this.m_lineWidths[0], this.m_lineWidths[0], 0f);
			Vector3 vector3 = new Vector3(-this.m_lineWidths[0], this.m_lineWidths[0], 0f);
			if (this.m_is2D)
			{
				for (int i = num; i <= num2; i++)
				{
					Vector3 vector4;
					if (flag)
					{
						vector4 = matrix4x.MultiplyPoint3x4(this.m_points2[i]);
					}
					else
					{
						vector4.x = this.m_points2[i].x;
						vector4.y = this.m_points2[i].y;
					}
					if (this.m_viewportDraw)
					{
						vector4.x *= vector.x;
						vector4.y *= vector.y;
					}
					if (num4 != 0)
					{
						vector2.x = (vector2.y = (vector3.y = this.m_lineWidths[num5]));
						vector3.x = -this.m_lineWidths[num5];
						num5++;
					}
					this.m_lineVertices[num3].x = vector4.x + vector3.x;
					this.m_lineVertices[num3].y = vector4.y + vector3.y;
					this.m_lineVertices[num3 + 3].x = vector4.x - vector2.x;
					this.m_lineVertices[num3 + 3].y = vector4.y - vector2.y;
					this.m_lineVertices[num3 + 1].x = vector4.x + vector2.x;
					this.m_lineVertices[num3 + 1].y = vector4.y + vector2.y;
					this.m_lineVertices[num3 + 2].x = vector4.x - vector3.x;
					this.m_lineVertices[num3 + 2].y = vector4.y - vector3.y;
					num3 += 4;
				}
			}
			else
			{
				for (int j = num; j <= num2; j++)
				{
					Vector3 vector4 = flag ? VectorLine.cam3D.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[j])) : VectorLine.cam3D.WorldToScreenPoint(this.m_points3[j]);
					if (vector4.z < 0f)
					{
						this.SkipQuad(ref num3, ref num5, ref num4);
					}
					else
					{
						if (num4 != 0)
						{
							vector2.x = (vector2.y = (vector3.y = this.m_lineWidths[num5]));
							vector3.x = -this.m_lineWidths[num5];
							num5++;
						}
						this.m_lineVertices[num3].x = vector4.x + vector3.x;
						this.m_lineVertices[num3].y = vector4.y + vector3.y;
						this.m_lineVertices[num3 + 3].x = vector4.x - vector2.x;
						this.m_lineVertices[num3 + 3].y = vector4.y - vector2.y;
						this.m_lineVertices[num3 + 1].x = vector4.x + vector2.x;
						this.m_lineVertices[num3 + 1].y = vector4.y + vector2.y;
						this.m_lineVertices[num3 + 2].x = vector4.x - vector3.x;
						this.m_lineVertices[num3 + 2].y = vector4.y - vector3.y;
						num3 += 4;
					}
				}
			}
			this.CheckNormals();
			this.m_vectorObject.UpdateVerts();
		}

		
		private void DrawPoints3D()
		{
			if (!this.m_active)
			{
				return;
			}
			Matrix4x4 matrix4x;
			bool flag = this.UseMatrix(out matrix4x);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.SetupDrawStartEnd(out num, out num2, true);
			int num4 = num * 4;
			int num5 = 0;
			if (this.m_lineWidths.Length > 1)
			{
				num3 = num;
				num5 = 1;
			}
			Vector3 vector = VectorLine.v3zero;
			Vector3 b = VectorLine.v3zero;
			Vector3 b2 = VectorLine.v3zero;
			for (int i = num; i <= num2; i++)
			{
				vector = (flag ? VectorLine.cam3D.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[i])) : VectorLine.cam3D.WorldToScreenPoint(this.m_points3[i]));
				if (vector.z < 0f)
				{
					this.SkipQuad(ref num4, ref num3, ref num5);
				}
				else
				{
					b.x = (b.y = (b2.y = this.m_lineWidths[num3]));
					b2.x = -this.m_lineWidths[num3];
					this.m_lineVertices[num4] = VectorLine.cam3D.ScreenToWorldPoint(vector + b2);
					this.m_lineVertices[num4 + 3] = VectorLine.cam3D.ScreenToWorldPoint(vector - b);
					this.m_lineVertices[num4 + 1] = VectorLine.cam3D.ScreenToWorldPoint(vector + b);
					this.m_lineVertices[num4 + 2] = VectorLine.cam3D.ScreenToWorldPoint(vector - b2);
					num4 += 4;
					num3 += num5;
				}
			}
			this.CheckNormals();
			this.m_vectorObject.UpdateVerts();
		}

		
		private void SkipQuad(ref int idx, ref int widthIdx, ref int widthIdxAdd)
		{
			this.m_lineVertices[idx] = VectorLine.v3zero;
			this.m_lineVertices[idx + 1] = VectorLine.v3zero;
			this.m_lineVertices[idx + 2] = VectorLine.v3zero;
			this.m_lineVertices[idx + 3] = VectorLine.v3zero;
			idx += 4;
			widthIdx += widthIdxAdd;
		}

		
		private void SkipQuad3D(ref int idx, ref int widthIdx, ref int widthIdxAdd)
		{
			this.m_lineVertices[idx] = VectorLine.v3zero;
			this.m_lineVertices[idx + 1] = VectorLine.v3zero;
			this.m_lineVertices[idx + 2] = VectorLine.v3zero;
			this.m_lineVertices[idx + 3] = VectorLine.v3zero;
			this.m_screenPoints[idx] = VectorLine.v3zero;
			this.m_screenPoints[idx + 1] = VectorLine.v3zero;
			this.m_screenPoints[idx + 2] = VectorLine.v3zero;
			this.m_screenPoints[idx + 3] = VectorLine.v3zero;
			idx += 4;
			widthIdx += widthIdxAdd;
		}

		
		private void WeldJoins(int start, int end, bool connectFirstAndLast)
		{
			if (connectFirstAndLast)
			{
				this.SetIntersectionPoint(this.m_vertexCount - 4, this.m_vertexCount - 3, 0, 1);
				this.SetIntersectionPoint(this.m_vertexCount - 1, this.m_vertexCount - 2, 3, 2);
			}
			for (int i = start; i < end; i += 4)
			{
				this.SetIntersectionPoint(i - 4, i - 3, i, i + 1);
				this.SetIntersectionPoint(i - 1, i - 2, i + 3, i + 2);
			}
		}

		
		private void WeldJoinsDiscrete(int start, int end, bool connectFirstAndLast)
		{
			if (connectFirstAndLast)
			{
				this.SetIntersectionPoint(this.m_vertexCount - 4, this.m_vertexCount - 3, 0, 1);
				this.SetIntersectionPoint(this.m_vertexCount - 1, this.m_vertexCount - 2, 3, 2);
			}
			int num = (start + 1) / 2 * 4;
			if (this.m_is2D)
			{
				for (int i = start; i < end; i += 2)
				{
					if (this.m_points2[i] == this.m_points2[i + 1])
					{
						this.SetIntersectionPoint(num - 4, num - 3, num, num + 1);
						this.SetIntersectionPoint(num - 1, num - 2, num + 3, num + 2);
					}
					num += 4;
				}
				return;
			}
			for (int j = start; j < end; j += 2)
			{
				if (this.m_points3[j] == this.m_points3[j + 1])
				{
					this.SetIntersectionPoint(num - 4, num - 3, num, num + 1);
					this.SetIntersectionPoint(num - 1, num - 2, num + 3, num + 2);
				}
				num += 4;
			}
		}

		
		private void SetIntersectionPoint(int p1, int p2, int p3, int p4)
		{
			Vector3 vector = this.m_lineVertices[p1];
			Vector3 vector2 = this.m_lineVertices[p2];
			Vector3 vector3 = this.m_lineVertices[p3];
			Vector3 vector4 = this.m_lineVertices[p4];
			if ((vector.x == vector2.x && vector.y == vector2.y) || (vector3.x == vector4.x && vector3.y == vector4.y))
			{
				return;
			}
			float num = (vector4.y - vector3.y) * (vector2.x - vector.x) - (vector4.x - vector3.x) * (vector2.y - vector.y);
			if (num > -0.005f && num < 0.005f)
			{
				if (Mathf.Abs(vector2.x - vector3.x) < 0.005f && Mathf.Abs(vector2.y - vector3.y) < 0.005f)
				{
					this.m_lineVertices[p2] = (vector2 + vector3) * 0.5f;
					this.m_lineVertices[p3] = this.m_lineVertices[p2];
				}
				return;
			}
			float num2 = ((vector4.x - vector3.x) * (vector.y - vector3.y) - (vector4.y - vector3.y) * (vector.x - vector3.x)) / num;
			Vector3 vector5 = new Vector3(vector.x + num2 * (vector2.x - vector.x), vector.y + num2 * (vector2.y - vector.y), vector.z);
			if ((vector5 - vector2).sqrMagnitude > this.m_maxWeldDistance)
			{
				return;
			}
			this.m_lineVertices[p2] = vector5;
			this.m_lineVertices[p3] = vector5;
		}

		
		private void WeldJoins3D(int start, int end, bool connectFirstAndLast)
		{
			if (connectFirstAndLast)
			{
				this.SetIntersectionPoint3D(this.m_vertexCount - 4, this.m_vertexCount - 3, 0, 1);
				this.SetIntersectionPoint3D(this.m_vertexCount - 1, this.m_vertexCount - 2, 3, 2);
			}
			if (this.m_drawStart > 0)
			{
				start += 4;
			}
			for (int i = start; i < end; i += 4)
			{
				this.SetIntersectionPoint3D(i - 4, i - 3, i, i + 1);
				this.SetIntersectionPoint3D(i - 1, i - 2, i + 3, i + 2);
			}
		}

		
		private void WeldJoinsDiscrete3D(int start, int end, bool connectFirstAndLast)
		{
			if (connectFirstAndLast)
			{
				this.SetIntersectionPoint3D(this.m_vertexCount - 4, this.m_vertexCount - 3, 0, 1);
				this.SetIntersectionPoint3D(this.m_vertexCount - 1, this.m_vertexCount - 2, 3, 2);
			}
			int num = (start + 1) / 2 * 4;
			for (int i = start; i < end; i += 2)
			{
				if (this.m_points3[i] == this.m_points3[i + 1])
				{
					this.SetIntersectionPoint3D(num - 4, num - 3, num, num + 1);
					this.SetIntersectionPoint3D(num - 1, num - 2, num + 3, num + 2);
				}
				num += 4;
			}
		}

		
		private void SetIntersectionPoint3D(int p1, int p2, int p3, int p4)
		{
			Vector3 vector = this.m_screenPoints[p1];
			Vector3 vector2 = this.m_screenPoints[p2];
			Vector3 vector3 = this.m_screenPoints[p3];
			Vector3 vector4 = this.m_screenPoints[p4];
			if ((vector.x == vector2.x && vector.y == vector2.y) || (vector3.x == vector4.x && vector3.y == vector4.y))
			{
				return;
			}
			float num = (vector4.y - vector3.y) * (vector2.x - vector.x) - (vector4.x - vector3.x) * (vector2.y - vector.y);
			if (num > -0.005f && num < 0.005f)
			{
				if (Mathf.Abs(vector2.x - vector3.x) < 0.005f && Mathf.Abs(vector2.y - vector3.y) < 0.005f)
				{
					this.m_lineVertices[p2] = VectorLine.cam3D.ScreenToWorldPoint((vector2 + vector3) * 0.5f);
					this.m_lineVertices[p3] = this.m_lineVertices[p2];
				}
				return;
			}
			float num2 = ((vector4.x - vector3.x) * (vector.y - vector3.y) - (vector4.y - vector3.y) * (vector.x - vector3.x)) / num;
			Vector3 vector5 = new Vector3(vector.x + num2 * (vector2.x - vector.x), vector.y + num2 * (vector2.y - vector.y), vector.z);
			if ((vector5 - vector2).sqrMagnitude > this.m_maxWeldDistance)
			{
				return;
			}
			this.m_lineVertices[p2] = VectorLine.cam3D.ScreenToWorldPoint(vector5);
			this.m_lineVertices[p3] = this.m_lineVertices[p2];
		}

		
		public static void LineManagerCheckDistance()
		{
			VectorLine.lineManager.StartCheckDistance();
		}

		
		public static void LineManagerDisable()
		{
			VectorLine.lineManager.DisableIfUnused();
		}

		
		public static void LineManagerEnable()
		{
			VectorLine.lineManager.EnableIfUsed();
		}

		
		public void Draw3DAuto()
		{
			this.Draw3DAuto(0f);
		}

		
		public void Draw3DAuto(float time)
		{
			if (time < 0f)
			{
				time = 0f;
			}
			VectorLine.lineManager.AddLine(this, this.m_drawTransform, time);
			this.m_isAutoDrawing = true;
			this.Draw3D();
		}

		
		public void StopDrawing3DAuto()
		{
			VectorLine.lineManager.RemoveLine(this);
			this.m_isAutoDrawing = false;
		}

		
		private void SetTextureScale()
		{
			if (this.pointsCount != this.m_pointsCount)
			{
				this.Resize();
			}
			int num;
			int num2;
			this.SetupDrawStartEnd(out num, out num2, false);
			int num3 = (this.m_lineType != LineType.Discrete) ? 1 : 2;
			int num4 = 0;
			int num5 = 0;
			int num6 = (this.m_lineWidths.Length == 1) ? 0 : 1;
			float num7 = 1f / this.m_textureScale;
			bool flag = this.m_drawTransform != null;
			Matrix4x4 matrix4x = flag ? this.m_drawTransform.localToWorldMatrix : Matrix4x4.identity;
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			Vector2 zero = Vector2.zero;
			float num8 = this.m_textureOffset;
			float num9 = this.m_capLength * 2f;
			if (this.m_is2D)
			{
				for (int i = 0; i < num2; i += num3)
				{
					if (!this.m_viewportDraw)
					{
						if (flag)
						{
							vector = matrix4x.MultiplyPoint3x4(this.m_points2[i]);
							vector2 = matrix4x.MultiplyPoint3x4(this.m_points2[i + 1]);
						}
						else
						{
							vector.x = this.m_points2[i].x;
							vector.y = this.m_points2[i].y;
							vector2.x = this.m_points2[i + 1].x;
							vector2.y = this.m_points2[i + 1].y;
						}
					}
					else if (flag)
					{
						vector = matrix4x.MultiplyPoint3x4(new Vector2(this.m_points2[i].x * (float)Screen.width, this.m_points2[i].y * (float)Screen.height));
						vector2 = matrix4x.MultiplyPoint3x4(new Vector2(this.m_points2[i + 1].x * (float)Screen.width, this.m_points2[i + 1].y * (float)Screen.height));
					}
					else
					{
						vector = new Vector2(this.m_points2[i].x * (float)Screen.width, this.m_points2[i].y * (float)Screen.height);
						vector2 = new Vector2(this.m_points2[i + 1].x * (float)Screen.width, this.m_points2[i + 1].y * (float)Screen.height);
					}
					zero.x = vector2.x - vector.x;
					zero.y = vector2.y - vector.y;
					float num10 = num7 / (this.m_lineWidths[num5] * 2f / ((float)Math.Sqrt((double)(zero.x * zero.x + zero.y * zero.y)) + num9));
					this.m_lineUVs[num4].x = num8;
					this.m_lineUVs[num4 + 3].x = num8;
					this.m_lineUVs[num4 + 2].x = num10 + num8;
					this.m_lineUVs[num4 + 1].x = num10 + num8;
					num4 += 4;
					num8 = (num8 + num10) % 1f;
					num5 += num6;
				}
			}
			else
			{
				if (!this.CheckCamera3D())
				{
					return;
				}
				for (int j = 0; j < num2; j += num3)
				{
					if (flag)
					{
						vector = VectorLine.cam3D.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[j]));
						vector2 = VectorLine.cam3D.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[j + 1]));
					}
					else
					{
						vector = VectorLine.cam3D.WorldToScreenPoint(this.m_points3[j]);
						vector2 = VectorLine.cam3D.WorldToScreenPoint(this.m_points3[j + 1]);
					}
					zero.x = vector.x - vector2.x;
					zero.y = vector.y - vector2.y;
					float num11 = num7 / (this.m_lineWidths[num5] * 2f / (float)Math.Sqrt((double)(zero.x * zero.x + zero.y * zero.y)));
					this.m_lineUVs[num4].x = num8;
					this.m_lineUVs[num4 + 3].x = num8;
					this.m_lineUVs[num4 + 2].x = num11 + num8;
					this.m_lineUVs[num4 + 1].x = num11 + num8;
					num4 += 4;
					num8 = (num8 + num11) % 1f;
					num5 += num6;
				}
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateUVs();
			}
		}

		
		private void ResetTextureScale()
		{
			for (int i = 0; i < this.m_vertexCount; i += 4)
			{
				this.m_lineUVs[i].x = 0f;
				this.m_lineUVs[i + 3].x = 0f;
				this.m_lineUVs[i + 2].x = 1f;
				this.m_lineUVs[i + 1].x = 1f;
			}
			if (this.m_vectorObject != null)
			{
				this.m_vectorObject.UpdateUVs();
			}
		}

		
		private void SetCollider(bool convertToWorldSpace)
		{
			if (!VectorLine.cam3D)
			{
				VectorLine.SetCamera3D();
				if (!VectorLine.cam3D)
				{
					Debug.LogError("No camera available...use VectorLine.SetCamera3D to assign a camera");
					return;
				}
			}
			if (VectorLine.cam3D.transform.rotation != Quaternion.identity)
			{
				Debug.LogWarning("The line collider will not be correct if the camera is rotated");
			}
			Vector3 vector = new Vector3(0f, 0f, -VectorLine.cam3D.transform.position.z);
			int drawStart = this.drawStart;
			int drawEnd = this.drawEnd;
			bool flag = this.m_capType != EndCap.None && this.m_capType <= EndCap.Mirror && this.drawStart == 0;
			bool flag2 = this.m_capType != EndCap.None && this.m_capType >= EndCap.Both && this.drawEnd == this.pointsCount - 1;
			int i = 0;
			if (this.m_lineType == LineType.Continuous)
			{
				EdgeCollider2D edgeCollider2D = this.m_go.GetComponent(typeof(EdgeCollider2D)) as EdgeCollider2D;
				int num = (drawEnd - drawStart) * 4 + 1;
				if (flag)
				{
					num += 4;
				}
				if (flag2)
				{
					num += 4;
				}
				Vector2[] array = new Vector2[num];
				int num2 = 0;
				int num3 = array.Length - 2;
				if (convertToWorldSpace)
				{
					if (flag)
					{
						i = this.m_vertexCount;
						this.SetPathWorldVerticesContinuous(ref i, ref vector, ref num2, ref num3, array);
					}
					for (i = drawStart * 4; i < drawEnd * 4; i += 4)
					{
						this.SetPathWorldVerticesContinuous(ref i, ref vector, ref num2, ref num3, array);
					}
					if (flag2)
					{
						i = this.m_vertexCount + 4;
						this.SetPathWorldVerticesContinuous(ref i, ref vector, ref num2, ref num3, array);
					}
				}
				else
				{
					if (flag)
					{
						i = this.m_vertexCount;
						this.SetPathVerticesContinuous(ref i, ref num2, ref num3, array);
					}
					for (i = drawStart * 4; i < drawEnd * 4; i += 4)
					{
						this.SetPathVerticesContinuous(ref i, ref num2, ref num3, array);
					}
					if (flag)
					{
						i = this.m_vertexCount + 4;
						this.SetPathVerticesContinuous(ref i, ref num2, ref num3, array);
					}
				}
				array[array.Length - 1] = array[0];
				edgeCollider2D.points = array;
				return;
			}
			PolygonCollider2D polygonCollider2D = this.m_go.GetComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
			Vector2[] path = new Vector2[4];
			int num4 = (drawEnd - drawStart + 1) / 2;
			if (flag)
			{
				num4++;
			}
			if (flag2)
			{
				num4++;
			}
			polygonCollider2D.pathCount = num4;
			int num5 = (drawEnd + 1) / 2 * 4;
			int num6 = 0;
			if (convertToWorldSpace)
			{
				if (flag)
				{
					i = this.m_vertexCount;
					this.SetPathWorldVerticesDiscrete(ref i, ref vector, ref num6, path, polygonCollider2D);
				}
				for (i = drawStart / 2 * 4; i < num5; i += 4)
				{
					this.SetPathWorldVerticesDiscrete(ref i, ref vector, ref num6, path, polygonCollider2D);
				}
				if (flag2)
				{
					i = this.m_vertexCount + 4;
					this.SetPathWorldVerticesDiscrete(ref i, ref vector, ref num6, path, polygonCollider2D);
					return;
				}
			}
			else
			{
				if (flag)
				{
					i = this.m_vertexCount;
					this.SetPathVerticesDiscrete(ref i, ref num6, path, polygonCollider2D);
				}
				for (i = drawStart / 2 * 4; i < num5; i += 4)
				{
					this.SetPathVerticesDiscrete(ref i, ref num6, path, polygonCollider2D);
				}
				if (flag2)
				{
					i = this.m_vertexCount + 4;
					this.SetPathVerticesDiscrete(ref i, ref num6, path, polygonCollider2D);
				}
			}
		}

		
		private void SetPathVerticesContinuous(ref int i, ref int startIdx, ref int endIdx, Vector2[] path)
		{
			path[startIdx].x = this.m_lineVertices[i].x;
			path[startIdx].y = this.m_lineVertices[i].y;
			path[startIdx + 1].x = this.m_lineVertices[i + 1].x;
			path[startIdx + 1].y = this.m_lineVertices[i + 1].y;
			path[endIdx].x = this.m_lineVertices[i + 3].x;
			path[endIdx].y = this.m_lineVertices[i + 3].y;
			path[endIdx - 1].x = this.m_lineVertices[i + 2].x;
			path[endIdx - 1].y = this.m_lineVertices[i + 2].y;
			startIdx += 2;
			endIdx -= 2;
		}

		
		private void SetPathWorldVerticesContinuous(ref int i, ref Vector3 v3, ref int startIdx, ref int endIdx, Vector2[] path)
		{
			v3.x = this.m_lineVertices[i].x;
			v3.y = this.m_lineVertices[i].y;
			path[startIdx] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			v3.x = this.m_lineVertices[i + 1].x;
			v3.y = this.m_lineVertices[i + 1].y;
			path[startIdx + 1] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			v3.x = this.m_lineVertices[i + 3].x;
			v3.y = this.m_lineVertices[i + 3].y;
			path[endIdx] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			v3.x = this.m_lineVertices[i + 2].x;
			v3.y = this.m_lineVertices[i + 2].y;
			path[endIdx - 1] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			startIdx += 2;
			endIdx -= 2;
		}

		
		private void SetPathVerticesDiscrete(ref int i, ref int pIdx, Vector2[] path, PolygonCollider2D collider)
		{
			path[0].x = this.m_lineVertices[i].x;
			path[0].y = this.m_lineVertices[i].y;
			path[1].x = this.m_lineVertices[i + 3].x;
			path[1].y = this.m_lineVertices[i + 3].y;
			path[2].x = this.m_lineVertices[i + 2].x;
			path[2].y = this.m_lineVertices[i + 2].y;
			path[3].x = this.m_lineVertices[i + 1].x;
			path[3].y = this.m_lineVertices[i + 1].y;
			int num = pIdx;
			pIdx = num + 1;
			collider.SetPath(num, path);
		}

		
		private void SetPathWorldVerticesDiscrete(ref int i, ref Vector3 v3, ref int pIdx, Vector2[] path, PolygonCollider2D collider)
		{
			v3.x = this.m_lineVertices[i].x;
			v3.y = this.m_lineVertices[i].y;
			path[0] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			v3.x = this.m_lineVertices[i + 3].x;
			v3.y = this.m_lineVertices[i + 3].y;
			path[1] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			v3.x = this.m_lineVertices[i + 2].x;
			v3.y = this.m_lineVertices[i + 2].y;
			path[2] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			v3.x = this.m_lineVertices[i + 1].x;
			v3.y = this.m_lineVertices[i + 1].y;
			path[3] = VectorLine.cam3D.ScreenToWorldPoint(v3);
			int num = pIdx;
			pIdx = num + 1;
			collider.SetPath(num, path);
		}

		
		public static List<Vector3> BytesToVector3List(byte[] lineBytes)
		{
			if (lineBytes.Length % 12 != 0)
			{
				Debug.LogError("VectorLine.BytesToVector3Array: Incorrect input byte length...must be a multiple of 12");
				return null;
			}
			VectorLine.SetupByteBlock();
			List<Vector3> list = new List<Vector3>(lineBytes.Length / 12);
			for (int i = 0; i < lineBytes.Length; i += 12)
			{
				list.Add(new Vector3(VectorLine.ConvertToFloat(lineBytes, i), VectorLine.ConvertToFloat(lineBytes, i + 4), VectorLine.ConvertToFloat(lineBytes, i + 8)));
			}
			return list;
		}

		
		public static List<Vector2> BytesToVector2List(byte[] lineBytes)
		{
			if (lineBytes.Length % 8 != 0)
			{
				Debug.LogError("VectorLine.BytesToVector2Array: Incorrect input byte length...must be a multiple of 8");
				return null;
			}
			VectorLine.SetupByteBlock();
			List<Vector2> list = new List<Vector2>(lineBytes.Length / 8);
			for (int i = 0; i < lineBytes.Length; i += 8)
			{
				list.Add(new Vector2(VectorLine.ConvertToFloat(lineBytes, i), VectorLine.ConvertToFloat(lineBytes, i + 4)));
			}
			return list;
		}

		
		private static void SetupByteBlock()
		{
			if (VectorLine.byteBlock == null)
			{
				VectorLine.byteBlock = new byte[4];
			}
			if (BitConverter.IsLittleEndian)
			{
				VectorLine.endianDiff1 = 0;
				VectorLine.endianDiff2 = 0;
				return;
			}
			VectorLine.endianDiff1 = 3;
			VectorLine.endianDiff2 = 1;
		}

		
		private static float ConvertToFloat(byte[] bytes, int i)
		{
			VectorLine.byteBlock[VectorLine.endianDiff1] = bytes[i];
			VectorLine.byteBlock[1 + VectorLine.endianDiff2] = bytes[i + 1];
			VectorLine.byteBlock[2 - VectorLine.endianDiff2] = bytes[i + 2];
			VectorLine.byteBlock[3 - VectorLine.endianDiff1] = bytes[i + 3];
			return BitConverter.ToSingle(VectorLine.byteBlock, 0);
		}

		
		public static void Destroy(ref VectorLine line)
		{
			VectorLine.DestroyLine(ref line);
		}

		
		public static void Destroy(VectorLine[] lines)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				VectorLine.DestroyLine(ref lines[i]);
			}
		}

		
		public static void Destroy(List<VectorLine> lines)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				VectorLine vectorLine = lines[i];
				VectorLine.DestroyLine(ref vectorLine);
			}
		}

		
		private static void DestroyLine(ref VectorLine line)
		{
			if (line != null)
			{
				UnityEngine.Object.Destroy(line.m_go);
				if (line.m_vectorObject != null)
				{
					line.m_vectorObject.Destroy();
				}
				if (line.isAutoDrawing)
				{
					line.StopDrawing3DAuto();
				}
				line = null;
			}
		}

		
		public static void Destroy(ref VectorLine line, GameObject go)
		{
			VectorLine.Destroy(ref line);
			if (go != null)
			{
				UnityEngine.Object.Destroy(go);
			}
		}

		
		public void SetDistances()
		{
			if (this.m_lineType == LineType.Points)
			{
				return;
			}
			if (this.m_distances == null || this.m_distances.Length != ((this.m_lineType != LineType.Discrete) ? this.pointsCount : (this.pointsCount / 2 + 1)))
			{
				this.m_distances = new float[(this.m_lineType != LineType.Discrete) ? this.pointsCount : (this.pointsCount / 2 + 1)];
			}
			double num = 0.0;
			int num2 = this.pointsCount - 1;
			if (this.is2D)
			{
				if (this.m_lineType != LineType.Discrete)
				{
					for (int i = 0; i < num2; i++)
					{
						Vector2 vector = this.m_points2[i] - this.m_points2[i + 1];
						num += Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y));
						this.m_distances[i + 1] = (float)num;
					}
					return;
				}
				int num3 = 1;
				for (int j = 0; j < num2; j += 2)
				{
					Vector2 vector2 = this.m_points2[j] - this.m_points2[j + 1];
					num += Math.Sqrt((double)(vector2.x * vector2.x + vector2.y * vector2.y));
					this.m_distances[num3++] = (float)num;
				}
				return;
			}
			else
			{
				if (this.m_lineType != LineType.Discrete)
				{
					for (int k = 0; k < num2; k++)
					{
						Vector3 vector3 = this.m_points3[k] - this.m_points3[k + 1];
						num += Math.Sqrt((double)(vector3.x * vector3.x + vector3.y * vector3.y + vector3.z * vector3.z));
						this.m_distances[k + 1] = (float)num;
					}
					return;
				}
				int num4 = 1;
				for (int l = 0; l < num2; l += 2)
				{
					Vector3 vector4 = this.m_points3[l] - this.m_points3[l + 1];
					num += Math.Sqrt((double)(vector4.x * vector4.x + vector4.y * vector4.y + vector4.z * vector4.z));
					this.m_distances[num4++] = (float)num;
				}
				return;
			}
		}

		
		public float GetLength()
		{
			if (this.m_distances == null || this.m_distances.Length != ((this.m_lineType != LineType.Discrete) ? this.pointsCount : (this.pointsCount / 2 + 1)))
			{
				this.SetDistances();
			}
			return this.m_distances[this.m_distances.Length - 1];
		}

		
		public Vector2 GetPoint01(float distance)
		{
			int num;
			return this.GetPoint(Mathf.Lerp(0f, this.GetLength(), distance), out num);
		}

		
		public Vector2 GetPoint01(float distance, out int index)
		{
			return this.GetPoint(Mathf.Lerp(0f, this.GetLength(), distance), out index);
		}

		
		public Vector2 GetPoint(float distance)
		{
			int num;
			return this.GetPoint(distance, out num);
		}

		
		public Vector2 GetPoint(float distance, out int index)
		{
			if (!this.m_is2D)
			{
				Debug.LogError("VectorLine.GetPoint only works with Vector2 points");
				index = 0;
				return Vector2.zero;
			}
			this.SetDistanceIndex(out index, distance);
			Vector2 vector;
			if (this.m_lineType != LineType.Discrete)
			{
				vector = Vector2.Lerp(this.m_points2[index - 1], this.m_points2[index], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
			}
			else
			{
				vector = Vector2.Lerp(this.m_points2[(index - 1) * 2], this.m_points2[(index - 1) * 2 + 1], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
			}
			if (this.m_drawTransform)
			{
				vector = this.m_drawTransform.localToWorldMatrix.MultiplyPoint3x4(vector);
			}
			index--;
			return vector;
		}

		
		public Vector3 GetPoint3D01(float distance)
		{
			int num;
			return this.GetPoint3D(Mathf.Lerp(0f, this.GetLength(), distance), out num);
		}

		
		public Vector3 GetPoint3D01(float distance, out int index)
		{
			return this.GetPoint3D(Mathf.Lerp(0f, this.GetLength(), distance), out index);
		}

		
		public Vector3 GetPoint3D(float distance)
		{
			int num;
			return this.GetPoint3D(distance, out num);
		}

		
		public Vector3 GetPoint3D(float distance, out int index)
		{
			if (this.m_is2D)
			{
				Debug.LogError("VectorLine.GetPoint3D only works with Vector3 points");
				index = 0;
				return Vector3.zero;
			}
			this.SetDistanceIndex(out index, distance);
			Vector3 vector;
			if (this.m_lineType != LineType.Discrete)
			{
				vector = Vector3.Lerp(this.m_points3[index - 1], this.m_points3[index], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
			}
			else
			{
				vector = Vector3.Lerp(this.m_points3[(index - 1) * 2], this.m_points3[(index - 1) * 2 + 1], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
			}
			if (this.m_drawTransform)
			{
				vector = this.m_drawTransform.localToWorldMatrix.MultiplyPoint3x4(vector);
			}
			index--;
			return vector;
		}

		
		private void SetDistanceIndex(out int i, float distance)
		{
			if (this.m_distances == null)
			{
				this.SetDistances();
			}
			i = this.m_drawStart + 1;
			if (this.m_lineType == LineType.Discrete)
			{
				i = (i + 1) / 2;
			}
			if (i >= this.m_distances.Length)
			{
				i = this.m_distances.Length - 1;
			}
			int num = this.m_drawEnd;
			if (this.m_lineType == LineType.Discrete)
			{
				num = (num + 1) / 2;
			}
			while (distance > this.m_distances[i] && i < num)
			{
				i++;
			}
		}

		
		public static void SetEndCap(string name, EndCap capType)
		{
			VectorLine.SetEndCap(name, capType, 0f, 0f, 1f, 1f, null);
		}

		
		public static void SetEndCap(string name, EndCap capType, params Texture2D[] textures)
		{
			VectorLine.SetEndCap(name, capType, 0f, 0f, 1f, 1f, textures);
		}

		
		public static void SetEndCap(string name, EndCap capType, float offset, params Texture2D[] textures)
		{
			VectorLine.SetEndCap(name, capType, offset, offset, 1f, 1f, textures);
		}

		
		public static void SetEndCap(string name, EndCap capType, float offsetFront, float offsetBack, params Texture2D[] textures)
		{
			VectorLine.SetEndCap(name, capType, offsetFront, offsetBack, 1f, 1f, textures);
		}

		
		public static void SetEndCap(string name, EndCap capType, float offsetFront, float offsetBack, float scaleFront, float scaleBack, params Texture2D[] textures)
		{
			if (VectorLine.capDictionary == null)
			{
				VectorLine.capDictionary = new Dictionary<string, CapInfo>();
			}
			if (name == null || name == "")
			{
				Debug.LogError("VectorLine.SetEndCap: must supply a name");
				return;
			}
			if (VectorLine.capDictionary.ContainsKey(name) && capType != EndCap.None)
			{
				Debug.LogError("VectorLine.SetEndCap: end cap \"" + name + "\" has already been set up");
				return;
			}
			if (capType == EndCap.None)
			{
				VectorLine.RemoveEndCap(name);
				return;
			}
			if ((capType == EndCap.Front || capType == EndCap.Back || capType == EndCap.Mirror) && textures.Length < 2)
			{
				Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): must supply two textures when using SetEndCap with EndCap.Front, EndCap.Back, or EndCap.Mirror");
				return;
			}
			if (textures[0] == null || textures[1] == null)
			{
				Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): end cap textures must not be null");
				return;
			}
			if (textures[0].width != textures[0].height)
			{
				Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): the line texture must be square");
				return;
			}
			if (textures[1].height != textures[0].height)
			{
				Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): all textures must be the same height");
				return;
			}
			if (capType == EndCap.Both)
			{
				if (textures.Length < 3)
				{
					Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): must supply three textures when using SetEndCap with EndCap.Both");
					return;
				}
				if (textures[2] == null)
				{
					Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): end cap textures must not be null");
					return;
				}
				if (textures[2].height != textures[0].height)
				{
					Debug.LogError("VectorLine.SetEndCap (\"" + name + "\"): all textures must be the same height");
					return;
				}
			}
			Texture2D texture2D = textures[0];
			Texture2D texture2D2 = textures[1];
			Texture2D texture2D3 = (textures.Length == 3) ? textures[2] : null;
			int num = 4;
			int width = texture2D.width;
			float num2 = 0f;
			float ratio = 0f;
			int num3 = 0;
			int num4 = 0;
			Color32[] array = null;
			Color32[] array2 = null;
			if (capType == EndCap.Front)
			{
				array = VectorLine.GetRotatedPixels(texture2D2);
				num3 = texture2D2.width;
				array2 = VectorLine.GetRowPixels(array, num, 0, width);
				num4 = num;
				num2 = (float)texture2D2.width / (float)texture2D2.height;
			}
			else if (capType == EndCap.Back)
			{
				array2 = VectorLine.GetRotatedPixels(texture2D2);
				num4 = texture2D2.width;
				array = VectorLine.GetRowPixels(array2, num, num4 - 1, width);
				num3 = num;
				ratio = (float)texture2D2.width / (float)texture2D2.height;
			}
			else if (capType == EndCap.Both)
			{
				array = VectorLine.GetRotatedPixels(texture2D2);
				num3 = texture2D2.width;
				array2 = VectorLine.GetRotatedPixels(texture2D3);
				num4 = texture2D3.width;
				num2 = (float)texture2D2.width / (float)texture2D2.height;
				ratio = (float)texture2D3.width / (float)texture2D3.height;
			}
			else if (capType == EndCap.Mirror)
			{
				array = VectorLine.GetRotatedPixels(texture2D2);
				num3 = texture2D2.width;
				array2 = VectorLine.GetRowPixels(array, num, 0, width);
				num4 = num;
				num2 = (float)texture2D2.width / (float)texture2D2.height;
				ratio = num2;
			}
			int num5 = texture2D.height + num3 + num4 + num * 4;
			Color32[] pixels = texture2D.GetPixels32();
			Color32[] array3 = new Color32[num * width];
			Color32 color = Color.clear;
			for (int i = 0; i < num * width; i++)
			{
				array3[i] = color;
			}
			Color32[] rowPixels = VectorLine.GetRowPixels(array2, num, num4 - 1, width);
			Color32[] rowPixels2 = VectorLine.GetRowPixels(array, num, 0, width);
			bool flag = texture2D.mipmapCount > 1;
			Texture2D texture2D4 = new Texture2D(width, num5, TextureFormat.ARGB32, flag);
			texture2D4.name = texture2D.name + " end cap";
			texture2D4.wrapMode = texture2D.wrapMode;
			texture2D4.filterMode = texture2D.filterMode;
			float num6 = 1f / (float)num5;
			float[] array4 = new float[6];
			int num7 = 0;
			texture2D4.SetPixels32(0, 0, width, num, array3);
			num7 += num;
			array4[0] = num6 * (float)num7;
			texture2D4.SetPixels32(0, num7, width, texture2D.height, pixels);
			num7 += texture2D.height;
			array4[1] = num6 * (float)num7;
			texture2D4.SetPixels32(0, num7, width, num, array3);
			num7 += num;
			array4[2] = num6 * (float)num7;
			texture2D4.SetPixels32(0, num7, width, num4, array2);
			num7 += num4;
			array4[3] = num6 * (float)num7;
			texture2D4.SetPixels32(0, num7, width, num, rowPixels);
			num7 += num;
			texture2D4.SetPixels32(0, num7, width, num, rowPixels2);
			num7 += num;
			array4[4] = num6 * (float)num7;
			texture2D4.SetPixels32(0, num7, width, num3, array);
			array4[5] = num6 * (float)(num7 + num3);
			texture2D4.Apply(flag, true);
			VectorLine.capDictionary.Add(name, new CapInfo(capType, texture2D4, num2, ratio, offsetFront, offsetBack, scaleFront, scaleBack, array4));
		}

		
		private static Color32[] GetRowPixels(Color32[] texPixels, int numberOfRows, int row, int w)
		{
			Color32[] array = new Color32[w * numberOfRows];
			for (int i = 0; i < numberOfRows; i++)
			{
				Array.Copy(texPixels, row * w, array, i * w, w);
			}
			return array;
		}

		
		private static Color32[] GetRotatedPixels(Texture2D tex)
		{
			Color32[] pixels = tex.GetPixels32();
			Color32[] array = new Color32[pixels.Length];
			int width = tex.width;
			int height = tex.height;
			int num = 0;
			for (int i = 0; i < height; i++)
			{
				int num2 = tex.width - 1;
				for (int j = 0; j < width; j++)
				{
					array[num2 * height + num] = pixels[i * width + j];
					num2--;
				}
				num++;
			}
			return array;
		}

		
		public static void RemoveEndCap(string name)
		{
			if (!VectorLine.capDictionary.ContainsKey(name))
			{
				Debug.LogError("VectorLine: RemoveEndCap: \"" + name + "\" has not been set up");
				return;
			}
			UnityEngine.Object.Destroy(VectorLine.capDictionary[name].texture);
			VectorLine.capDictionary.Remove(name);
		}

		
		public bool Selected(Vector2 p)
		{
			int num;
			return this.Selected(p, 0, 0, out num, VectorLine.cam3D);
		}

		
		public bool Selected(Vector2 p, out int index)
		{
			return this.Selected(p, 0, 0, out index, VectorLine.cam3D);
		}

		
		public bool Selected(Vector2 p, int extraDistance, out int index)
		{
			return this.Selected(p, extraDistance, 0, out index, VectorLine.cam3D);
		}

		
		public bool Selected(Vector2 p, int extraDistance, int extraLength, out int index)
		{
			return this.Selected(p, extraDistance, extraLength, out index, VectorLine.cam3D);
		}

		
		public bool Selected(Vector2 p, Camera cam)
		{
			int num;
			return this.Selected(p, 0, 0, out num, cam);
		}

		
		public bool Selected(Vector2 p, out int index, Camera cam)
		{
			return this.Selected(p, 0, 0, out index, cam);
		}

		
		public bool Selected(Vector2 p, int extraDistance, out int index, Camera cam)
		{
			return this.Selected(p, extraDistance, 0, out index, cam);
		}

		
		public bool Selected(Vector2 p, int extraDistance, int extraLength, out int index, Camera cam)
		{
			if (cam == null)
			{
				VectorLine.SetCamera3D();
				if (!VectorLine.cam3D)
				{
					Debug.LogError("VectorLine.Selected: camera cannot be null. If there is no camera tagged \"MainCamera\", supply one manually");
					index = 0;
					return false;
				}
				cam = VectorLine.cam3D;
			}
			int num = (this.m_lineWidths.Length == 1) ? 0 : 1;
			int num2 = (this.m_lineType != LineType.Discrete) ? (this.m_drawStart - num) : (this.m_drawStart / 2 - num);
			if (this.m_lineWidths.Length == 1)
			{
				num = 0;
				num2 = 0;
			}
			else
			{
				num = 1;
			}
			int num3 = this.m_drawEnd;
			bool flag = this.m_drawTransform != null;
			Matrix4x4 matrix4x = flag ? this.m_drawTransform.localToWorldMatrix : Matrix4x4.identity;
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			if (this.m_lineType == LineType.Points)
			{
				if (num3 == this.pointsCount)
				{
					num3--;
				}
				if (this.m_is2D)
				{
					for (int i = this.m_drawStart; i <= num3; i++)
					{
						num2 += num;
						float num4 = this.m_lineWidths[num2] + (float)extraDistance;
						Vector2 vector2 = flag ? matrix4x.MultiplyPoint3x4(this.m_points2[i]) : this.m_points2[i];
						if (this.m_viewportDraw)
						{
							vector2.x *= vector.x;
							vector2.y *= vector.y;
						}
						if (p.x >= vector2.x - num4 && p.x <= vector2.x + num4 && p.y >= vector2.y - num4 && p.y <= vector2.y + num4)
						{
							index = i;
							return true;
						}
					}
					index = -1;
					return false;
				}
				for (int j = this.m_drawStart; j <= num3; j++)
				{
					num2 += num;
					float num5 = this.m_lineWidths[num2] + (float)extraDistance;
					Vector2 vector2 = flag ? cam.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[j])) : cam.WorldToScreenPoint(this.m_points3[j]);
					if (p.x >= vector2.x - num5 && p.x <= vector2.x + num5 && p.y >= vector2.y - num5 && p.y <= vector2.y + num5)
					{
						index = j;
						return true;
					}
				}
				index = -1;
				return false;
			}
			else
			{
				int num6 = (this.m_lineType != LineType.Discrete) ? 1 : 2;
				Vector2 vector3 = Vector2.zero;
				if (this.m_lineType != LineType.Discrete && this.m_drawEnd == this.pointsCount)
				{
					num3--;
				}
				Vector2 vector4;
				Vector2 vector5;
				if (this.m_is2D)
				{
					for (int k = this.m_drawStart; k < num3; k += num6)
					{
						num2 += num;
						if (flag)
						{
							vector4 = matrix4x.MultiplyPoint3x4(this.m_points2[k]);
							vector5 = matrix4x.MultiplyPoint3x4(this.m_points2[k + 1]);
						}
						else
						{
							vector4.x = this.m_points2[k].x;
							vector4.y = this.m_points2[k].y;
							vector5.x = this.m_points2[k + 1].x;
							vector5.y = this.m_points2[k + 1].y;
						}
						if (this.m_viewportDraw)
						{
							vector4.x *= vector.x;
							vector4.y *= vector.y;
							vector5.x *= vector.x;
							vector5.y *= vector.y;
						}
						if (extraLength > 0)
						{
							vector3 = (vector4 - vector5).normalized * (float)extraLength;
							vector4.x += vector3.x;
							vector4.y += vector3.y;
							vector5.x -= vector3.x;
							vector5.y -= vector3.y;
						}
						float num7 = Vector2.Dot(p - vector4, vector5 - vector4) / (vector5 - vector4).sqrMagnitude;
						if (num7 >= 0f && num7 <= 1f && (p - (vector4 + num7 * (vector5 - vector4))).sqrMagnitude <= (this.m_lineWidths[num2] + (float)extraDistance) * (this.m_lineWidths[num2] + (float)extraDistance))
						{
							index = ((this.m_lineType != LineType.Discrete) ? k : (k / 2));
							return true;
						}
					}
					index = -1;
					return false;
				}
				Vector3 vector6 = VectorLine.v3zero;
				for (int l = this.m_drawStart; l < num3; l += num6)
				{
					num2 += num;
					Vector3 vector7;
					if (flag)
					{
						vector7 = cam.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[l]));
						vector6 = cam.WorldToScreenPoint(matrix4x.MultiplyPoint3x4(this.m_points3[l + 1]));
					}
					else
					{
						vector7 = cam.WorldToScreenPoint(this.m_points3[l]);
						vector6 = cam.WorldToScreenPoint(this.m_points3[l + 1]);
					}
					if (vector7.z >= 0f && vector6.z >= 0f)
					{
						vector4.x = (float)((int)vector7.x);
						vector5.x = (float)((int)vector6.x);
						vector4.y = (float)((int)vector7.y);
						vector5.y = (float)((int)vector6.y);
						if (vector4.x != vector5.x || vector4.y != vector5.y)
						{
							if (extraLength > 0)
							{
								vector3 = (vector4 - vector5).normalized * (float)extraLength;
								vector4.x += vector3.x;
								vector4.y += vector3.y;
								vector5.x -= vector3.x;
								vector5.y -= vector3.y;
							}
							float num7 = Vector2.Dot(p - vector4, vector5 - vector4) / (vector5 - vector4).sqrMagnitude;
							if (num7 >= 0f && num7 <= 1f && (p - (vector4 + num7 * (vector5 - vector4))).sqrMagnitude <= (this.m_lineWidths[num2] + (float)extraDistance) * (this.m_lineWidths[num2] + (float)extraDistance))
							{
								index = ((this.m_lineType != LineType.Discrete) ? l : (l / 2));
								return true;
							}
						}
					}
				}
				index = -1;
				return false;
			}
		}

		
		private bool Approximately(Vector2 p1, Vector2 p2)
		{
			return this.Approximately(p1.x, p2.x) && this.Approximately(p1.y, p2.y);
		}

		
		private bool Approximately(Vector3 p1, Vector3 p2)
		{
			return this.Approximately(p1.x, p2.x) && this.Approximately(p1.y, p2.y) && this.Approximately(p1.z, p2.z);
		}

		
		private bool Approximately(float a, float b)
		{
			return Mathf.Round(a * 100f) / 100f == Mathf.Round(b * 100f) / 100f;
		}

		
		private bool WrongArrayLength(int arrayLength, VectorLine.FunctionName functionName)
		{
			if (this.m_lineType == LineType.Continuous)
			{
				if (arrayLength != this.pointsCount - 1)
				{
					Debug.LogError(string.Concat(new object[]
					{
						VectorLine.functionNames[(int)functionName],
						" list for \"",
						this.name,
						"\" must be length of points array minus one for a continuous line (one entry per line segment). Expected ",
						this.pointsCount - 1,
						", got ",
						arrayLength
					}));
					return true;
				}
			}
			else if (arrayLength != this.pointsCount / 2)
			{
				Debug.LogError(string.Concat(new object[]
				{
					VectorLine.functionNames[(int)functionName],
					" list in \"",
					this.name,
					"\" must be exactly half the length of points array for a discrete line (one entry per line segment). Expected ",
					this.pointsCount / 2,
					", got ",
					arrayLength
				}));
				return true;
			}
			return false;
		}

		
		private bool CheckArrayLength(VectorLine.FunctionName functionName, int segments, int index)
		{
			if (segments < 1)
			{
				Debug.LogError("VectorLine." + VectorLine.functionNames[(int)functionName] + " needs at least 1 segment");
				return false;
			}
			if (index < 0)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"VectorLine.",
					VectorLine.functionNames[(int)functionName],
					": The index value for \"",
					this.name,
					"\" must be >= 0"
				}));
				return false;
			}
			if (this.m_lineType != LineType.Points)
			{
				if (this.m_lineType == LineType.Continuous)
				{
					if (index + (segments + 1) > this.pointsCount)
					{
						if (index == 0)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"VectorLine.",
								VectorLine.functionNames[(int)functionName],
								": The length of the array for continuous lines needs to be at least the number of segments plus one for \"",
								this.name,
								"\""
							}));
							return false;
						}
						Debug.LogError(string.Concat(new object[]
						{
							"VectorLine: Calling ",
							VectorLine.functionNames[(int)functionName],
							" with an index of ",
							index,
							" would exceed the length of the Vector array (",
							this.pointsCount,
							") for \"",
							this.name,
							"\""
						}));
						return false;
					}
				}
				else if (index + segments * 2 > this.pointsCount)
				{
					if (index == 0)
					{
						Debug.LogError(string.Concat(new string[]
						{
							"VectorLine.",
							VectorLine.functionNames[(int)functionName],
							": The length of the array for discrete lines needs to be at least twice the number of segments for \"",
							this.name,
							"\""
						}));
						return false;
					}
					Debug.LogError(string.Concat(new object[]
					{
						"VectorLine: Calling ",
						VectorLine.functionNames[(int)functionName],
						" with an index of ",
						index,
						" would exceed the length of the Vector array (",
						this.pointsCount,
						") for \"",
						this.name,
						"\""
					}));
					return false;
				}
				return true;
			}
			if (index + segments <= this.pointsCount)
			{
				return true;
			}
			if (index == 0)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"VectorLine.",
					VectorLine.functionNames[(int)functionName],
					": The number of segments cannot exceed the number of points in the array for \"",
					this.name,
					"\""
				}));
				return false;
			}
			Debug.LogError(string.Concat(new object[]
			{
				"VectorLine: Calling ",
				VectorLine.functionNames[(int)functionName],
				" with an index of ",
				index,
				" would exceed the length of the Vector array for \"",
				this.name,
				"\""
			}));
			return false;
		}

		
		public void MakeRect(Rect rect)
		{
			this.MakeRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), 0);
		}

		
		public void MakeRect(Rect rect, int index)
		{
			this.MakeRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), index);
		}

		
		public void MakeRect(Vector3 bottomLeft, Vector3 topRight)
		{
			this.MakeRect(bottomLeft, topRight, 0);
		}

		
		public void MakeRect(Vector3 bottomLeft, Vector3 topRight, int index)
		{
			if (this.m_lineType != LineType.Discrete)
			{
				if (index + 5 > this.pointsCount)
				{
					if (index == 0)
					{
						Debug.LogError("VectorLine.MakeRect: The length of the array for continuous lines needs to be at least 5 for \"" + this.name + "\"");
						return;
					}
					Debug.LogError(string.Concat(new object[]
					{
						"Calling VectorLine.MakeRect with an index of ",
						index,
						" would exceed the length of the Vector2 array for \"",
						this.name,
						"\""
					}));
					return;
				}
				else
				{
					if (this.m_is2D)
					{
						this.m_points2[index] = new Vector2(bottomLeft.x, bottomLeft.y);
						this.m_points2[index + 1] = new Vector2(topRight.x, bottomLeft.y);
						this.m_points2[index + 2] = new Vector2(topRight.x, topRight.y);
						this.m_points2[index + 3] = new Vector2(bottomLeft.x, topRight.y);
						this.m_points2[index + 4] = new Vector2(bottomLeft.x, bottomLeft.y);
						return;
					}
					this.m_points3[index] = new Vector3(bottomLeft.x, bottomLeft.y, bottomLeft.z);
					this.m_points3[index + 1] = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
					this.m_points3[index + 2] = new Vector3(topRight.x, topRight.y, topRight.z);
					this.m_points3[index + 3] = new Vector3(bottomLeft.x, topRight.y, topRight.z);
					this.m_points3[index + 4] = new Vector3(bottomLeft.x, bottomLeft.y, bottomLeft.z);
					return;
				}
			}
			else if (index + 8 > this.pointsCount)
			{
				if (index == 0)
				{
					Debug.LogError("VectorLine.MakeRect: The length of the array for discrete lines needs to be at least 8 for \"" + this.name + "\"");
					return;
				}
				Debug.LogError(string.Concat(new object[]
				{
					"Calling VectorLine.MakeRect with an index of ",
					index,
					" would exceed the length of the Vector2 array for \"",
					this.name,
					"\""
				}));
				return;
			}
			else
			{
				if (this.m_is2D)
				{
					this.m_points2[index] = new Vector2(bottomLeft.x, bottomLeft.y);
					this.m_points2[index + 1] = new Vector2(topRight.x, bottomLeft.y);
					this.m_points2[index + 2] = new Vector2(topRight.x, bottomLeft.y);
					this.m_points2[index + 3] = new Vector2(topRight.x, topRight.y);
					this.m_points2[index + 4] = new Vector2(topRight.x, topRight.y);
					this.m_points2[index + 5] = new Vector2(bottomLeft.x, topRight.y);
					this.m_points2[index + 6] = new Vector2(bottomLeft.x, topRight.y);
					this.m_points2[index + 7] = new Vector2(bottomLeft.x, bottomLeft.y);
					return;
				}
				this.m_points3[index] = new Vector3(bottomLeft.x, bottomLeft.y, bottomLeft.z);
				this.m_points3[index + 1] = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
				this.m_points3[index + 2] = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
				this.m_points3[index + 3] = new Vector3(topRight.x, topRight.y, topRight.z);
				this.m_points3[index + 4] = new Vector3(topRight.x, topRight.y, topRight.z);
				this.m_points3[index + 5] = new Vector3(bottomLeft.x, topRight.y, topRight.z);
				this.m_points3[index + 6] = new Vector3(bottomLeft.x, topRight.y, topRight.z);
				this.m_points3[index + 7] = new Vector3(bottomLeft.x, bottomLeft.y, bottomLeft.z);
				return;
			}
		}

		
		public void MakeRoundedRect(Rect rect, float cornerRadius, int cornerSegments)
		{
			this.MakeRoundedRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), cornerRadius, cornerSegments, 0);
		}

		
		public void MakeRoundedRect(Rect rect, float cornerRadius, int cornerSegments, int index)
		{
			this.MakeRoundedRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), cornerRadius, cornerSegments, index);
		}

		
		public void MakeRoundedRect(Vector3 bottomLeft, Vector3 topRight, float cornerRadius, int cornerSegments)
		{
			this.MakeRoundedRect(bottomLeft, topRight, cornerRadius, cornerSegments, 0);
		}

		
		public void MakeRoundedRect(Vector3 bottomLeft, Vector3 topRight, float cornerRadius, int cornerSegments, int index)
		{
			if (cornerSegments < 1)
			{
				Debug.LogError("VectorLine.MakeRoundedRect: cornerSegments value must be >= 1");
				return;
			}
			if (index < 0)
			{
				Debug.LogError("VectorLine.MakeRoundedRect: index value must be >= 0");
				return;
			}
			if (!this.m_is2D && bottomLeft.z != topRight.z)
			{
				Debug.LogError("VectorLine.MakeRoundedRect only works on the X/Y plane");
				return;
			}
			int num = (this.m_lineType != LineType.Discrete) ? (cornerSegments * 4 + 5 + index) : (cornerSegments * 8 + 8 + index);
			if (this.pointsCount < num)
			{
				this.Resize(num);
			}
			if (bottomLeft.x > topRight.x)
			{
				this.Exchange(ref bottomLeft, ref topRight, 0);
			}
			if (bottomLeft.y > topRight.y)
			{
				this.Exchange(ref bottomLeft, ref topRight, 1);
			}
			bottomLeft += new Vector3(cornerRadius, cornerRadius);
			topRight -= new Vector3(cornerRadius, cornerRadius);
			this.MakeCircle(bottomLeft, cornerRadius, 4 * cornerSegments, index);
			int num2 = (this.m_lineType != LineType.Discrete) ? (cornerSegments + 1) : (cornerSegments * 2);
			int originalCount = (this.m_lineType != LineType.Discrete) ? cornerSegments : (cornerSegments * 2);
			if (this.m_is2D)
			{
				this.CopyAndAddPoints(num2, originalCount, 3, new Vector2(0f, topRight.y - bottomLeft.y), index);
				this.CopyAndAddPoints(num2, originalCount, 2, Vector2.zero, index);
				this.CopyAndAddPoints(num2, originalCount, 1, new Vector2(topRight.x - bottomLeft.x, 0f), index);
				this.CopyAndAddPoints(num2, originalCount, 0, new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y), index);
				if (this.m_lineType != LineType.Discrete)
				{
					this.m_points2[num2 * 4 + index] = this.m_points2[index];
					return;
				}
				this.m_points2[num2 * 4 + 7 + index] = this.m_points2[index];
				this.m_points2[num2 * 3 + 5 + index] = this.m_points2[num2 * 3 + 6 + index];
				this.m_points2[num2 * 2 + 3 + index] = this.m_points2[num2 * 2 + 4 + index];
				this.m_points2[num2 + 1 + index] = this.m_points2[num2 + 2 + index];
				return;
			}
			else
			{
				this.CopyAndAddPoints(num2, originalCount, 3, Vector2.zero, index);
				this.CopyAndAddPoints(num2, originalCount, 2, new Vector2(0f, topRight.y - bottomLeft.y), index);
				this.CopyAndAddPoints(num2, originalCount, 1, new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y), index);
				this.CopyAndAddPoints(num2, originalCount, 0, new Vector2(topRight.x - bottomLeft.x, 0f), index);
				if (this.m_lineType != LineType.Discrete)
				{
					this.m_points3[num2 * 4 + index] = this.m_points3[index];
					return;
				}
				this.m_points3[num2 * 4 + 7 + index] = this.m_points3[index];
				this.m_points3[num2 * 3 + 5 + index] = this.m_points3[num2 * 3 + 6 + index];
				this.m_points3[num2 * 2 + 3 + index] = this.m_points3[num2 * 2 + 4 + index];
				this.m_points3[num2 + 1 + index] = this.m_points3[num2 + 2 + index];
				return;
			}
		}

		
		private void CopyAndAddPoints(int cornerPointCount, int originalCount, int sectionNumber, Vector2 add, int index)
		{
			Vector3 b = add;
			for (int i = cornerPointCount - 1; i >= 0; i--)
			{
				if (this.m_lineType != LineType.Discrete)
				{
					if (this.m_is2D)
					{
						this.m_points2[cornerPointCount * sectionNumber + i + index] = this.m_points2[originalCount * sectionNumber + i + index] + add;
					}
					else
					{
						this.m_points3[cornerPointCount * sectionNumber + i + index] = this.m_points3[originalCount * sectionNumber + i + index] + b;
					}
				}
				else if (this.m_is2D)
				{
					this.m_points2[cornerPointCount * sectionNumber + sectionNumber * 2 + i + index] = this.m_points2[originalCount * sectionNumber + i + index] + add;
				}
				else
				{
					this.m_points3[cornerPointCount * sectionNumber + sectionNumber * 2 + i + index] = this.m_points3[originalCount * sectionNumber + i + index] + b;
				}
			}
			if (this.m_lineType == LineType.Discrete)
			{
				int num = cornerPointCount * (sectionNumber + 1) + sectionNumber * 2 + index;
				if (this.m_is2D)
				{
					this.m_points2[num] = this.m_points2[num - 1];
					return;
				}
				this.m_points3[num] = this.m_points3[num - 1];
			}
		}

		
		private void Exchange(ref Vector3 v1, ref Vector3 v2, int i)
		{
			float value = v1[i];
			v1[i] = v2[i];
			v2[i] = value;
		}

		
		public void MakeCircle(Vector3 origin, float radius)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		
		public void MakeCircle(Vector3 origin, float radius, int segments)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, 0f, 0);
		}

		
		public void MakeCircle(Vector3 origin, float radius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, pointRotation, 0);
		}

		
		public void MakeCircle(Vector3 origin, float radius, int segments, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, 0f, index);
		}

		
		public void MakeCircle(Vector3 origin, float radius, int segments, float pointRotation, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, pointRotation, index);
		}

		
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, 0f, 0);
		}

		
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, pointRotation, 0);
		}

		
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, int index)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, 0f, index);
		}

		
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, float pointRotation, int index)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, pointRotation, index);
		}

		
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, segments, 0f, 0);
		}

		
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, segments, 0f, index);
		}

		
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, segments, pointRotation, 0);
		}

		
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, 0f, 0);
		}

		
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, int index)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, 0f, index);
		}

		
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, pointRotation, 0);
		}

		
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, float pointRotation, int index)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, pointRotation, index);
		}

		
		public void MakeArc(Vector3 origin, float xRadius, float yRadius, float startDegrees, float endDegrees)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, startDegrees, endDegrees, this.GetSegmentNumber(), 0f, 0);
		}

		
		public void MakeArc(Vector3 origin, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, 0);
		}

		
		public void MakeArc(Vector3 origin, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, index);
		}

		
		public void MakeArc(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, startDegrees, endDegrees, this.GetSegmentNumber(), 0f, 0);
		}

		
		public void MakeArc(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, 0);
		}

		
		public void MakeArc(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments, int index)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, index);
		}

		
		private void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments, float pointRotation, int index)
		{
			if (segments < 3)
			{
				Debug.LogError("VectorLine.MakeEllipse needs at least 3 segments");
				return;
			}
			if (!this.CheckArrayLength(VectorLine.FunctionName.MakeEllipse, segments, index))
			{
				return;
			}
			startDegrees = Mathf.Repeat(startDegrees, 360f);
			endDegrees = Mathf.Repeat(endDegrees, 360f);
			float num;
			float num2;
			if (startDegrees == endDegrees)
			{
				num = 360f;
				num2 = -pointRotation * 0.017453292f;
			}
			else
			{
				num = ((endDegrees > startDegrees) ? (endDegrees - startDegrees) : (360f - startDegrees + endDegrees));
				num2 = startDegrees * 0.017453292f;
			}
			float num3 = num / (float)segments * 0.017453292f;
			if (this.m_lineType != LineType.Discrete)
			{
				if (startDegrees != endDegrees)
				{
					segments++;
				}
				if (this.m_is2D)
				{
					Vector2 a = origin;
					int i;
					for (i = 0; i < segments; i++)
					{
						this.m_points2[index + i] = a + new Vector2(0.5f + Mathf.Sin(num2) * xRadius, 0.5f + Mathf.Cos(num2) * yRadius);
						num2 += num3;
					}
					if (this.m_lineType != LineType.Points && startDegrees == endDegrees)
					{
						this.m_points2[index + i] = this.m_points2[index + (i - segments)];
						return;
					}
				}
				else
				{
					Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(-upVector, upVector), Vector3.one);
					int i;
					for (i = 0; i < segments; i++)
					{
						this.m_points3[index + i] = origin + matrix4x.MultiplyPoint3x4(new Vector3(Mathf.Sin(num2) * xRadius, Mathf.Cos(num2) * yRadius, 0f));
						num2 += num3;
					}
					if (this.m_lineType != LineType.Points && startDegrees == endDegrees)
					{
						this.m_points3[index + i] = this.m_points3[index + (i - segments)];
						return;
					}
				}
			}
			else
			{
				if (this.m_is2D)
				{
					Vector2 a2 = origin;
					for (int j = 0; j < segments * 2; j++)
					{
						this.m_points2[index + j] = a2 + new Vector2(0.5f + Mathf.Sin(num2) * xRadius, 0.5f + Mathf.Cos(num2) * yRadius);
						num2 += num3;
						j++;
						this.m_points2[index + j] = a2 + new Vector2(0.5f + Mathf.Sin(num2) * xRadius, 0.5f + Mathf.Cos(num2) * yRadius);
					}
					return;
				}
				Matrix4x4 matrix4x2 = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(-upVector, upVector), Vector3.one);
				for (int k = 0; k < segments * 2; k++)
				{
					this.m_points3[index + k] = origin + matrix4x2.MultiplyPoint3x4(new Vector3(Mathf.Sin(num2) * xRadius, Mathf.Cos(num2) * yRadius, 0f));
					num2 += num3;
					k++;
					this.m_points3[index + k] = origin + matrix4x2.MultiplyPoint3x4(new Vector3(Mathf.Sin(num2) * xRadius, Mathf.Cos(num2) * yRadius, 0f));
				}
			}
		}

		
		public void MakeCurve(Vector2[] curvePoints)
		{
			this.MakeCurve(curvePoints, this.GetSegmentNumber(), 0);
		}

		
		public void MakeCurve(Vector2[] curvePoints, int segments)
		{
			this.MakeCurve(curvePoints, segments, 0);
		}

		
		public void MakeCurve(Vector2[] curvePoints, int segments, int index)
		{
			if (curvePoints.Length != 4)
			{
				Debug.LogError("VectorLine.MakeCurve needs exactly 4 points in the curve points array");
				return;
			}
			this.MakeCurve(curvePoints[0], curvePoints[1], curvePoints[2], curvePoints[3], segments, index);
		}

		
		public void MakeCurve(Vector3[] curvePoints)
		{
			this.MakeCurve(curvePoints, this.GetSegmentNumber(), 0);
		}

		
		public void MakeCurve(Vector3[] curvePoints, int segments)
		{
			this.MakeCurve(curvePoints, segments, 0);
		}

		
		public void MakeCurve(Vector3[] curvePoints, int segments, int index)
		{
			if (curvePoints.Length != 4)
			{
				Debug.LogError("VectorLine.MakeCurve needs exactly 4 points in the curve points array");
				return;
			}
			this.MakeCurve(curvePoints[0], curvePoints[1], curvePoints[2], curvePoints[3], segments, index);
		}

		
		public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2)
		{
			this.MakeCurve(anchor1, control1, anchor2, control2, this.GetSegmentNumber(), 0);
		}

		
		public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2, int segments)
		{
			this.MakeCurve(anchor1, control1, anchor2, control2, segments, 0);
		}

		
		public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2, int segments, int index)
		{
			if (!this.CheckArrayLength(VectorLine.FunctionName.MakeCurve, segments, index))
			{
				return;
			}
			if (this.m_lineType != LineType.Discrete)
			{
				int num = (this.m_lineType == LineType.Points) ? segments : (segments + 1);
				if (this.m_is2D)
				{
					Vector2 vector = anchor1;
					Vector2 vector2 = anchor2;
					Vector2 vector3 = control1;
					Vector2 vector4 = control2;
					for (int i = 0; i < num; i++)
					{
						this.m_points2[index + i] = VectorLine.GetBezierPoint(ref vector, ref vector3, ref vector2, ref vector4, (float)i / (float)segments);
					}
					return;
				}
				for (int j = 0; j < num; j++)
				{
					this.m_points3[index + j] = VectorLine.GetBezierPoint3D(ref anchor1, ref control1, ref anchor2, ref control2, (float)j / (float)segments);
				}
				return;
			}
			else
			{
				int num2 = 0;
				if (this.m_is2D)
				{
					Vector2 vector5 = anchor1;
					Vector2 vector6 = anchor2;
					Vector2 vector7 = control1;
					Vector2 vector8 = control2;
					for (int k = 0; k < segments; k++)
					{
						this.m_points2[index + num2++] = VectorLine.GetBezierPoint(ref vector5, ref vector7, ref vector6, ref vector8, (float)k / (float)segments);
						this.m_points2[index + num2++] = VectorLine.GetBezierPoint(ref vector5, ref vector7, ref vector6, ref vector8, (float)(k + 1) / (float)segments);
					}
					return;
				}
				for (int l = 0; l < segments; l++)
				{
					this.m_points3[index + num2++] = VectorLine.GetBezierPoint3D(ref anchor1, ref control1, ref anchor2, ref control2, (float)l / (float)segments);
					this.m_points3[index + num2++] = VectorLine.GetBezierPoint3D(ref anchor1, ref control1, ref anchor2, ref control2, (float)(l + 1) / (float)segments);
				}
				return;
			}
		}

		
		private static Vector2 GetBezierPoint(ref Vector2 anchor1, ref Vector2 control1, ref Vector2 anchor2, ref Vector2 control2, float t)
		{
			float num = 3f * (control1.x - anchor1.x);
			float num2 = 3f * (control2.x - control1.x) - num;
			float num3 = anchor2.x - anchor1.x - num - num2;
			float num4 = 3f * (control1.y - anchor1.y);
			float num5 = 3f * (control2.y - control1.y) - num4;
			float num6 = anchor2.y - anchor1.y - num4 - num5;
			return new Vector2(num3 * (t * t * t) + num2 * (t * t) + num * t + anchor1.x, num6 * (t * t * t) + num5 * (t * t) + num4 * t + anchor1.y);
		}

		
		private static Vector3 GetBezierPoint3D(ref Vector3 anchor1, ref Vector3 control1, ref Vector3 anchor2, ref Vector3 control2, float t)
		{
			float num = 3f * (control1.x - anchor1.x);
			float num2 = 3f * (control2.x - control1.x) - num;
			float num3 = anchor2.x - anchor1.x - num - num2;
			float num4 = 3f * (control1.y - anchor1.y);
			float num5 = 3f * (control2.y - control1.y) - num4;
			float num6 = anchor2.y - anchor1.y - num4 - num5;
			float num7 = 3f * (control1.z - anchor1.z);
			float num8 = 3f * (control2.z - control1.z) - num7;
			float num9 = anchor2.z - anchor1.z - num7 - num8;
			return new Vector3(num3 * (t * t * t) + num2 * (t * t) + num * t + anchor1.x, num6 * (t * t * t) + num5 * (t * t) + num4 * t + anchor1.y, num9 * (t * t * t) + num8 * (t * t) + num7 * t + anchor1.z);
		}

		
		public void MakeSpline(Vector2[] splinePoints)
		{
			this.MakeSpline(splinePoints, null, this.GetSegmentNumber(), 0, false);
		}

		
		public void MakeSpline(Vector2[] splinePoints, bool loop)
		{
			this.MakeSpline(splinePoints, null, this.GetSegmentNumber(), 0, loop);
		}

		
		public void MakeSpline(Vector2[] splinePoints, int segments)
		{
			this.MakeSpline(splinePoints, null, segments, 0, false);
		}

		
		public void MakeSpline(Vector2[] splinePoints, int segments, bool loop)
		{
			this.MakeSpline(splinePoints, null, segments, 0, loop);
		}

		
		public void MakeSpline(Vector2[] splinePoints, int segments, int index)
		{
			this.MakeSpline(splinePoints, null, segments, index, false);
		}

		
		public void MakeSpline(Vector2[] splinePoints, int segments, int index, bool loop)
		{
			this.MakeSpline(splinePoints, null, segments, index, loop);
		}

		
		public void MakeSpline(Vector3[] splinePoints)
		{
			this.MakeSpline(null, splinePoints, this.GetSegmentNumber(), 0, false);
		}

		
		public void MakeSpline(Vector3[] splinePoints, bool loop)
		{
			this.MakeSpline(null, splinePoints, this.GetSegmentNumber(), 0, loop);
		}

		
		public void MakeSpline(Vector3[] splinePoints, int segments)
		{
			this.MakeSpline(null, splinePoints, segments, 0, false);
		}

		
		public void MakeSpline(Vector3[] splinePoints, int segments, bool loop)
		{
			this.MakeSpline(null, splinePoints, segments, 0, loop);
		}

		
		public void MakeSpline(Vector3[] splinePoints, int segments, int index)
		{
			this.MakeSpline(null, splinePoints, segments, index, false);
		}

		
		public void MakeSpline(Vector3[] splinePoints, int segments, int index, bool loop)
		{
			this.MakeSpline(null, splinePoints, segments, index, loop);
		}

		
		private void MakeSpline(Vector2[] splinePoints2, Vector3[] splinePoints3, int segments, int index, bool loop)
		{
			int num = (splinePoints2 != null) ? splinePoints2.Length : splinePoints3.Length;
			if (num < 2)
			{
				Debug.LogError("VectorLine.MakeSpline needs at least 2 spline points");
				return;
			}
			if (splinePoints2 != null && !this.m_is2D)
			{
				Debug.LogError("VectorLine.MakeSpline was called with a Vector2 spline points array, but the line uses Vector3 points");
				return;
			}
			if (splinePoints3 != null && this.m_is2D)
			{
				Debug.LogError("VectorLine.MakeSpline was called with a Vector3 spline points array, but the line uses Vector2 points");
				return;
			}
			if (!this.CheckArrayLength(VectorLine.FunctionName.MakeSpline, segments, index))
			{
				return;
			}
			int num2 = index;
			int num3 = loop ? num : (num - 1);
			float num4 = 1f / (float)segments * (float)num3;
			float num5 = 0f;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int i;
			for (i = 0; i < num3; i++)
			{
				num6 = i - 1;
				num7 = i + 1;
				num8 = i + 2;
				if (num6 < 0)
				{
					num6 = (loop ? (num3 - 1) : 0);
				}
				if (loop && num7 > num3 - 1)
				{
					num7 -= num3;
				}
				if (num8 > num3 - 1)
				{
					num8 = (loop ? (num8 - num3) : num3);
				}
				float num9;
				if (this.m_lineType != LineType.Discrete)
				{
					if (this.m_is2D)
					{
						for (num9 = num5; num9 <= 1f; num9 += num4)
						{
							this.m_points2[num2++] = VectorLine.GetSplinePoint(ref splinePoints2[num6], ref splinePoints2[i], ref splinePoints2[num7], ref splinePoints2[num8], num9);
						}
					}
					else
					{
						for (num9 = num5; num9 <= 1f; num9 += num4)
						{
							this.m_points3[num2++] = VectorLine.GetSplinePoint3D(ref splinePoints3[num6], ref splinePoints3[i], ref splinePoints3[num7], ref splinePoints3[num8], num9);
						}
					}
				}
				else if (this.m_is2D)
				{
					for (num9 = num5; num9 <= 1f; num9 += num4)
					{
						this.m_points2[num2++] = VectorLine.GetSplinePoint(ref splinePoints2[num6], ref splinePoints2[i], ref splinePoints2[num7], ref splinePoints2[num8], num9);
						if (num2 > index + 1 && num2 < index + segments * 2)
						{
							this.m_points2[num2++] = this.m_points2[num2 - 2];
						}
					}
				}
				else
				{
					for (num9 = num5; num9 <= 1f; num9 += num4)
					{
						this.m_points3[num2++] = VectorLine.GetSplinePoint3D(ref splinePoints3[num6], ref splinePoints3[i], ref splinePoints3[num7], ref splinePoints3[num8], num9);
						if (num2 > index + 1 && num2 < index + segments * 2)
						{
							this.m_points3[num2++] = this.m_points3[num2 - 2];
						}
					}
				}
				num5 = num9 - 1f;
			}
			if ((this.m_lineType != LineType.Discrete && num2 < index + (segments + 1)) || (this.m_lineType == LineType.Discrete && num2 < index + segments * 2))
			{
				if (this.m_is2D)
				{
					this.m_points2[num2] = VectorLine.GetSplinePoint(ref splinePoints2[num6], ref splinePoints2[i - 1], ref splinePoints2[num7], ref splinePoints2[num8], 1f);
					return;
				}
				this.m_points3[num2] = VectorLine.GetSplinePoint3D(ref splinePoints3[num6], ref splinePoints3[i - 1], ref splinePoints3[num7], ref splinePoints3[num8], 1f);
			}
		}

		
		private static Vector2 GetSplinePoint(ref Vector2 p0, ref Vector2 p1, ref Vector2 p2, ref Vector2 p3, float t)
		{
			Vector4 zero = Vector4.zero;
			Vector4 zero2 = Vector4.zero;
			float num = Mathf.Pow(VectorLine.VectorDistanceSquared(ref p0, ref p1), 0.25f);
			float num2 = Mathf.Pow(VectorLine.VectorDistanceSquared(ref p1, ref p2), 0.25f);
			float num3 = Mathf.Pow(VectorLine.VectorDistanceSquared(ref p2, ref p3), 0.25f);
			if (num2 < 0.0001f)
			{
				num2 = 1f;
			}
			if (num < 0.0001f)
			{
				num = num2;
			}
			if (num3 < 0.0001f)
			{
				num3 = num2;
			}
			VectorLine.InitNonuniformCatmullRom(p0.x, p1.x, p2.x, p3.x, num, num2, num3, ref zero);
			VectorLine.InitNonuniformCatmullRom(p0.y, p1.y, p2.y, p3.y, num, num2, num3, ref zero2);
			return new Vector2(VectorLine.EvalCubicPoly(ref zero, t), VectorLine.EvalCubicPoly(ref zero2, t));
		}

		
		private static Vector3 GetSplinePoint3D(ref Vector3 p0, ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, float t)
		{
			Vector4 zero = Vector4.zero;
			Vector4 zero2 = Vector4.zero;
			Vector4 zero3 = Vector4.zero;
			float num = Mathf.Pow(VectorLine.VectorDistanceSquared(ref p0, ref p1), 0.25f);
			float num2 = Mathf.Pow(VectorLine.VectorDistanceSquared(ref p1, ref p2), 0.25f);
			float num3 = Mathf.Pow(VectorLine.VectorDistanceSquared(ref p2, ref p3), 0.25f);
			if (num2 < 0.0001f)
			{
				num2 = 1f;
			}
			if (num < 0.0001f)
			{
				num = num2;
			}
			if (num3 < 0.0001f)
			{
				num3 = num2;
			}
			VectorLine.InitNonuniformCatmullRom(p0.x, p1.x, p2.x, p3.x, num, num2, num3, ref zero);
			VectorLine.InitNonuniformCatmullRom(p0.y, p1.y, p2.y, p3.y, num, num2, num3, ref zero2);
			VectorLine.InitNonuniformCatmullRom(p0.z, p1.z, p2.z, p3.z, num, num2, num3, ref zero3);
			return new Vector3(VectorLine.EvalCubicPoly(ref zero, t), VectorLine.EvalCubicPoly(ref zero2, t), VectorLine.EvalCubicPoly(ref zero3, t));
		}

		
		private static float VectorDistanceSquared(ref Vector2 p, ref Vector2 q)
		{
			float num = q.x - p.x;
			float num2 = q.y - p.y;
			return num * num + num2 * num2;
		}

		
		private static float VectorDistanceSquared(ref Vector3 p, ref Vector3 q)
		{
			float num = q.x - p.x;
			float num2 = q.y - p.y;
			float num3 = q.z - p.z;
			return num * num + num2 * num2 + num3 * num3;
		}

		
		private static void InitNonuniformCatmullRom(float x0, float x1, float x2, float x3, float dt0, float dt1, float dt2, ref Vector4 p)
		{
			float num = ((x1 - x0) / dt0 - (x2 - x0) / (dt0 + dt1) + (x2 - x1) / dt1) * dt1;
			float num2 = ((x2 - x1) / dt1 - (x3 - x1) / (dt1 + dt2) + (x3 - x2) / dt2) * dt1;
			p.x = x1;
			p.y = num;
			p.z = -3f * x1 + 3f * x2 - 2f * num - num2;
			p.w = 2f * x1 - 2f * x2 + num + num2;
		}

		
		private static float EvalCubicPoly(ref Vector4 p, float t)
		{
			return p.x + p.y * t + p.z * (t * t) + p.w * (t * t * t);
		}

		
		public void MakeText(string text, Vector3 startPos, float size)
		{
			this.MakeText(text, startPos, size, 1f, 1.5f, true);
		}

		
		public void MakeText(string text, Vector3 startPos, float size, bool uppercaseOnly)
		{
			this.MakeText(text, startPos, size, 1f, 1.5f, uppercaseOnly);
		}

		
		public void MakeText(string text, Vector3 startPos, float size, float charSpacing, float lineSpacing)
		{
			this.MakeText(text, startPos, size, charSpacing, lineSpacing, true);
		}

		
		public void MakeText(string text, Vector3 startPos, float size, float charSpacing, float lineSpacing, bool uppercaseOnly)
		{
			if (this.m_lineType != LineType.Discrete)
			{
				Debug.LogError("VectorLine.MakeText only works with a discrete line");
				return;
			}
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				int num2 = Convert.ToInt32(text[i]);
				if (num2 < 0 || num2 > 256)
				{
					Debug.LogError("VectorLine.MakeText: Character '" + text[i].ToString() + "' is not valid");
					return;
				}
				if (uppercaseOnly && num2 >= 97 && num2 <= 122)
				{
					num2 -= 32;
				}
				if (VectorChar.data[num2] != null)
				{
					num += VectorChar.data[num2].Length;
				}
			}
			if (num != this.pointsCount)
			{
				this.Resize(num);
			}
			float num3 = 0f;
			float num4 = 0f;
			int num5 = 0;
			Vector2 vector = new Vector2(size, size);
			for (int j = 0; j < text.Length; j++)
			{
				int num6 = Convert.ToInt32(text[j]);
				if (num6 == 10)
				{
					num4 -= lineSpacing;
					num3 = 0f;
				}
				else if (num6 == 32)
				{
					num3 += charSpacing;
				}
				else
				{
					if (uppercaseOnly && num6 >= 97 && num6 <= 122)
					{
						num6 -= 32;
					}
					if (VectorChar.data[num6] != null)
					{
						int num7 = VectorChar.data[num6].Length;
						if (this.m_is2D)
						{
							for (int k = 0; k < num7; k++)
							{
								this.m_points2[num5++] = Vector2.Scale(VectorChar.data[num6][k] + new Vector2(num3, num4), vector) + startPos;
							}
						}
						else
						{
							for (int l = 0; l < num7; l++)
							{
								this.m_points3[num5++] = Vector3.Scale(VectorChar.data[num6][l] + new Vector3(num3, num4, 0f), vector) + startPos;
							}
						}
						num3 += charSpacing;
					}
					else
					{
						num3 += charSpacing;
					}
				}
			}
		}

		
		public void MakeWireframe(Mesh mesh)
		{
			if (this.m_lineType != LineType.Discrete)
			{
				Debug.LogError("VectorLine.MakeWireframe only works with a discrete line");
				return;
			}
			if (this.m_is2D)
			{
				Debug.LogError("VectorLine.MakeWireframe can only be used with Vector3 points, which \"" + this.name + "\" doesn't have");
				return;
			}
			if (mesh == null)
			{
				Debug.LogError("VectorLine.MakeWireframe can't use a null mesh");
				return;
			}
			Vector3[] vertices = mesh.vertices;
			Dictionary<Vector3Pair, bool> pairs = new Dictionary<Vector3Pair, bool>();
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				int[] indices = mesh.GetIndices(i);
				int num = (mesh.GetTopology(i) == MeshTopology.Triangles) ? 3 : 4;
				for (int j = 0; j < indices.Length; j += num)
				{
					for (int k = 0; k < num; k++)
					{
						VectorLine.CheckPairPoints(pairs, vertices[indices[j + k]], vertices[indices[j + (k + 1) % num]], list);
					}
				}
			}
			if (list.Count != this.m_pointsCount)
			{
				this.Resize(list.Count);
			}
			for (int l = 0; l < this.m_pointsCount; l++)
			{
				this.m_points3[l] = list[l];
			}
		}

		
		private static void CheckPairPoints(Dictionary<Vector3Pair, bool> pairs, Vector3 p1, Vector3 p2, List<Vector3> linePoints)
		{
			Vector3Pair key = new Vector3Pair(p1, p2);
			Vector3Pair key2 = new Vector3Pair(p2, p1);
			if (!pairs.ContainsKey(key) && !pairs.ContainsKey(key2))
			{
				pairs[key] = true;
				pairs[key2] = true;
				linePoints.Add(p1);
				linePoints.Add(p2);
			}
		}

		
		public void MakeCube(Vector3 position, float xSize, float ySize, float zSize)
		{
			this.MakeCube(position, xSize, ySize, zSize, 0);
		}

		
		public void MakeCube(Vector3 position, float xSize, float ySize, float zSize, int index)
		{
			if (this.m_lineType != LineType.Discrete)
			{
				Debug.LogError("VectorLine.MakeCube only works with a discrete line");
				return;
			}
			if (this.m_is2D)
			{
				Debug.LogError("VectorLine.MakeCube can only be used with Vector3 points, which \"" + this.name + "\" doesn't have");
				return;
			}
			if (index + 24 <= this.pointsCount)
			{
				xSize /= 2f;
				ySize /= 2f;
				zSize /= 2f;
				this.m_points3[index] = position + new Vector3(-xSize, ySize, -zSize);
				this.m_points3[index + 1] = position + new Vector3(xSize, ySize, -zSize);
				this.m_points3[index + 2] = position + new Vector3(xSize, ySize, -zSize);
				this.m_points3[index + 3] = position + new Vector3(xSize, ySize, zSize);
				this.m_points3[index + 4] = position + new Vector3(xSize, ySize, zSize);
				this.m_points3[index + 5] = position + new Vector3(-xSize, ySize, zSize);
				this.m_points3[index + 6] = position + new Vector3(-xSize, ySize, zSize);
				this.m_points3[index + 7] = position + new Vector3(-xSize, ySize, -zSize);
				this.m_points3[index + 8] = position + new Vector3(-xSize, -ySize, -zSize);
				this.m_points3[index + 9] = position + new Vector3(-xSize, ySize, -zSize);
				this.m_points3[index + 10] = position + new Vector3(xSize, -ySize, -zSize);
				this.m_points3[index + 11] = position + new Vector3(xSize, ySize, -zSize);
				this.m_points3[index + 12] = position + new Vector3(-xSize, -ySize, zSize);
				this.m_points3[index + 13] = position + new Vector3(-xSize, ySize, zSize);
				this.m_points3[index + 14] = position + new Vector3(xSize, -ySize, zSize);
				this.m_points3[index + 15] = position + new Vector3(xSize, ySize, zSize);
				this.m_points3[index + 16] = position + new Vector3(-xSize, -ySize, -zSize);
				this.m_points3[index + 17] = position + new Vector3(xSize, -ySize, -zSize);
				this.m_points3[index + 18] = position + new Vector3(xSize, -ySize, -zSize);
				this.m_points3[index + 19] = position + new Vector3(xSize, -ySize, zSize);
				this.m_points3[index + 20] = position + new Vector3(xSize, -ySize, zSize);
				this.m_points3[index + 21] = position + new Vector3(-xSize, -ySize, zSize);
				this.m_points3[index + 22] = position + new Vector3(-xSize, -ySize, zSize);
				this.m_points3[index + 23] = position + new Vector3(-xSize, -ySize, -zSize);
				return;
			}
			if (index == 0)
			{
				Debug.LogError("VectorLine.MakeCube: The number of Vector3 points needs to be at least 24 for \"" + this.name + "\"");
				return;
			}
			Debug.LogError(string.Concat(new object[]
			{
				"Calling VectorLine.MakeCube with an index of ",
				index,
				" would exceed the length of the Vector3 points for \"",
				this.name,
				"\""
			}));
		}

		
		[SerializeField]
		private Vector3[] m_lineVertices;

		
		[SerializeField]
		private Vector2[] m_lineUVs;

		
		[SerializeField]
		private Color32[] m_lineColors;

		
		[SerializeField]
		private List<int> m_lineTriangles;

		
		[SerializeField]
		private int m_vertexCount;

		
		[SerializeField]
		private GameObject m_go;

		
		[SerializeField]
		private RectTransform m_rectTransform;

		
		private IVectorObject m_vectorObject;

		
		[SerializeField]
		private Color32 m_color;

		
		[SerializeField]
		private CanvasState m_canvasState;

		
		[SerializeField]
		private bool m_is2D;

		
		[SerializeField]
		private List<Vector2> m_points2;

		
		[SerializeField]
		private List<Vector3> m_points3;

		
		[SerializeField]
		private int m_pointsCount;

		
		[SerializeField]
		private Vector3[] m_screenPoints;

		
		[SerializeField]
		private float[] m_lineWidths;

		
		[SerializeField]
		private float m_lineWidth;

		
		[SerializeField]
		private float m_maxWeldDistance;

		
		[SerializeField]
		private float[] m_distances;

		
		[SerializeField]
		private string m_name;

		
		[SerializeField]
		private Material m_material;

		
		[SerializeField]
		private Texture m_originalTexture;

		
		[SerializeField]
		private Texture m_texture;

		
		[SerializeField]
		private bool m_active = true;

		
		[SerializeField]
		private LineType m_lineType;

		
		[SerializeField]
		private float m_capLength;

		
		[SerializeField]
		private bool m_smoothWidth;

		
		[SerializeField]
		private bool m_smoothColor;

		
		[SerializeField]
		private Joins m_joins;

		
		[SerializeField]
		private bool m_isAutoDrawing;

		
		[SerializeField]
		private int m_drawStart;

		
		[SerializeField]
		private int m_drawEnd;

		
		[SerializeField]
		private int m_endPointsUpdate;

		
		[SerializeField]
		private bool m_useNormals;

		
		[SerializeField]
		private bool m_useTangents;

		
		[SerializeField]
		private bool m_normalsCalculated;

		
		[SerializeField]
		private bool m_tangentsCalculated;

		
		[SerializeField]
		private EndCap m_capType = EndCap.None;

		
		[SerializeField]
		private string m_endCap;

		
		[SerializeField]
		private bool m_useCapColors;

		
		[SerializeField]
		private Color32 m_frontColor;

		
		[SerializeField]
		private Color32 m_backColor;

		
		[SerializeField]
		private int m_frontEndCapIndex = -1;

		
		[SerializeField]
		private int m_backEndCapIndex = -1;

		
		[SerializeField]
		private float m_lineUVBottom;

		
		[SerializeField]
		private float m_lineUVTop;

		
		[SerializeField]
		private float m_frontCapUVBottom;

		
		[SerializeField]
		private float m_frontCapUVTop;

		
		[SerializeField]
		private float m_backCapUVBottom;

		
		[SerializeField]
		private float m_backCapUVTop;

		
		[SerializeField]
		private bool m_continuousTexture;

		
		[SerializeField]
		private Transform m_drawTransform;

		
		[SerializeField]
		private bool m_viewportDraw;

		
		[SerializeField]
		private float m_textureScale;

		
		[SerializeField]
		private bool m_useTextureScale;

		
		[SerializeField]
		private float m_textureOffset;

		
		[SerializeField]
		private bool m_useMatrix;

		
		[SerializeField]
		private Matrix4x4 m_matrix;

		
		[SerializeField]
		private bool m_collider;

		
		[SerializeField]
		private bool m_trigger;

		
		[SerializeField]
		private PhysicsMaterial2D m_physicsMaterial;

		
		[SerializeField]
		private bool m_alignOddWidthToPixels;

		
		private static Vector3 v3zero = Vector3.zero;

		
		private static Canvas m_canvas;

		
		private static Transform camTransform;

		
		private static Camera cam3D;

		
		private static Vector3 oldPosition;

		
		private static Vector3 oldRotation;

		
		private static bool lineManagerCreated = false;

		
		private static LineManager m_lineManager;

		
		private static Dictionary<string, CapInfo> capDictionary;

		
		private static int endianDiff1;

		
		private static int endianDiff2;

		
		private static byte[] byteBlock;

		
		private static string[] functionNames = new string[]
		{
			"VectorLine.SetColors: Length of color",
			"VectorLine.SetWidths: Length of line widths",
			"MakeCurve",
			"MakeSpline",
			"MakeEllipse"
		};

		
		private enum FunctionName
		{
			
			SetColors,
			
			SetWidths,
			
			MakeCurve,
			
			MakeSpline,
			
			MakeEllipse
		}
	}
}
