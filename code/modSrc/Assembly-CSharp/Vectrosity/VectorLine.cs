using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vectrosity
{
	// Token: 0x02000388 RID: 904
	[Serializable]
	public class VectorLine
	{
		// Token: 0x06002041 RID: 8257 RVA: 0x000155AD File Offset: 0x000137AD
		public static string Version()
		{
			return "Vectrosity version 5.6";
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x000155B4 File Offset: 0x000137B4
		public Vector3[] lineVertices
		{
			get
			{
				return this.m_lineVertices;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x000155BC File Offset: 0x000137BC
		public Vector2[] lineUVs
		{
			get
			{
				return this.m_lineUVs;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x000155C4 File Offset: 0x000137C4
		public Color32[] lineColors
		{
			get
			{
				return this.m_lineColors;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x000155CC File Offset: 0x000137CC
		public List<int> lineTriangles
		{
			get
			{
				return this.m_lineTriangles;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x000155D4 File Offset: 0x000137D4
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x000155EC File Offset: 0x000137EC
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x000155F4 File Offset: 0x000137F4
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x00015604 File Offset: 0x00013804
		public bool is2D
		{
			get
			{
				return this.m_is2D;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0001560C File Offset: 0x0001380C
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x00015638 File Offset: 0x00013838
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0001565F File Offset: 0x0001385F
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x0001568B File Offset: 0x0001388B
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x000156B2 File Offset: 0x000138B2
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x000156D3 File Offset: 0x000138D3
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x00154180 File Offset: 0x00152380
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x000156DB File Offset: 0x000138DB
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x000156E8 File Offset: 0x000138E8
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000156F3 File Offset: 0x000138F3
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x000156FB File Offset: 0x000138FB
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00015732 File Offset: 0x00013932
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x0001573A File Offset: 0x0001393A
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00015757 File Offset: 0x00013957
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0001575F File Offset: 0x0001395F
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0001578D File Offset: 0x0001398D
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000157AA File Offset: 0x000139AA
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x000157CE File Offset: 0x000139CE
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x000157D6 File Offset: 0x000139D6
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x000157F3 File Offset: 0x000139F3
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x001541D0 File Offset: 0x001523D0
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000157FB File Offset: 0x000139FB
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x00015803 File Offset: 0x00013A03
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00015820 File Offset: 0x00013A20
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x00015828 File Offset: 0x00013A28
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x0001583D File Offset: 0x00013A3D
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x00154294 File Offset: 0x00152494
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

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00015845 File Offset: 0x00013A45
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x001542E8 File Offset: 0x001524E8
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0001584D File Offset: 0x00013A4D
		public bool isAutoDrawing
		{
			get
			{
				return this.m_isAutoDrawing;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00015855 File Offset: 0x00013A55
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x0001585D File Offset: 0x00013A5D
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x00015887 File Offset: 0x00013A87
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0001588F File Offset: 0x00013A8F
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000158BC File Offset: 0x00013ABC
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x000158E0 File Offset: 0x00013AE0
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00015906 File Offset: 0x00013B06
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x00154364 File Offset: 0x00152564
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0001590E File Offset: 0x00013B0E
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x00015916 File Offset: 0x00013B16
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x00015928 File Offset: 0x00013B28
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x00015930 File Offset: 0x00013B30
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00015939 File Offset: 0x00013B39
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x00015941 File Offset: 0x00013B41
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0001595D File Offset: 0x00013B5D
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x00015965 File Offset: 0x00013B65
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x00015990 File Offset: 0x00013B90
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x00015998 File Offset: 0x00013B98
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x000159A7 File Offset: 0x00013BA7
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x000159AF File Offset: 0x00013BAF
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x000159CE File Offset: 0x00013BCE
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x000159F5 File Offset: 0x00013BF5
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x00015A1C File Offset: 0x00013C1C
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x00015A24 File Offset: 0x00013C24
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00015A44 File Offset: 0x00013C44
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x00015A4C File Offset: 0x00013C4C
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x00015A79 File Offset: 0x00013C79
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x00015A81 File Offset: 0x00013C81
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x00015AA1 File Offset: 0x00013CA1
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x00154420 File Offset: 0x00152620
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x00015AA9 File Offset: 0x00013CA9
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x00015AC3 File Offset: 0x00013CC3
		public static Vector3 camTransformPosition
		{
			get
			{
				return VectorLine.camTransform.position;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x00015ACF File Offset: 0x00013CCF
		public static bool camTransformExists
		{
			get
			{
				return VectorLine.camTransform != null;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x00015ADC File Offset: 0x00013CDC
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

		// Token: 0x0600208A RID: 8330 RVA: 0x00154458 File Offset: 0x00152658
		private void AddColliderIfNeeded()
		{
			if (this.m_go.GetComponent<Collider2D>() == null)
			{
				this.m_go.AddComponent((this.m_lineType == LineType.Continuous) ? typeof(EdgeCollider2D) : typeof(PolygonCollider2D));
				this.m_go.GetComponent<Collider2D>().isTrigger = this.m_trigger;
				this.m_go.GetComponent<Collider2D>().sharedMaterial = this.m_physicsMaterial;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00015B19 File Offset: 0x00013D19
		public VectorLine(string name, List<Vector3> points, float width)
		{
			this.m_points3 = points;
			this.SetupLine(name, null, width, LineType.Discrete, Joins.None, false);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00015B50 File Offset: 0x00013D50
		public VectorLine(string name, List<Vector3> points, Texture texture, float width)
		{
			this.m_points3 = points;
			this.SetupLine(name, texture, width, LineType.Discrete, Joins.None, false);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00015B88 File Offset: 0x00013D88
		public VectorLine(string name, List<Vector3> points, float width, LineType lineType)
		{
			this.m_points3 = points;
			this.SetupLine(name, null, width, lineType, Joins.None, false);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00015BC0 File Offset: 0x00013DC0
		public VectorLine(string name, List<Vector3> points, Texture texture, float width, LineType lineType)
		{
			this.m_points3 = points;
			this.SetupLine(name, texture, width, lineType, Joins.None, false);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00015BF9 File Offset: 0x00013DF9
		public VectorLine(string name, List<Vector3> points, float width, LineType lineType, Joins joins)
		{
			this.m_points3 = points;
			this.SetupLine(name, null, width, lineType, joins, false);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00015C32 File Offset: 0x00013E32
		public VectorLine(string name, List<Vector3> points, Texture texture, float width, LineType lineType, Joins joins)
		{
			this.m_points3 = points;
			this.SetupLine(name, texture, width, lineType, joins, false);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00015C6C File Offset: 0x00013E6C
		public VectorLine(string name, List<Vector2> points, float width)
		{
			this.m_points2 = points;
			this.SetupLine(name, null, width, LineType.Discrete, Joins.None, true);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00015CA3 File Offset: 0x00013EA3
		public VectorLine(string name, List<Vector2> points, Texture texture, float width)
		{
			this.m_points2 = points;
			this.SetupLine(name, texture, width, LineType.Discrete, Joins.None, true);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00015CDB File Offset: 0x00013EDB
		public VectorLine(string name, List<Vector2> points, float width, LineType lineType)
		{
			this.m_points2 = points;
			this.SetupLine(name, null, width, lineType, Joins.None, true);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00015D13 File Offset: 0x00013F13
		public VectorLine(string name, List<Vector2> points, Texture texture, float width, LineType lineType)
		{
			this.m_points2 = points;
			this.SetupLine(name, texture, width, lineType, Joins.None, true);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00015D4C File Offset: 0x00013F4C
		public VectorLine(string name, List<Vector2> points, float width, LineType lineType, Joins joins)
		{
			this.m_points2 = points;
			this.SetupLine(name, null, width, lineType, joins, true);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00015D85 File Offset: 0x00013F85
		public VectorLine(string name, List<Vector2> points, Texture texture, float width, LineType lineType, Joins joins)
		{
			this.m_points2 = points;
			this.SetupLine(name, texture, width, lineType, joins, true);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x001544D0 File Offset: 0x001526D0
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

		// Token: 0x06002098 RID: 8344 RVA: 0x00154784 File Offset: 0x00152984
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

		// Token: 0x06002099 RID: 8345 RVA: 0x00154A14 File Offset: 0x00152C14
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

		// Token: 0x0600209A RID: 8346 RVA: 0x00154BD8 File Offset: 0x00152DD8
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

		// Token: 0x0600209B RID: 8347 RVA: 0x00154E18 File Offset: 0x00153018
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

		// Token: 0x0600209C RID: 8348 RVA: 0x00154EE0 File Offset: 0x001530E0
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

		// Token: 0x0600209D RID: 8349 RVA: 0x001550AC File Offset: 0x001532AC
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

		// Token: 0x0600209E RID: 8350 RVA: 0x0015514C File Offset: 0x0015334C
		private static void SetupTransform(RectTransform rectTransform)
		{
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.zero;
			rectTransform.pivot = Vector2.zero;
			rectTransform.anchoredPosition = Vector2.zero;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00015DBF File Offset: 0x00013FBF
		private void ResizeMeshArrays(int newCount)
		{
			Array.Resize<Vector3>(ref this.m_lineVertices, newCount);
			Array.Resize<Vector2>(ref this.m_lineUVs, newCount);
			Array.Resize<Color32>(ref this.m_lineColors, newCount);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0015519C File Offset: 0x0015339C
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

		// Token: 0x060020A1 RID: 8353 RVA: 0x00155258 File Offset: 0x00153458
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

		// Token: 0x060020A2 RID: 8354 RVA: 0x0015542C File Offset: 0x0015362C
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

		// Token: 0x060020A3 RID: 8355 RVA: 0x00155494 File Offset: 0x00153694
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

		// Token: 0x060020A4 RID: 8356 RVA: 0x00155558 File Offset: 0x00153758
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

		// Token: 0x060020A5 RID: 8357 RVA: 0x00015DE5 File Offset: 0x00013FE5
		private int MaxPoints()
		{
			if (this.m_capType != EndCap.None)
			{
				return 16381;
			}
			return 16383;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00015DFB File Offset: 0x00013FFB
		public void AddNormals()
		{
			this.m_useNormals = true;
			this.m_normalsCalculated = false;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00015E0B File Offset: 0x0001400B
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

		// Token: 0x060020A8 RID: 8360 RVA: 0x001555D4 File Offset: 0x001537D4
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

		// Token: 0x060020A9 RID: 8361 RVA: 0x0015591C File Offset: 0x00153B1C
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

		// Token: 0x060020AA RID: 8362 RVA: 0x00015E31 File Offset: 0x00014031
		public static void SetCanvasCamera(Camera cam)
		{
			if (VectorLine.m_canvas == null)
			{
				VectorLine.SetupVectorCanvas();
			}
			VectorLine.m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
			VectorLine.m_canvas.worldCamera = cam;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00015E5C File Offset: 0x0001405C
		public void SetCanvas(GameObject canvasObject)
		{
			this.SetCanvas(canvasObject, true);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00155980 File Offset: 0x00153B80
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

		// Token: 0x060020AD RID: 8365 RVA: 0x00015E66 File Offset: 0x00014066
		public void SetCanvas(Canvas canvas)
		{
			this.SetCanvas(canvas, true);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x001559B0 File Offset: 0x00153BB0
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

		// Token: 0x060020AF RID: 8367 RVA: 0x00015E70 File Offset: 0x00014070
		public void SetMask(GameObject maskObject)
		{
			this.SetMask(maskObject, true);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00155A10 File Offset: 0x00153C10
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

		// Token: 0x060020B1 RID: 8369 RVA: 0x00015E7A File Offset: 0x0001407A
		public void SetMask(Mask mask)
		{
			this.SetMask(mask, true);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00155A40 File Offset: 0x00153C40
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

		// Token: 0x060020B3 RID: 8371 RVA: 0x00015E84 File Offset: 0x00014084
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

		// Token: 0x060020B4 RID: 8372 RVA: 0x00015EB8 File Offset: 0x000140B8
		public static void SetCamera3D()
		{
			if (Camera.main == null)
			{
				Debug.LogError("VectorLine.SetCamera3D: no camera tagged \"Main Camera\" found. Please call SetCamera3D with a specific camera instead.");
				return;
			}
			VectorLine.SetCamera3D(Camera.main);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00155A8C File Offset: 0x00153C8C
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

		// Token: 0x060020B6 RID: 8374 RVA: 0x00155ABC File Offset: 0x00153CBC
		public static void SetCamera3D(Camera camera)
		{
			VectorLine.camTransform = camera.transform;
			VectorLine.cam3D = camera;
			VectorLine.oldPosition = VectorLine.camTransform.position + Vector3.one;
			VectorLine.oldRotation = VectorLine.camTransform.eulerAngles + Vector3.one;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00015EDC File Offset: 0x000140DC
		public static bool CameraHasMoved()
		{
			return VectorLine.oldPosition != VectorLine.camTransform.position || VectorLine.oldRotation != VectorLine.camTransform.eulerAngles;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00015F0A File Offset: 0x0001410A
		public static void UpdateCameraInfo()
		{
			VectorLine.oldPosition = VectorLine.camTransform.position;
			VectorLine.oldRotation = VectorLine.camTransform.eulerAngles;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00015F2A File Offset: 0x0001412A
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

		// Token: 0x060020BA RID: 8378 RVA: 0x00155B0C File Offset: 0x00153D0C
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

		// Token: 0x060020BB RID: 8379 RVA: 0x00015F5F File Offset: 0x0001415F
		public void SetEndCapColor(Color32 color)
		{
			this.SetEndCapColor(color, color);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00155C2C File Offset: 0x00153E2C
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

		// Token: 0x060020BD RID: 8381 RVA: 0x00155C78 File Offset: 0x00153E78
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

		// Token: 0x060020BE RID: 8382 RVA: 0x00015F69 File Offset: 0x00014169
		public void SetColor(Color32 color)
		{
			this.SetColor(color, 0, this.pointsCount);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00015F79 File Offset: 0x00014179
		public void SetColor(Color32 color, int index)
		{
			this.SetColor(color, index, index);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00155CD8 File Offset: 0x00153ED8
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

		// Token: 0x060020C1 RID: 8385 RVA: 0x00155DE0 File Offset: 0x00153FE0
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

		// Token: 0x060020C2 RID: 8386 RVA: 0x00155FCC File Offset: 0x001541CC
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

		// Token: 0x060020C3 RID: 8387 RVA: 0x00156020 File Offset: 0x00154220
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

		// Token: 0x060020C4 RID: 8388 RVA: 0x00015F84 File Offset: 0x00014184
		private void SetupWidths(int max)
		{
			if ((max >= 2 && this.m_lineWidths.Length == 1) || (max >= 2 && this.m_lineWidths.Length != max))
			{
				this.ResizeLineWidths(max);
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00015FAB File Offset: 0x000141AB
		public void SetWidth(float width)
		{
			this.m_lineWidth = width;
			this.SetWidth(width, 0, this.pointsCount);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00015FC2 File Offset: 0x000141C2
		public void SetWidth(float width, int index)
		{
			this.SetWidth(width, index, index);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00156094 File Offset: 0x00154294
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

		// Token: 0x060020C8 RID: 8392 RVA: 0x00015FCD File Offset: 0x000141CD
		public void SetWidths(List<float> lineWidths)
		{
			this.SetWidths(lineWidths, null, lineWidths.Count, true);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00015FDE File Offset: 0x000141DE
		public void SetWidths(List<int> lineWidths)
		{
			this.SetWidths(null, lineWidths, lineWidths.Count, false);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00156104 File Offset: 0x00154304
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

		// Token: 0x060020CB RID: 8395 RVA: 0x001561D8 File Offset: 0x001543D8
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

		// Token: 0x060020CC RID: 8396 RVA: 0x00015FEF File Offset: 0x000141EF
		public static VectorLine SetLine(Color color, params Vector2[] points)
		{
			return VectorLine.SetLine(color, 0f, points);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00156260 File Offset: 0x00154460
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

		// Token: 0x060020CE RID: 8398 RVA: 0x00015FFD File Offset: 0x000141FD
		public static VectorLine SetLine(Color color, params Vector3[] points)
		{
			return VectorLine.SetLine(color, 0f, points);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x001562C0 File Offset: 0x001544C0
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

		// Token: 0x060020D0 RID: 8400 RVA: 0x0001600B File Offset: 0x0001420B
		public static VectorLine SetLine3D(Color color, params Vector3[] points)
		{
			return VectorLine.SetLine3D(color, 0f, points);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00016019 File Offset: 0x00014219
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

		// Token: 0x060020D2 RID: 8402 RVA: 0x00016058 File Offset: 0x00014258
		public static VectorLine SetRay(Color color, Vector3 origin, Vector3 direction)
		{
			return VectorLine.SetRay(color, 0f, origin, direction);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00156320 File Offset: 0x00154520
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

		// Token: 0x060020D4 RID: 8404 RVA: 0x00016067 File Offset: 0x00014267
		public static VectorLine SetRay3D(Color color, Vector3 origin, Vector3 direction)
		{
			return VectorLine.SetRay3D(color, 0f, origin, direction);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00156398 File Offset: 0x00154598
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

		// Token: 0x060020D6 RID: 8406 RVA: 0x001563FC File Offset: 0x001545FC
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

		// Token: 0x060020D7 RID: 8407 RVA: 0x00016076 File Offset: 0x00014276
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

		// Token: 0x060020D8 RID: 8408 RVA: 0x00156450 File Offset: 0x00154650
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

		// Token: 0x060020D9 RID: 8409 RVA: 0x00156CB8 File Offset: 0x00154EB8
		private void ScaleCapVertices(int offset, float scale, Vector3 center)
		{
			this.m_lineVertices[offset] = (this.m_lineVertices[offset] - center) * scale + center;
			this.m_lineVertices[offset + 1] = (this.m_lineVertices[offset + 1] - center) * scale + center;
			this.m_lineVertices[offset + 2] = (this.m_lineVertices[offset + 2] - center) * scale + center;
			this.m_lineVertices[offset + 3] = (this.m_lineVertices[offset + 3] - center) * scale + center;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00156D7C File Offset: 0x00154F7C
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

		// Token: 0x060020DB RID: 8411 RVA: 0x00156E34 File Offset: 0x00155034
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

		// Token: 0x060020DC RID: 8412 RVA: 0x000160A4 File Offset: 0x000142A4
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

		// Token: 0x060020DD RID: 8413 RVA: 0x000160E2 File Offset: 0x000142E2
		private void ClearTriangles()
		{
			if (this.m_capType == EndCap.None)
			{
				this.m_lineTriangles.Clear();
				return;
			}
			this.m_lineTriangles.RemoveRange(12, this.m_lineTriangles.Count - 12);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00156E84 File Offset: 0x00155084
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

		// Token: 0x060020DF RID: 8415 RVA: 0x00156F24 File Offset: 0x00155124
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

		// Token: 0x060020E0 RID: 8416 RVA: 0x00156FE4 File Offset: 0x001551E4
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

		// Token: 0x060020E1 RID: 8417 RVA: 0x001571EC File Offset: 0x001553EC
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

		// Token: 0x060020E2 RID: 8418 RVA: 0x001572CC File Offset: 0x001554CC
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

		// Token: 0x060020E3 RID: 8419 RVA: 0x00157844 File Offset: 0x00155A44
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

		// Token: 0x060020E4 RID: 8420 RVA: 0x00157DFC File Offset: 0x00155FFC
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

		// Token: 0x060020E5 RID: 8421 RVA: 0x00157E80 File Offset: 0x00156080
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

		// Token: 0x060020E6 RID: 8422 RVA: 0x001584B0 File Offset: 0x001566B0
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

		// Token: 0x060020E7 RID: 8423 RVA: 0x001585AC File Offset: 0x001567AC
		private Vector3 PlaneIntersectionPoint(ref Ray ray, ref Plane plane, ref Vector3 p1, ref Vector3 p2)
		{
			ray.origin = p1;
			ray.direction = p2 - p1;
			float distance = 0f;
			plane.Raycast(ray, out distance);
			return ray.GetPoint(distance);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x001585FC File Offset: 0x001567FC
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

		// Token: 0x060020E9 RID: 8425 RVA: 0x00158A6C File Offset: 0x00156C6C
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

		// Token: 0x060020EA RID: 8426 RVA: 0x00158C1C File Offset: 0x00156E1C
		private void SkipQuad(ref int idx, ref int widthIdx, ref int widthIdxAdd)
		{
			this.m_lineVertices[idx] = VectorLine.v3zero;
			this.m_lineVertices[idx + 1] = VectorLine.v3zero;
			this.m_lineVertices[idx + 2] = VectorLine.v3zero;
			this.m_lineVertices[idx + 3] = VectorLine.v3zero;
			idx += 4;
			widthIdx += widthIdxAdd;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00158C84 File Offset: 0x00156E84
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

		// Token: 0x060020EC RID: 8428 RVA: 0x00158D3C File Offset: 0x00156F3C
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

		// Token: 0x060020ED RID: 8429 RVA: 0x00158DAC File Offset: 0x00156FAC
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

		// Token: 0x060020EE RID: 8430 RVA: 0x00158EA4 File Offset: 0x001570A4
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

		// Token: 0x060020EF RID: 8431 RVA: 0x00159080 File Offset: 0x00157280
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

		// Token: 0x060020F0 RID: 8432 RVA: 0x001590FC File Offset: 0x001572FC
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

		// Token: 0x060020F1 RID: 8433 RVA: 0x00159198 File Offset: 0x00157398
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

		// Token: 0x060020F2 RID: 8434 RVA: 0x00016114 File Offset: 0x00014314
		public static void LineManagerCheckDistance()
		{
			VectorLine.lineManager.StartCheckDistance();
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00016120 File Offset: 0x00014320
		public static void LineManagerDisable()
		{
			VectorLine.lineManager.DisableIfUnused();
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0001612C File Offset: 0x0001432C
		public static void LineManagerEnable()
		{
			VectorLine.lineManager.EnableIfUsed();
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00016138 File Offset: 0x00014338
		public void Draw3DAuto()
		{
			this.Draw3DAuto(0f);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00016145 File Offset: 0x00014345
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

		// Token: 0x060020F7 RID: 8439 RVA: 0x00016175 File Offset: 0x00014375
		public void StopDrawing3DAuto()
		{
			VectorLine.lineManager.RemoveLine(this);
			this.m_isAutoDrawing = false;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00159394 File Offset: 0x00157594
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

		// Token: 0x060020F9 RID: 8441 RVA: 0x001598B8 File Offset: 0x00157AB8
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

		// Token: 0x060020FA RID: 8442 RVA: 0x00159948 File Offset: 0x00157B48
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

		// Token: 0x060020FB RID: 8443 RVA: 0x00159C5C File Offset: 0x00157E5C
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

		// Token: 0x060020FC RID: 8444 RVA: 0x00159D8C File Offset: 0x00157F8C
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

		// Token: 0x060020FD RID: 8445 RVA: 0x00159EF4 File Offset: 0x001580F4
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

		// Token: 0x060020FE RID: 8446 RVA: 0x0015A010 File Offset: 0x00158210
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

		// Token: 0x060020FF RID: 8447 RVA: 0x0015A170 File Offset: 0x00158370
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

		// Token: 0x06002100 RID: 8448 RVA: 0x0015A1D8 File Offset: 0x001583D8
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

		// Token: 0x06002101 RID: 8449 RVA: 0x00016189 File Offset: 0x00014389
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

		// Token: 0x06002102 RID: 8450 RVA: 0x0015A234 File Offset: 0x00158434
		private static float ConvertToFloat(byte[] bytes, int i)
		{
			VectorLine.byteBlock[VectorLine.endianDiff1] = bytes[i];
			VectorLine.byteBlock[1 + VectorLine.endianDiff2] = bytes[i + 1];
			VectorLine.byteBlock[2 - VectorLine.endianDiff2] = bytes[i + 2];
			VectorLine.byteBlock[3 - VectorLine.endianDiff1] = bytes[i + 3];
			return BitConverter.ToSingle(VectorLine.byteBlock, 0);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000161BD File Offset: 0x000143BD
		public static void Destroy(ref VectorLine line)
		{
			VectorLine.DestroyLine(ref line);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0015A290 File Offset: 0x00158490
		public static void Destroy(VectorLine[] lines)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				VectorLine.DestroyLine(ref lines[i]);
			}
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0015A2B8 File Offset: 0x001584B8
		public static void Destroy(List<VectorLine> lines)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				VectorLine vectorLine = lines[i];
				VectorLine.DestroyLine(ref vectorLine);
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000161C5 File Offset: 0x000143C5
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

		// Token: 0x06002107 RID: 8455 RVA: 0x000161FF File Offset: 0x000143FF
		public static void Destroy(ref VectorLine line, GameObject go)
		{
			VectorLine.Destroy(ref line);
			if (go != null)
			{
				UnityEngine.Object.Destroy(go);
			}
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0015A2E8 File Offset: 0x001584E8
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

		// Token: 0x06002109 RID: 8457 RVA: 0x0015A548 File Offset: 0x00158748
		public float GetLength()
		{
			if (this.m_distances == null || this.m_distances.Length != ((this.m_lineType != LineType.Discrete) ? this.pointsCount : (this.pointsCount / 2 + 1)))
			{
				this.SetDistances();
			}
			return this.m_distances[this.m_distances.Length - 1];
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0015A59C File Offset: 0x0015879C
		public Vector2 GetPoint01(float distance)
		{
			int num;
			return this.GetPoint(Mathf.Lerp(0f, this.GetLength(), distance), out num);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00016216 File Offset: 0x00014416
		public Vector2 GetPoint01(float distance, out int index)
		{
			return this.GetPoint(Mathf.Lerp(0f, this.GetLength(), distance), out index);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0015A5C4 File Offset: 0x001587C4
		public Vector2 GetPoint(float distance)
		{
			int num;
			return this.GetPoint(distance, out num);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0015A5DC File Offset: 0x001587DC
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

		// Token: 0x0600210E RID: 8462 RVA: 0x0015A6CC File Offset: 0x001588CC
		public Vector3 GetPoint3D01(float distance)
		{
			int num;
			return this.GetPoint3D(Mathf.Lerp(0f, this.GetLength(), distance), out num);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00016230 File Offset: 0x00014430
		public Vector3 GetPoint3D01(float distance, out int index)
		{
			return this.GetPoint3D(Mathf.Lerp(0f, this.GetLength(), distance), out index);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0015A6F4 File Offset: 0x001588F4
		public Vector3 GetPoint3D(float distance)
		{
			int num;
			return this.GetPoint3D(distance, out num);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0015A70C File Offset: 0x0015890C
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

		// Token: 0x06002112 RID: 8466 RVA: 0x0015A7F0 File Offset: 0x001589F0
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

		// Token: 0x06002113 RID: 8467 RVA: 0x0001624A File Offset: 0x0001444A
		public static void SetEndCap(string name, EndCap capType)
		{
			VectorLine.SetEndCap(name, capType, 0f, 0f, 1f, 1f, null);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00016268 File Offset: 0x00014468
		public static void SetEndCap(string name, EndCap capType, params Texture2D[] textures)
		{
			VectorLine.SetEndCap(name, capType, 0f, 0f, 1f, 1f, textures);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00016286 File Offset: 0x00014486
		public static void SetEndCap(string name, EndCap capType, float offset, params Texture2D[] textures)
		{
			VectorLine.SetEndCap(name, capType, offset, offset, 1f, 1f, textures);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0001629C File Offset: 0x0001449C
		public static void SetEndCap(string name, EndCap capType, float offsetFront, float offsetBack, params Texture2D[] textures)
		{
			VectorLine.SetEndCap(name, capType, offsetFront, offsetBack, 1f, 1f, textures);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0015A870 File Offset: 0x00158A70
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

		// Token: 0x06002118 RID: 8472 RVA: 0x0015ACF4 File Offset: 0x00158EF4
		private static Color32[] GetRowPixels(Color32[] texPixels, int numberOfRows, int row, int w)
		{
			Color32[] array = new Color32[w * numberOfRows];
			for (int i = 0; i < numberOfRows; i++)
			{
				Array.Copy(texPixels, row * w, array, i * w, w);
			}
			return array;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0015AD28 File Offset: 0x00158F28
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

		// Token: 0x0600211A RID: 8474 RVA: 0x0015ADA8 File Offset: 0x00158FA8
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

		// Token: 0x0600211B RID: 8475 RVA: 0x0015ADFC File Offset: 0x00158FFC
		public bool Selected(Vector2 p)
		{
			int num;
			return this.Selected(p, 0, 0, out num, VectorLine.cam3D);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000162B3 File Offset: 0x000144B3
		public bool Selected(Vector2 p, out int index)
		{
			return this.Selected(p, 0, 0, out index, VectorLine.cam3D);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000162C4 File Offset: 0x000144C4
		public bool Selected(Vector2 p, int extraDistance, out int index)
		{
			return this.Selected(p, extraDistance, 0, out index, VectorLine.cam3D);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000162D5 File Offset: 0x000144D5
		public bool Selected(Vector2 p, int extraDistance, int extraLength, out int index)
		{
			return this.Selected(p, extraDistance, extraLength, out index, VectorLine.cam3D);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0015AE1C File Offset: 0x0015901C
		public bool Selected(Vector2 p, Camera cam)
		{
			int num;
			return this.Selected(p, 0, 0, out num, cam);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000162E7 File Offset: 0x000144E7
		public bool Selected(Vector2 p, out int index, Camera cam)
		{
			return this.Selected(p, 0, 0, out index, cam);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000162F4 File Offset: 0x000144F4
		public bool Selected(Vector2 p, int extraDistance, out int index, Camera cam)
		{
			return this.Selected(p, extraDistance, 0, out index, cam);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0015AE38 File Offset: 0x00159038
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

		// Token: 0x06002123 RID: 8483 RVA: 0x00016302 File Offset: 0x00014502
		private bool Approximately(Vector2 p1, Vector2 p2)
		{
			return this.Approximately(p1.x, p2.x) && this.Approximately(p1.y, p2.y);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0001632C File Offset: 0x0001452C
		private bool Approximately(Vector3 p1, Vector3 p2)
		{
			return this.Approximately(p1.x, p2.x) && this.Approximately(p1.y, p2.y) && this.Approximately(p1.z, p2.z);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0001636A File Offset: 0x0001456A
		private bool Approximately(float a, float b)
		{
			return Mathf.Round(a * 100f) / 100f == Mathf.Round(b * 100f) / 100f;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0015B530 File Offset: 0x00159730
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

		// Token: 0x06002127 RID: 8487 RVA: 0x0015B60C File Offset: 0x0015980C
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

		// Token: 0x06002128 RID: 8488 RVA: 0x0015B89C File Offset: 0x00159A9C
		public void MakeRect(Rect rect)
		{
			this.MakeRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), 0);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0015B8F0 File Offset: 0x00159AF0
		public void MakeRect(Rect rect, int index)
		{
			this.MakeRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), index);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00016392 File Offset: 0x00014592
		public void MakeRect(Vector3 bottomLeft, Vector3 topRight)
		{
			this.MakeRect(bottomLeft, topRight, 0);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0015B944 File Offset: 0x00159B44
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

		// Token: 0x0600212C RID: 8492 RVA: 0x0015BDAC File Offset: 0x00159FAC
		public void MakeRoundedRect(Rect rect, float cornerRadius, int cornerSegments)
		{
			this.MakeRoundedRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), cornerRadius, cornerSegments, 0);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0015BE04 File Offset: 0x0015A004
		public void MakeRoundedRect(Rect rect, float cornerRadius, int cornerSegments, int index)
		{
			this.MakeRoundedRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), cornerRadius, cornerSegments, index);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0001639D File Offset: 0x0001459D
		public void MakeRoundedRect(Vector3 bottomLeft, Vector3 topRight, float cornerRadius, int cornerSegments)
		{
			this.MakeRoundedRect(bottomLeft, topRight, cornerRadius, cornerSegments, 0);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0015BE5C File Offset: 0x0015A05C
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

		// Token: 0x06002130 RID: 8496 RVA: 0x0015C1D0 File Offset: 0x0015A3D0
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

		// Token: 0x06002131 RID: 8497 RVA: 0x0015C328 File Offset: 0x0015A528
		private void Exchange(ref Vector3 v1, ref Vector3 v2, int i)
		{
			float value = v1[i];
			v1[i] = v2[i];
			v2[i] = value;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0015C354 File Offset: 0x0015A554
		public void MakeCircle(Vector3 origin, float radius)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0015C388 File Offset: 0x0015A588
		public void MakeCircle(Vector3 origin, float radius, int segments)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, 0f, 0);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0015C3B4 File Offset: 0x0015A5B4
		public void MakeCircle(Vector3 origin, float radius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, pointRotation, 0);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0015C3E0 File Offset: 0x0015A5E0
		public void MakeCircle(Vector3 origin, float radius, int segments, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, 0f, index);
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0015C410 File Offset: 0x0015A610
		public void MakeCircle(Vector3 origin, float radius, int segments, float pointRotation, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, radius, radius, 0f, 0f, segments, pointRotation, index);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0015C43C File Offset: 0x0015A63C
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0015C46C File Offset: 0x0015A66C
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, 0f, 0);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0015C498 File Offset: 0x0015A698
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, pointRotation, 0);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0015C4C0 File Offset: 0x0015A6C0
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, int index)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, 0f, index);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0015C4EC File Offset: 0x0015A6EC
		public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, float pointRotation, int index)
		{
			this.MakeEllipse(origin, upVector, radius, radius, 0f, 0f, segments, pointRotation, index);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0015C514 File Offset: 0x0015A714
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0015C548 File Offset: 0x0015A748
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, segments, 0f, 0);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0015C578 File Offset: 0x0015A778
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, segments, 0f, index);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0015C5A8 File Offset: 0x0015A7A8
		public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, 0f, 0f, segments, pointRotation, 0);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0015C5D4 File Offset: 0x0015A7D4
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, this.GetSegmentNumber(), 0f, 0);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0015C604 File Offset: 0x0015A804
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, 0f, 0);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0015C630 File Offset: 0x0015A830
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, int index)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, 0f, index);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0015C65C File Offset: 0x0015A85C
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, float pointRotation)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, pointRotation, 0);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0015C684 File Offset: 0x0015A884
		public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, float pointRotation, int index)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, 0f, 0f, segments, pointRotation, index);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0015C6AC File Offset: 0x0015A8AC
		public void MakeArc(Vector3 origin, float xRadius, float yRadius, float startDegrees, float endDegrees)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, startDegrees, endDegrees, this.GetSegmentNumber(), 0f, 0);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0015C6D8 File Offset: 0x0015A8D8
		public void MakeArc(Vector3 origin, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, 0);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0015C700 File Offset: 0x0015A900
		public void MakeArc(Vector3 origin, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments, int index)
		{
			this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, index);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0015C728 File Offset: 0x0015A928
		public void MakeArc(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, startDegrees, endDegrees, this.GetSegmentNumber(), 0f, 0);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0015C750 File Offset: 0x0015A950
		public void MakeArc(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, 0);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0015C774 File Offset: 0x0015A974
		public void MakeArc(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, float startDegrees, float endDegrees, int segments, int index)
		{
			this.MakeEllipse(origin, upVector, xRadius, yRadius, startDegrees, endDegrees, segments, 0f, index);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0015C79C File Offset: 0x0015A99C
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

		// Token: 0x0600214C RID: 8524 RVA: 0x000163AB File Offset: 0x000145AB
		public void MakeCurve(Vector2[] curvePoints)
		{
			this.MakeCurve(curvePoints, this.GetSegmentNumber(), 0);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000163BB File Offset: 0x000145BB
		public void MakeCurve(Vector2[] curvePoints, int segments)
		{
			this.MakeCurve(curvePoints, segments, 0);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0015CACC File Offset: 0x0015ACCC
		public void MakeCurve(Vector2[] curvePoints, int segments, int index)
		{
			if (curvePoints.Length != 4)
			{
				Debug.LogError("VectorLine.MakeCurve needs exactly 4 points in the curve points array");
				return;
			}
			this.MakeCurve(curvePoints[0], curvePoints[1], curvePoints[2], curvePoints[3], segments, index);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000163C6 File Offset: 0x000145C6
		public void MakeCurve(Vector3[] curvePoints)
		{
			this.MakeCurve(curvePoints, this.GetSegmentNumber(), 0);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000163D6 File Offset: 0x000145D6
		public void MakeCurve(Vector3[] curvePoints, int segments)
		{
			this.MakeCurve(curvePoints, segments, 0);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000163E1 File Offset: 0x000145E1
		public void MakeCurve(Vector3[] curvePoints, int segments, int index)
		{
			if (curvePoints.Length != 4)
			{
				Debug.LogError("VectorLine.MakeCurve needs exactly 4 points in the curve points array");
				return;
			}
			this.MakeCurve(curvePoints[0], curvePoints[1], curvePoints[2], curvePoints[3], segments, index);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00016418 File Offset: 0x00014618
		public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2)
		{
			this.MakeCurve(anchor1, control1, anchor2, control2, this.GetSegmentNumber(), 0);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0001642C File Offset: 0x0001462C
		public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2, int segments)
		{
			this.MakeCurve(anchor1, control1, anchor2, control2, segments, 0);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0015CB24 File Offset: 0x0015AD24
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

		// Token: 0x06002155 RID: 8533 RVA: 0x0015CCE8 File Offset: 0x0015AEE8
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

		// Token: 0x06002156 RID: 8534 RVA: 0x0015CDB0 File Offset: 0x0015AFB0
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

		// Token: 0x06002157 RID: 8535 RVA: 0x0001643C File Offset: 0x0001463C
		public void MakeSpline(Vector2[] splinePoints)
		{
			this.MakeSpline(splinePoints, null, this.GetSegmentNumber(), 0, false);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0001644E File Offset: 0x0001464E
		public void MakeSpline(Vector2[] splinePoints, bool loop)
		{
			this.MakeSpline(splinePoints, null, this.GetSegmentNumber(), 0, loop);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00016460 File Offset: 0x00014660
		public void MakeSpline(Vector2[] splinePoints, int segments)
		{
			this.MakeSpline(splinePoints, null, segments, 0, false);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0001646D File Offset: 0x0001466D
		public void MakeSpline(Vector2[] splinePoints, int segments, bool loop)
		{
			this.MakeSpline(splinePoints, null, segments, 0, loop);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0001647A File Offset: 0x0001467A
		public void MakeSpline(Vector2[] splinePoints, int segments, int index)
		{
			this.MakeSpline(splinePoints, null, segments, index, false);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00016487 File Offset: 0x00014687
		public void MakeSpline(Vector2[] splinePoints, int segments, int index, bool loop)
		{
			this.MakeSpline(splinePoints, null, segments, index, loop);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00016495 File Offset: 0x00014695
		public void MakeSpline(Vector3[] splinePoints)
		{
			this.MakeSpline(null, splinePoints, this.GetSegmentNumber(), 0, false);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000164A7 File Offset: 0x000146A7
		public void MakeSpline(Vector3[] splinePoints, bool loop)
		{
			this.MakeSpline(null, splinePoints, this.GetSegmentNumber(), 0, loop);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000164B9 File Offset: 0x000146B9
		public void MakeSpline(Vector3[] splinePoints, int segments)
		{
			this.MakeSpline(null, splinePoints, segments, 0, false);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000164C6 File Offset: 0x000146C6
		public void MakeSpline(Vector3[] splinePoints, int segments, bool loop)
		{
			this.MakeSpline(null, splinePoints, segments, 0, loop);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000164D3 File Offset: 0x000146D3
		public void MakeSpline(Vector3[] splinePoints, int segments, int index)
		{
			this.MakeSpline(null, splinePoints, segments, index, false);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000164E0 File Offset: 0x000146E0
		public void MakeSpline(Vector3[] splinePoints, int segments, int index, bool loop)
		{
			this.MakeSpline(null, splinePoints, segments, index, loop);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0015CEDC File Offset: 0x0015B0DC
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

		// Token: 0x06002164 RID: 8548 RVA: 0x0015D224 File Offset: 0x0015B424
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

		// Token: 0x06002165 RID: 8549 RVA: 0x0015D2F8 File Offset: 0x0015B4F8
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

		// Token: 0x06002166 RID: 8550 RVA: 0x0015D404 File Offset: 0x0015B604
		private static float VectorDistanceSquared(ref Vector2 p, ref Vector2 q)
		{
			float num = q.x - p.x;
			float num2 = q.y - p.y;
			return num * num + num2 * num2;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0015D434 File Offset: 0x0015B634
		private static float VectorDistanceSquared(ref Vector3 p, ref Vector3 q)
		{
			float num = q.x - p.x;
			float num2 = q.y - p.y;
			float num3 = q.z - p.z;
			return num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0015D474 File Offset: 0x0015B674
		private static void InitNonuniformCatmullRom(float x0, float x1, float x2, float x3, float dt0, float dt1, float dt2, ref Vector4 p)
		{
			float num = ((x1 - x0) / dt0 - (x2 - x0) / (dt0 + dt1) + (x2 - x1) / dt1) * dt1;
			float num2 = ((x2 - x1) / dt1 - (x3 - x1) / (dt1 + dt2) + (x3 - x2) / dt2) * dt1;
			p.x = x1;
			p.y = num;
			p.z = -3f * x1 + 3f * x2 - 2f * num - num2;
			p.w = 2f * x1 - 2f * x2 + num + num2;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000164EE File Offset: 0x000146EE
		private static float EvalCubicPoly(ref Vector4 p, float t)
		{
			return p.x + p.y * t + p.z * (t * t) + p.w * (t * t * t);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00016517 File Offset: 0x00014717
		public void MakeText(string text, Vector3 startPos, float size)
		{
			this.MakeText(text, startPos, size, 1f, 1.5f, true);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0001652D File Offset: 0x0001472D
		public void MakeText(string text, Vector3 startPos, float size, bool uppercaseOnly)
		{
			this.MakeText(text, startPos, size, 1f, 1.5f, uppercaseOnly);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00016544 File Offset: 0x00014744
		public void MakeText(string text, Vector3 startPos, float size, float charSpacing, float lineSpacing)
		{
			this.MakeText(text, startPos, size, charSpacing, lineSpacing, true);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0015D504 File Offset: 0x0015B704
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

		// Token: 0x0600216E RID: 8558 RVA: 0x0015D718 File Offset: 0x0015B918
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

		// Token: 0x0600216F RID: 8559 RVA: 0x0015D840 File Offset: 0x0015BA40
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

		// Token: 0x06002170 RID: 8560 RVA: 0x00016554 File Offset: 0x00014754
		public void MakeCube(Vector3 position, float xSize, float ySize, float zSize)
		{
			this.MakeCube(position, xSize, ySize, zSize, 0);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0015D890 File Offset: 0x0015BA90
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

		// Token: 0x040028D5 RID: 10453
		[SerializeField]
		private Vector3[] m_lineVertices;

		// Token: 0x040028D6 RID: 10454
		[SerializeField]
		private Vector2[] m_lineUVs;

		// Token: 0x040028D7 RID: 10455
		[SerializeField]
		private Color32[] m_lineColors;

		// Token: 0x040028D8 RID: 10456
		[SerializeField]
		private List<int> m_lineTriangles;

		// Token: 0x040028D9 RID: 10457
		[SerializeField]
		private int m_vertexCount;

		// Token: 0x040028DA RID: 10458
		[SerializeField]
		private GameObject m_go;

		// Token: 0x040028DB RID: 10459
		[SerializeField]
		private RectTransform m_rectTransform;

		// Token: 0x040028DC RID: 10460
		private IVectorObject m_vectorObject;

		// Token: 0x040028DD RID: 10461
		[SerializeField]
		private Color32 m_color;

		// Token: 0x040028DE RID: 10462
		[SerializeField]
		private CanvasState m_canvasState;

		// Token: 0x040028DF RID: 10463
		[SerializeField]
		private bool m_is2D;

		// Token: 0x040028E0 RID: 10464
		[SerializeField]
		private List<Vector2> m_points2;

		// Token: 0x040028E1 RID: 10465
		[SerializeField]
		private List<Vector3> m_points3;

		// Token: 0x040028E2 RID: 10466
		[SerializeField]
		private int m_pointsCount;

		// Token: 0x040028E3 RID: 10467
		[SerializeField]
		private Vector3[] m_screenPoints;

		// Token: 0x040028E4 RID: 10468
		[SerializeField]
		private float[] m_lineWidths;

		// Token: 0x040028E5 RID: 10469
		[SerializeField]
		private float m_lineWidth;

		// Token: 0x040028E6 RID: 10470
		[SerializeField]
		private float m_maxWeldDistance;

		// Token: 0x040028E7 RID: 10471
		[SerializeField]
		private float[] m_distances;

		// Token: 0x040028E8 RID: 10472
		[SerializeField]
		private string m_name;

		// Token: 0x040028E9 RID: 10473
		[SerializeField]
		private Material m_material;

		// Token: 0x040028EA RID: 10474
		[SerializeField]
		private Texture m_originalTexture;

		// Token: 0x040028EB RID: 10475
		[SerializeField]
		private Texture m_texture;

		// Token: 0x040028EC RID: 10476
		[SerializeField]
		private bool m_active = true;

		// Token: 0x040028ED RID: 10477
		[SerializeField]
		private LineType m_lineType;

		// Token: 0x040028EE RID: 10478
		[SerializeField]
		private float m_capLength;

		// Token: 0x040028EF RID: 10479
		[SerializeField]
		private bool m_smoothWidth;

		// Token: 0x040028F0 RID: 10480
		[SerializeField]
		private bool m_smoothColor;

		// Token: 0x040028F1 RID: 10481
		[SerializeField]
		private Joins m_joins;

		// Token: 0x040028F2 RID: 10482
		[SerializeField]
		private bool m_isAutoDrawing;

		// Token: 0x040028F3 RID: 10483
		[SerializeField]
		private int m_drawStart;

		// Token: 0x040028F4 RID: 10484
		[SerializeField]
		private int m_drawEnd;

		// Token: 0x040028F5 RID: 10485
		[SerializeField]
		private int m_endPointsUpdate;

		// Token: 0x040028F6 RID: 10486
		[SerializeField]
		private bool m_useNormals;

		// Token: 0x040028F7 RID: 10487
		[SerializeField]
		private bool m_useTangents;

		// Token: 0x040028F8 RID: 10488
		[SerializeField]
		private bool m_normalsCalculated;

		// Token: 0x040028F9 RID: 10489
		[SerializeField]
		private bool m_tangentsCalculated;

		// Token: 0x040028FA RID: 10490
		[SerializeField]
		private EndCap m_capType = EndCap.None;

		// Token: 0x040028FB RID: 10491
		[SerializeField]
		private string m_endCap;

		// Token: 0x040028FC RID: 10492
		[SerializeField]
		private bool m_useCapColors;

		// Token: 0x040028FD RID: 10493
		[SerializeField]
		private Color32 m_frontColor;

		// Token: 0x040028FE RID: 10494
		[SerializeField]
		private Color32 m_backColor;

		// Token: 0x040028FF RID: 10495
		[SerializeField]
		private int m_frontEndCapIndex = -1;

		// Token: 0x04002900 RID: 10496
		[SerializeField]
		private int m_backEndCapIndex = -1;

		// Token: 0x04002901 RID: 10497
		[SerializeField]
		private float m_lineUVBottom;

		// Token: 0x04002902 RID: 10498
		[SerializeField]
		private float m_lineUVTop;

		// Token: 0x04002903 RID: 10499
		[SerializeField]
		private float m_frontCapUVBottom;

		// Token: 0x04002904 RID: 10500
		[SerializeField]
		private float m_frontCapUVTop;

		// Token: 0x04002905 RID: 10501
		[SerializeField]
		private float m_backCapUVBottom;

		// Token: 0x04002906 RID: 10502
		[SerializeField]
		private float m_backCapUVTop;

		// Token: 0x04002907 RID: 10503
		[SerializeField]
		private bool m_continuousTexture;

		// Token: 0x04002908 RID: 10504
		[SerializeField]
		private Transform m_drawTransform;

		// Token: 0x04002909 RID: 10505
		[SerializeField]
		private bool m_viewportDraw;

		// Token: 0x0400290A RID: 10506
		[SerializeField]
		private float m_textureScale;

		// Token: 0x0400290B RID: 10507
		[SerializeField]
		private bool m_useTextureScale;

		// Token: 0x0400290C RID: 10508
		[SerializeField]
		private float m_textureOffset;

		// Token: 0x0400290D RID: 10509
		[SerializeField]
		private bool m_useMatrix;

		// Token: 0x0400290E RID: 10510
		[SerializeField]
		private Matrix4x4 m_matrix;

		// Token: 0x0400290F RID: 10511
		[SerializeField]
		private bool m_collider;

		// Token: 0x04002910 RID: 10512
		[SerializeField]
		private bool m_trigger;

		// Token: 0x04002911 RID: 10513
		[SerializeField]
		private PhysicsMaterial2D m_physicsMaterial;

		// Token: 0x04002912 RID: 10514
		[SerializeField]
		private bool m_alignOddWidthToPixels;

		// Token: 0x04002913 RID: 10515
		private static Vector3 v3zero = Vector3.zero;

		// Token: 0x04002914 RID: 10516
		private static Canvas m_canvas;

		// Token: 0x04002915 RID: 10517
		private static Transform camTransform;

		// Token: 0x04002916 RID: 10518
		private static Camera cam3D;

		// Token: 0x04002917 RID: 10519
		private static Vector3 oldPosition;

		// Token: 0x04002918 RID: 10520
		private static Vector3 oldRotation;

		// Token: 0x04002919 RID: 10521
		private static bool lineManagerCreated = false;

		// Token: 0x0400291A RID: 10522
		private static LineManager m_lineManager;

		// Token: 0x0400291B RID: 10523
		private static Dictionary<string, CapInfo> capDictionary;

		// Token: 0x0400291C RID: 10524
		private static int endianDiff1;

		// Token: 0x0400291D RID: 10525
		private static int endianDiff2;

		// Token: 0x0400291E RID: 10526
		private static byte[] byteBlock;

		// Token: 0x0400291F RID: 10527
		private static string[] functionNames = new string[]
		{
			"VectorLine.SetColors: Length of color",
			"VectorLine.SetWidths: Length of line widths",
			"MakeCurve",
			"MakeSpline",
			"MakeEllipse"
		};

		// Token: 0x02000389 RID: 905
		private enum FunctionName
		{
			// Token: 0x04002921 RID: 10529
			SetColors,
			// Token: 0x04002922 RID: 10530
			SetWidths,
			// Token: 0x04002923 RID: 10531
			MakeCurve,
			// Token: 0x04002924 RID: 10532
			MakeSpline,
			// Token: 0x04002925 RID: 10533
			MakeEllipse
		}
	}
}
