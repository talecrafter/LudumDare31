using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CraftingLegends.Framework;

public class InventoryDisplay : MonoBehaviour {

	public Image weaponDisplay;
	public Animator weaponDisplayAnimator;

	private Animator _animator;

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void SetWeapon(Weapon weapon)
	{
		weaponDisplay.sprite = weapon.inventoryImage;
		weaponDisplayAnimator.SetTrigger("switch");
	}

	public void Show()
	{
		_animator.SetBool("show", true);
	}

	public void Hide()
	{
		_animator.SetBool("show", false);
	}

}