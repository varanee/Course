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
	LinkedList<GameObject> listOthers;
	LinkedList<string> listOtherNames;
	StreamWriter writer;
	StreamReader reader;
	NetworkStream stream;
	Player player;
	TcpClient client;

	// Use this for initialization
	void Start () {

		listOthers = new LinkedList<GameObject> ();
		listOtherNames = new LinkedList<String> ();

		player = new Player ();
		player.n = "e";
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
	void Update(){
		try
		{
			

			if (stream.CanWrite) 
			{
				writer.WriteLine (JsonUtility.ToJson (player));
				writer.Flush ();
			}

			if (stream.CanRead) 
			{
				//string[] splits;
				//string[] output;
				string input = reader.ReadLine ();
				Debug.Log (input);
				/*if (input.Contains ("false")) {
					splits = input.Split ('}');
					int len = splits.Length - 1;
					output = new string[len];
					for (int i=0; i<len; i++) {
						if (splits [i].Contains ("false")) {
							output [i] = splits [i] + "}";
							Debug.Log ("output " + output [i]);
						}
					}
				}*/


				Player p = JsonUtility.FromJson<Player>(input);
				manageOtherPlayers (p);

			}

			player.x = UnityEngine.Random.Range(-4f, 4f);

		}catch(Exception e){
			Debug.Log (e.ToString ());
		}
	}

	void manageOtherPlayers(Player p)
	{
		Debug.Log (p.n + " " + p.a);

		if (!listOtherNames.Contains (p.n)) //New comming
		{ 
			listOtherNames.AddLast(p.n);
			GameObject newGo = (GameObject)Instantiate(go, new Vector3 (p.x, p.y, p.z), Quaternion.identity);
			newGo.name = p.n;
			listOthers.AddLast (newGo);
		} 
		else //Update their status 
		{
			foreach (GameObject g in listOthers) 
			{
				if(g.name.Equals(p.n))
				{
					Vector3 gPosition = new Vector3 (Mathf.Round(p.x), p.y, p.z);
					g.transform.position = gPosition;
				}
			}
		}
	}

	void OnGUI() {
		if (listOthers.Count > 0) {
			foreach (GameObject g in listOthers) {
				Vector3 position = Camera.main.WorldToScreenPoint (g.transform.position);
				Vector2 textSize = GUI.skin.label.CalcSize (new GUIContent (g.name));
				GUI.Label (new Rect (position.x, Screen.height - (position.y + 40f), textSize.x, textSize.y), g.name);
			}
		}
	}
}

public class Player{

	public float x,y,z;
	public string n;
	public int a;

}
	
