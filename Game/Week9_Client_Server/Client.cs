using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;

public class Client : MonoBehaviour
{
	StreamWriter writer;
	StreamReader reader;
	NetworkStream stream;
	string id;
	// Use this for initialization
	void Start () {
		print("Connection");
		TcpClient client = new TcpClient ("127.0.0.1", 16000);
		stream = client.GetStream();
		stream.ReadTimeout = 10;
		//stream.WriteTimeout = 2;
		if (stream.CanRead) {
			writer = new StreamWriter(stream);
			reader = new StreamReader (stream);
			print("Writer created");
			readData ();
		}
	}

	void readData(){
		if (stream.CanRead) {
			Debug.Log(reader.ReadLine ());
			writer.WriteLine("hi");
			writer.Flush ();

		}
	}

}
