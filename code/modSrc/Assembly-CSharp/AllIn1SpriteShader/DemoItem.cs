using System;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	public class DemoItem : MonoBehaviour
	{
		
		private void Update()
		{
			base.transform.LookAt(base.transform.position + DemoItem.lookAtZ);
		}

		
		private static Vector3 lookAtZ = new Vector3(0f, 0f, 1f);
	}
}
