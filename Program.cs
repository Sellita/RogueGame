using System;
using System.IO;
using RogueGame.View.Windows;
using RogueGame.Data;
using RogueGame.Control.Menu;

namespace RogueGame
{
	class Program
	{
		static void Main(string[] args)
		{
			
			Console.CursorVisible = false;
			Console.OutputEncoding = System.Text.Encoding.Unicode;

			MainMenuControler mainMenuControler = new MainMenuControler();
			mainMenuControler.ShowMenu();

			//GameControler gc = new GameControler();
			//gc.StartGameLoop();



		}
	}
}
