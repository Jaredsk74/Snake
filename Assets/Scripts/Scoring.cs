using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

    public Text s;
    public Text h;
    private int hscore = 0;
	private string HScoreString;
	private Scene level;

	// Use this for initialization
	void Start () {
		level = SceneManager.GetActiveScene();

		switch (level.name) {
			case "Normal":
				if (SnakeMove.hard) {
					HScoreString = "HSCORE_H";			// Hard
				}
				else {
					HScoreString = "HSCORE";			// Normal
				}
				break;
			case "A":
				HScoreString = "HSCORE_A";				// Level A
				break;
			case "B":
				HScoreString = "HSCORE_B";				// Level B
				break;
			case "C":
				HScoreString = "HSCORE_C";				// Level C
				break;
		}

		hscore = PlayerPrefs.GetInt(HScoreString, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (SnakeMove.score > hscore) {
            hscore = SnakeMove.score;
            PlayerPrefs.SetInt(HScoreString, hscore);
        }
        s.text = "Score = " + SnakeMove.score;
        h.text = "High Score = " + hscore;
    }

}
