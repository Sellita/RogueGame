using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	abstract class GuiObject
	{
		private int x;
		private int y;
		private int width;
		private int height;

		public GuiObject(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;	
			this.Width = width;
			this.Height = height;
		}

		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int Width { get => width; set => width = value; }
		public int Height { get => height; set => height = value; }

		public virtual void ClearObjectSpace()
		{
			//clear window
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;
			Console.SetCursorPosition(X, Y);
			//up
			string clearString = "";
			for (int i = 0; i < Width; i++)
			{
				clearString += " ";
			}
			//left and right
			for (int i = 0; i < Height; i++)
			{
				Console.SetCursorPosition(X, Y + i);
				Console.Write(clearString);

			}
			//set cursor position as it be at start
			Console.SetCursorPosition(lx, ly);
		}

		public abstract void Render();
		

		
	}
}
