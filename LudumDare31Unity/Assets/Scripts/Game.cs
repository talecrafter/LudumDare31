using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class Game : BaseGameController {

	public static new Game Instance;

	public Arena arena;
	public InputController inputController;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	void Awake()
	{
		Instance = this;
		BaseGameController.Instance = this;
		levelGrid = FindObjectOfType<LevelGrid>();
		inputController = GetComponent<InputController>();
	}

	protected override void Start()
	{
		base.Start();

		arena = FindObjectOfType<Arena>();
	}

}