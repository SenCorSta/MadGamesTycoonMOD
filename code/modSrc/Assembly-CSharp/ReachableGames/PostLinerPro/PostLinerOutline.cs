using System;
using System.Collections;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003EE RID: 1006
	public class PostLinerOutline : MonoBehaviour
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x00173210 File Offset: 0x00171410
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

		// Token: 0x060023EA RID: 9194 RVA: 0x00173264 File Offset: 0x00171464
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

		// Token: 0x060023EB RID: 9195 RVA: 0x001732BD File Offset: 0x001714BD
		private void OnApplicationQuit()
		{
			this._isQuitting = true;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x001732C6 File Offset: 0x001714C6
		private IEnumerator Add()
		{
			yield return new WaitUntil(() => PostLinerRenderer.Instance != null);
			PostLinerRenderer.Instance.AddToOutlines(base.transform);
			yield break;
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x001732D5 File Offset: 0x001714D5
		private IEnumerator Remove()
		{
			yield return new WaitUntil(() => PostLinerRenderer.Instance != null);
			PostLinerRenderer.Instance.RemoveFromOutlines(base.transform);
			yield break;
		}

		// Token: 0x04002DF4 RID: 11764
		private Coroutine _inProcess;

		// Token: 0x04002DF5 RID: 11765
		private bool _isQuitting;
	}
}
