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
		AtributeTextLine gold;

		AtributeTextLine dungLvl;


		TextLine infoHolder;
		TextLine errorHolder;
		TextLine keyInfoHolder1;
		TextLine keyInfoHolder2;
		TextLine keyInfoHolder3;
		UpdatableTextBlock LastEvents;
		public GameWindow() : base(0, 0, Console.WindowWidth, Console.WindowHeight, ' ')
		{
			gameField = new GameField(1, 1, 100, 27);
			health = new AtributeTextLine(3, 28, 11, "20", "Health:");
			defense = new AtributeTextLine(15, 28, 11, "0", "Defense:");
			speed = new AtributeTextLine(27, 28, 11, "15", "Speed:");
			atack = new AtributeTextLine(40, 28, 11, "3", "Atack:");
			gold = new AtributeTextLine(53, 28, 11, "0", "Gold:");
			dungLvl = new AtributeTextLine(102, 28, 15, "0", "DungeonLvl: ");

			infoHolder = new TextLine(1, 0, 100, "", TextAligment.Left);
			errorHolder = new TextLine(102, 1, 15, "", TextAligment.Left);
			keyInfoHolder1 = new TextLine(102, 2, 15, "[I] inventory", TextAligment.Left);
			keyInfoHolder2 = new TextLine(102, 3, 15, "[esc] Menu", TextAligment.Left);
			keyInfoHolder3 = new TextLine(102, 4, 15, "", TextAligment.Left);
			LastEvents = new UpdatableTextBlock(102, 21, 15, 5);
		}
		
		public void WriteEvent(string data)
		{
			LastEvents.Write(data);
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
		public void SetGold(int gold)
		{
			this.gold.Data = gold.ToString();
		}
		public void SetDungeonLvl(int lvl)
		{
			this.dungLvl.Data = lvl.ToString();
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
			gold.Render();
			dungLvl.Render();

			infoHolder.Render();
			keyInfoHolder1.Render();
			keyInfoHolder2.Render();
			keyInfoHolder3.Render();

			errorHolder.Render();
		}

		internal void RenderRoad(Road road)
		{
			gameField.RenderRoad(road);
		}

		internal void SetkeyInfoHolder3(string msg)
		{
			keyInfoHolder3.Data = msg;
		}

		internal void SetInfoHolder(string textToWrite)
		{
			infoHolder.Data = textToWrite;
		}
		internal void SetErorHolder(string textToWrite)
		{
			errorHolder.Data = textToWrite;
		}

		internal void renderHero(Hero hero)
		{
			health.Data = hero.Health.ToString();
			defense.Data = hero.Def.ToString();
			speed.Data = hero.Speed.ToString();
			atack.Data = hero.Damage.ToString();
			gold.Data = hero.gold.ToString();
		}
	}
}
