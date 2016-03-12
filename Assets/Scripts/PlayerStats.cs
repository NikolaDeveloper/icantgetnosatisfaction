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
	private float deadline = 240;

	private bool wasGameOver = false;
	private float currentTime;

	public float getCurrentTime () {

		if (TrainController.Instance.gameOver) {

			if (!wasGameOver) {
				wasGameOver = true;
				currentTime = Time.realtimeSinceStartup;
			}

			return currentTime;
		} else {
			return Time.realtimeSinceStartup;
		}

	}

	public float getTimeRemaining() {

		float time = getCurrentTime();

		return deadline - (time - gameStartTime);
	}

	public float getRemainingPercentage() {
		float time = getCurrentTime();
		float percentage = ((time - gameStartTime) / deadline) * 100;
		return 100 - percentage;
	}

	public bool isGameOver () {
		if (this.getTimeRemaining () < 0) {
			return true;
		} else {
			return false;
		}
	}

    public PlayerStats()
    {
        mainColor = SettingsMainMenu.Instance.mainCol;
        secondaryColor = SettingsMainMenu.Instance.stripeCol;
        secondaryColor = SettingsMainMenu.Instance.windowsCol;
        trainLineName = SettingsMainMenu.Instance.trainName;

        deadline = SettingsMainMenu.Instance.DiffLevels[SettingsMainMenu.Instance.gameDifficultyLevel].deadline;
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
		TrainController.passengerCapacity = 350;
		TrainController.passengerFull = 200;

	}

	public void removeSatisfaction (int val) {
		satisfaction -= val;
	}
		

}
