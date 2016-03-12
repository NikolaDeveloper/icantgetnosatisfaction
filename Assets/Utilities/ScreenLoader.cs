using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenLoader : MonoBehaviour {

	public void LoadGame(){
		SceneManager.LoadScene ("Landscape");

	}

	public void ResetGame(){
		SceneManager.LoadScene ("titles");
	}
}
