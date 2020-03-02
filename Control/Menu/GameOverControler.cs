using RogueGame.Utilites;
using RogueGame.View.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace RogueGame.Control.Menu
{
	class GameOverControler
	{
		enum gameOverMenuButons
		{
			Replay,
			[Description("Main menu")]
			MainMenu,
			Quit
		}

		private CreditWindow creditWindow;

		private GameOverWindow gameWindow;

		public GameOverControler(int lvl)
		{
			gameWindow = new GameOverWindow(gameOverMenuButons.Replay.ToString(), gameOverMenuButons.MainMenu.GetDescription(), gameOverMenuButons.Quit.ToString(), lvl);
			creditWindow = new CreditWindow();
		}

		public ActionsForMenu ShowMenu()
		{
			gameWindow.Render();
			return MenuLoop();
		}

		private ActionsForMenu MenuLoop()
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
							case "Replay":
								return ActionsForMenu.Restart;
							
							case "Main menu":
								return ActionsForMenu.MainMenu;
								
							case "Quit":
								Environment.Exit(0);
								return ActionsForMenu.Quit;
								

						}
						break;
					case ConsoleKey.Escape:
						return ActionsForMenu.Restart;
				
				}


			} while (needToRender);
			return ActionsForMenu.Restart;
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
