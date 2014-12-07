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

	void SensorEvent(SensorEvent type, IActor actor)
	{
		if (type == CraftingLegends.Framework.SensorEvent.ActorDetected)
		{
			_actor.SetTarget(actor as Actor);
		}
	}
}