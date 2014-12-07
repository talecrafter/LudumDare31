using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class EnemyCharacter : BaseCharacter {

	public Sensor2D sensor;

	protected EnemyGroup _group;

	protected override void Awake()
	{
		base.Awake();

		if (sensor != null)
			sensor.sensorEvent += SensorEvent;

		_actor.wasDamaged += ActorWasDamaged;
	}

	void ActorWasDamaged()
	{
		if (_group != null)
		{
			_group.Alarm();
		}
	}

	public void SetGroup(EnemyGroup group)
	{
		_group = group;
	}

	public void SetRank(int rank)
	{
		Actor actor = GetComponent<Actor>();

		if (rank == 1)
		{

		}
		else if (rank == 2)
		{
			actor.maxHealth *= 1.5f;
			actor.movementSpeed *= 1.2f;
			actor.transform.localScale *= 1.25f;
			actor.weapon.amount *= 1.5f;
		}
		else
		{
			actor.maxHealth *= 3f;
			actor.movementSpeed *= 1.4f;
			actor.transform.localScale *= 1.5f;
			actor.weapon.amount *= 2;
		}

		actor.health = actor.maxHealth;
	}

	void SensorEvent(SensorEvent type, IActor actor)
	{
		if (type == CraftingLegends.Framework.SensorEvent.ActorDetected)
		{
			_actor.SetTarget(actor as Actor);
		}
	}
}