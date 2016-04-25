using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DirtPool : MonoBehaviour {
	public GameObject dirtObj;
	public static Stack<GameObject> dirtPool = new Stack<GameObject>();

	float poolSize = 2000;
	int wait = 0;

	void Start () {
		StartCoroutine(CreatePool());
	}

	IEnumerator CreatePool() {
		for (int i = 0; i < poolSize; i++) {
			GameObject dirt = Instantiate(dirtObj, transform.position, transform.rotation) as GameObject;
			dirt.GetComponent<Renderer>().enabled = false;
			dirt.GetComponent<Collider2D>().enabled = false;
			PushPool(dirt);
			wait++;
			if (wait >= 5) {
				yield return new WaitForEndOfFrame();
				wait = 0;
			}
		}
	}

	public static GameObject PopPool(Vector3 position) {
		if (dirtPool.Count > 0) {
			GameObject dirt = dirtPool.Pop();
			dirt.transform.position = position;
			dirt.GetComponent<Renderer>().enabled = true;
			dirt.GetComponent<Collider2D>().enabled = true;
			return dirt;
		} else {
			Debug.Log("The pool is empty..");
		}
		return new GameObject("empty");
	}

	public static void PushPool(GameObject dirt) {
		dirtPool.Push(dirt);
	}
}
