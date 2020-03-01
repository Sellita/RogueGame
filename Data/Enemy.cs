using RogueGame.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Enemy : GameObject, IMovable, IAtackable, ILivable
	{
		private static int count = 0;
		private int id;
		public static readonly char EnemyDefaultChar = '\u03A8';
		private int hp;
		private int damage;

		public int Health { get => hp; set => hp = value; }
		public int Damage { get => damage; set => damage = value; }

		public Enemy(int x, int y)
		{
			this.x = x;
			this.y = y;
			id = count;
			count++;
			isColizionable = false;
			renderChar = EnemyDefaultChar;
		}
		public Enemy(int x, int y, char renderChar, int hp, int atactk)
		{
			this.x = x;
			this.y = y;
			id = count;
			count++;
			isColizionable = false;
			this.renderChar = renderChar;
			this.hp = hp;
			this.damage = atactk;
		}

		public void SetnStartPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

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

		public void Atack(ILivable livable)
		{
			livable.GetDamage(Damage);
		}

		public void GetDamage(int atack)
		{
			hp -= atack;
			if (hp < 0)
			{
				hp = 0;
			}
		}
	}
}
