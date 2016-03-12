using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerBarController : MonoBehaviour {

	public GameObject barFill;
	public Text timeLeftText;

	[Range(0,100)]
	public int value;
	public int timeLeft;
	
	// Update is called once per frame
	void Update () {

		value = Mathf.FloorToInt(PlayerStats.GetInstance().getRemainingPercentage());
		timeLeft = Mathf.FloorToInt(PlayerStats.GetInstance().getTimeRemaining());
	
		float progVal = (float)value;
		barFill.transform.localScale = new Vector3 (progVal / 100, 1);

		timeLeftText.text = timeLeft.ToString ("## 'seconds'");

	}
}
