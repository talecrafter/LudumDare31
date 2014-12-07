using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;
using CraftingLegends.Core;

public class PlayerInventory : MonoBehaviour {

	public List<Weapon> startWeapons = new List<Weapon>();

	public List<Weapon> currentWeapons = new List<Weapon>();

	public InventoryDisplay inventoryDisplay;

	public AudioClip switchSound;

	private int index = 0;

	private Actor _currentActor;
	private WeaponHand _weaponHand;

	private Timer _coolDown;

	private Weapon current
	{
		get
		{
			return currentWeapons[index];
		}
	}

	private bool canShoot
	{
		get
		{
			return _coolDown == null;
		}
	}

	// ================================================================================
	//  unity 
	// --------------------------------------------------------------------------------

	void Update()
	{
		if (_coolDown != null)
		{
			_coolDown.Update();

			if (_coolDown.hasEnded)
			{
				_coolDown = null;
			}
		}
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void Trigger(CrossHair crossHair)
	{
		if (canShoot)
		{
			Shoot(current, crossHair);
		}
	}

	public void HoldTrigger(CrossHair crossHair)
	{
		if (canShoot)
		{
			Shoot(current, crossHair);
		}
	}

	public void Switch()
	{
		if (currentWeapons.Count <= 1)
			return;
		
		index++;
		index = index % currentWeapons.Count;

		_weaponHand.SetWeapon(current);
		inventoryDisplay.SetWeapon(current);

		Game.Instance.audioManager.Play(switchSound);
	}

	public void AttachToCharacter(PlayerCharacter character)
	{
		_currentActor = character.GetComponent<Actor>();
		_weaponHand = character.GetComponentInChildren<WeaponHand>();
		currentWeapons.Clear();
		index = 0;

		foreach (var weapon in startWeapons)
		{
			currentWeapons.Add(weapon);
		}

		_weaponHand.SetWeapon(current);
		inventoryDisplay.SetWeapon(current);
		inventoryDisplay.Show();
	}

	public void DetachFromCharacter()
	{
		inventoryDisplay.Hide();
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void Shoot(Weapon weapon, CrossHair crossHair)
	{
		_coolDown = new Timer(weapon.attackDuration);

		Vector2 targetPosition = crossHair.worldPosition;

		Vector2 distanceVector = targetPosition - _currentActor.actionPivot.position.Vector2();
		float distance = distanceVector.magnitude;

		for (int i = 0; i < weapon.bulletCount; i++)
		{
			Vector2 scatter = Random.insideUnitCircle * distance * weapon.scatter;
			Vector2 actualTarget = targetPosition + scatter;

			Vector2 direction = actualTarget - _currentActor.actionPivot.position.Vector2();

			// recoil for one of the bullets
			if (i == 0)
				_currentActor.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * 55f);

			var bulletObject = Game.Instance.prefabPool.Pop(weapon.shellPrefab, _currentActor.actionPivot.position);
			Bullet bullet = bulletObject as Bullet;
			bullet.Shoot(_currentActor, weapon, direction);
		}

		if (weapon.attackSound != null)
			Game.Instance.audioManager.Play(weapon.attackSound);
	}
}