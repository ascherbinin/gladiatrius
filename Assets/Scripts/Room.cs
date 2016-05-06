using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum WallType : byte
{
	LeftTopCorner = 1,
	TopMid = 2,
	RightTopCorner = 3,
	RightMid = 4,
	RightBottomCorner = 5,
	BottomMid = 6,
	LeftBottomCorner = 7,
	LeftMid = 8,
	Door = 9,
	Floor
}

enum Direction : byte
{
	Up = 0,
	Right = 1,
	Down = 2,
	Left = 3
}

class Room
{
	private int _x;
	private int _y;
	private int _width;
	private int _height;
	private int _id;
	private Dictionary<Vector2, WallType> _tiles = new Dictionary<Vector2, WallType>();
	private Dictionary<Direction, List<Vector2>> _boundaries = new Dictionary<Direction, List<Vector2>> ();
	private Vector2 door;

	public int Width
	{
		get { return _width; }
		set { _width = value; }
	}

	public int Height {
		get { return _height; }
		set { _height = value; }
	}

	public int X {
		get { return _x; }
		set { _x = value; }
	}

	public int Y {
		get { return _y; }
		set { _y = value; }
	}

	public int ID {
		get { return _id; }
	}

	public Dictionary<Direction, List<Vector2>> GetBoundaries
	{
		get { return _boundaries;}
	}
		
	public Room(int x, int y, int width, int height, int id)
	{
		_x = x;
		_y = y;
		_width = width;
		_height = height;
		_id = id;
		setBoundaries ();
	}

	public Dictionary<Vector2, WallType> GenerateRoom()
	{
		for (int x = _x-1; x < _width + _x + 1; x++) 
		{
			for (int y = _y-1; y < _height + _y + 1; y++) 
			{
				if (_tiles.ContainsKey(new Vector2(x,y)))
					_tiles.Remove (new Vector2 (x, y));
				_tiles.Add (new Vector2 (x, y), WallType.Floor);
				//Левая стенка
				if (y == _y-1) 
				{
					if (_tiles.ContainsKey(new Vector2(x,y)))
						_tiles.Remove (new Vector2 (x, y));
					_tiles.Add (new Vector2 (x, y), WallType.BottomMid);
				} 

				if (x == _width + _x) 
				{
					//Правая стенка
					if (_tiles.ContainsKey (new Vector2 (x, y)))
						_tiles.Remove (new Vector2 (x, y));
					_tiles.Add (new Vector2 (x, y), WallType.RightMid);
					//Правый нижний угол
					if (y == _y-1) 
					{
						if (_tiles.ContainsKey(new Vector2(x,y)))
							_tiles.Remove (new Vector2 (x, y));
						_tiles.Add (new Vector2 (x, y), WallType.RightBottomCorner);
					}
				} 

				if (y == _height + _y) 
				{
					//Правый верхний угол
					if (x == _width + _x) 
					{
						if (_tiles.ContainsKey(new Vector2(x,y)))
							_tiles.Remove (new Vector2 (x, y));
						_tiles.Add (new Vector2 (x, y), WallType.RightTopCorner);
					} 
					else 
					{
						//Верхняя стенка
						if (_tiles.ContainsKey(new Vector2(x,y)))
							_tiles.Remove (new Vector2 (x, y));
						_tiles.Add (new Vector2 (x, y), WallType.TopMid);
					}
				} 

				if (x == _x-1) 
				{
					if (_tiles.ContainsKey(new Vector2(x,y)))
						_tiles.Remove (new Vector2 (x, y));
					_tiles.Add (new Vector2 (x, y), WallType.LeftMid);
					if (x == _x-1 && y == _y-1)
					{
						//Левый нижний угол
						if (_tiles.ContainsKey(new Vector2(x,y)))
							_tiles.Remove (new Vector2 (x, y));
						_tiles.Add (new Vector2 (x, y), WallType.LeftBottomCorner);
					}
					if (x == _x-1 && y == _height + _y) 
					{
						//Левый верхний уровень
						if (_tiles.ContainsKey(new Vector2(x,y)))
							_tiles.Remove (new Vector2 (x, y));
						_tiles.Add (new Vector2 (x, y), WallType.LeftTopCorner);
					}
				}

				if(x == door.x && y == door.y)
				{
					if (_tiles.ContainsKey(new Vector2(x,y)))
						_tiles.Remove (new Vector2 (x, y));
					_tiles.Add (new Vector2 (x, y), WallType.Door);
				}
			}
		}
		return _tiles;
	}

	private void setBoundaries()
	{
		List<Vector2> upBoundary = new List<Vector2> ();
		List<Vector2> rightBoundary = new List<Vector2> ();
		List<Vector2> downBoundary = new List<Vector2> ();
		List<Vector2> leftBoundary = new List<Vector2> ();
		for (int x = _x-1; x < _width + _x + 1; x++) 
		{
			for (int y = _y-1; y < _height + _y + 1; y++) 
			{
				//Левая стенка
				if (x == _x-1) 
				{
					leftBoundary.Add(new Vector2(x,y));
				} 

				if (x == _width + _x) 
				{
					rightBoundary.Add (new Vector2 (x, y));
				} 

				if (y == _height + _y) 
				{
					upBoundary.Add (new Vector2 (x, y));
				} 

				if (y == _y-1) 
				{
					downBoundary.Add (new Vector2 (x, y));
				}
			}
		}

		_boundaries.Add (Direction.Up, upBoundary);
		_boundaries.Add (Direction.Down, downBoundary);
		_boundaries.Add (Direction.Right, rightBoundary);
		_boundaries.Add (Direction.Left, leftBoundary);
	}

	public Vector2 GetRandomDoorPosition(out Direction dir)
	{
		dir = (Direction)Random.Range (0, 4);
		var arr = _boundaries [dir];
		door = arr [Random.Range (arr.Count / 2 - 2, arr.Count / 2 + 2)];
		return door;
	}
}
