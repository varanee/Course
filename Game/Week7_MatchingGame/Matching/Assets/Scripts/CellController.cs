using UnityEngine;
using System.Collections;

public class CellController : MonoBehaviour
{

	Animator animator;

	// Use this for initialization
	void Awake()
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

	//When mouse is over
	void OnMouseOver()
	{
		//When mouse is clicked
		if (Input.GetMouseButtonDown(0) && GameManager.isAllowToClick)
		{         
			//Debug.Log ("Num of picked match = " + Global2.PIC_MATCHES.Count);
			int cellIndex = int.Parse (this.name.Remove (0, 1));
			//Debug.Log ("cell index = " + cellIndex);

			if (GameManager.MATCH_CHK [cellIndex] == 1)
				return;
			
			GameManager.audioSources [0].Play ();
			int frameIndex = GameManager.CELLS [cellIndex];
			float frameNormalized = frameIndex / 9.0f;
			animator.Play ("cellAnim", 0, frameNormalized);
			AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
			AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
			float currentFrame = myAnimatorClip[0].clip.length * animationState.normalizedTime;
			//Debug.Log ("current frame "+currentFrame);
		

			//If open one cell, and can click, and is blank cell
			if (GameManager.PIC_MATCHES.Count < 2 && GameManager.MATCH_CHK [cellIndex] == 0 && currentFrame == 0f) {
				//Debug.Log ("Click first");
				//Debug.unityLogger.Log("Click first");
				GameManager.PIC_MATCHES.Add (frameIndex);
				GameManager.CELL_MATCHES.Add (cellIndex);
			
			} else {
				//Debug.unityLogger.Log ("Click twice on the open cell");
			}
		}
	}
}