using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class TextLine : GuiObject
	{
		private string data;
		private TextAligment aligment;
		
		

		public TextLine(int x, int y, int width, string data, TextAligment aligment = TextAligment.Center) : base(x, y, width, 1)
		{
			if (data.Length > width)
			{
				this.data = data.Substring(0, width);
			}
			else
			{
				this.data = data;
			}
			this.aligment = aligment;
		}

		public string Data { get => data; set {
				ClearCurrentText();
				data = value;
				Render();
			} }

		private void ClearCurrentText()
		{
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;

			if (aligment == TextAligment.Center)
			{
				Console.SetCursorPosition(X + Width / 2 - Data.Length / 2 - Data.Length % 2, Y);
			}
			else if(aligment == TextAligment.Left)
			{
				Console.SetCursorPosition(X, Y);
			}
			else if (aligment == TextAligment.Right)
			{
				Console.SetCursorPosition(X + Width - data.Length, Y);
			}
			string empty = "";
			for(int i =0; i < data.Length; i++)
			{
				empty += " ";
			}
			Console.Write(empty);

			Console.SetCursorPosition(lx, ly);
		}

		public override void Render()
		{
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;

			if (aligment == TextAligment.Center)
			{
				Console.SetCursorPosition(X + Width / 2 - Data.Length / 2 - Data.Length % 2, Y);
			}
			else if(aligment == TextAligment.Left)
			{
				Console.SetCursorPosition(X, Y);
			}
			else if(aligment == TextAligment.Right)
			{
				Console.SetCursorPosition(X + Width - data.Length, Y);
			}
			Console.Write(Data);

			Console.SetCursorPosition(lx, ly);
		}
	}
}
