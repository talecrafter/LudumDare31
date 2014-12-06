﻿using UnityEngine;
using System.Collections;
using CraftingLegends.Core;

namespace CraftingLegends.Framework
{
	public enum MoveDirection
	{
		HorizontalAndVertical,
		Horizontal,
		Vertical
	}

	public class CameraSmoothFollow2D : MonoBehaviour
	{
		private const float TARGET_REACHED_DISTANCE = 0.8f;

		// optional target
		public Transform target;

		[SerializeField]
		public MoveDirection direction = MoveDirection.HorizontalAndVertical;

		// speed when following target
		public float horizontalFollowSpeed = 2.0f;
		public float verticalFollowSpeed = 2.0f;

		public float smoothEdge = 2.0f;

		public float minimumOrthoGraphicSize = 1.0f;

		private bool _canMoveUp = false;
		public bool canMoveUp { get { return _canMoveUp; } }

		private bool _canMoveDown = false;
		public bool canMoveDown { get { return _canMoveDown; } }

		private bool _canMoveLeft = false;
		public bool canMoveLeft { get { return _canMoveLeft; } }

		private bool _canMoveRight = false;
		public bool canMoveRight { get { return _canMoveRight; } }

		public bool targetIsReached
		{
			get
			{
				if (target == null)
					return true;

				if (Vector2.Distance(_transform.position, target.position) < TARGET_REACHED_DISTANCE)
					return true;

				return false;
			}
		}

		// ================================================================================
		//  private
		// --------------------------------------------------------------------------------

		// level boundaries
		private bool _checkForBounds = false;
		private Rect _bounds;
		private Rect _innerBounds;

		protected Camera _camera;
		protected Transform _transform;

		// ================================================================================
		//  unity methods
		// --------------------------------------------------------------------------------

		protected void Awake()
		{
			Init();
		}

		protected void OnLevelWasLoaded(int levelIndex)
		{
			Init();
		}

		protected void LateUpdate()
		{
			if (target)
				FollowTarget();
		}

		// ================================================================================
		//  public methods
		// --------------------------------------------------------------------------------

		public void Zoom(float delta)
		{
			_camera.orthographicSize += delta;

			ApplyBounds();
		}

		public void SetOrthgraphicSizeAtPivot(float size, Vector2 pivot, Vector2 directionToCamera, float oldSize)
		{
			if (size < minimumOrthoGraphicSize && _camera.orthographicSize <= minimumOrthoGraphicSize)
				return;

			if (size < minimumOrthoGraphicSize)
				size = minimumOrthoGraphicSize;

			_camera.orthographicSize = size;

			float scale = size / oldSize;

			Vector3 newPosition = pivot + directionToCamera * scale;
			_camera.transform.position = new Vector3(newPosition.x, newPosition.y, _camera.transform.position.z);

			ApplyBounds();
		}

		public void Translate(Vector2 delta)
		{
			float d = Screen.height / (_camera.orthographicSize * 2.0f);
			float y = delta.y / d;
			float x = delta.x / d;

			_transform.Translate(x, y, 0);

			ApplyBounds();
		}

		public void SetPosition(Vector2 pos)
		{
			_transform.position = new Vector3(pos.x, pos.y, _transform.position.z);

			ApplyBounds();
		}

		public void SetBoundaries(LevelBoundaries levelBoundaries)
		{
			if (levelBoundaries != null)
			{
				_bounds = levelBoundaries.levelBounds;
				_innerBounds = new Rect(_bounds.x + smoothEdge, _bounds.y + smoothEdge, _bounds.width - 2 * smoothEdge, _bounds.height - 2 * smoothEdge);

				_checkForBounds = true;

				ApplyBounds();
			}
			else
			{
				_checkForBounds = false;
			}
		}

		public void SetOrthographicSize(float size)
		{
			_camera.orthographicSize = size;

			ApplyBounds();
		}

		// ================================================================================
		//  private methods
		// --------------------------------------------------------------------------------

		private void Init()
		{
			_camera = Camera.main;
			_transform = _camera.transform;

			LevelBoundaries levelBoundaries = FindObjectOfType<LevelBoundaries>();

			SetBoundaries(levelBoundaries);
		}

