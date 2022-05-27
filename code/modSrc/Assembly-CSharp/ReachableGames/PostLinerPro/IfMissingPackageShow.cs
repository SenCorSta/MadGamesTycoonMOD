using System;
using UnityEngine;

namespace ReachableGames.PostLinerPro
{
	
	public class IfMissingPackageShow : MonoBehaviour
	{
		
		private void OnEnable()
		{
			if (this._checkAsset == null)
			{
				this._showIfMissing.SetActive(true);
			}
		}

		
		public UnityEngine.Object _checkAsset;

		
		public GameObject _showIfMissing;
	}
}
