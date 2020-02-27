using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class Frame : GuiObject
	{
		private char renderChar;
		
		public Frame(int x, int y, int width, int height, char renderChar) : base(x, y, width, height)
		{
			this.RenderChar = renderChar;
			
		}

		public char RenderChar { get => renderChar; set => renderChar = value; }

		public override void Render()
		{
			//save current cursor position 
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;
			Console.SetCursorPosition(X, Y);
			//up
			for(int i = 0; i < Width; i++)
			{
				Console.Write(renderChar);
			}
			//left and right
			for (int i = 1; i < Height-1; i++)
			{
				Console.SetCursorPosition(X, Y + i);
				Console.Write(renderChar);
				Console.SetCursorPosition(X + Width - 1, Y + i);
				Console.Write(renderChar);
			}
			//down
			Console.SetCursorPosition(X, Y+Height-1);
			for (int i = 0; i < Width; i++)
			{
				Console.Write(renderChar);
			}

			//set cursor position as it be at start
			Console.SetCursorPosition(lx, ly);

			
		}

	}
}
