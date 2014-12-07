using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;
using CraftingLegends.Core;

public class InputController : MonoBehaviour {

	private PlayerCharacter _current = null;
	private Actor _currentActor = null;

	private CrossHair _crossHair;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	void Awake()
	{
		_crossHair = FindObjectOfType<CrossHair>();		
	}

    void Update()
	{
		if (_current != null && _currentActor != null && _currentActor.isAlive)
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
		_currentActor = character.GetComponent<Actor>();
		_crossHair.Show();
	}

	public void DisableInput()
	{
		_current = null;
		_crossHair.Hide();
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
		Vector2 movement = new Vector2(h, v);

		if (movement != Vector2.zero)
			_currentActor.SetMovement(movement);

		bool lookToRight = true;

		if (_crossHair.worldPosition.x < _currentActor.position.x)
		{
			lookToRight = false;
		}
		_currentActor.SetDisplayDirection(lookToRight);
	}
}
