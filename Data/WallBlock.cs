using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class WallBlock : GameObject
	{
		public static readonly char WallChar = '#';
		


		public WallBlock(int x, int y)
		{
			this.x = x;
			this.y = y;
			renderChar = WallChar;
		}

	}
}
