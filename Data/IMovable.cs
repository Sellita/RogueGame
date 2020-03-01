using System;
using System.Collections.Generic;
using System.Text;
using RogueGame.Control;

namespace RogueGame.Data
{
	interface IMovable
	{
		public void Move(Directions direction);
	}
}
