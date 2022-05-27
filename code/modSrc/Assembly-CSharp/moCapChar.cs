using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class moCapChar : MonoBehaviour
{
	// Token: 0x06001AAB RID: 6827 RVA: 0x0010CA54 File Offset: 0x0010AC54
	private void Start()
	{
		this.localPos = base.transform.localPosition;
		base.transform.localPosition = new Vector3(5000f, 5000f, 5000f);
		this.hided = true;
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.clothScript_)
		{
			this.clothScript_ = this.main_.GetComponent<clothScript>();
		}
		if (this.charAnimation == null)
		{
			this.charAnimation = base.GetComponent<Animator>();
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		this.skin.material = this.clothScript_.matColor_Skin[UnityEngine.Random.Range(0, this.clothScript_.matColor_Skin.Length)];
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x0010CB74 File Offset: 0x0010AD74
	private void Update()
	{
		if (!this.mS_)
		{
			return;
		}
		if (this.oS_.picked)
		{
			base.transform.localPosition = new Vector3(5000f, 5000f, 5000f);
			return;
		}
		this.roomS_ = this.mapS_.mapRoomScript[Mathf.RoundToInt(base.transform.root.transform.position.x), Mathf.RoundToInt(base.transform.root.transform.position.z)];
		if (!this.roomS_)
		{
			return;
		}
		if (this.roomS_.taskID == -1 || !this.oS_.inUse)
		{
			if (!this.hided)
			{
				this.hided = true;
				base.transform.parent.GetComponent<Animation>().Play("moCapCharScaleDown");
				base.StartCoroutine(this.RemoveChar());
			}
			return;
		}
		if (this.hided)
		{
			this.hided = false;
			base.transform.parent.GetComponent<Animation>().Play("moCapCharScaleUp");
			base.transform.localPosition = this.localPos;
		}
		this.timer -= this.mS_.GetDeltaTime();
		this.charAnimation.speed = this.mS_.GetGameSpeed();
		if (this.timer > 0f)
		{
			return;
		}
		this.timer = 3f;
		switch (UnityEngine.Random.Range(0, 14))
		{
		case 0:
			this.charAnimation.CrossFade("UppercutRight", 0.1f, 0, 0f, 0.4f);
			return;
		case 1:
			this.charAnimation.CrossFade("IdleLHandPunch", 0.1f, 0, 0f, 0.4f);
			return;
		case 2:
			this.charAnimation.CrossFade("IdleFrontKick", 0.1f, 0, 0f, 0.4f);
			return;
		case 3:
			this.charAnimation.CrossFade("FlipKick", 0.1f, 0, 0f, 0.4f);
			return;
		case 4:
			this.charAnimation.CrossFade("Fight540RoundHouse", 0.1f, 0, 0f, 0.4f);
			return;
		case 5:
			this.charAnimation.CrossFade("ChargePunch", 0.1f, 0, 0f, 0.4f);
			return;
		case 6:
			this.charAnimation.CrossFade("Cast", 0.1f, 0, 0f, 0.4f);
			return;
		case 7:
			this.charAnimation.CrossFade("UseItem", 0.1f, 0, 0f, 0.4f);
			return;
		case 8:
			this.charAnimation.CrossFade("Katana45DegSwing", 0.1f, 0, 0f, 0.4f);
			return;
		case 9:
			this.charAnimation.CrossFade("Mutilate", 0.1f, 0, 0f, 0.4f);
			return;
		case 10:
			this.charAnimation.CrossFade("StaffHeal", 0.1f, 0, 0f, 0.4f);
			return;
		case 11:
			this.charAnimation.CrossFade("BasketballJumpShot", 0.1f, 0, 0f, 0.4f);
			return;
		case 12:
			this.charAnimation.CrossFade("Namaste", 0.1f, 0, 0f, 0.4f);
			return;
		case 13:
			this.charAnimation.CrossFade("Lunges", 0.1f, 0, 0f, 0.4f);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x0010CF08 File Offset: 0x0010B108
	private IEnumerator RemoveChar()
	{
		yield return new WaitForSeconds(1f);
		base.transform.localPosition = new Vector3(5000f, 5000f, 5000f);
		this.skin.material = this.clothScript_.matColor_Skin[UnityEngine.Random.Range(0, this.clothScript_.matColor_Skin.Length)];
		yield break;
	}

	// Token: 0x040021C9 RID: 8649
	private Animator charAnimation;

	// Token: 0x040021CA RID: 8650
	private float timer;

	// Token: 0x040021CB RID: 8651
	private GameObject main_;

	// Token: 0x040021CC RID: 8652
	private mainScript mS_;

	// Token: 0x040021CD RID: 8653
	private clothScript clothScript_;

	// Token: 0x040021CE RID: 8654
	public SkinnedMeshRenderer skin;

	// Token: 0x040021CF RID: 8655
	public objectScript oS_;

	// Token: 0x040021D0 RID: 8656
	private roomScript roomS_;

	// Token: 0x040021D1 RID: 8657
	private mapScript mapS_;

	// Token: 0x040021D2 RID: 8658
	private Vector3 localPos;

	// Token: 0x040021D3 RID: 8659
	private bool hided = true;
}
