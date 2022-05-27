using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	public class DemoRepositionExpositor : MonoBehaviour
	{
		
		[ContextMenu("RepositionExpositor")]
		private void RepositionExpositor()
		{
			int num = 0;
			Vector3 zero = Vector3.zero;
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				zero.x = (float)num * this.paddingX;
				transform.localPosition = zero;
				num++;
			}
		}

		
		[SerializeField]
		private float paddingX = 10f;
	}
}
