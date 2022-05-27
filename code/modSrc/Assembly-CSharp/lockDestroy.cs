using System;
using UnityEngine;


public class lockDestroy : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (this.unlockSlot == -1)
		{
			return;
		}
		if (this.sonstigeForschung)
		{
			if (this.forschungSonstiges_.IsErforscht(this.unlockSlot))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (this.gameplayFeatures)
		{
			if (this.gF_.IsErforscht(this.unlockSlot))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (this.unlock_.Get(this.unlockSlot))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	public int unlockSlot = -1;

	
	public bool sonstigeForschung;

	
	public bool gameplayFeatures;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private unlockScript unlock_;

	
	private forschungSonstiges forschungSonstiges_;

	
	private gameplayFeatures gF_;
}
