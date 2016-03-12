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
	
	}
}
