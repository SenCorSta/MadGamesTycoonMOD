using System;
using UnityEngine;


public class characterGFXScript : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	public void Init(bool forcedClothes)
	{
		this.cS_ = base.transform.root.GetComponent<characterScript>();
		this.cS_.male = this.male;
		this.cS_.myRenderer = this.objectSkin.GetComponent<SkinnedMeshRenderer>();
		if (this.addTasse1)
		{
			this.cS_.addObjects[0] = this.addTasse1;
			this.cS_.addObjects[0].SetActive(false);
		}
		if (this.addMuel1)
		{
			this.cS_.addObjects[1] = this.addMuel1;
			this.cS_.addObjects[1].SetActive(false);
		}
		if (this.addGiesskanne1)
		{
			this.cS_.addObjects[2] = this.addGiesskanne1;
			this.cS_.addObjects[2].SetActive(false);
		}
		if (this.addBook1)
		{
			this.cS_.addObjects[3] = this.addBook1;
			this.cS_.addObjects[3].SetActive(false);
		}
		if (this.addTelefon1)
		{
			this.cS_.addObjects[4] = this.addTelefon1;
			this.cS_.addObjects[4].SetActive(false);
		}
		if (this.addDartPfeil1)
		{
			this.cS_.addObjects[5] = this.addDartPfeil1;
			this.cS_.addObjects[5].SetActive(false);
		}
		if (this.addController1)
		{
			this.cS_.addObjects[6] = this.addController1;
			this.cS_.addObjects[6].SetActive(false);
		}
		if (this.addStift1)
		{
			this.cS_.addObjects[7] = this.addStift1;
			this.cS_.addObjects[7].SetActive(false);
		}
		if (this.addHammer1)
		{
			this.cS_.addObjects[8] = this.addHammer1;
			this.cS_.addObjects[8].SetActive(false);
		}
		if (this.addSchraubenzieher1)
		{
			this.cS_.addObjects[9] = this.addSchraubenzieher1;
			this.cS_.addObjects[9].SetActive(false);
		}
		if (this.addGolf)
		{
			this.cS_.addObjects[10] = this.addGolf;
			this.cS_.addObjects[10].SetActive(false);
		}
		this.clothScript_ = GameObject.FindGameObjectWithTag("Main").GetComponent<clothScript>();
		this.SetEyes(forcedClothes, this.cS_.model_eyes);
		this.SetHairs(forcedClothes, this.cS_.model_hair);
		this.SetBeard(forcedClothes, this.cS_.model_beard);
		this.SetSkinColor(forcedClothes, this.cS_.model_skinColor);
		this.SetHairColor(forcedClothes, this.cS_.model_hairColor, this.cS_.model_beardColor);
		this.SetClothColors(forcedClothes, this.cS_.model_HoseColor, this.cS_.model_ShirtColor, this.cS_.model_Add1Color);
		UnityEngine.Object.Destroy(this);
	}

	
	private void SetEyes(bool force, int model1)
	{
		int num = 0;
		if (!force)
		{
			if (this.male)
			{
				if (UnityEngine.Random.Range(0, 100) < 20)
				{
					num = UnityEngine.Random.Range(1, this.clothScript_.prefabMaleEyes.Length);
				}
				this.myEyes = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabMaleEyes[num], this.boneHead.transform);
				this.cS_.model_eyes = num;
				return;
			}
			if (UnityEngine.Random.Range(0, 100) < 20)
			{
				num = UnityEngine.Random.Range(1, this.clothScript_.prefabFemaleEyes.Length);
			}
			this.myEyes = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabFemaleEyes[num], this.boneHead.transform);
			this.cS_.model_eyes = num;
			return;
		}
		else
		{
			if (model1 <= -1)
			{
				return;
			}
			if (this.male)
			{
				this.myEyes = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabMaleEyes[model1], this.boneHead.transform);
				return;
			}
			this.myEyes = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabFemaleEyes[model1], this.boneHead.transform);
			return;
		}
	}

	
	private void SetHairs(bool force, int model1)
	{
		if (!force)
		{
			int num;
			if (!this.male)
			{
				num = UnityEngine.Random.Range(0, this.clothScript_.prefabFemaleHairs.Length);
				this.myHair = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabFemaleHairs[num], this.boneHead.transform);
				this.cS_.model_hair = num;
				return;
			}
			if (UnityEngine.Random.Range(0, 100) < 10)
			{
				return;
			}
			num = UnityEngine.Random.Range(0, this.clothScript_.prefabMaleHairs.Length);
			this.myHair = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabMaleHairs[num], this.boneHead.transform);
			this.cS_.model_hair = num;
			return;
		}
		else
		{
			if (model1 <= -1)
			{
				return;
			}
			if (this.male)
			{
				this.myHair = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabMaleHairs[model1], this.boneHead.transform);
				return;
			}
			this.myHair = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabFemaleHairs[model1], this.boneHead.transform);
			return;
		}
	}

	
	private void SetBeard(bool force, int model1)
	{
		if (!this.male)
		{
			return;
		}
		if (!force)
		{
			if (UnityEngine.Random.Range(0, 100) < 33)
			{
				int num = UnityEngine.Random.Range(0, this.clothScript_.prefabBeards.Length);
				this.myBeard = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabBeards[num], this.boneHead.transform);
				this.cS_.model_beard = num;
				return;
			}
		}
		else
		{
			if (model1 <= -1)
			{
				return;
			}
			this.myBeard = UnityEngine.Object.Instantiate<GameObject>(this.clothScript_.prefabBeards[model1], this.boneHead.transform);
		}
	}

	
	private void SetSkinColor(bool force, int color1)
	{
		this.indexSkinColor = 0;
		if (force)
		{
			this.objectSkin.GetComponent<Renderer>().material = this.clothScript_.matColor_Skin[color1];
			return;
		}
		if (UnityEngine.Random.Range(0, 100) < 60)
		{
			int num = UnityEngine.Random.Range(0, this.clothScript_.matColor_Skin.Length);
			this.objectSkin.GetComponent<Renderer>().material = this.clothScript_.matColor_Skin[num];
			this.cS_.model_skinColor = num;
			return;
		}
		this.cS_.model_skinColor = 0;
	}

	
	private void SetHairColor(bool force, int colorHair, int colorBeard)
	{
		if (!force)
		{
			if (this.male)
			{
				this.indexHairColor = UnityEngine.Random.Range(0, this.clothScript_.matColor_MaleHair.Length);
				if (this.myHair)
				{
					this.myHair.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_MaleHair[this.indexHairColor];
					this.cS_.model_hairColor = this.indexHairColor;
				}
				if (this.myBeard)
				{
					this.myBeard.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_MaleHair[this.indexHairColor];
					this.cS_.model_beardColor = this.indexHairColor;
					return;
				}
			}
			else
			{
				this.indexHairColor = UnityEngine.Random.Range(0, this.clothScript_.matColor_FemaleHair.Length);
				if (this.myHair)
				{
					this.myHair.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_FemaleHair[this.indexHairColor];
					this.cS_.model_hairColor = this.indexHairColor;
				}
				if (this.myBeard)
				{
					this.myBeard.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_FemaleHair[this.indexHairColor];
					this.cS_.model_beardColor = this.indexHairColor;
					return;
				}
			}
		}
		else if (this.male)
		{
			if (this.myHair)
			{
				this.myHair.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_MaleHair[colorHair];
			}
			if (this.myBeard)
			{
				this.myBeard.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_MaleHair[colorBeard];
				return;
			}
		}
		else
		{
			if (this.myHair)
			{
				this.myHair.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_FemaleHair[colorHair];
			}
			if (this.myBeard)
			{
				this.myBeard.GetComponentInChildren<Renderer>().material = this.clothScript_.matColor_FemaleHair[colorBeard];
			}
		}
	}

	
	private void SetClothColors(bool force, int colorHose, int colorShirt, int colorAdd1)
	{
		if (!force)
		{
			if (!this.objectHose)
			{
				return;
			}
			int num;
			if (this.male)
			{
				num = UnityEngine.Random.Range(0, this.clothScript_.matColor_MaleHose.Length);
				this.objectHose.GetComponent<Renderer>().material = this.clothScript_.matColor_MaleHose[num];
				this.cS_.model_HoseColor = num;
			}
			else
			{
				num = UnityEngine.Random.Range(0, this.clothScript_.matColor_FemaleHose.Length);
				this.objectHose.GetComponent<Renderer>().material = this.clothScript_.matColor_FemaleHose[num];
				this.cS_.model_HoseColor = num;
			}
			if (!this.objectShirt)
			{
				return;
			}
			if (this.male)
			{
				num = UnityEngine.Random.Range(0, this.clothScript_.matColor_MaleShirt.Length);
				this.objectShirt.GetComponent<Renderer>().material = this.clothScript_.matColor_MaleShirt[num];
				this.cS_.model_ShirtColor = num;
			}
			else
			{
				num = UnityEngine.Random.Range(0, this.clothScript_.matColor_FemaleShirt.Length);
				this.objectShirt.GetComponent<Renderer>().material = this.clothScript_.matColor_FemaleShirt[num];
				this.cS_.model_ShirtColor = num;
			}
			if (!this.objectAdd1)
			{
				return;
			}
			num = UnityEngine.Random.Range(0, this.clothScript_.matColor_AllColors.Length);
			this.objectAdd1.GetComponent<Renderer>().material = this.clothScript_.matColor_AllColors[num];
			this.cS_.model_Add1Color = num;
			return;
		}
		else
		{
			if (!this.objectHose)
			{
				return;
			}
			if (this.male)
			{
				this.objectHose.GetComponent<Renderer>().material = this.clothScript_.matColor_MaleHose[this.cS_.model_HoseColor];
			}
			else
			{
				this.objectHose.GetComponent<Renderer>().material = this.clothScript_.matColor_FemaleHose[this.cS_.model_HoseColor];
			}
			if (!this.objectShirt)
			{
				return;
			}
			if (this.male)
			{
				this.objectShirt.GetComponent<Renderer>().material = this.clothScript_.matColor_MaleShirt[this.cS_.model_ShirtColor];
			}
			else
			{
				this.objectShirt.GetComponent<Renderer>().material = this.clothScript_.matColor_FemaleShirt[this.cS_.model_ShirtColor];
			}
			if (!this.objectAdd1)
			{
				return;
			}
			this.objectAdd1.GetComponent<Renderer>().material = this.clothScript_.matColor_AllColors[this.cS_.model_Add1Color];
			return;
		}
	}

	
	public bool male;

	
	public GameObject boneHead;

	
	public GameObject objectSkin;

	
	public GameObject objectHose;

	
	public GameObject objectShirt;

	
	public GameObject objectAdd1;

	
	public GameObject addTasse1;

	
	public GameObject addMuel1;

	
	public GameObject addGiesskanne1;

	
	public GameObject addBook1;

	
	public GameObject addTelefon1;

	
	public GameObject addDartPfeil1;

	
	public GameObject addController1;

	
	public GameObject addStift1;

	
	public GameObject addHammer1;

	
	public GameObject addSchraubenzieher1;

	
	public GameObject addGolf;

	
	private clothScript clothScript_;

	
	private GameObject myEyes;

	
	private GameObject myHair;

	
	private GameObject myBeard;

	
	private GameObject myHat;

	
	private int indexSkinColor;

	
	private int indexHairColor;

	
	private characterScript cS_;
}
