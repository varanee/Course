using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;

public class Main : MonoBehaviour
{
	StreamWriter writer;
	StreamReader reader;
	NetworkStream stream;
	Player player;
	TcpClient client;
	string id;
	// Use this for initialization
	void Start () {
		player = new Player ();
		player.name = "ekarat";

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

		//Send data every 1 second
		if (s % 60 == 0) {
			player.x = 1.5f;
		
			if (stream.CanWrite) {
				writer.WriteLine (JsonUtility.ToJson (player));
				writer.Flush ();
			}
			if (stream.CanRead) {
				string input = reader.ReadLine ();
				Debug.Log ("input "+input);
				Player p = JsonUtility.FromJson<Player>(input);
				Debug.Log (p.name);
			}				

			s = 0;
		}
		s++;
	}
}

public class Player{

	public float x,y,z;
	public string name;

}
