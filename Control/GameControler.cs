using System;
using System.Collections.Generic;
using System.Text;
using RogueGame.View.Windows;
using RogueGame.View.Elements;
using RogueGame.Data;
using System.IO;
using RogueGame.Control.Menu;

namespace RogueGame.Control
{
	class GameControler
	{
		private const int minRooms = 3;
		private const int MaxRooms = 4;
		private const int heroStartAtack = 3;
		private const int heroStartDef = 0;
		private const int heroStartHealth = 100;
		private const int heroStartSpeed = 50;
		private const int inventorySpace = 10;

		List<GameObject> enemysCanMove;


		GameWindow gameWindow;

		Hero hero;
		GameObject objectUnderHero = null;
		Enemy objectToAtack = null;
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

			//todo change every getlevelformfile

			//todo changee

			AddRoadsToRooms(); //UNCOMENT
			gameWindow.Render();//todo remove

			
			

			LvlGenerator.SetRoomsType(lvl.Rooms);
			spawner = new Spawner();
			spawner.SpawnAll(lvl.Rooms, lvl.Lvl);
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

		private Road GenerateRoad(int startx, int starty, Directions direction)
		{
			int randomSteps = LvlGenerator.rnd.Next(3, 10);
			if (direction == Directions.Down || direction == Directions.Up)
			{
				randomSteps = LvlGenerator.rnd.Next(3, 10);
			}
			else
			{
				randomSteps = LvlGenerator.rnd.Next(7, 30);
			}
			Road road = new Road();
			for (int i = 0; i < randomSteps; i++)
			{
				switch (direction)
				{
					case Directions.Left:
						road.AddBlock(new RoadBlock(startx - i, starty));
						break;
					case Directions.Right:
						road.AddBlock(new RoadBlock(startx + i, starty));
						break;
					case Directions.Up:
						road.AddBlock(new RoadBlock(startx, starty - i));
						break;
					case Directions.Down:
						road.AddBlock(new RoadBlock(startx, starty + i));
						break;
					default:
						break;
				}
				
			}
			return road;
		}

