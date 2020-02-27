using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class Window : GuiObject
	{
		private Frame border;

		public Window(int x, int y, int width, int heigth, char borderChar) : base(x, y, width, heigth)
		{
			border = new Frame(x, y, width, heigth, borderChar);
			//Render();
		}

		public override void Render()
		{
			ClearObjectSpace();

			border.Render();
			
		}



	}
}
