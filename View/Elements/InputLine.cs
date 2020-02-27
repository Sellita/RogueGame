using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class InputLine : GuiObject
	{
		public InputLine(int x, int y, int width) : base(x, y, width, 1)
		{

		}

		public override void Render()
		{
			ClearObjectSpace();
		}

		public string ReadValue()
		{
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;

			Console.CursorVisible = true;
			Console.SetCursorPosition(X, Y);
			string readedLine = Console.ReadLine();

			Console.SetCursorPosition(lx, ly);
			Console.CursorVisible = false;

			return readedLine;
		}
	}
}
