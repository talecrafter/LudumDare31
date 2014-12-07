using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CraftingLegends.Framework
{
	[System.Serializable]
	public class Weapon
	{
		public enum WeaponType
		{
			Melee,
			Artillery,
			Bullet,
			Healing
		}

		public string name;
		public Sprite image;
		public Sprite inventoryImage;

		public AudioClip attackSound;

		[SerializeField]
		public WeaponType type = WeaponType.Melee;

		public float amount;

		public float attackDuration;

		public float range;

		public float precision = 1.0f;

		public float scatter = 0.1f;

		public float rangeDelay;

		public int bulletCount = 1;
		public GameObject shellPrefab;
	}
}