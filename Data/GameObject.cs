using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	abstract class GameObject
	{
		internal string Name { get; set; }
		internal bool isColizionable { get; set; }
		internal bool IsExplored { get; set; }
		internal char renderChar;
		internal ConsoleColor renderFgColor;
		
		internal int x;
		internal int y;
	}
}
