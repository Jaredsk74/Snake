using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour {

	public Button normal;
	public Button hard;
	public Button other;

	public Text hScoreN;
	public Text hScoreH;

	private int hScoreNInt;
	private int hScoreHInt;
	
	// Use this for initialization
	void Start() {
		normal.onClick.AddListener(NormalStart);
		hard.onClick.AddListener(HardStart);
		other.onClick.AddListener(ViewLevels);

		hScoreNInt = PlayerPrefs.GetInt("HSCORE", 0);
		hScoreN.text = "High Score = " + hScoreNInt;
		hScoreHInt = PlayerPrefs.GetInt("HSCORE_H", 0);
		hScoreH.text = "High Score = " + hScoreHInt;


	}

	private void ViewLevels() {
		SceneManager.LoadScene("LevelSelect");
	}

	private void HardStart() {
		SnakeMove.hard = true;
		NormalStart();
	}

	private void NormalStart() {
		SceneManager.LoadScene("Normal");
	}

}