		private void RandomizeLevel()
		{
			int roomsCount = LvlGenerator.rnd.Next(minRooms, MaxRooms);
			int stopCounter = roomsCount * 4;
			int randomcoord = 0;
			Room generatedRoom = LvlGenerator.GetRandomRoom();
			lvl.AddRoom(generatedRoom);
			gameWindow.RenderRoom(generatedRoom);
			roomsCount--;
			Directions direction = Directions.Left;
			if (generatedRoom.X < 20)
			{
				direction = Directions.Left;
			}
			else if(generatedRoom.X>60)
			{
				direction = Directions.Right;
			}
		
			while (roomsCount > 1 && stopCounter > 0)
			{

				int directionInt = LvlGenerator.rnd.Next(0, Enum.GetNames(typeof(Directions)).Length);

				
				direction = (Directions)directionInt;

				switch (direction)
				{
					case Directions.Left:
						
						if (generatedRoom == null)
						{
							RoadBlock tmpblock = lvl.Roads[lvl.Roads.Count -1 ].RoadBlocks.Last.Value;
							Road tmpRoad = GenerateRoad(tmpblock.x, tmpblock.y, Directions.Left);
							foreach (var item in tmpRoad.RoadBlocks)
							{
								lvl.Roads[lvl.Roads.Count-1].AddBlock(new RoadBlock(item.x, item.y));
							}
						}
						else
						{
							randomcoord = LvlGenerator.rnd.Next(generatedRoom.Y + 1, generatedRoom.Y + generatedRoom.Height - 1);
							lvl.AddRoad(GenerateRoad(generatedRoom.X, randomcoord, Directions.Left));
						}
						break;
					case Directions.Right:
						
						if (generatedRoom == null)
						{
							RoadBlock tmpblock = lvl.Roads[lvl.Roads.Count-1].RoadBlocks.Last.Value;
							Road tmpRoad = GenerateRoad(tmpblock.x, tmpblock.y, Directions.Left);
							foreach (var item in tmpRoad.RoadBlocks)
							{
								lvl.Roads[lvl.Roads.Count-1].AddBlock(new RoadBlock(item.x, item.y));
							}
						}
						else
						{
							randomcoord = LvlGenerator.rnd.Next(generatedRoom.Y + 1, generatedRoom.Y + generatedRoom.Height - 1);
							lvl.AddRoad(GenerateRoad(generatedRoom.X + generatedRoom.Width - 1, randomcoord, Directions.Right));
						}
						break;
					case Directions.Up:
						if (generatedRoom == null)
						{
							RoadBlock tmpblock = lvl.Roads[lvl.Roads.Count-1].RoadBlocks.Last.Value;
							Road tmpRoad = GenerateRoad(tmpblock.x, tmpblock.y, Directions.Left);
							foreach (var item in tmpRoad.RoadBlocks)
							{
								lvl.Roads[lvl.Roads.Count-1].AddBlock(new RoadBlock(item.x, item.y));
							}
						}
						else
						{
							randomcoord = LvlGenerator.rnd.Next(generatedRoom.X + 1, generatedRoom.X + generatedRoom.Width - 1);
							
								lvl.AddRoad(GenerateRoad(randomcoord, generatedRoom.Y, Directions.Up));
						}
						break;
					case Directions.Down:
						
						if (generatedRoom == null)
						{
							RoadBlock tmpblock = lvl.Roads[lvl.Roads.Count-1].RoadBlocks.Last.Value;
							Road tmpRoad = GenerateRoad(tmpblock.x, tmpblock.y, Directions.Left);
							foreach (var item in tmpRoad.RoadBlocks)
							{
								lvl.Roads[lvl.Roads.Count-1].AddBlock(new RoadBlock(item.x, item.y));
							}
						}
						else
						{
							randomcoord = LvlGenerator.rnd.Next(generatedRoom.X + 1, generatedRoom.X + generatedRoom.Width - 1);
							lvl.AddRoad(GenerateRoad(randomcoord, generatedRoom.Y + generatedRoom.Height, Directions.Down));
						}
						break;
					default:
						break;
				}
				RoadBlock rb = lvl.Roads[lvl.Roads.Count - 1].RoadBlocks.Last.Value;
				generatedRoom = LvlGenerator.GetHalfRandomRoom(direction, rb.x, rb.y);
				if (generatedRoom != null)
				{
					lvl.AddRoom(generatedRoom);
					roomsCount--;
				}
				else
				{
					generatedRoom = null;
					
					
				}
				
				stopCounter--;
				if (generatedRoom != null)
				{
					gameWindow.RenderRoom(generatedRoom);
				}
				foreach (var item in lvl.Roads)
				{
					gameWindow.RenderRoad(item);
				}

			}
				/*
				int roomsCount = LvlGenerator.rnd.Next(minRooms, MaxRooms);
				int stopCounter = roomsCount * 4;
				//generate rooms
				while (roomsCount > 0 && stopCounter > 0)
				{
					Room generatedRoom = LvlGenerator.GetRandomRoom();
					//Check rooms colision
					//checl road colision
					bool isContactRooms = false;
					bool isContactRoads = false;

					if (lvl.Roads.Count > 0)
					{

						isContactRoads = CheckRoadColision(generatedRoom);
					}
					if(lvl.Rooms.Count > 0)
					{
						isContactRooms = CheckRoomColision(generatedRoom);
					}

					if(!isContactRoads && !isContactRooms)
					{
						//addRoomToLvl
						lvl.AddRoom(generatedRoom);
						gameWindow.RenderRoom(generatedRoom);//TODO remove
						roomsCount--;
						if (lvl.Rooms.Count > 1)
						{
							Road road = CreateRoad(generatedRoom, lvl.Rooms[lvl.Rooms.Count - 2]);
							lvl.AddRoad(road);
							//gameWindow.RenderRoad(road);
						}

					}
					stopCounter--;

				}
				*/
			}

