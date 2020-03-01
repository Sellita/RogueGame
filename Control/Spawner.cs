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
						break;
					case RoomType.Bonus:
						break;
					case RoomType.Boss:
						break;
					case RoomType.Hard:
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
			throw new NotImplementedException();
		}
		internal void SpawnGold(Room rooms)
		{
			throw new NotImplementedException();
		}
		internal void SpawnEquipment(Room rooms)
		{
			throw new NotImplementedException();
		}
		internal void SpawnPotion(Room rooms)
		{
			throw new NotImplementedException();
		}
	}
}
