using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Gold : GameObject
	{
		public static readonly char DefaultGoldChar = '\u0024';//$
		public int Value { get; set; }
		public Gold(int x, int y, int count)
		{
			this.x = x;
			this.y = y;
			this.Value = count;
			renderChar = DefaultGoldChar;
			renderFgColor = ConsoleColor.Yellow;
			isColizionable = true;
		}
	}
}
