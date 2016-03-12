using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCompleteController : MonoBehaviour {


	public Text scoreText;
	public Text timerText;
	public Text satisfactionText;
	public Text capacityText;
	public Text finalText;

	public GameObject MaximumCapacity;

	void Awake(){
		
		MaximumCapacity.SetActive (false);
		gameObject.SetActive (false);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		scoreText.text = moneyScore().ToString();
		timerText.text = timerScore ().ToString ();

		capacityText.text = capacityScore ().ToString ();

		satisfactionText.text = "x" + satisfactionMultiplier().ToString ();

		finalText.text = getFinalScore ().ToString ();
	}

	int moneyScore(){
		int money = PlayerStats.GetInstance ().playerMoney;

		return Mathf.RoundToInt (money * 1.5f);
	}

	int timerScore(){
		int time = (int)PlayerStats.GetInstance ().getTimeRemaining ();

		return time * 100;	
	}

	int capacityScore(){
		int capacity = TrainController.Instance.passengerFull;

		int score = capacity;

		if (TrainController.Instance.passengerFull >= TrainController.Instance.passengerCapacity){
			score = score * 10;
			showMaximumCapacityText ();
		}

		return score;
			
	}

	void showMaximumCapacityText(){
		MaximumCapacity.SetActive (true);
		MaximumCapacity.GetComponent<Animator> ().Play ("MaxCapAnim");
	}

	int satisfactionMultiplier(){
		int sat = PlayerStats.GetInstance ().satisfaction;
		return (sat / 10);
	}


	int getFinalScore(){
		int score = moneyScore () + timerScore () + capacityScore ();

		return score * satisfactionMultiplier ();
	}
}
