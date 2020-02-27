using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.View.Elements
{
	class AtributeTextLine : GuiObject
	{
		private string data;
		private readonly string atribute;
		private TextLine atributeText;
		private TextLine dataText;

		public string Data { get => data;
			set {

				data = value;
				dataText.Data = data;

			} }

		public AtributeTextLine(int x, int y, int width, string data, string atribute) : base(x, y, width, 1)
		{
			this.atribute = atribute;
			this.data = data;
			if (atribute.Length > width)
			{
				// todo: throw exception
				this.atribute = atribute.Substring(0, width);
				this.data = "";
			}
			else
			{
				if(data.Length > width - atribute.Length)
				{
					this.data = data.Substring(0, width - atribute.Length);
				}				
			}
			atributeText = new TextLine(x,y, this.atribute.Length, this.atribute, TextAligment.Left);
			if(data.Length > 0)
			{
				dataText = new TextLine(x + this.atribute.Length, y, width - atribute.Length, this.data, TextAligment.Left);
			}
		}

		

		public override void Render()
		{
			atributeText.Render();
			dataText?.Render();
			
		}
	}
}
