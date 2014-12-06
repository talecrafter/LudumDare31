﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CraftingLegends.Framework
{
	/// <summary>
	/// editor script for visualizing camera bounds with 2D / orthographic camera
	/// </summary>
	public class DrawCameraBounds2D : MonoBehaviour
	{
		// ================================================================================
		//  public
		// --------------------------------------------------------------------------------

		public bool drawRectangle = true;
		public bool drawHorizontalLines;
		public bool drawVerticalLines;

		public Color lineColor = Color.red;

		// ================================================================================
		//  private
		// --------------------------------------------------------------------------------

		private float longDistance = 10000.0f;

		// ================================================================================
		//  unity editor methods
		// --------------------------------------------------------------------------------

		void OnDrawGizmos()
		{
			Transform _transform = transform;
			Gizmos.color = lineColor;

			float vDistance = Camera.main.orthographicSize;	// orthographic size is half of camera height
			float hDistance = Camera.main.aspect * Camera.main.orthographicSize;

			// horizontal lines
			if (drawHorizontalLines || drawRectangle)
			{
				float h = longDistance;
				if (!drawHorizontalLines)
				{
					h = hDistance;
				}

				// draw bottom line
				Vector3 leftPos = new Vector3(_transform.position.x - h, _transform.position.y - vDistance, -1.0f);
				Vector3 rightPos = new Vector3(_transform.position.x + h, _transform.position.y - vDistance, -1.0f);
				Gizmos.DrawLine(leftPos, rightPos);

				// draw top line
				leftPos = new Vector3(_transform.position.x - h, _transform.position.y + vDistance, -1.0f);
				rightPos = new Vector3(_transform.position.x + h, _transform.position.y + vDistance, -1.0f);
				Gizmos.DrawLine(leftPos, rightPos);
			}

			// vertical lines
			if (drawVerticalLines || drawRectangle)
			{
				float v = longDistance;
				if (!drawVerticalLines)
				{
					v = vDistance;
				}

				// draw left line
				Vector3 leftPos = new Vector3(_transform.position.x - hDistance, _transform.position.y - v, -1.0f);
				Vector3 rightPos = new Vector3(_transform.position.x - hDistance, _transform.position.y + v, -1.0f);
				Gizmos.DrawLine(leftPos, rightPos);

				// draw right line
				leftPos = new Vector3(_transform.position.x + hDistance, _transform.position.y - v, -1.0f);
				rightPos = new Vector3(_transform.position.x + hDistance, _transform.position.y + v, -1.0f);
				Gizmos.DrawLine(leftPos, rightPos);
			}
		}
	}
}
