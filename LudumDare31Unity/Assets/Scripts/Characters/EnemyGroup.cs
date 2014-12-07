using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGroup {

	protected List<EnemyCharacter> enemies = new List<EnemyCharacter>();

	public void Add(EnemyCharacter enemy)
	{
		enemies.Add(enemy);
		enemy.SetGroup(this);
	}

	public void Alarm()
	{
		if (Game.Instance.arena.currentPlayerCharacter != null)
		{
			foreach (var enemy in enemies)
			{
				if (enemy.actor.isAlive && !enemy.actor.target.hasTarget)
				{
					enemy.actor.SetTarget(Game.Instance.arena.currentPlayerCharacter.actor);
				}
			}
		}
	}

}