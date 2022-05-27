using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	// Token: 0x020003F5 RID: 1013
	public class IfMissingPackageShow : MonoBehaviour
	{
		// Token: 0x060023C8 RID: 9160 RVA: 0x000185E6 File Offset: 0x000167E6
		private void OnEnable()
		{
			if (this._checkAsset == null)
			{
				this._showIfMissing.SetActive(true);
			}
		}

		// Token: 0x04002E1C RID: 11804
		public UnityEngine.Object _checkAsset;

		// Token: 0x04002E1D RID: 11805
		public GameObject _showIfMissing;
	}
}
