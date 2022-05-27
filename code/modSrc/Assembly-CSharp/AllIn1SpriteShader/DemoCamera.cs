using System;
using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	public class DemoCamera : MonoBehaviour
	{
		
		private void Awake()
		{
			this.offset = base.transform.position - this.targetedItem.position;
			base.StartCoroutine(this.SetCamAfterStart());
		}

		
		private void Update()
		{
			if (!this.canUpdate)
			{
				return;
			}
			this.target.y = (float)this.demoController.GetCurrExpositor() * this.demoController.expositorDistance;
			base.transform.position = Vector3.Lerp(base.transform.position, this.target, this.speed * Time.deltaTime);
		}

		
		private IEnumerator SetCamAfterStart()
		{
			yield return null;
			base.transform.position = this.targetedItem.position + this.offset;
			this.target = base.transform.position;
			this.canUpdate = true;
			yield break;
		}

		
		[SerializeField]
		private Transform targetedItem;

		
		[SerializeField]
		private All1ShaderDemoController demoController;

		
		[SerializeField]
		private float speed;

		
		private Vector3 offset;

		
		private Vector3 target;

		
		private bool canUpdate;
	}
}
