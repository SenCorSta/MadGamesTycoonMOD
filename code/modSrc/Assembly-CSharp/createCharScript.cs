using System;
using UnityEngine;


public class createCharScript : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	public characterScript CreateCharacter(int id_, bool male, int forceModel)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.charMainObject);
		characterScript component = gameObject.GetComponent<characterScript>();
		component.myID = id_;
		GameObject gameObject2;
		if (this.DEBUG_ForceMesh != -1)
		{
			if (this.DEBUG_Sex == 0)
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.charGfxMales[this.DEBUG_ForceMesh], gameObject.transform);
			}
			else
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.charGfxFemales[this.DEBUG_ForceMesh], gameObject.transform);
			}
		}
		else if (male)
		{
			if (forceModel == -1)
			{
				int num = UnityEngine.Random.Range(0, this.charGfxMales.Length);
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.charGfxMales[num], gameObject.transform);
				component.model_body = num;
			}
			else
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.charGfxMales[forceModel], gameObject.transform);
			}
		}
		else if (forceModel == -1)
		{
			int num2 = UnityEngine.Random.Range(0, this.charGfxFemales.Length);
			gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.charGfxFemales[num2], gameObject.transform);
			component.model_body = num2;
		}
		else
		{
			gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.charGfxFemales[forceModel], gameObject.transform);
		}
		if (forceModel == -1)
		{
			gameObject2.GetComponent<characterGFXScript>().Init(false);
		}
		gameObject2.transform.localPosition = new Vector3(0f, 0.5f, 0f);
		gameObject2.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
		component.Init();
		return component;
	}

	
	public GameObject charMainObject;

	
	public GameObject[] charGfxMales;

	
	public GameObject[] charGfxFemales;

	
	public int DEBUG_ForceMesh = -1;

	
	public int DEBUG_Sex;
}