		private Road CreateRoad(Room generatedRoom, Room room)
		{
			int firstRoomX1 = generatedRoom.X;
			int firstRoomX2 = generatedRoom.X + generatedRoom.Width;
			int firstRoomY1 = generatedRoom.Y;
			int firstRoomY2 = generatedRoom.Y + generatedRoom.Height;
			int secondRoomX1 = room.X;
			int secondRoomX2 = room.X + room.Width;
			int secondRoomY1 = room.Y;
			int secondRoomY2 = room.Y + room.Height;
			//check easies zones up down left right
			RoadDirections direction = GetPosibleDirection(generatedRoom, room);
			int directionForCorner = LvlGenerator.rnd.Next(0, 2);
			Road road = new Road();
			int randomOutPos = 0;
			int randomInPos = 0;
			int lastx = 0;
			int lasty = 0;
			int posibleLine = 0;
			switch (direction)
			{
				case RoadDirections.upLeft:
					
					if (directionForCorner == 0)
					{
						//first up
						randomOutPos = LvlGenerator.rnd.Next(firstRoomX1 + 1, firstRoomX2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomY1 + 1, secondRoomY2);
						
						road.AddBlock(new RoadBlock(randomOutPos ,firstRoomY1));
						lastx = randomOutPos;
						lasty = firstRoomY1 - 1;
						while (lasty != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty--;
						}
						while(lastx != secondRoomX2)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx--;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}
					else
					{
						//first left
						randomOutPos = LvlGenerator.rnd.Next(firstRoomY1 + 1, firstRoomY2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomX1 + 1, secondRoomX2);
						
						road.AddBlock(new RoadBlock(firstRoomX1 ,randomOutPos));
						lastx = firstRoomX1 - 1;
						lasty = randomOutPos;
						while (lastx != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx--;
						}
						while (lasty != secondRoomY2)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty--;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}

					break;
				case RoadDirections.upMidle:
					posibleLine = 0;
					
					if (firstRoomX1 < secondRoomX2)
					{
						posibleLine = secondRoomX2 - firstRoomX1;
						randomOutPos = LvlGenerator.rnd.Next(firstRoomX1 + 1, firstRoomX1 + posibleLine);
					}
					else
					{
						posibleLine = firstRoomX1 - secondRoomX2;
						randomOutPos = LvlGenerator.rnd.Next(secondRoomX2 + 1, secondRoomX2 + posibleLine);
					}
					lasty = firstRoomY1;
					while (lasty != secondRoomY2)
					{
						road.AddBlock(new RoadBlock(randomOutPos, lasty));
						lasty--;
					}				
					
					break;
				case RoadDirections.upRight:
					if (directionForCorner == 0)
					{
						//first up
						randomOutPos = LvlGenerator.rnd.Next(firstRoomX1 + 1, firstRoomX2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomY1 + 1, secondRoomY2);

						road.AddBlock(new RoadBlock(randomOutPos, firstRoomY1));
						lastx = randomOutPos;
						lasty = firstRoomY1 - 1;
						while (lasty != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty--;
						}
						while (lastx != secondRoomX1)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx++;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}
					else
					{
						//first left
						randomOutPos = LvlGenerator.rnd.Next(firstRoomY1 + 1, firstRoomY2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomX1 + 1, secondRoomX2);

						road.AddBlock(new RoadBlock(firstRoomX1, randomOutPos));
						lastx = firstRoomX1 + 1;
						lasty = randomOutPos;
						while (lastx != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx++;
						}
						while (lasty != secondRoomY2)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty--;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}
					break;
				case RoadDirections.MidleLeft:
					posibleLine = 0;
					while(!isInArea(firstRoomY1, firstRoomY2, randomOutPos) || !isInArea(secondRoomY1, secondRoomY2, randomOutPos))
					{
						randomOutPos = LvlGenerator.rnd.Next(firstRoomY1+1, firstRoomY2);
					}				

					lastx = firstRoomX1;
					while (lastx != secondRoomX2)
					{
						road.AddBlock(new RoadBlock(lastx, randomOutPos));
						lastx--;
					}
					road.AddBlock(new RoadBlock(lastx, randomOutPos));

					break;
				case RoadDirections.MidleRight:
					posibleLine = 0;
					while (!isInArea(firstRoomY1, firstRoomY2, randomOutPos) || !isInArea(secondRoomY1, secondRoomY2, randomOutPos))
					{
						randomOutPos = LvlGenerator.rnd.Next(firstRoomY1 + 1, firstRoomY2);
					}

					lastx = firstRoomX2;
					while (lastx != secondRoomX1)
					{
						road.AddBlock(new RoadBlock(lastx, randomOutPos));
						lastx++;
					}
					road.AddBlock(new RoadBlock(lastx, randomOutPos));
					break;
				case RoadDirections.DownLeft:
					if (directionForCorner == 0)
					{
						//first up
						randomOutPos = LvlGenerator.rnd.Next(firstRoomX1 + 1, firstRoomX2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomY1 + 1, secondRoomY2);

						road.AddBlock(new RoadBlock(randomOutPos, firstRoomY2));
						lastx = randomOutPos;
						lasty = firstRoomY2 + 1;
						while (lasty != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty++;
						}
						while (lastx != secondRoomX2)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx--;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}
					else
					{
						//first left
						randomOutPos = LvlGenerator.rnd.Next(firstRoomY1 + 1, firstRoomY2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomX1 + 1, secondRoomX2);

						road.AddBlock(new RoadBlock(firstRoomX1, randomOutPos));
						lastx = firstRoomX1 - 1;
						lasty = randomOutPos;
						while (lastx != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx--;
						}
						while (lasty != secondRoomY1)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty++;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}

					break;
				case RoadDirections.DownMidle:
					posibleLine = 0;

					if (firstRoomX1 < secondRoomX2)
					{
						posibleLine = secondRoomX2 - firstRoomX1;
						randomOutPos = LvlGenerator.rnd.Next(firstRoomX1 + 1, firstRoomX1 + posibleLine);
					}
					else
					{
						posibleLine = firstRoomX1 - secondRoomX2;
						randomOutPos = LvlGenerator.rnd.Next(secondRoomX2 + 1, secondRoomX2 + posibleLine);
					}
					lasty = firstRoomY1;
					while (lasty != secondRoomY1)
					{
						road.AddBlock(new RoadBlock(randomOutPos, lasty));
						lasty++;
					}
					break;
				case RoadDirections.DownRight:
					if (directionForCorner == 0)
					{
						//first down
						randomOutPos = LvlGenerator.rnd.Next(firstRoomX1 + 1, firstRoomX2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomY1 + 1, secondRoomY2);

						road.AddBlock(new RoadBlock(randomOutPos, firstRoomY2));
						lastx = randomOutPos;
						lasty = firstRoomY2 + 1;
						while (lasty != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty++;
						}
						while (lastx != secondRoomX1)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx++;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}
					else
					{
						//first left
						randomOutPos = LvlGenerator.rnd.Next(firstRoomY1 + 1, firstRoomY2);
						randomInPos = LvlGenerator.rnd.Next(secondRoomX1 + 1, secondRoomX2);

						road.AddBlock(new RoadBlock(firstRoomX1, randomOutPos));
						lastx = firstRoomX1 + 1;
						lasty = randomOutPos;
						while (lastx != randomInPos)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lastx++;
						}
						while (lasty != secondRoomY1)
						{
							road.AddBlock(new RoadBlock(lastx, lasty));
							lasty++;
						}
						road.AddBlock(new RoadBlock(lastx, lasty));
					}
					break;
					
				default:
					break;
			}
			return road;
		}

