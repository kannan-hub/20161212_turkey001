using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

	public void toMainScene (){
		SceneManager.LoadScene ("Main");
	}

	public void exitGame(){
		Application.Quit ();
	}
}
