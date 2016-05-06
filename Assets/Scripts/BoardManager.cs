using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

enum Tile
{
	Unused = 0,
	DirtWall,
	DirtFloor,
	Corridor,
	Door,
	UpStairs,
	DownStairs
};

public class BoardManager : MonoBehaviour 
{
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min , int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	private List<Room> _rooms = new List<Room>();
	private List<Corridor> _corridors = new List<Corridor>();
	public GameObject[] wallTiles;
	public GameObject[] floorTiles;
	public GameObject[] doorTiles;
	private int _numberOfRooms = 0 ;
	private Transform _boardHolder;
	private List<Vector3> gridPosition = new List<Vector3> ();

	private int _xSize = 100;
	private int _ySize = 50;

	Tile[] _dungeonMap = {};

	const int ChanceRoom = 75;

	public static bool IsWall(int x, int y, int xLen, int yLen, int xT, int yT, Direction dir)
	{
		Func<int, int, int> = GetFeatureLowerBound;
		Func<int, int, int> = IsFeatureWallBound;

		switch (dir) 
		{

		}
	}

	void BoardSetup(int level)
	{
		_boardHolder = new GameObject ("Board").transform;

		Room room = new Room (0,
			0,
			Random.Range (5, 10),
			Random.Range (6, 10),
			level);
		_rooms.Add (room);
		Direction dir = Direction.Up;
		var door = room.GetRandomDoorPosition (out dir);
		Corridor cor = new Corridor((int)door.x, (int)door.y, Random.Range(4,8), 5, dir);
		_corridors.Add (cor);
	}

	public void SetupScene(int level)
	{
		BoardSetup (level);
//		InitialiseList ();
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
