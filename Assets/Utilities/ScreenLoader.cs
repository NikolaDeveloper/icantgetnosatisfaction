﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenLoader : MonoBehaviour {

	public void LoadGame(){
		PlayerStats.GetInstance().startNewGame();
		SceneManager.LoadScene ("Landscape");

	}

	public void ResetGame(){
		SceneManager.LoadScene ("titles");
	}
}
