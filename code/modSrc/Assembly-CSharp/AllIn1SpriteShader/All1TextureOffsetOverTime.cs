using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	// Token: 0x020003FA RID: 1018
	public class All1TextureOffsetOverTime : MonoBehaviour
	{
		// Token: 0x06002424 RID: 9252 RVA: 0x0017445C File Offset: 0x0017265C
		private void Start()
		{
			if (this.mat == null)
			{
				SpriteRenderer component = base.GetComponent<SpriteRenderer>();
				if (component != null)
				{
					this.mat = component.material;
				}
				else
				{
					Image component2 = base.GetComponent<Image>();
					if (component2 != null)
					{
						this.mat = component2.material;
					}
				}
			}
			if (this.mat == null)
			{
				this.DestroyComponentAndLogError(base.gameObject.name + " has no valid Material, deleting All1TextureOffsetOverTIme component");
				return;
			}
			if (this.mat.HasProperty(this.texturePropertyName))
			{
				this.textureShaderId = Shader.PropertyToID(this.texturePropertyName);
				return;
			}
			this.DestroyComponentAndLogError(base.gameObject.name + "'s Material doesn't have a " + this.texturePropertyName + " property");
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00174528 File Offset: 0x00172728
		private void Update()
		{
			this.currOffset.x = this.currOffset.x + this.offsetSpeed.x * Time.deltaTime;
			this.currOffset.y = this.currOffset.y + this.offsetSpeed.y * Time.deltaTime;
			this.mat.SetTextureOffset(this.textureShaderId, this.currOffset);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0017458C File Offset: 0x0017278C
		private void DestroyComponentAndLogError(string logError)
		{
			Debug.LogError(logError);
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04002E3E RID: 11838
		[SerializeField]
		private string texturePropertyName = "_MainTex";

		// Token: 0x04002E3F RID: 11839
		[SerializeField]
		private Vector2 offsetSpeed;

		// Token: 0x04002E40 RID: 11840
		[SerializeField]
		[Header("If missing will search object Sprite Renderer or UI Image")]
		private Material mat;

		// Token: 0x04002E41 RID: 11841
		private int textureShaderId;

		// Token: 0x04002E42 RID: 11842
		private Vector2 currOffset = Vector2.zero;
	}
}
