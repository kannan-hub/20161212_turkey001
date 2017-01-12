using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;


public class test : MonoBehaviour {

	int index = 0;

	Dictionary<int, int[]> dic = new Dictionary<int, int[]> ();

	public GameObject[] targets;

	// Use this for initialization
	void Start () {
		PermutationNumber (targets.Count(), targets.Count()-2); 

		//Debug
//		foreach(var i in dic){
//			print(string.Format("{0}:{1}", i.Key, i.Value));
//			for (int j = 0; j < i.Value.Count(); j++) {
//				print (i.Value [j]);
//				print(dic[i.Key][j]);
//			}
//		}
		Spawn ();
	}

	public void Spawn(){

		int route = Random.Range (0, dic.Count ());
		print (route);

		GameObject[] tars = new GameObject[dic[0].Length];
		//print (tars.Length);

		for (int i = 0; i < tars.Length; i++) {
			tars[i] = targets[dic[route][i]-1]; //targetは0スタートなので、順列の数ー1が必要
			//print (dic[route][i]-1);
		}
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
