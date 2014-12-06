using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class Game : BaseGameController {

	public Arena arena;

	protected override void Start()
	{
		base.Start();

		arena = FindObjectOfType<Arena>();
	}

}