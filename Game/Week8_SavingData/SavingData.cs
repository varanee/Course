
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public class SavingData : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }

    void OnGUI(){

    
        //Save data
        if (GUI.Button (new Rect (0, 0, 200, 80), "Save PlayerPrefs data")) {
            PlayerPrefs.SetInt ("score", 100);
            PlayerPrefs.SetFloat ("HP", 80.5f);
            PlayerPrefs.SetString ("name", "aaa");
            PlayerPrefs.SetInt ("volume", 70);
            Debug.Log ("Save data");
        }

        //Load data
        if (GUI.Button (new Rect (200, 0, 200, 80), "Load PlayerPrefs data")) {
            int score = PlayerPrefs.GetInt ("score");
//            Debug.Log ("Load data = " + score);

            string name = PlayerPrefs.GetString ("name");
            Debug.Log ("name = " + name);
        }

        //Save data
        if (GUI.Button (new Rect (0, 100, 200, 80), 
            "Save Serializable data")) {

            BinaryFormatter b = new BinaryFormatter ();
            FileStream f = File.Open (
            Application.persistentDataPath + "/filename.dat",
            FileMode.Create);
            Player obj = new Player ();
            obj.name = "Peter";
            obj.health = 20;
            obj.score = 100;
            b.Serialize (f, obj);
            f.Close ();
            Debug.Log ("Save serializable data");
        }

        if (GUI.Button (new Rect (200, 100, 200, 80), 
                "Load Serializable data")) {

            if(File.Exists(Application.persistentDataPath + "/filename.dat"))
            {
                BinaryFormatter b = new BinaryFormatter ();
                FileStream f = File.Open (
                Application.persistentDataPath + "/filename.dat",
                FileMode.Open);
                Player player = (Player)b.Deserialize (f);
                Debug.Log (player.name);            
            }
        }
    }

    [System.Serializable]
    class Player{
        public string name;
        public int health;
        public int score;
    }
}




