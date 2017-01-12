using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

	float highScore;
	float longTime;
	string keyScore = "HIGH SCORE";
	string keyTime = "LONG TIME";

	public Text timeLabel;
	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		highScore = PlayerPrefs.GetFloat (keyScore, 0);
		longTime = PlayerPrefs.GetFloat (keyTime, 0);

		timeLabel.text = "Time: " + longTime.ToString("##0.00") + " sec";
		scoreLabel.text = "Score : " + highScore.ToString() + " point";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("return")) {
			SceneManager.LoadScene ("Start");
		}
	}
}
