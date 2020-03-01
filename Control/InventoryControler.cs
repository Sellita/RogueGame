using RogueGame.Data;
using RogueGame.View.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Control
{
	class InventoryControler
	{
		InventoryWindow inventoryWindow;
		Inventory inventory;
		Hero hero;
		private int activeId;
		public InventoryControler(Inventory inventory, Hero hero)
		{
			inventoryWindow = new InventoryWindow(hero);
			this.inventory = inventory;
			this.hero = hero;
		}

		public void RunLoop()
		{
			inventoryWindow.Render();
			RenderItemsInInventory();
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
						Equip();
						//equip
						break;
					case ConsoleKey.X:
						//remove
						RemoveItem();
						break;
				}
			}
		}

		private void RemoveItem()
		{
			inventory.Items[activeId] = null;
			inventoryWindow.RenderItem(activeId, GetText(inventory.Items[activeId]));
		}

		private void Equip()
		{
			inventory.Items[activeId] = hero.Equip(inventory.Items[activeId]);
			inventoryWindow.RenderItem(activeId, GetText(inventory.Items[activeId]));
			RenderHero();
			WriteKeyInfo();
		}

		private void RenderHero()
		{
			inventoryWindow.SetHelmet(GetText(hero.Helmet));
			inventoryWindow.SetArmor(GetText(hero.Armor));
			inventoryWindow.SetPanties(GetText(hero.Panties));
			inventoryWindow.SetBoots(GetText(hero.Boots));
			inventoryWindow.SetGun(GetText(hero.Gun));

			inventoryWindow.SetAtack(hero.Damage);
			inventoryWindow.SetDefense(hero.Def);
		}

		private void RenderItemsInInventory()
		{
			for (int i = 0; i < inventory.Items.Length; i++)
			{
				string data = GetText(inventory.Items[i]);

				inventoryWindow.RenderItem(i, data);

			}
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
			inventoryWindow.SetActive(activeId, isactive);
		}

		public void WriteKeyInfo()
		{
			if(inventory.Items[activeId] == null)
			{
				inventoryWindow.SetKeyInfo2("");
				inventoryWindow.SetKeyInfo3("");
			}
			else if (inventory.Items[activeId] is Equipment)
			{
				inventoryWindow.SetKeyInfo2("[E] Equip");
				inventoryWindow.SetKeyInfo3("[X] Drop(delete)");
			}
		}

		private void SetActivePrievious()
		{
			RenderActive(false);
			activeId--;
			if(activeId < 0)
			{
				activeId = inventory.Items.Length - 1;
			}
			RenderActive(true);
			WriteKeyInfo();
		}

		private void SetActiveNext()
		{
			RenderActive(false);
			activeId++;
			if (activeId >= inventory.Items.Length)
			{
				activeId = 0;
			}
			RenderActive(true);
			WriteKeyInfo();
		}
	}
}
