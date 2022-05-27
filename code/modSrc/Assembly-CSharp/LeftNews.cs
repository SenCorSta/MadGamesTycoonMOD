using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AD RID: 173
public class LeftNews : MonoBehaviour
{
	// Token: 0x0600065C RID: 1628 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x000500A4 File Offset: 0x0004E2A4
	public void Init(int roomID_, Sprite sprite_, string tooltip_, Sprite smallSprite_)
	{
		this.roomID = roomID_;
		this.uiObjects[0].GetComponent<Image>().sprite = sprite_;
		this.uiObjects[1].GetComponent<Image>().sprite = smallSprite_;
		base.gameObject.GetComponent<tooltip>().c = tooltip_;
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x000500F0 File Offset: 0x0004E2F0
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 30f)
		{
			this.Remove();
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00050118 File Offset: 0x0004E318
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

	// Token: 0x06000660 RID: 1632 RVA: 0x0003D679 File Offset: 0x0003B879
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
