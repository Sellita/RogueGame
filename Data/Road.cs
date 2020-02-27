using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Road
	{
		private LinkedList<RoadBlock> roadBlocks;


		public Road()
		{
			roadBlocks = new LinkedList<RoadBlock>();
		}

		public Road(LinkedList<RoadBlock> roadBlocks)
		{
			this.roadBlocks = roadBlocks;
		}

		public void AddBlock(RoadBlock roadBlock)
		{
			roadBlocks.AddLast(roadBlock);
			
		}

	}
}
