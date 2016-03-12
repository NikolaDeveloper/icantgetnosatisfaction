using UnityEngine;
using System.Collections;

public class PlayerStats {

	public static PlayerStats instance;

	public string trainLineName = "";
	public int playerMoney = 500;
	public int satisfaction = 100;

	public Color mainColor = Color.white;
	public Color secondaryColor = Color.white;
	public Color tertiaryColor = Color.white;

	private float gameStartTime;
	private float deadline = 300;

	public float getTimeRemaining() {
		float currentTime = Time.realtimeSinceStartup;
		return deadline - (currentTime - gameStartTime);
	}

	public static PlayerStats GetInstance () {
		
		if (instance == null) {
			instance = new PlayerStats();
		}

		return instance;

	}

	public void startNewGame () {

		gameStartTime = Time.realtimeSinceStartup;
		satisfaction = 100;
		playerMoney = 500;

	}

	public void removeSatisfaction (int val) {
		satisfaction -= val;
	}
		

}
