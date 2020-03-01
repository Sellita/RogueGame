using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Road
	{
		private LinkedList<RoadBlock> roadBlocks;
		private LinkedListNode<RoadBlock> lastUsed;
		public bool IsExplored { get; set; }
		private bool isActive = false;
			

		internal LinkedList<RoadBlock> RoadBlocks { get => roadBlocks; }
		public bool IsActive { get => isActive; 
			set 
			{ 
				isActive = value;
				if (isActive)
				{
					IsExplored = true;
				}
			} }

		public Road()
		{
			roadBlocks = new LinkedList<RoadBlock>();
		}
		



		public RoadBlock GetLastRoadBlock()
		{	lastUsed = roadBlocks.Last;
			return roadBlocks.Last.Value;			
		}

		public RoadBlock GetFirstRoadBlock()
		{
			lastUsed = roadBlocks.First;
			return roadBlocks.First.Value;
		}

		public void AddBlock(RoadBlock roadBlock)
		{
			roadBlocks.AddLast(roadBlock);
			roadBlock.Road = this;
		}

		internal LinkedListNode<RoadBlock> GetNodeWithHero()
		{
			LinkedListNode<RoadBlock> curent = roadBlocks.First;
			while(curent!= null)
			{
				if (curent.Value.IsExistObject())
				{
					GameObject obj = curent.Value.getLastObject();
					if (obj is Hero)
					{
						return curent;
					}
				}
				curent = curent.Next;
			}
			return null;
		}
	}
}
