using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

public class tree : MonoBehaviour {
	string output = "";
	// Use this for initialization
	void Start () {
		//1 = o, 2 = x
		char playerTurn = '2';
		TreeNode<string> root = new TreeNode<string> ("121001012"); //good example
		int numOfBlankCell = 3;

		generateTree (root, playerTurn, numOfBlankCell);

		//Show output
		int count = 0;
		foreach (TreeNode<string> node in root) {
			string indent = CreateIndent (node.Level);
			output += indent + (node.Data + "\n" ?? "null\n");
			count++;
		}
		Debug.Log ("Num of nodes = " + count);
		Debug.Log (output);
	}

	void generateTree(TreeNode<string> treeNode, char player, int numOfBlankCell)
	{
		int countZero = 0;
		for (int i = 0; i < numOfBlankCell; i++) 
		{
			StringBuilder _value = new StringBuilder (treeNode.Data);
			for (int j = countZero; j < _value.Length; j++) 
			{
				char cell = _value [j];
				if (cell == '0') 
				{
					countZero = j + 1;
					_value.Replace (cell, player, j, 1);

					int whoWin = checkGameStatusV2(_value.ToString());
					if (whoWin == 1) {
						Debug.Log (whoWin + " win "+_value.ToString());
						break;
					} else if (whoWin == 2) {
						Debug.Log (whoWin + " win."+_value.ToString());
				
					//if (checkGameStatus(_value.ToString())) {
					//	Debug.Log (_value.ToString());
						break;
					} else {
						TreeNode<string> _treeNode = treeNode.AddChild (_value.ToString ());
						char _player = (player == '1' ? '2' : '1');
						generateTree (_treeNode, _player, numOfBlankCell - 1);
						Debug.Log (" Draw "+_value.ToString());
						break;
					}
				}
			}
		}
	}
	//If true is game over
	public bool checkGameStatus(string treeNodeValue){

		string firstRow = treeNodeValue.Substring (0, 3);
		if (firstRow.Equals ("111") || firstRow.Equals ("222")) {
			return true;
		}

		string secondRow = treeNodeValue.Substring (3, 3);
		if (secondRow.Equals ("111") || secondRow.Equals ("222")) {
			return true;
		}

		string thirdRow = treeNodeValue.Substring (6, 3);
		if (thirdRow.Equals ("111") || thirdRow.Equals ("222")) {
			return true;
		}

		string firstCol = treeNodeValue.ToCharArray () [0].ToString () +
			treeNodeValue.ToCharArray () [3].ToString () +
			treeNodeValue.ToCharArray () [6].ToString ();

		if (firstCol.Equals ("111") || firstCol.Equals ("222")) {
			return true;
		}

		string secondCol = treeNodeValue.ToCharArray () [1].ToString () +
			treeNodeValue.ToCharArray () [4].ToString () +
			treeNodeValue.ToCharArray () [7].ToString ();

		if (secondCol.Equals ("111") || secondCol.Equals ("222")) {
			return true;
		}

		string thirdCol = treeNodeValue.ToCharArray () [2].ToString () +
			treeNodeValue.ToCharArray () [5].ToString () +
			treeNodeValue.ToCharArray () [8].ToString ();

		if (thirdCol.Equals ("111") || thirdCol.Equals ("222")) {
			return true;
		}

		string diagLeft = treeNodeValue.ToCharArray () [0].ToString () +
			treeNodeValue.ToCharArray () [4].ToString () +
			treeNodeValue.ToCharArray () [8].ToString ();

		if (diagLeft.Equals ("111") || diagLeft.Equals ("222")) {
			return true;
		}

		string diagRight = treeNodeValue.ToCharArray () [2].ToString () +
			treeNodeValue.ToCharArray () [4].ToString () +
			treeNodeValue.ToCharArray () [6].ToString ();

		if (diagRight.Equals ("111") || diagRight.Equals ("222")) {
			return true;
		}

		return false;
	}


	//Return 1,2, and -1
	public int checkGameStatusV2(string treeNodeValue){

		string firstRow = treeNodeValue.Substring (0, 3);
		if (firstRow.Equals ("111")) {
			return 1;
		}
		if (firstRow.Equals ("222")) {
			return 2;
		}

		string secondRow = treeNodeValue.Substring (3, 3);
		if (secondRow.Equals ("111")) {
			return 1;
		}
		if (secondRow.Equals ("222")) {
			return 2;
		}

		string thirdRow = treeNodeValue.Substring (3, 3);
		if (thirdRow.Equals ("111")) {
			return 1;
		}
		if (thirdRow.Equals ("222")) {
			return 2;
		}
			

		string firstCol = treeNodeValue.ToCharArray () [0].ToString () +
			treeNodeValue.ToCharArray () [3].ToString () +
			treeNodeValue.ToCharArray () [6].ToString ();

		if (firstCol.Equals ("111")){
			return 1;
		}

		if (firstCol.Equals ("222")){
			return 1;
		}

		string secondCol = treeNodeValue.ToCharArray () [1].ToString () +
			treeNodeValue.ToCharArray () [4].ToString () +
			treeNodeValue.ToCharArray () [7].ToString ();

		if (secondCol.Equals ("111")){
			return 1;
		}

		if (secondCol.Equals ("222")){
			return 1;
		}

		string thirdCol = treeNodeValue.ToCharArray () [2].ToString () +
			treeNodeValue.ToCharArray () [5].ToString () +
			treeNodeValue.ToCharArray () [8].ToString ();

		if (thirdCol.Equals ("111")){
			return 1;
		}

		if (thirdCol.Equals ("222")){
			return 1;
		}

		string diagLeft = treeNodeValue.ToCharArray () [0].ToString () +
			treeNodeValue.ToCharArray () [4].ToString () +
			treeNodeValue.ToCharArray () [8].ToString ();

		if (diagLeft.Equals ("111")) {
			return 1;
		}

		if (diagLeft.Equals ("222")) {
			return 2;
		}

		string diagRight = treeNodeValue.ToCharArray () [2].ToString () +
			treeNodeValue.ToCharArray () [4].ToString () +
			treeNodeValue.ToCharArray () [6].ToString ();

		if (diagRight.Equals ("111"))
		{
			return 1;
		}

		if (diagRight.Equals ("111"))
		{
			return 2;
		}

		return -1; // -1 is draw
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

	//iterative
	int Factorial(int i){
		int ret = i;
		for (int x = i-1; x >= 1; x--) {
			ret = ret * x; 
		}
		return ret;
	}

	//Recursive
	int Factorial2(int i){
		if (i == 1)
			return i;
		else
			return Factorial2 (i-1) * i;
	}

	private static String CreateIndent(int depth)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < depth; i++)
		{
			sb.Append("    ");
		}
		return sb.ToString();
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

	public TreeNode(T data){
		this.Data = data;
		this.Children = new LinkedList<TreeNode<T>>();
	}

	public TreeNode<T> AddChild(T child)    {
		TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
		this.Children.Add(childNode);
		return childNode;
	}

	public Boolean IsRoot    {
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

	public override string ToString(){
		return Data != null ? Data.ToString() : "[data null]";
	}


	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<TreeNode<T>> GetEnumerator()
	{
		yield return this;
		foreach (var directChild in this.Children)
		{
			foreach (var anyChild in directChild)
				yield return anyChild;
		}
	}
}
