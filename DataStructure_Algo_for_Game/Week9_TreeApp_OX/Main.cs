using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

public class Main : MonoBehaviour
{
	string output = "";
	// Use this for initialization
	void Start ()
	{
		//1 = o, 2 = x
		char playerTurn = '1';
		TreeNode<string> root = new TreeNode<string> ("121212000");
		int numOfBlankCell = 3;
		generateTree (root, root.Data, playerTurn, numOfBlankCell);
	
		//Show output
		foreach (TreeNode<string> node in root) {
			string indent = CreateIndent (node.Level);
			output += indent + (node.Data + "\n" ?? "null\n");
		}

		Debug.Log (output);
	}

	void generateTree(TreeNode<string> treeNode, string value, char player, int numChild)
	{
		int countZero = 0;
		for (int i = 0; i < numChild; i++) 
		{
			StringBuilder _value = new StringBuilder (value);
			for (int j = countZero; j < value.Length; j++) 
			{
				char cell = _value [j];
				if (cell == '0') 
				{
					countZero = j + 1;
					_value.Replace (cell, player, j, 1);
					TreeNode<string> _treeNode = treeNode.AddChild (_value.ToString ());
					char _player = (player == '1' ? '2' : '1');
					generateTree (_treeNode, _treeNode.Data, _player, numChild - 1);
					break;
				}
			}
		}
	}

	public Vector2 scrollPosition = Vector2.zero;
	void OnGUI ()
	{
		scrollPosition = GUILayout.BeginScrollView (scrollPosition,
			GUILayout.Width (600),
			GUILayout.Height (800));
		GUILayout.Label (output);
		GUILayout.EndScrollView();
	}

	private static String CreateIndent (int depth)
	{
		StringBuilder sb = new StringBuilder ();
		for (int i = 0; i < depth; i++) {
			sb.Append ("    ");
		}
		return sb.ToString ();
	}
}


/*
 	TreeNode<string> node20 = node2.AddChild(null);
				TreeNode<string> node21 = node2.AddChild("node21");
					TreeNode<string> node210 = node21.AddChild("node210");
					TreeNode<string> node211 = node21.AddChild("node211");
			TreeNode<string> node3 = root.AddChild("node3");
				TreeNode<string> node30 = node3.AddChild("node30");
*/

public class TreeNode<T> : IEnumerable<TreeNode<T>>
{
	public T Data { get; set; }

	public TreeNode<T> Parent { get; set; }

	public ICollection<TreeNode<T>> Children { get; set; }

	public TreeNode (T data)
	{
		this.Data = data;
		this.Children = new LinkedList<TreeNode<T>> ();
	}

	public TreeNode<T> AddChild (T child)
	{
		TreeNode<T> childNode = new TreeNode<T> (child) { Parent = this };
		this.Children.Add (childNode);
		return childNode;
	}

	public Boolean IsRoot {
		get { return Parent == null; }
	}

	public Boolean IsLeaf {
		get { return Children.Count == 0; }
	}

	public int Level {
		get {
			if (this.IsRoot)
				return 0;
			return Parent.Level + 1;
		}
	}

	public override string ToString ()
	{
		return Data != null ? Data.ToString () : "[data null]";
	}


	IEnumerator IEnumerable.GetEnumerator ()
	{
		return GetEnumerator ();
	}

	public IEnumerator<TreeNode<T>> GetEnumerator ()
	{
		yield return this;
		foreach (var directChild in this.Children) {
			foreach (var anyChild in directChild)
				yield return anyChild;
		}
	}
}
