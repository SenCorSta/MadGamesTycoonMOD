using System;
using System.Collections;
using UnityEngine;


public class moCapChar : MonoBehaviour
{
	
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

	
	private IEnumerator RemoveChar()
	{
		yield return new WaitForSeconds(1f);
		base.transform.localPosition = new Vector3(5000f, 5000f, 5000f);
		this.skin.material = this.clothScript_.matColor_Skin[UnityEngine.Random.Range(0, this.clothScript_.matColor_Skin.Length)];
		yield break;
	}

	
	private Animator charAnimation;

	
	private float timer;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private clothScript clothScript_;

	
	public SkinnedMeshRenderer skin;

	
	public objectScript oS_;

	
	private roomScript roomS_;

	
	private mapScript mapS_;

	
	private Vector3 localPos;

	
	private bool hided = true;
}
