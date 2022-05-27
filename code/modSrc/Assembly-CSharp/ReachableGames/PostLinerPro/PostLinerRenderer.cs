using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F1 RID: 1009
	[ExecuteInEditMode]
	public class PostLinerRenderer : MonoBehaviour
	{
		// Token: 0x060023B1 RID: 9137 RVA: 0x000184D4 File Offset: 0x000166D4
		private void Awake()
		{
			PostLinerRenderer.Instance = null;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000184DC File Offset: 0x000166DC
		private void Start()
		{
			PostLinerRenderer.Instance = this;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000184D4 File Offset: 0x000166D4
		private void OnDestroy()
		{
			PostLinerRenderer.Instance = null;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000184E4 File Offset: 0x000166E4
		public void ClearAllOutlines()
		{
			this._recursiveList.Clear();
			this._outlineObjects.Clear();
			this._objectLayers.Clear();
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00018507 File Offset: 0x00016707
		public void AddToOutlines(Transform t)
		{
			this.DoRecursive(t, delegate(Transform o)
			{
				this._outlineObjects.Add(o);
			});
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0001851C File Offset: 0x0001671C
		public void RemoveFromOutlines(Transform t)
		{
			this.DoRecursive(t, delegate(Transform o)
			{
				this._outlineObjects.Remove(o);
			});
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00170BF8 File Offset: 0x0016EDF8
		private void DoRecursive(Transform root, PostLinerRenderer.DoAction action)
		{
			this._recursiveList.Clear();
			this._recursiveList.Enqueue(root);
			while (this._recursiveList.Count > 0)
			{
				Transform transform = this._recursiveList.Dequeue();
				foreach (object obj in transform)
				{
					Transform item = (Transform)obj;
					this._recursiveList.Enqueue(item);
				}
				action(transform);
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00018531 File Offset: 0x00016731
		public void OnPreRender()
		{
			this.UpdateRenderTexture(Camera.current);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00170C8C File Offset: 0x0016EE8C
		private void UpdateRenderTexture(Camera c)
		{
			if (c.depthTextureMode == DepthTextureMode.None)
			{
				c.depthTextureMode = DepthTextureMode.Depth;
			}
			this._recursiveList.Clear();
			this._objectLayers.Clear();
			using (HashSet<Transform>.Enumerator enumerator = this._outlineObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Transform transform = enumerator.Current;
					if (transform == null)
					{
						this._recursiveList.Enqueue(transform);
					}
					else
					{
						this._objectLayers.Add(transform.gameObject.layer);
						transform.gameObject.layer = this._outlineLayer;
					}
				}
				goto IL_A9;
			}
			IL_92:
			this._outlineObjects.Remove(this._recursiveList.Dequeue());
			IL_A9:
			if (this._recursiveList.Count <= 0)
			{
				if (this._hiddenCamera == null)
				{
					GameObject gameObject = new GameObject("OutlineCamera");
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					gameObject.transform.SetParent(base.transform);
					this._hiddenCamera = gameObject.AddComponent<Camera>();
				}
				this._hiddenCamera.CopyFrom(c);
				if (this._renderTexture == null || this._hiddenCamera.pixelWidth != this._renderTexture.width || this._hiddenCamera.pixelHeight != this._renderTexture.height)
				{
					if (this._renderTexture != null)
					{
						this._renderTexture.Release();
					}
					this._renderTexture = new RenderTexture(this._hiddenCamera.pixelWidth, this._hiddenCamera.pixelHeight, 16, RenderTextureFormat.Depth, RenderTextureReadWrite.Default);
					this._renderTexture.hideFlags = HideFlags.HideAndDontSave;
				}
				this._hiddenCamera.enabled = false;
				this._hiddenCamera.depthTextureMode = DepthTextureMode.Depth;
				this._hiddenCamera.clearFlags = CameraClearFlags.Depth;
				this._hiddenCamera.targetTexture = this._renderTexture;
				this._hiddenCamera.forceIntoRenderTexture = true;
				this._hiddenCamera.rect = new Rect(0f, 0f, 1f, 1f);
				this._hiddenCamera.cullingMask = 1 << this._outlineLayer;
				this._hiddenCamera.Render();
				Shader.SetGlobalTexture(PostLinerRenderer._globalTextureId, this._renderTexture);
				int num = 0;
				foreach (Transform transform2 in this._outlineObjects)
				{
					transform2.gameObject.layer = this._objectLayers[num++];
				}
				return;
			}
			goto IL_92;
		}

		// Token: 0x04002E0D RID: 11789
		[HideInInspector]
		public int _outlineLayer;

		// Token: 0x04002E0E RID: 11790
		private Camera _hiddenCamera;

		// Token: 0x04002E0F RID: 11791
		private RenderTexture _renderTexture;

		// Token: 0x04002E10 RID: 11792
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		// Token: 0x04002E11 RID: 11793
		private HashSet<Transform> _outlineObjects = new HashSet<Transform>();

		// Token: 0x04002E12 RID: 11794
		private List<int> _objectLayers = new List<int>();

		// Token: 0x04002E13 RID: 11795
		public static PostLinerRenderer Instance = null;

		// Token: 0x04002E14 RID: 11796
		private Queue<Transform> _recursiveList = new Queue<Transform>();

		// Token: 0x020003F2 RID: 1010
		// (Invoke) Token: 0x060023BF RID: 9151
		private delegate void DoAction(Transform g);
	}
}
