using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class Room : MonoBehaviour {

	private bool _isSpawned = false;

	private Collider2D _collider2D;
	private Bounds _bounds;

	private Animator _animator;
	private SpriteRenderer _image;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

	public void Awake()
	{
		_collider2D = GetComponent<Collider2D>();
		_bounds = _collider2D.bounds;

		_animator = GetComponent<Animator>();

		_image = GetComponentInChildren<SpriteRenderer>();
		_image.enabled = false;
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void Show()
	{
		_collider2D.enabled = false;
		BaseGameController.Instance.levelGrid.UpdateField(_bounds);

		_image.enabled = true;
		if (_animator != null)
			_animator.SetBool("spawned", true);
	}

	public void Hide()
	{
		_collider2D.enabled = true;
		BaseGameController.Instance.levelGrid.UpdateField(_bounds);

		if (_animator != null)
			_animator.SetBool("spawned", false);
	}

}