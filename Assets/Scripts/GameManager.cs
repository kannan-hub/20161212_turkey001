using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	Vector3 target;
	Vector3 diff;
	public float moveAmount = 0.01f;
	public float score = 0;
	public float countTime;
	public int life = 5;
	int hightLimitMin = 0;
	int hightLimitMax = 5;
	int upLimit = 18;
	int downLimit = -15;
	int rightLimit = 17;
	int leftLimit = -13;
	public bool isDust = false;

	float highScore;
	float longTime;
	string keyScore = "HIGH SCORE";
	string keyTime = "LONG TIME";

	public Text timeLabel;
	public Text scoreLabel;
	public LifePanel lifePanel;
	public GameObject spawner;

	// Use this for initialization
	void Start () {
		countTime = 0;
		highScore = PlayerPrefs.GetFloat (keyScore, 0);
		longTime = PlayerPrefs.GetFloat (keyTime, 0);
	}
	
	// Update is called once per frame
	void Update () {
		target = transform.position;

		if (transform.position.x > leftLimit && (Input.GetKey ("left") || Input.GetKey("a"))) {
			target.x -= moveAmount;
			transform.position = target;
		}
		if (transform.position.x < rightLimit && (Input.GetKey ("right") || Input.GetKey("d"))) {
			target.x += moveAmount;
			transform.position = target;
		}
		if (transform.position.z > downLimit && (Input.GetKey ("down") || Input.GetKey("s"))) {
			target.z -= moveAmount;
			transform.position = target;
		}
		if (transform.position.z < upLimit && (Input.GetKey ("up") || Input.GetKey("w"))) {
			target.z += moveAmount;
			transform.position = target;
		}
		
		var scroll = Input.GetAxis ("Mouse ScrollWheel");

		if (transform.position.y < hightLimitMax && scroll > 0) {
			target.y++;
			transform.position = target;
		}
		if (transform.position.y > hightLimitMin && scroll < 0) {
			target.y--;
			transform.position = target;
		}

		countTime += Time.deltaTime;

		timeLabel.text = "Time: " + countTime.ToString("##0.00") + " sec";
		scoreLabel.text = "Score : " + score + " point";
		lifePanel.UpdateLife (life);

		if (countTime < 30) {
			spawner.GetComponent<Spawner> ().badRate = 0.2f;
		} else if (countTime < 60) {
			spawner.GetComponent<Spawner> ().badRate = 0.4f;
		} else if (countTime < 100) {
			spawner.GetComponent<Spawner> ().badRate = 0.6f;
		} else {
			spawner.GetComponent<Spawner> ().badRate = 0.8f;
		}

		if (life <= 0) {
			Debug.Log ("GameOver");
			PlayerPrefs.SetFloat (keyScore, score);
			PlayerPrefs.SetFloat (keyTime, countTime);
			SceneManager.LoadScene ("Result");
		}

	}

	public void CalcScore(string scoreTag, bool isCatch){

		if (isCatch) {
			if (scoreTag == "Good") {
				score -= 200;
			} else if (scoreTag == "Bad") {
				score += 100;
			}
		} else {
			if (scoreTag == "Good") {
				score += 100;
			} else if (scoreTag == "Bad") {
				score -= 200;
				life--;
			}
		}
	}
}
