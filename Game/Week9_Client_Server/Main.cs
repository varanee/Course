using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;


public class Main : MonoBehaviour
{
	public GameObject go;
	public GameObject me;
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
	}

	//60 fps
	int s = 0;
	void Update ()
	{
		try {
			
			player.x = Mathf.Round(Ship.POS.x);
			player.y = Mathf.Round(Ship.POS.y);

			if(Ship.isMoving){
				if (s % 10 == 0)
				{
					s = 0;
					if (stream.CanWrite) {
						writer.WriteLine (JsonUtility.ToJson (player));
						writer.Flush ();
					}
				} 
				else 
				{
					s++;
				}
			}

			if (stream.CanRead) {
				string input = reader.ReadLine ();
				reader.DiscardBufferedData();
				Player p = JsonUtility.FromJson<Player> (input);
				if(p.a == 0)
					Debug.Log("Exited alerted");
				manageOtherPlayers (p);
			}
		} 
		catch (Exception e) {
			Debug.Log (e.ToString ());
		}
	}

	Vector3 sPos;
	Vector3 ePos;
	float lerpTime = 1f;
	float currentLerpTime = 0f;

	void manageOtherPlayers (Player p)
	{
		//Debug.Log (p.n + " " + p.a);
		if (!listOtherNames.Contains (p.n)) 
		{
			listOtherNames.AddLast (p.n);
			GameObject newGo = 
				(GameObject)Instantiate (
					go, 
					new Vector3 (p.x, p.y, p.z), 
					Quaternion.identity);
			newGo.name = p.n;
			listOthers.AddLast (newGo);
		} 
		else 
		{ 

			GameObject e = GameObject.Find (p.n);
			if (p.a == 1) {
				e.transform.position = 
				Vector3.Lerp
				(
					e.transform.position, 
					new Vector3 (p.x, p.y, p.z),
					0.1f
				);
			} else {
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
				Vector3 position = Camera.main.WorldToScreenPoint (g.transform.position);
				Vector2 textSize = GUI.skin.label.CalcSize (new GUIContent (g.name));
				GUI.Label (new Rect (position.x, Screen.height - (position.y + 60f), textSize.x, textSize.y), g.name);
			}
		}
	}

	/*void OnApplicationQuit()
	{
		try
		{
			client.Close();
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
	}*/
}

public class Player
{
	public float x, y, z;
	public string n;
	public int a;
}
