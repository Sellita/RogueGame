using System;
using System.Collections.Generic;
using System.Text;
using RogueGame.View.Windows;
using RogueGame.View.Elements;
using RogueGame.Data;
using System.IO;

namespace RogueGame.Control
{
	class GameControler
	{
		GameWindow gameWindow;
		Hero hero;
		Level lvl;
		const string txtRoadBlock = "=";
		Room activeRoom;
		Road activeRoad;
		bool isGameStarted = false;
		const int exploringBlocksOnRoad = 3;
		Spawner spawner;

		public GameControler()
		{
			gameWindow = new GameWindow();
			lvl = new Level();
			GetLevelFromFile("Map.txt");//TODO: change to generation!!!!!!!!!!!
			AddRoadsToRooms();
			//spawner.SpawnEnemy(lvl.Rooms);
		}

		private void AddRoadsToRooms()
		{
			foreach(Road road in lvl.Roads)
			{
				RoadBlock start = road.GetFirstRoadBlock();
				RoadBlock finish = road.GetLastRoadBlock();

				Room startRoom = lvl.GetRommWithCoord(start.x, start.y);
				Room finishRoom = lvl.GetRommWithCoord(finish.x, finish.y);

				startRoom.AddReplaceObject(start);
				finishRoom.AddReplaceObject(finish);

			}			
		}

