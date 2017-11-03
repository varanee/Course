using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Graph graph = new Graph();
		Node A = graph.createStartNode("A");
		Node B = graph.createNode("B");
		Node C = graph.createNode("C");

		A.addEdge(B, 1);
		A.addEdge(C, 1);
		B.addEdge(C, 1);
		int[,] am = graph.createAdjacentMatrix();

		string output = "";
		int rows = am.GetLength(0);
		int cols = am.GetLength(0);
		for(int r=0; r<rows; r++)
		{
			for(int c=0; c<cols; c++)
			{
				//Debug.Log(am[r, c]);
				output += am[r, c];
			}
			output += "\n";
		}
		print(output);
	}

	// Update is called once per frame
	void Update () {

	}
}

public class Graph
{
	public Node startNode;
	public List<Node> allNodes = new List<Node>();

	public Node createStartNode(string name)
	{
		startNode = createNode(name);
		return startNode;
	}

	public Node createNode(string name)
	{
		Node n = new Node(name);
		allNodes.Add(n);
		return n;
	}
	//From Graph to AdjacentMatrix
	public int[,] createAdjacentMatrix()
	{
		int[,] am = new int[allNodes.Count, allNodes.Count];

		for(int i=0; i<allNodes.Count; i++)
		{
			Node n1 = allNodes[i];
			for(int j=0; j<allNodes.Count; j++)
			{
				Node n2 = allNodes[j];
				if (n1.name.Equals(n2.name))
				{
					am[i, j] = 0;
					continue;
				}
				bool exist = false;
				int w = -1;
				for(int k=0; k<n1.edges.Count; k++)
				{
					if (n1.edges[k].endNode == n2)
					{
						exist = true;
						w = n1.edges [k].Weight; 
					}
				}

				if (exist) {
					am [i, j] = w;
				} else {
					am [i, j] = 0;
				}
			}
		}
		return am;
	}
}

public class Node
{
	public string name;
	public List<Edge> edges = new List<Edge>();

	public Node(string n)
	{
		name = n;
	}

	public Node addEdge(Node endNode, int w)
	{
		Edge newEdge = new Edge();
		newEdge.startNode = this;
		newEdge.endNode = endNode;
		newEdge.Weight = w;
		if (!edges.Contains (newEdge)) {
			edges.Add (newEdge);
		
			//endNode.addEdge(this, w);
			bool exist = false;
			for (int i = 0; i < endNode.edges.Count; i++) {
				Edge tmp = endNode.edges [i];
				if (tmp.endNode == this) {
					exist = true;
				}
			}

			if (!exist) {
				endNode.addEdge (this, w);
			}

			return this;
		} else {
			return null;
		}
	}
}

public class Edge
{
	public Node startNode;
	public Node endNode;
	public int Weight;
}
