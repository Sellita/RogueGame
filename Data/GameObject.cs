using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	abstract class GameObject
	{
		internal bool IsExplored { get; set; }
		internal char renderChar;
		internal int x;
		internal int y;
	}
}
