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
		private int lvl;
		private const int MaxGoldValue = 100;
		private const int minPotionValue = 5;
		private const int MaxPotionValue = 15;
		private const int EnemyMinAtack = 1;
		private const int EnemyMaxAtack = 7;

		public Spawner()
		{

		}

		public void SpawnAll(List<Room> rooms, int lvl)
		{
			this.lvl = lvl;
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
				int rndEnemyAtack = rnd.Next(EnemyMinAtack + (lvl*3), EnemyMaxAtack + 1 + (lvl * 3));
				int rndEnemy = rnd.Next(0, defaultEnemys.Count);
				ArrayElementsStruct coord = GetRandomCoord(rooms);
				if (coord.x != -1 && coord.y != -1)
				{
					rooms.AddReplaceObject(new Enemy(coord.x, coord.y, defaultEnemys[rndEnemy].renderChar, defaultEnemys[rndEnemy].Health, defaultEnemys[rndEnemy].Damage));
				}
			}
			

			//throw new NotImplementedException();
		}
		internal void SpawnGold(Room rooms)
		{
			//Todo: add sql
			//throw new NotImplementedException();
			int rndCount = rnd.Next(0, maxGoldCount);
			if (rooms.GetArea() < 60)
			{
				rndCount = rnd.Next(0, 1);
			}

			for (int i = 0; i < rndCount; i++)
			{
				int rndGoldValue = rnd.Next(0, MaxGoldValue);
				ArrayElementsStruct coord = GetRandomCoord(rooms);
				if (coord.x != -1 && coord.y != -1)
				{
					rooms.AddReplaceObject(new Gold(coord.x, coord.y, rndGoldValue));
				}
			}
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
				if (coord.x != -1 && coord.y != -1)
				{
					rooms.AddReplaceObject(new Equipment(coord.x, coord.y, defaultEquipment[rndEquipment].Equipmenttype, defaultEquipment[rndEquipment].Damage, defaultEquipment[rndEquipment].Defense, defaultEquipment[rndEquipment].renderChar, defaultEquipment[rndEquipment].Name));
				}
			}
		}



		internal void SpawnPotion(Room rooms)
		{
			//Todo: add sql
			//throw new NotImplementedException();
			int rndCount = rnd.Next(0, maxPotionCount);
			if (rooms.GetArea() < 60)
			{
				rndCount = rnd.Next(0, 1);
			}

			for (int i = 0; i < rndCount; i++)
			{
				int rndType = rnd.Next(0, Enum.GetNames(typeof(PotionType)).Length);
				int rndValue = rnd.Next(minPotionValue, MaxPotionValue + 1);
				if((PotionType)rndType == PotionType.Atack || (PotionType)rndType == PotionType.Defense)
				{
					rndValue = 1;
				}
				

				ArrayElementsStruct coord = GetRandomCoord(rooms);
				if (coord.x != -1 && coord.y != -1)
				{
					rooms.AddReplaceObject(new Potion(coord.x, coord.y, rndValue, (PotionType)rndType));
				}
			}
		}
		internal void SpawnDoorToNextLvl(Room rooms)
		{
			//throw new NotImplementedException();


			ArrayElementsStruct coord = GetRandomCoord(rooms);
			if (coord.x != -1 && coord.y != -1)
			{
				rooms.AddReplaceObject(new Door(coord.x, coord.y));
			}
		}



		private ArrayElementsStruct GetRandomCoord(Room rooms)
		{
			int x = rnd.Next(2, rooms.Width - 2);
			int y = rnd.Next(2, rooms.Height - 2);
			int exitCounter = 20;
			ArrayElementsStruct coord = rooms.ArrayElementsToXY(x, y);
			while (rooms.GetFirstObject(coord.x, coord.y) != null || exitCounter <= 0)
			{
				x = rnd.Next(2, rooms.Width - 2);
				y = rnd.Next(2, rooms.Height - 2);
				coord = rooms.ArrayElementsToXY(x, y);
				exitCounter--;
				
			}
			if(exitCounter <= 0)
			{
				return new ArrayElementsStruct(-1, -1);
			}
			return coord;
		}
	}
}
