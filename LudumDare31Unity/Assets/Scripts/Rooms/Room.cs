using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class Room : MonoBehaviour {

	public bool isActive { get { return _isActive; }}
	private bool _isActive = false;

	public List<Room> neighbours = new List<Room>();

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

	public void OnDrawGizmos()
	{
		RenderDepthUpdate updateScript = GetComponent<RenderDepthUpdate>();
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + updateScript.offset);

		// draw all connections
		foreach (var item in neighbours)
		{
			Vector3 direction = item.transform.position - transform.position;
			direction *= 0.5f;

			Gizmos.DrawLine(transform.position, transform.position + direction);
		}
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
			_animator.SetBool("active", true);
	}

	public void Hide()
	{
		_collider2D.enabled = true;
		BaseGameController.Instance.levelGrid.UpdateField(_bounds);

		if (_animator != null)
			_animator.SetBool("active", false);
	}

	public void FindNeighbourRooms()
	{
		neighbours.Clear();

		Collider2D coll = GetComponent<Collider2D>();

		float rangeX = 1.8f;
		float rangeY = 0.9f;
		Vector2 fromPos = new Vector2(transform.position.x - rangeX, transform.position.y - rangeY);
		Vector2 toPos = new Vector2(transform.position.x + rangeX, transform.position.y + rangeY);
		var hits = Physics2D.OverlapAreaAll(fromPos, toPos);

		//var hits = Physics2D.OverlapCircleAll(transform.position, 1.7f);

		foreach (var item in hits)
		{
			Room otherRoom = item.GetComponent<Room>();
			if (otherRoom != null && otherRoom != this)
			{
				neighbours.Add(otherRoom);
			}
		}
	}

}