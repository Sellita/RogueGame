using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class Button : GuiObject
	{
		private Frame activeFrame;
		private bool isActive;
		private Frame notActiveFrame;
		private TextLine textLine;

		public bool IsActive { get => isActive; set => isActive = value; }
		internal TextLine TextLine { get => textLine; set => textLine = value; }

		public Button(int x, int y, int width, int heigth, string text) : base(x, y, width, heigth)
		{
			TextLine = new TextLine(x+1, y+heigth/2, width-2, text);
			activeFrame = new Frame(x, y, width, heigth, '#');
			notActiveFrame = new Frame(x, y, width, heigth, '+');
			IsActive = false;
			
		}
		public void SetActive()
		{
			IsActive = true;
			activeFrame.Render();			
		}
		public void SetNotActive()
		{
			IsActive = false;
			notActiveFrame.Render();
		}
		public override void Render()
		{
			ClearObjectSpace();
			if (!isActive)
			{
				notActiveFrame.Render();
			}
			else
			{
				activeFrame.Render();
			}
			TextLine.Render();
		}
	}
}
