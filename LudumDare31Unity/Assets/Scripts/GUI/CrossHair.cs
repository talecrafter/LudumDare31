using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CraftingLegends.Core;

public class CrossHair : MonoBehaviour
{
	private Image _image;

	void Awake()
	{
		_image = GetComponent<Image>();

		Screen.showCursor = false;
		Hide();
	}

	void Update()
	{
		transform.position = Input.mousePosition;
	}

	public void Show()
	{
		_image.enabled = true;
	}

	public void Hide()
	{
		_image.enabled = false;
	}

	public Vector3 worldPosition
	{
		get
		{
			return Camera.main.ScreenToWorldPoint(Utilities.MouseToScreenCoords());
		}
	}

}