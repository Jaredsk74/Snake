using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	public Button A;
	public Button B;
	public Button C;
	public Button returnToTitle;

	public Text hScoreA;
	public Text hScoreB;
	public Text hScoreC;

	private int hScoreAInt;
	private int hScoreBInt;
	private int hScoreCInt;

	// Use this for initialization
	void Start() {
		A.onClick.AddListener(StartA);
		B.onClick.AddListener(StartB);
		C.onClick.AddListener(StartC);
		returnToTitle.onClick.AddListener(Return);

		hScoreAInt = PlayerPrefs.GetInt("HSCORE_A", 0);
		hScoreA.text = "High Score = " + hScoreAInt;
		hScoreBInt = PlayerPrefs.GetInt("HSCORE_B", 0);
		hScoreB.text = "High Score = " + hScoreBInt;
		hScoreCInt = PlayerPrefs.GetInt("HSCORE_C", 0);
		hScoreC.text = "High Score = " + hScoreCInt;

	}

	private void StartA() {
		SceneManager.LoadScene("A");
	}

	private void StartB() {
		SceneManager.LoadScene("B");
	}

	private void StartC() {
		SceneManager.LoadScene("C");
	}

	private void Return() {
		SceneManager.LoadScene("Menu");
	}
}
