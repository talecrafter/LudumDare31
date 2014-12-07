using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class Game : BaseGameController {

	public static new Game Instance;

	public Arena arena;
	public PlayerInventory inventory;
	public InputController inputController;

	public PrefabPool prefabPool = new PrefabPool();

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	void Awake()
	{
		Instance = this;
		BaseGameController.Instance = this;
		levelGrid = FindObjectOfType<LevelGrid>();
		inputController = GetComponent<InputController>();
		inventory = GetComponent<PlayerInventory>();
		audioManager = GetComponent<BaseAudioManager>();
	}

	protected override void Start()
	{
		base.Start();

		arena = FindObjectOfType<Arena>();
	}

	protected override void Update()
	{
		base.Update();

		if (!Application.isWebPlayer && Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}