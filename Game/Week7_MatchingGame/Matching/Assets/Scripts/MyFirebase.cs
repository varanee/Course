using Firebase;
using Firebase.Unity.Editor;
using UnityEngine;

public class MyFirebase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://matchinggame-ca597.firebaseio.com");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
