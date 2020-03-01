using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Equipment: GameObject, IVearable
	{
		private int damage = 0;
		private int defense = 0;
		public EquipmentType Equipmenttype { get; set; }
		public int Damage { get => damage; set => damage = value; }
		public int Defense { get => defense; set => defense = value; }

		public Equipment(int x, int y, EquipmentType type, int damage, int defense, char renderChar, string name)
		{
			this.x = x;
			this.y = y;
			this.Equipmenttype = type;
			this.damage = damage;
			this.defense = defense;
			this.renderChar = renderChar;
			this.Name = name;
			isColizionable = true;
		}

	}
}
