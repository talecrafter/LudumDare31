using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CraftingLegends.Framework;

public class PlayerDisplay : MonoBehaviour {

	public Text nameDisplay;
	public Image healthImage;

	private Actor _actor;

	private Animator _animator;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}

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

		_animator.SetBool("show", true);
	}

	public void DetachFromPlayer()
	{
		_actor = null;
		healthImage.fillAmount = 0;

		_animator.SetBool("show", false);
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