using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;
using CraftingLegends.Framework;

public class PlayerCharacter : MonoBehaviour {

	public string characterName;

	private Gender _gender;

	private Actor _actor;

    void Awake() {
		_actor = GetComponent<Actor>();

		if (ExtRandom.SplitChance())
			_gender = Gender.Male;
		else
			_gender = Gender.Female;

		characterName = RandomNames.GetUniqueName(_gender);
    }

	public void Die()
	{
		_actor.Kill();
	}
}