using System;
using System.Collections;
using UnityEngine;


public class arbeitsmarkt : MonoBehaviour
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
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	public charArbeitsmarkt CreateArbeitsmarktItem()
	{
		charArbeitsmarkt component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<charArbeitsmarkt>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.guiMain_ = this.guiMain_;
		return component;
	}

	
	public void ArbeitsmarktUpdaten()
	{
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Arbeitsmarkt");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				charArbeitsmarkt component = array[i].GetComponent<charArbeitsmarkt>();
				if (component)
				{
					component.wochenAmArbeitsmarkt++;
					if (component.wochenAmArbeitsmarkt > 12 && UnityEngine.Random.Range(0, component.wochenAmArbeitsmarkt * 3) > UnityEngine.Random.Range(0, 100))
					{
						base.StartCoroutine(this.Remove(component));
					}
				}
			}
		}
		if (this.mS_.globalEvent != 3 && array.Length < 30)
		{
			if (!this.mS_.multiplayer)
			{
				for (int j = 0; j < 2; j++)
				{
					if (UnityEngine.Random.Range(0, 100) > 50)
					{
						charArbeitsmarkt charArbeitsmarkt = this.CreateArbeitsmarktItem();
						if (charArbeitsmarkt)
						{
							charArbeitsmarkt.Create(null);
						}
					}
				}
				return;
			}
			for (int k = 0; k < 7; k++)
			{
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					charArbeitsmarkt charArbeitsmarkt2 = this.CreateArbeitsmarktItem();
					if (charArbeitsmarkt2)
					{
						charArbeitsmarkt2.Create(null);
					}
				}
			}
		}
	}

	
	private IEnumerator Remove(charArbeitsmarkt script_)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (script_)
		{
			script_.RemoveFromArbeitsmarkt(false);
		}
		yield break;
	}

	
	public const int perk_player = 0;

	
	public const int perk_starDesigner = 1;

	
	public const int perk_noPause = 2;

	
	public const int perk_noBugs = 3;

	
	public const int perk_loyal = 4;

	
	public const int perk_talent = 5;

	
	public const int perk_glueck = 6;

	
	public const int perk_sport = 7;

	
	public const int perk_sauber = 8;

	
	public const int perk_naturfreund = 9;

	
	public const int perk_krank = 10;

	
	public const int perk_frieren = 11;

	
	public const int perk_bescheiden = 12;

	
	public const int perk_klo = 13;

	
	public const int perk_fuehrung = 14;

	
	public const int perk_allrounder = 15;

	
	public const int perk_unordentlich = 16;

	
	public const int perk_menschenfreund = 17;

	
	public const int perk_gierig = 18;

	
	public const int perk_immunschwach = 19;

	
	public const int perk_unbelastbar = 20;

	
	public const int perk_unkonzentriert = 21;

	
	public const int perk_untalentiert = 22;

	
	public const int perk_pixelArtist = 23;

	
	public const int perk_portSpecialist = 24;

	
	public const int perk_serienDesigner = 25;

	
	public const int perk_engineExperte = 26;

	
	public const int perk_noCritic = 27;

	
	public const int perk_arbeitstier = 28;

	
	public const int perk_effizient = 29;

	
	public GameObject[] uiPrefabs;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;
}
