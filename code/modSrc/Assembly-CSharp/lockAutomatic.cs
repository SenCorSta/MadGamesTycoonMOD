using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EF RID: 751
public class lockAutomatic : MonoBehaviour
{
	// Token: 0x06001A4C RID: 6732 RVA: 0x0010FB00 File Offset: 0x0010DD00
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

	// Token: 0x04002181 RID: 8577
	public GameObject uiRoom;

	// Token: 0x04002182 RID: 8578
	public GameObject lockGameObject;
}
