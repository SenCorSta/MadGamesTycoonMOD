using System;
using System.Collections.Generic;


public class player_mp
{
	
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

	
	public publisherScript myPubScript_;

	
	public float timeout;

	
	public int playerID;

	
	public string playerName;

	
	public long money;

	
	public int fans;

	
	public bool playerReady;

	
	public bool playerPause;

	
	public List<object_mp> objects;

	
	public int[,] mapRoomID;

	
	public int[,] mapRoomTyp;

	
	public int[,] mapDoors;

	
	public int[,] mapWindows;

	
	public bool ready;

	
	public bool[] forschungSonstiges;

	
	public bool[] genres;

	
	public bool[] themes;

	
	public bool[] engineFeatures;

	
	public bool[] gameplayFeatures;

	
	public bool[] hardware;

	
	public bool[] hardwareFeatures;
}
