using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{

	class InputBlock : GuiObject
	{
		private Frame border;
		private TextLine label;
		private InputLine input;

		public InputBlock(int x, int y, int width, string label) : base(x, y, width, 4)
		{
			border = new Frame(x, y, width, 4, '=');
			this.label = new TextLine(x + 1, y + 1, width - 2, label, TextAligment.Left);
			input = new InputLine(x+1, y+2, width-2);
		}

		~InputBlock()
		{
			ClearObjectSpace();
		}

		public override void Render()
		{
			border.Render();
			label.Render();
			input.Render();
		}

		public string GetText()
		{
			return input.ReadValue();
		}
	}
}
