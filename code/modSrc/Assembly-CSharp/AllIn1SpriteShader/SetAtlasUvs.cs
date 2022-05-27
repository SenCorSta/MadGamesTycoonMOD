using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x02000406 RID: 1030
	[ExecuteInEditMode]
	public class SetAtlasUvs : MonoBehaviour
	{
		// Token: 0x06002453 RID: 9299 RVA: 0x00175125 File Offset: 0x00173325
		private void Start()
		{
			this.Setup();
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00175125 File Offset: 0x00173325
		private void Reset()
		{
			this.Setup();
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0017512D File Offset: 0x0017332D
		private void Setup()
		{
			if (this.GetRendererReferencesIfNeeded())
			{
				this.GetAndSetUVs();
			}
			if (!this.updateEveryFrame && Application.isPlaying && this != null)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0017515C File Offset: 0x0017335C
		private void OnWillRenderObject()
		{
			if (this.updateEveryFrame)
			{
				this.GetAndSetUVs();
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0017516C File Offset: 0x0017336C
		public void GetAndSetUVs()
		{
			if (!this.GetRendererReferencesIfNeeded())
			{
				return;
			}
			if (!this.isUI)
			{
				Rect textureRect = this.spriteRender.sprite.textureRect;
				textureRect.x /= (float)this.spriteRender.sprite.texture.width;
				textureRect.width /= (float)this.spriteRender.sprite.texture.width;
				textureRect.y /= (float)this.spriteRender.sprite.texture.height;
				textureRect.height /= (float)this.spriteRender.sprite.texture.height;
				this.render.sharedMaterial.SetFloat("_MinXUV", textureRect.xMin);
				this.render.sharedMaterial.SetFloat("_MaxXUV", textureRect.xMax);
				this.render.sharedMaterial.SetFloat("_MinYUV", textureRect.yMin);
				this.render.sharedMaterial.SetFloat("_MaxYUV", textureRect.yMax);
				return;
			}
			Rect textureRect2 = this.uiImage.sprite.textureRect;
			textureRect2.x /= (float)this.uiImage.sprite.texture.width;
			textureRect2.width /= (float)this.uiImage.sprite.texture.width;
			textureRect2.y /= (float)this.uiImage.sprite.texture.height;
			textureRect2.height /= (float)this.uiImage.sprite.texture.height;
			this.uiImage.material.SetFloat("_MinXUV", textureRect2.xMin);
			this.uiImage.material.SetFloat("_MaxXUV", textureRect2.xMax);
			this.uiImage.material.SetFloat("_MinYUV", textureRect2.yMin);
			this.uiImage.material.SetFloat("_MaxYUV", textureRect2.yMax);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x001753B0 File Offset: 0x001735B0
		public void ResetAtlasUvs()
		{
			if (!this.GetRendererReferencesIfNeeded())
			{
				return;
			}
			if (!this.isUI)
			{
				this.render.sharedMaterial.SetFloat("_MinXUV", 0f);
				this.render.sharedMaterial.SetFloat("_MaxXUV", 1f);
				this.render.sharedMaterial.SetFloat("_MinYUV", 0f);
				this.render.sharedMaterial.SetFloat("_MaxYUV", 1f);
				return;
			}
			this.uiImage.material.SetFloat("_MinXUV", 0f);
			this.uiImage.material.SetFloat("_MaxXUV", 1f);
			this.uiImage.material.SetFloat("_MinYUV", 0f);
			this.uiImage.material.SetFloat("_MaxYUV", 1f);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0017549F File Offset: 0x0017369F
		public void UpdateEveryFrame(bool everyFrame)
		{
			this.updateEveryFrame = everyFrame;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x001754A8 File Offset: 0x001736A8
		private bool GetRendererReferencesIfNeeded()
		{
			if (this.spriteRender == null)
			{
				this.spriteRender = base.GetComponent<SpriteRenderer>();
			}
			if (this.spriteRender != null)
			{
				if (this.spriteRender.sprite == null)
				{
					UnityEngine.Object.DestroyImmediate(this);
					return false;
				}
				if (this.render == null)
				{
					this.render = base.GetComponent<Renderer>();
				}
				this.isUI = false;
			}
			else
			{
				if (this.uiImage == null)
				{
					this.uiImage = base.GetComponent<Image>();
					if (!(this.uiImage != null))
					{
						UnityEngine.Object.DestroyImmediate(this);
						return false;
					}
				}
				if (this.render == null)
				{
					this.render = base.GetComponent<Renderer>();
				}
				this.isUI = true;
			}
			if (this.spriteRender == null && this.uiImage == null)
			{
				UnityEngine.Object.DestroyImmediate(this);
				return false;
			}
			return true;
		}

		// Token: 0x04002E6B RID: 11883
		[SerializeField]
		private bool updateEveryFrame;

		// Token: 0x04002E6C RID: 11884
		private Renderer render;

		// Token: 0x04002E6D RID: 11885
		private SpriteRenderer spriteRender;

		// Token: 0x04002E6E RID: 11886
		private Image uiImage;

		// Token: 0x04002E6F RID: 11887
		private bool isUI;
	}
}
