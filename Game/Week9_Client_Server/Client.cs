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
	float speedx = 3f;
	float posy = 3f;
	Vector3 startPos;
	Vector3 endPos;
	int numPlayers = 0;

	// Use this for initialization
	void Start ()
	{
		posy = UnityEngine.Random.Range (-3f, 3f);
		me.transform.position = new Vector3 (me.transform.position.x, posy, me.transform.position.z);
		startPos = endPos = me.transform.position;

		listOthers = new LinkedList<GameObject> ();
		listOtherNames = new LinkedList<String> ();

		player = new Player ();
		player.n = "pok1";
		player.a = 1;

		print ("Connection");

		client = new TcpClient ("127.0.0.1", 16000);

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
	void FixedUpdate ()
	{
		try {

			if (me.transform.position.x < -5 || me.transform.position.x > 5) {
				speedx *= -1;
			}

			me.transform.Translate (speedx * Time.deltaTime, 0, 0);
			player.x = me.transform.position.x;
			player.y = me.transform.position.y;

			if (s % 300 == 0){

				//player.x = UnityEngine.Random.Range(-4f, 4f);
				s = 0;
				if (stream.CanWrite) {
					writer.WriteLine (JsonUtility.ToJson (player));
					writer.Flush ();
				}


			} else {
				s++;
			}

			if (stream.CanRead) {
				string input = reader.ReadLine ();
				reader.DiscardBufferedData();
				//Debug.Log (input);
				Player p = JsonUtility.FromJson<Player> (input);
				manageOtherPlayers (p);
			}
		} catch (Exception e) {
			Debug.Log (e.ToString ());
		}
	}

	void manageOtherPlayers (Player p)
	{
		Debug.Log (p.n + " " + p.a);

		if (!listOtherNames.Contains (p.n)) { //New comming
			listOtherNames.AddLast (p.n);
			GameObject newGo = (GameObject)Instantiate (go, new Vector3 (p.x, p.y, p.z), Quaternion.identity);
			newGo.name = p.n;
			listOthers.AddLast (newGo);
		} else { //Update their status 
			foreach (GameObject g in listOthers) {
				if (g.name.Equals (p.n)) 
				{
					Vector3 gPosition = new Vector3 (Mathf.Round (p.x), p.y, p.z);
					g.transform.position = gPosition;
					//endPos = new Vector3 (Mathf.Round(p.x), p.y, p.z);
					//g.transform.position = endPos; //Vector3.Lerp(startPos, endPos, perc);
				} else {
					//Destroy (g);
				}
			}
		}
	}

	void OnGUI ()
	{
		if (listOthers.Count > 0) {
			foreach (GameObject g in listOthers) {
				Vector3 position = Camera.main.WorldToScreenPoint (g.transform.position);
				Vector2 textSize = GUI.skin.label.CalcSize (new GUIContent (g.name));
				GUI.Label (new Rect (position.x, Screen.height - (position.y + 40f), textSize.x, textSize.y), g.name);
			}
		}
	}

	void OnApplicationQuit()
	{
		try
		{
			client.Close();
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
	}
}

public class Player
{

	public float x, y, z;
	public string n;
	public int a;

}
