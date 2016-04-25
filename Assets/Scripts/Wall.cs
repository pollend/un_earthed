using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour {

	BoxCollider2D[] walls;

	void OnTriggerExit2D(Collider2D col) {
		StartCoroutine(TurnSolid());
	}

	IEnumerator TurnSolid() {
		yield return new WaitForSeconds(.2f);
//		Camera.main.backgroundColor = new Color(.1f,.1f,.0f);
		walls = gameObject.GetComponents<BoxCollider2D>();
		for (int i = 0; i < walls.Length; i++)
			walls[i].isTrigger = false;
	}
}
