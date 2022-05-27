using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class createCharScript : MonoBehaviour
{
	// Token: 0x06000164 RID: 356 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0002B370 File Offset: 0x00029570
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

	// Token: 0x04000353 RID: 851
	public GameObject charMainObject;

	// Token: 0x04000354 RID: 852
	public GameObject[] charGfxMales;

	// Token: 0x04000355 RID: 853
	public GameObject[] charGfxFemales;

	// Token: 0x04000356 RID: 854
	public int DEBUG_ForceMesh = -1;

	// Token: 0x04000357 RID: 855
	public int DEBUG_Sex;
}
