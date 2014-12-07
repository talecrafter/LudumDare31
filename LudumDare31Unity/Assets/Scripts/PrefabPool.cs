using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class PrefabPool
{
	private Dictionary<GameObject, GameObjectPool> _prefabPool = new Dictionary<GameObject,GameObjectPool>();

	// get an Object from the pool
	public IPooledObject Pop(GameObject prefab, Vector3? pos = null)
	{
		if (!_prefabPool.ContainsKey(prefab))
		{
			_prefabPool[prefab] = new GameObjectPool(prefab);
		}

		return _prefabPool[prefab].Pop(pos);
	}
}