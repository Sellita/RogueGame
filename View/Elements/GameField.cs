using RogueGame.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace RogueGame.View.Elements
{
	class GameField : Window
	{
		public GameField(int x, int y, int width, int height) : base(x, y, width, height, '~')
		{

		}

		public void RenderRoad(Road road)
		{
			LinkedListNode<RoadBlock> roadBloc = road.RoadBlocks.First;
			while(roadBloc != null)
			{
				if (roadBloc.Value.IsExplored)
				{
					if (!roadBloc.Value.IsExistObject())
					{
						RenderObject(roadBloc.Value);
					}
					else
					{
						RenderObject(roadBloc.Value.getLastObject());
					}
					
				}
				roadBloc = roadBloc.Next;
			}
		}

		public void RenderRoom(Room room)
		{
			for (int i = 0; i < room.RoomObjects.Length; i++)
			{
				for (int j = 0; j < room.RoomObjects[i].Length; j++)
				{
					if (room.RoomObjects[i][j].Count != 0)
					{
						RenderObject(room.RoomObjects[i][j][room.RoomObjects[i][j].Count-1]);
					}
					else
					{
						if (room.IsActive)
						{
							RenderCoord(room.X+i, room.Y+j, '.');
						}
						else
						{
							RenderCoord(room.X+i, room.Y + j, ' ');
						}
					}
				}
			}


			//foreach (GameObject[] column in room.RoomObjects)
			//{
			//	foreach (GameObject obj in column)
			//	{
			//		if (obj != null)
			//		{
			//			RenderObject(obj);
			//		}
			//		else
			//		{
			//
			//		}
			//	}
			//}
		}

		public void RenderObject(GameObject gameObject)
		{
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;
			ConsoleColor fgColor = Console.ForegroundColor;

			if (gameObject.renderFgColor != ConsoleColor.Black)//TODO: change whend added background
			{
				Console.ForegroundColor = gameObject.renderFgColor;
			}
			Console.SetCursorPosition(X + gameObject.x, Y + gameObject.y);
			

			Console.Write(gameObject.renderChar);

			Console.SetCursorPosition(lx, ly);
			Console.ForegroundColor = fgColor;
		}

		public void RenderCoord(int x, int y, char c)
		{
			int lx = Console.CursorLeft;
			int ly = Console.CursorTop;

			Console.SetCursorPosition(X+x, Y+y);

			Console.Write(c);

			Console.SetCursorPosition(lx, ly);
		}
		public override void Render()
		{
			base.Render();
		}
	}
}

