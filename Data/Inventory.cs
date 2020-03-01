using System;
using System.Collections.Generic;
using System.Text;

namespace RogueGame.Data
{
	class Inventory
	{
		
		GameObject[] items;

		internal GameObject[] Items { get => items;}

		public Inventory(int MaxItems)
		{
			items = new GameObject[MaxItems];
		}

		public bool IsInventoryFull()
		{
			foreach (GameObject gameObject in items)
			{
				if(gameObject == null)
				{
					return false;
				}
			}
			return true;
		}

		public int GetEmptyPosition()
		{
			for (int i = 0; i < items.Length; i++)	
			{
				if (items[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		public void AddItem(GameObject gameObject)
		{
			items[GetEmptyPosition()] = gameObject;
		}

	}
}
