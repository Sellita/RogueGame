using RogueGame.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Control
{
	class LvlGenerator
	{
		private const int minimumAreaForEnemy = 70;
		public static Random rnd = new Random();

		private const int minX = 1;
		private const int maxX = 98;
		private const int minY = 1;
		private const int maxY = 20;
		private const int minWidth = 7;
		private const int maxWidth = 30;
		private const int minHeight = 5;
		private const int maxHeight = 15;
		public LvlGenerator()
		{
			
		}

		public static Room GetRandomRoom()
		{
			int x = rnd.Next(minX, maxX);
			int y = rnd.Next(minY, maxY);
			int width = rnd.Next(minWidth, maxWidth);
			int height = rnd.Next(minHeight, maxHeight);
			while(x+width > 97) 
			{
				x = rnd.Next(minX, maxX);
				width = rnd.Next(minWidth, maxWidth);
			}
			while (y + height > 26)
			{
				y = rnd.Next(minY, maxY);
				height = rnd.Next(minHeight, maxHeight);
			}
			return new Room(x, y, width,height);
		}

		public static Room GetHalfRandomRoom(Directions direction, int x, int y)
		{
			int randomWidth = 0;
			int randomheight = 0;
			int roomX = 0;
			int roomY = 0;
			int stopCounter = 5;
			while (roomX < 1 || roomY < 1 || roomX + randomWidth > 97 || roomY + randomheight > 25)
			{
				if(stopCounter <= 0) { return null; }
				stopCounter--;
				randomWidth = rnd.Next(minWidth, maxWidth);
				randomheight = rnd.Next(minHeight, maxHeight);
				switch (direction)
				{
					case Directions.Left:
						roomX = x - randomWidth +1;
						roomY = rnd.Next(y - randomheight + 2, y - 2);
						break;
					case Directions.Right:
						roomX = x;
						roomY = rnd.Next(y - randomheight + 2, y - 2);
						break;
					case Directions.Up:
						roomX = rnd.Next(x - randomWidth + 2, x - 2);
						roomY = y - randomheight+1;
						break;
					case Directions.Down:
						roomX = rnd.Next(x - randomWidth + 2, x - 2);
						roomY = y;
						break;
					default:
						break;
				}
			}
			return new Room(roomX, roomY, randomWidth, randomheight);
		}

		


		public static void SetRoomsType(List<Room> rooms)
		{
			//random room with nextLvl
			int randomRoom = rnd.Next(0, rooms.Count);
			while (rooms[randomRoom].IsRoomtypeInitialized)
			{
				randomRoom = rnd.Next(0, rooms.Count);
			}
			rooms[randomRoom].RoomType = RoomType.Boss;
			while (rooms[randomRoom].IsRoomtypeInitialized)
			{
				randomRoom = rnd.Next(0, rooms.Count);
			}
			rooms[randomRoom].RoomType = RoomType.Start;		

			foreach (Room room in rooms)
			{
				if (room.IsRoomtypeInitialized == false)
				{
					int area = room.GetArea();
					if (area > minimumAreaForEnemy)
					{
						int randomtypeInt = rnd.Next(0, Enum.GetNames(typeof(RoomType)).Length);

						while ((RoomType)randomtypeInt == RoomType.Start || (RoomType)randomtypeInt == RoomType.Boss)//boss and start room only one 
						{
							randomtypeInt = rnd.Next(0, Enum.GetNames(typeof(RoomType)).Length);
						}
						room.RoomType = (RoomType)randomtypeInt;
					}
					else
					{
						room.RoomType = RoomType.Bonus;
					}
				}

			}
		}
	}
}
