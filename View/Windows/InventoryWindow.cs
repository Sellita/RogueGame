using RogueGame.Data;
using RogueGame.View.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Windows
{
	class InventoryWindow : Window
	{
		public static readonly string emptyTxt = "Empty";
		private TextLine[] items;
		TextLine keyInfoHolder1;
		TextLine keyInfoHolder2;
		TextLine keyInfoHolder3;
		

		TextLine heroName;

		AtributeTextLine helmet;
		AtributeTextLine armor;
		AtributeTextLine panties;
		AtributeTextLine boots;
		AtributeTextLine gun;

		AtributeTextLine defense;
		AtributeTextLine atack;
		public InventoryWindow(Hero hero) : base(1, 1, 100, 27, '~')
		{
			items = new TextLine[hero.Inventory.Items.Length];
			generateItemsLines();
			keyInfoHolder1 = new TextLine(102, 2, 16, "[Esc] close", TextAligment.Left);
			keyInfoHolder2 = new TextLine(102, 3, 16, "", TextAligment.Left);
			keyInfoHolder2 = new TextLine(102, 4, 16, "", TextAligment.Left);
			keyInfoHolder3 = new TextLine(102, 5, 16, "", TextAligment.Left);

			heroName = new TextLine(53, 3, 20, hero.Name, TextAligment.Left); 

			//todo fill from hero
			helmet = new AtributeTextLine(53, 5, 20, atribute:"Helmet: ", data:"Empty");
			armor = new AtributeTextLine(53, 6, 20, atribute:"Armor: ", data: "Empty");
			panties = new AtributeTextLine(53, 7, 20, atribute:"Panties: ", data: "Empty");
			boots = new AtributeTextLine(53, 8, 20, atribute:"Boots: ", data: "Empty");
			gun = new AtributeTextLine(53, 10, 20, atribute:"Gun: ", data: "Empty");

			defense = new AtributeTextLine(53, 12, 20, atribute:"Defense: ", data: hero.Def.ToString());
			atack = new AtributeTextLine(53, 13, 20, atribute:"Atack: ", data: hero.Damage.ToString());

		}

		public void SetAtack(int atack)
		{
			this.atack.Data = atack.ToString();
		}
		public void SetDefense(int defense)
		{
			this.defense.Data = defense.ToString();
		}
		public void SetHelmet(string data)
		{
			this.helmet.Data = data;
		}
		public void SetArmor(string data)
		{
			this.armor.Data = data;
		}
		public void SetPanties(string data)
		{
			this.panties.Data = data;
		}
		public void SetBoots(string data)
		{
			this.boots.Data = data;
		}
		public void SetGun(string data)
		{
			this.gun.Data = data;
		}

		public void SetKeyInfo2(string data)
		{
			keyInfoHolder2.Data = data;
		}
		public void SetKeyInfo3(string data)
		{
			keyInfoHolder3.Data = data;
		}

		private void generateItemsLines()
		{
			for (int i = 0; i < items.Length; i++)
			{		
				items[i] = new TextLine(3, 3+i, 20, "Empty", TextAligment.Left);
			}
		}

		public override void Render()
		{
			base.Render();
			foreach (TextLine txt in items)
			{
				txt.Render();
			}
			keyInfoHolder1.Render();
			keyInfoHolder2.Render();
			keyInfoHolder3.Render();

			heroName.Render();
			helmet.Render();
			armor.Render();
			panties.Render();
			boots.Render();
			gun.Render();

			defense.Render();
			atack.Render();
		}

		public void RenderItem(int pos, string data) 
		{
			items[pos].Data = data;
		}

		internal void SetActive(int activeId, bool isactive)
		{
			items[activeId].IsActive = isactive;
		}
	}
}
