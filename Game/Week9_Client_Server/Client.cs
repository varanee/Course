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
	string id;
	// Use this for initialization
	void Start () {

		listOthers = new LinkedList<GameObject> ();
		listOtherNames = new LinkedList<String> ();

		player = new Player ();
		player.name = "ekarat";
		player.isActive = true;

		print ("Connection");
		client = new TcpClient ("192.168.1.42", 16000);

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

		//Send data every 1 second
		if (s % 60 == 0) 
		{
			player.x = UnityEngine.Random.Range(-4f, 4f);
		
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
				if (!p.name.Equals ("ekarat")) 
				{
					manageOtherPlayers (p);
				}
			}				
			s = 0;
		}
		s++;
	}

	void manageOtherPlayers(Player p)
	{
		Debug.Log (p.name + " " + p.isActive);

		if (!listOtherNames.Contains (p.name)) //New comming
		{ 
			listOtherNames.AddLast(p.name);
			GameObject newGo = (GameObject)Instantiate(
				go, 
				new Vector3 (p.x, p.y, p.z), 
				Quaternion.identity);
			newGo.name = p.name;
			listOthers.AddLast (newGo);
		} 
		else //Update their status 
		{
			foreach (GameObject g in listOthers) 
			{
				if(g.name.Equals(p.name))
				{
					Vector3 gPosition = new Vector3 (p.x, p.y, p.z);
					g.transform.position = gPosition;
				}
			}
		}
	}

	void OnGUI() {
		foreach (GameObject g in listOthers) {
			Vector3 position = Camera.main.WorldToScreenPoint (g.transform.position);
			Vector2 textSize = GUI.skin.label.CalcSize (new GUIContent (g.name));
			GUI.Label (new Rect (position.x, Screen.height - (position.y+ 40f), textSize.x, textSize.y), g.name);
		}
	}
}

public class Player{

	public float x,y,z;
	public string name;
	public bool isActive;

}
	
