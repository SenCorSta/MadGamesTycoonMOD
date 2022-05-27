using System;
using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
	
	public class Demo2AutoScroll : MonoBehaviour
	{
		
		private void Start()
		{
			this.sceneDescription.SetActive(false);
			Camera.main.fieldOfView = 60f;
			this.children = base.GetComponentsInChildren<Transform>();
			for (int i = 0; i < this.children.Length; i++)
			{
				if (this.children[i].gameObject != base.gameObject)
				{
					this.children[i].gameObject.SetActive(false);
					this.children[i].localPosition = Vector3.zero;
				}
			}
			this.totalTime /= (float)this.children.Length;
			base.StartCoroutine(this.ScrollElements());
		}

		
		private IEnumerator ScrollElements()
		{
			int i = 0;
			for (;;)
			{
				if (this.children[i].gameObject == base.gameObject)
				{
					i = (i + 1) % this.children.Length;
				}
				else
				{
					this.children[i].gameObject.SetActive(true);
					yield return new WaitForSeconds(this.totalTime);
					this.children[i].gameObject.SetActive(false);
					i = (i + 1) % this.children.Length;
				}
			}
			yield break;
		}

		
		private Transform[] children;

		
		public float totalTime;

		
		public GameObject sceneDescription;
	}
}
