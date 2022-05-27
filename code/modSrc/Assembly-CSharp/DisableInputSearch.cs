using System;
using UnityEngine;
using UnityEngine.UI;


public class DisableInputSearch : MonoBehaviour
{
	
	private void OnDisable()
	{
		if (base.GetComponent<InputField>())
		{
			base.GetComponent<InputField>().text = "";
		}
	}
}
