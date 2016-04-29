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

	public int columns = 10;
	public int rows = 10;
	public GameObject[] wallTiles;
	public GameObject[] floorTiles;

	private Transform _boardHolder;
	private List<Vector3> gridPosition = new List<Vector3> ();

	void InitialiseList()
	{
		gridPosition.Clear ();

		for (int x = 1; x < columns-1; x++) 
		{
			for (int y = 1; y < rows-1; y++) 
			{
				gridPosition.Add (new Vector3 (x * 0.16f, y * 0.16f, 0f));
			}
			
		}
	}

	void BoardSetup()
	{
		_boardHolder = new GameObject ("Board").transform;
	
		for (int x = -1; x < columns + 1; x++) 
		{
			for (int y = -1; y < rows + 1; y++) 
			{
				// Пол
				GameObject toInstantiate = floorTiles[0];
				 
				//Левая стенка
				if (y == -1) 
				{
					toInstantiate = wallTiles [5];
				} 

				if (x == columns) 
				{
					//Правая стенка
					toInstantiate = wallTiles [3];
					//Правый нижний угол
					if (y == -1) 
					{
						toInstantiate = wallTiles [4];
					}
				} 

				if (y == rows) 
				{
					//Правый верхний угол
					if (x == columns) 
					{
						toInstantiate = wallTiles [2];
					} 
					else 
					{
						//Верхняя стенка
						toInstantiate = wallTiles [1];
					}
				} 
					
				if (x == -1) 
				{
					//Левая стенка
					toInstantiate = wallTiles [7];
					if (x == -1 && y == -1)
					{
						//Левый нижний угол
						toInstantiate = wallTiles [6];
					}
					if (x == -1 && y == rows) 
					{
						//Левый верхний уровень
						toInstantiate = wallTiles [0];
					}
				}

				GameObject instance = Instantiate (toInstantiate, new Vector3 (x * 0.16f, y * 0.16f, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (_boardHolder);
			}
		}
	}

	public void SetupScene(int level)
	{
		BoardSetup ();

		InitialiseList ();
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
