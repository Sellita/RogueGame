using System;
using System.Collections.Generic;
using System.Text;
using RogueGame.View.Elements;

namespace RogueGame.View.Windows
{
	sealed class CreditWindow : Window
	{
		private Button backButton;
		private TextBlock creditTextBlock;
		
		public CreditWindow() : base (x: Console.WindowWidth/2-30, y: 10, width: 60, heigth: 18, borderChar: '@')
		{
			int titleboxWidth = 30;
			List<string> title = new List<string>();
			title.Add("Game design:");
			title.Add("Vardas Vardaitis");
			title.Add("");
			title.Add("Programuotojas:");
			title.Add("Vardas Vardaitis");
			title.Add("");
			title.Add("Art:");
			title.Add("Vardas Vardaitis");
			title.Add("");
			title.Add("Marketingas:");
			title.Add("Vardas Vardaitis");


			int buttonWidth = 16;
			int buttonHeigth = 3;
			creditTextBlock = new TextBlock(Console.WindowWidth / 2 - titleboxWidth / 2, Y + 2, titleboxWidth, title);
			backButton = new Button(Console.WindowWidth / 2 - (buttonWidth / 2), Y + 2 + title.Count + 1, buttonWidth, buttonHeigth, "Back");
			
			backButton.SetActive();

		}

		public override void Render()
		{
			base.Render();			//title
			
			creditTextBlock.Render();

			//buttons

			backButton.Render();
			

		}
	}
}
