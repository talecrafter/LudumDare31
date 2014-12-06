using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CraftingLegends.Framework;

public class PlayerDisplay : MonoBehaviour {

	public Text nameDisplay;
	public Image healthImage;

	private Actor _actor;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	void Update()
	{
		if (_actor != null)
		{
			UpdateHealth();
		}
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void AttachToPlayer(PlayerCharacter character)
	{
		nameDisplay.text = character.characterName;
		_actor = character.GetComponent<Actor>();

		gameObject.SetActive(true);
	}

	public void DetachFromPlayer()
	{
		_actor = null;

		gameObject.SetActive(false);
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void UpdateHealth()
	{
		float remaining = _actor.health / _actor.maxHealth;
		healthImage.fillAmount = remaining;
	}
}