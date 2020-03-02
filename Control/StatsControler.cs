using RogueGame.Data;
using RogueGame.View.Elements;
using RogueGame.View.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Control
{
	class StatsControler
	{
		StatsWindow StatsWindow;

		Hero hero;
		List<int> items;
		private int activeId;
		public StatsControler(Hero hero)
		{
			StatsWindow = new StatsWindow(hero);
			items = new List<int>();
			this.hero = hero;
		}

		public void RunLoop()
		{
			StatsWindow.Render();
			RenderHeroStats();
			RenderHero();
			InventoryLoop();
		}

		private void InventoryLoop()
		{
			bool isNeedRender = true;

			while (isNeedRender)
			{
				ConsoleKeyInfo pressedChar = Console.ReadKey(true);

				switch (pressedChar.Key)
				{
					case ConsoleKey.Escape:
						isNeedRender = false;
						break;
					case ConsoleKey.DownArrow:
						SetActiveNext();
						break;
					case ConsoleKey.UpArrow:
						SetActivePrievious();
						break;
					case ConsoleKey.E:
						AddStat();
						//equip
						break;

				}
			}
		}

		private void AddStat()
		{
			switch (activeId)
			{
				case 0://demagee
					hero.Basedamage++;
					StatsWindow.RenderItem(0, hero.Damage.ToString());
					
					break;
				case 1://def
					hero.BaseDef++;
					StatsWindow.RenderItem(1, hero.Def.ToString());
					break;
				case 2://speed
					hero.Speed++;
					StatsWindow.RenderItem(2, hero.Speed.ToString());
					break;
				case 3://health
					hero.Health++;
					StatsWindow.RenderItem(3, hero.Health.ToString());
					break;
				default:
					break;
			}
			hero.Stats--;
			RenderHero();
			//RenderHeroStats();
		}

		private void RenderHero()
		{
			StatsWindow.SetExp(hero.Exp.ToString());
			StatsWindow.SetPoints(hero.Stats.ToString());
			StatsWindow.SetLevel(hero.Lvl.ToString());



		}

		private void RenderHeroStats()
		{

			StatsWindow.RenderItem(0, hero.Damage.ToString());
			StatsWindow.RenderItem(1, hero.Def.ToString());
			StatsWindow.RenderItem(2, hero.Speed.ToString());
			StatsWindow.RenderItem(3, hero.Health.ToString());
			SetFirstItemActive();
		}

		private string GetText(GameObject gameObject)
		{
			string data = "";

			if (gameObject == null)
			{
				data = InventoryWindow.emptyTxt;
			}
			else
			{
				data = gameObject.Name;
				if (gameObject is Equipment)
				{
					Equipment eq = gameObject as Equipment;
					if (eq.Damage > 0)
					{
						data += " Damage: " + eq.Damage;
					}
					if (eq.Defense > 0)
					{
						data += " Defense: " + eq.Defense;
					}
				}
				//todo add potions
			}
			return data;
		}

		private void SetFirstItemActive()
		{
			activeId = 0;
			RenderActive(true);
			WriteKeyInfo();
		}

		private void RenderActive(bool isactive)
		{
			StatsWindow.SetActive(activeId, isactive);
		}

		public void WriteKeyInfo()
		{
			if (hero.Stats <= 0)
			{
				StatsWindow.SetKeyInfo2("");
				
			}
			else
			{
				StatsWindow.SetKeyInfo2("[E] Add stat");
				
			}
		}

		private void SetActivePrievious()
		{
			RenderActive(false);
			activeId--;
			if (activeId < 0)
			{
				activeId = 4 - 1;
			}
			RenderActive(true);
			WriteKeyInfo();
		}

		private void SetActiveNext()
		{
			RenderActive(false);
			activeId++;
			if (activeId >= 4)
			{
				activeId = 0;
			}
			RenderActive(true);
			WriteKeyInfo();
		}
	}
}
