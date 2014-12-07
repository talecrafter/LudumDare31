using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

public class HitReceiver : MonoBehaviour {

	public Actor actor;

    void Awake() {
		actor = GetComponentInParent<Actor>();
    }

}