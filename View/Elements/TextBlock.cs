using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class TextBlock : GuiObject
	{
		private List<TextLine> textBlocks = new List<TextLine>();

		public TextBlock(int x, int y, int width, List<string> lines) : base(x, y, width, lines.Count)
		{
			for(int i=0;i<lines.Count;i++)
			{
				textBlocks.Add(new TextLine(x, y+i, width, lines[i]));
			}
		}

		internal List<TextLine> TextBlocks { get => textBlocks; set => textBlocks = value; }

		public override void Render()
		{
			ClearObjectSpace();
			foreach(TextLine line in textBlocks)
			{
				line.Render();
			}
		}
	}
}
