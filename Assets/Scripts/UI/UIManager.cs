using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject gameOver;

	public SatisfactionBarController satBar;
	public CapacityBoxController capacityBox;
	public OffloadBoxController offloadBox;
	public OnloadBoxController onloadBox;
	public TimerBarController timerBar;
	public LineProgressController lineProgress;
	public MoneyBoxController moneyBox;

	public GameObject missedStation;
	public GameObject emergencyStop;

	public GameCompleteController gameComplete;


	// Use this for initialization
	void Start () {
		instance = this;
	}

	void Awake(){
		missedStation.SetActive (false);
		emergencyStop.SetActive (false);
		gameOver.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerStats.GetInstance ().satisfaction <= 0) {
			TrainController.Instance.gameOver = true;
			gameOver.SetActive (true);
		}
	}

	public void GameOverTime(){
		gameOver.SetActive (true);
	}

	public void CompleteGame(){
		gameComplete.gameObject.SetActive (true);
	}

	public void MissStation(){
		StartCoroutine (showMissedStation ());
	}

	public void EmergencyStop(){
		StartCoroutine (showEmergencyStop ());
	}

	IEnumerator showMissedStation(){
		missedStation.SetActive (true);

		missedStation.GetComponent<Animator> ().Play ("Screen On Right");
		yield return new WaitForSeconds (3);

		missedStation.GetComponent<Animator> ().Play ("Screen Off Left");
		yield return new WaitForSeconds (3);
		missedStation.SetActive (false);
	}

	IEnumerator showEmergencyStop(){
		emergencyStop.SetActive (true);

		emergencyStop.GetComponent<Animator> ().Play ("Screen On Right");
		yield return new WaitForSeconds (3);

		emergencyStop.GetComponent<Animator> ().Play ("Screen Off Left");
		yield return new WaitForSeconds (3);
		emergencyStop.SetActive (false);
	}
}