		private bool isInArea(int x, int y, int check)
		{
			return (x < check && y > check); 
		}

		private RoadDirections GetPosibleDirection(Room generatedRoom, Room room)
		{
			int firstRoomX1 = generatedRoom.X;
			int firstRoomX2 = generatedRoom.X+generatedRoom.Width;
			int firstRoomY1 = generatedRoom.Y;
			int firstRoomY2 = generatedRoom.Y + generatedRoom.Height;
			int secondRoomX1 = room.X;
			int secondRoomX2 = room.X + room.Width;
			int secondRoomY1 = room.Y;
			int secondRoomY2 = room.Y + room.Height;

			if(firstRoomX1 >= secondRoomX2 && firstRoomY1 >= secondRoomY2)
			{
				return RoadDirections.upLeft;
			}
			if ((firstRoomX1 < secondRoomX2 && firstRoomY1 >= secondRoomY2 && secondRoomX1<firstRoomX2-2) || (firstRoomX2 > secondRoomX1 && firstRoomY1 >= secondRoomY2))
				{
				return RoadDirections.upMidle;
			}
			if (firstRoomX2 < secondRoomX1 && firstRoomY1 >= secondRoomY2)
			{
				return RoadDirections.upRight;
			}

			if ((firstRoomX1 > secondRoomX2 && firstRoomY1 + 2 > secondRoomY2) || (firstRoomX1 > secondRoomX2 && firstRoomY2-2 > secondRoomY1))
			{
				return RoadDirections.MidleLeft;
			}
			if ((firstRoomX2 < secondRoomX1 && firstRoomY1 + 2 > secondRoomY2) || (firstRoomX2 < secondRoomX1 && firstRoomY2+2 > secondRoomY1))
			{
				return RoadDirections.MidleRight;
			}

			if (firstRoomX1 >= secondRoomX2 && firstRoomY2 < secondRoomY2)
			{
				return RoadDirections.DownLeft;
			}
			if ((firstRoomX1 < secondRoomX2 && firstRoomY2 < secondRoomY1) || (firstRoomX2 > secondRoomX1 && firstRoomY2 < secondRoomY1))
			{
				return RoadDirections.DownMidle;
			}
			if (firstRoomX2 <= secondRoomX1 && firstRoomY2 < secondRoomY1)
			{
				return RoadDirections.DownRight;
			}
			return RoadDirections.unspecified;

		}

