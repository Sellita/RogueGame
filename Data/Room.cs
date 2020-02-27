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
		public bool IsExplored { get; set; }
		public bool isActive { get; set; }
		public GameObject[][] RoomObjects { get => roomObjects; }

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


		
	}
}
