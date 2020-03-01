using RogueGame.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Hero : GameObject, IMovable, ILivable, IAtackable
	{
		public static readonly char HeroChar = '\u263A';
		
		private int hp;
		private int def;
		private int damage;
		private const int Basedamage = 4;
		private int speed;

		Equipment helmet;
		Equipment armor;
		Equipment panties;
		Equipment boots;
		Equipment gun;

		public Inventory Inventory { get; set; }
		public Hero(string name, int inventorySpace)
		{
			Name = name;
			Inventory = new Inventory(inventorySpace);
			this.renderChar = HeroChar;
			RecalculateStats();
		}
		public Hero(int x, int y, string name)
		{
			Name = name;
			this.x = x;
			this.y = y;
			this.renderChar = HeroChar;
		}

		
		public int Def { get => def; set => def = value; }
		public int Speed { get => speed; set => speed = value; }
		public int Health { get => hp; set => hp = value; }
		public int Damage { get => damage; set => damage = value; }
		internal Equipment Helmet { get => helmet; set => helmet = value; }
		internal Equipment Armor { get => armor; set => armor = value; }
		internal Equipment Panties { get => panties; set => panties = value; }
		internal Equipment Boots { get => boots; set => boots = value; }
		internal Equipment Gun { get => gun; set => gun = value; }

		public GameObject Equip(GameObject obj)
		{
			if(obj is Equipment)
			{
				Equipment newEquip = obj as Equipment;
				switch (newEquip.Equipmenttype)
				{
					case EquipmentType.Helmet:
						if(helmet != null)
						{
							Equipment toReturn = helmet;
							helmet = newEquip;
							RecalculateStats();
							return toReturn;
						}
						else
						{
							helmet = newEquip;
							RecalculateStats();
							return null;
						}
						
					case EquipmentType.Armor:
						if (armor != null)
						{
							Equipment toReturn = armor;
							armor = newEquip;
							RecalculateStats();
							return toReturn;
						}
						else
						{
							armor = newEquip;
							RecalculateStats();
							return null;
						}
						
					case EquipmentType.Panties:
						if (panties != null)
						{
							Equipment toReturn = panties;
							panties = newEquip;
							RecalculateStats();
							return toReturn;
						}
						else
						{
							panties = newEquip;
							RecalculateStats();
							return null;
						}
						
					case EquipmentType.boots:
						if (boots != null)
						{
							Equipment toReturn = boots;
							boots = newEquip;
							RecalculateStats();
							return toReturn;
						}
						else
						{
							boots = newEquip;
							RecalculateStats();
							return null;
						}
						
					case EquipmentType.Gun:
						if (gun != null)
						{
							Equipment toReturn = gun;
							gun = newEquip;
							RecalculateStats();
							return toReturn;
						}
						else
						{
							gun = newEquip;
							RecalculateStats();
							return null;
						}
						
					default:
						break;
				}
			}
			return obj;
		}

		private void RecalculateStats()
		{
			def = 0;
			if (helmet != null)
			{
				def += helmet.Defense;
			}
			if (armor != null)
			{
				def += armor.Defense;
			}
			if (panties != null)
			{
				def += panties.Defense;
			}
			if (boots != null)
			{
				def += boots.Defense;
			}

			damage = Basedamage;
			if(gun!= null)
			{
				damage += gun.Damage;
			}
		}

		public void Atack(ILivable livable)
		{
			livable.GetDamage(damage);
		}

		public void GetDamage(int atack)
		{
			int dmg = atack - def;
			if (dmg > 0)
			{
				hp = hp - dmg;
				if (hp < 0)
				{
					hp = 0;
				}
			}
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

		public void SetStartPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

	}
}
