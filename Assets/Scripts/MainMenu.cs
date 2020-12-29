using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void pressedPlay() {
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
	}

	public void pressedQuit() {
		Application.Quit ();
	}

	public void LoadMenu() {
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);	
	}

}
