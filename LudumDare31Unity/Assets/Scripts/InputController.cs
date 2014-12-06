﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class InputController : MonoBehaviour {

	private PlayerCharacter _current = null;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

    void Update()
	{
		if (_current != null)
		{
			HandleDirectMovement();
		}

		CheckSpecialKeys();
    }

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void SetInput(PlayerCharacter character)
	{
		_current = character;

	}

	public void DisableInput()
	{
		_current = null;
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void CheckSpecialKeys()
	{
		if (Application.isEditor)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Game.Instance.arena.Reset();
			}
		}
	}

	private void HandleDirectMovement()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Actor actor = _current.GetComponent<Actor>();
		actor.SetMovement(new Vector2(h, v));
	}
}
