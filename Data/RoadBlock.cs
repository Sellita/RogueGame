using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class RoadBlock:GameObject
	{
		public static readonly char RoadChar = '\u2588';
		

		public RoadBlock(int x, int y)
		{
			this.x = x;
			this.y = y;
			renderChar = RoadChar;
		}

	}
}
