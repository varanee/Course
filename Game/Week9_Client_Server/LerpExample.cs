using UnityEngine;
using System.Collections;

public class LerpExample : MonoBehaviour {
	//float lerpTime = 1f;
	float currentLerpTime;
	float moveDistance = 5f;

	Vector3 startPos;
	Vector3 endPos;
	public static Vector3 POS;
	float speed = 5f;

	protected void Start() {
		startPos = transform.position;
		endPos = transform.position + transform.up * moveDistance;
	}

	protected void Update() {
		//reset when we press spacebar
		/*if (Input.GetKeyDown(KeyCode.Space)) {
			currentLerpTime = 0f;
		}

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!
		float perc = currentLerpTime / lerpTime;
		transform.position = Vector3.Lerp(startPos, endPos, perc);
		*/
		if(Input.GetKey(KeyCode.W)){
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D)){
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S)){
			transform.Translate (Vector3.down * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A)){
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		POS = transform.position;
	}
}
