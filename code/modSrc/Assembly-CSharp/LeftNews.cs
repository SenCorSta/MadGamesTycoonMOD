using System;
using UnityEngine;
using UnityEngine.UI;


public class LeftNews : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	public void Init(int roomID_, Sprite sprite_, string tooltip_, Sprite smallSprite_)
	{
		this.roomID = roomID_;
		this.uiObjects[0].GetComponent<Image>().sprite = sprite_;
		this.uiObjects[1].GetComponent<Image>().sprite = smallSprite_;
		base.gameObject.GetComponent<tooltip>().c = tooltip_;
	}

	
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 30f)
		{
			this.Remove();
		}
	}

	
	public void BUTTON_Click()
	{
		sfxScript component = GameObject.Find("SFX").GetComponent<sfxScript>();
		if (component)
		{
			component.PlaySound(3, true);
		}
		if (this.roomID != -1)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
			int i = 0;
			while (i < array.Length)
			{
				roomScript component2 = array[i].GetComponent<roomScript>();
				if (component2 && component2.myID == this.roomID)
				{
					GameObject gameObject = GameObject.Find("CamMovement");
					if (gameObject)
					{
						gameObject.transform.position = new Vector3(component2.uiPos.x, gameObject.transform.position.y, component2.uiPos.z);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
		this.Remove();
	}

	
	private void Remove()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public GameObject[] uiObjects;

	
	private float timer;

	
	public int roomID = -1;
}
