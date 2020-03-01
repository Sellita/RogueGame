using RogueGame.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Control
{
	class LvlGenerator
	{
		private const int minimumAreaForEnemy = 70;
		static Random rnd = new Random();
		public LvlGenerator()
		{
			
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
