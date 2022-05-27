using System;
using System.Collections;
using UnityEngine;


public class uiRoomMouseOver : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void OnMouseOver()
	{
		Debug.Log("KKKKKKKKKKK");
		base.StartCoroutine(this.iSetAsLastSibling());
	}

	
	private IEnumerator iSetAsLastSibling()
	{
		yield return new WaitForEndOfFrame();
		base.gameObject.transform.SetAsLastSibling();
		yield break;
	}
}
