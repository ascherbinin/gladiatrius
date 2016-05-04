using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

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
	public GameObject[] wallTiles;
	public GameObject[] floorTiles;

	private Transform _boardHolder;
	private List<Vector3> gridPosition = new List<Vector3> ();

//	void InitialiseList()
//	{
//		gridPosition.Clear ();
//
//		for (int x = 1; x < columns-1; x++) 
//		{
//			for (int y = 1; y < rows-1; y++) 
//			{
//				gridPosition.Add (new Vector3 (x * 0.16f, y * 0.16f, 0f));
//			}
//			
//		}
//	}
//
	void BoardSetup()
	{
		_boardHolder = new GameObject ("Board").transform;
		Room room = new Room (0, 0, 10, 10, 1);
		Room room1 = new Room (room.Width+2, room.Height+3, Random.Range(4,7), Random.Range(0,8), 2);
		_rooms.Add (room);
		_rooms.Add (room1);
	}

	public void SetupScene(int level)
	{
		BoardSetup ();
		FillRooms (_rooms);
//		InitialiseList ();
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FillRooms(List<Room> rooms)
	{
		foreach (var room in rooms) {
			Dictionary<Vector2, WallType> tiles = room.GenerateRoom ();
			GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
			foreach (var tile in tiles) {
				switch (tile.Value) {
				case WallType.LeftTopCorner:
					toInstantiate = wallTiles [0];
					break;
				case WallType.TopMid:
					toInstantiate = wallTiles [1];
					break;
				case WallType.RightTopCorner:
					toInstantiate = wallTiles [2];
					break;
				case WallType.RightMid:
					toInstantiate = wallTiles [3];
					break;
				case WallType.RightBottomCorner:
					toInstantiate = wallTiles [4];
					break;
				case WallType.BottomMid:
					toInstantiate = wallTiles [5];
					break;
				case WallType.LeftBottomCorner:
					toInstantiate = wallTiles [6];
					break;
				case WallType.LeftMid:
					toInstantiate = wallTiles [7];
					break;
				default :
					toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
					break;
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (tile.Key.x * 0.16f, tile.Key.y * 0.16f, 0f), Quaternion.identity) as GameObject;
				instance.transform.parent = _boardHolder;
			}
		}
	}
}
