using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;
using CraftingLegends.Framework;

public class PlayerCharacter : BaseCharacter
{
	public string characterName;

	private Gender _gender;

	protected override void Awake()
	{
		base.Awake();

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