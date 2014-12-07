using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class RandomImages : MonoBehaviour {

	public SpriteRenderer headImage;
	public SpriteRenderer torsoImage;

	public List<Sprite> heads;
	public List<Sprite> torsos;

    void Awake() {
		headImage.sprite = heads.PickRandom();
		torsoImage.sprite = torsos.PickRandom();
    }

    
}