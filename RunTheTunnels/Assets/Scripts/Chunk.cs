using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

	public GameObject dirtPrefab;
	int halfDistance;
	int wait = 0;

	void Start() {
		halfDistance = (int) (transform.localScale.x / 2f - 0.5f);
		StartCoroutine(Dechunk());
	}

	IEnumerator Dechunk() {
		for (int x = (int)transform.position.x - halfDistance; x <= (int)transform.position.x + halfDistance; x++) {
			for (int y = (int)transform.position.y - halfDistance; y <= (int)transform.position.y + halfDistance; y++) {
				GameObject dirt = Instantiate(dirtPrefab, new Vector3(x,y,0), Quaternion.identity) as GameObject;
				Global.unbrokenBlocks[x, y] = dirt;
				wait++;
				if (wait >= 5) {
					yield return new WaitForEndOfFrame();
					wait = 0;
				}
			}
		}
		gameObject.SetActive(false);

	}
}
