﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class BaseCharacter : MonoBehaviour {

	public delegate void DeathDelegate(BaseCharacter character);
	public event DeathDelegate DeathEvent;

	protected Actor _actor;
	public Actor actor
	{
		get
		{
			return _actor;
		}
	}

	protected AnimationController _animationController;

	protected virtual void Awake()
	{
		_actor = GetComponent<Actor>();
		_actor.stateChanged += ActorStateChanged;
		_animationController = GetComponent<AnimationController>();
		_animationController.FadeIn();
	}

	protected virtual void Start()
	{
		_actor.target.SetPathField(Game.Instance.levelGrid);
	}

	void ActorStateChanged(IActor actor, ActorState state)
	{
		if (state == ActorState.Dead)
		{
			if (DeathEvent != null)
			{
				DeathEvent(this);
			}
		}
	}

	public void Spawn(Room room)
	{
		if (_animationController != null)
		{
			_animationController = GetComponent<AnimationController>();
			_animationController.SetMaterialColor(new Color(1f, 1f, 1f, 0));
		}

		GetComponent<ArenaItem>().currentRoom = room;
	}

	public void Despawn()
	{
		if (this != null) // could be destroyed in this frame but reference still hold
			StartCoroutine(DespawnRoutine());
	}

	private IEnumerator DespawnRoutine()
	{
		if (_actor.isAlive)
			_actor.MakeInActive();

		_animationController.FadeOutFast(1f);

		yield return new WaitForSeconds(1f);

		Destroy(gameObject);
	}
}