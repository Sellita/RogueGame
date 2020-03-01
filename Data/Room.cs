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
		public bool IsRoomtypeInitialized = false;
		private List<Road> roads;



		private List<Enemy> enemies;
		private List<GameObject>[][] roomObjects;
		private bool isExplored;
		private bool isActive;
		public List<GameObject>[][] RoomObjects { get => roomObjects; }
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

		internal RoomType RoomType { get => roomType; set { roomType = value; IsRoomtypeInitialized = true; } }

		public Room(int x, int y, int width, int height)
		{
			id = count;
			count++;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;

			//create empty field
			roomObjects = new List<GameObject>[width][];
			for (int i = 0; i < roomObjects.Length; i++)
			{
				roomObjects[i] = new List<GameObject>[height];
				for (int j = 0; j < roomObjects[i].Length; j++)
				{
					roomObjects[i][j] = new List<GameObject>();
				}
			}
			FillWalls();

		}

		internal void AddReplaceObject(GameObject start)
		{
			roomObjects[start.x - x][start.y - y].Clear();
			roomObjects[start.x-x][start.y-y].Add(start);			
		}

		private void AddObject(GameObject start)
		{
			roomObjects[start.x - x][start.y - y].Add(start);
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
						roomObjects[i][j].Add(new WallBlock(x+i, y+j));
					}
				}
				else
				{
					roomObjects[i][0].Add(new WallBlock(x + i, y));
					roomObjects[i][height-1].Add(new WallBlock(x + i, y + height-1));
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

		public ArrayElementsStruct ArrayElementsToXY(int x, int y)
		{
			return new ArrayElementsStruct(x + this.x, y + this.y);
		}

		internal GameObject GetFirstObject(int x, int y)
		{
			ArrayElementsStruct coord = XYToArrayElements(x, y);
			if (roomObjects[coord.x][coord.y].Count > 0)
			{
				return roomObjects[coord.x][coord.y][0];
			}
			return null;
		}
		internal GameObject GetLastObject(int x, int y)
		{
			ArrayElementsStruct coord = XYToArrayElements(x, y);
			return roomObjects[coord.x][coord.y][roomObjects[coord.x][coord.y].Count-1];
		}
		internal List<GameObject> GetObjects(int x, int y)
		{
			ArrayElementsStruct coord = XYToArrayElements(x, y);
			return roomObjects[coord.x][coord.y];
		}

		internal void MoveObject(int oldX, int oldY, Hero hero)
		{
			//AddReplaceObject(hero);
			AddObject(hero);
			RemoveObject(oldX, oldY, hero);
		}



		internal void RemoveObject(int oldX, int oldY, GameObject gameObject)
		{
			ArrayElementsStruct coord = XYToArrayElements(oldX, oldY);
			roomObjects[coord.x][coord.y].Remove(gameObject);
		}
		internal void RemoveObject(GameObject gameObject)
		{
			ArrayElementsStruct coord = XYToArrayElements(gameObject.x, gameObject.y);
			roomObjects[coord.x][coord.y].Remove(gameObject);
		}
		internal int GetArea()
		{
			return width * height;
		}

		internal ArrayElementsStruct GetEmptyCenter()
		{
			int x = X + Width / 2;
			int y = Y + Height / 2;

			while (GetObjects(x, y).Count != 0)
			{
				x++;
				if(GetObjects(x, y).Count != 0)
				{
					y++;
				}
			}
			if (GetObjects(x, y).Count == 0)
			{
				return new ArrayElementsStruct(x, y);
			}
			return new ArrayElementsStruct(-1, -1);
		}
	}
}
