using RogueGame.Data;
using RogueGame.View.Elements;
using System;

namespace RogueGame.View.Windows
{
	class GameWindow : Window
	{
		GameField gameField;
		AtributeTextLine health;
		AtributeTextLine defense;
		AtributeTextLine speed;
		AtributeTextLine atack;
		public GameWindow() : base(0, 0, Console.WindowWidth, Console.WindowHeight, ' ')
		{
			gameField = new GameField(1, 1, 100, 27);
			health = new AtributeTextLine(3, 28, 10, "20", "Health:");
			defense = new AtributeTextLine(13, 28, 10, "0", "Defense:");
			speed = new AtributeTextLine(23, 28, 10, "15", "Speed:");
			atack = new AtributeTextLine(33, 28, 10, "3", "Atack:");
		}

		public void SetHealth(int hp)
		{
			health.Data = hp.ToString();
		}
		public void SetDefense(int def)
		{
			defense.Data = def.ToString();
		}
		public void SetSpeed(int speed)
		{
			this.speed.Data = speed.ToString();
		}
		public void SetAtack(int atack)
		{
			this.atack.Data = atack.ToString();
		}

		public void RenderRoom(Room room)
		{

			gameField.RenderRoom(room);



		}

		public void DrawInGameField(int x, int y, char c)
		{
			gameField.RenderCoord(x, y, c);
		}
		public void DrawInGameField(GameObject obj)
		{
			gameField.RenderCoord(obj.x, obj.y, obj.renderChar);
		}

		public override void Render()
		{
			base.Render();
			gameField.Render();
			health.Render();
			defense.Render();
			speed.Render();
			atack.Render();
		}

		internal void RenderRoad(Road road)
		{
			gameField.RenderRoad(road);
		}
	}
}
