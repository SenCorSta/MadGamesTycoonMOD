using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
	
	public class AllIn1ScrollProperty : MonoBehaviour
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
			if (this.mat.HasProperty(this.numericPropertyName))
			{
				this.propertyShaderID = Shader.PropertyToID(this.numericPropertyName);
			}
			else
			{
				this.DestroyComponentAndLogError(base.gameObject.name + "'s Material doesn't have a " + this.numericPropertyName + " property");
			}
			this.currValue = this.mat.GetFloat(this.propertyShaderID);
		}

		
		private void Update()
		{
			this.currValue += this.scrollSpeed * Time.deltaTime;
			if (this.applyModulo)
			{
				this.currValue %= this.modulo;
			}
			this.mat.SetFloat(this.propertyShaderID, this.currValue);
		}

		
		private void DestroyComponentAndLogError(string logError)
		{
			Debug.LogError(logError);
			UnityEngine.Object.Destroy(this);
		}

		
		[SerializeField]
		private string numericPropertyName = "_RotateUvAmount";

		
		[SerializeField]
		private float scrollSpeed;

		
		[Space]
		[SerializeField]
		private bool applyModulo;

		
		[SerializeField]
		private float modulo = 1f;

		
		[Space]
		[SerializeField]
		[Header("If missing will search object Sprite Renderer or UI Image")]
		private Material mat;

		
		private int propertyShaderID;

		
		private float currValue;
	}
}
