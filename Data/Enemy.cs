using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Enemy : GameObject
	{
		private static int count = 0;
		private int id;
		public static readonly char EnemyDefaultChar = '\u03A8';
		public Enemy(int x, int y)
		{
			this.x = x;
			this.y = y;
			id = count;
			count++;
			renderChar = EnemyDefaultChar;
		}
	}
}
