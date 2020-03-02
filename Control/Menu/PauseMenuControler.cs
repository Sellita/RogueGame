using RogueGame.Utilites;
using RogueGame.View.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace RogueGame.Control.Menu
{
	class PauseMenuControler
	{
		enum gameOverMenuButons
		{
			Continue,
			[Description("Main menu")]
			MainMenu,
			Quit
		}

		private CreditWindow creditWindow;

		private PauseWindow gameWindow;

		public PauseMenuControler()
		{
			gameWindow = new PauseWindow(gameOverMenuButons.Continue.ToString(), gameOverMenuButons.MainMenu.GetDescription(), gameOverMenuButons.Quit.ToString());
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
							case "Continue":
								return ActionsForMenu.Continue;

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
