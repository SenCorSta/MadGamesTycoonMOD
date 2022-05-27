using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class arbeitsmarkt : MonoBehaviour
{
	// Token: 0x06001C71 RID: 7281 RVA: 0x00118BC3 File Offset: 0x00116DC3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x00118BCC File Offset: 0x00116DCC
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

	// Token: 0x06001C73 RID: 7283 RVA: 0x00118C54 File Offset: 0x00116E54
	public charArbeitsmarkt CreateArbeitsmarktItem()
	{
		charArbeitsmarkt component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<charArbeitsmarkt>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		component.guiMain_ = this.guiMain_;
		return component;
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x00118CA4 File Offset: 0x00116EA4
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

	// Token: 0x06001C75 RID: 7285 RVA: 0x00118DCC File Offset: 0x00116FCC
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

	// Token: 0x04002375 RID: 9077
	public const int perk_player = 0;

	// Token: 0x04002376 RID: 9078
	public const int perk_starDesigner = 1;

	// Token: 0x04002377 RID: 9079
	public const int perk_noPause = 2;

	// Token: 0x04002378 RID: 9080
	public const int perk_noBugs = 3;

	// Token: 0x04002379 RID: 9081
	public const int perk_loyal = 4;

	// Token: 0x0400237A RID: 9082
	public const int perk_talent = 5;

	// Token: 0x0400237B RID: 9083
	public const int perk_glueck = 6;

	// Token: 0x0400237C RID: 9084
	public const int perk_sport = 7;

	// Token: 0x0400237D RID: 9085
	public const int perk_sauber = 8;

	// Token: 0x0400237E RID: 9086
	public const int perk_naturfreund = 9;

	// Token: 0x0400237F RID: 9087
	public const int perk_krank = 10;

	// Token: 0x04002380 RID: 9088
	public const int perk_frieren = 11;

	// Token: 0x04002381 RID: 9089
	public const int perk_bescheiden = 12;

	// Token: 0x04002382 RID: 9090
	public const int perk_klo = 13;

	// Token: 0x04002383 RID: 9091
	public const int perk_fuehrung = 14;

	// Token: 0x04002384 RID: 9092
	public const int perk_allrounder = 15;

	// Token: 0x04002385 RID: 9093
	public const int perk_unordentlich = 16;

	// Token: 0x04002386 RID: 9094
	public const int perk_menschenfreund = 17;

	// Token: 0x04002387 RID: 9095
	public const int perk_gierig = 18;

	// Token: 0x04002388 RID: 9096
	public const int perk_immunschwach = 19;

	// Token: 0x04002389 RID: 9097
	public const int perk_unbelastbar = 20;

	// Token: 0x0400238A RID: 9098
	public const int perk_unkonzentriert = 21;

	// Token: 0x0400238B RID: 9099
	public const int perk_untalentiert = 22;

	// Token: 0x0400238C RID: 9100
	public const int perk_pixelArtist = 23;

	// Token: 0x0400238D RID: 9101
	public const int perk_portSpecialist = 24;

	// Token: 0x0400238E RID: 9102
	public const int perk_serienDesigner = 25;

	// Token: 0x0400238F RID: 9103
	public const int perk_engineExperte = 26;

	// Token: 0x04002390 RID: 9104
	public const int perk_noCritic = 27;

	// Token: 0x04002391 RID: 9105
	public const int perk_arbeitstier = 28;

	// Token: 0x04002392 RID: 9106
	public const int perk_effizient = 29;

	// Token: 0x04002393 RID: 9107
	public GameObject[] uiPrefabs;

	// Token: 0x04002394 RID: 9108
	private GameObject main_;

	// Token: 0x04002395 RID: 9109
	private mainScript mS_;

	// Token: 0x04002396 RID: 9110
	private textScript tS_;

	// Token: 0x04002397 RID: 9111
	private GUI_Main guiMain_;
}
