using RogueGame.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Control
{
	class Spawner
	{
		//room Maximum
		private const int maxEnemyCount = 4;
		private const int maxGoldCount = 3;
		private const int maxEquipmentCount = 2;
		private const int maxPotionCount = 2;
		Random rnd = new Random();

		//Posible Default objects =)
		List<Enemy> defaultEnemys = new List<Enemy>() {
			new Enemy(0, 0, 'E', 10, 2),		
			new Enemy(0, 0, 'S', 20, 4),		
			new Enemy(0, 0, 'D', 15, 3)		
		};

		List<Equipment> defaultEquipment = new List<Equipment>()
		{
			new Equipment(0, 0, EquipmentType.Armor, 0, 1, 'A', "Armor"),
			new Equipment(0, 0, EquipmentType.boots, 0, 1, 'B', "Boots"),
			new Equipment(0, 0, EquipmentType.Gun, 3, 0, '/', "Gun"),
			new Equipment(0, 0, EquipmentType.Helmet, 0, 1, 'H', "Helmet"),
			new Equipment(0, 0, EquipmentType.Panties, 0, 1, 'P', "Panties")
		};

		public Spawner()
		{

		}

		public void SpawnAll(List<Room> rooms, int lvl)
		{
			foreach (Room room in rooms)
			{
				switch (room.RoomType)
				{
					case RoomType.Standart:
						SpawnEnemy(room);
						SpawnGold(room);
						SpawnPotion(room);
						break;
					case RoomType.Bonus:
						SpawnGold(room);
						SpawnPotion(room);
						SpawnEquipment(room);
						break;
					case RoomType.Boss:
						//todo spawn Boss
						SpawnDoorToNextLvl(room);
						SpawnGold(room);
						SpawnPotion(room);
						SpawnEquipment(room);
						break;
					case RoomType.Hard:
						SpawnEnemy(room);
						SpawnEquipment(room);
						SpawnGold(room);
						SpawnPotion(room);						
						break;
					case RoomType.Start:
						break;
					default:
						break;
				}
			}
		}

		internal void SpawnEnemy(Room rooms)
		{
			//Todo: add sql

			//spawner when sql not working 
			int rndEnemyCount = rnd.Next(0, maxEnemyCount);
			for (int i = 0; i < rndEnemyCount; i++)
			{
				int rndEnemy = rnd.Next(0, defaultEnemys.Count);
				ArrayElementsStruct coord = GetRandomCoord(rooms);
				rooms.AddReplaceObject(new Enemy(coord.x, coord.y, defaultEnemys[rndEnemy].renderChar, defaultEnemys[rndEnemy].Health, defaultEnemys[rndEnemy].Damage));
			}
			

			//throw new NotImplementedException();
		}
		internal void SpawnGold(Room rooms)
		{
			//Todo: add sql
			//throw new NotImplementedException();
		}
		internal void SpawnEquipment(Room rooms)
		{
			//Todo: add sql
			//throw new NotImplementedException();
			int rndEquipmentCount = rnd.Next(0, maxEquipmentCount);
			if (rooms.GetArea() < 60)
			{
				rndEquipmentCount = rnd.Next(0, 1);
			}
			
			for (int i = 0; i < rndEquipmentCount; i++)
			{
				int rndEquipment = rnd.Next(0, defaultEquipment.Count);
				ArrayElementsStruct coord = GetRandomCoord(rooms);
				rooms.AddReplaceObject(new Equipment(coord.x, coord.y, defaultEquipment[rndEquipment].Equipmenttype, defaultEquipment[rndEquipment].Damage, defaultEquipment[rndEquipment].Defense, defaultEquipment[rndEquipment].renderChar, defaultEquipment[rndEquipment].Name));
			}
		}



		internal void SpawnPotion(Room rooms)
		{
			//Todo: add sql
			//throw new NotImplementedException();
		}
		internal void SpawnDoorToNextLvl(Room rooms)
		{
			//throw new NotImplementedException();
		}
		private ArrayElementsStruct GetRandomCoord(Room rooms)
		{
			int x = rnd.Next(2, rooms.Width - 2);
			int y = rnd.Next(2, rooms.Height - 2);
			ArrayElementsStruct coord = rooms.ArrayElementsToXY(x, y);
			while (rooms.GetFirstObject(coord.x, coord.y) != null)
			{
				x = rnd.Next(2, rooms.Width - 2);
				y = rnd.Next(2, rooms.Height - 2);
				coord = rooms.ArrayElementsToXY(x, y);
			}
			return coord;
		}
	}
}
