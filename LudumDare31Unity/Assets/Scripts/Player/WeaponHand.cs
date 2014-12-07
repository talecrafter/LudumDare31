using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class WeaponHand : MonoBehaviour
{
	Transform _transform;

	void Awake()
	{
		_transform = transform;
	}

	public void SetWeapon(Weapon weapon)
	{
		GetComponent<SpriteRenderer>().sprite = weapon.image;
	}

	public void SetLookDirection(Vector3 lookPosition)
	{
		Vector2 lookDirection = lookPosition - _transform.position;

		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

		if (angle > 90)
			angle -= 180f;
		else if (angle < -90f)
			angle += 180f;
		else
			angle = -angle; // flip top and bottom

		angle *= 0.4f; // don't allow high angles

		_transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}