using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class RandomMonsterImages : MonoBehaviour {

	public SpriteRenderer headImage;
	public SpriteRenderer armImage;

	public List<Sprite> heads;
	public List<Sprite> arms;

    void Awake() {
		headImage.sprite = heads.PickRandom();
		armImage.sprite = arms.PickRandom();
    }

    
}