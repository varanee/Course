using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public static bool isHumanClicked = false;
    public static ArrayList cells;
    void Start()
    {
        Cell.cells = new ArrayList(9);
        for(int i=0; i< 9; i++)
        {
            Cell.cells.Add(i);
	    }
    }

    // Update is called once per frame
    void Update()
    {		
    }

    void enemyTurn()
    {
		if (Cell.isHumanClicked && Cell.cells.Count > 0)
		{
	        int enemyGuess = Random.Range(0, cells.Count); //0, 1, 2, ..., 8
	        int enemyPicked = (int)Cell.cells[enemyGuess];
	        Cell.cells.RemoveAt(enemyGuess);
			GameObject cellObj = GameObject.Find ("" + enemyPicked);
			cellObj.GetComponent<SpriteRenderer>().color = Color.blue;
	        printCell();
			Cell.isHumanClicked = false;
		}
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked "+this.name);
            this.GetComponent<SpriteRenderer>().color = Color.red;
            Cell.cells.Remove(int.Parse(this.name));
            printCell();
            Cell.isHumanClicked = true;
			Invoke ("enemyTurn", 1f);
        }
    }

    void printCell()
    {
        string cellStr = "";
        for(int i=0; i<Cell.cells.Count; i++)
        {
            cellStr += " "+Cell.cells[i];
        }
        Debug.Log(cellStr);
    }
}
