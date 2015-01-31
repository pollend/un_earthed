using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.QuadTree;

public class Player : MonoBehaviour {

	// NETWORK VARIABLES
//	float lastSynchronizationTime = 0f;
//	float syncOffset = 1f;
//	float syncDelay = 0f;
//	Vector3 syncStartPosition = Vector3.zero;
//	Vector3 syncEndPosition = Vector3.zero;

	// LOCAL VARIABLES
	float moveSpeed = 3.4f;
	float backMoveSpeed = 1.6f;
	int rotateSpeed = 300;
	float digSpeed = .01f;

	float rotateDestination = 0f;
	bool finishRotating = false;

	bool digging = false;
	bool routineInProgress = false;
	float lastTrigger = 0;

	List<Dirt> triggeredBlocks = new List<Dirt>();

	void Start() {
		InvokeRepeating("BreakBlocks", 0f, digSpeed);
//		if (!networkView.isMine) {
//			gameObject.GetComponent<CircleCollider2D>().enabled = false;
//		}
	}

	void Update() {
//		if (networkView.isMine) {
			if (Input.GetKey(KeyCode.A)) {
				finishRotating = false;
				transform.RotateAround(transform.position, Vector3.forward, rotateSpeed*Time.deltaTime);
			} else if (Input.GetKeyUp(KeyCode.A)) {
				finishRotating = true;
				float currentAngle = transform.rotation.eulerAngles.z;
				float angleDifference = 45f - (currentAngle % 45f);
				rotateDestination = currentAngle + angleDifference;
			}
			if (Input.GetKey(KeyCode.D)) {
				finishRotating = false;
				transform.RotateAround(transform.position, Vector3.forward, -rotateSpeed*Time.deltaTime);
			}  else if (Input.GetKeyUp(KeyCode.D)) {
				finishRotating = true;
				float currentAngle = transform.rotation.eulerAngles.z;
				float angleDifference = currentAngle % 45f;
				rotateDestination = currentAngle - angleDifference;
			}
			if (finishRotating) {
				Quaternion destAngle = Quaternion.AngleAxis(rotateDestination, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, destAngle, rotateSpeed*Time.deltaTime);
			}
	
//		} else {
//			syncOffset += Time.deltaTime;
//			transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncOffset / syncDelay);
//		}
	}

	void FixedUpdate () {
//		if (networkView.isMine) {
			if (Input.GetAxis("Vertical") >= 0) {
				rigidbody2D.velocity = (Input.GetAxis("Vertical") * transform.up) * moveSpeed;
			} else {
				rigidbody2D.velocity = (Input.GetAxis("Vertical") * transform.up) * backMoveSpeed;
			}
		GameObject.Find("Chunk").GetComponent<QuadTree>().GetBucket().Divide(this.transform.position,.8f,typeof(Bucket_empty));
//		}
	}

	void BreakBlocks() {
//		if ((Input.GetKey(KeyCode.W) && digging) || !networkView.isMine) {
		if (Input.GetKey(KeyCode.W) && digging) {

            
			for (int i = 0; i < triggeredBlocks.Count; i++) {
				triggeredBlocks[i].Break();
			}
			triggeredBlocks.Clear();
		}
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		if (!routineInProgress && !digging) {
			routineInProgress = true;
			StartCoroutine(StartDigging());
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "dirt") {
			if (Time.time - lastTrigger > .4f)
				digging = false;
			lastTrigger = Time.time;

		
			triggeredBlocks.Add(col.gameObject.GetComponent<Dirt>());
		}
	}
	void OnTriggerExit2D (Collider2D col) {

		if (col.gameObject.tag == "dirt") {
			Dirt tempDirt = col.gameObject.GetComponent<Dirt>();
			if (triggeredBlocks.Contains(tempDirt))
				triggeredBlocks.Remove(tempDirt);
		}

	}

	IEnumerator StartDigging() {
		yield return new WaitForSeconds(.3f);
		routineInProgress = false;
		digging = true;
	}

	// NETWORK

//	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) {
//		Vector3 tempPosition = Vector3.zero;
//		Quaternion tempRotation = Quaternion.identity;
//
//		if (stream.isWriting) {
//			tempPosition = transform.position;
//			tempRotation = transform.rotation;
//			stream.Serialize(ref tempPosition);
//			stream.Serialize(ref tempRotation);
//		} else {
//			stream.Serialize(ref syncEndPosition);
//			stream.Serialize(ref tempRotation);
//			transform.rotation = tempRotation;
//			syncStartPosition = transform.position;
//			syncOffset = 0f;
//			syncDelay = Time.time - lastSynchronizationTime;
//			lastSynchronizationTime = Time.time;			
//		}
//	}
}