		private void GetLevelFromFile(string file)//only for first tests
		{
			using (FileStream map = File.OpenRead(file))
			{
				string s = "";
				List<int> computedPositions = new List<int>();


				map.Position = 105;
				//byte[] x = new byte[1];
				//map.Read(x, 0, (int)(1));

				//s = System.Text.Encoding.UTF8.GetString(x);
				//Console.WriteLine(	s);
				//101 space between lines

				List<int> roadEndings = new List<int>();
				while (map.Length != map.Position)
				{
					
					byte[] b = new byte[1];
					map.Read(b, 0, (int)(1));
					s = System.Text.Encoding.UTF8.GetString(b);
					if (s == Char.ToString(WallBlock.WallChar))
					{
						//tikriname ar tai pirma karta surastas symbolis
						if (!computedPositions.Contains((int)map.Position - 1))
						{
							//naujas room
							string readedChar = Char.ToString(WallBlock.WallChar);
							int FirstCorner = (int)map.Position - 1;
							//computedPositions.Add(FirstCorner);
							while(readedChar == Char.ToString(WallBlock.WallChar) || readedChar == txtRoadBlock)
							{
								computedPositions.Add((int)map.Position - 1);
								byte[] bt = new byte[1];
								map.Read(bt, 0, (int)(1));
								readedChar = System.Text.Encoding.UTF8.GetString(bt);
								if (readedChar == txtRoadBlock)
								{
									//pridedam apdirbimui
									roadEndings.Add(((int)map.Position - 1));
								}
							}
							int lastCorner = (int)map.Position - 2;

							int nextLineStart = FirstCorner;
							int nextLineStop = lastCorner;
							bool isLastLine = false;

							while (!isLastLine)
							{
								isLastLine = true;
								nextLineStart += 101;
								nextLineStop += 101;
								map.Position = nextLineStart;
								for (int i = nextLineStart; i < nextLineStop + 1; i++)
								{
									byte[] bt = new byte[1];
									map.Read(bt, 0, (int)(1));
									computedPositions.Add((int)map.Position - 1);
									readedChar = System.Text.Encoding.UTF8.GetString(bt);
									if ((readedChar != Char.ToString(WallBlock.WallChar) && readedChar != txtRoadBlock))
									{
										isLastLine = false;
										computedPositions.Add((int)map.Position - 1);
									}
									if (readedChar == txtRoadBlock)
									{
										//pridedam apdirbimui
										roadEndings.Add(((int)map.Position - 1));
									}
								}
								
							}

							
							lvl.AddRoom(new Room(FirstCorner%101, FirstCorner/101, lastCorner-FirstCorner+1, (nextLineStart-FirstCorner)/100 + 1));
							//po apdirbimo 
							map.Position = lastCorner+2;
						}
						else
						{
							//praleisti =)
						}
					}
				}
				//Roads
				while (roadEndings.Count>0)
				{
					bool isEnd = false;
					Directions lastDirection = Directions.Down;
					Road road = new Road();
					road.AddBlock(new RoadBlock(roadEndings[0]%101, roadEndings[0]/101));
					if(GetSymbolFromText(map, roadEndings[0]+1) == txtRoadBlock)//right
					{
						lastDirection = Directions.Right;
						RoadBlock last = road.GetLastRoadBlock();
						road.AddBlock(new RoadBlock(last.x+1, last.y));
						
					}else if (GetSymbolFromText(map, roadEndings[0] - 1) == txtRoadBlock)//left
					{
						lastDirection = Directions.Left;
						RoadBlock last = road.GetLastRoadBlock();
						road.AddBlock(new RoadBlock(last.x-1, last.y));
						
					}
					else if (GetSymbolFromText(map, roadEndings[0] - 101) == txtRoadBlock)//up
					{
						lastDirection = Directions.Up;
						RoadBlock last = road.GetLastRoadBlock();
						road.AddBlock(new RoadBlock(last.x, last.y-1));
						
					}
					else if (GetSymbolFromText(map, roadEndings[0] + 101) == txtRoadBlock)//down
					{
						lastDirection = Directions.Down;
						RoadBlock last = road.GetLastRoadBlock();
						road.AddBlock(new RoadBlock(last.x, last.y+1));
						
					}


					while (!isEnd)
					{
						RoadBlock lastBlock = road.GetLastRoadBlock();
						switch (lastDirection)
						{
							case Directions.Left:
								if (GetSymbolFromText(map, lastBlock.y*101 + lastBlock.x - 1) == txtRoadBlock)//left
								{
									lastDirection = Directions.Left;									
									road.AddBlock(new RoadBlock(lastBlock.x - 1, lastBlock.y));

								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x - 101) == txtRoadBlock)//up
								{
									lastDirection = Directions.Up;									
									road.AddBlock(new RoadBlock(lastBlock.x, lastBlock.y -1));

								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x + 101) == txtRoadBlock)//down
								{
									lastDirection = Directions.Down;									
									road.AddBlock(new RoadBlock(lastBlock.x, lastBlock.y + 1));
								}
								else
								{
									isEnd = true;
								}
								break;
							case Directions.Right:
								if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x + 1) == txtRoadBlock)//rigth
								{
									lastDirection = Directions.Right;
									road.AddBlock(new RoadBlock(lastBlock.x + 1, lastBlock.y));

								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x - 101) == txtRoadBlock)//up
								{
									lastDirection = Directions.Up;
									road.AddBlock(new RoadBlock(lastBlock.x, lastBlock.y - 1));

								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x + 101) == txtRoadBlock)//down
								{
									lastDirection = Directions.Down;
									road.AddBlock(new RoadBlock(lastBlock.x, lastBlock.y + 1));
								}
								else
								{
									isEnd = true;
								}
								break;
							case Directions.Up:
								if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x - 1) == txtRoadBlock)//left
								{
									lastDirection = Directions.Left;
									road.AddBlock(new RoadBlock(lastBlock.x - 1, lastBlock.y));
								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x + 1) == txtRoadBlock)//rigth
								{
									lastDirection = Directions.Right;
									road.AddBlock(new RoadBlock(lastBlock.x + 1, lastBlock.y));

								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x - 101) == txtRoadBlock)//up
								{
									lastDirection = Directions.Up;
									road.AddBlock(new RoadBlock(lastBlock.x, lastBlock.y - 1));

								}
								else
								{
									isEnd = true;
								}
								break;
							case Directions.Down:
								if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x - 1) == txtRoadBlock)//left
								{
									lastDirection = Directions.Left;
									road.AddBlock(new RoadBlock(lastBlock.x - 1, lastBlock.y));
								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x + 1) == txtRoadBlock)//rigth
								{
									lastDirection = Directions.Right;
									road.AddBlock(new RoadBlock(lastBlock.x + 1, lastBlock.y));

								}
								else if (GetSymbolFromText(map, lastBlock.y * 101 + lastBlock.x + 101) == txtRoadBlock)//down
								{
									lastDirection = Directions.Down;
									road.AddBlock(new RoadBlock(lastBlock.x, lastBlock.y + 1));
								}
								else
								{
									isEnd = true;
								}
								break;							
						}
					}

					lvl.AddRoad(road);
					roadEndings.RemoveAt(0);//remove start road point
					RoadBlock endBlock = road.GetLastRoadBlock();
					roadEndings.Remove(endBlock.y * 101 + endBlock.x);

				}
				lvl.Rooms[0].IsActive = true;
				lvl.Rooms[0].RoomType = RoomType.Start;
				/*while((s = map.ReadLine()) != null)
				{
					Console.WriteLine(s);
				}*/
			}

		}

		public string GetSymbolFromText(FileStream stream, int position)
		{
			int oldPos = (int)stream.Position;
			stream.Position = position;
			byte[] bt = new byte[1];
			stream.Read(bt, 0, (int)(1));
			stream.Position = oldPos;
			return System.Text.Encoding.UTF8.GetString(bt);
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
			activeRoom = lvl.GetActiveRoom();
			hero.SetStartPosition(activeRoom.X+activeRoom.Width/2, activeRoom.Y+activeRoom.Height/2);
			gameWindow.Render();			
			
			activeRoom.AddReplaceObject(hero);

			gameWindow.RenderRoom(activeRoom);
			//gameWindow.DrawInGameField(hero);
			GameLoop();
		}
		public void GameLoop()
		{
			isGameStarted = true;

			while (isGameStarted)
			{
				ConsoleKeyInfo pressedChar = Console.ReadKey(true);
				switch (pressedChar.Key)
				{
					case ConsoleKey.LeftArrow:
						MoveObject(hero ,Directions.Left);
						break;
					case ConsoleKey.RightArrow:
						MoveObject(hero, Directions.Right);
						break;
					case ConsoleKey.UpArrow:
						MoveObject(hero, Directions.Up);
						break;
					case ConsoleKey.DownArrow:
						MoveObject(hero, Directions.Down);
						break;
				}
			}
			//game moves only when hero moves
			
			
			
			//Room room = new Room(2, 2, 20, 6);
			//room.isActive = false;
			//gameWindow.RenderRoom(room);
			Console.ReadKey();
			//room.isActive = true;
			//gameWindow.RenderRoom(room);
		}

		private void MoveObject(GameObject gameObject, Directions directions)
		{
			int oldX = gameObject.x;
			int oldY = gameObject.y;
			if (activeRoom != null)
			{
				GameObject objectInMovingPlacee = null;
				//jei aktivuotas kambarys, tai juda kambarys
				switch (directions)
				{
					case Directions.Left:
						objectInMovingPlacee = activeRoom.GetObject(gameObject.x - 1, gameObject.y);
						break;
					case Directions.Right:
						objectInMovingPlacee = activeRoom.GetObject(gameObject.x + 1, gameObject.y);
						break;
					case Directions.Up:
						objectInMovingPlacee = activeRoom.GetObject(gameObject.x, gameObject.y - 1);
						break;
					case Directions.Down:
						objectInMovingPlacee = activeRoom.GetObject(gameObject.x, gameObject.y + 1);
						break;
					default:
						break;
				}
				if (objectInMovingPlacee != null)
				{
					//TODO: susidurimo apdirbimas

					if (objectInMovingPlacee is RoadBlock)
					{
						//perduodam valdyma keliui
						RoadBlock roadToMove = objectInMovingPlacee as RoadBlock;
						
						activeRoom.RemoveObject(oldX, oldY);
						
						activeRoad = roadToMove.Road;
						activeRoad.IsActive = true;
						roadToMove.AddObject(hero);
						hero.Move(directions);
						roadToMove.IsExplored = true;
						ExploreRoad();
						activeRoom.IsActive = false;
						gameWindow.RenderRoom(activeRoom);
						
						
						activeRoom = null;
						
					}

				}
				else
				{
					hero.Move(directions);
					activeRoom.MoveObject(oldX, oldY, hero);
				}
				if (activeRoad != null)
				{
					gameWindow.RenderRoad(activeRoad);
				}
				if (activeRoom != null)
				{
					gameWindow.RenderRoom(activeRoom);
				}
			}
			else
			{
				//jei neaktyvuotas kambarys tai juda takelis
				LinkedListNode<RoadBlock> curentPosition = activeRoad.GetNodeWithHero();
				LinkedListNode<RoadBlock> nextPosition = curentPosition.Next;
				LinkedListNode<RoadBlock> previousPosition = curentPosition.Previous;
				LinkedListNode<RoadBlock> nodeMoveTo = null;
				GameObject objectInMovingPlacee = null;
				//jei aktivuotas лудшфы, tai juda kelias
				int newX = -1;
				int newY = -1;
				switch (directions)
				{
					case Directions.Left:

						newX = curentPosition.Value.x - 1;
						newY = curentPosition.Value.y;
						break;
					case Directions.Right:
						newX = curentPosition.Value.x + 1;
						newY = curentPosition.Value.y;
						break;
					case Directions.Up:
						newX = curentPosition.Value.x;
						newY = curentPosition.Value.y - 1;
						break;
					case Directions.Down:
						newX = curentPosition.Value.x;
						newY = curentPosition.Value.y + 1;
						break;
					default:
						break;
				}
				if (nextPosition != null && newX == nextPosition.Value.x && newY == nextPosition.Value.y)
				{
					//next
					nodeMoveTo = nextPosition;
				}
				else if (previousPosition != null && newX == previousPosition.Value.x && newY== previousPosition.Value.y)
				{
					//previous
					nodeMoveTo = previousPosition;
				}
				if (nodeMoveTo != null)
				{
					if (!nodeMoveTo.Value.IsExistObject())
					{

						//takelis, galima judeti
						curentPosition.Value.RemoveObject(hero);
						hero.Move(directions);
						nodeMoveTo?.Value.AddObject(hero);
					}
					else
					{
						//todo apdirbti susidurima su objektu
					}
					
				}		
				else
				{
					//nebuvo node tikriname ar tai ne kambarys
					if (TryActivateRoom(curentPosition.Value.x, curentPosition.Value.y, directions))
					{
						//kambarys, perduodam valdyma kambariui
						RoadBlock roadToMove = objectInMovingPlacee as RoadBlock;
						hero.Move(directions);
						activeRoom.AddReplaceObject(hero);
						activeRoad.IsActive = false;
						curentPosition.Value.RemoveObject(hero);							
						gameWindow.RenderRoom(activeRoom);
						gameWindow.RenderRoad(activeRoad);
						activeRoad = null;
					}
				}
				if (activeRoad != null)
				{
					ExploreRoad();
					gameWindow.RenderRoad(activeRoad);
				}
			}

			
		}

		private bool TryActivateRoom(int x, int y, Directions directions)
		{
			switch (directions)
			{
				case Directions.Left:
					activeRoom = lvl.GetRommWithCoord(x - 1, y);
					break;
				case Directions.Right:
					activeRoom = lvl.GetRommWithCoord(x + 1, y);
					break;
				case Directions.Up:
					activeRoom = lvl.GetRommWithCoord(x, y - 1);
					break;
				case Directions.Down:
					activeRoom = lvl.GetRommWithCoord(x, y + 1);
					break;
				default:
					break;
			}
			if (activeRoom != null)
			{
				activeRoom.IsActive = true;
				return true;
			}
			return false;
		}

		private void ExploreRoad()
		{
			LinkedListNode<RoadBlock> nextNode = activeRoad.GetNodeWithHero();
			LinkedListNode<RoadBlock> previousNode = activeRoad.GetNodeWithHero();

			for (int i = 0; i < exploringBlocksOnRoad; i++)
			{
				if (nextNode != null)
				{
					nextNode.Value.IsExplored = true;
					nextNode = nextNode.Next;
				}
				if (previousNode != null)
				{
					previousNode.Value.IsExplored = true;
					previousNode = previousNode.Previous;
				}
			}
			
		}

		public void RenderRoom(Room room)
		{
			gameWindow.RenderRoom(room); 
		}
	}
}
