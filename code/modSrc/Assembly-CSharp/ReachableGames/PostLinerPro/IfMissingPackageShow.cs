using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F8 RID: 1016
	public class IfMissingPackageShow : MonoBehaviour
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x00174073 File Offset: 0x00172273
		private void OnEnable()
		{
			if (this._checkAsset == null)
			{
				this._showIfMissing.SetActive(true);
			}
		}

		// Token: 0x04002E32 RID: 11826
		public UnityEngine.Object _checkAsset;

		// Token: 0x04002E33 RID: 11827
		public GameObject _showIfMissing;
	}
}
