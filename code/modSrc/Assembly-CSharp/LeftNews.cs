using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AD RID: 173
public class LeftNews : MonoBehaviour
{
	// Token: 0x06000653 RID: 1619 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00062B04 File Offset: 0x00060D04
	public void Init(int roomID_, Sprite sprite_, string tooltip_, Sprite smallSprite_)
	{
		this.roomID = roomID_;
		this.uiObjects[0].GetComponent<Image>().sprite = sprite_;
		this.uiObjects[1].GetComponent<Image>().sprite = smallSprite_;
		base.gameObject.GetComponent<tooltip>().c = tooltip_;
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x000059D4 File Offset: 0x00003BD4
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 30f)
		{
			this.Remove();
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00062B50 File Offset: 0x00060D50
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

	// Token: 0x06000657 RID: 1623 RVA: 0x00004174 File Offset: 0x00002374
	private void Remove()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040009EA RID: 2538
	public GameObject[] uiObjects;

	// Token: 0x040009EB RID: 2539
	private float timer;

	// Token: 0x040009EC RID: 2540
	public int roomID = -1;
}
