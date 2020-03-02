using RogueGame.Data;
using RogueGame.View.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Windows
{
	class StatsWindow : Window
	{
		public static readonly string emptyTxt = "Empty";
		private AtributeTextLine[] items;
		TextLine keyInfoHolder1;
		TextLine keyInfoHolder2;
		TextLine keyInfoHolder3;


		TextLine heroName;

		AtributeTextLine exp;
		AtributeTextLine Level;
		AtributeTextLine points;

		public StatsWindow(Hero hero) : base(1, 1, 100, 27, '~')
		{
			items = new AtributeTextLine[4];
			generateItemsLines();
			keyInfoHolder1 = new TextLine(102, 2, 16, "[Esc] close", TextAligment.Left);
			keyInfoHolder2 = new TextLine(102, 3, 16, "", TextAligment.Left);
			keyInfoHolder2 = new TextLine(102, 4, 16, "", TextAligment.Left);
			keyInfoHolder3 = new TextLine(102, 5, 16, "", TextAligment.Left);

			heroName = new TextLine(53, 3, 20, hero.Name, TextAligment.Left);

			//todo fill from hero
			exp = new AtributeTextLine(53, 5, 20, atribute: "Exp: ", data: "0");
			Level = new AtributeTextLine(53, 6, 20, atribute: "Level: ", data: "0");
			points = new AtributeTextLine(53, 7, 20, atribute: "Points: ", data: "Empty");


		}


		public void SetExp(string data)
		{
			this.exp.Data = data;
		}

		public void SetPoints(string data)
		{
			this.points.Data = data;
		}	
		public void SetLevel(string data)
		{
			this.Level.Data = data;
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
			
				items[0] = new AtributeTextLine(3, 3, 20, "0", "Atack");
				items[1] = new AtributeTextLine(3, 4, 20, "0", "Defense");
				items[2] = new AtributeTextLine(3, 5, 20, "0", "Speed");
				items[3] = new AtributeTextLine(3, 6, 20, "0", "Hp");
			
		}

		public override void Render()
		{
			base.Render();
			foreach (AtributeTextLine txt in items)
			{
				txt.Render();
			}
			keyInfoHolder1.Render();
			keyInfoHolder2.Render();
			keyInfoHolder3.Render();

			heroName.Render();
			exp.Render();
			Level.Render();
			points.Render();

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
