using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public static Vector3 POS;
	float speed = 5f;
	public static bool isMoving = false;
	protected void Start() {
	}

	protected void Update() {
		if (Input.anyKey) {
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector3.up * speed * Time.deltaTime);
			} 

			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} 

			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector3.down * speed * Time.deltaTime);
			} 

			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector3.left * speed * Time.deltaTime);
			}
			isMoving = true;
		} else {
			isMoving = false;
		}

		Debug.Log ("Is moving = "+isMoving);
		POS = transform.position;
	}
}
