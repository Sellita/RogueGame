using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Door : GameObject
	{
		public static readonly char doorChar = 'Ω';
		public Door(int x, int y)
		{
			this.x = x;
			this.y = y;
			renderChar = doorChar;
			renderFgColor = ConsoleColor.DarkMagenta;
			isColizionable = true;
		}
	}
}
