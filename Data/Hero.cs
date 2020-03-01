using RogueGame.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Hero : GameObject, IMovable
	{
		public static readonly char HeroChar = '\u263A';
		private string name;
		private int hp;
		private int def;
		private int speed;
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

		public int Hp { get => hp; set => hp = value; }
		public int Def { get => def; set => def = value; }
		public int Speed { get => speed; set => speed = value; }

		public void Move(Directions direction)
		{
			switch (direction)
			{
				case Directions.Left:
					x--;
					break;
				case Directions.Right:
					x++;
					break;
				case Directions.Up:
					y--;
					break;
				case Directions.Down:
					y++;
					break;
				default:
					break;
			}
		}

		public void SetStartPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

	}
}
