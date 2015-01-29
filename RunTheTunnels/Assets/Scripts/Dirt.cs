using UnityEngine;
using System.Collections;

public class Dirt : MonoBehaviour {
	
	float size;
	float darkness;

	bool breakable = true;

	void Awake() {
		size = 1;
		darkness = 0;
	}

	public void SetAttributes (float _size, float _darkness) {
		SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();
		darkness = _darkness;
		size = _size;

		transform.localScale = new Vector3(size, size, 1);
		spriteRender.color = new Color((111/255f)-(15*darkness/255f), (103/255f)-(15*darkness/255f), (93/255f)-(15*darkness/255f));
	}

	public void Break() {
		/*if (breakable) {
			if (size >= 0.25f) {
				if (size == 1) {
					int x = (int)transform.position.x;
					int y = (int)transform.position.y;
					Global.unbrokenBlocks[x, y] = null;
					Global.CheckIslands(x, y);
				}
				for (int x = 0; x < 2; x++) {
					for (int y = 0; y < 2; y++) {
						//GameObject newDirt = DirtPool.PopPool(new Vector3((transform.position.x-(size/4f))+(x*(size/2f)), (transform.position.y-(size/4f))+(y*(size/2f)), 0));
						//newDirt.GetComponent<Dirt>().SetAttributes(size/2f, darkness+1);
					}
				}
			}

			//renderer.enabled = false;
			///collider2D.enabled = false;
			///SetAttributes(1,0);
			DirtPool.PushPool(gameObject);
		}*/
	}

	public void SetUnbreakable() {
		breakable = false;
		SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();
		spriteRender.color = new Color(.7f,.3f,.3f);
	}
}
