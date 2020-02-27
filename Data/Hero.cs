using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Hero : GameObject
	{
		public static readonly char HeroChar = '\u263A';
		private string name;
		public Hero(string name)
		{
			this.name = name;
			
			this.renderChar = HeroChar;
		}
		public Hero(int x, int y, string name)
		{
			this.name = name;
			this.x = x;
			this.y = y;
			this.renderChar = HeroChar;
		}

		public void SetStartPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
