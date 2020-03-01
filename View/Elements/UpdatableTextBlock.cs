using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class UpdatableTextBlock : GuiObject
	{
		TextBlock items;
		public UpdatableTextBlock(int x, int y, int width, int height) : base(x, y, width, height)
		{
			List<string> texts = new List<string>();
			for (int i = 0; i < height; i++)
			{
				texts.Add("");
			}
			items = new TextBlock(x, y, width, texts);
		}

		public void Write(string data)
		{
			for (int i = 1; i < items.TextBlocks.Count; i++)
			{
				items.TextBlocks[i - 1].Data = items.TextBlocks[i].Data;
			}
			items.TextBlocks[items.TextBlocks.Count-1].Data = data;
		}

		public override void Render()
		{
			ClearObjectSpace();
		}
	}
}
