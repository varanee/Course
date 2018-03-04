using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
	public static float levelStartDelay = 2f;

	private Text levelText;
	private GameObject levelImage;
	private GameObject mainMenuImage;
	private bool doingSetup;
	private int level = 1;

	public ArrayList cells = new ArrayList();
	public GameObject cell;
	//1
	public static ArrayList PIC_MATCHES = new ArrayList();

	//2. Is able to be clicked or not
	public static int[] MATCH_CHK = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

	//3. To set random images 
	public static int[] CELLS = { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8 };

	//4. Cell Match
	public static ArrayList CELL_MATCHES = new ArrayList();

	//5. al
	public static ArrayList al;

	//6.
	float delayTime = 0f;

	//7.
	public AudioClip[] audioClips;

	//8.
	public static AudioSource[] audioSources;

	void Awake()
	{
		//InitGame ();
		launchMainMenu();
	}

	void launchMainMenu(){
		mainMenuImage = GameObject.Find ("MainMenu");
		mainMenuImage.SetActive (true);
	}

	public void startGame(){
		InitGame ();
	}

	private void OnLevelWasLoaded()
	{
		level++;
		InitGame2();
	}

	void InitGame()
	{
		mainMenuImage.SetActive (false);
		doingSetup = true;
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);
	}

	void InitGame2()
	{
		doingSetup = true;
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		resetResource();
		Invoke ("HideLevelImage", levelStartDelay);
	}

	void resetResource()
	{
		for (int i = 0; i < cells.Count; i++) {
			Destroy ((GameObject)cells [i]);
		}

		cells.Clear ();

		MATCH_CHK = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		CELLS = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8 };
		CELL_MATCHES.Clear();
		PIC_MATCHES.Clear ();
	}

	void HideLevelImage()
	{
		levelImage.SetActive (false);
		mainSetup();
		doingSetup = false;

	}

	void mainSetup()
	{
		cellSetup ();
		soundSetup ();
	}

	void cellSetup()
	{
		al = new ArrayList(CELLS.Length);

		for (int i = 0; i < CELLS.Length; i++)
		{
			al.Add(i);
		}

		int dim = 4;

		//Random for each item
		for (int i = 1; i <= 8; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				int rand = UnityEngine.Random.Range(0, al.Count);

				int inAl = int.Parse(al[rand].ToString());

				CELLS[inAl] = i;

				GameObject cellObj = Instantiate(cell, new Vector3 (inAl/dim, inAl%dim, 0), Quaternion.identity);

				cellObj.name = "c" + inAl;

				cells.Add (cellObj);

				al.RemoveAt(rand);
			}
		}
	}

	void soundSetup()
	{
		
		//Create Sound
		audioSources = new AudioSource[audioClips.Length];

		int s = 0;

		while (audioClips.Length > s)
		{
			GameObject gb = GameObject.Find(this.name);
			audioSources[s] = gb.AddComponent<AudioSource>();
			audioSources[s].clip = audioClips[s];
			s++;
		}
	}

	public static bool isAllowToClick = true;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
		if (PIC_MATCHES.Count == 2 && !doingSetup)
		{
			//Debug.Log ("Global2 Update");
			//if match
			if (PIC_MATCHES[0].Equals(PIC_MATCHES[1]))
			{
				audioSources[1].Play();
				MATCH_CHK[int.Parse(CELL_MATCHES[0].ToString())] = 1;
				MATCH_CHK[int.Parse(CELL_MATCHES[1].ToString())] = 1;
				PIC_MATCHES.Clear();
				CELL_MATCHES.Clear();

				//check game over, index = -1, otherwise index = 0;
				int index = Array.FindIndex(MATCH_CHK, item => item == 0);
				//Debug.Log ("index " + index);
				if (index == -1) {
					OnLevelWasLoaded ();
				}
			}
			else //Not match, return to blank cell
			{
				isAllowToClick = false;
				delayTime += Time.deltaTime;
				if (delayTime > 1f) //Wait 1 sec, then return to blank cell
				{
					Debug.unityLogger.Log ("Not match");
					delayTime = 0f;
					for (int i = 0; i < 2; i++)
					{
						int cellIndex = int.Parse(CELL_MATCHES[i].ToString());
						GameObject go = GameObject.Find("c" + cellIndex);
						go.GetComponent<Animator>().Play("cellAnim", 0, 0.0f);
					}
					PIC_MATCHES.Clear();
					CELL_MATCHES.Clear();
					isAllowToClick = true;
				}
			}
		}
	}
}