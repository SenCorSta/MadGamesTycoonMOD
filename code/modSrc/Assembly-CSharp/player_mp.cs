using System;
using System.Collections.Generic;

// Token: 0x020002C6 RID: 710
public class player_mp
{
	// Token: 0x06001985 RID: 6533 RVA: 0x0010BC54 File Offset: 0x00109E54
	public player_mp(int playerID_)
	{
		this.timeout = 0f;
		this.playerID = playerID_;
		this.playerName = "";
		this.companyName = "";
		this.companyLogo = 0;
		this.companyCountry = 0;
		this.money = 0L;
		this.fans = 0;
		this.playerReady = false;
		this.playerPause = false;
		this.awards = new int[30];
		this.objects = new List<object_mp>();
		this.mapRoomID = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapRoomTyp = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapDoors = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapWindows = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.awards_SOTY = 0L;
		this.awards_POTY = 0L;
		this.ready = false;
		this.forschungSonstiges = new bool[1];
		this.genres = new bool[1];
		this.themes = new bool[1];
		this.engineFeatures = new bool[1];
		this.gameplayFeatures = new bool[1];
		this.hardware = new bool[1];
		this.hardwareFeatures = new bool[1];
	}

	// Token: 0x040020AB RID: 8363
	public float timeout;

	// Token: 0x040020AC RID: 8364
	public int playerID;

	// Token: 0x040020AD RID: 8365
	public string playerName;

	// Token: 0x040020AE RID: 8366
	public string companyName;

	// Token: 0x040020AF RID: 8367
	public int companyLogo;

	// Token: 0x040020B0 RID: 8368
	public int companyCountry;

	// Token: 0x040020B1 RID: 8369
	public long money;

	// Token: 0x040020B2 RID: 8370
	public int fans;

	// Token: 0x040020B3 RID: 8371
	public bool playerReady;

	// Token: 0x040020B4 RID: 8372
	public bool playerPause;

	// Token: 0x040020B5 RID: 8373
	public int[] awards;

	// Token: 0x040020B6 RID: 8374
	public List<object_mp> objects;

	// Token: 0x040020B7 RID: 8375
	public int[,] mapRoomID;

	// Token: 0x040020B8 RID: 8376
	public int[,] mapRoomTyp;

	// Token: 0x040020B9 RID: 8377
	public int[,] mapDoors;

	// Token: 0x040020BA RID: 8378
	public int[,] mapWindows;

	// Token: 0x040020BB RID: 8379
	public long awards_SOTY;

	// Token: 0x040020BC RID: 8380
	public long awards_POTY;

	// Token: 0x040020BD RID: 8381
	public bool ready;

	// Token: 0x040020BE RID: 8382
	public bool[] forschungSonstiges;

	// Token: 0x040020BF RID: 8383
	public bool[] genres;

	// Token: 0x040020C0 RID: 8384
	public bool[] themes;

	// Token: 0x040020C1 RID: 8385
	public bool[] engineFeatures;

	// Token: 0x040020C2 RID: 8386
	public bool[] gameplayFeatures;

	// Token: 0x040020C3 RID: 8387
	public bool[] hardware;

	// Token: 0x040020C4 RID: 8388
	public bool[] hardwareFeatures;
}
