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

	public GameCompleteController gameComplete;


	// Use this for initialization
	void Start () {
		instance = this;
	}

	void Awake(){
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
}
