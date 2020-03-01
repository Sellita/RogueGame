using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class RoadBlock:GameObject
	{
		public static readonly char RoadChar = '\u2588';
		public Road Road { get; set; }

		List<GameObject> objectsOnRoad;

		public RoadBlock(int x, int y)
		{
			objectsOnRoad = new List<GameObject>();
			IsExplored = false;
			this.x = x;
			this.y = y;
			renderChar = RoadChar;
		}

		public void AddObject(GameObject gameObject)
		{
			objectsOnRoad.Add(gameObject);
		}

		internal bool IsExistObject()
		{
			if (objectsOnRoad.Count > 0)
			{
				return true;
			}
			return false;
		}

		internal GameObject getLastObject()
		{
			if (IsExistObject())
			{
				return objectsOnRoad[objectsOnRoad.Count-1];
			}
			return null;
		}

		public void RemoveObject(GameObject gameObject)
		{
			objectsOnRoad.Remove(gameObject);
		}

	}
}
