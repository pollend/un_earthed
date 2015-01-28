using UnityEngine;
using System.Collections;

public class CameraMotion : MonoBehaviour {

	public GameObject player;

//	void FixedUpdate () {

//		transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, Quaternion.Angle(transform.rotation, player.transform.rotation) * 1f);
//
//		Vector3 move = player.transform.position - transform.position;
//		move += transform.up * .8f;
//		move.z = 0;
//		transform.position += move * 1f;
//	}

//	void LateUpdate() {
//		Vector3 newPosition = Input.GetAxis("Vertical") * transform.up * 50 * Time.deltaTime;
//		newPosition.z = -10;
//		transform.localPosition = newPosition;
//	}
}
