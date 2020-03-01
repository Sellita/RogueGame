using System;
using System.IO;
using RogueGame.View.Windows;
using RogueGame.Data;
using RogueGame.Control;

namespace RogueGame
{
	class Program
	{
		static void Main(string[] args)
		{
			//*
			Console.CursorVisible = false;
			Console.OutputEncoding = System.Text.Encoding.Unicode;

			GameControler gc = new GameControler();
			gc.StartGameLoop();
			Console.ReadKey();
			//*/

			//GameWindow game = new GameWindow();
			//game.Render();
			//char RoadChar = '=';
			//game.DrawInGameField(1, 1, Hero.HeroChar);
			//game.DrawInGameField(1, 2, Enemy.EnemyDefaultChar);
			//game.DrawInGameField(2, 2, RoadChar);
			//game.DrawInGameField(3, 2, '@');
			//game.DrawInGameField(4, 2, RoadChar);
			//game.DrawInGameField(98, 25,'K');
			//
			//Console.ReadKey();
			//game.DrawInGameField(1, 1, ' ');
			//game.SetHealth(99);
			//Console.ReadKey();
			//game.SetHealth(9);
			//Console.ReadKey();
			//game.SetHealth(99);


			/*
				using (FileStream map = File.OpenRead("Map.txt"))
				{
					string s = "";
				for (int i = 0; i < 30; i++)
				{
					for (int j = i*101; j <= i*101+101; j++)
					{
						map.Position = j;
						byte[] b = new byte[1];
						map.Read(b, 0, (int)(1));
						s = System.Text.Encoding.UTF8.GetString(b);
						Console.Write(s);
					}
					Console.WriteLine();
				}
				}	*/
			
			
					/*while((s = map.ReadLine()) != null)
					{
						Console.WriteLine(s);
					}*/
				
		}
	}
}
