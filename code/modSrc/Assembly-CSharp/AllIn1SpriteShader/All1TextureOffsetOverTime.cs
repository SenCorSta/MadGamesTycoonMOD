using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	
	public class All1TextureOffsetOverTime : MonoBehaviour
	{
		
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

		
		private void Update()
		{
			this.currOffset.x = this.currOffset.x + this.offsetSpeed.x * Time.deltaTime;
			this.currOffset.y = this.currOffset.y + this.offsetSpeed.y * Time.deltaTime;
			this.mat.SetTextureOffset(this.textureShaderId, this.currOffset);
		}

		
		private void DestroyComponentAndLogError(string logError)
		{
			Debug.LogError(logError);
			UnityEngine.Object.Destroy(this);
		}

		
		[SerializeField]
		private string texturePropertyName = "_MainTex";

		
		[SerializeField]
		private Vector2 offsetSpeed;

		
		[SerializeField]
		[Header("If missing will search object Sprite Renderer or UI Image")]
		private Material mat;

		
		private int textureShaderId;

		
		private Vector2 currOffset = Vector2.zero;
	}
}