		protected void FollowTarget()
		{
			Rect cameraRect = Utilities2D.CameraBounds2D();
			float horizontalScale = 1.0f;
			float verticalScale = 1.0f;

			if (_checkForBounds)
			{
				// left smooth edge
				if (target.position.x < _transform.position.x
					&& cameraRect.x > _bounds.x && cameraRect.x < _innerBounds.x)
				{
					horizontalScale = 1.0f + (cameraRect.x - _innerBounds.x) / smoothEdge;
				}
				// right smooth edge
				else if (target.position.x > _transform.position.x
					&& cameraRect.xMax > _innerBounds.xMax && cameraRect.xMax < _bounds.xMax)
				{
					horizontalScale = 1.0f - (cameraRect.xMax - _innerBounds.xMax) / smoothEdge;
				}
				// hard left edge
				else if (target.position.x < _transform.position.x
					&& cameraRect.x <= _bounds.x)
				{
					horizontalScale = 0;
				}
				// hard right edge
				else if (target.position.x > _transform.position.x
					&& cameraRect.xMax >= _bounds.xMax)
				{
					horizontalScale = 0;
				}

				// bottom edge
				if (target.position.y < _transform.position.y
					&& cameraRect.y > _bounds.y && cameraRect.y < _innerBounds.y)
				{
					verticalScale = 1.0f + (cameraRect.y - _innerBounds.y) / smoothEdge;
				}
				// top edge
				else if (target.position.y > _transform.position.y
					&& cameraRect.yMax > _innerBounds.yMax && cameraRect.yMax < _bounds.yMax)
				{
					verticalScale = 1.0f - (cameraRect.yMax - _innerBounds.yMax) / smoothEdge;
				}
				// hard bottom edge
				else if (target.position.y < _transform.position.y
					&& cameraRect.y <= _bounds.y)
				{
					verticalScale = 0;
				}
				// hard top edge
				else if (target.position.y > _transform.position.y
					&& cameraRect.yMax >= _bounds.yMax)
				{
					verticalScale = 0;
				}
			}

			float targetX = Mathf.Lerp(_transform.position.x, target.position.x, horizontalFollowSpeed * Time.deltaTime * horizontalScale);
			if (direction == MoveDirection.Vertical)
				targetX = _transform.position.x;

			float targetY = Mathf.Lerp(_transform.position.y, target.position.y, verticalFollowSpeed * Time.deltaTime * verticalScale);
			if (direction == MoveDirection.Horizontal)
				targetY = _transform.position.y;

			transform.position = new Vector3(targetX, targetY, transform.position.z);

			ApplyBounds();
		}

		protected void ApplyBounds()
		{
			if (!_checkForBounds)
				return;

			Rect cameraRect = Utilities2D.CameraBounds2D();

			_canMoveRight = true;
			_canMoveLeft = true;
			_canMoveDown = true;
			_canMoveUp = true;

			// scaling

			if (_camera.orthographicSize < minimumOrthoGraphicSize)
			{
				_camera.orthographicSize = minimumOrthoGraphicSize;
				cameraRect = Utilities2D.CameraBounds2D();
			}

			if (cameraRect.width > _bounds.width)
			{
				float resize = _bounds.width / cameraRect.width;
				_camera.orthographicSize *= resize;
				cameraRect = Utilities2D.CameraBounds2D();
			}

			if (cameraRect.height > _bounds.height)
			{
				float resize = _bounds.height / cameraRect.height;
				_camera.orthographicSize *= resize;
				cameraRect = Utilities2D.CameraBounds2D();
			}

			ApplyHardBounds(cameraRect);
		}

		private Rect ApplyHardBounds(Rect cameraRect)
		{
			float errorMargin = _bounds.height / 100.0f;

			if (cameraRect.xMin <= _bounds.xMin - errorMargin)
			{
				_transform.position += new Vector3(_bounds.xMin - cameraRect.xMin, 0, 0);
				cameraRect = Utilities2D.CameraBounds2D();
				_canMoveLeft = false;
			}

			if (cameraRect.yMin <= _bounds.yMin - errorMargin)
			{
				_transform.position += new Vector3(0, _bounds.yMin - cameraRect.yMin, 0);
				cameraRect = Utilities2D.CameraBounds2D();
				_canMoveDown = false;
			}

			if (cameraRect.xMax >= _bounds.xMax + errorMargin)
			{
				_transform.position -= new Vector3(cameraRect.xMax - _bounds.xMax, 0, 0);
				cameraRect = Utilities2D.CameraBounds2D();
				_canMoveRight = false;
			}

			if (cameraRect.yMax >= _bounds.yMax + errorMargin)
			{
				_transform.position -= new Vector3(0, cameraRect.yMax - _bounds.yMax, 0);
				cameraRect = Utilities2D.CameraBounds2D();
				_canMoveUp = false;
			}

			return cameraRect;
		}
	}
}
