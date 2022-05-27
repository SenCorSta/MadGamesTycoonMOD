using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class moCapChar : MonoBehaviour
{
	// Token: 0x06001A61 RID: 6753 RVA: 0x00110870 File Offset: 0x0010EA70
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

	// Token: 0x06001A62 RID: 6754 RVA: 0x00110990 File Offset: 0x0010EB90
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

	// Token: 0x06001A63 RID: 6755 RVA: 0x00011BBD File Offset: 0x0000FDBD
	private IEnumerator RemoveChar()
	{
		yield return new WaitForSeconds(1f);
		base.transform.localPosition = new Vector3(5000f, 5000f, 5000f);
		this.skin.material = this.clothScript_.matColor_Skin[UnityEngine.Random.Range(0, this.clothScript_.matColor_Skin.Length)];
		yield break;
	}

	// Token: 0x040021AF RID: 8623
	private Animator charAnimation;

	// Token: 0x040021B0 RID: 8624
	private float timer;

	// Token: 0x040021B1 RID: 8625
	private GameObject main_;

	// Token: 0x040021B2 RID: 8626
	private mainScript mS_;

	// Token: 0x040021B3 RID: 8627
	private clothScript clothScript_;

	// Token: 0x040021B4 RID: 8628
	public SkinnedMeshRenderer skin;

	// Token: 0x040021B5 RID: 8629
	public objectScript oS_;

	// Token: 0x040021B6 RID: 8630
	private roomScript roomS_;

	// Token: 0x040021B7 RID: 8631
	private mapScript mapS_;

	// Token: 0x040021B8 RID: 8632
	private Vector3 localPos;

	// Token: 0x040021B9 RID: 8633
	private bool hided = true;
}
