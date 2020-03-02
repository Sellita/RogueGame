using RogueGame.View.Windows;
using System;
using System.Collections.Generic;
using System.Text;


namespace RogueGame.Control.Menu
{
	class MainMenuControler
	{
		enum mainMenuButons
		{
			Start,
			Credits,
			Quit
		}

		private CreditWindow creditWindow;

		private MainMenuWindow gameWindow;

		public MainMenuControler()
		{
			gameWindow = new MainMenuWindow(mainMenuButons.Start.ToString(), mainMenuButons.Credits.ToString(), mainMenuButons.Quit.ToString());
			creditWindow = new CreditWindow();
		}

		public void ShowMenu()
		{
			gameWindow.Render();
			MenuLoop();
		}

		private void MenuLoop()
		{
			bool needToRender = true;
			do
			{

				ConsoleKeyInfo pressedChar = Console.ReadKey(true);
				switch (pressedChar.Key)
				{
					case ConsoleKey.LeftArrow:
						gameWindow.setPreviousButtonActive();
						break;
					case ConsoleKey.RightArrow:
						gameWindow.setNextButtonActive();
						break;
					case ConsoleKey.Enter:
						//todo:
						switch (gameWindow.GetActiveButton().TextLine.Data)
						{
							case "Start":
								//start Game
								StartGame();
								gameWindow.Render();
								break;
							case "Credits":
								CreditWindowControl();								
								break;
							case "Quit":
								Environment.Exit(0);
								return;
								
						}
						break;
					case ConsoleKey.Escape:
						Environment.Exit(0);
						break;
				}

				
			} while (needToRender);
		}

		private void StartGame()
		{
			GameControler gc = new GameControler();
			gc.StartGameLoop();
		}

		private void CreditWindowControl()
		{
			creditWindow.Render();
			do
			{

				ConsoleKeyInfo pressedChar = Console.ReadKey(true);
				switch (pressedChar.Key)
				{
					case ConsoleKey.Escape:
					case ConsoleKey.Enter:						
						//gameWindow = new GameWindow();
						gameWindow.Render();
						return;

						
				}


			} while (true);
		}

		

	}
}
