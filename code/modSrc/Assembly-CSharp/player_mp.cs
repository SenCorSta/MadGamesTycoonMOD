using System;
using System.Collections.Generic;

// Token: 0x020002C9 RID: 713
public class player_mp
{
	// Token: 0x060019CE RID: 6606 RVA: 0x001073D4 File Offset: 0x001055D4
	public player_mp(int playerID_)
	{
		this.myPubScript_ = null;
		this.timeout = 0f;
		this.playerID = playerID_;
		this.playerName = "";
		this.money = 0L;
		this.fans = 0;
		this.playerReady = false;
		this.playerPause = false;
		this.objects = new List<object_mp>();
		this.mapRoomID = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapRoomTyp = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapDoors = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.mapWindows = new int[mapScript.mapSizeX, mapScript.mapSizeY];
		this.ready = false;
		this.forschungSonstiges = new bool[1];
		this.genres = new bool[1];
		this.themes = new bool[1];
		this.engineFeatures = new bool[1];
		this.gameplayFeatures = new bool[1];
		this.hardware = new bool[1];
		this.hardwareFeatures = new bool[1];
	}

	// Token: 0x040020CA RID: 8394
	public publisherScript myPubScript_;

	// Token: 0x040020CB RID: 8395
	public float timeout;

	// Token: 0x040020CC RID: 8396
	public int playerID;

	// Token: 0x040020CD RID: 8397
	public string playerName;

	// Token: 0x040020CE RID: 8398
	public long money;

	// Token: 0x040020CF RID: 8399
	public int fans;

	// Token: 0x040020D0 RID: 8400
	public bool playerReady;

	// Token: 0x040020D1 RID: 8401
	public bool playerPause;

	// Token: 0x040020D2 RID: 8402
	public List<object_mp> objects;

	// Token: 0x040020D3 RID: 8403
	public int[,] mapRoomID;

	// Token: 0x040020D4 RID: 8404
	public int[,] mapRoomTyp;

	// Token: 0x040020D5 RID: 8405
	public int[,] mapDoors;

	// Token: 0x040020D6 RID: 8406
	public int[,] mapWindows;

	// Token: 0x040020D7 RID: 8407
	public bool ready;

	// Token: 0x040020D8 RID: 8408
	public bool[] forschungSonstiges;

	// Token: 0x040020D9 RID: 8409
	public bool[] genres;

	// Token: 0x040020DA RID: 8410
	public bool[] themes;

	// Token: 0x040020DB RID: 8411
	public bool[] engineFeatures;

	// Token: 0x040020DC RID: 8412
	public bool[] gameplayFeatures;

	// Token: 0x040020DD RID: 8413
	public bool[] hardware;

	// Token: 0x040020DE RID: 8414
	public bool[] hardwareFeatures;
}
