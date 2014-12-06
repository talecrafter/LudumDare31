using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class PlayerCharacter : MonoBehaviour {

	private Gender _gender;

	private string _characterName;

    void Awake() {
		if (ExtRandom.SplitChance())
			_gender = Gender.Male;
		else
			_gender = Gender.Female;

		_characterName = RandomNames.GetUniqueName(_gender);
    }

}