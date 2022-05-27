using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F4 RID: 1012
	[ExecuteInEditMode]
	public class PostLinerRenderer : MonoBehaviour
	{
		// Token: 0x06002404 RID: 9220 RVA: 0x001739AA File Offset: 0x00171BAA
		private void Awake()
		{
			PostLinerRenderer.Instance = null;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x001739B2 File Offset: 0x00171BB2
		private void Start()
		{
			PostLinerRenderer.Instance = this;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x001739AA File Offset: 0x00171BAA
		private void OnDestroy()
		{
			PostLinerRenderer.Instance = null;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x001739BA File Offset: 0x00171BBA
		public void ClearAllOutlines()
		{
			this._recursiveList.Clear();
			this._outlineObjects.Clear();
			this._objectLayers.Clear();
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x001739DD File Offset: 0x00171BDD
		public void AddToOutlines(Transform t)
		{
			this.DoRecursive(t, delegate(Transform o)
			{
				this._outlineObjects.Add(o);
			});
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x001739F2 File Offset: 0x00171BF2
		public void RemoveFromOutlines(Transform t)
		{
			this.DoRecursive(t, delegate(Transform o)
			{
				this._outlineObjects.Remove(o);
			});
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00173A08 File Offset: 0x00171C08
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

		// Token: 0x0600240B RID: 9227 RVA: 0x00173A9C File Offset: 0x00171C9C
		public void OnPreRender()
		{
			this.UpdateRenderTexture(Camera.current);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00173AAC File Offset: 0x00171CAC
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

		// Token: 0x04002E23 RID: 11811
		[HideInInspector]
		public int _outlineLayer;

		// Token: 0x04002E24 RID: 11812
		private Camera _hiddenCamera;

		// Token: 0x04002E25 RID: 11813
		private RenderTexture _renderTexture;

		// Token: 0x04002E26 RID: 11814
		private static int _globalTextureId = Shader.PropertyToID("_OutlineDepth");

		// Token: 0x04002E27 RID: 11815
		private HashSet<Transform> _outlineObjects = new HashSet<Transform>();

		// Token: 0x04002E28 RID: 11816
		private List<int> _objectLayers = new List<int>();

		// Token: 0x04002E29 RID: 11817
		public static PostLinerRenderer Instance = null;

		// Token: 0x04002E2A RID: 11818
		private Queue<Transform> _recursiveList = new Queue<Transform>();

		// Token: 0x020003F5 RID: 1013
		// (Invoke) Token: 0x06002412 RID: 9234
		private delegate void DoAction(Transform g);
	}
}
