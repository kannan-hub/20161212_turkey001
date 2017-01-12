using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;

public class Spawner : MonoBehaviour {

	private float timer = 2;
	private float instantiateInterval = 2;
	private int santaCount = 0;
	private int santaMax = 10;

	[SerializeField]
	private int targetNum = 5;

	private bool santaKind = true;
	public float badRate; 

	// Use this for initialization
	void Start()
	{
		if (targets.Count () < targetNum + 1) {
			targetNum = targets.Count () - 2;
		}
		PermutationNumber (targets.Count(), targetNum); 
	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;

		if (timer < 0) {
			if (Random.Range (0.0f, 1.0f) < badRate) {
				santaKind = false;
			} else {
				santaKind = true;
			}

			if (santaCount < santaMax) {
				Spawn (santaKind);
				santaCount++;
			}

			instantiateInterval -= 0.1F;
			instantiateInterval = Mathf.Clamp (instantiateInterval, 1.0F, float.MaxValue);

			timer = instantiateInterval;
		}
	}

	public void Subtract(){
		santaCount--;
		return;
	}


	int index = 0;
	Dictionary<int, int[]> dic = new Dictionary<int, int[]> ();

	public GameObject goodQuery;
	public GameObject badQuery;
	public GameObject[] targets;
	public GameObject destTarget;

	public void Spawn(bool spawnKind){

		GameObject go;

		if (spawnKind) {
			go = Instantiate (goodQuery, this.transform.position, this.transform.rotation) as GameObject;
		} else {
			go = Instantiate (badQuery, this.transform.position, this.transform.rotation) as GameObject;
		}

		int route = Random.Range (0, dic.Count ());

		GameObject[] tars = new GameObject[dic[0].Length];

		for (int i = 0; i < tars.Length; i++) {
			tars[i] = targets[dic[route][i]-1]; //targetは0スタートなので、順列の数ー1が必要
		}

		go.GetComponent<NavgateCo> ().Init (tars);
		go.GetComponent<NavgateCo> ().parent = this.gameObject;
		go.GetComponent<NavgateCo> ().dest = destTarget;
		go.SetActive (true);
	}

	void PermutationNumber(int n, int r){
		var number = Enumerable.Range(1, n).Select (x => x).ToArray();

		PermurationNest (number, r, 0, "");
	}

	void PermurationNest(int[] n, int r, int columns, string resume){
		if (columns < r) {
			columns++;
			for (int i = 0; i < n.Length; i++) {
				string resumeClone = resume + n [i].ToString () + ",";
				int[] numClone = n.Where (x => x != n [i]).ToArray();
				PermurationNest (numClone, r, columns, resumeClone);

				if (columns == r) {
					dic.Add(index, n);
					index++;
				}
			}
		}
	}
}
