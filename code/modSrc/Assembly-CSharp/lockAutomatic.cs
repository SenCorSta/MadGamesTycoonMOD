using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F2 RID: 754
public class lockAutomatic : MonoBehaviour
{
	// Token: 0x06001A96 RID: 6806 RVA: 0x0010BC54 File Offset: 0x00109E54
	private void OnEnable()
	{
		if (this.uiRoom)
		{
			roomScript rS_ = this.uiRoom.GetComponent<roomButtonScript>().rS_;
			if (rS_ && rS_.taskGameObject)
			{
				bool flag = false;
				if (rS_.taskGameObject.GetComponent<taskForschung>())
				{
					flag = rS_.taskGameObject.GetComponent<taskForschung>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskMarketing>())
				{
					flag = rS_.taskGameObject.GetComponent<taskMarketing>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskTraining>())
				{
					flag = rS_.taskGameObject.GetComponent<taskTraining>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskContractWork>())
				{
					flag = rS_.taskGameObject.GetComponent<taskContractWork>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskUpdate>())
				{
					flag = rS_.taskGameObject.GetComponent<taskUpdate>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskFankampagne>())
				{
					flag = rS_.taskGameObject.GetComponent<taskFankampagne>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskSpielbericht>())
				{
					flag = rS_.taskGameObject.GetComponent<taskSpielbericht>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskProduction>())
				{
					flag = rS_.taskGameObject.GetComponent<taskProduction>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskF2PUpdate>())
				{
					flag = rS_.taskGameObject.GetComponent<taskF2PUpdate>().automatic;
				}
				if (rS_.taskGameObject.GetComponent<taskMitarbeitersuche>())
				{
					flag = rS_.taskGameObject.GetComponent<taskMitarbeitersuche>().automatic;
				}
				if (flag && rS_.mS_)
				{
					this.lockGameObject.SetActive(false);
					base.gameObject.GetComponent<Button>().interactable = true;
					base.gameObject.transform.GetChild(0).GetComponent<Text>().text = rS_.mS_.tS_.GetText(168);
					return;
				}
				this.lockGameObject.SetActive(false);
				base.gameObject.GetComponent<Button>().interactable = true;
				base.gameObject.transform.GetChild(0).GetComponent<Text>().text = rS_.mS_.tS_.GetText(1403);
			}
		}
	}

	// Token: 0x0400219B RID: 8603
	public GameObject uiRoom;

	// Token: 0x0400219C RID: 8604
	public GameObject lockGameObject;
}
