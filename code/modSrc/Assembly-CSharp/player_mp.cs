using System;
using System.Collections.Generic;


public class player_mp
{
	
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

	
	public float timeout;

	
	public int playerID;

	
	public string playerName;

	
	public string companyName;

	
	public int companyLogo;

	
	public int companyCountry;

	
	public long money;

	
	public int fans;

	
	public bool playerReady;

	
	public bool playerPause;

	
	public int[] awards;

	
	public List<object_mp> objects;

	
	public int[,] mapRoomID;

	
	public int[,] mapRoomTyp;

	
	public int[,] mapDoors;

	
	public int[,] mapWindows;

	
	public long awards_SOTY;

	
	public long awards_POTY;

	
	public bool ready;

	
	public bool[] forschungSonstiges;

	
	public bool[] genres;

	
	public bool[] themes;

	
	public bool[] engineFeatures;

	
	public bool[] gameplayFeatures;

	
	public bool[] hardware;

	
	public bool[] hardwareFeatures;
}
