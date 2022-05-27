using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class arbeitsmarkt : MonoBehaviour
{
	// Token: 0x06001C27 RID: 7207 RVA: 0x0001362A File Offset: 0x0001182A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x0011AF74 File Offset: 0x00119174
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

	// Token: 0x06001C29 RID: 7209 RVA: 0x0011AFFC File Offset: 0x001191FC
	public charArbeitsmarkt CreateArbeitsmarktItem()
	{
		charArbeitsmarkt component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<charArbeitsmarkt>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.guiMain_ = this.guiMain_;
		return component;
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x0011B04C File Offset: 0x0011924C
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

	// Token: 0x06001C2B RID: 7211 RVA: 0x00013632 File Offset: 0x00011832
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

	// Token: 0x0400235B RID: 9051
	public const int perk_player = 0;

	// Token: 0x0400235C RID: 9052
	public const int perk_starDesigner = 1;

	// Token: 0x0400235D RID: 9053
	public const int perk_noPause = 2;

	// Token: 0x0400235E RID: 9054
	public const int perk_noBugs = 3;

	// Token: 0x0400235F RID: 9055
	public const int perk_loyal = 4;

	// Token: 0x04002360 RID: 9056
	public const int perk_talent = 5;

	// Token: 0x04002361 RID: 9057
	public const int perk_glueck = 6;

	// Token: 0x04002362 RID: 9058
	public const int perk_sport = 7;

	// Token: 0x04002363 RID: 9059
	public const int perk_sauber = 8;

	// Token: 0x04002364 RID: 9060
	public const int perk_naturfreund = 9;

	// Token: 0x04002365 RID: 9061
	public const int perk_krank = 10;

	// Token: 0x04002366 RID: 9062
	public const int perk_frieren = 11;

	// Token: 0x04002367 RID: 9063
	public const int perk_bescheiden = 12;

	// Token: 0x04002368 RID: 9064
	public const int perk_klo = 13;

	// Token: 0x04002369 RID: 9065
	public const int perk_fuehrung = 14;

	// Token: 0x0400236A RID: 9066
	public const int perk_allrounder = 15;

	// Token: 0x0400236B RID: 9067
	public const int perk_unordentlich = 16;

	// Token: 0x0400236C RID: 9068
	public const int perk_menschenfreund = 17;

	// Token: 0x0400236D RID: 9069
	public const int perk_gierig = 18;

	// Token: 0x0400236E RID: 9070
	public const int perk_immunschwach = 19;

	// Token: 0x0400236F RID: 9071
	public const int perk_unbelastbar = 20;

	// Token: 0x04002370 RID: 9072
	public const int perk_unkonzentriert = 21;

	// Token: 0x04002371 RID: 9073
	public const int perk_untalentiert = 22;

	// Token: 0x04002372 RID: 9074
	public const int perk_pixelArtist = 23;

	// Token: 0x04002373 RID: 9075
	public const int perk_portSpecialist = 24;

	// Token: 0x04002374 RID: 9076
	public const int perk_serienDesigner = 25;

	// Token: 0x04002375 RID: 9077
	public const int perk_engineExperte = 26;

	// Token: 0x04002376 RID: 9078
	public const int perk_noCritic = 27;

	// Token: 0x04002377 RID: 9079
	public const int perk_arbeitstier = 28;

	// Token: 0x04002378 RID: 9080
	public const int perk_effizient = 29;

	// Token: 0x04002379 RID: 9081
	public GameObject[] uiPrefabs;

	// Token: 0x0400237A RID: 9082
	private GameObject main_;

	// Token: 0x0400237B RID: 9083
	private mainScript mS_;

	// Token: 0x0400237C RID: 9084
	private textScript tS_;

	// Token: 0x0400237D RID: 9085
	private GUI_Main guiMain_;
}
