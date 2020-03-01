using RogueGame.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Room
	{
		private static int count = 0;
		int id;
		private int x;
		private int y;
		private int width;
		private int height;
		private RoomType roomType;
		private List<Road> roads;
		private List<Enemy> enemies;
		private GameObject[][] roomObjects;
		private bool isExplored;
		private bool isActive;
		public GameObject[][] RoomObjects { get => roomObjects; }
		public int X { get => x;  }
		public int Y { get => y; }
		public int Width { get => width;  }
		public int Height { get => height;  }
		public bool IsExplored { get => isExplored; }
		public bool IsActive {
			get => isActive;
			set
			{
				isActive = value;
				if (isActive)
				{
					isExplored = true;
				}
			} 
		}

		internal RoomType RoomType { get => roomType; set => roomType = value; }

		public Room(int x, int y, int width, int height)
		{
			id = count;
			count++;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;

			//create empty field
			roomObjects = new GameObject[width][];
			for (int i = 0; i < roomObjects.Length; i++)
			{
				roomObjects[i] = new GameObject[height];
			}
			FillWalls();

		}

		internal void AddReplaceObject(GameObject start)
		{
			roomObjects[start.x-x][start.y-y] = start;			
		}

		private void FillWalls()
		{
			for (int i = 0; i < roomObjects.Length; i++)
			{
				if(i==0 || i == roomObjects.Length - 1)
				{
					//jai pirmas ar paskutinis stulpelis, pildoma visa eilute, jei ne tik krastines
					for (int j = 0; j < roomObjects[i].Length; j++)
					{
						roomObjects[i][j] = new WallBlock(x+i, y+j);
					}
				}
				else
				{
					roomObjects[i][0] = new WallBlock(x + i, y);
					roomObjects[i][height-1] = new WallBlock(x + i, y + height-1);
				}				
			}

			
		}

		private ArrayElementsStruct XYToArrayElements(GameObject gameObject)
		{
			return new ArrayElementsStruct(gameObject.x - x, gameObject.y - y);
		}
		private ArrayElementsStruct XYToArrayElements(int x, int y)
		{
			return new ArrayElementsStruct(x - this.x, y - this.y);
		}

		internal GameObject GetObject(int x, int y)
		{
			ArrayElementsStruct coord = XYToArrayElements(x, y);
			return roomObjects[coord.x][coord.y];
		}

		internal void MoveObject(int oldX, int oldY, Hero hero)
		{
			AddReplaceObject(hero);
			RemoveObject(oldX, oldY);
		}

		internal void RemoveObject(int oldX, int oldY)
		{
			ArrayElementsStruct coord = XYToArrayElements(oldX, oldY);
			roomObjects[coord.x][coord.y] = null;
		}
	}
}
