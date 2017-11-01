using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;
using System.Threading;

public class Main : MonoBehaviour
{
	public GameObject go;
	public GameObject me;
	Queue<string> buffer;
	LinkedList<GameObject> listOthers;
	LinkedList<string> listOtherNames;
	StreamWriter writer;
	StreamReader reader;
	NetworkStream stream;
	Player player;
	TcpClient client;

	// Use this for initialization
	void Start ()
	{
		buffer = new Queue<string> ();
		listOthers = new LinkedList<GameObject> ();
		listOtherNames = new LinkedList<String> ();

		player = new Player ();
		player.n = "pok1";
		player.a = 1;

		client = new TcpClient ("127.0.0.1", 16000);
		//client = new TcpClient ("54.254.188.162", 16000); //AWS

		stream = client.GetStream ();
		stream.ReadTimeout = 10;
		if (stream.CanRead) {
			writer = new StreamWriter (stream);
			reader = new StreamReader (stream);
			Debug.Log (reader.ReadLine ());
		}
		if (stream.CanWrite) {
			Debug.Log ("send data " + player.n);
			writer.Write (player.n);
			writer.Flush ();
		}


		Thread incomeThread = new Thread (new ThreadStart (incomeMsg));
		incomeThread.Start ();

		//player.y = UnityEngine.Random.Range (-4f, 4f);
		float yPos = UnityEngine.Random.Range (-4f, 4f);
		me.transform.position = new Vector3 (0, yPos, 0);
	}

	//bool isRecvMsg = true;
	void incomeMsg ()
	{
		while (true) {
			stream = client.GetStream ();
			if (stream.DataAvailable) {
				string input = reader.ReadLine ();
				Debug.Log ("input in thread " + input);
				lock (buffer) {
					buffer.Enqueue (input);
				}
			}
		}
	}


	//60 fps
	float sx = 3f;

	void Update ()
	{
		try {
			
			//player.x = Mathf.Round(Ship.POS.x);
			//player.y = Mathf.Round(Ship.POS.y);

			if (me.transform.position.x < -7f || me.transform.position.x > 7f)
				sx = -sx;

			me.transform.position += new Vector3 (sx * Time.deltaTime, 0, 0);
			player.x = Mathf.Round(me.transform.position.x);
				player.y = Mathf.Round(me.transform.position.y);

			//if (Ship.isMoving) 
			{
				if (stream.CanWrite) {
					writer.WriteLine (JsonUtility.ToJson (player));
					writer.Flush ();
				}
			}

			lock (buffer) {
				while (buffer.Count > 0) {
					string inMsg = buffer.Dequeue ();
					Debug.Log ("inMsg = " + inMsg);
					Player p = JsonUtility.FromJson<Player> (inMsg);
					manageOtherPlayers (p);
				}
			}
			/* old
			 * if (stream.CanRead) 
			{
				string input = reader.ReadLine ();
				buffer.Enqueue(input);
				Player p = JsonUtility.FromJson<Player> (buffer.Dequeue());
				manageOtherPlayers (p);
			}*/
		} catch (Exception e) {
			Debug.Log (e.ToString ());
		}
	}

	Vector3 sPos;
	Vector3 ePos;
	float lerpTime = 1f;
	float currentLerpTime = 0f;

	void manageOtherPlayers (Player p)
	{
		Debug.Log (p.n + " " + p.a);
		if (!listOtherNames.Contains (p.n)) {
			
			GameObject newGo = 
				(GameObject)Instantiate (
					go, 
					new Vector3 (p.x, p.y, 0f), 
					Quaternion.identity);
			newGo.name = p.n;
			listOthers.AddLast (newGo);
			listOtherNames.AddLast (p.n);

		} else { 
			
			GameObject e = GameObject.Find (p.n);
			Debug.Log ("p.a = " + p.a);
			if (p.a == 1) {
				//float dist = Vector3.Distance (e.transform.position, new Vector3 (p.x, p.y, 0f));
				//Debug.Log ("dist = " + dist);
				//Debug.Log (Time.time);

				//currentLerpTime += 0.1f;
				e.transform.position = Vector3.Lerp
				(
					e.transform.position, 
					new Vector3 (p.x, p.y, 0f),
					0.1f
				);

				//if (currentLerpTime > lerpTime)
				//	currentLerpTime = 0f;
				
				//e.transform.position = new Vector3 (p.x, p.y, 0f);
	
			} else {
				Debug.Log ("p.a = " + p.a);
				DestroyObject (e);
				listOthers.Remove (e);
				listOtherNames.Remove (e.name);
				Debug.Log ("destroy" + e.name);
			}
		}
	}

	void OnGUI ()
	{
		if (listOthers.Count > 0) {
			foreach (GameObject g in listOthers) {
				Debug.Log ("Debug " + g.name);
				Vector3 position = Camera.main.WorldToScreenPoint (g.transform.position);
				Vector2 textSize = GUI.skin.label.CalcSize (new GUIContent (g.name));
				GUI.Label (new Rect (position.x, Screen.height - (position.y + 60f), textSize.x, textSize.y), g.name);
			}
		}
	}

	void OnApplicationQuit ()
	{
		try {
			client.Close ();
			stream.Close ();
		} catch (Exception e) {
			Debug.Log (e.Message);
		}
	}
}

public class Player
{
	public float x, y;
	public string n;
	public int a;
}
