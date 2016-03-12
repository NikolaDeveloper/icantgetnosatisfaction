using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCompleteController : MonoBehaviour {


	public Text scoreText;
	public Text timerText;
	public Text satisfactionText;
	public Text finalText;

	void Awake(){
		gameObject.SetActive (false);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		scoreText.text = PlayerStats.GetInstance ().playerMoney.ToString ();
		timerText.text = PlayerStats.GetInstance ().getTimeRemaining ().ToString ();
		satisfactionText.text = PlayerStats.GetInstance ().satisfaction.ToString ();
		finalText.text = (PlayerStats.GetInstance ().playerMoney + PlayerStats.GetInstance ().getTimeRemaining () + PlayerStats.GetInstance ().satisfaction).ToString ();
	
	}
}
