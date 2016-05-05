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
	public GameObject[] doorTiles;
	private int _numberOfRooms = 0 ;
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
		Room room1 = new Room((int)door.x, (int)door.y, 5, 5, dir);
		_rooms.Add (room1);
	}

	public void SetupScene(int level)
	{
		BoardSetup (level);
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
			Transform _roomHolder = new GameObject ("ROOM_"+room.ID).transform;
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
				case WallType.Door:
					toInstantiate = doorTiles [0];
					break;
				default :
					toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
					break;
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (tile.Key.x * 0.16f, tile.Key.y * 0.16f, 0f), Quaternion.identity) as GameObject;
				instance.transform.parent = _roomHolder;
				_roomHolder.transform.parent = _boardHolder;
			}
		}
	}

	bool CheckIntersectRoom(int x, int y, int width, int height)
	{
		var topLeft = new Vector2(x, y + height);
		var bottomRight = new Vector2(x + width, y);

		foreach (var checkRoom in _rooms) 
		{
			var topLeftCheck = new Vector2 (checkRoom.X, checkRoom.Y + checkRoom.Height);
			var bottomRightCheck = new Vector2(checkRoom.X + checkRoom.Width, checkRoom.Y);

			if ((topLeft.x < bottomRightCheck.x || topLeftCheck.x > bottomRight.x)
				&& (topLeft.y < bottomRightCheck.y || topLeftCheck.y < bottomRight.y)) {
				Debug.Log ("NOT Intersect ");
			} else {
				Debug.Log("Intersect ");
				return true;
			}
		}
		return false;
	}
}
