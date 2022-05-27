using System;
using System.Collections;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003EB RID: 1003
	public class PostLinerOutline : MonoBehaviour
	{
		// Token: 0x06002396 RID: 9110 RVA: 0x001704D0 File Offset: 0x0016E6D0
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

		// Token: 0x06002397 RID: 9111 RVA: 0x00170524 File Offset: 0x0016E724
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

		// Token: 0x06002398 RID: 9112 RVA: 0x0001845E File Offset: 0x0001665E
		private void OnApplicationQuit()
		{
			this._isQuitting = true;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00018467 File Offset: 0x00016667
		private IEnumerator Add()
		{
			yield return new WaitUntil(() => PostLinerRenderer.Instance != null);
			PostLinerRenderer.Instance.AddToOutlines(base.transform);
			yield break;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00018476 File Offset: 0x00016676
		private IEnumerator Remove()
		{
			yield return new WaitUntil(() => PostLinerRenderer.Instance != null);
			PostLinerRenderer.Instance.RemoveFromOutlines(base.transform);
			yield break;
		}

		// Token: 0x04002DDE RID: 11742
		private Coroutine _inProcess;

		// Token: 0x04002DDF RID: 11743
		private bool _isQuitting;
	}
}
