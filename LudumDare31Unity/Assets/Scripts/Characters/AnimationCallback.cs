using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class AnimationCallback : MonoBehaviour {

	private Actor _actor;

	void Awake()
	{
		_actor = GetComponentInParent<Actor>();
	}

	public void Execute()
	{
		_actor.ExecuteAttack();
	}

}