using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Potion : GameObject
	{
		public static readonly char DefaultPotionChar = '₪'; //'\u03C8';
		public int Value { get; set; }

		public PotionType PotionType;

		public Potion(int x, int y, int value, PotionType potionType)
		{
			this.x = x;
			this.y = y;
			this.Value = value;
			this.PotionType = potionType;
			renderChar = DefaultPotionChar;
			isColizionable = true;
			SetColor();
		}

		private void SetColor()
		{
			switch (PotionType)
			{
				case PotionType.Health:
					renderFgColor = ConsoleColor.Red;
					break;
				case PotionType.Speed:
					renderFgColor = ConsoleColor.Yellow;
					break;
				case PotionType.Defense:
					renderFgColor = ConsoleColor.Green;
					break;
				case PotionType.Atack:
					renderFgColor = ConsoleColor.Blue;
					break;
				default:
					break;
			}
		}
	}
}
