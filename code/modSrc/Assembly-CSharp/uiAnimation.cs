using System;
using UnityEngine;


public class uiAnimation : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void OnEnable()
	{
		Debug.Log("LKJK" + UnityEngine.Random.Range(0, 100000).ToString() + " " + base.gameObject.name);
		base.GetComponent<Animator>().Play(this.anim);
		base.GetComponent<characterScript>().male = true;
	}

	
	public string anim = "";
}
