using UnityEngine;
using System.Collections;

public class Global2 : MonoBehaviour
{
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

	void Start()
	{
		Global2.al = new ArrayList(Global2.CELLS.Length);
		for (int i = 0; i < Global2.CELLS.Length; i++)
		{
			Global2.al.Add(i);
		}

		//For each item
		for (int i = 1; i <= 8; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				int rand = Random.Range(0, Global2.al.Count);
				int inAl = int.Parse(Global2.al[rand].ToString());
				Global2.CELLS[inAl] = i;
				Global2.al.RemoveAt(rand);
			}
		}

		//Sound
		audioSources = new AudioSource[audioClips.Length];
		int s = 0;
		while (s < audioClips.Length)
		{
			GameObject gb = GameObject.Find(this.name);
			audioSources[s] = gb.AddComponent<AudioSource>();
			audioSources[s].clip = audioClips[s];
			s++;
		}
	} //End start()

	void Update()
	{
		if (Global2.PIC_MATCHES.Count == 2)
		{
			if (Global2.PIC_MATCHES[0].Equals(Global2.PIC_MATCHES[1]))
			{
				audioSources[1].Play();
				MATCH_CHK[int.Parse(Global2.CELL_MATCHES[0].ToString())] = 1;
				MATCH_CHK[int.Parse(Global2.CELL_MATCHES[1].ToString())] = 1;
				Global2.PIC_MATCHES.Clear();
				Global2.CELL_MATCHES.Clear();
			}
			else
			{
				delayTime += Time.deltaTime;
				if (delayTime > 1f)
				{
					delayTime = 0f;
					for (int i = 0; i < 2; i++)
					{
						int cellIndex = int.Parse(Global2.CELL_MATCHES[i].ToString());
						GameObject go = GameObject.Find("c" + cellIndex);
						go.GetComponent<Animator>().Play("cellAnim", 0, 0.1f);
					}
					Global2.PIC_MATCHES.Clear();
					Global2.CELL_MATCHES.Clear();
				}
			}
		}
	}
}