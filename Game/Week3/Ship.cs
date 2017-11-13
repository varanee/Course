using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Ship : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	public Rigidbody2D projectile;
	float speed = 5.0f;
	// Update is called once per frame
	void Update ()
	{
		//1/60 = 0.0167 s
		if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0, 0); //x y z
			GetComponent<SpriteRenderer>().flipX = false;
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.position -= new Vector3 (speed * Time.deltaTime, 0, 0); //x y z
			GetComponent<SpriteRenderer>().flipX = true;

		}

		if (Input.GetKey (KeyCode.W)) {
			transform.position += new Vector3 (0, speed * Time.deltaTime, 0); //x y z
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.position -= new Vector3 (0, speed * Time.deltaTime, 0); //x y z
		}

		//Rotation turn left
		if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate (0, 0, 40f * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.E)) {
			transform.Rotate (0, 0, -40f * Time.deltaTime);
		}

		//Double size (x,y)
		if (Input.GetKey (KeyCode.Z)) {
			transform.localScale = new Vector3(2f, 2f,0);   
		}

		//Back to normal size (x,y) KeyCode.C
		if (Input.GetKey (KeyCode.C)) {
			transform.localScale = new Vector3(1f, 1f,0);   
		}

		Vector3 mouse = Input.mousePosition;
		Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
			mouse.x, 
			mouse.y,
			0));

		Vector2 bulletDir = new Vector2(mouseWorld.x,mouseWorld.y) - 
			new Vector2(this.transform.position.x,this.transform.position.y);

	
		float angle = Mathf.Atan2(bulletDir.y,bulletDir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);


		if (Input.GetButtonDown("Fire1")) {
			Rigidbody2D clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody2D;
			clone.velocity = bulletDir;
		}



		//checkCollision ();
	}



	void checkCollision(){

		GameObject[] itemObjs = GameObject.FindGameObjectsWithTag ("item");
		for (int i = 0; i < itemObjs.Length; i++) {
			Assert.IsNotNull (itemObjs [i]);
			if (itemObjs [i] != null) {
				if (GetComponent<SpriteRenderer> ().bounds.Intersects (
					itemObjs [i].GetComponent<SpriteRenderer> ().bounds)) {
					Destroy (itemObjs [i]);
				}
			}
		}
	}



	void OnCollisionEnter2D(Collision2D col)
	{
		/*foreach (ContactPoint2D contact in col.contacts) {
			Debug.Log (contact);
			Debug.DrawLine(contact.point, contact.point + contact.normal, Color.green, 2, false);
		}*/

		if (col.gameObject.tag == "item") {
			Destroy (col.gameObject);
		}
	}

	/*void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 2);
	}*/

}
