using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Level
	{
		List<Room> rooms;
		List<Road> roads;

		internal List<Room> Rooms { get => rooms; set => rooms = value; }
		internal List<Road> Roads { get => roads; set => roads = value; }

		public Level()
		{
			rooms = new List<Room>();
			roads = new List<Road>();
		}

		public void AddRoom(Room room)
		{
			rooms.Add(room);
		}

		public void RenderAllRoom()
		{
			
		}

		public void AddRoad(Road road)
		{
			roads.Add(road);
		}

		internal Room GetRommWithCoord(int x, int y)
		{
			List<Room> output = rooms.FindAll(room=>(room.X <= x && (room.X + room.Width) >= x) && (room.Y <= y && (room.Y + room.Height) >= y));
			//todo: patikrinti
			if (output.Count > 0)
			{
				return output[0];
			}
			return null;
		}

		internal Room GetActiveRoom()
		{
			List<Room> output = rooms.FindAll(room =>room.IsActive);
			//todo: patikrinti
			return output[0];
		}
	}
}
