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

		public void RenderRoom(Room room)
		{
			for (int i = 0; i < room.RoomObjects.Length; i++)
			{
				for (int j = 0; j < room.RoomObjects[i].Length; j++)
				{
					if (room.RoomObjects[i][j] != null)
					{
						RenderObject(room.RoomObjects[i][j]);
					}
					else
					{
						if (room.isActive)
						{
							RenderCoord(room.RoomObjects[i][0].x, room.RoomObjects[i][0].y+j, '.');
						}
						else
						{
							RenderCoord(room.RoomObjects[i][0].x, room.RoomObjects[i][0].y + j, ' ');
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

			Console.SetCursorPosition(X + gameObject.x, Y + gameObject.y);

			Console.Write(gameObject.renderChar);

			Console.SetCursorPosition(lx, ly);
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

