using System;
using System.Collections.Generic;
using System.Text;
using RogueGame.View.Windows;
using RogueGame.View.Elements;
using RogueGame.Data;

namespace RogueGame.Control
{
	class GameControler
	{
		GameWindow gameWindow;
		Hero hero;

		public GameControler()
		{
			gameWindow = new GameWindow();
		}

		public void StartGameLoop()
		{
			//request hero name
			int inputBlockWidth = 30;
			InputBlock input = new InputBlock(Console.WindowWidth / 2 - inputBlockWidth / 2, Console.WindowHeight / 2, inputBlockWidth, "Enter your name: ");
			input.Render();
			string heroName = input.GetText();
			//create hero
			hero = new Hero(heroName);
			GameLoop();
		}
		public void GameLoop()
		{
			//game moves only when hero moves
			hero.SetStartPosition(1, 1);
			gameWindow.Render();
			gameWindow.DrawInGameField(hero);
			Room room = new Room(2, 2, 20, 6);
			room.isActive = false;
			gameWindow.RenderRoom(room);
			Console.ReadKey();
			room.isActive = true;
			gameWindow.RenderRoom(room);
		}

		public void RenderRoom(Room room)
		{
			gameWindow.RenderRoom(room); 
		}
	}
}
