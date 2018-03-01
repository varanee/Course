using UnityEngine;
using System.Collections;

public class CellController2 : MonoBehaviour
{

	Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		animator.speed = 0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{         
			if (Global2.PIC_MATCHES.Count < 2)
			{
				Global2.audioSources[0].Play();       
				int cellIndex = 0;
				if(this.name.Length == 2) //c0, ..., c9
				{
					cellIndex = int.Parse(this.name[1].ToString());
				}
				else //c10,...,c15
				{
					string sIndex = this.name[1].ToString() + this.name[2].ToString();
					cellIndex = int.Parse(sIndex);
				}

				if (Global2.MATCH_CHK[cellIndex] == 0)
				{
					int frameIndex = Global2.CELLS[cellIndex];
					float frameNormalized = frameIndex / 9.0f;
					animator.Play("cellAnim", 0, frameNormalized);
					Global2.PIC_MATCHES.Add(frameIndex);
					Global2.CELL_MATCHES.Add(cellIndex);
				}
			}
		}
	}
}