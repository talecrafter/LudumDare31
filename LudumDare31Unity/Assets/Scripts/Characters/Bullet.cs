using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;
using CraftingLegends.Framework;

public class Bullet : MonoBehaviour, IPooledObject {

	const float bulletSpeed = 25f;
	const float hitDisplayTime = 0.15f;

	public Sprite hitImage;
	public Sprite missImage;
	private Sprite _bulletImage;
	private SpriteRenderer _renderer;

	private Weapon _weapon;
	private Actor _shooter;
	private Rigidbody2D _rigidBody2D;
	private Collider2D _collider2D;
	private Transform _transform;

	private float maxRangeSquared;

	private Vector2 startPos;

	private bool _isActive = true;

	void Awake()
	{
		Init();
	}

	public void Shoot(Actor shooter, Weapon weapon, Vector2 direction)
	{
		_shooter = shooter;
		_weapon = weapon;

		Init();

		_rigidBody2D.velocity = direction.normalized * bulletSpeed;

		startPos = _transform.position.Vector2();
		float maxRange = weapon.range + Random.Range(-weapon.range * 0.1f, weapon.range * 0.1f);
		maxRangeSquared = maxRange * maxRange;

		StartCoroutine(Fly());
	}

	private IEnumerator Fly()
	{
		while ((_transform.position.Vector2() - startPos).sqrMagnitude < maxRangeSquared)
		{
			yield return null;
		}

		if (missImage != null && _renderer.sprite != hitImage)
			_renderer.sprite = missImage;

		StartCoroutine(RemainAndVanish());
	}

	private IEnumerator RemainAndVanish()
	{
		//_rigidBody2D.velocity *= 0.002f;
		_rigidBody2D.velocity = Vector2.zero;
		yield return new WaitForSeconds(hitDisplayTime);
		Vanish();
	}

	private void Vanish()
	{
		if (isDisabled != null)
			isDisabled(this);
	}

	private void Init()
	{
		if (_rigidBody2D == null)
			_rigidBody2D = GetComponent<Rigidbody2D>();
		if (_collider2D == null)
			_collider2D = GetComponent<Collider2D>();
		if (_transform == null)
			_transform = transform;
		if (_renderer == null)
		{
			_renderer = GetComponent<SpriteRenderer>();
			_bulletImage = _renderer.sprite;
		}
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (!_isActive)
			return;

		HitReceiver target = coll.transform.GetComponent<HitReceiver>();

		if (target != null && target.actor != _shooter && target.actor.isAlive)
		{
			// we have a hit
			target.actor.ApplyDamage(_weapon.amount);
			_isActive = false; // disarm bullet

			if (hitImage != null)
				_renderer.sprite = hitImage;

			StartCoroutine(RemainAndVanish());
		}
	}

	public void ToggleOn()
	{
		Init();

		_collider2D.enabled = true;
		_renderer.enabled = true;
		_renderer.sprite = _bulletImage;

		_isActive = true;
	}

	public void ToggleOff()
	{
		Init();

		StopAllCoroutines();

		_rigidBody2D.velocity = Vector2.zero;
		_collider2D.enabled = false;
		_renderer.enabled = false;

		_isActive = false;
	}

	public event System.Action<IPooledObject> isDisabled;

	public bool isInactiveInObjectPool
	{
		get; set;
	}
}