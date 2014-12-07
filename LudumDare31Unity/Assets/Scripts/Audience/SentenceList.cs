using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class SentenceList : List<string> {

	private int current = -1;

	public string Next()
	{
		if (current < Count - 1)
		{
			current++;
			return this[current];
		}

		return this.PickRandom();
	}

}