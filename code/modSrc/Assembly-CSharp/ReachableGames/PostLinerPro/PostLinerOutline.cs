using System;
using System.Collections;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	
	public class PostLinerOutline : MonoBehaviour
	{
		
		private void OnEnable()
		{
			if (this._inProcess != null)
			{
				base.StopCoroutine(this._inProcess);
			}
			if (PostLinerRenderer.Instance != null)
			{
				PostLinerRenderer.Instance.AddToOutlines(base.transform);
				return;
			}
			this._inProcess = base.StartCoroutine(this.Add());
		}

		
		private void OnDisable()
		{
			if (this._inProcess != null)
			{
				base.StopCoroutine(this._inProcess);
			}
			if (PostLinerRenderer.Instance != null)
			{
				PostLinerRenderer.Instance.RemoveFromOutlines(base.transform);
				return;
			}
			if (!this._isQuitting)
			{
				this._inProcess = base.StartCoroutine(this.Remove());
			}
		}

		
		private void OnApplicationQuit()
		{
			this._isQuitting = true;
		}

		
		private IEnumerator Add()
		{
			yield return new WaitUntil(() => PostLinerRenderer.Instance != null);
			PostLinerRenderer.Instance.AddToOutlines(base.transform);
			yield break;
		}

		
		private IEnumerator Remove()
		{
			yield return new WaitUntil(() => PostLinerRenderer.Instance != null);
			PostLinerRenderer.Instance.RemoveFromOutlines(base.transform);
			yield break;
		}

		
		private Coroutine _inProcess;

		
		private bool _isQuitting;
	}
}
