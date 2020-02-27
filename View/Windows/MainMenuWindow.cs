using System;
using System.Collections.Generic;
using System.Text;
using RogueGame.View.Elements;

namespace RogueGame.View.Windows
{
	sealed class MainMenuWindow : Window
	{
		//private Button creditsButton;
		//private Button quitButton;
		//private Button startButton;
		private List<Button> menuButtons = new List<Button>();
		private TextBlock titleTextBlock;
		

		public MainMenuWindow(string firstButton, string secondButton, string thirdButton) : base(x: 0, y: 0, width: Console.WindowWidth, heigth: Console.WindowHeight, borderChar: '%')
		{
			int titleboxWidth = 40;
			List<string> title = new List<string>();
			title.Add("Rogue game");
			title.Add("Vardas pavardaitis kuryba");
			title.Add("Made in Vilnius Coding school!");
			titleTextBlock = new TextBlock(Console.WindowWidth / 2 - titleboxWidth / 2, 5, titleboxWidth, title);

			int buttonWidth = 20;
			int buttonHeigth = 5;
			//buttons
			Button startButton = new Button(Console.WindowWidth / 2 - (buttonWidth * 2), 15, buttonWidth, buttonHeigth, firstButton);

			//startButton.Render();
			startButton.SetActive();
			
			//creditsButton.Render();
			
			//quitButton.Render();
			menuButtons.Add(startButton);
			menuButtons.Add(new Button(Console.WindowWidth / 2 - (buttonWidth / 2), 15, buttonWidth, buttonHeigth, secondButton));
			menuButtons.Add(new Button(Console.WindowWidth / 2 + (buttonWidth), 15, buttonWidth, buttonHeigth, thirdButton));
		}

		public override void Render()
		{
			base.Render();
			//title

			titleTextBlock.Render();
			foreach(Button btn in menuButtons)
			{
				btn.Render();
			}

			
		}

		public Button GetActiveButton()
		{
			foreach(Button btn in menuButtons)
			{
				if (btn.IsActive)
				{
					return btn;
				}
			}
			
				return null;
			
		}

		public void setNextButtonActive()
		{
			for(int i=0; i < menuButtons.Count; i++)
			{
				if (menuButtons[i].IsActive)
				{
					menuButtons[i].SetNotActive();
					if (i != menuButtons.Count - 1)
					{
						menuButtons[i + 1].SetActive();
					}
					else
					{
						menuButtons[0].SetActive();
					}
					return;
				}
			}
		}
		public void setPreviousButtonActive()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				if (menuButtons[i].IsActive)
				{
					menuButtons[i].SetNotActive();
					if (i != 0)
					{
						menuButtons[i - 1].SetActive();
					}
					else
					{
						menuButtons[menuButtons.Count-1].SetActive();
					}
					return;
				}
			}
		}
	}
}
