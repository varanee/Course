using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Global2 : MonoBehaviour
{
	public static float levelStartDelay = 2f;

	private Text levelText;
	private GameObject levelImage;
	private bool doingSetup;
	private int level = 1;
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
		InitGame ();
	}


	/*private void OnLevelWasLoaded(int index)
	{
		level++;

		InitGame ();
	}*/

	void InitGame()
	{
		doingSetup = true;

		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		//Clear old resource
		//Set up new resource

	}

	void HideLevelImage()
	{
		levelImage.SetActive (false);
		doingSetup = false;
		Setup();
	}

	void Setup()
	{
		Global2.al = new ArrayList(Global2.CELLS.Length);
		for (int i = 0; i < Global2.CELLS.Length; i++)
		{
			Global2.al.Add(i);
		}

		int dim = 4;
	
		//Random for each item
		for (int i = 1; i <= 8; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				int rand = Random.Range(0, Global2.al.Count);

				int inAl = int.Parse(Global2.al[rand].ToString());

				Global2.CELLS[inAl] = i;

				cell = Instantiate(cell, new Vector3 (inAl/dim, inAl%dim, 0), Quaternion.identity);

				cell.name = "c" + inAl;

				Global2.al.RemoveAt(rand);
			}
		}
			

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

	} //End start()

	public static bool isAllowToClick = true;

	void Update()
	{
		if (Global2.PIC_MATCHES.Count == 2)
		{
			//Debug.Log ("Global2 Update");
			if (Global2.PIC_MATCHES[0].Equals(Global2.PIC_MATCHES[1]))
			{
				//audioSources[1].Play();
				Global2.MATCH_CHK[int.Parse(Global2.CELL_MATCHES[0].ToString())] = 1;
				Global2.MATCH_CHK[int.Parse(Global2.CELL_MATCHES[1].ToString())] = 1;
				Global2.PIC_MATCHES.Clear();
				Global2.CELL_MATCHES.Clear();
			}
			else
			{
				isAllowToClick = false;
				delayTime += Time.deltaTime;
				if (delayTime > 1f) //Wait 1 sec, then return to blank cell
				{
					Debug.unityLogger.Log ("Not match");
					delayTime = 0f;
					for (int i = 0; i < 2; i++)
					{
						int cellIndex = int.Parse(Global2.CELL_MATCHES[i].ToString());
						GameObject go = GameObject.Find("c" + cellIndex);
						go.GetComponent<Animator>().Play("cellAnim", 0, 0.0f);
					}
					Global2.PIC_MATCHES.Clear();
					Global2.CELL_MATCHES.Clear();
					isAllowToClick = true;
				}
			}
		}
	}
}