		private bool CheckRoadColision(Room generatedRoom)
		{
			foreach (Road road in lvl.Roads)
			{
				foreach (RoadBlock block in road.RoadBlocks)
				{
					if (generatedRoom.X < block.x && generatedRoom.X + generatedRoom.Width > block.x && generatedRoom.Y < block.y && generatedRoom.Y + generatedRoom.Height > block.y)
					{
						return true;
					}
					
				}
			
			}
			return false;
		}

		private bool CheckRoomColision(Room generatedRoom)
		{
			for (int i = generatedRoom.X-1; i < generatedRoom.X + generatedRoom.Width+1; i++)
			{
				for (int j = generatedRoom.Y-1; j < generatedRoom.Y + generatedRoom.Height+1; j++)
				{
					if (lvl.GetRommWithCoord(i, j) != null)
					{
						return true;
					}
				}
			}
			return false;
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
				//lvl.Rooms[0].IsActive = true;
				//lvl.Rooms[0].RoomType = RoomType.Start;
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
			hero = new Hero(heroName, inventorySpace);
			hero.Def = heroStartDef;
			hero.Speed = heroStartSpeed;
			hero.Health = heroStartHealth;
			hero.Damage = heroStartAtack;
			//positioning hero TODO:
			//lvl.Rooms[0].IsActive = true;
			//lvl.Rooms[0].RoomType = RoomType.Start;


			activeRoom = lvl.GetStartRoom();
			ArrayElementsStruct coord = activeRoom.GetEmptyCenter();
			activeRoom.IsActive = true;
			hero.SetStartPosition(coord.x, coord.y);
			gameWindow.Render();	
			
			
			activeRoom.AddReplaceObject(hero);

			gameWindow.RenderRoom(activeRoom);
			gameWindow.SetDungeonLvl(lvl.Lvl);
			//gameWindow.DrawInGameField(hero);
			GameLoop();
		}
		public void GameLoop()
		{
			isGameStarted = true;

			while (isGameStarted)
			{
				
				ConsoleKeyInfo pressedChar = Console.ReadKey(true);
				ClearTmpHolders();
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
					case ConsoleKey.U:
						IteractWithObject();
						break;
					case ConsoleKey.I:
						OpenInventory();
						continue;
						//break;
					case ConsoleKey.Escape:
						ActionsForMenu actionAfterPause = PauseMenu();
						switch (actionAfterPause)
						{
							case ActionsForMenu.MainMenu:
								isGameStarted = false;
								continue;
								break;
							case ActionsForMenu.Quit:
								Environment.Exit(0);
								break;

							default:
								gameWindow.Render();
								RenderExploriedGameField();
								break;
						}
						continue;
						//break;
				}
				if (objectToAtack != null)
				{
					HeroAtackEnemy(objectToAtack);
				}
				if (activeRoom != null && enemysCanMove != null)
				{
					if (enemysCanMove.Count > 0)
					{
						EnemysMoveAtack();
					}
				}

				RenderHeroStats();
				if(hero.Health <= 0)
				{
					ActionsForMenu action = GameOver();
					switch (action)
					{
						case ActionsForMenu.Restart:
							RestartGame();
							break;
						default:
							isGameStarted = false;
							break;
					}

					
				}
			}

		}

		private ActionsForMenu PauseMenu()
		{
			PauseMenuControler pauseControler = new PauseMenuControler();
			return pauseControler.ShowMenu();
		}

		private void RestartGame()
		{
			lvl = new Level();
			GetLevelFromFile("Map.txt");//TODO: change to generation!!!!!!!!!!!
			AddRoadsToRooms();
			LvlGenerator.SetRoomsType(lvl.Rooms);
			spawner = new Spawner();
			spawner.SpawnAll(lvl.Rooms, lvl.Lvl);
			hero = new Hero(hero.Name, inventorySpace);
			hero.Def = heroStartDef;
			hero.Speed = heroStartSpeed;
			hero.Health = heroStartHealth;
			hero.Damage = heroStartAtack;

			activeRoom = lvl.GetStartRoom();
			ArrayElementsStruct coord = activeRoom.GetEmptyCenter();
			activeRoom.IsActive = true;
			hero.SetStartPosition(coord.x, coord.y);
			gameWindow.Render();


			activeRoom.AddReplaceObject(hero);

			gameWindow.RenderRoom(activeRoom);
			gameWindow.SetDungeonLvl(lvl.Lvl);
			////
			///		
			GameObject objectUnderHero = null;
			Enemy objectToAtack = null;
			enemysCanMove.Clear();
			RenderHeroStats();
		}

		private ActionsForMenu GameOver()
		{
			GameOverControler gameOverControler = new GameOverControler(lvl.Lvl);
			return gameOverControler.ShowMenu();

			
		}

		private void EnemysMoveAtack()
		{
			
			foreach (Enemy enemy in enemysCanMove)
			{
				int xDiff = enemy.x > hero.x ? enemy.x - hero.x : hero.x - enemy.x;
				int yDiff = enemy.y > hero.y ? enemy.y - hero.y : hero.y - enemy.y;
				if ((xDiff == 0 && yDiff == 1) || (xDiff == 1 && yDiff == 0))
				{
					//near make attack
					int Atackrezult = enemy.Atack(hero);
					gameWindow.WriteEvent($"{enemy.renderChar} deal you {Atackrezult} dmg");
					RenderHeroStats();
				}
				else
				{
					if (xDiff > yDiff)
					{
						//moveByX
						if (!MoveByX(enemy))
						{
							MoveByY(enemy);
						}
					}
					else
					{   //moveByY;
						if (!MoveByY(enemy))
						{
							MoveByX(enemy);
						}

					}
				}
			}
			gameWindow.RenderRoom(activeRoom);
		}

		private bool MoveByX(Enemy enemy)
		{
			if (hero.x > enemy.x)
			{
				return 	MoveEnemy(enemy, Directions.Right);
			}
			else
			{
				return MoveEnemy(enemy, Directions.Left);
			}
		}
		private bool MoveByY(Enemy enemy)
		{
			if (hero.y > enemy.y)
			{
				return MoveEnemy(enemy, Directions.Down);
			}
			else
			{
				return MoveEnemy(enemy, Directions.Up);
			}
		}



		//private void EnemysMoveToHero(List<game);

		private void RenderHeroStats()
		{
			gameWindow.renderHero(hero);
		}

		private void HeroAtackEnemy(Enemy objectToAtack)
		{
			int dmg = hero.Atack(objectToAtack);
			gameWindow.WriteEvent($"You Atack: {dmg}");
			gameWindow.WriteEvent($"{objectToAtack.renderChar} health is {objectToAtack.Health}");
			TryRemoveDeadEnemy(objectToAtack);
			this.objectToAtack = null;
		}

		private void TryRemoveDeadEnemy(Enemy objectToAtack)
		{
			if(objectToAtack.Health <= 0)
			{
				activeRoom.RemoveObject(objectToAtack);
				enemysCanMove.Remove(objectToAtack);
				RenderRoom(activeRoom);
			}
		}

		private void OpenInventory()
		{

			//todo: open inventory
			InventoryControler inventoryControler = new InventoryControler(hero.Inventory, hero);
			inventoryControler.RunLoop();

			gameWindow.Render();
			RenderExploriedGameField();
			//throw new NotImplementedException();
		}

		private void RenderExploriedGameField()
		{

			foreach (Room room in lvl.Rooms)
			{
				if (room.IsExplored)
				{
					gameWindow.RenderRoom(room);
				}
			}
			foreach (Road road in lvl.Roads)
			{
				if (road.IsExplored)
				{
					gameWindow.RenderRoad(road);
				}
			}
		}

		private void ClearTmpHolders()
		{
			gameWindow.SetkeyInfoHolder3("");
			gameWindow.SetInfoHolder("");
			gameWindow.SetErorHolder("");
		}

		private void IteractWithObject()
		{
			if (objectUnderHero is Equipment)
			{
				Equipment eq = objectUnderHero as Equipment;
				if (hero.Inventory.IsInventoryFull())
				{
					gameWindow.WriteEvent("Inventory full!");
				}
				else
				{
					hero.Inventory.AddItem(objectUnderHero);
					activeRoom.RemoveObject(objectUnderHero);
					objectUnderHero = null;
				}
			}
			if (objectUnderHero is Gold)
			{
				Gold gold = objectUnderHero as Gold;
				hero.gold += gold.Value;
				activeRoom.RemoveObject(objectUnderHero);
				objectUnderHero = null;

			}
			if (objectUnderHero is Potion)
			{
				Potion potion = objectUnderHero as Potion;
				DrinkPotion(potion);
				
				activeRoom.RemoveObject(objectUnderHero);
				objectUnderHero = null;
			}
			if (objectUnderHero is Door)
			{


				int curentLvl = lvl.Lvl;
				lvl = new Level();
				lvl.Lvl+= curentLvl;
				GetLevelFromFile("Map.txt");//TODO: change to generation!!!!!!!!!!!
				AddRoadsToRooms();

				LvlGenerator.SetRoomsType(lvl.Rooms);
				spawner = new Spawner();
				spawner.SpawnAll(lvl.Rooms, lvl.Lvl);

				activeRoom = lvl.GetStartRoom();
				ArrayElementsStruct coord = activeRoom.GetEmptyCenter();
				activeRoom.IsActive = true;
				hero.SetStartPosition(coord.x, coord.y);
				gameWindow.Render();


				activeRoom.AddReplaceObject(hero);

				gameWindow.RenderRoom(activeRoom);
				objectUnderHero = null;
				gameWindow.SetDungeonLvl(lvl.Lvl);

			}

		}

		private void DrinkPotion(Potion potion)
		{
			switch (potion.PotionType)
			{
				case PotionType.Health:
					hero.Health += potion.Value;
					break;
				case PotionType.Speed:
					hero.Speed += potion.Value;
					break;
				case PotionType.Defense:
					hero.BaseDef += potion.Value;
					break;
				case PotionType.Atack:
					hero.Basedamage += potion.Value;
					
					break;
				default:
					break;
			}
		}

		private bool MoveEnemy(Enemy enemy, Directions directions)
		{
			GameObject objectInMovingPlacee = null;
			switch (directions)
			{
				case Directions.Left:
					objectInMovingPlacee = activeRoom.GetFirstObject(enemy.x - 1, enemy.y);
					break;
				case Directions.Right:
					objectInMovingPlacee = activeRoom.GetFirstObject(enemy.x + 1, enemy.y);
					break;
				case Directions.Up:
					objectInMovingPlacee = activeRoom.GetFirstObject(enemy.x, enemy.y - 1);
					break;
				case Directions.Down:
					objectInMovingPlacee = activeRoom.GetFirstObject(enemy.x, enemy.y + 1);
					break;
				default:
					break;
			}
			if (objectInMovingPlacee != null)
			{
				return false;

			}
			else
			{
				int oldX = enemy.x;
				int oldY = enemy.y;
				enemy.Move(directions);
				activeRoom.MoveObject(oldX, oldY, enemy);
			}
			return true;
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
						objectInMovingPlacee = activeRoom.GetFirstObject(gameObject.x - 1, gameObject.y);
						break;
					case Directions.Right:
						objectInMovingPlacee = activeRoom.GetFirstObject(gameObject.x + 1, gameObject.y);
						break;
					case Directions.Up:
						objectInMovingPlacee = activeRoom.GetFirstObject(gameObject.x, gameObject.y - 1);
						break;
					case Directions.Down:
						objectInMovingPlacee = activeRoom.GetFirstObject(gameObject.x, gameObject.y + 1);
						break;
					default:
						break;
				}
				if (objectInMovingPlacee != null)
				{
					//TODO: susidurimo apdirbimas
					if (objectInMovingPlacee.isColizionable)
					{

						//zengem ant objekto
						hero.Move(directions);
						activeRoom.MoveObject(oldX, oldY, hero);
						objectUnderHero = objectInMovingPlacee;

						//write text in infoHolder
						WriteObjectInfoInHolders(objectUnderHero);
						
						
					}

					if (objectInMovingPlacee is RoadBlock)
					{
						//perduodam valdyma keliui
						RoadBlock roadToMove = objectInMovingPlacee as RoadBlock;
						
						activeRoom.RemoveObject(oldX, oldY, hero);
						
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

					if (objectInMovingPlacee is Enemy)
					{
						objectToAtack = objectInMovingPlacee as Enemy;
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
				//jei aktivuotas kelias, tai juda kelias
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
						enemysCanMove = activeRoom.GetEnemys();
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

		private void WriteObjectInfoInHolders(GameObject gameObject)
		{
			string textToWrite = $"You found";

			if(gameObject is Equipment)
			{
				textToWrite += $" {gameObject.Name} with:";
				Equipment eq = gameObject as Equipment;
				if (eq.Damage > 0)
				{
					textToWrite +=" Damage: " + eq.Damage;
				}
				if (eq.Defense > 0)
				{
					textToWrite += " Defense: " + eq.Defense;
				}
				gameWindow.SetkeyInfoHolder3("[U] to Pick up");
			}
			if (gameObject is Gold)
			{
				Gold gold = gameObject as Gold;
				textToWrite += $" {gold.Value}$";
				gameWindow.SetkeyInfoHolder3("[U] to Pick up");
			}
			if (gameObject is Potion)
			{
				Potion potion = gameObject as Potion;
				textToWrite += $" {potion.PotionType.ToString()} potion with: {potion.Value}";
				switch (potion.PotionType)
				{
					case PotionType.Health:
						textToWrite += $" HP";
						break;
					case PotionType.Speed:
						textToWrite += $" Speed";
						break;
					case PotionType.Defense:
						textToWrite += $" Defense";
						break;
					case PotionType.Atack:
						textToWrite += $" Atack";
						break;
					default:
						break;
				}
				gameWindow.SetkeyInfoHolder3("[U] to Drink");
			}
			if (gameObject is Door)
			{
				textToWrite += $" Next LVL portal";
				gameWindow.SetkeyInfoHolder3("[U] Move next LVL");
			}

			gameWindow.SetInfoHolder(textToWrite);
		}

		private bool TryActivateRoom(int x, int y, Directions directions)
		{
			switch (directions)
			{
				case Directions.Left:
					x--;
					break;
				case Directions.Right:
					x++;
					break;
				case Directions.Up:
					y--;
					break;
				case Directions.Down:
					y++;
					break;
				default:
					break;
			}
			activeRoom = lvl.GetRommWithCoord(x, y);
			if (activeRoom != null)
			{
				//patikriname ar musu koordinate nebus siena
				if(activeRoom.GetFirstObject(x, y) is WallBlock)
				{
					activeRoom = null;
					return false;
				}
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
