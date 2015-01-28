using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Global {

	static int size = 27;
	public static GameObject[,] unbrokenBlocks = new GameObject[size, size];
	static bool[,] unbrokenChecked = new bool[size, size];
	static List<GameObject> island = new List<GameObject>();

	public static void CheckIslands(int x, int y) {
		// RESET CHECKED
//		for (int x = 0; x < unbrokenChecked.GetLength(0); x++) {
//			for (int y = 0; y < unbrokenChecked.GetLength(1); y++) {
//				unbrokenChecked[x,y] = false;
//			}
//		}

		// CHECK FOR ISLANDS
		if (!IsOnWall(x-1,y)) {
			if (unbrokenBlocks[x-1, y] != null) {
				Debug.Log("Left");
				for (int i = x-1; i > 0; i--) {
					if (unbrokenBlocks[i, y] == null) {
						Debug.Log("Theres an empty on the left");
					}
				}
			}
		}
		if (x < size-1)
			if (unbrokenBlocks[x+1, y] != null)
				Debug.Log("Right");
		if (y > 0)
			if (unbrokenBlocks[x, y-1] != null)
				Debug.Log("Behind");
		if (y < size-1)
			if (unbrokenBlocks[x, y+1] != null)
				Debug.Log("Front");
	}

	static void CheckNeighbors (int x, int y) {
//		unbrokenChecked[x,y] = true;
//		island.Add(unbrokenBlocks[x,y]);
	}

	static bool IsOnWall (int x, int y) {
		if (x <= 0 || x >= size-1 || y <= 0 || y >= size-1) {
			return true;
		}
		return false;
	}
}
